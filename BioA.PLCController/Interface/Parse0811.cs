using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Service;
using CLMode.Protocol;
using CLMode.Machine;
using CLMode.Entities;

namespace CLMode.Interface
{
    ////NT400清洗状态解码器
    public class Parse0811 : IParse
    {
        public string Parse(List<byte> Data)
        {
            if (Data.Count < 904)
            {
                LogService.Log("非法数据包:" + MachineControlProtocol.BytelistToHexString(Data), LogType.Debug);
                return null;
            }

            RunService RunSer = new RunService();
            RGTPOSManager RGTPOSMgr = new RGTPOSManager();
            TroubleLogService TroubleLogSer = new TroubleLogService();

            int Pt1stWn = 0;
            int Pt3ndWn = 0;
            int Pt14thWn = 0;

            int BlkCUVNO = MachineControlProtocol.HexConverToDec(Data[904], Data[905], Data[906]);
            int BlkWN = MachineControlProtocol.HexConverToDec(Data[2], Data[3], Data[4]);

            for (int i = 2; i < 886; i = i + 17)
            {
                int WN = MachineControlProtocol.HexConverToDec(Data[i], Data[i + 1], Data[i + 2]);
                int PT = MachineControlProtocol.HexConverToDec(Data[i + 3], Data[i + 4]);
              
               
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

               
            }
            //温度
            float tcv = MachineControlProtocol.HexConverToFloat(Data[887], Data[888], Data[889], Data[890]);
            RunSer.UpdateLatestCUVPanelTemperature(tcv * 10);
            float tr1 = MachineControlProtocol.HexConverToFloat(Data[891], Data[892], Data[893], Data[894]);
            RunSer.UpdateLatestR1PanelTemperature(tr1 * 10);
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

                int errcount = Data[erindex + 2] - 0x30;
                //Console.WriteLine(string.Format("there is {0} errors!", errcount));
                for (int i = 0; i < errcount; i++)
                {
                    int index = (erindex + 3) + i * 7;

                    string cmdname = string.Format("{0}{1}", (char)Data[index], (char)Data[index + 1]);
                    if (cmdname == "77" && Data[index + 2] == 0x30)//R1
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"0000770";
                        trouble.TroubleType = TROUBLETYPE.ERR;
                        trouble.TroubleUnit = @"设备";
                        trouble.TroubleInfo = string.Format("试剂位清洗液添加失败. ");
                        TroubleLogSer.Save(trouble);

                    }
                    if (cmdname == "77" && Data[index + 2] == 0x31)//R2
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"0000771";
                        trouble.TroubleType = TROUBLETYPE.ERR;
                        trouble.TroubleUnit = @"设备";
                        trouble.TroubleInfo = string.Format("试剂位清洗液添加失败. ");
                        TroubleLogSer.Save(trouble);

                    }
                    if (cmdname == "57" && Data[index + 2] == 0x30)//SMP
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"0000570";
                        trouble.TroubleType = TROUBLETYPE.ERR;
                        trouble.TroubleUnit = @"设备";
                        trouble.TroubleInfo = string.Format("样本位清洗液添加失败. ");
                        TroubleLogSer.Save(trouble);
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

        void RgtWarning(int d, int p)
        {
            RGTPOSManager RGTPOSMgr = new RGTPOSManager();
            RunService RunSer = new RunService();
            TroubleLogService TroubleLogSer = new TroubleLogService();

            int rgtwarncount = RunSer.GetRgtWarnCount();
            int rgtleastcount = RunSer.GetRgtLeastCount();

            RGTPosition rgp = RGTPOSMgr.Get(1, p.ToString());
            if (rgp != null)
            {
                int c = 0;
                int v = rgp.CType.Volume * rgp.ValidPercent / 100 * 1000;
                switch (rgp.AssayPara)
                {
                    case "R1":
                        c = v / 250;
                        if (c < rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000772";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂耗尽. ", p);
                            TroubleLogSer.Save(trouble);

                            return;
                        }
                        if (c < rgtwarncount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000773";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂即将耗尽. ", p);
                            TroubleLogSer.Save(trouble);
                            return;
                        }
                        break;
                    case "R2":
                        c = v / 150;
                        if (c < rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000774";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂耗尽. ", p);
                            TroubleLogSer.Save(trouble);

                            return;
                        }
                        if (c < rgtwarncount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000775";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂即将耗尽. ", p);
                            TroubleLogSer.Save(trouble);
                            return;
                        }
                        break;
                }
            }
        }

    }
}
