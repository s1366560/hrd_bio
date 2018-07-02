using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class AlarmLogInfo
    {
      public AlarmLogInfo()
        {
            faultCode = string.Empty;
            alarmReason= string.Empty;
            logDetails= string.Empty;
            alarmLevel= string.Empty;
            userName= string.Empty;
            logDateTime = DateTime.Now;
            logStartTime= DateTime.Now;
            logEndTime = DateTime.Now;
            isSolve = string.Empty;
        }



        string faultCode;
       /// <summary>
        /// 故障编码
       /// </summary>
        public string FaultCode
        {
            get { return faultCode; }
            set { faultCode = value; }
        }
        string alarmReason;
       /// <summary>
        /// 报警的原因
       /// </summary>
        public string AlarmReason
        {
            get { return alarmReason; }
            set { alarmReason = value; }
        }
        string logDetails;
       /// <summary>
       /// 日志详细信息
       /// </summary>
        public string LogDetails
        {
            get { return logDetails; }
            set { logDetails = value; }
        }
        string alarmLevel;
       /// <summary>
       /// 报警等级
       /// </summary>
        public string AlarmLevel
        {
            get { return alarmLevel; }
            set { alarmLevel = value; }
        }
        string userName;
       /// <summary>
       /// 用户名
       /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        DateTime logDateTime;

       public DateTime LogDateTime
        {
            get { return logDateTime; }
            set { logDateTime = value; }
        }

        DateTime logStartTime;
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime LogstartTime
        {
            get { return logStartTime; }
            set { logStartTime = value; }
        }
        DateTime logEndTime;
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime LogEndTime
        {
            get { return logEndTime; }
            set { logEndTime = value; }
        }
        string isSolve;

        public string IsSolve
        {
            get { return isSolve; }
            set { isSolve = value; }
        }
    }
}
