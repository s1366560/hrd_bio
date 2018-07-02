using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class MntLog
    {
        public string Operator { get; set; }

        public string OperatedInfo { get; set; }

        DateTime _OperatedTime = DateTime.Now;
        public DateTime OperatedTime 
        {
            get { return _OperatedTime; }
            set
            {
                _OperatedTime = value;
            }
        }
    }
}
