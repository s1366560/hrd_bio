using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class SMPPosition:CLItem
    {
        int _Disk = 1;
        public int Disk
        {
            get { return _Disk; }
            set { _Disk = value; }
        }
        string _Position = "1";
        public string Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        string _SMPRack;
        public string SMPRack
        {
            get { return _SMPRack; }
            set { _SMPRack = value; }
        }
        string _SMPNO;
        public string SMPNO
        {
            get { return _SMPNO; }
            set { _SMPNO = value; }
        }
    }
}
