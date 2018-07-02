using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.Service
{
    /// <summary>
    /// 客户端注册信息
    /// </summary>
    public class ClientRegisterInfo
    {
        private readonly object syncNotifyObj = new object();

        public ClientRegisterInfo()
        { 
            
        }

        private INotifyCallBack notifyCallBack;

        public INotifyCallBack NotifyCallBack
        {
            get { return notifyCallBack; }
            set 
            { 
                lock (syncNotifyObj)
                {
                    notifyCallBack = value;
                    if (notifyCallBack != null)
                    {
                        var communication = notifyCallBack as ICommunicationObject;
                        if (communication != null)
                        {
                            communication.Closed += OnChannelClose;
                            communication.Faulted += OnChannelFault;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当通信对象第一次进入有缺陷的状态时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChannelFault(object sender, EventArgs e)
        {
            Console.WriteLine("client Fault " + this.ClientName);

            ClientInfoCache.Instance.Remove(this);
        }

        /// <summary>
        /// 当通信对象完成从正在关闭状态到完成关闭状态的转换时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChannelClose(object sender, EventArgs e)
        {
            Console.WriteLine("client channel close " + this.ClientName);
            ClientInfoCache.Instance.Remove(this);
        }

        public string ClientName
        {
            get;
            set;
        }
    }
}
