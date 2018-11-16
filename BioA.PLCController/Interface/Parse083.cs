using BioA.Common;
using BioA.Common.Machine;
using BioA.PLCController.Service;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //NT-1000数据包解码器
    public class Parse083 : IParse
    {
        MyBatis myBatis = new MyBatis();
        ResultService resultService = new ResultService();
        public string Parse(List<byte> Data)
        {
            //LogService.Log("开始解析测试数据包--------------", LogType.Trace,"log083.lg");
            string machinestate = null;


            //int Pt1stWn = 0;
            //int Pt3ndWn = 0;
            //int Pt26thWn = 0;

            //空白值比色杯
            int blki = 0;
            for (int j = 0; j < Data.Count(); j++)
            {
                if (Data[j] == 0x2B)
                {
                    blki = j;
                    blki += 8;
                    break;
                }
            }
            //比色表编号
            int BlkCUVNO = MachineControlProtocol.HexConverToDec(Data[blki+1], Data[blki + 2], Data[blki + 3]);
            int BlkWN = MachineControlProtocol.HexConverToDec(Data[2], Data[3], Data[4]);
            myBatis.SaveCuvNumber(BlkWN, BlkCUVNO);
            //LogService.Log(string.Format("比色杯:{0}空白值:{1}", BlkCUVNO, BlkWN), LogType.Debug);
            //生化数据包
            int i = 2; 
            int count = 1;
            int PressErrorWn = 0;
            while (count<=44)
            {
                //工作盘号
                int WN = MachineControlProtocol.HexConverToDec(Data[i], Data[i + 1], Data[i + 2]);
                //比色杯测光点
                int PT = MachineControlProtocol.HexConverToDec(Data[i + 3], Data[i + 4]);
                //主波长比色杯值
                float PWL = MachineControlProtocol.HexConverToFloat(Data[i + 5], Data[i + 6], Data[i + 7], Data[i + 8], Data[i + 9], Data[i + 10]);
                //次波长比色杯值
                float SWL = MachineControlProtocol.HexConverToFloat(Data[i + 11], Data[i + 12], Data[i + 13], Data[i + 14], Data[i + 15], Data[i + 16]);

                if (PWL > -0.000001 && PWL < 0.000001)
                {
                    PWL = 3.5f;
                }
                else
                {
                    PWL = (float)Math.Log10(10 / PWL) * RunConfigureUtility.LightSpan;
                }
                if (SWL > -0.000001 && SWL < 0.000001)
                {
                    SWL = 3.5f;
                }
                else
                {
                    SWL = (float)Math.Log10(10 / SWL) * RunConfigureUtility.LightSpan;
                }

                if (WN != 0 && PT != 0)
                {
                    SaveABS(WN, PT, PWL, SWL);
                }

                if (PT == 2)
                {
                    PressErrorWn = WN;
                }
                //R1,R2,SMP错误日志对照
                //if (PT == 1)1.10797185
                //{
                //    Pt1stWn = WN;
                //}
                //if (PT == 3)
                //{
                //    Pt3ndWn = WN;
                //}
                //if (PT == 26)
                //{
                //    Pt26thWn = WN;
                //}

                i = i + 17;

                //Console.WriteLine(string.Format("WN:{0}PT:{1}PWL:{2}SWL:{3}", WN, PT, PWL, SWL));
                //LogService.Log(string.Format("WN:{0}PT:{1}PWL:{2}SWL:{3}", WN, PT, PWL, SWL), LogType.Debug);

                count++;
            }

            //温度
            for (int j = 0; j < Data.Count; j++)
            {
                if (Data[j] == 0x0E)
                {
                    i = j;
                    break;
                }
            }
            myBatis.UpdateLatestWaterState(Data[i + 1], Data[i + 2]);
            ProcessWaterState(Data[i + 1], Data[i + 2]);
            //压力传感器有
            int s2 = Data[i + 2] - 0x30;
            if ((s2 & 0x02) == 0x02)
            {
                RunningErrors(PressErrorWn, "PE");
                RealTimeCUVDataInfo rt = new RealTimeCUVDataInfo();
                bool bExistRes = resultService.GetResultBeExistFromRealTimeWorkNum(PressErrorWn, out rt);
                if (bExistRes)
                {
                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleCode = @"0000571";
                    trouble.TroubleType = TROUBLETYPE.ERR;
                    trouble.TroubleUnit = "设备";
                    trouble.TroubleInfo = "样本" + rt.SmpNo + "项目" + rt.Assay + "测试时检测到堵针" + "反应进程:" + rt.TC.ToString();// string.Format("样本{0}项目{1}测试时检测到堵针,反应进程{2}。", r.SMPNO, r.ItemName, r.TCNO);
                    myBatis.TroubleLogSave("TroubleLogSave", trouble);
                }
            }
            if (IsWaterExchangeEnable(Data[i + 1], Data[i + 2]) == false)
            {
                //标示设备液路有异常
                machinestate = "ME";
            }

            int inttemp = MachineControlProtocol.HexConverToDec(Data[i + 3], Data[i + 4], Data[i + 5]);
            myBatis.UpdateLatestCUVPanelTemperature(inttemp * 0.1f);

            //LogService.Log(string.Format("反应盘温度:{0};", tcv * 10), LogType.Debug);
            //试剂余量
            for (int j = 0; j < Data.Count; j++)
            {
                if (Data[j] == 0x2B)
                {
                    i = j;
                    break;
                }
            }
            int R1P = MachineControlProtocol.HexConverToDec(Data[i+1], Data[i+2]);
            int R1V = MachineControlProtocol.HexConverToDec(Data[i+3], Data[i+4]);
            R1P = R1P > 45 ? (R1P - 45) : R1P;
            UpdateLatestRgtVol(1, R1P, R1V);
            RgtWarning(1, R1P);
            //LogService.Log(string.Format("R1位置:{0} R1体积:{1}", R1P, R1V), LogType.Trace, "log083.lg");
            int R2P = MachineControlProtocol.HexConverToDec(Data[i+5], Data[i+6]);
            int R2V = MachineControlProtocol.HexConverToDec(Data[i+7], Data[i+8]);
            UpdateLatestRgtVol(2, R2P, R2V);
            RgtWarning(2, R2P);
            //LogService.Log(string.Format("R2位置:{0} R2体积:{1}", R2P, R2V), LogType.Trace, "log083.lg");
            //查找错误报头
            int erindex = -1;
            for (int j = 0; j < Data.Count(); j++)
            {
                if (Data[j] == 0x1C)
                {
                    erindex = j;
                    break;
                }
            }
            if (erindex > 0)//发生设备故障
            {
                int errcount = Data[erindex + 2] - 0x30;
                for (int j = 0; j < errcount; j++)
                {
                    int index = (erindex + 3) + j * 10;

                    int erwn = MachineControlProtocol.HexConverToDec(Data[index], Data[index + 1], Data[index + 2]);
                    string ercode = string.Format("{0}{1}{2}{3}{4}{5}{6}", (char)Data[index + 3], (char)Data[index + 4], (char)Data[index + 5], (char)Data[index + 6], (char)Data[index + 7], (char)Data[index + 8], (char)Data[index + 9]);

                    TroubleLog t = new TroubleLog();
                    t.TroubleCode = ercode;
                    t.TroubleType = TROUBLETYPE.ERR;
                    t.TroubleUnit = "设备";
                    t.TroubleInfo = null;
                    myBatis.TroubleLogSave("TroubleLogSave", t);

                    RealTimeCUVDataInfo rt = new RealTimeCUVDataInfo();
                    bool bExistRes = resultService.GetResultBeExistFromRealTimeWorkNum(PressErrorWn, out rt);
                    string cmdname = string.Format("{0}{1}", (char)Data[index + 3], (char)Data[index + 4]);
                    if (cmdname == "77" && Data[index + 5] == 0x30)//R1
                    {
                        RunningErrors(erwn, "R1");

                        if (bExistRes)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000770";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = "设备";
                            trouble.TroubleInfo = "样本" + rt.SmpNo + "项目" + rt.Assay + "添加试剂1失败" + "反应进程:" + rt.TC.ToString();//string.Format("样本{0}项目{1}反应进程{2},添加试剂1失败. ", r.SMPNO, r.ItemName, r.TCNO);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                        }
                    }
                    if (cmdname == "77" && Data[index + 5] == 0x31)//R2
                    {
                        RunningErrors(erwn, "R2");

                        if (bExistRes)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000771";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = "设备";
                            trouble.TroubleInfo = "样本" + rt.SmpNo + "项目" + rt.Assay + "添加试剂2失败" + "反应进程:" + rt.TC.ToString(); //string.Format("样本{0}项目{1}反应进程{2},添加试剂2失败. ", r.SMPNO, r.ItemName, r.TCNO);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                        }

                    }
                    if (cmdname == "57" && Data[index + 5] == 0x30)//SMP
                    {
                        RunningErrors(erwn, "SMP");

                        if (bExistRes)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000570";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = "设备";
                            trouble.TroubleInfo = "样本" + rt.SmpNo + "项目" + rt.Assay + "添加样本失败" + "反应进程:" + rt.TC.ToString(); //string.Format("样本{0}项目{1}反应进程{2},添加样本失败. ", r.SMPNO, r.ItemName, r.TCNO);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                        }
                    }
                }
            }

            return machinestate;
        }

        private bool IsWaterExchangeEnable(int state1, int state2)
        {
            //高位状态码
            int s1 = state1 - 0x30;

            //纯水槽低位浮球
            if ((s1 & 0x01) == 0x01)
            {
                return false;
            }

            //反应槽液位
            if ((s1 & 0x02) == 0x02)
            {
                return false;
            }

            //溢流罐液位报警
            if ((s1 & 0x04) == 0x04)
            {
                return false;
            }

            //真空罐液位报警
            if ((s1 & 0x08) == 0x08)
            {
                return false;
            }

            //低位状态码
            int s2 = state2 - 0x30;

            //恒温槽浮球错误
            if ((s2 & 0x04) == 0x04)
            {
                return false;
            }

            //纯水槽高位浮球
            if ((s2 & 0x08) == 0x08)
            {
                return false;
            }

            return true;
        }

        byte[] GetData(List<byte> data, int s, int l)
        {
            byte[] v = new byte[l];
            for (int i = 0, p = s; i < l; i++, p++)
            {
                v[i] = data[p];
            }
            return v;
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
                int v = System.Convert.ToInt32(rsi.ReagentContainer.Substring(0, rsi.ReagentContainer.IndexOf("ml"))) * rgp.ValidPercent / 100 * 1000;

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
        public void RunningErrors(int wn, string er)
        {

            RealTimeCUVDataInfo rt = myBatis.GetRealTimeCUVDataByWorkNo(wn);
            if (rt == null)
            {
                return;
            }

            
            switch (rt.WorkType)
            {
                case WORKTYPE.N:
                case WORKTYPE.E:
                    SampleResultInfo samResInfo = myBatis.GetNORResult(rt);
                    samResInfo.Remarks += "|" + er + "|";
                    myBatis.UpdateNORResultRunLog(samResInfo);
                    break;
                case WORKTYPE.B:
                case WORKTYPE.S:
                    CalibrationResultinfo calibResInfo = myBatis.QueryCalibResultInfoByTCNO(rt);
                    calibResInfo.Remarks += "|" + er + "|";
                    myBatis.UpdateCalibResultRunLog(calibResInfo);
                    break;
                case WORKTYPE.C:
                    QualityControlResultInfo qcResInfo = myBatis.GetQCResult(rt);
                    qcResInfo.Remarks += "|" + er + "|";
                    myBatis.UpdateQCResultRunLog(qcResInfo);
                    break;
            }
        }


        private void SaveABS(int WN, int PT, float PW, float SW)
        {
            myBatis.UpdateRealTimeState(WN, PT);
            RealTimeCUVDataInfo realTimeData = myBatis.QueryRealTimeCUVDataTC(WN);
            if (realTimeData == null)
            {
                return;
            }

            if (PT == RunConfigureUtility.BlankPoint)
            {
                myBatis.UpdateBlkABSData(realTimeData.TC, PW, SW);
            }
            else
            {
                myBatis.UpdateABSData(realTimeData.TC, PT, PW, SW);
            }

            if (PT == RunConfigureUtility.LastPoint)
            {
                switch (realTimeData.WorkType)
                {
                    case WORKTYPE.N:
                    case WORKTYPE.E:
                        myBatis.UpdateSMPScheduleFinishCount(realTimeData.SmpNo, realTimeData.Assay, myBatis.GetSMPScheduleFinishCount(realTimeData.SmpNo, realTimeData.Assay) + 1);
                        break;
                    case WORKTYPE.B:
                    case WORKTYPE.S:
                        CalibrationResultinfo calibResult = myBatis.QueryCalibResultInfo(realTimeData.Assay, realTimeData.SmpNo, realTimeData.TC);
                        myBatis.UpdateSDTScheduleFinishCount(calibResult.SampleNum, calibResult.ProjectName, calibResult.CalibratorName, calibResult.CalibrationDT,
                                myBatis.GetSDTScheduleFinishCount(calibResult.SampleNum, calibResult.ProjectName, calibResult.CalibratorName, calibResult.CalibrationDT) + 1);
                        break;
                    case WORKTYPE.C:
                        myBatis.UpdateQCScheduleFinishCount(realTimeData.SmpNo, realTimeData.Assay, myBatis.GetQCScheduleFinishCount(realTimeData.SmpNo, realTimeData.Assay) + 1);
                        break;
                }

                myBatis.DeleteRealTimeCUVData(WN);
                RealTimeCalculate(realTimeData);

                switch (realTimeData.WorkType)
                {
                    case WORKTYPE.N:
                    case WORKTYPE.E:
                        TaskInfo t = new TaskInfo();
                        t = myBatis.GetSMPSchedule(realTimeData.SmpNo, realTimeData.Assay);
                        if (t != null && t.FinishTimes == t.InspectTimes)
                        {
                            myBatis.UpdateSampleStatePerform(t, TaskState.SUCC);
                            //2018 9/4
                            //myBatis.UpdteTaskState(t.SampleNum.ToString(), t.ProjectName);
                            myBatis.UpdateTaskStatePerform(t, TaskState.SUCC);
                        }
                        break;
                    case WORKTYPE.B:
                    case WORKTYPE.S:
                        CalibrationResultinfo calibResult = myBatis.QueryCalibResultInfo(realTimeData.Assay, realTimeData.SmpNo, realTimeData.TC);
                        CalibratorinfoTask s = myBatis.GetSDTSchedule(calibResult.ProjectName, calibResult.SampleNum, calibResult.CalibratorName,calibResult.CalibrationDT);
                        if (s != null && s.SendTimes == s.InspectTimes && s.SendTimes == s.FinishTimes)
                        {
                            //myBatis.ClearSDTSchedules(s.SampleNum, s.ProjectName);
                            
                            myBatis.UpdateSDTResultState(s, TaskState.SUCC);
                        }
                        break;
                    case WORKTYPE.C:
                        QCTaskInfo c = myBatis.GetQCSchedule(realTimeData.SmpNo, realTimeData.Assay) as QCTaskInfo;
                        //if (c != null && c.FinishTimes >= c.InspectTimes)
                        //{
                        //myBatis.ClearQCSchedules(c.SampleNum, c.ProjectName);
                        //}
                        if (c != null && c.InspectTimes == c.SendTimes && c.SendTimes == c.FinishTimes)
                        {
                            myBatis.UpdateQCTaksState(c.SampleNum, c.ProjectName);
                        }
                        break;
                }
            }
        }

        private void RealTimeCalculate(RealTimeCUVDataInfo realTimeData)
        {
            SampleResultInfo norResult = null;
            switch (realTimeData.WorkType)
            {
                case WORKTYPE.N:
                case WORKTYPE.E:
                    norResult = myBatis.GetNORResult(realTimeData) as SampleResultInfo;
                    if (norResult != null)
                    {
                        if (resultService.IsResultRight(norResult.ToString()) == true)
                        {
                            norResult.AbsValue = resultService.GetResultAbsValue(norResult);
                            float rc = resultService.GetResultConcValue(norResult);
                            norResult.ConcResult = rc < 0 ? 0 : rc;
                        }
                        else
                        {
                            norResult.AbsValue = -1;
                            norResult.ConcResult = -1;
                        }
                        //时时结果处理
                        myBatis.UpdateCurrentNORResult(norResult);
                       // resultService.ProcessCurrentNormalResultCalValue(norResult); 针对计算项目，在前台计算

                        resultService.AnalyzeResult(norResult);

                        //resultService.SetIntradayNorResultCalculated(true, norResult);
                    }
                    break;
                case WORKTYPE.B:
                case WORKTYPE.S:
                    CalibrationResultinfo calibResInfo = myBatis.QueryCalibResultInfoByTCNO(realTimeData);
                    if (calibResInfo != null)
                    {
                        if (resultService.IsResultRight(calibResInfo.Remarks) == true)
                        {
                            calibResInfo.CalibAbs = resultService.GetResultAbsValue(calibResInfo);
                            /*显示定标实际测量值
                            AssayRunPara ar = new AssayRunParaService().Get(R.ItemName) as AssayRunPara;
                            if(ar!=null)
                            {
                                float k = (ar.SDTVol.VolPre+ar.SDTVol.VolDil)/ar.SDTVol.VolPre;
                                R.RAbsValue = (float.Parse(R.RAbsValue) * k).ToString("#0.0000");
                            }
                            * */
                            //R.RConcValue = resultService.GetResultConcValue(R).ToString("#0.0000");
                        }
                        else
                        {
                            calibResInfo.CalibAbs = -1;
                           // R.RConcValue = "NA";
                        }
                        calibResInfo.TCNO = realTimeData.TC;
                        myBatis.UpdateSDTTaskState(calibResInfo.SampleNum, calibResInfo.ProjectName, calibResInfo.CalibratorName, calibResInfo.CalibrationDT, TaskState.SUCC);
                        
                        resultService.OnSDTCalibrateCurve(calibResInfo);
                        //resultService.SetSDTResultCalculated(true, R);
                    }
                    break;
                case WORKTYPE.C:
                    QualityControlResultInfo qCResInfo = myBatis.GetQCResult(realTimeData);
                    if (qCResInfo != null)
                    {
                        if (resultService.IsResultRight(qCResInfo.Remarks) == true)
                        {
                            qCResInfo.AbsValue = resultService.GetResultAbsValue(qCResInfo);
                            qCResInfo.ConcResult = resultService.GetResultConcValue(qCResInfo);
                        }
                        else
                        {
                            qCResInfo.AbsValue = -1;
                            qCResInfo.ConcResult = -1;
                        }
                        myBatis.UpdateQCResult(qCResInfo);
                        //resultService.SetQCResultCalculated(true, R);
                    }
                    break;
            }
        }

        public void ProcessWaterState(int state1, int state2)
        {
            //高位状态码
            int s1 = state1 - 0x30;

            //纯水槽低位浮球
            if ((s1 & 0x01) == 0x01)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10001";
                trouble.TroubleInfo = "纯水槽低位浮球状态异常";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //反应槽液位
            if ((s1 & 0x02) == 0x02)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10002";
                trouble.TroubleInfo = "反应槽液位状态异常";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //溢流罐液位报警
            if ((s1 & 0x04) == 0x04)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10003";
                trouble.TroubleInfo = "溢流罐液位状态异常";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //真空罐液位报警
            if ((s1 & 0x08) == 0x08)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10004";
                trouble.TroubleInfo = "真空罐液位状态异常";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //低位状态码
            int s2 = state2 - 0x30;
            //恒温值有问题 OK
            if ((s2 & 0x01) == 0x01)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.WARN;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10005";
                trouble.TroubleInfo = "恒温槽水温不在理想状态下";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //压力传感器有 OK
            if ((s2 & 0x02) == 0x02)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.WARN;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10006";
                trouble.TroubleInfo = "样本吸量器压力传感器状态异常";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //恒温槽浮球错误
            if ((s2 & 0x04) == 0x04)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10007";
                trouble.TroubleInfo = "恒温槽浮球状态异常";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //纯水槽高位浮球
            if ((s2 & 0x08) == 0x08)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"10008";
                trouble.TroubleInfo = "纯水槽高位浮球状态异常";
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }
        }

        private void UpdateLatestRgtVol(int d,int p,int v)
        {
            int v2 = myBatis.GetValidPercent(d, p);

            myBatis.UpdateReagentValidPercent(v, d, p);

            if (p > 0)
            {
                ReagentSettingsInfo reaSettingInfo = myBatis.GetReagentSettingsInfoByPos(d, p);
                if (reaSettingInfo != null && v > 0 && v2 == 0)
                {
                    //myBatis.UpdateNorTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                    myBatis.UpdateQCTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                    myBatis.UpdateCalibTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                    myBatis.UpdateCalibCurveState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                }
            }
        }
    }
}
