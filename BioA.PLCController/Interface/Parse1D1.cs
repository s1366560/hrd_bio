using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMode.Interface
{
    public class Parse1D1 : IParse
    {
        public string Parse(List<byte> data)
        {
            string code = string.Format("{0}{1}{2}{3}", (char)data[2], (char)data[3], (char)data[4], (char)data[5]);
            return code;
        }
    }
}
