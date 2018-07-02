using BioA.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BioA.WindowsService
{
    public partial class BioAServiceHost : ServiceBase
    {
        private ServiceHost serviceHost;
        public BioAServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                serviceHost = new ServiceHost(typeof(BioAService));
                serviceHost.Opened += delegate {  };
                if (serviceHost != null)
                    serviceHost.Open();
             
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
