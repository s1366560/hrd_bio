using BioA.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bio.TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost;
            try
            {
                serviceHost = new ServiceHost(typeof(BioAService));
                serviceHost.Opened += delegate { System.Console.WriteLine("service has opened!"); };
                serviceHost.Faulted += delegate { System.Console.WriteLine("service has faulted!"); };
                if (serviceHost != null)
                    serviceHost.Open();

                Console.ReadLine();

            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
           
        }

    }
}
