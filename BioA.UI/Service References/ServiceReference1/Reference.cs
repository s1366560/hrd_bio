﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BioA.UI.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/BioA.Service")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IBioAService", CallbackContract=typeof(BioA.UI.ServiceReference1.IBioAServiceCallback))]
    public interface IBioAService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/GetData", ReplyAction="http://tempuri.org/IBioAService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/GetData", ReplyAction="http://tempuri.org/IBioAService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IBioAService/GetDataUsingDataContractResponse")]
        BioA.UI.ServiceReference1.CompositeType GetDataUsingDataContract(BioA.UI.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IBioAService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<BioA.UI.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(BioA.UI.ServiceReference1.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBioAService/RegisterClient")]
        void RegisterClient(string strClientName);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBioAService/RegisterClient")]
        System.Threading.Tasks.Task RegisterClientAsync(string strClientName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/ClientSendMsgToClient", ReplyAction="http://tempuri.org/IBioAService/ClientSendMsgToClientResponse")]
        int ClientSendMsgToClient(string sendClientName, string RecClientName, BioA.Common.CommunicationEntity param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/ClientSendMsgToClient", ReplyAction="http://tempuri.org/IBioAService/ClientSendMsgToClientResponse")]
        System.Threading.Tasks.Task<int> ClientSendMsgToClientAsync(string sendClientName, string RecClientName, BioA.Common.CommunicationEntity param);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBioAService/ClientSendMsgToService")]
        void ClientSendMsgToService(BioA.Common.ModuleInfo sendClientName, string param);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBioAService/ClientSendMsgToService")]
        System.Threading.Tasks.Task ClientSendMsgToServiceAsync(BioA.Common.ModuleInfo sendClientName, string param);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/GetClients", ReplyAction="http://tempuri.org/IBioAService/GetClientsResponse")]
        string[] GetClients();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBioAService/GetClients", ReplyAction="http://tempuri.org/IBioAService/GetClientsResponse")]
        System.Threading.Tasks.Task<string[]> GetClientsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBioAServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBioAService/NotifyFunction")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.UI.ServiceReference1.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.Common.CommunicationEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.Common.ModuleInfo))]
        void NotifyFunction(object sender);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBioAService/ClientNotifyFunction")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.UI.ServiceReference1.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.Common.CommunicationEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.Common.ModuleInfo))]
        void ClientNotifyFunction(string strSendClientName, object Sender);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IBioAService/DatabaseNotifyFunction")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.UI.ServiceReference1.CompositeType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.Common.CommunicationEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BioA.Common.ModuleInfo))]
        void DatabaseNotifyFunction(BioA.Common.ModuleInfo moduleInfo, string strMethod, object sender);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBioAServiceChannel : BioA.UI.ServiceReference1.IBioAService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BioAServiceClient : System.ServiceModel.DuplexClientBase<BioA.UI.ServiceReference1.IBioAService>, BioA.UI.ServiceReference1.IBioAService {
        
        public BioAServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public BioAServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public BioAServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BioAServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public BioAServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public BioA.UI.ServiceReference1.CompositeType GetDataUsingDataContract(BioA.UI.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<BioA.UI.ServiceReference1.CompositeType> GetDataUsingDataContractAsync(BioA.UI.ServiceReference1.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public void RegisterClient(string strClientName) {
            base.Channel.RegisterClient(strClientName);
        }
        
        public System.Threading.Tasks.Task RegisterClientAsync(string strClientName) {
            return base.Channel.RegisterClientAsync(strClientName);
        }
        
        public int ClientSendMsgToClient(string sendClientName, string RecClientName, BioA.Common.CommunicationEntity param) {
            return base.Channel.ClientSendMsgToClient(sendClientName, RecClientName, param);
        }
        
        public System.Threading.Tasks.Task<int> ClientSendMsgToClientAsync(string sendClientName, string RecClientName, BioA.Common.CommunicationEntity param) {
            return base.Channel.ClientSendMsgToClientAsync(sendClientName, RecClientName, param);
        }
        
        public void ClientSendMsgToService(BioA.Common.ModuleInfo sendClientName, string param) {
            base.Channel.ClientSendMsgToService(sendClientName, param);
        }
        
        public System.Threading.Tasks.Task ClientSendMsgToServiceAsync(BioA.Common.ModuleInfo sendClientName, string param) {
            return base.Channel.ClientSendMsgToServiceAsync(sendClientName, param);
        }
        
        public string[] GetClients() {
            return base.Channel.GetClients();
        }
        
        public System.Threading.Tasks.Task<string[]> GetClientsAsync() {
            return base.Channel.GetClientsAsync();
        }
    }
}