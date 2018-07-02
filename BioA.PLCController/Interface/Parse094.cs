using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //License解码器
    public class Parse094 : IParse
    {
        public string Parse(List<byte> Data)
        {
            string code = null;
            for (int i = 3, j = 1; j <= 32; i++, j++)
            {
                code += (char)Data[i];
            }
            
            return code.Substring(0, 12); ;
        }
    }
}
