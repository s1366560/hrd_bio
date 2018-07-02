using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Service;
using CLMode.Protocol;
using CLMode.Entities;
using CLMode.Land;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Parse11
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse11
 * 创建时间：       4/25/2012 12:48:35 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace CLMode.Interface
{
    public class Parse110 : IParse
    {
        public string Parse(List<byte> data)
        {
            TroubleLogService TroubleLogSer = new TroubleLogService();

            int n = MachineControlProtocol.HexConverToDec(data[2], data[3]);

            TroubleLog trouble = new TroubleLog();
            trouble.TroubleType = TROUBLETYPE.ERR;
            trouble.TroubleUnit = MyResources.Instance.FindResource("Parse1101").ToString();
            trouble.TroubleCode = "CUV001";
            trouble.TroubleInfo = n + MyResources.Instance.FindResource("Parse1102").ToString();//string.Format("{0}号比色杯有污垢，测试无法在此号比色杯中进行. ", n); ;
            TroubleLogSer.Save(trouble);

            return n + MyResources.Instance.FindResource("Parse1103").ToString();//string.Format("{0}号比色杯有污垢. ", n);
        }
    }
}
