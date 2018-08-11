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


        public List<string> QueryDepartment(string strDBMethod)
        {
            List<string> lstQueryDepartment = new List<string>();
            lstQueryDepartment = myBatis.QueryDepartment(strDBMethod);
            
            return lstQueryDepartment;
        }
        public string AddDepartmentInfo(string strDBMethod, string dataConfig)
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


        public int DeleteDepartment(string strDBMethod, string dataConfig)
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

        public int UpdataDepartment(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33", Module.WindowsService);
            return myBatis.UpdataDepartment(strDBMethod, dataConfig, dataConfigOld);
           
        }

        public List<ApplyDoctorInfo> QueryApplyDoctorInfo(string strDBMethod)
        {
            List<ApplyDoctorInfo> lstQueryDepartment = new List<ApplyDoctorInfo>();
            lstQueryDepartment = myBatis.QueryApplyDoctorInfo(strDBMethod);
            return lstQueryDepartment;
        }

        public string AddApplyDoctor(string strDBMethod, ApplyDoctorInfo applyDoctorInfo)
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

        public int DeleteApplyDoctorInfo(string strDBMethod, ApplyDoctorInfo applyDoctorInfo)
        {
            return myBatis.DeleteApplyDoctorInfo(strDBMethod, applyDoctorInfo);
        }

        public int UpdataApplyDoctorInfo(string strDBMethod, ApplyDoctorInfo applyDoctorInfo, ApplyDoctorInfo applyDoctorInfoOld)
        {
            return myBatis.UpdataApplyDoctorInfo(strDBMethod, applyDoctorInfo, applyDoctorInfoOld);
        }

        public List<string> QueryAuditPhysician(string strDBMethod)
        {
            List<string> lstQueryAuditPhysician = new List<string>();
            lstQueryAuditPhysician = myBatis.QueryAuditPhysician(strDBMethod);
            return lstQueryAuditPhysician;
        }

        public string AddAuditPhysician(string strDBMethod, string dataConfig)
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

        public int DeleteAuditPhysician(string strDBMethod, string dataConfig)
        {
            return myBatis.DeleteAuditPhysician(strDBMethod, dataConfig);
        }

        public int UpDataAuditPhysician(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            return myBatis.UpDataAuditPhysician(strDBMethod, dataConfig, dataConfigOld);
        }
    }
}
