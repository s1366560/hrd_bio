using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.Service
{
    class SystemLogCheck : DataTransmit
    {


        internal List<MaintenanceLogInfo> QueryMaintenanceLogInfo(string strDBMethod, string p2)
        {
            List<MaintenanceLogInfo> lstQueryMaintenanceLogInfo = new List<MaintenanceLogInfo>();
            try
            {
                lstQueryMaintenanceLogInfo = myBatis.QueryMaintenanceLogInfo(strDBMethod, null);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstQueryMaintenanceLogInfo, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryMaintenanceLogInfo;
        }

        internal List<MaintenanceLogInfo> QueryOperationLogInfo(string strDBMethod, string p2)
        {
            List<MaintenanceLogInfo> lstQueryMaintenanceLogInfo = new List<MaintenanceLogInfo>();
            try
            {
                lstQueryMaintenanceLogInfo = myBatis.QueryOperationLogInfo(strDBMethod, null);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstQueryMaintenanceLogInfo, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryMaintenanceLogInfo;
        }

        internal List<AlarmLogInfo> QueryAlarmLogInfo(string strDBMethod, string p2)
        {
            List<AlarmLogInfo> lstQueryAlarmLogInfo = new List<AlarmLogInfo>();
            try
            {
                lstQueryAlarmLogInfo = myBatis.QueryAlarmLogInfo(strDBMethod, null);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstQueryAlarmLogInfo, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryAlarmLogInfo;
        }



        internal List<AlarmLogInfo> SelectAlarmLogInfo(string strDBMethod, AlarmLogInfo alarmLogInfo)
        {
            List<AlarmLogInfo> lstQueryAlarmLogInfo = new List<AlarmLogInfo>();

            lstQueryAlarmLogInfo = myBatis.SelectAlarmLogInfo(strDBMethod, alarmLogInfo);

            return lstQueryAlarmLogInfo;
        }
    }
}
