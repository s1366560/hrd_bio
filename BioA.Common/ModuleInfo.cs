using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public enum ModuleInfo
    {
        WorkingAreaApplyTask = 1,
        WorkingAreaCalibDataCheck = 2,
        WorkingAreaDataCheck = 3,
        ReagentSetting = 4,
        ReagentState = 5,
        CalibrationMaintain = 6,
        CalibrationState = 7,
        QCMaintain = 8,
        QCState = 9,
        SettingsChemicalParameter = 10,
        SettingsCombinationItem = 11,
        SettingsCalculateItem = 12,
        SettingsEnvironment = 13,
        SettingsCrossPollution = 14,
        SettingsDataConfig = 15,
        SettingsLISCommunicate = 16,
        SystemMaintenance = 17,
        SystemEquipmentManage = 18,
        SystemUserManagement = 19,
        SystemDepartmentManage = 20,
        SystemConfigure = 21,
        SystemLogCheck = 22,
        SystemVersionInfomation = 23
    }
}
