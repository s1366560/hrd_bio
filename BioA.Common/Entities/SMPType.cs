using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class SMPType: CLItem
    {
        public SMPType()
        {
        }
        public SMPType(string name)
            :base(name)
        {
        }
        //类型体积 S:血清体积 U:尿液体积
        string _SMPVOLType;
        public string SMPVOLType
        {
            get { return _SMPVOLType; }
            set { _SMPVOLType = value; }
        }
    }
}
