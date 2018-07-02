using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Parse1C
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse1C
 * 创建时间：       4/25/2012 12:36:36 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    public class Parse1C0 : IParse
    {
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> data)
        {
            if (data.Count < (5 + 7))
            {
                return null;
            }

            TroubleLog trouble = new TroubleLog();
            trouble.TroubleType = TROUBLETYPE.ERR;
            trouble.TroubleUnit = "设备";
            trouble.TroubleCode = string.Format("{0}{1}{2}{3}{4}{5}{6}", (char)data[2], (char)data[3], (char)data[4], (char)data[5], (char)data[6], (char)data[7], (char)data[8]);
            trouble.TroubleInfo = MachineControlProtocol.BytelistToHexString(data); ;
            myBatis.TroubleLogSave("TroubleLogSave", trouble);

            return trouble.TroubleCode;
        }
    }
}
