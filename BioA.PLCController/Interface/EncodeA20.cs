using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class EncodeA20 : IEncode
    {
        public byte[] Encode(object o)
        {
            byte[] bytes = new byte[9];
            bytes[0] = 0x02;
            bytes[1] = 0xA2;

            string v = o as string;
            if (string.IsNullOrEmpty(v) || string.IsNullOrWhiteSpace(v))
            {
                bytes = null;
                return null;
            }
            else
            {
                string[] vs = v.Split(' ');
                if (vs.Count() != 4)
                {
                    bytes = null;
                    return null;
                }
                byte[] d = MachineControlProtocol.HexStringToByteArray(v, ' ');
                bytes[2] = d[0];
                bytes[3] = d[1];
                bytes[4] = d[2];
                bytes[5] = d[3];

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
}
