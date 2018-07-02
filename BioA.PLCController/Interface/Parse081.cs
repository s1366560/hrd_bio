using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Service;
using CLMode.Protocol;
using CLMode.Entities;
using CLMode.Machine;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Parse08NT
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse08NT
 * 创建时间：       4/25/2012 11:04:33 AM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace CLMode.Interface
{
    //NT400解码器
    public class Parse081 : IParse
    {
        public string Parse(List<byte> Data)
        {
            if (Data.Count < 904)
            {
                LogService.Log("非法数据包:"+MachineControlProtocol.BytelistToHexString(Data), LogType.Debug);
                return null;
            }
            
            RunService RunSer = new RunService();
            RGTPOSManager RGTPOSMgr = new RGTPOSManager();
            TroubleLogService TroubleLogSer = new TroubleLogService();
            RealTimeCUVDataService RealTimeCUVDataSer = new RealTimeCUVDataService();

            int Pt1stWn = 0;
            int Pt3ndWn = 0;
            int Pt14thWn = 0;

            int BlkCUVNO = MachineControlProtocol.HexConverToDec(Data[904], Data[905], Data[906]);
            int BlkWN = MachineControlProtocol.HexConverToDec(Data[2], Data[3], Data[4]);
            RealTimeCUVDataSer.SaveCuvNumber(BlkWN, BlkCUVNO);

            for (int i = 2; i < 886; i = i + 17)
            {
                int WN = MachineControlProtocol.HexConverToDec(Data[i], Data[i + 1], Data[i + 2]);
                int PT = MachineControlProtocol.HexConverToDec(Data[i + 3], Data[i + 4]);
                float PWL = MachineControlProtocol.HexConverToFloat(Data[i + 5], Data[i + 6], Data[i + 7], Data[i + 8], Data[i + 9], Data[i + 10]);
                float SWL = MachineControlProtocol.HexConverToFloat(Data[i + 11], Data[i + 12], Data[i + 13], Data[i + 14], Data[i + 15], Data[i + 16]);

                if (PWL > -0.000001 && PWL < 0.000001)
                {
                    PWL = 6.8f;
                }
                else
                {
                    PWL = (float)Math.Log10(10 / PWL) * MachineInfo.LightSpan;
                }
                if (SWL > -0.000001 && SWL < 0.000001)
                {
                    SWL = 6.8f;
                }
                else
                {
                    SWL = (float)Math.Log10(10 / SWL) * MachineInfo.LightSpan;
                }

                if (WN != 0 && PT != 0)
                {
                    RealTimeCUVDataService.SaveABS(WN, PT, PWL, SWL);
                }
                if (PT == 1)
                {
                    Pt1stWn = WN;
                }
                if (PT == 3)
                {
                    Pt3ndWn = WN;
                }
                if (PT == 14)
                {
                    Pt14thWn = WN;
                }

                //Console.WriteLine(string.Format("WN:{0}PT:{1}PWL:{2}SWL:{3}", WN, PT, PWL, SWL));
            }
            //温度
            float tcv = MachineControlProtocol.HexConverToFloat(Data[887], Data[888], Data[889], Data[890]);
            RunSer.UpdateLatestCUVPanelTemperature(tcv*10);
            float tr1 = MachineControlProtocol.HexConverToFloat(Data[891], Data[892], Data[893], Data[894]);
            RunSer.UpdateLatestR1PanelTemperature(tr1*10);
            LogService.Log(string.Format("反应盘温度:{0};试剂盘温度 :{1}", tcv * 10, tr1 * 10), LogType.Debug);
            //试剂余量
            int R1P = MachineControlProtocol.HexConverToDec(Data[896], Data[897]);
            int R1V = MachineControlProtocol.HexConverToDec(Data[898], Data[899]);
            RGTPOSMgr.UpdateLatestRgtVol(1, R1P, R1V);
            RgtWarning(1, R1P);
            int R2P = MachineControlProtocol.HexConverToDec(Data[900], Data[901]);
            int R2V = MachineControlProtocol.HexConverToDec(Data[902], Data[903]);
            RGTPOSMgr.UpdateLatestRgtVol(1, R2P, R2V);
            RgtWarning(1, R2P);
            LogService.Log(string.Format("R1位置:{0} R1体积:{1} R2位置:{2} R2体积:{3}", R1P, R1V, R2P, R2V), LogType.Debug);
            //查找错误报头
            int erindex = 0;
            for (int i = 0; i < Data.Count(); i++)
            {
                if (Data[i] == 0x1C)
                {
                    erindex = i;
                    break;
                }
            }
            //错误信息
            if (Data[erindex] == 0x1C)
            {
                LogService.Log(MachineControlProtocol.BytelistToHexString(Data), LogType.Debug);

                int errcount = Data[erindex+2] - 0x30;
                //Console.WriteLine(string.Format("there is {0} errors!", errcount));
                for (int i = 0; i < errcount; i++)
                {
                    int index = (erindex+3) + i * 7;

                    string cmdname = string.Format("{0}{1}", (char)Data[index], (char)Data[index + 1]);
                    if (cmdname == "77" && Data[index + 2] == 0x30)//R1
                    {
                        RealTimeCUVDataService.RunningErrors(Pt1stWn, "R1");

                        Result r = new RealTimeCUVDataService().GetResultFromRealTimeWorkNum(Pt1stWn);
                        if (r != null)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000770";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("样本{0}项目{1}反应进程{2}:添加试剂1失败. ", r.SMPNO, r.ItemName, r.TCNO);
                            TroubleLogSer.Save(trouble);
                        }
                        
                    }
                    if (cmdname == "77" && Data[index + 2] == 0x31)//R2
                    {
                        RealTimeCUVDataService.RunningErrors(Pt14thWn, "R2");

                        Result r = new RealTimeCUVDataService().GetResultFromRealTimeWorkNum(Pt14thWn);
                        if (r != null)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000771";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("样本{0}项目{1}反应进程{2}:添加试剂2失败. ", r.SMPNO, r.ItemName, r.TCNO);
                            TroubleLogSer.Save(trouble);
                        }
                       
                    }
                    if (cmdname == "57" && Data[index + 2] == 0x30)//SMP
                    {
                        RealTimeCUVDataService.RunningErrors(Pt3ndWn, "SMP");

                        Result r = new RealTimeCUVDataService().GetResultFromRealTimeWorkNum(Pt3ndWn);
                        if (r != null)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000570";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("样本{0}项目{1}反应进程{2}:添加样本失败. ", r.SMPNO, r.ItemName, r.TCNO);
                            TroubleLogSer.Save(trouble);
                        }
                       
                    }
                    

                    TroubleLog t = new TroubleLog();
                    t.TroubleCode = string.Format("{0}{1}{2}{3}{4}{5}{6}", (char)Data[index], (char)Data[index + 1], (char)Data[index + 2], (char)Data[index + 3], (char)Data[index + 4], (char)Data[index + 5], (char)Data[index + 6]);
                    t.TroubleType = TROUBLETYPE.ERR;
                    t.TroubleUnit = @"设备";
                    t.TroubleInfo = null;
                    TroubleLogSer.Save(t);

                    LogService.Log("测试运行设备发生错误:" + t.TroubleCode, LogType.Debug);
                }
            }
            return null;
        }

        void RgtWarning(int d,int p)
        {
            RGTPOSManager RGTPOSMgr = new RGTPOSManager();
            AssayRunParaService AssayRunParaSer = new AssayRunParaService();
            RunService RunSer = new RunService();
            TroubleLogService TroubleLogSer = new TroubleLogService();

            int rgtwarncount =RunSer.GetRgtWarnCount();
            int rgtleastcount = RunSer.GetRgtLeastCount();

            RGTPosition rgp = RGTPOSMgr.Get(1, p.ToString());
            if (rgp != null)
            {
                AssayRunPara arp = AssayRunParaSer.Get(rgp.Assay) as AssayRunPara;
                int c = 0;
                int v = rgp.CType.Volume * rgp.ValidPercent / 100 * 1000;
                switch (rgp.AssayPara)
                {
                    case "R1":
                        c = arp.R1Vol == 0 ? 0 : v / arp.R1Vol;
                        if (c < rgtleastcount)
                        {
                            if (RunSer.IsMutiRgtEnable() == true)//多试剂位开关标志
                            {
                                RGTPosition mrgt = RGTPOSMgr.GetEnableMutiRgtPosition(rgp);
                                if (mrgt != null)
                                {
                                    RGTPOSMgr.BetweenMutiRgtPositionAndRgtPositionChange(mrgt, rgp);

                                    TroubleLog trouble = new TroubleLog();
                                    trouble.TroubleCode = @"0000773";
                                    trouble.TroubleType = TROUBLETYPE.WARN;
                                    trouble.TroubleUnit = @"试剂";
                                    trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂1由于余量不足开始启用其多试剂位{2}. ", p, rgp.Assay, mrgt.Position);
                                    TroubleLogSer.Save(trouble);
                                }
                                else
                                {
                                    if (RunSer.IsLockRgtEnable() == true)
                                    {
                                        rgp.IsLocked = true;
                                        RGTPOSMgr.UpdateLockState(rgp);

                                        TroubleLog trouble = new TroubleLog();
                                        trouble.TroubleCode = @"0000773";
                                        trouble.TroubleType = TROUBLETYPE.WARN;
                                        trouble.TroubleUnit = @"试剂";
                                        trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂1由于余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                                        TroubleLogSer.Save(trouble);
                                    }
                                    else
                                    {
                                        TroubleLog trouble = new TroubleLog();
                                        trouble.TroubleCode = @"0000773";
                                        trouble.TroubleType = TROUBLETYPE.ERR;
                                        trouble.TroubleUnit = @"试剂";
                                        trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂1由于余量不足. ", p, rgp.Assay);
                                        TroubleLogSer.Save(trouble);
                                    }
                                }
                            }
                            else
                            {
                                 if (RunSer.IsLockRgtEnable() == true)
                                    {
                                        rgp.IsLocked = true;
                                        RGTPOSMgr.UpdateLockState(rgp);

                                        TroubleLog trouble = new TroubleLog();
                                        trouble.TroubleCode = @"0000773";
                                        trouble.TroubleType = TROUBLETYPE.WARN;
                                        trouble.TroubleUnit = @"试剂";
                                        trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂1由于余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                                        TroubleLogSer.Save(trouble);
                                    }
                                    else
                                    {
                                        TroubleLog trouble = new TroubleLog();
                                        trouble.TroubleCode = @"0000773";
                                        trouble.TroubleType = TROUBLETYPE.ERR;
                                        trouble.TroubleUnit = @"试剂";
                                        trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂1由于余量不足. ", p, rgp.Assay);
                                        TroubleLogSer.Save(trouble);
                                    }
                                }
                        }

                        if (c < rgtwarncount && c > rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000773";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}项目{1}:试剂1余量即将耗尽. ", p, rgp.Assay);
                            TroubleLogSer.Save(trouble);
                            return;
                        }
                        break;
                    /*
                    case "R2":
                        c = arp.R2Vol == 0 ? 0 : v / arp.R2Vol;
                        if (c < rgtleastcount)
                        {
                            if (RunSer.IsMutiRgtEnable() == true)//多试剂位开关标志
                            {
                                RGTPosition mrgt = RGTPOSMgr.GetEnableMutiRgtPosition(rgp);
                                if (mrgt != null)
                                {
                                    RGTPOSMgr.BetweenMutiRgtPositionAndRgtPositionChange(mrgt, rgp);

                                    TroubleLog trouble = new TroubleLog();
                                    trouble.TroubleCode = @"0000773";
                                    trouble.TroubleType = TROUBLETYPE.WARN;
                                    trouble.TroubleUnit = @"试剂";
                                    trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂2由于余量不足开始启用其多试剂位{2}. ", p, rgp.Assay, mrgt.Position);
                                    TroubleLogSer.Save(trouble);
                                }
                                else
                                {
                                    if (RunSer.IsLockRgtEnable() == true)
                                    {
                                        rgp.IsLocked = true;
                                        RGTPOSMgr.UpdateLockState(rgp);

                                        TroubleLog trouble = new TroubleLog();
                                        trouble.TroubleCode = @"0000773";
                                        trouble.TroubleType = TROUBLETYPE.WARN;
                                        trouble.TroubleUnit = @"试剂";
                                        trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂2由于余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                                        TroubleLogSer.Save(trouble);
                                    }
                                    else
                                    {
                                        TroubleLog trouble = new TroubleLog();
                                        trouble.TroubleCode = @"0000773";
                                        trouble.TroubleType = TROUBLETYPE.ERR;
                                        trouble.TroubleUnit = @"试剂";
                                        trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂2余量不足. ", p, rgp.Assay);
                                        TroubleLogSer.Save(trouble);
                                    }
                                }
                            }
                            else
                            {
                                if (RunSer.IsLockRgtEnable() == true)
                                {
                                    rgp.IsLocked = true;
                                    RGTPOSMgr.UpdateLockState(rgp);

                                    TroubleLog trouble = new TroubleLog();
                                    trouble.TroubleCode = @"0000773";
                                    trouble.TroubleType = TROUBLETYPE.WARN;
                                    trouble.TroubleUnit = @"试剂";
                                    trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂2余量不足将锁定其对应的工作表. ", p, rgp.Assay);
                                    TroubleLogSer.Save(trouble);
                                }
                                else
                                {
                                    TroubleLog trouble = new TroubleLog();
                                    trouble.TroubleCode = @"0000773";
                                    trouble.TroubleType = TROUBLETYPE.ERR;
                                    trouble.TroubleUnit = @"试剂";
                                    trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂2余量不足. ", p, rgp.Assay);
                                    TroubleLogSer.Save(trouble);
                                }
                            }
                        }
                        if (c < rgtwarncount && c > rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000775";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}项目{1}:试剂2余量即将耗尽. ", p, rgp.Assay);
                            TroubleLogSer.Save(trouble);
                            return;
                        }
                        break;
                     */
                }
            }
        }
        
    }
}
