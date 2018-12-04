using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       ParseE2
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       ParseE2
 * 创建时间：       4/25/2012 10:44:19 AM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    public class ParseE20 : IParse
    {
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> data)
        {
            bool HasError = false;
            bool HasWarn = false;
            for (int i = 2; i < 86; i = i + 7)
            {
                int WaveIndex = System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[data[i] - '0']);
                int OffSet = MachineControlProtocol.HexConverToDec(data[i + 1], data[i + 2], data[i + 3]);
                int Gain = MachineControlProtocol.HexConverToDec(data[i + 4], data[i + 5], data[i + 6]);

                if (Gain < 60)
                {
                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleType = TROUBLETYPE.ERR;
                    trouble.TroubleUnit = "光度计";
                    trouble.TroubleInfo = "波长" + WaveIndex + "增益" + Gain + "低于60，光度计不能正常工作";// string.Format(@"波长{0}增益值{1}低于60，光度计不能正常工作。", WaveIndex, Gain);
                    trouble.TroubleCode = "00001";
                    myBatis.TroubleLogSave("TroubleLogSave", trouble);

                    HasError = true;
                }

                OffSetGain offgain = myBatis.GetLatestOffSetGain(WaveIndex);
                if (offgain == null)
                {
                    offgain = new OffSetGain();
                    offgain.WaveLength = WaveIndex;
                    offgain.OffSet = OffSet;
                    offgain.Gain = Gain;
                    offgain.InspectTime = DateTime.Now;
                    if (WaveIndex == 795)
                    {
                        offgain.InspectTime = DateTime.Now;
                    }
                    myBatis.AddLatestOffSetGain(offgain);
                }
                else
                {
                    if (Math.Abs(Gain - offgain.Gain) >= 30)
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleType = TROUBLETYPE.WARN;
                        trouble.TroubleUnit = "光度计";
                        trouble.TroubleInfo = "波长" + WaveIndex + "增益值波动幅度大于30";// string.Format(@"波长{0}增益值波动幅度{1}，影响结果的准确性。", WaveIndex, Math.Abs(Gain - offgain.Gain));
                        trouble.TroubleCode = "00003";
                        myBatis.TroubleLogSave("TroubleLogSave", trouble);

                        HasWarn = true;
                    }
                    myBatis.DeleteOldOffSetGain(WaveIndex);
                    myBatis.AddOldOffSetGain(offgain);
                    offgain.OffSet = OffSet;
                    offgain.Gain = Gain;
                    if (WaveIndex == 795)
                    {
                        offgain.InspectTime = DateTime.Now;
                    }
                    myBatis.DeleteNewOffSetGain(WaveIndex);
                    myBatis.AddLatestOffSetGain(offgain);
                }
            }

            if (HasError == true)
            {
                return "PhotometerWrong";
            }

            if (HasWarn==true)
            {
                return "PhotometerWarn";
            }

            return "PhotometerRight";
        }
    }
}
