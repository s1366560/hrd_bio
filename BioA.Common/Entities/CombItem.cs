using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class CombItem : CLItem
    {
        public CombItem()
        {
        }

        public CombItem(string name)
            :base(name)
        {
        }

        List<string> _Items = new List<string>();
        public List<string> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }
    }
}
