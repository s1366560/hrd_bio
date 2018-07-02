using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class CrossContaminationItem:CLItem
    {
        string _PreTest;
        public string PreTest
        {
            get { return _PreTest; }
            set { _PreTest = value; }
        }
        string _NexTest;
        public string NexTest
        {
            get { return _NexTest; }
            set { _NexTest = value; }
        }
    }
}
