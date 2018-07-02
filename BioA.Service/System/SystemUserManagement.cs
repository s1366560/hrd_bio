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
        internal List<UserInfo> QueryUserManagement(string strDBMethod, string dataConfig)
        {
            List<UserInfo> lstQueryUserManagement = new List<UserInfo>();
            try
            {
                lstQueryUserManagement = myBatis.QueryUserManagement(strDBMethod, null);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstQueryUserManagement, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryUserManagement;
        }

       

        internal string AddUserInfo(string strDBMethod, UserInfo userInfo)
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

        internal int EditUserInfoUpDate(string strDBMethod, UserInfo dataConfig, UserInfo dataConfigOld)
        {
            return myBatis.EditUserInfoUpDate(strDBMethod, dataConfig, dataConfigOld);
        }

        internal int DeleteUserInfo(string strDBMethod, string dataConfig,string UserName)
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

        internal List<UserInfo> QueryUserCeation(string strDBMethod, string p2)
        {
            List<UserInfo> lstQueryUserCeation = new List<UserInfo>();
            try
            {
                lstQueryUserCeation = myBatis.QueryUserCeation(strDBMethod, p2);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstQueryUserCeation, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryUserCeation;
        }
    }
}
