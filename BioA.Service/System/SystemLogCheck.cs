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


        public List<MaintenanceLogInfo> QueryMaintenanceLogInfo(string strDBMethod)
        {
            List<MaintenanceLogInfo> lstQueryMaintenanceLogInfo = new List<MaintenanceLogInfo>();
            
            lstQueryMaintenanceLogInfo = myBatis.QueryMaintenanceLogInfo(strDBMethod);
            
            return lstQueryMaintenanceLogInfo;
        }

        public List<MaintenanceLogInfo> QueryOperationLogInfo(string strDBMethod)
        {
            List<MaintenanceLogInfo> lstQueryMaintenanceLogInfo = new List<MaintenanceLogInfo>();
            lstQueryMaintenanceLogInfo = myBatis.QueryOperationLogInfo(strDBMethod);
            return lstQueryMaintenanceLogInfo;
        }

        public List<AlarmLogInfo> QueryAlarmLogInfo(string strDBMethod)
        {
            List<AlarmLogInfo> lstQueryAlarmLogInfo = new List<AlarmLogInfo>();
            lstQueryAlarmLogInfo = myBatis.QueryAlarmLogInfo(strDBMethod);
            return lstQueryAlarmLogInfo;
        }



        public List<AlarmLogInfo> SelectAlarmLogInfoByUName(string strDBMethod, AlarmLogInfo alarmLogInfo)
        {
            List<AlarmLogInfo> lstQueryAlarmLogInfo = new List<AlarmLogInfo>();

            lstQueryAlarmLogInfo = myBatis.SelectAlarmLogInfoByUName(strDBMethod, alarmLogInfo);

            return lstQueryAlarmLogInfo;
        }
    }
}
