using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BioA.UI.ServiceReference1;
using BioA.Common;
using System.Windows.Forms;
using System.Threading;

namespace BioA.UI
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class NotifyCallBack : IBioAServiceCallback
    {
        public delegate void DataTransferDelegate(string strMethod, object sender);
        /// <summary>
        /// 接收数据库传输的数据，并发送至窗体
        /// </summary>
        public DataTransferDelegate DataTransferEvent;
        public DataTransferDelegate LoginDataTransferEvent; // 登录
        public DataTransferDelegate StartTestTaskDataTransferEvent; //启动测试任务事件
        public DataTransferDelegate ApplyTaskDataTransferEvent; // 常规任务申请事件
        public DataTransferDelegate CommonSampleDataEvent; // 普通任务结果        
        public DataTransferDelegate QCMaintainDataTransferEvent; // 质控品维护事件
        public DataTransferDelegate QCGraphicsDataTransferEvent; // 质控图事件
        public DataTransferDelegate QCResultDataTransferEvent;   // 质控结果事件
        public DataTransferDelegate QCTaskDataTransferEvent;     // 质控任务事件
        public DataTransferDelegate CalcProjectDataTransferEvent;  // 计算项目事件
        public DataTransferDelegate ChemicalParamDataTransferEvent; // 生化项目事件
        public DataTransferDelegate CombProjectDataTransferEvent; // 组合项目
        public DataTransferDelegate EnvironmentDataTransferEvent; // 环境参数
        public DataTransferDelegate LISCommunicateDataTransferEvent; // LIS通讯
        public DataTransferDelegate DataConfigDataTransferEvent; // 数据配置
        public DataTransferDelegate ReagentNeedleDataTransferEvent; // 交叉污染
        public DataTransferDelegate SystemMaintenanceDataTransferEvent; //QueryWaterBlankValueByWave
        public DataTransferDelegate SystemTestEquipmentEvent;
        public DataTransferDelegate UserManagementDataTransferEvent; // 用户管理
        public DataTransferDelegate DepartmentManageDataTransferEvent; // 科室管理
        public DataTransferDelegate LogDataTransferEvent; // 日志管理
        public DataTransferDelegate ReagentSettingsDataTransferEvent; // 试剂装载
        public DataTransferDelegate ReagentStateDataTransferEvent; // 试剂状态
        public DataTransferDelegate CalibMaintainDataTransferEvent; // 校准设置
        public DataTransferDelegate CalibrationStateDataTransferEvent; // 校准状态
        public DataTransferDelegate CalibControlTaskDataTransferEvent; // 校准状态
       
        public void NotifyFunction(object sender)
        {

        }

        public void ClientNotifyFunction(string strSendClientName, object Sender)
        {

        }
        public void DatabaseNotifyFunction(ModuleInfo moduleInfo, string strMethod, object sender)
        {
            Console.WriteLine("DatabaseNotifyFunction begin" + DateTime.Now.Ticks);
            Console.WriteLine("DatabaseNotfityFunction end " + DateTime.Now.Ticks);
        }

        /// <summary>
        /// 接收服务器返回数据并传递给客户端
        /// </summary>
        /// <param name="moduleInfo"></param>
        /// <param name="strMethodParam"></param>
        public void DataAllReturnFunction(ModuleInfo moduleInfo, Dictionary<string, object> strMethodParam)
        {
            switch (moduleInfo)
            {
                case ModuleInfo.WorkingAreaApplyTask:
                    if (ApplyTaskDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            ApplyTaskDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.WorkingAreaDataCheck:
                    if (CommonSampleDataEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            CommonSampleDataEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.ReagentState:
                    if (ReagentStateDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            ReagentStateDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.ReagentSetting:
                    if (ReagentSettingsDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            ReagentSettingsDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.CalibControlTask:
                    if (CalibControlTaskDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            CalibControlTaskDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.CalibrationState:
                    if (CalibrationStateDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            CalibrationStateDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.CalibrationMaintain:
                    if (CalibMaintainDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            CalibMaintainDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.QCResult:
                    if (QCResultDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            QCResultDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.QCMaintain:
                    if (QCMaintainDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            QCMaintainDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.QCGraphic:
                    if (QCGraphicsDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            QCGraphicsDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.QCTask:
                    if (QCTaskDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            QCTaskDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SettingsChemicalParameter:
                    if (ChemicalParamDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            ChemicalParamDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SettingsCombProject:
                    if (CombProjectDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            CombProjectDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SettingsCalculateItem:
                    if (CalcProjectDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            CalcProjectDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SettingsCrossPollution:
                    if (ReagentNeedleDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            ReagentNeedleDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SettingsDataConfig:
                    if (DataConfigDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            DataConfigDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SettingsEnvironment:
                    if (EnvironmentDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            EnvironmentDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SettingsLISCommunicate:
                    if (LISCommunicateDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            LISCommunicateDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SystemDepartmentManage:
                    if (DepartmentManageDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            DepartmentManageDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SystemEquipmentManage:
                    if (SystemTestEquipmentEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            SystemTestEquipmentEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SystemLogCheck:
                    if (LogDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            LogDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SystemMaintenance:
                    if (SystemMaintenanceDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            SystemMaintenanceDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.SystemUserManagement:
                    if (UserManagementDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            UserManagementDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                case ModuleInfo.MainTain:
                    if (StartTestTaskDataTransferEvent != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in strMethodParam)
                        {
                            StartTestTaskDataTransferEvent(kvp.Key, kvp.Value);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
