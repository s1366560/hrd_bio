using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class Doctor : CLItem
    {
        string _DoctorName;
        public string DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }
    }
}
