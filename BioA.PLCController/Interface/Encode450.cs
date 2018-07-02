using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    class Encode450 : IEncode
    {
        public byte[] Encode(object o)
        {
            List<byte> data = new List<byte>();
            data.Add(0x02);
            data.Add(0x45);

            string para = o as string;
            string d = "";
            string p = "";
            for (int i = 0; i < para.Length; i++)
            {
                if (para[i] != ':')
                {
                    d += para[i];
                }
                else
                {
                    p = para.Substring(i + 1, para.Length - (i + 1));
                    break;
                }
            }

            switch (int.Parse(d))
            {
                case 1:
                    data.Add(0x30);
                    break;
                case 2:
                    data.Add(0x31);
                    break;
            }

            string[] poses = p.Split('|');
            foreach (string i in poses)
            {
                try
                {
                    int[] dp = MachineControlProtocol.DecConverToHex(int.Parse(i));
                    data.Add((byte)dp[0]);
                    data.Add((byte)dp[1]);
                }
                catch
                {
                    continue;
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
