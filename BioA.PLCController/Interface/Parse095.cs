using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class Parse095 : IParse
    {
        //版本解码
        public string Parse(List<byte> Data)
        {
            string code = null;
            for (int i = 3, j = 1; j <= 4; i++, j++)
            {
                code += (char)Data[i] + ".";
            }

            code = code.TrimEnd('.');
            return code;
        }
    }
}
