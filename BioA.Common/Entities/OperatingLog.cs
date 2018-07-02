using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class OperatingLog : CLItem
    {
        DateTime _DrawDT = DateTime.Now;
        public DateTime DrawDT
        {
            get { return _DrawDT; }
            set { _DrawDT = value; }
        }

        string _Operator;
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }
        string _Operating;
        public string Operating
        {
            get { return _Operating; }
            set { _Operating = value; }
        }
    }
}
