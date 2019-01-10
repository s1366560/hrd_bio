using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Parse2B
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse2B
 * 创建时间：       4/25/2012 12:50:05 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    //NT300/400试剂扫描液面返回解析器
    public class Parse2B0 : IParse
    {
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> data)
        {
            byte disk = data[2];

            for (int i = 3; i < data.Count - 3; i = i + 4)
            {
                int p = MachineControlProtocol.HexConverToDec(data[i], data[i + 1]);
                p = p > 90 ? (p - 45) : p;
                int v = MachineControlProtocol.HexConverToDec(data[i + 2], data[i + 3]);
                if (p != 0)
                {
                    switch (disk)
                    {
                        case 0x30: UpdateLatestRgtVol(1, p, v); break;
                        case 0x31: UpdateLatestRgtVol(2, p, v); break;
                    }
                }
            }
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == 0x1C)
                {
                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleType = TROUBLETYPE.ERR;
                    trouble.TroubleUnit = "设备";
                    trouble.TroubleCode = string.Format("{0}{1}{2}{3}", (char)data[i + 1], (char)data[i + 2], (char)data[i + 3], (char)data[i + 4]);
                    trouble.TroubleInfo = null;
                    myBatis.TroubleLogSave("TroubleLogSave", trouble);
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
    }
}
