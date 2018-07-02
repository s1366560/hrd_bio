using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class PrinterConnector
    {
        bool _IsAutoStart;
        public bool IsAutoStart
        {
            get { return _IsAutoStart; }
            set { _IsAutoStart = value; }
        }

        string _SMPReportTemplate;
        public string SMPReportTemplate
        {
            get { return _SMPReportTemplate; }
            set { _SMPReportTemplate = value; }
        }
        string _QCReportTemplate;
        public string QCReportTemplate
        {
            get { return _QCReportTemplate; }
            set { _QCReportTemplate = value; }
        }
        string _PrinterName;
        public string PrinterName
        {
            get { return _PrinterName; }
            set { _PrinterName = value; }
        }
        string _PrinteState = "LOAD";
        public string PrinteState
        {
            get { return _PrinteState; }
            set { _PrinteState = value; }
        }
    }
}
