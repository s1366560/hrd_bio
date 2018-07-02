using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.269
 * 类 名 称：       Encode270
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Encode270
 * 创建时间：       5/23/2012 2:54:23 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    public class Encode270 : IEncode
    {
        MyBatis myBatis = new MyBatis();
        public byte[] Encode(object o)
        {
            ManuOffsetGain ManuOffsetGain = myBatis.QueryManuOffsetGain("QueryManuOffsetGain");

            byte[] bytes = new byte[12];

            bytes[0] = 0x02;
            bytes[1] = 0x27;
            bytes[2] = (byte)('0' + ManuOffsetGain.WaveLength);
            int[] d = MachineControlProtocol.HDecConverToHex(ManuOffsetGain.OffSet);
            bytes[3] = (byte)d[0];
            bytes[4] = (byte)d[1];
            bytes[5] = (byte)d[2];
            d = MachineControlProtocol.HDecConverToHex(ManuOffsetGain.Gain);
            bytes[6] = (byte)d[0];
            bytes[7] = (byte)d[1];
            bytes[8] = (byte)d[2];
            bytes[9] = 0x03;
            bytes[10] = 0x00;
            bytes[11] = 0x00;
            byte[] checksum = MachineControlProtocol.CheckSum(bytes);
            bytes[10] = checksum[0];
            bytes[11] = checksum[1];

            return bytes;
        }
    }
}
