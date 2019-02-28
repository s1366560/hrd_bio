using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.HL7.V231
{
    public class MSH
    {
        public string GetString()
        {
            string Str = "|";
            for (int i = 1; i < Fields.Length; i++)
            {
                Str += Fields[i];
                Str += "|";
            }
            return "MSH" + Str;
        }
        public string[] Fields = new string[20];
    }
}
