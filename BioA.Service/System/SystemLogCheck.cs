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

        public List<TroubleLog> SelectTroubleLogInfoByTimeQuantum(string strDBMethod, string logStateTime, string logEnditTime)
        {
           return myBatis.SelectTroubleLogInfoByTimeQuantum(strDBMethod, logStateTime, logEnditTime);
        }

        public int AffirmTroubleLogInfo(string strDBMethid, List<string> lstDrawDateTime)
        {
            return myBatis.AffirmTroubleLogInfo(strDBMethid, lstDrawDateTime);
        }
    }
}
