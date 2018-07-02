using BioA.UI.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BioA.UI
{
    //[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]

    //public class ReagentNeedleCallBack : IBioAServiceCallback
    //{
    //    public delegate void DataTransferDelegate(string strMethod, object sender);
    //    /// <summary>
    //    /// 接收数据库传输的数据，并发送至窗体
    //    /// </summary>
    //    public DataTransferDelegate DataTransferEvent;

    //    public void NotifyFunction(object sender)
    //    {

    //    }

    //    public void ClientNotifyFunction(string strSendClientName, object Sender)
    //    {

    //    }

    //    public void DatabaseNotifyFunction(string strMethod, object sender)
    //    {
    //        //List<AssayProjectInfo> lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
    //        if (DataTransferEvent != null)
    //        {
    //            DataTransferEvent(strMethod, sender);
    //        }
    //        //List<AssayProjectInfo> lstAssayProInfos
    //    }
    //}
  
    
}
