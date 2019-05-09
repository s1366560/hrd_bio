using BioA.Common;
using BioA.Common.IO;
using BioA.UI.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BioA.UI
{
    public class CommunicationUI
    {
        private static BioAServiceClient serviceClient;
        public static NotifyCallBack notifyCallBack = new NotifyCallBack();

        public static BioAServiceClient ServiceClient
        {
            get 
            {
                if (serviceClient == null)
                {
                    serviceClient = new BioAServiceClient(new InstanceContext(notifyCallBack));
                    // 注册客户端
                    serviceClient.RegisterClient("BioA.UI");
                }
                if (serviceClient.State == CommunicationState.Faulted)
                {
                    serviceClient.Abort();

                    serviceClient = new BioAServiceClient(new InstanceContext(notifyCallBack));
                    // 注册客户端
                    serviceClient.RegisterClient("BioA.UI");
                    Thread.Sleep(300);
                }

                if (serviceClient.State == CommunicationState.Closed)
                {
                    serviceClient.Abort();
                    serviceClient = new BioAServiceClient(new InstanceContext(notifyCallBack));
                    // 注册客户端
                    serviceClient.RegisterClient("BioA.UI");
                    Thread.Sleep(300);
                    //serviceClient.Open();
                }
                return serviceClient;
            }

        }
    }
}
