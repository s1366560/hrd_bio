using BioA.Common;
using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class Encode1D0 : IEncode
    {
        public byte[] Encode(object o)
        {
            AdjustNode AdjustNode = o as AdjustNode;
            if (AdjustNode == null || AdjustNode.NodeCode == null || AdjustNode.NodeCode.Count() != 1)
            {
                Console.WriteLine("试剂臂节点配置错误. ");
                return null;
            }

            byte[] bytes = new byte[8];
            bytes[0] = 0x02;
            bytes[1] = 0xED;
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
