using BioA.Common;
using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class Encode892 : IEncode
    {
        public byte[] Encode(object o)
        {
            AdjustNode AdjustNode = o as AdjustNode;
            if (AdjustNode == null || AdjustNode.NodeCode == null || AdjustNode.NodeCode.Count() != 2)
            {
                Console.WriteLine("试剂臂节点配置错误. ");
                return null;
            }

            byte[] bytes = new byte[9];
            bytes[0] = 0x02;
            bytes[1] = 0x89;
            bytes[2] = AdjustNode.NodeCode[0];
            bytes[3] = AdjustNode.NodeCode[1];
            if (AdjustNode.OffsetCount > 0)
            {
                bytes[4] = 0x30;
            }
            else
            {
                bytes[4] = 0x31;
            }
            bytes[5] = (byte)(0x30 + Math.Abs(AdjustNode.OffsetCount));
            bytes[6] = 0x03;
            bytes[7] = 0x00;
            bytes[8] = 0x00;
            byte[] checksum = MachineControlProtocol.CheckSum(bytes);
            bytes[7] = checksum[0];
            bytes[8] = checksum[1];

            return bytes;
        }
    }
}
