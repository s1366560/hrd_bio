using BioA.Common;
using BioA.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BioA.Service.MainTains;

namespace BioA.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class BioAService : IBioAService
    {
        private WorkAreaApplyTask workAreaApplyTask;
        private WorkingAreaDataCheck workAreaDataCheck;
        private QCMaintian qcMaintian;
        private QCResult qcResult;
        private QCGraphics qcGraphics;
        private QCTask qcTask;
        private SettingsChemicalParameter settingsChemicalParam;
        private CombProjectParameter combProjectParam;
        private CalcProjectParameter calcProjectParam;
        private EnvironmentParameter environmentParam;
        private SettingsDataConfig settingsDataConfig;
        private SettingsReagentNeedle settingsReagentNeedle;
        private SystemUserManagement systemUserManagement;
        private SystemDepartmentManage systemDepartmentManage;
        private SystemLogCheck systemLogCheck;
        private ReagentSetting reagentSetting;
        private ReagentState reagentState;
        private Calibrator calibrator;
        private Login login;
        private SystemMaintenance systemMaintenance;
        private SystemEquipmentManage systemEquipmentManage;
        private MainTain mainTain;

        private static object lockObj = new object();

        public BioAService()
        {
            workAreaApplyTask = new WorkAreaApplyTask();
            workAreaDataCheck = new WorkingAreaDataCheck();
            qcMaintian = new QCMaintian();
            qcResult = new QCResult();
            qcTask = new QCTask();
            settingsChemicalParam = new SettingsChemicalParameter();
            combProjectParam = new CombProjectParameter();
            calcProjectParam = new CalcProjectParameter();
            environmentParam = new EnvironmentParameter();
            settingsDataConfig = new SettingsDataConfig();
            settingsReagentNeedle = new SettingsReagentNeedle();
            systemUserManagement = new SystemUserManagement();
            systemDepartmentManage = new SystemDepartmentManage();
            systemLogCheck = new SystemLogCheck();
            qcGraphics = new QCGraphics();
            reagentSetting = new ReagentSetting();
            reagentState = new ReagentState();
            calibrator = new Calibrator();
            login = new Login();
            systemMaintenance = new SystemMaintenance();
            systemEquipmentManage = new SystemEquipmentManage();
            mainTain = new MainTain();
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

        /// <summary>
        ///  注册客户端
        /// </summary>
        /// <param name="clientName"></param>
        public void RegisterClient(string strClientName)
        {
            lock (lockObj)
            {
                ClientRegisterInfo client = new ClientRegisterInfo { ClientName = strClientName };
                client.NotifyCallBack = OperationContext.Current.GetCallbackChannel<INotifyCallBack>();
                client.NotifyCallBack.NotifyFunction("RegisterSuccess");
                ClientInfoCache.Instance.Add(client);
                LogInfo.WriteProcessLog("ClientInfoCache.Instance.Clients.Count" + ClientInfoCache.Instance.Clients.Count.ToString(), Module.WindowsService);
            }
        }

        /// <summary>
        /// 向指定客户端发送信息，可由一客户端发送给另一客户端
        /// </summary>
        /// <param name="clientName">发送客户端名字</param>
        /// <param name="msg">发送内容</param>
        /// <returns>0, 1; 0代表发送失败，1代表发送成功</returns>
        public int ClientSendMsgToClient(string sendClientName, string RecClientName, CommunicationEntity param)
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
        public void ClientSendMsgToService(ModuleInfo sendClientName, string param)
        {
            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param, Module.WindowsService);
                ClientRegisterInfo client = ClientInfoCache.Instance.Clients.Find(x => x.ClientName == "BioA.UI");
                int i = ClientInfoCache.Instance.Clients.Count;
                switch (sendClientName)
                {
                    case ModuleInfo.Login://UserLogin
                        HandleLogin(ModuleInfo.Login, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.WorkingAreaApplyTask:
                        HandleWorkingAreaApplyTask(ModuleInfo.WorkingAreaApplyTask, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.WorkingAreaCalibDataCheck:
                        break; 
                    case ModuleInfo.WorkingAreaDataCheck:
                        HandleWorkingAreaDataCheck(ModuleInfo.WorkingAreaDataCheck, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.ReagentSetting:
                        HandleReagentSetting(ModuleInfo.ReagentSetting, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.ReagentState:
                        HandleReagentState(ModuleInfo.ReagentState, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.CalibrationMaintain:
                        HandleCalibrationMaintain(ModuleInfo.CalibrationMaintain, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.CalibrationState:
                        HandleCalibrationState(ModuleInfo.CalibrationState, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.CalibControlTask:
                        HandleCalibControlTask(ModuleInfo.CalibControlTask, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.QCMaintain:
                        HandleQCMaintain(ModuleInfo.QCMaintain, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.QCResult:
                        HandleQCResult(ModuleInfo.QCResult, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.QCGraphic:
                        HandleQCGraphics(ModuleInfo.QCGraphic, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.QCTask:
                        HandleQCTask(ModuleInfo.QCTask, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SettingsChemicalParameter:
                        HandleSettingsChemicalParam(ModuleInfo.SettingsChemicalParameter, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SettingsCombProject:
                        HandleSettingsCombProjectInfo(ModuleInfo.SettingsCombProject, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SettingsCalculateItem:
                        HandleSettingsCalcProjectInfo(ModuleInfo.SettingsCalculateItem, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SettingsEnvironment:
                        LogInfo.WriteProcessLog(param, Module.WindowsService);
                        HandleEnvironmentParamInfo(ModuleInfo.SettingsEnvironment, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SettingsCrossPollution:
                        HandleSettingsCrossPollution(ModuleInfo.SettingsCrossPollution, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SettingsDataConfig:
                        HandleSettingsDataConfig(ModuleInfo.SettingsDataConfig, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SettingsLISCommunicate:
                        HandleSettingsLISCommunicate(ModuleInfo.SettingsLISCommunicate, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SystemMaintenance:
                        HandleSystemMaintenance(ModuleInfo.SystemMaintenance, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SystemEquipmentManage:
                        SystemEquipmentManage(ModuleInfo.SystemEquipmentManage, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SystemUserManagement:
                        HandleSystemUserManagement(ModuleInfo.SystemUserManagement, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SystemDepartmentManage:
                        HandleSystemDepartmentManage(ModuleInfo.SystemDepartmentManage, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SystemConfigure:
                        break;
                    case ModuleInfo.SystemLogCheck:
                        HandleSystemLogCheck(ModuleInfo.SystemLogCheck, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    case ModuleInfo.SystemVersionInfomation:
                        break;
                    case ModuleInfo.MainTain:
                        HandleMainTain(ModuleInfo.MainTain, client, (CommunicationEntity)XmlUtility.Deserialize(typeof(CommunicationEntity), param));
                        break;
                    default:
                        break;
                }
            }            

            // LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);

        }

        private void HandleCalibControlTask(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            //lock (lockObj)
            //{
            //    LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);

                switch (param.StrmethodName)
                {
                    case "QueryCalibratorinfoTask":
                        List<CalibratorinfoTask> CalibratorinfoTask = (List<CalibratorinfoTask>)XmlUtility.Deserialize(typeof(List<CalibratorinfoTask>), param.ObjParam);
                        List<CalibratorinfoTask> lisCalibrationCurveInfo = calibrator.QueryListCalibrationCurveInfo(param.StrmethodName, CalibratorinfoTask);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lisCalibrationCurveInfo));
                        break;
                    case "QueryAssayProNameAllInfo":
                        List<string> lstAllProjectName = qcTask.QueryAssayProNameAllInfo(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstAllProjectName));
                        LogInfo.WriteProcessLog(lstAllProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryProjectNameInfoByCalib":
                        List<string[]> lstProinfo = calibrator.QueryProjectNameInfoByCalib(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string[]>), lstProinfo));
                        LogInfo.WriteProcessLog(lstProinfo.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryCombProjectNameAllInfo":
                        List<string> lstCombProName = workAreaApplyTask.QueryCombProjectNameAllInfo(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstCombProName));
                        break;
                    case "QueryProjectByCombProName":
                        List<string> lstProNames = combProjectParam.QueryProjectByCombProName(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        break;
                    case "QueryAssayProNameAll":
                        List<CalibratorinfoTask> lstAll = calibrator.QueryAssayProNameAll(param.StrmethodName, param.ObjParam, param.ObjLastestParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lstAll));
                        //LogInfo.WriteProcessLog(lstAllProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryBigestCalibCTaskInfoForToday":
                        int intSampleNum = calibrator.QueryBigestCalibCTaskInfoForToday(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intSampleNum);
                        LogInfo.WriteProcessLog(intSampleNum.ToString(),Module.WindowsService);
                        break;
                    //case "QueryCalibProjectInfo":
                    //    string lstCalibProinfo = param.ObjParam.ToString();
                    //    string strSampleType = param.ObjLastestParam.ToString();
                    //    List<string[]> lstProjectName = calibrator.QueryCalibProjectInfo(param.StrmethodName,lstCalibProinfo,strSampleType);
                    //    LogInfo.WriteProcessLog(lstProjectName.Count.ToString(),Module.WindowsService);
                    //    break;
                    default:
                        break;
                }
            //}
        }  
        private void HandleLogin(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "UserLogin":
                    string[] strCommunicate = (string[])XmlUtility.Deserialize(typeof(string[]), param.ObjParam);
                    string strLogin = login.UserLogin(param.StrmethodName, strCommunicate);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strLogin);
                    break;
                case "QueryUserAuthority":
                    UserInfo userInfo = login.QueryUserAuthority(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(UserInfo), userInfo));
                    break;
            }
        }     
        private void HandleCalibrationState(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "QueryCalibrationState":
                    List<CalibrationResultinfo> lisCalibratorinfo = calibrator.QueryCalibrationState(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), lisCalibratorinfo));
                    break;
                case "QueryCalibrationResultinfo":
                    CalibrationResultinfo calibrationResultinfo = (CalibrationResultinfo)XmlUtility.Deserialize(typeof(CalibrationResultinfo), param.ObjParam);
                    List<CalibrationResultinfo> Resultinfo = calibrator.QueryCalibrationResultinfo(param.StrmethodName, calibrationResultinfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), Resultinfo));
                    break;     
               case  "QueryCalibrationReactionProcess":
                    //CalibrationResultinfo calibrationReactionProcess = (CalibrationResultinfo)XmlUtility.Deserialize(typeof(CalibrationResultinfo), param.ObjParam);
                    //List<CalibrationReactionProcess> calibrationReactionProcessResult = calibrator.QueryCalibrationReactionProcess(param.StrmethodName, calibrationReactionProcess);
                    //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibrationReactionProcess>), calibrationReactionProcessResult));
                    TimeCourseInfo calibrationResultinfoProcess = XmlUtility.Deserialize(typeof(TimeCourseInfo), param.ObjParam) as TimeCourseInfo;
                    TimeCourseInfo calibrationReactionProcessResult = calibrator.QueryCalibrationReactionProcess(param.StrmethodName, calibrationResultinfoProcess);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(TimeCourseInfo), calibrationReactionProcessResult));
                    
                    break;
               case "QueryCalibrationResultInfoAndTimeCUVNO":
                    CalibrationResultinfo calibrationResultinfoAndTimeCUVNO = XmlUtility.Deserialize(typeof(CalibrationResultinfo), param.ObjParam) as CalibrationResultinfo;
                    List<CalibrationResultinfo> calibrationResultsAndTimeCUVNO = calibrator.QueryCalibrationResultInfoAndTimeCUVNO(param.StrmethodName, calibrationResultinfoAndTimeCUVNO);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), calibrationResultsAndTimeCUVNO));                    
                    break;
                   
                case "QueryCalibrationCurveInfo":
                    CalibrationCurveInfo calibrationCurveInfo = (CalibrationCurveInfo)XmlUtility.Deserialize(typeof(CalibrationCurveInfo), param.ObjParam);
                    List<SDTTableItem> lisCalibrationCurveInfo = calibrator.QueryCalibrationCurveInfo(param.StrmethodName, calibrationCurveInfo);
                    LogInfo.WriteProcessLog("zhusizhe2", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SDTTableItem>), lisCalibrationCurveInfo));
                    LogInfo.WriteProcessLog("zhusizhe3", Module.WindowsService);
                    break;
                case "AddSDTTableItem":
                    SDTTableItem TableItem = XmlUtility.Deserialize(typeof(SDTTableItem), param.ObjParam) as SDTTableItem;
                    string strInfoState = calibrator.AddSDTTableItem(param.StrmethodName, TableItem);              
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strInfoState);
                  break;
                case "QuerysDTTableItem":

                  List<SDTTableItem> lisSDTTableItem = calibrator.QuerysDTTableItem(param.StrmethodName, param.ObjParam);
                  LogInfo.WriteProcessLog("zhusizhe2", Module.WindowsService);
                  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SDTTableItem>), lisSDTTableItem));
                  LogInfo.WriteProcessLog("zhusizhe3", Module.WindowsService);
                  break;
                default:
                    break;
            }
        }

        private void HandleCalibrationMaintain(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "QueryCalibrationMaintain":
                    List<Calibratorinfo> lisCalibratorinfo = calibrator.QueryCalibratorinfo(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfo));
                  break;
                case "QueryCalibratorProjectinfo": 
                  List<CalibratorProjectinfo> lisCalibratorProjectinfo = calibrator.QueryCalibratorProjectinfo(param.StrmethodName, param.ObjParam);
                  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo));
                  break;
                case "QueryAssayProAllInfo":
                  AssayProjectInfo assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                  List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(param.StrmethodName, assProInfo);
                  string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, str);
                  LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);
                  break;
                case "QueryProjectItemsByCalibration":
                  List<CalibratorProjectinfo> lisCalibratorProjectinfo1 = calibrator.QueryProjectItemsByCalibration(param.StrmethodName, param.ObjParam);
                  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo1));
                  break;
                case "QueryCalibPos":
                  List<Calibratorinfo> lisCalibratorinfoPos = calibrator.QueryCalibPos(param.StrmethodName, param.ObjParam);
                  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfoPos));
                  break;
                case "AddCalibratorinfo":
                  Calibratorinfo calibratorinfo = XmlUtility.Deserialize(typeof(Calibratorinfo), param.ObjParam) as Calibratorinfo;
                  List<CalibratorProjectinfo> liscalibratorProjectinfo = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), param.ObjLastestParam) as List<CalibratorProjectinfo>;
                  string strInfoState = calibrator.AddCalibratorinfo(param.StrmethodName, calibratorinfo, liscalibratorProjectinfo);              
                  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strInfoState);
                  break;
                //case "AddCalibratorProjectinfo":
                //  List<CalibratorProjectinfo> liscalibratorProjectinfo = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), param.ObjParam) as List<CalibratorProjectinfo>;
                //  string strCalibratorProjectinfo = calibrator.AddCalibratorProjectinfo(param.StrmethodName, liscalibratorProjectinfo);
                //  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), strCalibratorProjectinfo));
                //  break;
                case "DeleteCalibrationMaintain":
                   LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                   string DeteleCount = calibrator.DeleteCalibrationMaintain(param.StrmethodName, param.ObjParam);
                   LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22" + DeteleCount.ToString(), Module.WindowsService);
                   client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, DeteleCount);
                   LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                  break;         
                //case "DeleteCalibratorProjectinfo":
                //   LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                //   int Count = calibrator.DeleteCalibratorProjectinfo(param.StrmethodName, param.ObjParam);                  
                //   client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), Count.ToString()));
                //   LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                //  break;
                case "EditCalibratorinfo":
                  Calibratorinfo Editcalibratorinfo = XmlUtility.Deserialize(typeof(Calibratorinfo), param.ObjParam) as Calibratorinfo;
                  List<CalibratorProjectinfo> lisEditCalibratorProjectinfo = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), param.ObjThirdParam) as List<CalibratorProjectinfo>;
                  string ModifyCalibAndProInfo = calibrator.EditCalibratorinfo(param.StrmethodName, Editcalibratorinfo, param.ObjLastestParam, lisEditCalibratorProjectinfo);
                  LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                  client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, ModifyCalibAndProInfo);
                  LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                  break;
                //case "EditCalibratorProjectinfo":
                // List<CalibratorProjectinfo> lisEditCalibratorProjectinfo = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), param.ObjParam) as List<CalibratorProjectinfo>;
                // string EditCount = calibrator.EditCalibratorProjectinfo(param.StrmethodName, lisEditCalibratorProjectinfo, param.ObjLastestParam);
                // LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                // client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, EditCount);
                // LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                // break;
                default:
                  break;
            }
        }

        private void HandleReagentState(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "QueryReagentState":                  
                    List<ReagentStateInfoR1R2> lisReagentStateInfo = reagentState.QueryReagentStateInfo(param.StrmethodName, param.ObjParam);                
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), lisReagentStateInfo));                 
                    break;
                   
                case  "LockReagentState":
                    List<ReagentStateInfoR1R2> ReagentStateInfo = new List<ReagentStateInfoR1R2>();
                    ReagentStateInfo = XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), param.ObjParam) as List<ReagentStateInfoR1R2>;
                    int UpdataReagentStateInfo = reagentState.UpdataReagentStateInfo(param.StrmethodName, ReagentStateInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), UpdataReagentStateInfo.ToString()));                  
                    break;
                case "UnlockReagentState":
                    List<ReagentStateInfoR1R2> UnlockReagentStateInfo = new List<ReagentStateInfoR1R2>();
                    ReagentStateInfo = XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), param.ObjParam) as List<ReagentStateInfoR1R2>;
                    int UpdataUnlockReagentStateInfo = reagentState.UpdataUnlockReagentState(param.StrmethodName, ReagentStateInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), UpdataUnlockReagentStateInfo.ToString()));
                    break;
                default:
                    break;
            }
        }

        private void HandleReagentSetting(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "QueryReagentSetting1":                
                    List<ReagentSettingsInfo> lisReagentSettingsInfo = reagentSetting.QueryReagentSettingsInfo(param.StrmethodName, param.ObjParam);                 
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<ReagentSettingsInfo>), lisReagentSettingsInfo));                   
                    break;
                case "QueryReagentSetting2":              
                    List<ReagentSettingsInfo> lisReagentSettingsR2Info = reagentSetting.QueryReagentSettingsInfo2(param.StrmethodName, param.ObjParam);             
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<ReagentSettingsInfo>), lisReagentSettingsR2Info));                                
                    break;
                case "reagentSettingAddR1":                    
                    ReagentSettingsInfo reagentSettingsInfo1 = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), param.ObjParam) as ReagentSettingsInfo;
                    string strInfo1 = reagentSetting.AddreagentSettingInfo(param.StrmethodName, reagentSettingsInfo1);               
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), strInfo1));                                      
                    break;
                case "reagentSettingAddR2":                
                    ReagentSettingsInfo reagentSettingsInfo2 = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), param.ObjParam) as ReagentSettingsInfo;
                    string strInfo2 = reagentSetting.AddreagentSettingInfo2(param.StrmethodName, reagentSettingsInfo2);             
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), strInfo2));                 
                    break;
                case "reagentStateAdd":                   
                    ReagentStateInfoR1R2 reagentStateInfoR1R2 = XmlUtility.Deserialize(typeof(ReagentStateInfoR1R2), param.ObjParam) as ReagentStateInfoR1R2;
                    string strInfoState = reagentSetting.AddreagentStateInfoR1R2(param.StrmethodName, reagentStateInfoR1R2);               
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), strInfoState));                   
                    break;
                case "DeleteReagentSettingsR1":
                    ReagentSettingsInfo DeletereagentSettingsInfo = new ReagentSettingsInfo();
                    DeletereagentSettingsInfo = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), param.ObjParam) as ReagentSettingsInfo;                  
                    int DeteleDoctorCount = reagentSetting.DeletereagentSettingsInfo(param.StrmethodName, DeletereagentSettingsInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DeteleDoctorCount.ToString()));                   
                    break;
                case "DeleteReagentSettingsR2":
                    ReagentSettingsInfo DeletereagentSettingsInfo2 = new ReagentSettingsInfo();
                    DeletereagentSettingsInfo2 = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), param.ObjParam) as ReagentSettingsInfo;
                    int DeteleDoctorCount2 = reagentSetting.DeletereagentSettingsInfo2(param.StrmethodName, DeletereagentSettingsInfo2);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DeteleDoctorCount2.ToString()));
                    break;
                case "QueryAssayProAllInfo":
                    LogInfo.WriteProcessLog("zhusizhe1", Module.WindowsService);
                    AssayProjectInfo assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(param.StrmethodName, assProInfo);
                    string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, str);
                    LogInfo.WriteProcessLog("zhusizhe2", Module.WindowsService);
                    break;
                default:
                    break;

            }
        }

        private void HandleWorkingAreaDataCheck(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);
                List<SampleInfoForResult> lstSampleInfo = new List<SampleInfoForResult>();
                List<SampleResultInfo> lstSampleResultInfo = new List<SampleResultInfo>();
                SampleInfoForResult sampleInfo = new SampleInfoForResult();
                switch (param.StrmethodName)
                {
                    case "QueryCommonSampleData":
                        sampleInfo = (SampleInfoForResult)XmlUtility.Deserialize(typeof(SampleInfoForResult), param.ObjParam);
                        string strFilter = param.ObjLastestParam;
                        lstSampleInfo = workAreaDataCheck.QueryCommonSampleData(param.StrmethodName, sampleInfo, strFilter);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SampleInfoForResult>), lstSampleInfo));
                        break;
                    case "QueryProjectResultBySampleNum":
                        string[] QueryCommunicate = (string[])XmlUtility.Deserialize(typeof(string[]), param.ObjParam);
                        lstSampleResultInfo = workAreaDataCheck.QueryProjectResultBySampleNum(param.StrmethodName, QueryCommunicate);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SampleResultInfo>), lstSampleResultInfo));
                        break;
                    case "QueryProjectResultForTestAudit":
                        string[] QueryCommuForTest = (string[])XmlUtility.Deserialize(typeof(string[]), param.ObjParam);
                        lstSampleResultInfo = workAreaDataCheck.QueryProjectResultBySampleNum(param.StrmethodName, QueryCommuForTest);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SampleResultInfo>), lstSampleResultInfo));
                        break;                    case "DeleteCommonSampleBySampleNum":
                        string[] DeleteCommunicate = (string[])XmlUtility.Deserialize(typeof(string[]), param.ObjParam);
                        string strDeleteRes = workAreaDataCheck.DeleteCommonSampleBySampleNum(param.StrmethodName, DeleteCommunicate);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strDeleteRes);
                        break;
                    case "ReviewCheck":
                        string[] reviewCheckParam = (string[])XmlUtility.Deserialize(typeof(string[]), param.ObjParam);
                        string strReviewCheckRes = workAreaDataCheck.ReviewCheck(param.StrmethodName, reviewCheckParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strReviewCheckRes);
                        break;
                    case "AuditSampleTest":
                        string[] auditParam = (string[])XmlUtility.Deserialize(typeof(string[]), param.ObjParam);
                        string strAuditRes = workAreaDataCheck.AuditSampleTest(param.StrmethodName, auditParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strAuditRes);
                        break;
                    case "QueryTimeCourse":
                        SampleResultInfo sampleResInfo = XmlUtility.Deserialize(typeof(SampleResultInfo), param.ObjParam) as SampleResultInfo;
                        TimeCourseInfo sampleReactionInfo = workAreaDataCheck.QueryCommonTaskReaction(param.StrmethodName, sampleResInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(TimeCourseInfo), sampleReactionInfo));
                        break;
                    case "QueryCommonTaskReactionForAudit":
                        SampleResultInfo sampleResInfoForAudit = XmlUtility.Deserialize(typeof(SampleResultInfo), param.ObjParam) as SampleResultInfo;
                        TimeCourseInfo sampleReacInfoForAudit = workAreaDataCheck.QueryCommonTaskReaction("QueryTimeCourse", sampleResInfoForAudit);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(TimeCourseInfo), sampleReacInfoForAudit));
                        break;
                    case "BatchAuditSampleTest":
                        List<string[]> lstBatchAuditParam = XmlUtility.Deserialize(typeof(List<string[]>), param.ObjParam) as List<string[]>;
                        string strBatchResult = workAreaDataCheck.BatchAuditSampleTest(param.StrmethodName, lstBatchAuditParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strBatchResult);
                        break;
                    case "ConfirmCommonTask":
                        List<string[]> lstConfirmInfo = XmlUtility.Deserialize(typeof(List<string[]>), param.ObjParam) as List<string[]>;
                        string strConfirmInfo = workAreaDataCheck.ConfirmCommonTask(param.StrmethodName, lstConfirmInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strConfirmInfo);
                        break;
                    default:
                        break;
                }
            }
        }
        private void HandleSystemLogCheck(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {            
            switch (param.StrmethodName)
            {

                case "QueryMaintenanceLogInfo":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    List<MaintenanceLogInfo> lisMaintenanceLogInfo = systemLogCheck.QueryMaintenanceLogInfo(param.StrmethodName, param.ObjParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<MaintenanceLogInfo>), lisMaintenanceLogInfo));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "QueryOperationLogInfo":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    List<MaintenanceLogInfo> lisOperationLogInfo = systemLogCheck.QueryOperationLogInfo(param.StrmethodName, param.ObjParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<MaintenanceLogInfo>), lisOperationLogInfo));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "QueryAlarmLogInfo":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    List<AlarmLogInfo> lisAlarmLogInfo = systemLogCheck.QueryAlarmLogInfo(param.StrmethodName, param.ObjParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<AlarmLogInfo>), lisAlarmLogInfo));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "SelectAlarmLogInfo":
                    AlarmLogInfo alarmLogInfo = XmlUtility.Deserialize(typeof(AlarmLogInfo), param.ObjParam) as AlarmLogInfo;
                    List<AlarmLogInfo> lisSelectAlarmLogInfo = systemLogCheck.SelectAlarmLogInfo(param.StrmethodName, alarmLogInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<AlarmLogInfo>), lisSelectAlarmLogInfo));
                    break;
                default:
                    break;
            }
        }

        private void SystemEquipmentManage(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "QueryManuOffsetGain":
                    ManuOffsetGain manuGain = systemEquipmentManage.QueryManuOffsetGain(param.StrmethodName);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(ManuOffsetGain), manuGain));
                    break;
                case "InitialPhotometerManualCheck":
                    int iResult = systemEquipmentManage.InitialPhotometerManualCheck(param.StrmethodName, XmlUtility.Deserialize(typeof(ManuOffsetGain), param.ObjParam) as ManuOffsetGain);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, iResult);
                    break;
                case "GetLatestOffSetGain":
                    OffSetGain offSetGain = systemEquipmentManage.GetLatestOffSetGain(int.Parse(param.ObjParam as string));
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(OffSetGain), offSetGain));
                    break;
                default:
                    break;
            }
        }

        private void HandleSystemDepartmentManage(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();

            switch (param.StrmethodName)
            {
                case "QueryDepartmentInfo":
                    List<string> lisDepartment = systemDepartmentManage.QueryDepartment(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lisDepartment));
                    break;
                case "AddDepartmentInfo":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    string strInfo = systemDepartmentManage.AddDepartmentInfo(param.StrmethodName, param.ObjParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), strInfo));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "UpDataDepartment":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    int UpdataCount = systemDepartmentManage.UpdataDepartment(param.StrmethodName, param.ObjParam, param.ObjLastestParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), UpdataCount.ToString()));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "DeleteDepartment":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    int DeteleCount = systemDepartmentManage.DeleteDepartment(param.StrmethodName, param.ObjParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22" + DeteleCount.ToString(), Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DeteleCount.ToString()));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "QueryApplyDoctorInfo":
                    List<ApplyDoctorInfo> lstApplyDoctor = systemDepartmentManage.QueryApplyDoctor(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<ApplyDoctorInfo>), lstApplyDoctor));
                    break;
                case "AddApplyDoctorInfo":
                    applyDoctorInfo = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), param.ObjParam) as ApplyDoctorInfo;
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    string AddApplyDoctor = systemDepartmentManage.AddApplyDoctor(param.StrmethodName, applyDoctorInfo);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(ApplyDoctorInfo), AddApplyDoctor));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "DeleteApplyDoctorInfo":
                    applyDoctorInfo = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), param.ObjParam) as ApplyDoctorInfo;
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    int DeteleDoctorCount = systemDepartmentManage.DeleteApplyDoctorInfo(param.StrmethodName, applyDoctorInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DeteleDoctorCount.ToString()));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "UpdataApplyDoctorInfo":
                    ApplyDoctorInfo applyDoctorInfoOld = new ApplyDoctorInfo();
                    applyDoctorInfo = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), param.ObjParam) as ApplyDoctorInfo;
                    applyDoctorInfoOld = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), param.ObjLastestParam) as ApplyDoctorInfo;
                    int UpdataDoctorInfoOldCount = systemDepartmentManage.UpdataApplyDoctorInfo(param.StrmethodName, applyDoctorInfo, applyDoctorInfoOld);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), UpdataDoctorInfoOldCount.ToString()));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "QueryUserInfo":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    List<UserInfo> lisUserInfo = systemUserManagement.QueryUserManagement(param.StrmethodName, param.ObjParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<UserInfo>), lisUserInfo));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "QueryAuditPhysician":
                    List<string> lstAuditPhysician = systemDepartmentManage.QueryAuditPhysician(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstAuditPhysician));
                    break;
                case "AddAuditPhysician":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    string AddAuditPhysician = systemDepartmentManage.AddAuditPhysician(param.StrmethodName, param.ObjParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), AddAuditPhysician));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;
                case "DeleteAuditPhysician":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    int DeteleAuditPhysicianCount = systemDepartmentManage.DeleteAuditPhysician(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DeteleAuditPhysicianCount.ToString()));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;

                case "UpdataAuditPhysician":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe11", Module.WindowsService);
                    int UpDataAuditPhysicianCount = systemDepartmentManage.UpDataAuditPhysician(param.StrmethodName, param.ObjParam, param.ObjLastestParam);
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe22", Module.WindowsService);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), UpDataAuditPhysicianCount.ToString()));
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe33", Module.WindowsService);
                    break;

                default:
                    break;
            }
        }

        private void HandleSystemUserManagement(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            UserInfo userInfo = new UserInfo();
            switch (param.StrmethodName)
            {

                case "QueryUserInfo":
                    List<UserInfo> lisUserInfo = systemUserManagement.QueryUserManagement(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<UserInfo>), lisUserInfo));
                    break;
                case "QueryUserCeation":
                    List<UserInfo> lisCeation = systemUserManagement.QueryUserCeation(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<UserInfo>), lisCeation));
                    break;
                case "AddUserInfo":
                    userInfo = XmlUtility.Deserialize(typeof(UserInfo), param.ObjParam) as UserInfo;
                    string strInfo = systemUserManagement.AddUserInfo(param.StrmethodName, userInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strInfo);
                    LogInfo.WriteProcessLog(strInfo, Module.WindowsService);
                    break;
                case "EditUserInfo":
                    UserInfo UserInfoOld = new UserInfo();
                    userInfo = XmlUtility.Deserialize(typeof(UserInfo), param.ObjParam) as UserInfo;
                    UserInfoOld.UserID = param.ObjLastestParam;
                    int intUpDateResult = systemUserManagement.EditUserInfoUpDate(param.StrmethodName, userInfo, UserInfoOld);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intUpDateResult);
                    break;
                case "DeleteUserInfo":
                    int intDeleteCount = systemUserManagement.DeleteUserInfo(param.StrmethodName, param.ObjParam, param.ObjLastestParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intDeleteCount.ToString());
                    break;
                default:
                    break;
            }
        }

        private void HandleWorkingAreaApplyTask(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            lock (lockObj)
            {
                string strResult = "";
                List<TaskInfo> lstTask = new List<TaskInfo>();
                switch (param.StrmethodName)
                {
                    case "QueryMaxSampleNum":
                        int intMaxNum = workAreaApplyTask.QueryMaxSampleNum(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intMaxNum);
                        break;
                    case "QuerySampleDiluteRatio":
                        List<string> lisQueryDilutionRatio = settingsDataConfig.QueryDilutionRatio(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lisQueryDilutionRatio));
                        break;                
                    case "QueryProNameForApplyTask":
                        List<string[]> lstProName = workAreaApplyTask.QueryProNameForApplyTask(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string[]>), lstProName));
                        break;
                    case "QueryCombProjectNameAllInfo":
                        List<string> lstCombProName = workAreaApplyTask.QueryCombProjectNameAllInfo(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstCombProName));
                        break;
                    case "QueryApplyTaskLsvt":
                        List<SampleInfo> lstSampleInfo = workAreaApplyTask.QueryApplyTaskLsvt(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SampleInfo>), lstSampleInfo));
                        break;
                    case "QueryProjectByCombProName":
                        List<string> lstProNames = combProjectParam.QueryProjectByCombProName(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        break;
                    case "AddTask":
                        SampleInfo sample = XmlUtility.Deserialize(typeof(SampleInfo), param.ObjParam)as SampleInfo;
                        lstTask = XmlUtility.Deserialize(typeof(List<TaskInfo>), param.ObjLastestParam) as List<TaskInfo>;
                        strResult = workAreaApplyTask.AddTask(param.StrmethodName, sample, lstTask);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strResult);
                        break;
                    case "AddTaskForBatch":
                        SampleInfo sampleForBatch = XmlUtility.Deserialize(typeof(SampleInfo), param.ObjParam)as SampleInfo;
                        lstTask = XmlUtility.Deserialize(typeof(List<TaskInfo>), param.ObjLastestParam) as List<TaskInfo>;
                        strResult = workAreaApplyTask.AddTask(param.StrmethodName, sampleForBatch, lstTask);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, sampleForBatch.SampleNum.ToString() + "," + strResult);
                        break;
                    case "QueryTaskInfoBySampleNum":
                        lstTask = workAreaApplyTask.QueryTaskInfoBySampleNum(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<TaskInfo>), lstTask));
                        break;
                    case "QueryPatientInfoBySampleNum":
                        PatientInfo patientInfo = workAreaApplyTask.QueryPatientInfoBySampleNum(param.StrmethodName, System.Convert.ToInt32(param.ObjParam));
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(PatientInfo), patientInfo));
                        break;
                    case "UpdatePatientInfo":
                        string strUpdateInfo = workAreaApplyTask.UpdatePatientInfo(param.StrmethodName, XmlUtility.Deserialize(typeof(PatientInfo), param.ObjParam) as PatientInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strUpdateInfo);
                        break;
                    case "QueryApplyApartment":
                        List<string> applyApartment = workAreaApplyTask.QueryApplyApartment(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), applyApartment));
                        break;
                    case "QueryApplyDoctor":
                        List<string> applyDoctor = workAreaApplyTask.QueryApplyDoctor(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), applyDoctor));
                        break;
                    case "QueryCheckDoctor":
                        List<string> checkDoctor = workAreaApplyTask.QueryCheckDoctor(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), checkDoctor));
                        break;
                    case "QueryInspectDoctor":
                        List<string> inspectDoctor = workAreaApplyTask.QueryInspectDoctor(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), inspectDoctor));
                        break;
                    case "QueryPatientInfos":
                        List<PatientInfo> lstPatientInfo = workAreaApplyTask.QueryPatientInfos(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<PatientInfo>), lstPatientInfo));
                        break;
                    case "QueryTaskInfoForSamplePanel":
                        string[] paramInfo = XmlUtility.Deserialize(typeof(string[]), param.ObjParam) as string[];
                        TaskInfoForSamplePanelInfo taskInfoForSamPanel = workAreaApplyTask.QueryTaskInfoForSamplePanel(param.StrmethodName, paramInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(TaskInfoForSamplePanelInfo), taskInfoForSamPanel));
                        break;
                    case "QuerySamplePanelState":
                        List<SampleInfo> lstSampleInfoForPanel = workAreaApplyTask.QuerySamplePanelState(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SampleInfo>), lstSampleInfoForPanel));
                        break;
                    case "UpdateRunningTaskWorDisk":
                        workAreaApplyTask.UpdateRunningTaskWorDisk(param.StrmethodName, param.ObjParam);
                        break;
                    default:
                        break;
                }
            }
        }

        private void HandleSettingsCrossPollution(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo = new ReagentNeedleAntifoulingStrategyInfo();
            AssayProjectInfo assProInfo = new AssayProjectInfo();
            switch (param.StrmethodName)
            {
                case "AddReagentNeedle":
                    reagentNeedleAntifoulingStrategyInfo = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), param.ObjParam) as ReagentNeedleAntifoulingStrategyInfo;
                    string strInfo = settingsReagentNeedle.AddsettingsReagentNeedle(param.StrmethodName, reagentNeedleAntifoulingStrategyInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strInfo);
                    break;
                case "QueryReagentNeedle":
                    reagentNeedleAntifoulingStrategyInfo = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), param.ObjParam) as ReagentNeedleAntifoulingStrategyInfo;
                    List<ReagentNeedleAntifoulingStrategyInfo> lisQueryReagentNeedle = settingsReagentNeedle.QueryReagentNeedle(param.StrmethodName, null);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<ReagentNeedleAntifoulingStrategyInfo>), lisQueryReagentNeedle));
                    break;
                case "DeleteReagentNeedle":
                    reagentNeedleAntifoulingStrategyInfo = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), param.ObjParam) as ReagentNeedleAntifoulingStrategyInfo;
                    int intDeleteCount = settingsReagentNeedle.ReagentNeedleDelete(param.StrmethodName, reagentNeedleAntifoulingStrategyInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intDeleteCount);
                    break;
                case "UpdataReagentNeedle":
                    ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfoOld = new ReagentNeedleAntifoulingStrategyInfo();
                    reagentNeedleAntifoulingStrategyInfo = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), param.ObjParam) as ReagentNeedleAntifoulingStrategyInfo;
                    reagentNeedleAntifoulingStrategyInfoOld = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), param.ObjLastestParam) as ReagentNeedleAntifoulingStrategyInfo;
                    string strUpDateResult = settingsReagentNeedle.ReagentNeedleUpDate(param.StrmethodName, reagentNeedleAntifoulingStrategyInfo, reagentNeedleAntifoulingStrategyInfoOld);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strUpDateResult);

                    break;
                case "QueryAssayProAllInfo":
                    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(param.StrmethodName, assProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos));
                    break;
                case "QueryWashingLiquid":
                    List<string> lstWashingLiquid = settingsReagentNeedle.QueryWashingLiquid(param.StrmethodName);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstWashingLiquid));
                    break;
                default:
                    break;
            }
        }

        private void HandleSettingsDataConfig(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "QueryDataConfig":
                    List<string> lisQueryDataConfig = settingsDataConfig.QueryDataConfig(param.StrmethodName, null);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lisQueryDataConfig));
                    break;
                case "DataConfigAdd":
                    string strInfo = settingsDataConfig.DataConfigAdd(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), strInfo));
                    break;
                case "UpdataDataConfig":
                    int UpdataCount = settingsDataConfig.UpdataDataConfig(param.StrmethodName, param.ObjParam, param.ObjLastestParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), UpdataCount.ToString()));
                    break;
                case "DeleteDataConfig":
                    int DeteleCount = settingsDataConfig.DeleteDataConfig(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DeteleCount.ToString()));
                    break;
                case "QueryDilutionRatio":
                    List<string> lisQueryDilutionRatio = settingsDataConfig.QueryDilutionRatio(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lisQueryDilutionRatio));
                    break;

                case "DilutionRatioAdd":
                    string DilutionRatioInfo = settingsDataConfig.DilutionRatioAdd(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DilutionRatioInfo));
                    break;

                case "UpdataDilutionRatio":
                    int UpdataDilutionRatioCount = settingsDataConfig.UpdataDilutionRatio(param.StrmethodName, param.ObjParam, param.ObjLastestParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), UpdataDilutionRatioCount.ToString()));
                    break;

                case "DeleteDilutionRatio":
                    int DeteleDilutionRatioCount = settingsDataConfig.DeleteDilutionRatio(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), DeteleDilutionRatioCount.ToString()));
                    break;
                default:
                    break;
            }
        }

        private void HandleSettingsLISCommunicate(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            LISCommunicateNetworkInfo lISCommunicateInfo = new LISCommunicateNetworkInfo();
            SerialCommunicationInfo serialCommunicationInfo = new SerialCommunicationInfo();
            switch (param.StrmethodName)
            {
                case "NetworkLISCommunicate":
                    lISCommunicateInfo = XmlUtility.Deserialize(typeof(LISCommunicateNetworkInfo), param.ObjParam) as LISCommunicateNetworkInfo;
                    List<LISCommunicateNetworkInfo> lstLISCommunicateInfo = settingsChemicalParam.QueryLISCommunicateInfo(param.StrmethodName, lISCommunicateInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<LISCommunicateNetworkInfo>), lstLISCommunicateInfo));
                    break;
                case "NetworkLISCommunicateUpDate":
                    lISCommunicateInfo = XmlUtility.Deserialize(typeof(LISCommunicateNetworkInfo), param.ObjParam) as LISCommunicateNetworkInfo;
                    int intNetworkUpDateResult = settingsChemicalParam.NetworkUpDate(param.StrmethodName, lISCommunicateInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intNetworkUpDateResult);
                    break;
                case "SerialLISCommunicate":
                    serialCommunicationInfo = XmlUtility.Deserialize(typeof(SerialCommunicationInfo), param.ObjParam) as SerialCommunicationInfo;
                    List<SerialCommunicationInfo> lstSerialCommunicationInfo = settingsChemicalParam.QuerySerialCommunicationInfo(param.StrmethodName, serialCommunicationInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<SerialCommunicationInfo>), lstSerialCommunicationInfo));
                    break;
                case "SerialLISCommunicateUpDate":
                    LogInfo.WriteProcessLog(param.StrmethodName + "zhuszihe1", Module.WindowsService);
                    serialCommunicationInfo = XmlUtility.Deserialize(typeof(SerialCommunicationInfo), param.ObjParam) as SerialCommunicationInfo;
                    int intSerialUpDateResult = settingsChemicalParam.SerialUpDate(param.StrmethodName, serialCommunicationInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intSerialUpDateResult);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处理设置-生化项目数据
        /// </summary>
        private void HandleSettingsChemicalParam(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {

            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);
                AssayProjectInfo assProInfo = new AssayProjectInfo();
                switch (param.StrmethodName)
                {
                    case "QueryAssayProAllInfo":
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(param.StrmethodName, assProInfo);
                        string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, str);
                        LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "AssayProjectAdd":
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        string[] strInfo = settingsChemicalParam.AddAssayProject(param.StrmethodName, assProInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string[]), strInfo));
                        LogInfo.WriteProcessLog(strInfo[4], Module.WindowsService);
                        break;
                    case "GetAssayProjectParamInfoByNameAndType":
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        AssayProjectParamInfo assayProParamInfo = settingsChemicalParam.GetAssayProjectParamInfoByNameAndType(param.StrmethodName, assProInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(AssayProjectParamInfo), assayProParamInfo));
                        LogInfo.WriteProcessLog(assayProParamInfo.ProjectName, Module.WindowsService);
                        break;
                    case "QueryProjectResultUnits":
                        List<string> lstUnits = settingsChemicalParam.QueryProjectResultUnits(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstUnits));
                        break;
                    case "UpdateAssayProjectParamInfo":
                        AssayProjectParamInfo assProParamInfo = XmlUtility.Deserialize(typeof(AssayProjectParamInfo), param.ObjParam) as AssayProjectParamInfo;
                        int intResult = settingsChemicalParam.UpdateAssayProjectParamInfo(param.StrmethodName, assProParamInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intResult);
                        break;
                    case "AssayProjectEdit":
                        AssayProjectInfo assayProInfoOld = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        AssayProjectInfo assayProInfoLast = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjLastestParam) as AssayProjectInfo;
                        LogInfo.WriteProcessLog(param.ObjParam, Module.WindowsService);
                        LogInfo.WriteProcessLog(param.ObjLastestParam, Module.WindowsService);
                        int intEditResult = settingsChemicalParam.EditAssayProject(param.StrmethodName, assayProInfoOld, assayProInfoLast);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intEditResult);
                        break;
                    case "AssayProjectDelete":
                        List<AssayProjectInfo> lstProInfos = XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), param.ObjParam) as List<AssayProjectInfo>;
                        int intDeleteCount = settingsChemicalParam.AssayProjectDelete(param.StrmethodName, lstProInfos);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intDeleteCount);
                        break;
                    //case "ProjectPageinfo":         // 获取生化项目信息
                    //    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    //    List<AssayProjectInfo> assayProInfos = settingsChemicalParam.QueryAssayProAllInfoByDistinctProName(param.StrmethodName, assProInfo);
                    //    client.NotifyCallBack.DatabaseNotifyFunction(param.StrmethodName, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), assayProInfos));
                    //    break;
                    case "QueryAssayProAllInfoForCalibParam": // 校准参数界面获取项目信息
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        List<AssayProjectInfo> lstAssayProInfosForCalibparam = settingsChemicalParam.QueryAssayProAllInfo("QueryAssayProAllInfo", assProInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfosForCalibparam));
                        LogInfo.WriteProcessLog(lstAssayProInfosForCalibparam.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryCalibParamByProNameAndType": // 通过项目名称和项目类型获取项目校准参数
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        AssayProjectCalibrationParamInfo calibParam = settingsChemicalParam.QueryCalibParamByProNameAndType(param.StrmethodName, assProInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(AssayProjectCalibrationParamInfo), calibParam));
                        break;
                    case "UpdateCalibParamByProNameAndType":    // 通过项目名称和类型更新校准参数
                        AssayProjectCalibrationParamInfo sender = XmlUtility.Deserialize(typeof(AssayProjectCalibrationParamInfo), param.ObjParam) as AssayProjectCalibrationParamInfo;
                        int intCalibResult = settingsChemicalParam.UpdateCalibParamByProNameAndType(param.StrmethodName, sender);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intCalibResult);
                        break;
                    case "QueryRangeParamByProNameAndType": // 通过项目名称和项目类型获取项目范围参数
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        AssayProjectRangeParamInfo rangeParam = settingsChemicalParam.QueryRangeParamByProNameAndType(param.StrmethodName, assProInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(AssayProjectRangeParamInfo), rangeParam));
                        break;
                    case "UpdateRangeParamByProNameAndType":
                        AssayProjectRangeParamInfo rangeSender = XmlUtility.Deserialize(typeof(AssayProjectRangeParamInfo), param.ObjParam) as AssayProjectRangeParamInfo;
                        int intRangeResult = settingsChemicalParam.UpdateRangeParamByProNameAndType(param.StrmethodName, rangeSender);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intRangeResult);
                        break;
                    case "QueryAssayProAllInfoForRangeParam": // 范围参数界面获取项目信息
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        List<AssayProjectInfo> lstAssayProInfosForRangeparam = settingsChemicalParam.QueryAssayProAllInfo("QueryAssayProAllInfo", assProInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfosForRangeparam));
                        LogInfo.WriteProcessLog(lstAssayProInfosForRangeparam.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryCalibratorProinfo": 
                        List<CalibratorProjectinfo> lisCalibratorProjectinfo = settingsChemicalParam.QueryCalibratorProjectinfo(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo));
                        break;
                    case "QueryCalib":
                        List<Calibratorinfo> lisCalibratorinfo = settingsChemicalParam.QueryCalib(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfo));
                        break;
                    case "AddCalibrationCurveInfo":
                        List<CalibrationCurveInfo> calibrationCurveInfo = XmlUtility.Deserialize(typeof(List<CalibrationCurveInfo>), param.ObjParam) as List<CalibrationCurveInfo>;
                        string strCurveInfo = settingsChemicalParam.AddCalibrationCurveInfo(param.StrmethodName, calibrationCurveInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(string), strCurveInfo));
                        break;
                    case "QueryCalibrationCurve":
                        List<CalibrationCurveInfo> lisCalibrationCurveInfo = settingsChemicalParam.QueryCalibrationCurveInfo(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalibrationCurveInfo>), lisCalibrationCurveInfo));
                        break;
                       
                    default:
                        break;
                }

            }

        }

        /// <summary>
        /// 处理设置-组合项目数据
        /// </summary>
        private void HandleSettingsCombProjectInfo(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {

            LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);

            switch (param.StrmethodName)
            {
                case "QueryCombProjectNameAllInfo":
                    List<CombProjectInfo> lstCombProInfos = combProjectParam.QueryCombProjectNameAllInfo(param.StrmethodName);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CombProjectInfo>), lstCombProInfos));
                    break;
                case "QueryProjectByCombProName":
                    List<string> lstProNames = combProjectParam.QueryProjectByCombProName(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                    break;
                case "AddCombProjectName":
                    CombProjectInfo combProInfo = (CombProjectInfo)XmlUtility.Deserialize(typeof(CombProjectInfo), param.ObjParam);
                    string strResult = combProjectParam.AddCombProjectName(param.StrmethodName, combProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strResult);
                    break;
                case "DeleteCombProjectName":
                    List<CombProjectInfo> lstInfos = (List<CombProjectInfo>)XmlUtility.Deserialize(typeof(List<CombProjectInfo>), param.ObjParam);
                    int intResult = combProjectParam.DeleteCombProjectName(param.StrmethodName, lstInfos);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intResult);
                    break;
                case "UpdateCombProjectName":
                    CombProjectInfo combProInfoOld = (CombProjectInfo)XmlUtility.Deserialize(typeof(CombProjectInfo), param.ObjParam);
                    CombProjectInfo combProInfoNew = (CombProjectInfo)XmlUtility.Deserialize(typeof(CombProjectInfo), param.ObjLastestParam);
                    int intUpdateResult = combProjectParam.UpdateCombProjectName(param.StrmethodName, combProInfoOld, combProInfoNew);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intUpdateResult);
                    break;
                case "ProjectPageinfo":     // 获取生化项目信息
                    AssayProjectInfo assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                    List<string> assayProInfos = settingsChemicalParam.QueryAssayProAllInfoByDistinctProName("QueryAssayProAllInfoByDistinctProName", assProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), assayProInfos));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处理设置-计算项目数据
        /// </summary>
        private void HandleSettingsCalcProjectInfo(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {

            LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);

            switch (param.StrmethodName)
            {
                case "QueryCalcProjectAllInfo":
                    List<CalcProjectInfo> lstCalcProInfos = calcProjectParam.QueryCalcProjectAllInfo(param.StrmethodName);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CalcProjectInfo>), lstCalcProInfos));
                    break;
                case "AddCalcProject":
                    CalcProjectInfo calcProInfo = (CalcProjectInfo)XmlUtility.Deserialize(typeof(CalcProjectInfo), param.ObjParam);
                    string strResult = calcProjectParam.AddCalcProject(param.StrmethodName, calcProInfo);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strResult);
                    break;
                case "DeleteCalcProject":
                    List<CalcProjectInfo> lstInfos = (List<CalcProjectInfo>)XmlUtility.Deserialize(typeof(List<CalcProjectInfo>), param.ObjParam);
                    int intResult = calcProjectParam.DeleteCalcProject(param.StrmethodName, lstInfos);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intResult);
                    break;
                case "UpdateCalcProject":
                    CalcProjectInfo calcProInfoOld = (CalcProjectInfo)XmlUtility.Deserialize(typeof(CalcProjectInfo), param.ObjParam);
                    CalcProjectInfo calcProInfoNew = (CalcProjectInfo)XmlUtility.Deserialize(typeof(CalcProjectInfo), param.ObjLastestParam);
                    int intUpdateResult = calcProjectParam.UpdateCalcProject(param.StrmethodName, calcProInfoOld, calcProInfoNew);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intUpdateResult);
                    break;
                case "QueryProjectResultUnits":
                    List<string> lstUnits = settingsChemicalParam.QueryProjectResultUnits(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstUnits));
                    break;
                case "ProjectPageinfoForCalc":         // 获取生化项目信息
                    List<string> assayProInfos = calcProjectParam.ProjectPageinfoForCalc(param.StrmethodName, param.ObjParam);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), assayProInfos));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 处理设置-环境参数数据
        /// </summary>
        private void HandleEnvironmentParamInfo(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            LogInfo.WriteProcessLog(param.StrmethodName + "zhusizhe", Module.WindowsService);

            switch (param.StrmethodName)
            {
                case "UpdateEnvironmentParamInfo":
                    EnvironmentParamInfo environment = XmlUtility.Deserialize(typeof(EnvironmentParamInfo), param.ObjParam) as EnvironmentParamInfo;
                    RunningStateInfo running = XmlUtility.Deserialize(typeof(RunningStateInfo), param.ObjLastestParam) as RunningStateInfo;
                    int intResult = environmentParam.UpdateEnvironmentParamInfo(param.StrmethodName, environment, running);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intResult);
                    break;
                case "QueryEnvironmentParamInfo":
                    List<EnvironmentParamInfo> lstEnvironmentParamInfo = environmentParam.QueryEnvironmentParamInfo(param.StrmethodName);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<EnvironmentParamInfo>), lstEnvironmentParamInfo));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 质控-质控品维护处理
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCMaintain(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);

                AssayProjectInfo assProInfo = new AssayProjectInfo();
                QualityControlInfo qcInfo = new QualityControlInfo();
                switch (param.StrmethodName)
                {
                    case "QueryAssayProAllInfo":
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), param.ObjParam) as AssayProjectInfo;
                        List<AssayProjectInfo> lstAssayProInfos = qcMaintian.QueryAssayProAllInfo(param.StrmethodName, assProInfo);
                        string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, str);
                        LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryQCPosition":
                        List<string> qcPositons = qcMaintian.QueryQCPosition(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), qcPositons));
                        LogInfo.WriteProcessLog(qcPositons.Count.ToString(), Module.WindowsService);
                        break;
                    case "AddQualityControl":
                        qcInfo = XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam) as QualityControlInfo;
                        List<QCRelationProjectInfo> lstQCRelationProInfo = XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), param.ObjLastestParam) as List<QCRelationProjectInfo>;
                        string strResult = qcMaintian.AddQualityControl("AddQualityControl", qcInfo, lstQCRelationProInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strResult);
                        LogInfo.WriteProcessLog(strResult, Module.WindowsService);
                        break;
                    case "QueryQCAllInfo":
                        List<QualityControlInfo> lstQCInfos = qcMaintian.QueryQCAllInfo(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                        LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryRelativelyProjectByQCInfo":
                        qcInfo = XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam) as QualityControlInfo;
                        List<QCRelationProjectInfo> lstQCRelationPros = qcMaintian.QueryRelativelyProjectByQCInfo(param.StrmethodName, qcInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QCRelationProjectInfo>), lstQCRelationPros));
                        LogInfo.WriteProcessLog(lstQCRelationPros.Count.ToString(), Module.WindowsService);
                        break;
                    case "EditQCRelateProInfo":
                        int iEditQCProResult = qcMaintian.EditQCRelateProInfo(param.StrmethodName, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam), (List<QCRelationProjectInfo>)XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), param.ObjLastestParam));
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, iEditQCProResult);
                        LogInfo.WriteProcessLog(iEditQCProResult.ToString(), Module.WindowsService);
                        break;
                    case "EditQualityControl":
                        string strQCRes = qcMaintian.EditQualityControl("EditQualityControl", (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam), (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjLastestParam));
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strQCRes);
                        LogInfo.WriteProcessLog(strQCRes.ToString(), Module.WindowsService);
                        break;
                    case "LockQualityControl":
                        int intLockResult = qcMaintian.LockQualityControl(param.StrmethodName, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam));
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intLockResult);
                        LogInfo.WriteProcessLog(intLockResult.ToString(), Module.WindowsService);
                        break;
                    case "UnLockQualityControl":
                        int intUnLockResult = qcMaintian.UnLockQualityControl(param.StrmethodName, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam));
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, intUnLockResult);
                        LogInfo.WriteProcessLog(intUnLockResult.ToString(), Module.WindowsService);
                        break;
                    case "DeleteQualityControl":
                        string strDeleteResult = qcMaintian.DeleteQualityControl(param.StrmethodName, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam));
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strDeleteResult);
                        LogInfo.WriteProcessLog(strDeleteResult, Module.WindowsService);
                        break;
                    default:
                        break;
                }

            }

        }


        /// <summary>
        /// 质控-质控结果数据处理
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCResult(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);
                QCResultForUIInfo qCResForUI = new QCResultForUIInfo();
                switch (param.StrmethodName)
                {
                    case "QueryQCResultInfo":
                        qCResForUI = XmlUtility.Deserialize(typeof(QCResultForUIInfo), param.ObjParam) as QCResultForUIInfo;
                        List<QCResultForUIInfo> lstqCResForUIInfos = qcResult.QueryQCResultInfo(param.StrmethodName, qCResForUI);
                        string str = XmlUtility.Serializer(typeof(List<QCResultForUIInfo>), lstqCResForUIInfos);                        
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, str);
                        LogInfo.WriteProcessLog(lstqCResForUIInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryQCInfosForAddQCResult":
                        QualityControlInfo qcInfo = new QualityControlInfo();
                        List<QualityControlInfo> lstQCInfos = qcResult.QueryQCInfosForAddQCResult(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                        LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryProjectName":
                        List<string> lstProjectName = new List<string>();
                        lstProjectName = qcResult.QueryProjectName(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstProjectName));
                        LogInfo.WriteProcessLog(lstProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "EditQCResultForManual":
                        qCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), param.ObjParam);
                        QCResultForUIInfo editQCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), param.ObjLastestParam);
                        string EditResult = qcResult.EditQCResultForManual(param.StrmethodName, qCResForUI, editQCResForUI);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, EditResult);
                        LogInfo.WriteProcessLog(EditResult, Module.WindowsService);
                        break;
                    case "AddQCResultForManual":                        
                        QCResultForUIInfo AddQCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), param.ObjParam);
                        string AddResult = qcResult.AddQCResultForManual(param.StrmethodName, AddQCResForUI);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, AddResult);
                        LogInfo.WriteProcessLog(AddResult, Module.WindowsService);
                        break;
                    case "DeleteQCResult":
                        qCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), param.ObjParam);
                        string strDeleteRes = qcResult.DeleteQCResult(param.StrmethodName, qCResForUI);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strDeleteRes);
                        LogInfo.WriteProcessLog(strDeleteRes, Module.WindowsService);
                        break;
                    case "QueryTimeCourseByQCInfo":
                        qCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), param.ObjParam);
                        TimeCourseInfo qCTimeCourseInfo = qcResult.QueryTimeCourseByQCInfo(param.StrmethodName, qCResForUI);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(TimeCourseInfo), qCTimeCourseInfo));
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 质控-质控图数据处理
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCGraphics(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);


                switch (param.StrmethodName)
                {
                    case "QueryProjectName":
                        List<string> lstProjectName = new List<string>();
                        lstProjectName = qcGraphics.QueryProjectName(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstProjectName));
                        LogInfo.WriteProcessLog(lstProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryQCAllInfo":
                        List<QualityControlInfo> lstQCInfos = qcGraphics.QueryQCAllInfo(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                        LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryQCResultForQCGraphics":
                        QCResultForUIInfo qCResForUIInfo = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), param.ObjParam);
                        List<QCResultForUIInfo> results = qcGraphics.QueryQCResultForQCGraphics(param.StrmethodName, qCResForUIInfo);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QCResultForUIInfo>), results));
                        LogInfo.WriteProcessLog(results.Count.ToString(), Module.WindowsService);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 质控-质控任务数据处理
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCTask(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);


                switch (param.StrmethodName)
                {
                    case "QueryBigestQCTaskInfoForToday":
                        List<QCTaskInfo> lstQCTask = qcTask.QueryBigestQCTaskInfoForToday(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QCTaskInfo>), lstQCTask));
                        LogInfo.WriteProcessLog(lstQCTask.ToString(), Module.WindowsService);
                        break;
                    case "QueryQCAllInfoForUnLocked":
                        List<QualityControlInfo> lstQCInfos = qcTask.QueryQCAllInfoForUnLocked(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                        LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryAssayProNameAllInfo":
                        List<string> lstAllProjectName = qcTask.QueryAssayProNameAllInfo(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstAllProjectName));
                        LogInfo.WriteProcessLog(lstAllProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryProjectNameInfoByQC":
                        QualityControlInfo qcRelProInfo = (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), param.ObjParam);
                        string strSampleType = param.ObjLastestParam.ToString();
                        List<string[]> lstProjectName = qcTask.QueryProjectNameInfoByQC(param.StrmethodName, qcRelProInfo, strSampleType);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string[]>), lstProjectName));
                        LogInfo.WriteProcessLog(lstProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryCombProjectNameAllInfo":
                        List<string> lstCombProjectNames = qcTask.QueryCombProjectNameAllInfo(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstCombProjectNames));
                        LogInfo.WriteProcessLog(lstCombProjectNames.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryProjectByCombProName":
                        List<string> lstProNames = combProjectParam.QueryProjectByCombProName(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        break;
                    case "AddQCTask":
                        List<QCTaskInfo> lstQCTaskInfos = (List<QCTaskInfo>)XmlUtility.Deserialize(typeof(List<QCTaskInfo>), param.ObjParam);
                        string strAddResult = qcTask.AddQCTask(param.StrmethodName, lstQCTaskInfos);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, strAddResult);
                        LogInfo.WriteProcessLog(strAddResult, Module.WindowsService);
                        break;
                    case "QueryQCTaskForLstv":
                        List<QCTaskInfo> lstQCTaskResult = qcTask.QueryQCTaskForLstv(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<QCTaskInfo>), lstQCTaskResult));
                        LogInfo.WriteProcessLog(lstQCTaskResult.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryQCTaskBySampleNum":
                        QCTaskInfoQuery qCTaskInfoQuery = qcTask.QueryNextQCTaskBySampleNum(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(QCTaskInfoQuery), qCTaskInfoQuery));
                        //LogInfo.WriteProcessLog(lstQCTaskByNum.Count.ToString(), Module.WindowsService);
                        break;
                    //case "InitMachineUpdateQCTaskState":
                    //    qcTask.InitMachineUpdateQCTaskState(param.StrmethodName);
                    //    break;
                    default:
                        break;
                }
            }
        }

        private void HandleSystemMaintenance(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            lock (lockObj)
            {
                LogInfo.WriteProcessLog(param.StrmethodName, Module.WindowsService);


                switch (param.StrmethodName)
                {
                    case "QueryWaterBlankValueByWave":
                        List<CuvetteBlankInfo> lstCuvBlk = systemMaintenance.QueryWaterBlankValueByWave(param.StrmethodName, param.ObjParam);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<CuvetteBlankInfo>), lstCuvBlk));
                        break;
                    case "QueryNewPhotemetricValue":
                        List<List<OffSetGain>> lstNewPhotoGain = systemMaintenance.QueryNewPhotemetricValue(param.StrmethodName);
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<List<OffSetGain>>), lstNewPhotoGain));
                        break;
                    //case "QueryOldPhotemetricValue":
                    //    List<OffSetGain> lstOldPhotoGain = systemMaintenance.QueryOldPhotemetricValue(param.StrmethodName);
                    //    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<OffSetGain>), lstOldPhotoGain));
                    //    break;
                }
            }
        }

        private void HandleMainTain(ModuleInfo moduleInfo, ClientRegisterInfo client, CommunicationEntity param)
        {
            switch (param.StrmethodName)
            {
                case "GetAllTasksCount":
                    int task = mainTain.GetAllTasksCount(param.StrmethodName);
                    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, task);
                    break;
            }
        }
        
        /// 获取所有客户端名称        /// </summary>
        /// <returns>返回客户端名集合</returns>
        public List<string> GetClients()
        {
            return ClientInfoCache.Instance.Clients.Select(x => x.ClientName).ToList();
        }
    }
}
