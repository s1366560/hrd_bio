using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    /// <summary>
    /// 信息传输接口
    /// </summary>
    public interface INotifyCallBack
    {
        /// <summary>
        /// 通知客户端信息
        /// </summary>
        /// <param name="sender">消息信息</param>
        [OperationContract(IsOneWay = true)]
        void NotifyFunction(object sender);
        /// <summary>
        /// 由客户端将消息传递到客户端
        /// </summary>
        /// <param name="strSendClientName">发送的客户端名称</param>
        /// <param name="strSender">参数对象</param>
        [OperationContract(IsOneWay = true)]
        void ClientNotifyFunction(string strSendClientName, object Sender);
        /// <summary>
        /// 消息传递到客户端
        /// </summary>
        /// <param name="strMethod">访问数据库方法名</param>
        /// <param name="sender">参数</param>
        [OperationContract(IsOneWay = true)]
        void DatabaseNotifyFunction(ModuleInfo moduleInfo, string strMethod, object sender);

        [OperationContract(IsOneWay = true)]
        void DataAllReturnFunction(ModuleInfo moduleInfo, Dictionary<string, object> strMethodParam);
    }
}
