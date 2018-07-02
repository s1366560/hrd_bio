using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Service;
using CLMode.Protocol;

namespace CLMode.Interface
{
    //激活码编码器
    public class Encode090 : IEncode
    {
        public byte[] Encode(object o)
        {
            List<byte> data = new List<byte>();
            data.Add(0x02);
            data.Add(0x09);
            data.Add(0x30);

            string key = new ActivityKeyService().GetActivityKey();
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key) || key.Length != 25)
            {
                for (int i = 1; i <= 32; i++)
                {
                    data.Add(0x30);
                }
            }
            else
            {
                foreach (char e in key)
                {
                    data.Add((byte)e);
                }
                for (int i = 1; i <= 7; i++)
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
