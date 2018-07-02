using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Service;
using CLMode.Protocol;
using CLMode.Entities;
using CLMode.Machine;
using CLMode.Land;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Parse08
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse08
 * 创建时间：       4/25/2012 11:04:07 AM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace CLMode.Interface
{
    public class Parse080 : IParse
    {
        public string Parse(List<byte> Data)
        {
            RunService RunSer = new RunService();
            RGTPOSManager RGTPOSMgr = new RGTPOSManager();
            TroubleLogService TroubleLogSer = new TroubleLogService();

            int Pt1stWn = 0;
            int Pt2ndWn = 0;
            int Pt12thWn = 0;
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

                //Console.WriteLine(string.Format("WN:{0}PT:{1}PWL:{2}SWL:{3}", WN, PT, PWL, SWL));
                if (WN != 0 && PT != 0)
                {
                    RealTimeCUVDataService.SaveABS(WN, PT, PWL, SWL);
                }

                if (PT == 2)
                {
                    Pt2ndWn = WN;
                }
                if (PT == 12)
                {
                    Pt12thWn = WN;
                }
            }
            //温度
            float tcv = MachineControlProtocol.HexConverToFloat(Data[887], Data[888], Data[889], Data[890]);
            RunSer.UpdateLatestCUVPanelTemperature(tcv);
            float tr1 = MachineControlProtocol.HexConverToFloat(Data[891], Data[892], Data[893], Data[894]);
            RunSer.UpdateLatestR1PanelTemperature(tr1);
            //Console.WriteLine(string.Format("the CUV-Panel temperature:{0};the R1-Panel temperature :{1}", tcv * 10, tr1 * 10));
            //试剂余量
            int R1P = MachineControlProtocol.HexConverToDec(Data[896], Data[897]);
            int R1V = MachineControlProtocol.HexConverToDec(Data[898], Data[899]);
            RGTPOSMgr.UpdateLatestRgtVol(1, R1P, R1V);
            int R2P = MachineControlProtocol.HexConverToDec(Data[900], Data[901]);
            int R2V = MachineControlProtocol.HexConverToDec(Data[902], Data[903]);
            RGTPOSMgr.UpdateLatestRgtVol(1, R2P, R2V);
            //Console.WriteLine(string.Format("R1P:{0} R1V:{1} R2P:{2} R2V:{3}", R1P, R1V, R2P, R2V));
            //错误信息
            if (Data[904] == 0x1C)
            {
                int errcount = Data[906] - 0x30;
                //Console.WriteLine(string.Format("there is {0} errors!", errcount));
                for (int i = 0; i < errcount; i++)
                {
                    int index = 907 + i * 7;
                    switch (Data[index] - 0x30)
                    {
                        case 0x02:
                            if (Pt2ndWn != 0)
                            {
                                RealTimeCUVDataService.RunningErrors(Pt2ndWn, "SMP");
                            }
                            break;//SMP
                        case 0x03:
                            if (Pt1stWn != 0)
                            {
                                RealTimeCUVDataService.RunningErrors(Pt1stWn, "R1");
                            }
                            break;//R1
                        case 0x04:
                            if (Pt12thWn != 0)
                            {
                                RealTimeCUVDataService.RunningErrors(Pt12thWn, "R2");
                            }
                            break;//R2
                    }

                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleType = TROUBLETYPE.ERR;
                    trouble.TroubleUnit = MyResources.Instance.FindResource("TroubleUnit1").ToString();
                    trouble.TroubleCode = string.Format("{0}{1}{2}{3}", (char)Data[index + 1], (char)Data[index + 2], (char)Data[index + 3], (char)Data[index + 4]);
                    trouble.TroubleInfo = null;
                    TroubleLogSer.Save(trouble);
                }
            }
            return null;
        }
    }
}
