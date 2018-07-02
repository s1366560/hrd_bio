using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class PrintItem : CLItem
    {
        string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        int _PrintSequence;
        public int PrintSequence
        {
            get { return _PrintSequence; }
            set { _PrintSequence = value; }
        }
    }
}
