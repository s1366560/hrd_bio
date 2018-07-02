using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Protocol;

namespace CLMode.Interface
{
    //License编码器
    public class Encode094 : IEncode
    {
        public byte[] Encode(object o)
        {
            List<byte> data = new List<byte>();
            data.Add(0x02);
            data.Add(0x09);
            data.Add(0x34);

            string key = o as string;
            if (key != null)
            {
                foreach (char e in key)
                {
                    data.Add((byte)e);
                }
                for (int i = 1; i <= 32 - key.Length; i++)
                {
                    data.Add(0x30);
                }
            }
            else
            {
                for (int i = 1; i <= 32; i++)
                {
                    data.Add(0x30);
                }
            }

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
