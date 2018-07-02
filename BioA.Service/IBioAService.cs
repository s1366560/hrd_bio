using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BioA.Common;

namespace BioA.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(INotifyCallBack))]
    public interface IBioAService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        CommunicationEntity1 GetCommunicationEntity1(CommunicationEntity1 composite);
        [OperationContract]
        CommunicationEntityThreeParam1 GetCommunicationEntityThreeParam1(CommunicationEntityThreeParam1 composite);

        // TODO: Add your service operations here
        [OperationContract(IsOneWay = true)]
        void RegisterClient(string strClientName);

        [OperationContract]
        int ClientSendMsgToClient(ModuleInfo sendClientName, ModuleInfo RecClientName, CommunicationEntity param);

        [OperationContract(IsOneWay = true)]
        void ClientSendMsgToService(ModuleInfo sendClientName, CommunicationEntity1 param);

        [OperationContract]
        List<ModuleInfo> GetClients();
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "BioA.Service.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    /// <summary>
    /// 传输过程中为一个方法名、一个参数实体类
    /// </summary>
    [DataContract]
    public class CommunicationEntity1
    {
        public CommunicationEntity1()
        {
            strMethodName = string.Empty;
            objParam = string.Empty;
        }

        public CommunicationEntity1(string methodName, string sender)
        {
            strMethodName = methodName;
            objParam = sender;
        }

        private string strMethodName;
        /// <summary>
        /// 访问数据库方法名
        /// </summary>
        [DataMember]
        public string StrmethodName
        {
            get { return strMethodName; }
            set { strMethodName = value; }
        }

        private string objParam;
        /// <summary>
        /// 访问数据库参数
        /// </summary>
        [DataMember]
        public string ObjParam
        {
            get { return objParam; }
            set { objParam = value; }
        }
    }
    [DataContract]
    public class CommunicationEntityThreeParam1 : CommunicationEntity1
    {
        private string objLastestParam;
        /// <summary>
        /// 访问数据库参数
        /// </summary>
        [DataMember]
        public string ObjLastestParam
        {
            get { return objLastestParam; }
            set { objLastestParam = value; }
        }

        public CommunicationEntityThreeParam1()
        {
            objLastestParam = string.Empty;
        }

        public CommunicationEntityThreeParam1(string methodName, string sender, string lastParam)
        {
            StrmethodName = methodName;
            ObjParam = sender;
            objLastestParam = lastParam;
        }
    }
}
