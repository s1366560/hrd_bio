using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;
using System.Collections;

namespace BioA.SqlMaps
{
    public partial class MyBatis
    {
        public string UserLogin(string strMethodName, string userName, string password)
        {
            string strResult = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("UserID", userName);
                ht.Add("Password", password);

                int count = (int)ism_SqlMap.QueryForObject("LogInfo." + strMethodName, ht);

                if (count > 0)
                {
                    strResult = "登录成功！";
                }
                else
                {
                    strResult = "登录失败";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UserLogin(string strMethodName, string[] strCommunicates)==" + e.ToString(), Module.DAO);
            }

            return strResult;
        }
        /// <summary>
        /// 用户登录前获取权限
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public UserInfo QueryUserAuthority(string strMethodName, string UserName)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                Hashtable hashtable = new Hashtable();
                userInfo = ism_SqlMap.QueryForObject("LogInfo." + strMethodName, UserName) as UserInfo;
                if(userInfo.UserName != null && userInfo.UserPassword != null)
                {
                    hashtable.Add("UserName", userInfo.UserName);
                    hashtable.Add("LogDetails", "登录系统");
                    hashtable.Add("LogDateTime", DateTime.Now);
                    ism_SqlMap.Insert("LogInfo.SaveLoginLog", hashtable);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryUserAuthority(string strMethodName, string UserName)==" + e.ToString(), Module.DAO);
            }
            return userInfo;
        }
        /// <summary>
        /// 访问数据库记录注销时间
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="UserName"></param>
        public void SaveUserExitInfo(string strMethodName, string UserName)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                if (UserName != null)
                {
                    hashtable.Add("UserName", UserName);
                    hashtable.Add("LogDetails", "注销系统");
                    hashtable.Add("LogDateTime", DateTime.Now);
                    ism_SqlMap.Insert("LogInfo.SaveLoginLog", hashtable);
                }
               
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SaveUserExitInfo(string strMethodName, string UserName)==" + e.ToString(), Module.DAO);
            }

        }
        /// <summary>
        /// 保存保养日志信息
        /// </summary>
        /// <param name="strDBMethodParam"></param>
        /// <param name="maintenanceLogInfo"></param>
        public void SaveMaintenanceLogInfo(string strDBMethodParam, MaintenanceLogInfo maintenanceLogInfo)
        {
            try
            {
                ism_SqlMap.Insert("LogInfo." + strDBMethodParam, maintenanceLogInfo);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("SaveMaintenanceLogInfo(string strDBMethodParam, MaintenanceLogInfo maintenanceLogInfo) ==" + ex.ToString(), Module.DAO);
            }
        }
    }
}
