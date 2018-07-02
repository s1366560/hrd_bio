using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class SMPVolRf
    {
        public SMPVolRf()
        {
        }

        private float _VolPre = 5;
        public float VolPre
        {
            get { return _VolPre; }
            set { _VolPre = value; }
        }

        private float _VolAft = 0;
        public float VolAft
        {
            get { return _VolAft; }
            set { _VolAft = value; }
        }

        private float _VolDil = 0;
        public float VolDil
        {
            get { return _VolDil; }
            set { _VolDil = value; }
        }
    }
}
