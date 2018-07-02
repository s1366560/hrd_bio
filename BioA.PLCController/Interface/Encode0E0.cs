using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //温控设置
    public class Encode0E0 : IEncode
    {
        public byte[] Encode(object o)
        {

            List<byte> data = new List<byte>();
            data.Add(0x02);
            data.Add(0x0E);
            data.Add(0x3D);

            float t = (float)o;
            float v = (t - 37.00f + 10) * 10;
            int iv = (int)v;
            byte[] ivb = MachineControlProtocol.CheckSum(iv);
            data.Add((byte)ivb[0]);
            data.Add((byte)ivb[1]);

            data.Add(0x03);
            data.Add(0x00);
            data.Add(0x00);

            byte[] bytes = new byte[data.Count];
            for (int j = 0; j < data.Count; j++)
            {
                bytes[j] = data[j];
            }

            byte[] checksum = MachineControlProtocol.CheckSum(bytes);

            bytes[bytes.Count() - 2] = checksum[0];
            bytes[bytes.Count() - 1] = checksum[1];

            return bytes;

        }
    }
}
