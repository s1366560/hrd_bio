using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Entities;
using CLMode.Protocol;

namespace CLMode.Interface
{
    public class EncodeAB0 : IEncode
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
            bytes[1] = 0xAB;
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
