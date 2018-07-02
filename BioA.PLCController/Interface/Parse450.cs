using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //样本条码数据解析
    public class Parse450 : IParse
    {
        public string Parse(List<byte> data)
        {
            int disk = 0;

            switch (data[2])
            {
                case 0x30: disk = 1; break;
                case 0x31: disk = 2; break;
            }

            int p = MachineControlProtocol.HexConverToDec(data[3], data[4]);

            string barcode = null;
            for (int i = 5; i < data.Count; i++)
            {
                if (data[i] == 0x03)
                {
                    break;
                }

                string v = string.Format("{0}", (char)data[i]);
                barcode += v;
            }

            return disk + "|" + p + "|" + barcode;
        }
    }
}
