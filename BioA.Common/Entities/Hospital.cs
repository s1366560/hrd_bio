using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class Hospital : CLItem
    {
        string _HospitalName;
        public string HospitalName
        {
            get { return _HospitalName; }
            set { _HospitalName = value; }
        }
        string _Assert;
        public string Assert
        {
            get { return _Assert; }
            set { _Assert = value; }
        }
    }
}
