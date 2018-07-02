using BioA.Common;
using BioA.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BioA.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class BioAService : IBioAService
    {
        private SettingsChemicalParameter settingsChemicalParam;

        public BioAService()
        {
            settingsChemicalParam = new SettingsChemicalParameter();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public CommunicationEntity1 GetCommunicationEntity1(CommunicationEntity1 composite)
        {
            CommunicationEntity1 a = new CommunicationEntity1();
            return a;
        }

        public CommunicationEntityThreeParam1 GetCommunicationEntityThreeParam1(CommunicationEntityThreeParam1 composite)
        {
            CommunicationEntityThreeParam1 a = new CommunicationEntityThreeParam1();
            return a;
        }

        /// <summary>
        ///  注册客户端
        /// </summary>
        /// <param name="clientName"></param>
        public void RegisterClient(string strClientName)
        {
            Console.WriteLine("new client register" + strClientName);

            ModuleInfo clientName = (ModuleInfo)XmlUtility.Deserialize(typeof(ModuleInfo), strClientName);

            ClientRegisterInfo client = new ClientRegisterInfo { ClientName = clientName };
            client.NotifyCallBack = OperationContext.Current.GetCallbackChannel<INotifyCallBack>();
            client.NotifyCallBack.NotifyFunction("RegisterSuccess");
            ClientInfoCache.Instance.Add(client);
        }

        /// <summary>
        /// 向指定客户端发送信息，可由一客户端发送给另一客户端
        /// </summary>
        /// <param name="clientName">发送客户端名字</param>
        /// <param name="msg">发送内容</param>
        /// <returns>0, 1; 0代表发送失败，1代表发送成功</returns>
        public int ClientSendMsgToClient(ModuleInfo sendClientName, ModuleInfo RecClientName, CommunicationEntity param)
        {
            ClientRegisterInfo client = ClientInfoCache.Instance.Clients.FirstOrDefault(x => x.ClientName == RecClientName);

            if (client != null)
            {
                client.NotifyCallBack.NotifyFunction(param);
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// 客户端向服务器发送信息
        /// </summary>
        /// <param name="param">发送对象</param>
        /// <returns>0, 1; 0代表发送失败，1代表发送成功</returns>
        public void ClientSendMsgToService(ModuleInfo sendClientName, CommunicationEntity1 param)
        {
            LogInfo.WriteProcessLog(sendClientName.ToString(), Module.WindowsService);
            ClientRegisterInfo client = ClientInfoCache.Instance.Clients.Find(x => x.ClientName == sendClientName);

            switch (sendClientName)
            {
                case ModuleInfo.WorkingAreaApplyTask:
                    break;
                case ModuleInfo.WorkingAreaCalibDataCheck:
                    break;
                case ModuleInfo.WorkingAreaDataCheck:
                    break;
                case ModuleInfo.ReagentSetting:
                    break;
                case ModuleInfo.ReagentState:
                    break;
                case ModuleInfo.CalibrationMaintain:
                    break;
                case ModuleInfo.CalibrationState:
                    break;
                case ModuleInfo.QCMaintain:
                    break;
                case ModuleInfo.QCState:
                    break;
                case ModuleInfo.SettingsChemicalParameter:
                    HandleSettingsChemicalParam(client, param);
                    break;
                case ModuleInfo.SettingsCombinationItem:
                    break;
                case ModuleInfo.SettingsCalculateItem:
                    break;
                case ModuleInfo.SettingsEnvironment:
                    break;
                case ModuleInfo.SettingsCrossPollution:
                    break;
                case ModuleInfo.SettingsDataConfig:
                    break;
                case ModuleInfo.SettingsLISCommunicate:
                    break;
                case ModuleInfo.SystemMaintenance:
                    break;
                case ModuleInfo.SystemEquipmentManage:
                    break;
                case ModuleInfo.SystemUserManagement:
                    break;
                case ModuleInfo.SystemDepartmentManage:
                    break;
                case ModuleInfo.SystemConfigure:
                    break;
                case ModuleInfo.SystemLogCheck:
                    break;
                case ModuleInfo.SystemVersionInfomation:
                    break;
                default:
                    break;
            }

            // LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);

        }

        /// <summary>
        /// 处理设置-生化项目数据
        /// </summary>
        private void HandleSettingsChemicalParam(ClientRegisterInfo client, CommunicationEntity1 param)
        {

            LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);

            AssayProjectInfo assProInfo = new AssayProjectInfo();
            switch (param.StrmethodName)
            {
                case "QueryAssayProAllInfo":
                    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(param.StrmethodName, assProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos));
                    LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);
                    break;
                case "AssayProjectAdd":
                    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    string strInfo = settingsChemicalParam.AddAssayProject(param.StrmethodName, assProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, strInfo);
                    LogInfo.WriteProcessLog(strInfo, Module.WindowsService);
                    break;
                case "GetAssayProjectParamInfoByNameAndType":
                    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    AssayProjectParamInfo assayProParamInfo = settingsChemicalParam.GetAssayProjectParamInfoByNameAndType(param.StrmethodName, assProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, XmlUtility.Serializer(typeof(List<AssayProjectParamInfo>), assayProParamInfo));
                    LogInfo.WriteProcessLog(assayProParamInfo.ProjectName, Module.WindowsService);
                    break;
                case "QueryProjectResultUnits":
                    List<string> lstUnits = settingsChemicalParam.QueryProjectResultUnits(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstUnits));
                    break;
                case "UpdateAssayProjectParamInfo":
                    AssayProjectParamInfo assProParamInfo = XmlUtility.Deserialize(typeof(AssayProjectParamInfo), param.ObjParam) as AssayProjectParamInfo;
                    int intResult = settingsChemicalParam.UpdateAssayProjectParamInfo(param.StrmethodName, assProParamInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, intResult);
                    break;
                case "AssayProjectEdit":
                    AssayProjectInfo assayProInfoOld = XmlUtility.Deserialize(typeof(AssayProjectInfo), ((CommunicationEntityThreeParam1)param).ObjParam) as AssayProjectInfo;
                    AssayProjectInfo assayProInfoLast = XmlUtility.Deserialize(typeof(AssayProjectInfo), ((CommunicationEntityThreeParam1)param).ObjLastestParam) as AssayProjectInfo;
                    LogInfo.WriteProcessLog("1"+((CommunicationEntityThreeParam1)param).ObjParam, Module.WindowsService);
                    LogInfo.WriteProcessLog("2" + ((CommunicationEntityThreeParam1)param).ObjLastestParam, Module.WindowsService);
                    int intEditResult = settingsChemicalParam.EditAssayProject(param.StrmethodName, assayProInfoOld, assayProInfoLast);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, intEditResult);
                    break;
                case "QueryAssayProDelete":
                    List<AssayProjectInfo> lstProInfos = XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), param.ObjParam) as List<AssayProjectInfo>;
                    int intDeleteCount = settingsChemicalParam.QueryAssayProDelete(param.StrmethodName, lstProInfos);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, intDeleteCount);
                    break;
                case "ProjectPageinfo":
                    List<AssayProjectInfo> assayProInfos = settingsChemicalParam.QueryAssayProAllInfoByDistinctProName(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), assayProInfos));
                    break;
                // 通过项目名称和项目类型获取项目校准参数
                case "QueryCalibParamByProNameAndType":
                    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    AssayProjectCalibrationParamInfo calibParam = settingsChemicalParam.QueryCalibParamByProNameAndType(param.StrmethodName, assProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, XmlUtility.Serializer(typeof(AssayProjectCalibrationParamInfo), calibParam));
                    break;
                // 通过项目名称和类型更新校准参数
                case "UpdateCalibParamByProNameAndType":
                    AssayProjectCalibrationParamInfo sender = XmlUtility.Deserialize(typeof(AssayProjectCalibrationParamInfo), param.ObjParam) as AssayProjectCalibrationParamInfo;
                    int intCalibResult = settingsChemicalParam.UpdateCalibParamByProNameAndType(param.StrmethodName, sender);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, intCalibResult);
                    break;
                // 通过项目名称和项目类型获取项目范围参数
                case "QueryRangeParamByProNameAndType":
                    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    AssayProjectRangeParamInfo rangeParam = settingsChemicalParam.QueryRangeParamByProNameAndType(param.StrmethodName, assProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, XmlUtility.Serializer(typeof(AssayProjectRangeParamInfo), rangeParam));
                    break;
                case "UpdateRangeParamByProNameAndType":
                    AssayProjectRangeParamInfo rangeSender = XmlUtility.Deserialize(typeof(AssayProjectRangeParamInfo), param.ObjParam) as AssayProjectRangeParamInfo;
                    int intRangeResult = settingsChemicalParam.UpdateRangeParamByProNameAndType(param.StrmethodName, rangeSender);
                    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, intRangeResult);
                    break;
                default:
                    break;
            }
        }        /// 获取所有客户端名称
        /// </summary>
        /// <returns>返回客户端名集合</returns>
        public List<ModuleInfo> GetClients()
        {
            return ClientInfoCache.Instance.Clients.Select(x => x.ClientName).ToList();
        }
    }
}
