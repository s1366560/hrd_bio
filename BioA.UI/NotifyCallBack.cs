using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BioA.UI.ServiceReference1;
using BioA.Common;
using System.Windows.Forms;

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
            switch (moduleInfo)
            {
                case ModuleInfo.Login:
                    if (LoginDataTransferEvent != null)
                    {
                        LoginDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.WorkingAreaApplyTask:
                    if (ApplyTaskDataTransferEvent != null)
                    {
                        ApplyTaskDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.WorkingAreaDataCheck:
                    if (CommonSampleDataEvent != null)
                    {
                        CommonSampleDataEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.WorkingAreaCalibDataCheck:
                    break;
                case ModuleInfo.ReagentState:
                    if (ReagentStateDataTransferEvent != null)
                    {
                        ReagentStateDataTransferEvent(strMethod, sender);
                    }

                    break;
                case ModuleInfo.CalibControlTask:
                    if (CalibControlTaskDataTransferEvent != null)
                    {
                        CalibControlTaskDataTransferEvent(strMethod, sender);
                    }

                    break;
                case ModuleInfo.ReagentSetting:
                    if (ReagentSettingsDataTransferEvent != null)
                    {
                        ReagentSettingsDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.CalibrationState:               
                    if ( CalibrationStateDataTransferEvent!= null)
                    {
                         CalibrationStateDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.CalibrationMaintain:
                    if (CalibMaintainDataTransferEvent != null)
                    {
                        CalibMaintainDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.QCResult:
                    if (QCResultDataTransferEvent != null)
                    {
                        QCResultDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.QCMaintain:
                    if (QCMaintainDataTransferEvent != null)
                    {
                        QCMaintainDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.QCGraphic:
                    if (QCGraphicsDataTransferEvent != null)
                    {
                        QCGraphicsDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.QCTask:
                    if (QCTaskDataTransferEvent != null)
                    {
                        QCTaskDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SettingsChemicalParameter:
                    if (ChemicalParamDataTransferEvent != null)
                    {
                        ChemicalParamDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SettingsCombProject:
                    if (CombProjectDataTransferEvent != null)
                    {
                        CombProjectDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SettingsCalculateItem:
                    if (CalcProjectDataTransferEvent != null)
                    {
                        CalcProjectDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SettingsCrossPollution:
                    if (ReagentNeedleDataTransferEvent != null)
                    {
                        ReagentNeedleDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SettingsDataConfig:
                    if (DataConfigDataTransferEvent != null)
                    {
                        DataConfigDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SettingsEnvironment:
                    if (EnvironmentDataTransferEvent != null)
                    {
                        EnvironmentDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SettingsLISCommunicate:
                    if (LISCommunicateDataTransferEvent != null)
                    {
                        LISCommunicateDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SystemConfigure:
                    break;
                case ModuleInfo.SystemDepartmentManage:
                    if (DepartmentManageDataTransferEvent != null)
                    {
                        DepartmentManageDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SystemEquipmentManage:
                    if (SystemTestEquipmentEvent != null)
                    {
                        SystemTestEquipmentEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SystemLogCheck:
                    if (LogDataTransferEvent != null)
                    {
                        LogDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SystemMaintenance:
                    if (SystemMaintenanceDataTransferEvent != null)
                    {
                        SystemMaintenanceDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SystemUserManagement:
                    if (UserManagementDataTransferEvent != null)
                    {
                        UserManagementDataTransferEvent(strMethod, sender);
                    }
                    break;
                case ModuleInfo.SystemVersionInfomation:
                    break;
                case ModuleInfo.MainTain:
                    if (StartTestTaskDataTransferEvent != null)
                    {
                        StartTestTaskDataTransferEvent(strMethod, sender);
                    }
                    break;
                default:
                    break;
            }

            if (DataTransferEvent != null)
            {
                DataTransferEvent(strMethod, sender);
            }
        }
    }
}
