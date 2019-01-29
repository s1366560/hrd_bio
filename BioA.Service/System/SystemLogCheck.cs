using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.Service
{
  public  class SystemLogCheck : DataTransmit
    {


        public List<MaintenanceLogInfo> QueryMaintenanceLogInfo(string strDBMethod)
        {
            return myBatis.QueryMaintenanceLogInfo(strDBMethod);
        }

        public List<MaintenanceLogInfo> QueryOperationLogInfo(string strDBMethod, string startDate, string endDate)
        {
            return myBatis.QueryOperationLogInfo(strDBMethod, startDate, endDate);
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
        /// <summary>
        /// 删除操作日志
        /// </summary>
        /// <param name="strDBMethid"></param>
        /// <param name="lstDrawDateTime"></param>
        /// <returns></returns>
        public int DeleteOperationLogInfo(string strDBMethid, List<string> lstDrawDateTime)
        {
            return myBatis.DeleteOperationLogInfo(strDBMethid, lstDrawDateTime);
        }
    }
}
