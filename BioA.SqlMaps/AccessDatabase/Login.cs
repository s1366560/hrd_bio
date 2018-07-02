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
        public string UserLogin(string strMethodName, string[] strCommunicates)
        {
            string strResult = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("UserID", strCommunicates[0]);
                ht.Add("Password", strCommunicates[1]);

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

        public UserInfo QueryUserAuthority(string strMethodName, string UserName)
        {
            UserInfo userInfo = new UserInfo();
            try
            {
                userInfo = ism_SqlMap.QueryForObject("LogInfo." + strMethodName, UserName) as UserInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryUserAuthority(string strMethodName, string UserName)==" + e.ToString(), Module.DAO);
            }
            return userInfo;
        }




    }
}
