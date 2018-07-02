using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class SMPContainerType : CLItem
    {
        public SMPContainerType()
        {
        }
        public SMPContainerType(string name)
            : base(name)
        {
        }
        //容量
        int _Volume;
        public int Volume
        {
            get { return _Volume; }
            set { _Volume = value; }
        }
        //通讯码
        int _Code;
        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
    }
}
