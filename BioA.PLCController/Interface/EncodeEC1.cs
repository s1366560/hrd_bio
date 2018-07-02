using BioA.Common;
using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class EncodeEC1 : IEncode
    {
        public byte[] Encode(object o)
        {
            AdjustNode AdjustNode = o as AdjustNode;
            if (AdjustNode == null)
            {
                return null;
            }

            byte[] bytes = new byte[7];
            bytes[0] = 0x02;
            bytes[1] = 0xEC;
            if (AdjustNode.OffsetCount > 0)
            {
                bytes[2] = 0x30;
            }
            else
            {
                bytes[2] = 0x31;
            }
            bytes[3] = (byte)(0x30 + Math.Abs(AdjustNode.OffsetCount));
            bytes[4] = 0x03;
            bytes[5] = 0x00;
            bytes[6] = 0x00;
            byte[] checksum = MachineControlProtocol.CheckSum(bytes);
            bytes[5] = checksum[0];
            bytes[6] = checksum[1];

            return bytes;
        }
    }
}
