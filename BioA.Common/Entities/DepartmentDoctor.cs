using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class DepartmentDoctor : CLItem
    {
        string _DepartmentName;
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }
        string _DoctorName;
        public string DoctorName
        {
            get { return _DoctorName; }
            set { _DoctorName = value; }
        }
    }
}
