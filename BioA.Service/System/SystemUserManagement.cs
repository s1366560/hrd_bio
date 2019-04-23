using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class SystemUserManagement : DataTransmit
    {
        public List<UserInfo> QueryUserManagement(string strDBMethod, bool bol)
        {
            return myBatis.QueryUserManagement(strDBMethod, bol);
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int AddUserInfo(string strDBMethod, UserInfo userInfo)
        {
            int iResult = 0;
            try
            {
                int count = myBatis.SelectUserInfo("SelectUserInfo", userInfo);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    iResult = myBatis.AddUserInfo(strDBMethod, userInfo);
                    
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }

            return iResult;
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
        /// <summary>
        /// 修改普通用户信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int IUpdateCommonUserInfo(UserInfo userInfo)
        {
            return myBatis.UpdateCommonUserInfo(userInfo);
        }
    }
}
