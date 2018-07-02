using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class RunAssaySQ : CLItem
    {
        string _AssayName;
        public string AssayName
        {
            get { return _AssayName; }
            set { _AssayName = value; }
        }
        int _RunSQ;
        public int RunSQ
        {
            get { return _RunSQ; }
            set { _RunSQ = value; }
        }
    }
}
