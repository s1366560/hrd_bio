using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BioA.UI.ServiceReference1;

namespace BioA.UI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    public class NotifyCallBack : IBioAServiceCallback
    {
        public void NotifyFunction(object sender)
        {

        }

        public void ClientNotifyFunction(string strSendClientName, object Sender)
        {

        }

        public void DatabaseNotifyFunction(string strMethod, object sender)
        {

        }
    }
}
