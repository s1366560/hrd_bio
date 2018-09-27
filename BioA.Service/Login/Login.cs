using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.Service
{
    public class Login : DataTransmit
    {
        public string UserLogin(string strMethodName, string userName, string password)
        {
            return myBatis.UserLogin(strMethodName, userName, password);
        }

        public UserInfo QueryUserAuthority(string strMethodName, string UserName)
        {
            return myBatis.QueryUserAuthority(strMethodName, UserName);
        }

        public void ISaveMaintenanceLogInfo(string strDBMethodParam, MaintenanceLogInfo maintenanceLogInfo)
        {
            myBatis.SaveMaintenanceLogInfo(strDBMethodParam,maintenanceLogInfo);
        }
    }
}
