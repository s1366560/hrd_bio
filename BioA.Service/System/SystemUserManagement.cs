using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    class SystemUserManagement : DataTransmit
    {
        public List<UserInfo> QueryUserManagement(string strDBMethod)
        {
            List<UserInfo> lstQueryUserManagement = new List<UserInfo>();
            lstQueryUserManagement = myBatis.QueryUserManagement(strDBMethod);
            return lstQueryUserManagement;
        }



        public string AddUserInfo(string strDBMethod, UserInfo userInfo)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectUserInfo("SelectUserInfo", userInfo);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.AddUserInfo(strDBMethod, userInfo);
                    count = myBatis.SelectUserInfo("SelectUserInfo", userInfo);
                    if (count > 0)
                    {
                        strInfo = "项目创建成功！";
                    }
                    else
                    {
                        strInfo = "项目创建失败，请联系管理员！";
                    }
                }
                else
                {
                    strInfo = "该项目已存在，请重新录入。";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }

            return strInfo;
        }

        public int EditUserInfoUpDate(string strDBMethod, UserInfo dataConfig, string OldUserId)
        {
            return myBatis.EditUserInfoUpDate(strDBMethod, dataConfig, OldUserId);
        }

        public int DeleteUserInfo(string strDBMethod, string dataConfig, string UserName)
        {
            int count = 0;

            try
            {
                count = myBatis.SelectDeleteAuditPhysician("SelectDeleteAuditPhysicianInfo", UserName);
                // 当count>0代表已存在此项目
                if (count == 0)
                {
                    myBatis.DeleteUserInfo(strDBMethod, dataConfig);
                }
                else
                {

                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }


            return count;
            
        }

        public UserInfo QueryUserCeation(string strDBMethod, string p2)
        {
            return myBatis.QueryUserCeation(strDBMethod, p2);
        }
    }
}
