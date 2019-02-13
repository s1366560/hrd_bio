using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    ////NT1000清洗状态解码器
    public class Parse0831 : IParse
    {
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> Data)
        {
            int i = 0;
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
            int R1P = MachineControlProtocol.HexConverToDec(Data[i + 1], Data[i + 2]);
            int R1V = MachineControlProtocol.HexConverToDec(Data[i + 3], Data[i + 4]);
            UpdateLatestRgtVol(1, R1P, R1V);
            RgtWarning(1, R1P);
            int R2P = MachineControlProtocol.HexConverToDec(Data[i + 5], Data[i + 6]);
            int R2V = MachineControlProtocol.HexConverToDec(Data[i + 7], Data[i + 8]);
            UpdateLatestRgtVol(2, R2P, R2V);
            RgtWarning(2, R2P);
            //LogService.Log(string.Format("R1位置:{0} R1体积:{1} R2位置:{2} R2体积:{3}", R1P, R1V, R2P, R2V), LogType.Debug);
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
                    if (ercode == "E000415" || ercode == "E000615")
                    {

                    }
                    else
                    {
                        TroubleLog t = new TroubleLog();
                        t.TroubleCode = ercode;
                        t.TroubleType = TROUBLETYPE.ERR;
                        t.TroubleUnit = "设备";
                        t.TroubleInfo = null;
                        myBatis.TroubleLogSave("TroubleLogSave", t);
                    }

                    string cmdname = string.Format("{0}{1}", (char)Data[index + 3], (char)Data[index + 4]);
                    if (cmdname == "77" && Data[index + 5] == 0x30)//R1
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"0000770";
                        trouble.TroubleType = TROUBLETYPE.ERR;
                        trouble.TroubleUnit = "设备";
                        trouble.TroubleInfo = string.Format("试剂仓1清洗剂添加失败. ");
                        myBatis.TroubleLogSave("TroubleLogSave", trouble);
                    }
                    if (cmdname == "77" && Data[index + 5] == 0x31)//R2
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"0000771";
                        trouble.TroubleType = TROUBLETYPE.ERR;
                        trouble.TroubleUnit = "设备";
                        trouble.TroubleInfo = string.Format("试剂仓2清洗剂添加失败. ");
                        myBatis.TroubleLogSave("TroubleLogSave", trouble);

                    }
                    if (cmdname == "57" && Data[index + 5] == 0x30)//SMP
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"0000570";
                        trouble.TroubleType = TROUBLETYPE.ERR;
                        trouble.TroubleUnit = "设备";
                        trouble.TroubleInfo = string.Format("样本位清洗剂添加失败. ");
                        myBatis.TroubleLogSave("TroubleLogSave", trouble);
                    }
                }
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
                if (reaSettingInfo != null && v > 0 && v2 ==0)
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
                int c = 0;
                int v = System.Convert.ToInt32(rsi.ReagentContainer.Substring(0, rsi.ReagentContainer.IndexOf("ml"))) * rgp.ValidPercent / 100 * 1000;
                
                switch (d)
                {
                    case 1:
                        c = v / 250;
                        if (c < rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000772";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = "设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂耗尽. ", p);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);

                            return;
                        }
                        if (c < rgtwarncount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000773";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = "设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂即将耗尽. ", p);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                            return;
                        }
                        break;
                    case 2:
                        c = v / 150;
                        if (c < rgtleastcount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000774";
                            trouble.TroubleType = TROUBLETYPE.ERR;
                            trouble.TroubleUnit = "设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂耗尽. ", p);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);

                            return;
                        }
                        if (c < rgtwarncount)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000775";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = "设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}清洗剂即将耗尽. ", p);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                            return;
                        }
                        break;
                }
            }
        }

    }
}
