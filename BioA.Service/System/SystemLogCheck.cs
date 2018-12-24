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
            return myBatis.QueryMaintenanceLogInfo(strDBMethod);
        }

        public List<MaintenanceLogInfo> QueryOperationLogInfo(string strDBMethod)
        { 
            return myBatis.QueryOperationLogInfo(strDBMethod);
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
