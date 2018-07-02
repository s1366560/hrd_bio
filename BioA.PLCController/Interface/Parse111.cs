using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Parse11NT
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse11NT
 * 创建时间：       4/25/2012 12:48:48 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    public class Parse111 : IParse
    {
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> data)
        {
            int n = MachineControlProtocol.HexConverToDec(data[2], data[3], data[4]);

            TroubleLog trouble = new TroubleLog();
            trouble.TroubleType = TROUBLETYPE.ERR;
            trouble.TroubleUnit = "比色杯";
            trouble.TroubleCode = string.Format("CUV001");
            trouble.TroubleInfo = n + "号比色杯有污垢，测试无法在此号比色杯中进行";
            myBatis.TroubleLogSave("TroubleLogSave", trouble);

            return n + "号比色杯有污垢";
        }
    }
}
