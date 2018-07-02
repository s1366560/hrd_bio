using BioA.Common.Entities;
using BioA.Common.Interface;
using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Encode6E0
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Encode6E0
 * 创建时间：       5/1/2012 10:22:45 AM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace CLMode.Interface
{
    public class Encode6E0:IEncode
    {
        public byte[] Encode(object o)
        {
            AdjustNode AdjustNode = o as AdjustNode;
            if (AdjustNode == null || AdjustNode.NodeCode==null)
            {
                return null;
            }

            byte[] bytes = new byte[8];
            bytes[0] = 0x02;
            bytes[1] = 0x6E;
            bytes[2] = AdjustNode.NodeCode[0];
            if (AdjustNode.OffsetCount > 0)
            {
                bytes[3] = 0x30;
            }
            else
            {
                bytes[3] = 0x31;
            }
            bytes[4] = (byte)(0x30 + Math.Abs(AdjustNode.OffsetCount));
            bytes[5] = 0x03;
            bytes[6] = 0x00;
            bytes[7] = 0x00;
            byte[] checksum = MachineControlProtocol.CheckSum(bytes);
            bytes[6] = checksum[0];
            bytes[7] = checksum[1];

            return bytes;
        }
    }
}
