using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    class SystemDepartmentManage : DataTransmit
    {


        internal List<string> QueryDepartment(string strDBMethod, string p2)
        {
            List<string> lstQueryDepartment = new List<string>();
            try
            {
                lstQueryDepartment = myBatis.QueryDepartment(strDBMethod, null);
               
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryDepartment;
        }
        internal string AddDepartmentInfo(string strDBMethod, string dataConfig)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectDepartment("SelectDepartment", dataConfig);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.AddDepartmentInfo(strDBMethod, dataConfig);
                    count = myBatis.SelectDepartment("SelectDepartment", dataConfig);
                    if (count > 0)
                    {
                        strInfo = "科室创建成功！";
                    }
                    else
                    {
                        strInfo = "科室创建失败，请联系管理员！";
                    }
                }
                else
                {
                    strInfo = "该科室已存在，请重新录入。";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }

            return strInfo;
        }


        internal int DeleteDepartment(string strDBMethod, string dataConfig)
        {
            int count = 0;
            
            try
            {
                 count = myBatis.SelectApplyDoctorDepartment("SelectApplyDoctorDepartmentInfo", dataConfig);
                // 当count>0代表已存在此项目
                if (count == 0)
                {
                     myBatis.DeleteDepartment(strDBMethod, dataConfig);
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

        internal int UpdataDepartment(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33", Module.WindowsService);
            return myBatis.UpdataDepartment(strDBMethod, dataConfig, dataConfigOld);
           
        }

        internal List<ApplyDoctorInfo> QueryApplyDoctor(string strDBMethod, string p2)
        {
            List<ApplyDoctorInfo> lstQueryDepartment = new List<ApplyDoctorInfo>();
            try
            {
                lstQueryDepartment = myBatis.QueryApplyDoctor(strDBMethod, null);
               
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryDepartment;
        }

        internal string AddApplyDoctor(string strDBMethod, ApplyDoctorInfo applyDoctorInfo)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectApplyDoctor("SelectApplyDoctorInfo", applyDoctorInfo);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.AddApplyDoctor(strDBMethod, applyDoctorInfo);
                    count = myBatis.SelectApplyDoctor("SelectApplyDoctorInfo", applyDoctorInfo);
                    if (count > 0)
                    {
                        strInfo = "申请医生创建成功！";
                    }
                    else
                    {
                        strInfo = "申请医生创建失败，请联系管理员！";
                    }
                }
                else
                {
                    strInfo = "该申请医生已存在，请重新录入。";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }

            return strInfo;
        }

        internal int DeleteApplyDoctorInfo(string strDBMethod, ApplyDoctorInfo applyDoctorInfo)
        {
            return myBatis.DeleteApplyDoctorInfo(strDBMethod, applyDoctorInfo);
        }

        internal int UpdataApplyDoctorInfo(string strDBMethod, ApplyDoctorInfo applyDoctorInfo, ApplyDoctorInfo applyDoctorInfoOld)
        {
            return myBatis.UpdataApplyDoctorInfo(strDBMethod, applyDoctorInfo, applyDoctorInfoOld);
        }

        internal List<string> QueryAuditPhysician(string strDBMethod, string p2)
        {
            List<string> lstQueryAuditPhysician = new List<string>();
            try
            {
                lstQueryAuditPhysician = myBatis.QueryAuditPhysician(strDBMethod, null);

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryAuditPhysician;
        }

        internal string AddAuditPhysician(string strDBMethod, string dataConfig)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectAuditPhysician("SelectAuditPhysician", dataConfig);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.AddAuditPhysician(strDBMethod, dataConfig);
                    count = myBatis.SelectAuditPhysician("SelectAuditPhysician", dataConfig);
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

        internal int DeleteAuditPhysician(string strDBMethod, string dataConfig)
        {
            return myBatis.DeleteAuditPhysician(strDBMethod, dataConfig);
        }

        internal int UpDataAuditPhysician(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            return myBatis.UpDataAuditPhysician(strDBMethod, dataConfig, dataConfigOld);
        }
    }
}
