using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLMode.Interface
{
    public class ParseE10 : IParse
    {
        public string Parse(List<byte> data)
        {
            string info = "";
            switch (data[2])
            {
                case 0x1D: info = "Finished Washing Cuv"; break;
            }
            return info;
        }
    }
}
