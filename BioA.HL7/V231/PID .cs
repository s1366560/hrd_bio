using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.HL7.V231
{
    public class PID
    {
        public string GetString()
        {
            string Str = "|";
            for (int i = 0; i < Fields.Length; i++)
            {
                Str += Fields[i];
                Str += "|";
            }
            return "PID" + Str;
        }
        public string[] Fields = new string[30];
    }
}
