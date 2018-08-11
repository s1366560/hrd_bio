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
        /// 此方法没用地方调用
        /// </summary>
        /// <param name="sendClientName"></param>
        /// <param name="param"></param>
        public void ClientSendMsgToService(ModuleInfo sendClientName, string param)
        {
            
        }

        /// <summary>
        /// 保存回调给客户端信息（方法名，参数）的泛型集合
        /// </summary>
        private Dictionary<string, object> strMethodParam = new Dictionary<string, object>();

        /// <summary>
        /// 工作区（普通任务界面数据处理）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleWorkingAreaApplyTasks(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                LogInfo.WriteProcessLog(kvp.ToString(), Module.WindowsService);
                string strResult = "";
                List<TaskInfo> lstTask = new List<TaskInfo>();
                switch (kvp.Key)
                {
                    case "QueryMaxSampleNum":
                        int intMaxNum = workAreaApplyTask.QueryMaxSampleNum(kvp.Key);
                        strMethodParam.Add(kvp.Key, intMaxNum);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intMaxNum);
                        break;
                    case "QuerySampleDiluteRatio":
                        List<string> lisQueryDilutionRatio = settingsDataConfig.QueryDilutionRatio(kvp.Key);
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisQueryDilutionRatio));
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisQueryDilutionRatio));
                        break;
                    case "QueryProNameForApplyTask":
                        List<string[]> lstProName = workAreaApplyTask.QueryProNameForApplyTask(kvp.Key, kvp.Value[0].ToString());
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string[]>), lstProName));
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string[]>), lstProName));
                        break;
                    case "QueryCombProjectNameAllInfo":
                        List<string> lstCombProName = workAreaApplyTask.QueryCombProjectNameAllInfo(kvp.Key);
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstCombProName));
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstCombProName));
                        break;
                    case "QueryApplyTaskLsvt":
                        List<SampleInfo> lstSampleInfo = workAreaApplyTask.QueryApplyTaskLsvt(kvp.Key);
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SampleInfo>), lstSampleInfo));
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<SampleInfo>), lstSampleInfo));
                        break;
                    case "AddTask":
                        SampleInfo sample = XmlUtility.Deserialize(typeof(SampleInfo), kvp.Value[0].ToString()) as SampleInfo;
                        lstTask = XmlUtility.Deserialize(typeof(List<TaskInfo>), kvp.Value[1].ToString()) as List<TaskInfo>;
                        strResult = workAreaApplyTask.AddTask(kvp.Key, sample, lstTask);
                        strMethodParam.Add(kvp.Key, strResult);
                        break;
                    case "QueryProjectByCombProName":
                        List<string> lstProNames = combProjectParam.QueryProjectByCombProName(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        break;
                    case "AddTaskForBatch":
                        //批量录入返回结果集合
                        List<string> lstReslut = new List<string>();
                        for (int i = 0; i < kvp.Value.Length; i++)
                        {
                            List<object> lstObj = XmlUtility.Deserialize(typeof(List<object>), kvp.Value[i].ToString()) as List<object>;
                            SampleInfo sampleForBatch = XmlUtility.Deserialize(typeof(SampleInfo), lstObj[0].ToString()) as SampleInfo;
                            lstTask = XmlUtility.Deserialize(typeof(List<TaskInfo>), lstObj[1].ToString()) as List<TaskInfo>;
                            strResult = workAreaApplyTask.AddTask(kvp.Key, sampleForBatch, lstTask);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, sampleForBatch.SampleNum.ToString() + "," + strResult);
                            lstReslut.Add(sampleForBatch.SampleNum.ToString() + "," + strResult);
                        }
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>),lstReslut));
                        break;
                    case "QueryTaskInfoBySampleNum":
                        lstTask = workAreaApplyTask.QueryTaskInfoBySampleNum(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<TaskInfo>), lstTask));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<TaskInfo>), lstTask));
                        break;
                    case "QueryPatientInfoBySampleNum":
                        PatientInfo patientInfo = workAreaApplyTask.QueryPatientInfoBySampleNum(kvp.Key, System.Convert.ToInt32(kvp.Value[0].ToString()));
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(PatientInfo), patientInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(PatientInfo), patientInfo));
                        break;
                    case "UpdatePatientInfo":
                        string strUpdateInfo = workAreaApplyTask.UpdatePatientInfo(kvp.Key, XmlUtility.Deserialize(typeof(PatientInfo), kvp.Value[0].ToString()) as PatientInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strUpdateInfo);
                        strMethodParam.Add(kvp.Key, strUpdateInfo);
                        break;
                    case "QueryApplyApartment":
                        List<string> applyApartment = workAreaApplyTask.QueryApplyApartment(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), applyApartment));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), applyApartment));
                        break;
                    case "QueryApplyDoctor":
                        List<string> applyDoctor = workAreaApplyTask.QueryApplyDoctor(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), applyDoctor));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), applyDoctor));
                        break;
                    case "QueryCheckDoctor":
                        List<string> checkDoctor = workAreaApplyTask.QueryCheckDoctor(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), checkDoctor));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), checkDoctor));
                        break;
                    case "QueryInspectDoctor":
                        List<string> inspectDoctor = workAreaApplyTask.QueryInspectDoctor(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), inspectDoctor));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), inspectDoctor));
                        break;
                    case "QueryPatientInfos":
                        List<PatientInfo> lstPatientInfo = workAreaApplyTask.QueryPatientInfos(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<PatientInfo>), lstPatientInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<PatientInfo>), lstPatientInfo));
                        break;
                    case "QueryTaskInfoForSamplePanel":
                        string[] paramInfo = XmlUtility.Deserialize(typeof(string[]), kvp.Value[0].ToString()) as string[];
                        TaskInfoForSamplePanelInfo taskInfoForSamPanel = workAreaApplyTask.QueryTaskInfoForSamplePanel(kvp.Key, paramInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(TaskInfoForSamplePanelInfo), taskInfoForSamPanel));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(TaskInfoForSamplePanelInfo), taskInfoForSamPanel));
                        break;
                    case "QuerySamplePanelState":
                        List<SampleInfo> lstSampleInfoForPanel = workAreaApplyTask.QuerySamplePanelState(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<SampleInfo>), lstSampleInfoForPanel));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SampleInfo>), lstSampleInfoForPanel));
                        break;
                    case "UpdateRunningTaskWorDisk":
                        workAreaApplyTask.UpdateRunningTaskWorDisk(kvp.Key, kvp.Value[0].ToString());
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 工作区（数据审核界面）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="communicationEntity"></param>
        private void HandleWorkingAreaDataChecks(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            lock (lockObj)
            {
                strMethodParam.Clear();
                foreach (KeyValuePair<string, object[]> kvp in param)
                {
                    LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                    List<SampleInfoForResult> lstSampleInfo = new List<SampleInfoForResult>();
                    List<SampleResultInfo> lstSampleResultInfo = new List<SampleResultInfo>();
                    SampleInfoForResult sampleInfo = new SampleInfoForResult();
                    switch (kvp.Key)
                    {
                        case "QueryCommonSampleData":
                            sampleInfo = (SampleInfoForResult)XmlUtility.Deserialize(typeof(SampleInfoForResult), kvp.Value[0].ToString());
                            string strFilter = kvp.Value[1].ToString();
                            lstSampleInfo = workAreaDataCheck.QueryCommonSampleData(kvp.Key, sampleInfo, strFilter);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<SampleInfoForResult>), lstSampleInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SampleInfoForResult>), lstSampleInfo));
                            break;
                        case "QueryProjectResultBySampleNum":
                            string[] QueryCommunicate = (string[])XmlUtility.Deserialize(typeof(string[]), kvp.Value[0].ToString());
                            lstSampleResultInfo = workAreaDataCheck.QueryProjectResultBySampleNum(kvp.Key, QueryCommunicate);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<SampleResultInfo>), lstSampleResultInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SampleResultInfo>), lstSampleResultInfo));
                            break;
                        case "QueryProjectResultForTestAudit":
                            string[] QueryCommuForTest = (string[])XmlUtility.Deserialize(typeof(string[]), kvp.Value[0].ToString());
                            lstSampleResultInfo = workAreaDataCheck.QueryProjectResultBySampleNum(kvp.Key, QueryCommuForTest);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<SampleResultInfo>), lstSampleResultInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SampleResultInfo>), lstSampleResultInfo));
                            break;
                        case "DeleteCommonSampleBySampleNum":
                            string[] DeleteCommunicate = (string[])XmlUtility.Deserialize(typeof(string[]), kvp.Value[0].ToString());
                            string strDeleteRes = workAreaDataCheck.DeleteCommonSampleBySampleNum(kvp.Key, DeleteCommunicate);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strDeleteRes);
                            strMethodParam.Add(kvp.Key, strDeleteRes);
                            break;
                        case "ReviewCheck":
                            string[] reviewCheckParam = (string[])XmlUtility.Deserialize(typeof(string[]), kvp.Value[0].ToString());
                            string strReviewCheckRes = workAreaDataCheck.ReviewCheck(kvp.Key, reviewCheckParam);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strReviewCheckRes);
                            strMethodParam.Add(kvp.Key, strReviewCheckRes);
                            break;
                        case "AuditSampleTest":
                            string[] auditParam = (string[])XmlUtility.Deserialize(typeof(string[]), kvp.Value[0].ToString());
                            string strAuditRes = workAreaDataCheck.AuditSampleTest(kvp.Key, auditParam);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strAuditRes);
                            strMethodParam.Add(kvp.Key, strAuditRes);
                            break;
                        case "QueryTimeCourse":
                            SampleResultInfo sampleResInfo = XmlUtility.Deserialize(typeof(SampleResultInfo), kvp.Value[0].ToString()) as SampleResultInfo;
                            TimeCourseInfo sampleReactionInfo = workAreaDataCheck.QueryCommonTaskReaction(kvp.Key, sampleResInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), sampleReactionInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), sampleReactionInfo));
                            break;
                        case "QueryCommonTaskReactionForAudit":
                            SampleResultInfo sampleResInfoForAudit = XmlUtility.Deserialize(typeof(SampleResultInfo), kvp.Value[0].ToString()) as SampleResultInfo;
                            TimeCourseInfo sampleReacInfoForAudit = workAreaDataCheck.QueryCommonTaskReaction("QueryTimeCourse", sampleResInfoForAudit);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), sampleReacInfoForAudit));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), sampleReacInfoForAudit));
                            break;
                        case "BatchAuditSampleTest":
                            List<string[]> lstBatchAuditParam = XmlUtility.Deserialize(typeof(List<string[]>), kvp.Value[0].ToString()) as List<string[]>;
                            string strBatchResult = workAreaDataCheck.BatchAuditSampleTest(kvp.Key, lstBatchAuditParam);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strBatchResult);
                            strMethodParam.Add(kvp.Key, strBatchResult);
                            break;
                        case "ConfirmCommonTask":
                            List<string[]> lstConfirmInfo = XmlUtility.Deserialize(typeof(List<string[]>), kvp.Value[0].ToString()) as List<string[]>;
                            string strConfirmInfo = workAreaDataCheck.ConfirmCommonTask(kvp.Key, lstConfirmInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strConfirmInfo);
                            strMethodParam.Add(kvp.Key, strConfirmInfo);
                            break;
                        default:
                            break;
                    }
                }
                client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
            }
        }
        /// <summary>
        /// 试剂（试剂状态）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="communicationEntity"></param>
        private void HandleReagentStates(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "QueryReagentState":
                        List<ReagentStateInfoR1R2> lisReagentStateInfo = reagentState.QueryReagentStateInfo(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), lisReagentStateInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), lisReagentStateInfo));
                        break;
                    case "LockReagentState":
                        List<ReagentStateInfoR1R2> ReagentStateInfo = new List<ReagentStateInfoR1R2>();
                        ReagentStateInfo = XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), kvp.Value[0].ToString()) as List<ReagentStateInfoR1R2>;
                        int UpdataReagentStateInfo = reagentState.UpdataReagentStateInfo(kvp.Key, ReagentStateInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key,UpdataReagentStateInfo.ToString());
                        strMethodParam.Add(kvp.Key, UpdataReagentStateInfo.ToString());
                        break;
                    case "UnlockReagentState":
                        List<ReagentStateInfoR1R2> UnlockReagentStateInfo = new List<ReagentStateInfoR1R2>();
                        ReagentStateInfo = XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), kvp.Value[0].ToString()) as List<ReagentStateInfoR1R2>;
                        int UpdataUnlockReagentStateInfo = reagentState.UpdataUnlockReagentState(kvp.Key, ReagentStateInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, UpdataUnlockReagentStateInfo.ToString());
                        strMethodParam.Add(kvp.Key, UpdataUnlockReagentStateInfo.ToString());
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 试剂（试剂设置）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="communicationEntity"></param>
        private void HandleReagentSettings(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "QueryReagentSetting1":
                        List<ReagentSettingsInfo> lisReagentSettingsInfo = reagentSetting.QueryReagentSettingsInfo(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<ReagentSettingsInfo>), lisReagentSettingsInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<ReagentSettingsInfo>), lisReagentSettingsInfo));
                        break;
                    case "QueryReagentSetting2":
                        List<ReagentSettingsInfo> lisReagentSettingsR2Info = reagentSetting.QueryReagentSettingsInfo2(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<ReagentSettingsInfo>), lisReagentSettingsR2Info));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<ReagentSettingsInfo>), lisReagentSettingsR2Info));
                        break;
                    case "reagentSettingAddR1":
                        ReagentSettingsInfo reagentSettingsInfo1 = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), kvp.Value[0].ToString()) as ReagentSettingsInfo;
                        string strInfo1 = reagentSetting.AddreagentSettingInfo(kvp.Key, reagentSettingsInfo1);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strInfo1);
                        strMethodParam.Add(kvp.Key, strInfo1);
                        break;
                    case "reagentSettingAddR2":
                        ReagentSettingsInfo reagentSettingsInfo2 = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), kvp.Value[0].ToString()) as ReagentSettingsInfo;
                        string strInfo2 = reagentSetting.AddreagentSettingInfo2(kvp.Key, reagentSettingsInfo2);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strInfo2);
                        strMethodParam.Add(kvp.Key, strInfo2);
                        break;
                    case "reagentStateAdd":
                        ReagentStateInfoR1R2 reagentStateInfoR1R2 = XmlUtility.Deserialize(typeof(ReagentStateInfoR1R2), kvp.Value[0].ToString()) as ReagentStateInfoR1R2;
                        string strInfoState = reagentSetting.AddreagentStateInfoR1R2(kvp.Key, reagentStateInfoR1R2);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strInfoState);
                        strMethodParam.Add(kvp.Key, strInfoState);
                        break;
                    case "DeleteReagentSettingsR1":
                        ReagentSettingsInfo DeletereagentSettingsInfo = new ReagentSettingsInfo();
                        DeletereagentSettingsInfo = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), kvp.Value[0].ToString()) as ReagentSettingsInfo;
                        int DeteleDoctorCount = reagentSetting.DeletereagentSettingsInfo(kvp.Key, DeletereagentSettingsInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, DeteleDoctorCount.ToString());
                        strMethodParam.Add(kvp.Key, DeteleDoctorCount.ToString());
                        break;
                    case "DeleteReagentSettingsR2":
                        ReagentSettingsInfo DeletereagentSettingsInfo2 = new ReagentSettingsInfo();
                        DeletereagentSettingsInfo2 = XmlUtility.Deserialize(typeof(ReagentSettingsInfo), kvp.Value[0].ToString()) as ReagentSettingsInfo;
                        int DeteleDoctorCount2 = reagentSetting.DeletereagentSettingsInfo2(kvp.Key, DeletereagentSettingsInfo2);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, DeteleDoctorCount2.ToString());
                        strMethodParam.Add(kvp.Key, DeteleDoctorCount2.ToString());
                        break;
                    case "QueryAssayProAllInfo":
                        LogInfo.WriteProcessLog("zhusizhe1", Module.WindowsService);
                        AssayProjectInfo assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                        List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(kvp.Key, assProInfo);
                        string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, str);
                        strMethodParam.Add(kvp.Key, str);
                        LogInfo.WriteProcessLog("zhusizhe2", Module.WindowsService);
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }

        /// <summary>
        /// 校准（校准任务界面数据处理）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleCalibControlTasks(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "QueryCalibratorinfoTask":
                        List<CalibratorinfoTask> CalibratorinfoTask = (List<CalibratorinfoTask>)XmlUtility.Deserialize(typeof(List<CalibratorinfoTask>), kvp.Value[0].ToString());
                        List<CalibratorinfoTask> lisCalibrationCurveInfo = calibrator.QueryListCalibrationCurveInfo(kvp.Key, CalibratorinfoTask);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lisCalibrationCurveInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lisCalibrationCurveInfo));
                        break;
                    case "QueryAssayProNameAllInfo":
                        List<string> lstAllProjectName = qcTask.QueryAssayProNameAllInfo(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstAllProjectName));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstAllProjectName));
                        LogInfo.WriteProcessLog(lstAllProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryProjectNameInfoByCalib":
                        List<string[]> lstProinfo = calibrator.QueryProjectNameInfoByCalib(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string[]>), lstProinfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string[]>), lstProinfo));
                        LogInfo.WriteProcessLog(lstProinfo.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryCombProjectNameAllInfo":
                        List<string> lstCombProName = workAreaApplyTask.QueryCombProjectNameAllInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstCombProName));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstCombProName));
                        break;
                    case "QueryProjectByCombProName":
                        List<string> lstProNames = combProjectParam.QueryProjectByCombProName(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        break;
                    case "QueryAssayProNameAll":
                        List<CalibratorinfoTask> lstAll = calibrator.QueryAssayProNameAll(kvp.Key, kvp.Value[0].ToString(), kvp.Value[1].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lstAll));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorinfoTask>), lstAll));
                        //LogInfo.WriteProcessLog(lstAllProjectName.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryBigestCalibCTaskInfoForToday":
                        int intSampleNum = calibrator.QueryBigestCalibCTaskInfoForToday(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intSampleNum);
                        strMethodParam.Add(kvp.Key, intSampleNum);
                        LogInfo.WriteProcessLog(intSampleNum.ToString(), Module.WindowsService);
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
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 校准（校准状态界面）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleCalibrationStates(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "QueryCalibrationState":
                        List<CalibrationResultinfo> lisCalibratorinfo = calibrator.QueryCalibrationState(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), lisCalibratorinfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), lisCalibratorinfo));
                        break;
                    case "QueryCalibrationResultinfo":
                        CalibrationResultinfo calibrationResultinfo = (CalibrationResultinfo)XmlUtility.Deserialize(typeof(CalibrationResultinfo), kvp.Value[0].ToString());
                        List<CalibrationResultinfo> Resultinfo = calibrator.QueryCalibrationResultinfo(kvp.Key, calibrationResultinfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), Resultinfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), Resultinfo));
                        break;
                    case "QueryCalibrationReactionProcess":
                        //CalibrationResultinfo calibrationReactionProcess = (CalibrationResultinfo)XmlUtility.Deserialize(typeof(CalibrationResultinfo), kvp.Value[0].ToString());
                        //List<CalibrationReactionProcess> calibrationReactionProcessResult = calibrator.QueryCalibrationReactionProcess(kvp.Key, calibrationReactionProcess);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationReactionProcess>), calibrationReactionProcessResult));
                        TimeCourseInfo calibrationResultinfoProcess = XmlUtility.Deserialize(typeof(TimeCourseInfo), kvp.Value[0].ToString()) as TimeCourseInfo;
                        TimeCourseInfo calibrationReactionProcessResult = calibrator.QueryCalibrationReactionProcess(kvp.Key, calibrationResultinfoProcess);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), calibrationReactionProcessResult));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), calibrationReactionProcessResult));
                        break;
                    case "QueryCalibrationResultInfoAndTimeCUVNO":
                        CalibrationResultinfo calibrationResultinfoAndTimeCUVNO = XmlUtility.Deserialize(typeof(CalibrationResultinfo), kvp.Value[0].ToString()) as CalibrationResultinfo;
                        List<CalibrationResultinfo> calibrationResultsAndTimeCUVNO = calibrator.QueryCalibrationResultInfoAndTimeCUVNO(kvp.Key, calibrationResultinfoAndTimeCUVNO);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), calibrationResultsAndTimeCUVNO));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationResultinfo>), calibrationResultsAndTimeCUVNO));
                        break;

                    case "QueryCalibrationCurveInfo":
                        CalibrationCurveInfo calibrationCurveInfo = (CalibrationCurveInfo)XmlUtility.Deserialize(typeof(CalibrationCurveInfo), kvp.Value[0].ToString());
                        List<SDTTableItem> lisCalibrationCurveInfo = calibrator.QueryCalibrationCurveInfo(kvp.Key, calibrationCurveInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<SDTTableItem>), lisCalibrationCurveInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SDTTableItem>), lisCalibrationCurveInfo));
                        break;
                    case "AddSDTTableItem":
                        SDTTableItem TableItem = XmlUtility.Deserialize(typeof(SDTTableItem), kvp.Value[0].ToString()) as SDTTableItem;
                        string strInfoState = calibrator.AddSDTTableItem(kvp.Key, TableItem);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strInfoState);
                        strMethodParam.Add(kvp.Key, strInfoState);
                        break;
                    case "QuerysDTTableItem":
                        List<SDTTableItem> lisSDTTableItem = calibrator.QuerysDTTableItem(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<SDTTableItem>), lisSDTTableItem));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SDTTableItem>), lisSDTTableItem));
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 校准（校准品维护界面）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleCalibrationMaintains(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "QueryCalibrationMaintain":
                        List<Calibratorinfo> lisCalibratorinfo = calibrator.QueryCalibratorinfo(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfo));
                        break;
                    case "QueryCalibratorProjectinfo":
                        List<CalibratorProjectinfo> lisCalibratorProjectinfo = calibrator.QueryCalibratorProjectinfo(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo));
                        break;
                    case "QueryAssayProAllInfo":
                        AssayProjectInfo assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                        List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(kvp.Key, assProInfo);
                        string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, str);
                        strMethodParam.Add(kvp.Key, str);
                        LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);
                        break;
                    case "QueryProjectItemsByCalibration":
                        List<CalibratorProjectinfo> lisCalibratorProjectinfo1 = calibrator.QueryProjectItemsByCalibration(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo1));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo1));
                        break;
                    case "QueryCalibPos":
                        List<Calibratorinfo> lisCalibratorinfoPos = calibrator.QueryCalibPos(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfoPos));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfoPos));
                        break;
                    case "AddCalibratorinfo":
                        Calibratorinfo calibratorinfo = XmlUtility.Deserialize(typeof(Calibratorinfo), kvp.Value[0].ToString()) as Calibratorinfo;
                        List<CalibratorProjectinfo> liscalibratorProjectinfo = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), kvp.Value[1].ToString()) as List<CalibratorProjectinfo>;
                        string strInfoState = calibrator.AddCalibratorinfo(kvp.Key, calibratorinfo, liscalibratorProjectinfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strInfoState);
                        strMethodParam.Add(kvp.Key, strInfoState);
                        break;
                    case "DeleteCalibrationMaintain":
                        LogInfo.WriteProcessLog(kvp.Key + "zhuszihe11", Module.WindowsService);
                        string DeteleCount = calibrator.DeleteCalibrationMaintain(kvp.Key, kvp.Value[0].ToString());
                        LogInfo.WriteProcessLog(kvp.Key + "zhuszihe22" + DeteleCount.ToString(), Module.WindowsService);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, DeteleCount);
                        strMethodParam.Add(kvp.Key, DeteleCount);
                        LogInfo.WriteProcessLog(kvp.Key + "zhuszihe33", Module.WindowsService);
                        break;
                    case "EditCalibratorinfo":
                        Calibratorinfo Editcalibratorinfo = XmlUtility.Deserialize(typeof(Calibratorinfo), kvp.Value[0].ToString()) as Calibratorinfo;
                        List<CalibratorProjectinfo> lisEditCalibratorProjectinfo = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), kvp.Value[2].ToString()) as List<CalibratorProjectinfo>;
                        string ModifyCalibAndProInfo = calibrator.EditCalibratorinfo(kvp.Key, Editcalibratorinfo, kvp.Value[1].ToString(), lisEditCalibratorProjectinfo);
                        LogInfo.WriteProcessLog(kvp.Key + "zhuszihe22", Module.WindowsService);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, ModifyCalibAndProInfo);
                        strMethodParam.Add(kvp.Key, ModifyCalibAndProInfo);
                        LogInfo.WriteProcessLog(kvp.Key + "zhuszihe33", Module.WindowsService);
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 质控（质控状态界面）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCResults(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            lock (lockObj)
            {
                foreach (KeyValuePair<string, object[]> kvp in param)
                {
                    LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                    QCResultForUIInfo qCResForUI = new QCResultForUIInfo();
                    switch (kvp.Key)
                    {
                        case "QueryQCResultInfo":
                            qCResForUI = XmlUtility.Deserialize(typeof(QCResultForUIInfo), kvp.Value[0].ToString()) as QCResultForUIInfo;
                            List<QCResultForUIInfo> lstqCResForUIInfos = qcResult.QueryQCResultInfo(kvp.Key, qCResForUI);
                            string str = XmlUtility.Serializer(typeof(List<QCResultForUIInfo>), lstqCResForUIInfos);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, str);
                            strMethodParam.Add(kvp.Key, str);
                            LogInfo.WriteProcessLog(lstqCResForUIInfos.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryQCInfosForAddQCResult":
                            QualityControlInfo qcInfo = new QualityControlInfo();
                            List<QualityControlInfo> lstQCInfos = qcResult.QueryQCInfosForAddQCResult(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryProjectName":
                            List<string> lstProjectName = new List<string>();
                            lstProjectName = qcResult.QueryProjectName(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProjectName));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProjectName));
                            LogInfo.WriteProcessLog(lstProjectName.Count.ToString(), Module.WindowsService);
                            break;
                        case "EditQCResultForManual":
                            qCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), kvp.Value[0].ToString());
                            QCResultForUIInfo editQCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), kvp.Value[1].ToString());
                            string EditResult = qcResult.EditQCResultForManual(kvp.Key, qCResForUI, editQCResForUI);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, EditResult);
                            strMethodParam.Add(kvp.Key, EditResult);
                            LogInfo.WriteProcessLog(EditResult, Module.WindowsService);
                            break;
                        case "AddQCResultForManual":
                            QCResultForUIInfo AddQCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), kvp.Value[0].ToString());
                            string AddResult = qcResult.AddQCResultForManual(kvp.Key, AddQCResForUI);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, AddResult);
                            strMethodParam.Add(kvp.Key, AddResult);
                            LogInfo.WriteProcessLog(AddResult, Module.WindowsService);
                            break;
                        case "DeleteQCResult":
                            qCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), kvp.Value[0].ToString());
                            string strDeleteRes = qcResult.DeleteQCResult(kvp.Key, qCResForUI);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strDeleteRes);
                            strMethodParam.Add(kvp.Key, strDeleteRes);
                            LogInfo.WriteProcessLog(strDeleteRes, Module.WindowsService);
                            break;
                        case "QueryTimeCourseByQCInfo":
                            qCResForUI = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), kvp.Value[0].ToString());
                            TimeCourseInfo qCTimeCourseInfo = qcResult.QueryTimeCourseByQCInfo(kvp.Key, qCResForUI);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), qCTimeCourseInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(TimeCourseInfo), qCTimeCourseInfo));
                            break;
                        default:
                            break;
                    }
                }
                client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
            }
        }

        /// <summary>
        /// 质控（质控任务界面）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCTasks(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            //lock (lockObj)
            //{
                foreach (KeyValuePair<string, object[]> kvp in param)
                {
                    LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                    switch (kvp.Key)
                    {
                        case "QueryBigestQCTaskInfoForToday":
                            List<QCTaskInfo> lstQCTask = qcTask.QueryBigestQCTaskInfoForToday(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QCTaskInfo>), lstQCTask));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QCTaskInfo>), lstQCTask));
                            LogInfo.WriteProcessLog(lstQCTask.ToString(), Module.WindowsService);
                            break;
                        case "QueryQCAllInfoForUnLocked":
                            List<QualityControlInfo> lstQCInfos = qcTask.QueryQCAllInfoForUnLocked(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryAssayProNameAllInfo":
                            List<string> lstAllProjectName = qcTask.QueryAssayProNameAllInfo(kvp.Key, kvp.Value[0].ToString());
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstAllProjectName));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstAllProjectName));
                            LogInfo.WriteProcessLog(lstAllProjectName.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryProjectNameInfoByQC":
                            QualityControlInfo qcRelProInfo = (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString());
                            string strSampleType = kvp.Value[1].ToString();
                            List<string[]> lstProjectName = qcTask.QueryProjectNameInfoByQC(kvp.Key, qcRelProInfo, strSampleType);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string[]>), lstProjectName));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string[]>), lstProjectName));
                            LogInfo.WriteProcessLog(lstProjectName.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryCombProjectNameAllInfo":
                            List<string> lstCombProjectNames = qcTask.QueryCombProjectNameAllInfo(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstCombProjectNames));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstCombProjectNames));
                            LogInfo.WriteProcessLog(lstCombProjectNames.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryProjectByCombProName":
                            List<string> lstProNames = combProjectParam.QueryProjectByCombProName(kvp.Key, kvp.Value[0].ToString());
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                            break;
                        case "AddQCTask":
                            List<QCTaskInfo> lstQCTaskInfos = (List<QCTaskInfo>)XmlUtility.Deserialize(typeof(List<QCTaskInfo>), kvp.Value[0].ToString());
                            string strAddResult = qcTask.AddQCTask(kvp.Key, lstQCTaskInfos);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strAddResult);
                            strMethodParam.Add(kvp.Key, strAddResult);
                            LogInfo.WriteProcessLog(strAddResult, Module.WindowsService);
                            break;
                        case "QueryQCTaskForLstv":
                            List<QCTaskInfo> lstQCTaskResult = qcTask.QueryQCTaskForLstv(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QCTaskInfo>), lstQCTaskResult));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QCTaskInfo>), lstQCTaskResult));
                            LogInfo.WriteProcessLog(lstQCTaskResult.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryQCTaskBySampleNum":
                            QCTaskInfoQuery qCTaskInfoQuery = qcTask.QueryNextQCTaskBySampleNum(kvp.Key, kvp.Value[0].ToString());
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(QCTaskInfoQuery), qCTaskInfoQuery));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(QCTaskInfoQuery), qCTaskInfoQuery));
                            //LogInfo.WriteProcessLog(lstQCTaskByNum.Count.ToString(), Module.WindowsService);
                            break;
                        //case "InitMachineUpdateQCTaskState":
                        //    qcTask.InitMachineUpdateQCTaskState(param.StrmethodName);
                        //    break;
                        default:
                            break;
                    }
                }
                client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
            //}
        }
        /// <summary>
        /// 质控（质控品维护界面）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCMaintains(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            lock (lockObj)
            {
                foreach(KeyValuePair<string,object[]> kvp in param)
                {
                    LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);

                    AssayProjectInfo assProInfo = new AssayProjectInfo();
                    QualityControlInfo qcInfo = new QualityControlInfo();
                    switch (kvp.Key)
                    {
                        case "QueryAssayProAllInfo":
                            assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            List<AssayProjectInfo> lstAssayProInfos = qcMaintian.QueryAssayProAllInfo(kvp.Key, assProInfo);
                            string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, str);
                            strMethodParam.Add(kvp.Key, str);
                            LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryQCPosition":
                            List<string> qcPositons = qcMaintian.QueryQCPosition(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), qcPositons));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), qcPositons));
                            LogInfo.WriteProcessLog(qcPositons.Count.ToString(), Module.WindowsService);
                            break;
                        case "AddQualityControl":
                            qcInfo = XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString()) as QualityControlInfo;
                            List<QCRelationProjectInfo> lstQCRelationProInfo = XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), kvp.Value[1].ToString()) as List<QCRelationProjectInfo>;
                            string strResult = qcMaintian.AddQualityControl("AddQualityControl", qcInfo, lstQCRelationProInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strResult);
                            strMethodParam.Add(kvp.Key, strResult);
                            LogInfo.WriteProcessLog(strResult, Module.WindowsService);
                            break;
                        case "QueryQCAllInfo":
                            List<QualityControlInfo> lstQCInfos = qcMaintian.QueryQCAllInfo(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryRelativelyProjectByQCInfo":
                            //qcInfo = XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString()) as QualityControlInfo;
                            List<QCRelationProjectInfo> lstQCRelationPros = qcMaintian.QueryRelativelyProjectByQCInfo(kvp.Key, null);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QCRelationProjectInfo>), lstQCRelationPros));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QCRelationProjectInfo>), lstQCRelationPros));
                            LogInfo.WriteProcessLog(lstQCRelationPros.Count.ToString(), Module.WindowsService);
                            break;
                        //case "EditQCRelateProInfo":
                        //    int iEditQCProResult = qcMaintian.EditQCRelateProInfo(kvp.Key, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString()), (List<QCRelationProjectInfo>)XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), kvp.Value[1].ToString()));
                        //    //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, iEditQCProResult);
                        //    strMethodParam.Add(kvp.Key, iEditQCProResult);
                        //    LogInfo.WriteProcessLog(iEditQCProResult.ToString(), Module.WindowsService);
                        //    break;
                        case "EditQualityControl":
                            string strQCRes = qcMaintian.EditQualityControl(kvp.Key, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString()), (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[1].ToString()), (List<QCRelationProjectInfo>)XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), kvp.Value[2].ToString()));
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strQCRes);
                            strMethodParam.Add(kvp.Key, strQCRes);
                            LogInfo.WriteProcessLog(strQCRes.ToString(), Module.WindowsService);
                            break;
                        case "LockQualityControl":
                            int intLockResult = qcMaintian.LockQualityControl(kvp.Key, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString()));
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intLockResult);
                            strMethodParam.Add(kvp.Key, intLockResult);
                            LogInfo.WriteProcessLog(intLockResult.ToString(), Module.WindowsService);
                            break;
                        case "UnLockQualityControl":
                            int intUnLockResult = qcMaintian.UnLockQualityControl(kvp.Key, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString()));
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intUnLockResult);
                            strMethodParam.Add(kvp.Key, intUnLockResult);
                            LogInfo.WriteProcessLog(intUnLockResult.ToString(), Module.WindowsService);
                            break;
                        case "DeleteQualityControl":
                            string strDeleteResult = qcMaintian.DeleteQualityControl(kvp.Key, (QualityControlInfo)XmlUtility.Deserialize(typeof(QualityControlInfo), kvp.Value[0].ToString()));
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strDeleteResult);
                            strMethodParam.Add(kvp.Key, strDeleteResult);
                            LogInfo.WriteProcessLog(strDeleteResult, Module.WindowsService);
                            break;
                        default:
                            break;
                    }
                }
                client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
            }
        }
        /// <summary>
        /// 质控（质控数据图处理）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleQCGraphics(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            lock (lockObj)
            {
                foreach (KeyValuePair<string, object[]> kvp in param)
                {
                    LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                    switch (kvp.Key)
                    {
                        case "QueryProjectName":
                            List<string> lstProjectName = new List<string>();
                            lstProjectName = qcGraphics.QueryProjectName(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProjectName));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProjectName));
                            LogInfo.WriteProcessLog(lstProjectName.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryQCAllInfo":
                            List<QualityControlInfo> lstQCInfos = qcGraphics.QueryQCAllInfo(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QualityControlInfo>), lstQCInfos));
                            LogInfo.WriteProcessLog(lstQCInfos.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryQCResultForQCGraphics":
                            QCResultForUIInfo qCResForUIInfo = (QCResultForUIInfo)XmlUtility.Deserialize(typeof(QCResultForUIInfo), kvp.Value[0].ToString());
                            List<QCResultForUIInfo> results = qcGraphics.QueryQCResultForQCGraphics(kvp.Key, qCResForUIInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<QCResultForUIInfo>), results));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<QCResultForUIInfo>), results));
                            LogInfo.WriteProcessLog(results.Count.ToString(), Module.WindowsService);
                            break;
                        default:
                            break;
                    }
                }
                client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
            }
        }
        /// <summary>
        /// 系统设置（生化参数）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSettingsChemicalParams(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            lock (lockObj)
            {
                foreach (KeyValuePair<string, object[]> kvp in param)
                {
                    LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                    AssayProjectInfo assProInfo = new AssayProjectInfo();
                    switch (kvp.Key)
                    {
                        case "QueryAssayProAllInfo":
                            assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(kvp.Key, assProInfo);
                            //string str = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, str);
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos));
                            LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);
                            break;
                        case "AssayProjectAdd":
                            assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            string[] strInfo = settingsChemicalParam.AddAssayProject(kvp.Key, assProInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string[]), strInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(string[]), strInfo));
                            LogInfo.WriteProcessLog(strInfo[4], Module.WindowsService);
                            break;
                        case "QueryAssayProjectParamInfoAll":
                            //assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            List<AssayProjectParamInfo> assayProParamInfo = settingsChemicalParam.QueryAssayProjectParamInfoAll(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(AssayProjectParamInfo), assayProParamInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectParamInfo>), assayProParamInfo));
                            //LogInfo.WriteProcessLog(assayProParamInfo.ProjectName, Module.WindowsService);
                            break;
                        case "QueryProjectResultUnits":
                            List<string> lstUnits = settingsChemicalParam.QueryProjectResultUnits(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstUnits));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstUnits));
                            break;
                        case "UpdateAssayProjectParamInfo":
                            AssayProjectParamInfo assProParamInfo = XmlUtility.Deserialize(typeof(AssayProjectParamInfo), kvp.Value[0].ToString()) as AssayProjectParamInfo;
                            int intResult = settingsChemicalParam.UpdateAssayProjectParamInfo(kvp.Key, assProParamInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intResult);
                            strMethodParam.Add(kvp.Key, intResult);
                            break;
                        case "AssayProjectEdit":
                            AssayProjectInfo assayProInfoOld = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            AssayProjectInfo assayProInfoLast = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[1].ToString()) as AssayProjectInfo;
                            LogInfo.WriteProcessLog(kvp.Value[0].ToString(), Module.WindowsService);
                            LogInfo.WriteProcessLog(kvp.Value[1].ToString(), Module.WindowsService);
                            int intEditResult = settingsChemicalParam.EditAssayProject(kvp.Key, assayProInfoOld, assayProInfoLast);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intEditResult);
                            strMethodParam.Add(kvp.Key, intEditResult);
                            break;
                        case "AssayProjectDelete":
                            List<AssayProjectInfo> lstProInfos = XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), kvp.Value[0].ToString()) as List<AssayProjectInfo>;
                            int intDeleteCount = settingsChemicalParam.AssayProjectDelete(kvp.Key, lstProInfos);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intDeleteCount);
                            strMethodParam.Add(kvp.Key, intDeleteCount);
                            break;
                        //case "ProjectPageinfo":         // 获取生化项目信息
                        //    assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                        //    List<AssayProjectInfo> assayProInfos = settingsChemicalParam.QueryAssayProAllInfoByDistinctProName(kvp.Key, assProInfo);
                        //    client.NotifyCallBack.DatabaseNotifyFunction(kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), assayProInfos));
                        //    break;
                        case "QueryAssayProAllInfoForCalibParam": // 校准参数界面获取项目信息
                            assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            List<AssayProjectInfo> lstAssayProInfosForCalibparam = settingsChemicalParam.QueryAssayProAllInfo("QueryAssayProAllInfo", assProInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfosForCalibparam));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfosForCalibparam));
                            LogInfo.WriteProcessLog(lstAssayProInfosForCalibparam.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryCalibParamInfoAll": // 通过项目名称和项目类型获取项目校准参数
                            //assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            List<AssayProjectCalibrationParamInfo> calibParam = settingsChemicalParam.QueryCalibParamInfoAll(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(AssayProjectCalibrationParamInfo), calibParam));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectCalibrationParamInfo>), calibParam));
                            break;
                        case "UpdateCalibParamByProNameAndType":    // 通过项目名称和类型更新校准参数
                            AssayProjectCalibrationParamInfo sender = XmlUtility.Deserialize(typeof(AssayProjectCalibrationParamInfo), kvp.Value[0].ToString()) as AssayProjectCalibrationParamInfo;
                            int intCalibResult = settingsChemicalParam.UpdateCalibParamByProNameAndType(kvp.Key, sender);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intCalibResult);
                            strMethodParam.Add(kvp.Key, intCalibResult);
                            break;
                        case "QueryRangeParamByProNameAndType": // 通过项目名称和项目类型获取项目范围参数
                            assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            AssayProjectRangeParamInfo rangeParam = settingsChemicalParam.QueryRangeParamByProNameAndType(kvp.Key, assProInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(AssayProjectRangeParamInfo), rangeParam));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(AssayProjectRangeParamInfo), rangeParam));
                            break;
                        case "UpdateRangeParamByProNameAndType":
                            AssayProjectRangeParamInfo rangeSender = XmlUtility.Deserialize(typeof(AssayProjectRangeParamInfo), kvp.Value[0].ToString()) as AssayProjectRangeParamInfo;
                            int intRangeResult = settingsChemicalParam.UpdateRangeParamByProNameAndType(kvp.Key, rangeSender);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intRangeResult);
                            strMethodParam.Add(kvp.Key, intRangeResult);
                            break;
                        case "QueryAssayProAllInfoForRangeParam": // 范围参数界面获取项目信息
                            assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                            List<AssayProjectInfo> lstAssayProInfosForRangeparam = settingsChemicalParam.QueryAssayProAllInfo("QueryAssayProAllInfo", assProInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfosForRangeparam));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfosForRangeparam));
                            LogInfo.WriteProcessLog(lstAssayProInfosForRangeparam.Count.ToString(), Module.WindowsService);
                            break;
                        case "QueryCalibratorProinfo":
                            List<CalibratorProjectinfo> lisCalibratorProjectinfo = settingsChemicalParam.QueryCalibratorProjectinfo(kvp.Key, kvp.Value[0].ToString());
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lisCalibratorProjectinfo));
                            break;
                        case "QueryCalib":
                            List<Calibratorinfo> lisCalibratorinfo = settingsChemicalParam.QueryCalib(kvp.Key, kvp.Value[0].ToString());
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<Calibratorinfo>), lisCalibratorinfo));
                            break;
                        case "AddCalibrationCurveInfo":
                            List<CalibrationCurveInfo> calibrationCurveInfo = XmlUtility.Deserialize(typeof(List<CalibrationCurveInfo>), kvp.Value[0].ToString()) as List<CalibrationCurveInfo>;
                            string strCurveInfo = settingsChemicalParam.AddCalibrationCurveInfo(kvp.Key, calibrationCurveInfo);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), strCurveInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(string), strCurveInfo));
                            break;
                        case "QueryCalibrationCurve":
                            List<CalibrationCurveInfo> lisCalibrationCurveInfo = settingsChemicalParam.QueryCalibrationCurveInfo(kvp.Key, kvp.Value[0].ToString());
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationCurveInfo>), lisCalibrationCurveInfo));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalibrationCurveInfo>), lisCalibrationCurveInfo));
                            break;

                        default:
                            break;
                    }
                }
                client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
            }
        }
        /// <summary>
        /// 系统设置（组合项目处理）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSettingsCombProjectInfos(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);

                switch (kvp.Key)
                {
                    case "QueryCombProjectNameAllInfo":
                        List<CombProjectInfo> lstCombProInfos = combProjectParam.QueryCombProjectNameAllInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CombProjectInfo>), lstCombProInfos));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CombProjectInfo>), lstCombProInfos));
                        break;
                    case "QueryProjectAndCombProName":
                        List<CombProjectInfo> lstProNames = combProjectParam.QueryProjectAndCombProName(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstProNames));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CombProjectInfo>), lstProNames));
                        break;
                    case "AddCombProjectName":
                        CombProjectInfo combProInfo = (CombProjectInfo)XmlUtility.Deserialize(typeof(CombProjectInfo), kvp.Value[0].ToString());
                        int strResult = combProjectParam.AddCombProjectName(kvp.Key, combProInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strResult);
                        strMethodParam.Add(kvp.Key, strResult);
                        break;
                    case "DeleteCombProjectName":
                        List<CombProjectInfo> lstInfos = (List<CombProjectInfo>)XmlUtility.Deserialize(typeof(List<CombProjectInfo>), kvp.Value[0].ToString());
                        int intResult = combProjectParam.DeleteCombProjectName(kvp.Key, lstInfos);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intResult);
                        strMethodParam.Add(kvp.Key, intResult);
                        break;
                    case "UpdateCombProjectName":
                        //CombProjectInfo combProInfoOld = (CombProjectInfo)XmlUtility.Deserialize(typeof(CombProjectInfo), kvp.Value[0].ToString());
                        CombProjectInfo combProInfoNew = (CombProjectInfo)XmlUtility.Deserialize(typeof(CombProjectInfo), kvp.Value[1].ToString());
                        int intUpdateResult = combProjectParam.UpdateCombProjectName(kvp.Key, kvp.Value[0].ToString(), combProInfoNew);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intUpdateResult);
                        strMethodParam.Add(kvp.Key, intUpdateResult);
                        break;
                    case "ProjectPageinfo":     // 获取生化项目信息
                        //AssayProjectInfo assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                        List<string> assayProInfos = settingsChemicalParam.QueryAssayProAllInfoByDistinctProName("QueryAssayProAllInfoByDistinctProName");
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), assayProInfos));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), assayProInfos));
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 系统设置（计算项目）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSettingsCalcProjectInfos(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                switch (kvp.Key)
                {
                    case "QueryCalcProjectAllInfo":
                        List<CalcProjectInfo> lstCalcProInfos = calcProjectParam.QueryCalcProjectAllInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CalcProjectInfo>), lstCalcProInfos));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CalcProjectInfo>), lstCalcProInfos));
                        break;
                    case "AddCalcProject":
                        CalcProjectInfo calcProInfo = (CalcProjectInfo)XmlUtility.Deserialize(typeof(CalcProjectInfo), kvp.Value[0].ToString());
                        string strResult = calcProjectParam.AddCalcProject(kvp.Key, calcProInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strResult);
                        strMethodParam.Add(kvp.Key, strResult);
                        break;
                    case "DeleteCalcProject":
                        List<CalcProjectInfo> lstInfos = (List<CalcProjectInfo>)XmlUtility.Deserialize(typeof(List<CalcProjectInfo>), kvp.Value[0].ToString());
                        int intResult = calcProjectParam.DeleteCalcProject(kvp.Key, lstInfos);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intResult);
                        strMethodParam.Add(kvp.Key, intResult);
                        break;
                    case "UpdateCalcProject":
                        CalcProjectInfo calcProInfoOld = (CalcProjectInfo)XmlUtility.Deserialize(typeof(CalcProjectInfo), kvp.Value[0].ToString());
                        CalcProjectInfo calcProInfoNew = (CalcProjectInfo)XmlUtility.Deserialize(typeof(CalcProjectInfo), kvp.Value[1].ToString());
                        int intUpdateResult = calcProjectParam.UpdateCalcProject(kvp.Key, calcProInfoOld, calcProInfoNew);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intUpdateResult);
                        strMethodParam.Add(kvp.Key, intUpdateResult);
                        break;
                    case "QueryProjectResultUnits":
                        List<string> lstUnits = settingsChemicalParam.QueryProjectResultUnits(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstUnits));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstUnits));
                        break;
                    case "ProjectPageinfoForCalc":         // 获取生化项目信息
                        List<string> assayProInfos = calcProjectParam.ProjectPageinfoForCalc(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), assayProInfos));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), assayProInfos));
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 系统设置（环境参数）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleEnvironmentParamInfso(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                switch (kvp.Key)
                {
                    case "UpdateEnvironmentParamInfo":
                        EnvironmentParamInfo environment = XmlUtility.Deserialize(typeof(EnvironmentParamInfo),kvp.Value[0].ToString()) as EnvironmentParamInfo;
                        RunningStateInfo running = XmlUtility.Deserialize(typeof(RunningStateInfo), kvp.Value[1].ToString()) as RunningStateInfo;
                        int intResult = environmentParam.UpdateEnvironmentParamInfo(kvp.Key, environment, running);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intResult);
                        strMethodParam.Add(kvp.Key, intResult);
                        break;
                    case "QueryEnvironmentParamInfo":
                        List<EnvironmentParamInfo> lstEnvironmentParamInfo = environmentParam.QueryEnvironmentParamInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<EnvironmentParamInfo>), lstEnvironmentParamInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<EnvironmentParamInfo>), lstEnvironmentParamInfo));
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo,strMethodParam);
        }

        /// <summary>
        /// 系统设置（交叉污染）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSettingsCrossPollutions(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo = new ReagentNeedleAntifoulingStrategyInfo();
                AssayProjectInfo assProInfo = new AssayProjectInfo();
                switch (kvp.Key)
                {
                    case "AddReagentNeedle":
                        reagentNeedleAntifoulingStrategyInfo = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), kvp.Value[0].ToString()) as ReagentNeedleAntifoulingStrategyInfo;
                        string strInfo = settingsReagentNeedle.AddsettingsReagentNeedle(kvp.Key, reagentNeedleAntifoulingStrategyInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strInfo);
                        strMethodParam.Add(kvp.Key, strInfo);
                        break;
                    case "QueryReagentNeedle":
                        List<ReagentNeedleAntifoulingStrategyInfo> lisQueryReagentNeedle = settingsReagentNeedle.QueryReagentNeedle(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<ReagentNeedleAntifoulingStrategyInfo>), lisQueryReagentNeedle));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<ReagentNeedleAntifoulingStrategyInfo>), lisQueryReagentNeedle));
                        break;
                    case "DeleteReagentNeedle":
                        reagentNeedleAntifoulingStrategyInfo = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), kvp.Value[0].ToString()) as ReagentNeedleAntifoulingStrategyInfo;
                        int intDeleteCount = settingsReagentNeedle.ReagentNeedleDelete(kvp.Key, reagentNeedleAntifoulingStrategyInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intDeleteCount);
                        strMethodParam.Add(kvp.Key, intDeleteCount);
                        break;
                    case "UpdataReagentNeedle":
                        ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfoOld = new ReagentNeedleAntifoulingStrategyInfo();
                        reagentNeedleAntifoulingStrategyInfo = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), kvp.Value[0].ToString()) as ReagentNeedleAntifoulingStrategyInfo;
                        reagentNeedleAntifoulingStrategyInfoOld = XmlUtility.Deserialize(typeof(ReagentNeedleAntifoulingStrategyInfo), kvp.Value[1].ToString()) as ReagentNeedleAntifoulingStrategyInfo;
                        string strUpDateResult = settingsReagentNeedle.ReagentNeedleUpDate(kvp.Key, reagentNeedleAntifoulingStrategyInfo, reagentNeedleAntifoulingStrategyInfoOld);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strUpDateResult);
                        strMethodParam.Add(kvp.Key, strUpDateResult);
                        break;
                    case "QueryAssayProAllInfo":
                        assProInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), kvp.Value[0].ToString()) as AssayProjectInfo;
                        List<AssayProjectInfo> lstAssayProInfos = settingsChemicalParam.QueryAssayProAllInfo(kvp.Key, assProInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfos));
                        break;
                    case "QueryWashingLiquid":
                        List<string> lstWashingLiquid = settingsReagentNeedle.QueryWashingLiquid(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstWashingLiquid));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstWashingLiquid));
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 系统设置（数据配置）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSettingsDataConfigs(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "QueryDataConfig":
                        List<string> lisQueryDataConfig = settingsDataConfig.QueryDataConfig(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisQueryDataConfig));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisQueryDataConfig));
                        break;
                    case "DataConfigAdd":
                        string strInfo = settingsDataConfig.DataConfigAdd(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), strInfo));
                        strMethodParam.Add(kvp.Key, strInfo);
                        break;
                    case "UpdataDataConfig":
                        int UpdataCount = settingsDataConfig.UpdataDataConfig(kvp.Key, kvp.Value[0].ToString(), kvp.Value[1].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), UpdataCount.ToString()));
                        strMethodParam.Add(kvp.Key, UpdataCount);
                        break;
                    case "DeleteDataConfig":
                        int DeteleCount = settingsDataConfig.DeleteDataConfig(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), DeteleCount.ToString()));
                        strMethodParam.Add(kvp.Key, DeteleCount);
                        break;
                    case "QueryDilutionRatio":
                        List<string> lisQueryDilutionRatio = settingsDataConfig.QueryDilutionRatio(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisQueryDilutionRatio));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisQueryDilutionRatio));
                        break;

                    case "DilutionRatioAdd":
                        string DilutionRatioInfo = settingsDataConfig.DilutionRatioAdd(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), DilutionRatioInfo));
                        strMethodParam.Add(kvp.Key, DilutionRatioInfo);
                        break;

                    case "UpdataDilutionRatio":
                        int UpdataDilutionRatioCount = settingsDataConfig.UpdataDilutionRatio(kvp.Key, kvp.Value[0].ToString(), kvp.Value[1].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), UpdataDilutionRatioCount.ToString()));
                        strMethodParam.Add(kvp.Key, UpdataDilutionRatioCount);
                        break;

                    case "DeleteDilutionRatio":
                        int DeteleDilutionRatioCount = settingsDataConfig.DeleteDilutionRatio(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), DeteleDilutionRatioCount.ToString()));
                        strMethodParam.Add(kvp.Key, DeteleDilutionRatioCount);
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 系统设置（lis配置）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSettingsLISCommunicates(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                LISCommunicateNetworkInfo lISCommunicateInfo = new LISCommunicateNetworkInfo();
                SerialCommunicationInfo serialCommunicationInfo = new SerialCommunicationInfo();
                switch (kvp.Key)
                {
                    case "NetworkLISCommunicate":
                        List<LISCommunicateNetworkInfo> lstLISCommunicateInfo = settingsChemicalParam.QueryLISCommunicateInfo(kvp.Key);
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<LISCommunicateNetworkInfo>), lstLISCommunicateInfo));
                        break;
                    case "NetworkLISCommunicateUpDate":
                        lISCommunicateInfo = XmlUtility.Deserialize(typeof(LISCommunicateNetworkInfo), kvp.Value[0].ToString()) as LISCommunicateNetworkInfo;
                        int intNetworkUpDateResult = settingsChemicalParam.NetworkUpDate(kvp.Key, lISCommunicateInfo);
                        strMethodParam.Add(kvp.Key, intNetworkUpDateResult);
                        break;
                    case "SerialLISCommunicate":
                        List<SerialCommunicationInfo> lstSerialCommunicationInfo = settingsChemicalParam.QuerySerialCommunicationInfo(kvp.Key);
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<SerialCommunicationInfo>), lstSerialCommunicationInfo));
                        break;
                    case "SerialLISCommunicateUpDate":
                        serialCommunicationInfo = XmlUtility.Deserialize(typeof(SerialCommunicationInfo), kvp.Value[0].ToString()) as SerialCommunicationInfo;
                        int intSerialUpDateResult = settingsChemicalParam.SerialUpDate(kvp.Key, serialCommunicationInfo);
                        strMethodParam.Add(kvp.Key, intSerialUpDateResult);
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }

        /// <summary>
        /// 安全管理（水空白检测）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSystemMaintenances(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            lock (lockObj)
            {
                foreach (KeyValuePair<string, object[]> kvp in param)
                {
                    LogInfo.WriteProcessLog(kvp.Key, Module.WindowsService);
                    switch (kvp.Key)
                    {
                        case "QueryWaterBlankValueByWave":
                            List<CuvetteBlankInfo> lstCuvBlk = systemMaintenance.QueryWaterBlankValueByWave(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<CuvetteBlankInfo>), lstCuvBlk));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<CuvetteBlankInfo>), lstCuvBlk));
                            break;
                        case "QueryNewPhotemetricValue":
                            List<List<OffSetGain>> lstNewPhotoGain = systemMaintenance.QueryNewPhotemetricValue(kvp.Key);
                            //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<List<OffSetGain>>), lstNewPhotoGain));
                            strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<List<OffSetGain>>), lstNewPhotoGain));
                            break;
                        //case "QueryOldPhotemetricValue":
                        //    List<OffSetGain> lstOldPhotoGain = systemMaintenance.QueryOldPhotemetricValue(param.StrmethodName);
                        //    client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, param.StrmethodName, XmlUtility.Serializer(typeof(List<OffSetGain>), lstOldPhotoGain));
                        //    break;
                    }
                }
                client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
            }
        }
        /// <summary>
        /// 安全管理（账号管理）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSystemUserManagements(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                UserInfo userInfo = new UserInfo();
                switch (kvp.Key)
                {

                    case "QueryUserInfo":
                        List<UserInfo> lisUserInfo = systemUserManagement.QueryUserManagement(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<UserInfo>), lisUserInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<UserInfo>), lisUserInfo));
                        break;
                    case "QueryUserCeation":
                        List<UserInfo> lisCeation = systemUserManagement.QueryUserCeation(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<UserInfo>), lisCeation));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<UserInfo>), lisCeation));
                        break;
                    case "AddUserInfo":
                        userInfo = XmlUtility.Deserialize(typeof(UserInfo), kvp.Value[0].ToString()) as UserInfo;
                        string strInfo = systemUserManagement.AddUserInfo(kvp.Key, userInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, strInfo);
                        strMethodParam.Add(kvp.Key, strInfo);
                        LogInfo.WriteProcessLog(strInfo, Module.WindowsService);
                        break;
                    case "EditUserInfo":
                        UserInfo UserInfoOld = new UserInfo();
                        userInfo = XmlUtility.Deserialize(typeof(UserInfo), kvp.Value[0].ToString()) as UserInfo;
                        UserInfoOld.UserID = kvp.Value[1].ToString();
                        int intUpDateResult = systemUserManagement.EditUserInfoUpDate(kvp.Key, userInfo, UserInfoOld);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intUpDateResult);
                        strMethodParam.Add(kvp.Key, intUpDateResult);
                        break;
                    case "DeleteUserInfo":
                        int intDeleteCount = systemUserManagement.DeleteUserInfo(kvp.Key, kvp.Value[0].ToString(), kvp.Value[1].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, intDeleteCount.ToString());
                        strMethodParam.Add(kvp.Key, intDeleteCount.ToString());
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 安全管理（用户操作）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSystemLogChecks(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {

                    case "QueryMaintenanceLogInfo":
                        List<MaintenanceLogInfo> lisMaintenanceLogInfo = systemLogCheck.QueryMaintenanceLogInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<MaintenanceLogInfo>), lisMaintenanceLogInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<MaintenanceLogInfo>), lisMaintenanceLogInfo));
                        break;
                    case "QueryOperationLogInfo":
                        List<MaintenanceLogInfo> lisOperationLogInfo = systemLogCheck.QueryOperationLogInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<MaintenanceLogInfo>), lisOperationLogInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<MaintenanceLogInfo>), lisOperationLogInfo));
                        break;
                    case "QueryAlarmLogInfo":
                        List<AlarmLogInfo> lisAlarmLogInfo = systemLogCheck.QueryAlarmLogInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<AlarmLogInfo>), lisAlarmLogInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AlarmLogInfo>), lisAlarmLogInfo));
                        break;
                    case "SelectAlarmLogInfoByUName":
                        AlarmLogInfo alarmLogInfo = XmlUtility.Deserialize(typeof(AlarmLogInfo), kvp.Value[0].ToString()) as AlarmLogInfo;
                        List<AlarmLogInfo> lisSelectAlarmLogInfo = systemLogCheck.SelectAlarmLogInfoByUName(kvp.Key, alarmLogInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<AlarmLogInfo>), lisSelectAlarmLogInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<AlarmLogInfo>), lisSelectAlarmLogInfo));
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 安全管理（科室管理）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void HandleSystemDepartmentManages(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
                switch (kvp.Key)
                {
                    case "QueryDepartmentInfo":
                        List<string> lisDepartment = systemDepartmentManage.QueryDepartment(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisDepartment));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lisDepartment));
                        LogInfo.WriteProcessLog(kvp.Key + ":" + lisDepartment.Count, Module.WindowsService);
                        break;
                    case "AddDepartmentInfo":
                        string strInfo = systemDepartmentManage.AddDepartmentInfo(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), strInfo));
                        strMethodParam.Add(kvp.Key, strInfo);
                        LogInfo.WriteProcessLog(kvp.Key + ":" + strInfo,Module.WindowsService);
                        break;
                    case "UpDataDepartment":
                        int UpdataCount = systemDepartmentManage.UpdataDepartment(kvp.Key, kvp.Value[0].ToString(), kvp.Value[1].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), UpdataCount.ToString()));
                        strMethodParam.Add(kvp.Key, UpdataCount);
                        LogInfo.WriteProcessLog(kvp.Key + ":" +UpdataCount, Module.WindowsService);
                        break;
                    case "DeleteDepartment":
                        int DeteleCount = systemDepartmentManage.DeleteDepartment(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, DeteleCount);
                        strMethodParam.Add(kvp.Key, DeteleCount);
                        LogInfo.WriteProcessLog(kvp.Key + ":" + DeteleCount, Module.WindowsService);
                        break;
                    case "QueryApplyDoctorInfo":
                        List<ApplyDoctorInfo> lstApplyDoctor = systemDepartmentManage.QueryApplyDoctorInfo(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<ApplyDoctorInfo>), lstApplyDoctor));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<ApplyDoctorInfo>), lstApplyDoctor));
                        break;
                    case "AddApplyDoctorInfo":
                        applyDoctorInfo = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), kvp.Value[0].ToString()) as ApplyDoctorInfo;
                        string AddApplyDoctor = systemDepartmentManage.AddApplyDoctor(kvp.Key, applyDoctorInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(ApplyDoctorInfo), AddApplyDoctor));
                        strMethodParam.Add(kvp.Key, AddApplyDoctor);
                        LogInfo.WriteProcessLog(kvp.Key + ":" + AddApplyDoctor, Module.WindowsService);
                        break;
                    case "DeleteApplyDoctorInfo":
                        applyDoctorInfo = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), kvp.Value[0].ToString()) as ApplyDoctorInfo;
                        int DeteleDoctorCount = systemDepartmentManage.DeleteApplyDoctorInfo(kvp.Key, applyDoctorInfo);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), DeteleDoctorCount.ToString()));
                        strMethodParam.Add(kvp.Key, DeteleDoctorCount);
                        LogInfo.WriteProcessLog(kvp.Key + ":" + DeteleDoctorCount, Module.WindowsService);
                        break;
                    case "UpdataApplyDoctorInfo":
                        ApplyDoctorInfo applyDoctorInfoOld = new ApplyDoctorInfo();
                        applyDoctorInfo = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), kvp.Value[0].ToString()) as ApplyDoctorInfo;
                        applyDoctorInfoOld = XmlUtility.Deserialize(typeof(ApplyDoctorInfo), kvp.Value[1].ToString()) as ApplyDoctorInfo;
                        int UpdataDoctorInfoOldCount = systemDepartmentManage.UpdataApplyDoctorInfo(kvp.Key, applyDoctorInfo, applyDoctorInfoOld);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), UpdataDoctorInfoOldCount.ToString()));
                        strMethodParam.Add(kvp.Key, UpdataDoctorInfoOldCount);
                        LogInfo.WriteProcessLog(kvp.Key + ":" + UpdataDoctorInfoOldCount, Module.WindowsService);
                        break;
                    case "QueryUserInfo":
                        List<UserInfo> lisUserInfo = systemUserManagement.QueryUserManagement(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<UserInfo>), lisUserInfo));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<UserInfo>), lisUserInfo));
                        break;
                    case "QueryAuditPhysician":
                        List<string> lstAuditPhysician = systemDepartmentManage.QueryAuditPhysician(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstAuditPhysician));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(List<string>), lstAuditPhysician));
                        break;
                    case "AddAuditPhysician":
                        string AddAuditPhysician = systemDepartmentManage.AddAuditPhysician(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), AddAuditPhysician));
                        strMethodParam.Add(kvp.Key, AddAuditPhysician);
                        break;
                    case "DeleteAuditPhysician":
                        int DeteleAuditPhysicianCount = systemDepartmentManage.DeleteAuditPhysician(kvp.Key, kvp.Value[0].ToString());
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), DeteleAuditPhysicianCount.ToString()));
                        strMethodParam.Add(kvp.Key, DeteleAuditPhysicianCount);
                        break;

                    case "UpdataAuditPhysician":
                        int UpDataAuditPhysicianCount = systemDepartmentManage.UpDataAuditPhysician(kvp.Key, kvp.Value[0].ToString(), kvp.Value[1].ToString());
                        client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(string), UpDataAuditPhysicianCount.ToString()));
                        strMethodParam.Add(kvp.Key, UpDataAuditPhysicianCount);
                        break;

                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }
        /// <summary>
        /// 安全管理（设备调试）
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="client"></param>
        /// <param name="param"></param>
        private void SystemEquipmentManages(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "QueryManuOffsetGain":
                        ManuOffsetGain manuGain = systemEquipmentManage.QueryManuOffsetGain(kvp.Key);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(ManuOffsetGain), manuGain));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(ManuOffsetGain), manuGain));
                        break;
                    case "InitialPhotometerManualCheck":
                        int iResult = systemEquipmentManage.InitialPhotometerManualCheck(kvp.Key, XmlUtility.Deserialize(typeof(ManuOffsetGain), kvp.Value[0].ToString()) as ManuOffsetGain);
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, iResult);
                        strMethodParam.Add(kvp.Key, iResult);
                        break;
                    case "GetLatestOffSetGain":
                        OffSetGain offSetGain = systemEquipmentManage.GetLatestOffSetGain(int.Parse(kvp.Value[0].ToString()));
                        //client.NotifyCallBack.DatabaseNotifyFunction(moduleInfo, kvp.Key, XmlUtility.Serializer(typeof(OffSetGain), offSetGain));
                        strMethodParam.Add(kvp.Key, XmlUtility.Serializer(typeof(OffSetGain), offSetGain));
                        break;
                    default:
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo, strMethodParam);
        }

        private void HandleMainTains(ModuleInfo moduleInfo, ClientRegisterInfo client, Dictionary<string, object[]> param)
        {
            strMethodParam.Clear();
            foreach (KeyValuePair<string, object[]> kvp in param)
            {
                switch (kvp.Key)
                {
                    case "GetAllTasksCount":
                        int task = mainTain.GetAllTasksCount(kvp.Key);
                        strMethodParam.Add(kvp.Key, task);
                        break;
                }
            }
            client.NotifyCallBack.DataAllReturnFunction(moduleInfo,strMethodParam);
        }
        

        /// <summary>
        /// 客户端向服务器发送信息
        /// </summary>
        /// <param name="param">发送对象</param>
        /// <returns>0, 1; 0代表发送失败，1代表发送成功</returns>
        public void ClientSendMsgToServiceMethod(ModuleInfo sendClientName, Dictionary<string, object[]> param)
        {
            lock (lockObj)
            {
                //LogInfo.WriteProcessLog(kvp.ToString(), Module.WindowsService);
                ClientRegisterInfo client = ClientInfoCache.Instance.Clients.Find(x => x.ClientName == "BioA.UI");
                int i = ClientInfoCache.Instance.Clients.Count;
                switch (sendClientName)
                {
                    case ModuleInfo.WorkingAreaApplyTask:
                        Console.WriteLine("WorkingAreaApplyTask begin " + DateTime.Now.Ticks);
                        HandleWorkingAreaApplyTasks(ModuleInfo.WorkingAreaApplyTask, client, param);
                        Console.WriteLine("WorkingAreaApplyTask End   " + DateTime.Now.Ticks);
                        break;
                    case ModuleInfo.WorkingAreaDataCheck:
                        HandleWorkingAreaDataChecks(ModuleInfo.WorkingAreaDataCheck, client, param);
                        break;
                    case ModuleInfo.ReagentSetting:
                        HandleReagentSettings(ModuleInfo.ReagentSetting, client, param);
                        break;
                    case ModuleInfo.ReagentState:
                        HandleReagentStates(ModuleInfo.ReagentState, client, param);
                        break;
                    case ModuleInfo.CalibControlTask:
                        HandleCalibControlTasks(ModuleInfo.CalibControlTask, client, param);
                        break;
                    case ModuleInfo.CalibrationMaintain:
                        HandleCalibrationMaintains(ModuleInfo.CalibrationMaintain, client, param);
                        break;
                    case ModuleInfo.CalibrationState:
                        HandleCalibrationStates(ModuleInfo.CalibrationState, client, param);
                        break;
                    case ModuleInfo.QCMaintain:
                        HandleQCMaintains(ModuleInfo.QCMaintain, client, param);
                        break;
                    case ModuleInfo.QCResult:
                        HandleQCResults(ModuleInfo.QCResult, client, param);
                        break;
                    case ModuleInfo.QCGraphic:
                        HandleQCGraphics(ModuleInfo.QCGraphic, client, param);
                        break;
                    case ModuleInfo.QCTask:
                        HandleQCTasks(ModuleInfo.QCTask, client, param);
                        break;
                    case ModuleInfo.SettingsChemicalParameter:
                        HandleSettingsChemicalParams(ModuleInfo.SettingsChemicalParameter, client, param);
                        break;
                    case ModuleInfo.SettingsCombProject:
                        HandleSettingsCombProjectInfos(ModuleInfo.SettingsCombProject, client, param);
                        break;
                    case ModuleInfo.SettingsCalculateItem:
                        HandleSettingsCalcProjectInfos(ModuleInfo.SettingsCalculateItem, client, param);
                        break;
                    case ModuleInfo.SettingsEnvironment:
                        HandleEnvironmentParamInfso(ModuleInfo.SettingsEnvironment, client, param);
                        break;
                    case ModuleInfo.SettingsCrossPollution:
                        HandleSettingsCrossPollutions(ModuleInfo.SettingsCrossPollution, client, param);
                        break;
                    case ModuleInfo.SettingsDataConfig:
                        HandleSettingsDataConfigs(ModuleInfo.SettingsDataConfig, client, param);
                        break;
                    case ModuleInfo.SettingsLISCommunicate:
                        HandleSettingsLISCommunicates(ModuleInfo.SettingsLISCommunicate, client, param);
                        break;
                    case ModuleInfo.SystemMaintenance:
                        HandleSystemMaintenances(ModuleInfo.SystemMaintenance, client, param);
                        break;
                    case ModuleInfo.SystemEquipmentManage:
                        SystemEquipmentManages(ModuleInfo.SystemEquipmentManage, client, param);
                        break;
                    case ModuleInfo.SystemUserManagement:
                        HandleSystemUserManagements(ModuleInfo.SystemUserManagement, client, param);
                        break;
                    case ModuleInfo.SystemDepartmentManage:
                        HandleSystemDepartmentManages(ModuleInfo.SystemDepartmentManage, client, param);
                        break;
                    case ModuleInfo.SystemLogCheck:
                        HandleSystemLogChecks(ModuleInfo.SystemLogCheck, client, param);
                        break;
                    case ModuleInfo.MainTain:
                        HandleMainTains(ModuleInfo.MainTain, client, param);
                        break;
                    default:
                        break;
                }
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
