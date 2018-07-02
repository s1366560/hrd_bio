using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class RGTContainerType : CLItem
    {
        public RGTContainerType()
        {
        }
        public RGTContainerType(string name)
            : base(name)
        {
        }
        //容量
        int _Volume = 0;
        public int Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }

        int _Code;
        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
    }
}
