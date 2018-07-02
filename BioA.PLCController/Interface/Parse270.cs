using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.269
 * 类 名 称：       Parse270
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse270
 * 创建时间：       5/23/2012 3:04:51 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    public class Parse270 : IParse
    {
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> Data)
        {
            float v = MachineControlProtocol.HexConverToFloat(Data[9], Data[10], Data[11], Data[12], Data[13], Data[14]);

            myBatis.UpdateVoltageValue(v);

            return null;
        }
    }
}
