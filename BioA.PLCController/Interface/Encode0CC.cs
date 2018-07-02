using BioA.Common;
using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.PLCController.Interface
{
    /// <summary>
    /// 检验机器——冯旗
    /// </summary>
    class Encode0CC : IEncode
    {
        public byte[] Encode(object o)
        {
            //string strcpuID = ComputerInfo.CpuID;

            List<byte> data = new List<byte>();
            data.Add(0x02);
            data.Add(0x01);
            data.Add(0x22);

            try
            {
                string strcpuID = ComputerInfo.CpuID;
                for (int i = 0; i < strcpuID.Length; i++)
                {
                    data.Add(System.Convert.ToByte(strcpuID[i]));
                }
            }
            catch
            {
                data.Add(0x30);
            }

            data.Add(0x03);
            data.Add(0x00);
            data.Add(0x00);

            byte[] bytes = data.ToArray();

            byte[] checksum = MachineControlProtocol.CheckSum(bytes);

            bytes[bytes.Count() - 2] = checksum[0];
            bytes[bytes.Count() - 1] = checksum[1];

            return bytes;


        }
    }
}
