using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class Parse0B0 : IParse
    {
        public string Parse(List<byte> data)
        {
            int BlkNO = MachineControlProtocol.HexConverToDec(data[2], data[3], data[4]);
            int index = 1;
            int i = 5;
            List<float> datas = new List<float>();
            while (index >= 120)
            {
                float PWL = MachineControlProtocol.HexConverToFloat(data[i], data[i + 1], data[i + 2], data[i + 3], data[i + 4], data[i + 5]);

                datas.Add(PWL);

                i += 6;
            }

            string v = "";
            foreach (float e in datas)
            {
                v += e.ToString();
                v += "	";
            }

            //LogService.Log(BlkNO.ToString() + "	" + v, "CuvBlk.lg");

            return "";
        }
    }
}
