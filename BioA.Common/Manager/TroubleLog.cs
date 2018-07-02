using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common
{
    public class TROUBLETYPE
    {
        public const string WARN = "WARNING";
        public const string ERR = "ERROR";
    }
    public class TroubleLog : CLItem
    {
        DateTime _DrawDT = DateTime.Now;
        public DateTime DrawDT
        {
            get { return _DrawDT; }
            set { _DrawDT = value; }
        }
        string _TroubleType;
        public string TroubleType
        {
            get { return _TroubleType; }
            set { _TroubleType = value; }
        }
        string _TroubleUnit;
        public string TroubleUnit
        {
            get { return _TroubleUnit; }
            set { _TroubleUnit = value; }
        }
        string _TroubleCode;
        public string TroubleCode
        {
            get { return _TroubleCode; }
            set { _TroubleCode = value; }
        }
        string _TroubleInfo;
        public string TroubleInfo
        {
            get { return _TroubleInfo; }
            set { _TroubleInfo = value; }
        }

        bool _IsComfirm = false;
        public bool IsComfirm 
        {
            get { return _IsComfirm; }
            set { _IsComfirm = value; } 
        }
    }
}
