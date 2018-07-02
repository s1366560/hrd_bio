using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Protocol;

namespace CLMode.Interface
{
    //AdjustNotSave
    public class Encode096 : IEncode
    {
        public byte[] Encode(object o)
        {
            string str = o as string;

            try
            {
                return EncodeString(str);
            }
            catch
            {

            }

            return null;
        }

        byte[] EncodeString(string str)
        {
            List<byte> data = new List<byte>();
            data.Add(0x02);
            data.Add(0x09);
            data.Add(0x3d);


            str = str.TrimEnd('|');
            string[] stres = str.Split('|');
            for (int i = 0; i < stres.Length; i++)
            {
                string tstr = stres[i];
                string[] tstres = tstr.Split(',');
                int v = int.Parse(tstres[1]);
                int[] dd = MachineControlProtocol.HDecConverToHex99999(v);
                for (int j = 0; j < dd.Length; j++)
                {
                    data.Add((byte)dd[j]);
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
