﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class MaintenanceLogInfo
    {
        public MaintenanceLogInfo ()
        {
            userName=string.Empty;
            logDetails=string.Empty;
            logDateTime = DateTime.Now;
        }

        string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        string logDetails;

        public string LogDetails
        {
            get { return logDetails; }
            set { logDetails = value; }
        }
        DateTime logDateTime;

        public DateTime LogDateTime
        {
            get { return logDateTime; }
            set { logDateTime = value; }
        }
    }
}
