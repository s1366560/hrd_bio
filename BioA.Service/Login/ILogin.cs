using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.Service
{
    public class ILogin : DataTransmit
    {
        public string UserLogin(string strMethodName, string userName, string password)
        {
            return myBatis.UserLogin(strMethodName, userName, password);
        }

        public UserInfo QueryUserAuthority(string strMethodName, string UserName)
        {
            return myBatis.QueryUserAuthority(strMethodName, UserName);
        }
        /// <summary>
        /// 将注销系统时间记入数据库
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="UserName"></param>
        public void SaveUserExitInfo(string strMethodName, string UserName)
        {
            myBatis.SaveUserExitInfo(strMethodName, UserName);
        }
        public void ISaveMaintenanceLogInfo(string strDBMethodParam, MaintenanceLogInfo maintenanceLogInfo)
        {
            myBatis.SaveMaintenanceLogInfo(strDBMethodParam,maintenanceLogInfo);
        }
    }
}
