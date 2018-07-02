using BioA.PLCController.Interface;
using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //SN编码器
    public class Encode092 : IEncode
    {
        public byte[] Encode(object o)
        {
            List<byte> data = new List<byte>();
            data.Add(0x02);
            data.Add(0x09);
            data.Add(0x32);

            try
            {
                string str = o as string;
                string[] filedstrs = str.Split('|');
                foreach (char e in filedstrs[0])
                {
                    data.Add((byte)e);
                }
                data.Add((byte)filedstrs[1][0]);

                int n1 = int.Parse(filedstrs[2]);
                int[] nbytes = MachineControlProtocol.DecConverToHex(n1);
                data.Add((byte)nbytes[0]);
                data.Add((byte)nbytes[1]);

                int n2 = int.Parse(filedstrs[3]);
                nbytes = MachineControlProtocol.DecConverToHex(n2);
                data.Add((byte)nbytes[0]);
                data.Add((byte)nbytes[1]);

                data.Add((byte)filedstrs[4][0]);

                for (int i = 15; i <= 32; i++)
                {
                    data.Add(0x30);
                }

                
            }
            catch
            {
                data.Clear();
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
