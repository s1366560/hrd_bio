using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class ApplyDoctorInfo
    {
        public ApplyDoctorInfo()
        {
            department = string.Empty;
            doctor = string.Empty;
        }
        string department;

        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        string doctor;

        public string Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }
    }
}
