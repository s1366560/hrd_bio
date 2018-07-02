using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class ERRInfo:CLItem
    {
        //故障代码
        private string _ERCode;
        public string ERCode
        {
            get { return _ERCode; }
            set { _ERCode = value; }
        }
        //描述
        private string _ERDescription;
        public string ERDescription
        {
            get { return _ERDescription; }
            set { _ERDescription = value; }
        }
    }
}
