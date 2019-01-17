using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class Parse250 : IParse
    {
        MyBatis myBatis = new MyBatis();

        public string Parse(List<byte> Data)
        {
            try
            {
                //RunService RunSer = new RunService();
                //RGTPOSManager RGTPOSMgr = new RGTPOSManager();
                //TroubleLogService TroubleLogSer = new TroubleLogService();
                //RealTimeCUVDataService RealTimeCUVDataSer = new RealTimeCUVDataService();

                int i = 2;
                //温度
                for (int j = 0; j < Data.Count; j++)
                {
                    if (Data[j] == 0x0E)
                    {
                        i = j;
                        break;
                    }
                }
                float tcv = MachineControlProtocol.HexConverToFloat(Data[i + 2], Data[i + 3], Data[i + 4], Data[i + 5]);
                myBatis.UpdateLatestCUVPanelTemperature(tcv * 10);
                //LogService.Log(string.Format("React Panel Temp.:{0};", tcv * 10), LogType.Debug);
                //试剂余量
                for (int j = 0; j < Data.Count; j++)
                {
                    if (Data[j] == 0x2B)
                    {
                        i = j;
                        break;
                    }
                }
                int R1P = MachineControlProtocol.HexConverToDec(Data[i + 1], Data[i + 2]);
                int R1V = MachineControlProtocol.HexConverToDec(Data[i + 3], Data[i + 4]);
                R1P = R1P > 90 ? (R1P - 90) : R1P;
                UpdateLatestRgtVol(1, R1P, R1V);
                RgtWarning(1, R1P);
                int R2P = MachineControlProtocol.HexConverToDec(Data[i + 5], Data[i + 6]);
                int R2V = MachineControlProtocol.HexConverToDec(Data[i + 7], Data[i + 8]);
                UpdateLatestRgtVol(2, R2P, R2V);
                RgtWarning(2, R2P);
                //LogService.Log(string.Format("R1 Pos.:{0} R1 Vol:{1} R2 Pos.:{2} R2 Vol:{3}", R1P, R1V, R2P, R2V), LogType.Debug);
                //查找错误报头
                int erindex = 0;
                for (int j = 0; j < Data.Count(); j++)
                {
                    if (Data[j] == 0x1C)
                    {
                        erindex = j;
                        break;
                    }
                }
                int errcount = Data[erindex + 2] - 0x30;
                if (errcount > 0)
                {
                    for (int j = 0; j < errcount; j++)
                    {
                        int index = (erindex + 6) + j * 7;

                        TroubleLog t = new TroubleLog();
                        t.TroubleCode = string.Format("{0}{1}{2}{3}{4}{5}{6}", (char)Data[index], (char)Data[index + 1], (char)Data[index + 2], (char)Data[index + 3], (char)Data[index + 4], (char)Data[index + 5], (char)Data[index + 6]);
                        t.TroubleType = TROUBLETYPE.ERR;
                        t.TroubleUnit = "设备";
                        t.TroubleInfo = null;
                        myBatis.TroubleLogSave("TroubleLogSave", t);

                        //LogService.Log("Err code:" + t.TroubleCode, LogType.Debug);
                    }
                }
            }
            catch
            {
                TroubleLog t = new TroubleLog();
                t.TroubleCode = @"0X25001";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = "设备";
                t.TroubleInfo = null;
                myBatis.TroubleLogSave("TroubleLogSave", t);
            }
            return null;
        }

        private void UpdateLatestRgtVol(int d, int p, int v)
        {
            int v2 = myBatis.GetValidPercent(d, p);

            myBatis.UpdateReagentValidPercent(v, d, p);

            if (p > 0)
            {
                ReagentSettingsInfo reaSettingInfo = myBatis.GetReagentSettingsInfoByPos(d, p);
                if (reaSettingInfo != null && v > 0 && v2 == 0)
                {
                    myBatis.UpdateNorTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                    myBatis.UpdateQCTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                    myBatis.UpdateCalibTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                    myBatis.UpdateCalibCurveState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                }
            }
        }


        void RgtWarning(int d, int p)
        {
            float rgtwarncount = myBatis.GetRgtWarnCount();
            float rgtleastcount = myBatis.GetRgtLeastCount();

            ReagentStateInfoR1R2 rgp = myBatis.GetReagentStateInfoByPos(d, p);
            ReagentSettingsInfo rsi = myBatis.GetReagentSettingsInfoByPos(d, p);

            if (rgp != null)
            {
                AssayProjectParamInfo arp = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = rsi.ProjectName, SampleType = rsi.ReagentType });
                int c = 0;
                int v = System.Convert.ToInt32(rsi.ReagentContainer.Substring(0, rsi.ReagentContainer.IndexOf("ml"))) * (rgp.ValidPercent -2) / 100 * 1000;
                
                switch (d)
                {
                    case 1:
                        c = arp.Reagent1VolSettings == 0 ? 0 : v / arp.Reagent1VolSettings;
                        if (c < rgtleastcount)
                        {
                            //if (RunSer.IsMutiRgtEnable() == true)//多试剂位开关标志
                            //{
                            //    RGTPosition mrgt = RGTPOSMgr.GetEnableMutiRgtPosition(rgp);
                            //    if (mrgt != null)
                            //    {
                            //        RGTPOSMgr.BetweenMutiRgtPositionAndRgtPositionChange(mrgt, rgp);

                            //        TroubleLog trouble = new TroubleLog();
                            //        trouble.TroubleCode = @"0000773";
                            //        trouble.TroubleType = TROUBLETYPE.WARN;
                            //        trouble.TroubleUnit = "试剂";
                            //        trouble.TroubleInfo = "试剂位" + p + "项目" + rgp.Assay + "试剂1由于余量不足开始启用其多试剂位" + mrgt.Position;//string.Format("试剂位{0}项目{1}试剂1由于余量不足开始启用其多试剂位{2}. ", p, rgp.Assay, mrgt.Position);
                            //        TroubleLogSer.Save(trouble);
                            //    }
                            //    else
                            //    {
                            //        if (RunSer.IsLockRgtEnable() == true)
                            //        {
                            //            rgp.IsLocked = true;
                            //            RGTPOSMgr.UpdateLockState(rgp);

                            //            TroubleLog trouble = new TroubleLog();
                            //            trouble.TroubleCode = @"0000773";
                            //            trouble.TroubleType = TROUBLETYPE.WARN;
                            //            trouble.TroubleUnit = "试剂";
                            //            trouble.TroubleInfo = MyResources.Instance.FindResource("Parse0839").ToString() + p + MyResources.Instance.FindResource("Parse0832").ToString() + rgp.Assay + MyResources.Instance.FindResource("Parse08312").ToString();//string.Format("试剂位{0}项目{1}试剂1由于余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                            //            TroubleLogSer.Save(trouble);
                            //        }
                            //        else
                            //        {
                            //            TroubleLog trouble = new TroubleLog();
                            //            trouble.TroubleCode = @"0000773";
                            //            trouble.TroubleType = TROUBLETYPE.ERR;
                            //            trouble.TroubleUnit = "试剂";
                            //            trouble.TroubleInfo = MyResources.Instance.FindResource("Parse0839").ToString() + p + MyResources.Instance.FindResource("Parse0832").ToString() + rgp.Assay + MyResources.Instance.FindResource("Parse08313").ToString();// string.Format("试剂位{0}项目{1}试剂1由于余量不足. ", p, rgp.Assay);
                            //            TroubleLogSer.Save(trouble);
                            //        }
                            //    }
                            //}
                            //else
                            //{
                                //if (RunSer.IsLockRgtEnable() == true)
                                //{
                            rsi.Locked = true;
                            myBatis.UpdateLockState("R1", rsi);

                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000773";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = "试剂";
                            trouble.TroubleInfo = "试剂位" + p + "项目" + rgp.ProjectName + "余量不足将锁定其对应的工作表";//string.Format("试剂位{0}项目{1}试剂1由于余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                                //}
                                //else
                                //{
                                //    TroubleLog trouble = new TroubleLog();
                                //    trouble.TroubleCode = @"0000773";
                                //    trouble.TroubleType = TROUBLETYPE.ERR;
                                //    trouble.TroubleUnit = "试剂";
                                //    trouble.TroubleInfo = MyResources.Instance.FindResource("Parse0839").ToString() + p + MyResources.Instance.FindResource("Parse0832").ToString() + rgp.Assay + MyResources.Instance.FindResource("Parse08315").ToString();// string.Format("试剂位{0}项目{1}试剂1由于余量不足. ", p, rgp.Assay);
                                //    TroubleLogSer.Save(trouble);
                                //}
                            //}
                        }

                        if (c < rgtwarncount && c > rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000773";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = "试剂";
                            trouble.TroubleInfo = "试剂位" + p + "项目" + rgp.ProjectName + "试剂1即将耗尽";// string.Format("试剂位{0}项目{1}:试剂1余量即将耗尽. ", p, rgp.Assay);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                            return;
                        }
                        break;
                    case 2:
                        c = arp.Reagent2VolSettings == 0 ? 0 : v / arp.Reagent2VolSettings;
                        if (c < rgtleastcount)
                        {
                            //if (RunSer.IsMutiRgtEnable() == true)//多试剂位开关标志
                            //{
                            //    RGTPosition mrgt = RGTPOSMgr.GetEnableMutiRgtPosition(rgp);
                            //    if (mrgt != null)
                            //    {
                            //        RGTPOSMgr.BetweenMutiRgtPositionAndRgtPositionChange(mrgt, rgp);

                            //        TroubleLog trouble = new TroubleLog();
                            //        trouble.TroubleCode = @"0000773";
                            //        trouble.TroubleType = TROUBLETYPE.WARN;
                            //        trouble.TroubleUnit = "试剂";
                            //        trouble.TroubleInfo = MyResources.Instance.FindResource("Parse0839").ToString() + p + MyResources.Instance.FindResource("Parse0832").ToString() + rgp.Assay + MyResources.Instance.FindResource("Parse08317").ToString() + mrgt.Position;// string.Format("试剂位{0}项目{1}试剂2由于余量不足开始启用其多试剂位{2}. ", p, rgp.Assay, mrgt.Position);
                            //        TroubleLogSer.Save(trouble);
                            //    }
                            //    else
                            //    {
                            //        if (RunSer.IsLockRgtEnable() == true)
                            //        {
                            //            rgp.IsLocked = true;
                            //            RGTPOSMgr.UpdateLockState(rgp);

                            //            TroubleLog trouble = new TroubleLog();
                            //            trouble.TroubleCode = @"0000773";
                            //            trouble.TroubleType = TROUBLETYPE.WARN;
                            //            trouble.TroubleUnit = "试剂";
                            //            trouble.TroubleInfo = MyResources.Instance.FindResource("Parse0839").ToString() + p + MyResources.Instance.FindResource("Parse0832").ToString() + rgp.Assay + MyResources.Instance.FindResource("Parse08318").ToString();// string.Format("试剂位{0}项目{1}试剂2由于余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                            //            TroubleLogSer.Save(trouble);
                            //        }
                            //        else
                            //        {
                            //            TroubleLog trouble = new TroubleLog();
                            //            trouble.TroubleCode = @"0000773";
                            //            trouble.TroubleType = TROUBLETYPE.ERR;
                            //            trouble.TroubleUnit = "试剂";
                            //            trouble.TroubleInfo = MyResources.Instance.FindResource("Parse0839").ToString() + p + MyResources.Instance.FindResource("Parse0832").ToString() + rgp.Assay + MyResources.Instance.FindResource("Parse08319").ToString();// string.Format("试剂位{0}项目{1}试剂2余量不足. ", p, rgp.Assay);
                            //            TroubleLogSer.Save(trouble);
                            //        }
                            //    }
                            //}
                            //else
                            //{
                                //if (RunSer.IsLockRgtEnable() == true)
                                //{
                            rsi.Locked = true;
                            myBatis.UpdateLockState("R2", rsi);

                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000773";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = "试剂";
                            trouble.TroubleInfo = "试剂位" + p + "项目" + rgp.ProjectName + "试剂2余量不足将锁定其对应的工作表";//string.Format("试剂位{0}项目{1}试剂2余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                                //}
                                //else
                                //{
                                //    TroubleLog trouble = new TroubleLog();
                                //    trouble.TroubleCode = @"0000773";
                                //    trouble.TroubleType = TROUBLETYPE.ERR;
                                //    trouble.TroubleUnit = "试剂";
                                //    trouble.TroubleInfo = MyResources.Instance.FindResource("Parse0839").ToString() + p + MyResources.Instance.FindResource("Parse0832").ToString() + rgp.Assay + MyResources.Instance.FindResource("Parse08321").ToString();// string.Format("试剂位{0}项目{1}试剂2余量不足. ", p, rgp.Assay);
                                //    TroubleLogSer.Save(trouble);
                                //}
                            //}
                        }
                        if (c < rgtwarncount && c > rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000775";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = "试剂";
                            trouble.TroubleInfo = "试剂位" + p + "项目" + rgp.ProjectName + "试剂2余量即将耗尽";// string.Format("试剂位{0}项目{1}:试剂2余量即将耗尽. ", p, rgp.Assay);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                            return;
                        }
                        break;
                }
            }
        }
    }
}
