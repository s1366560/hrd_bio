using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Protocol;

namespace CLMode.Interface
{
    public class Parse096 : IParse
    {
        //解码 0x9 0x3c 
        public string Parse(List<byte> data)
        {
            string code = null;
            for (int i = 3, j = 1; j <= 53; i+=5, j++)
            {
                int v = MachineControlProtocol.HexConverToDec(data[i], data[i + 1], data[i+2], data[i + 3], data[i + 4]);
                string data1 = string.Format("{0},{1}|", j, v);
                code += data1;
            }
            code = code.TrimEnd('|');

            return code;
        }
    }
}
