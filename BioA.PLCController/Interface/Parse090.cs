using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //激活码解码器
    public class Parse090 : IParse
    {
        public string Parse(List<byte> Data)
        {
            string code = null;
            for (int i = 3,j=1; j <= 32; i++,j++)
            {
                code += (char)Data[i];
            }
            string mcodestr = code.Substring(0, 25);

            //new ActivityKeyService().DeleteActivityKey();
            //new ActivityKeyService().InsertActivityKey(mcodestr);

            return mcodestr;
        }
    }
}
