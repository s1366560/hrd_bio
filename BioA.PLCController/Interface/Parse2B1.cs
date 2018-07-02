using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //NT-1000清洗剂D余量判读
    class Parse2B1 : IParse
    {
        public string Parse(List<byte> data)
        {
            int v = 0;
            int p = 0;
            for (int i = 3; i < data.Count - 3; i = i + 4)
            {
                p = MachineControlProtocol.HexConverToDec(data[i], data[i + 1]);
                v = MachineControlProtocol.HexConverToDec(data[i + 2], data[i + 3]);
            }
            if (v < 5)
            {
                return "DERR";
            }

            return null;
        }
    }
}
