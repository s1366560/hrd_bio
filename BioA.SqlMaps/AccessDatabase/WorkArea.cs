using BioA.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.SqlMaps
{
    public partial class MyBatis
    {
        public int QueryMaxSampleNum(string StrmethodName)
        {
            int intResult = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());
                object obj = ism_SqlMap.QueryForObject("WorkAreaApplyTask." + StrmethodName, ht);
                if (obj != null)
                {
                    intResult = (int)obj;
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryMaxSampleNum(string StrmethodName)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 通过样本类型获取项目名称
        /// </summary>
        /// <param name="StrmethodName">访问数据库方法</param>
        /// <param name="sampleType">样本类型</param>
        /// <returns>项目名称</returns>
        public List<string> QueryProNameForApplyTask(string StrmethodName, string sampleType)
        {
            List<string> lstProName = new List<string>();
            try
            {
                lstProName = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo.ProjectPageinfoBySampleType", sampleType);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProNameForApplyTask(string StrmethodName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return lstProName;
        }
        /// <summary>
        /// 获取常规样本任务信息
        /// </summary>
        /// <param name="StrmethodName"></param>
        /// <returns></returns>
        public List<SampleInfo> QueryApplyTaskLsvt(string StrmethodName)
        {
            List<SampleInfo> lstSampleInfo = new List<SampleInfo>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());

                lstSampleInfo = (List<SampleInfo>)ism_SqlMap.QueryForList<SampleInfo>("WorkAreaApplyTask." + StrmethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryApplyTaskLsvt(string StrmethodName)==" + e.ToString(), Module.DAO);
            }

            return lstSampleInfo;
        }
        /// <summary>
        /// 添加普通和急诊任务
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="sampleInfo"></param>
        /// <param name="lstSampleInfo"></param>
        /// <returns></returns>
        public string AddTask(string strMethodName, SampleInfo sampleInfo, List<TaskInfo> lstSampleInfo)
        {
            string strResult = string.Empty;

            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleInfo.SampleNum);
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());

                int count = (int)ism_SqlMap.QueryForObject("WorkAreaApplyTask.QuerySampleCountByNumber", ht);
                if (count > 0)
                {
                    strResult = "此样本任务已经存在，请重新录入！";
                }
                else
                {
                    ism_SqlMap.Insert("WorkAreaApplyTask.AddSample", sampleInfo);

                    foreach (TaskInfo t in lstSampleInfo)
                    {
                        ism_SqlMap.Insert("WorkAreaApplyTask.AddTask", t);

                        ht.Clear();
                        ht.Add("SampleNum", t.SampleNum);
                        ht.Add("SampleCreateTime", t.CreateDate);
                        ht.Add("ProjectName", t.ProjectName);
                        ht.Add("SampleType", t.SampleType);
                        ism_SqlMap.Insert("WorkAreaApplyTask.AddSampleResult", ht);
                        //ism_SqlMap.Insert("WorkAreaApplyTask.AddSampleReactionProcess", ht);
                    }

                    ht.Clear();
                    ht.Add("SampleNum", sampleInfo.SampleNum);
                    ht.Add("InputTime", sampleInfo.CreateTime);
                    ism_SqlMap.Insert("WorkAreaApplyTask.AddPatientInfoByAddTask", ht);

                    strResult = "任务创建成功！";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddTask(string strMethodName, SampleInfo sampleInfo, List<TaskInfo> lstSampleInfo)==" + e.ToString(), Module.DAO);
            }

            return strResult;
        }

        public List<TaskInfo> QueryTaskInfoBySampleNum(string strMethodName, string SampleNum)
        {
            List<TaskInfo> lstTaskInfo = new List<TaskInfo>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", SampleNum);
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());

                lstTaskInfo = (List<TaskInfo>)ism_SqlMap.QueryForList<TaskInfo>("WorkAreaApplyTask." + strMethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryTaskInfoBySampleNum(string strMethodName, string SampleNum)==" + e.ToString(), Module.DAO);
            }

            return lstTaskInfo;
        }

        public PatientInfo QueryPatientInfoBySampleNum(string strMethodName, int sampleNum)
        {
            PatientInfo patientInfo = new PatientInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());
                ht.Add("SampleNum", sampleNum);

                patientInfo = (PatientInfo)ism_SqlMap.QueryForObject("WorkAreaApplyTask." + strMethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryPatientInfoBySampleNum(string strMethodName, int sampleNum)==" + e.ToString(), Module.DAO);
            }

            return patientInfo;
        }

        public string UpdatePatientInfo(string strMethodName, PatientInfo patientInfo)
        {
            string str = "更新成功！";
            try
            {
                patientInfo.InputTime = DateTime.Now;

                Hashtable ht = new Hashtable();
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());
                ht.Add("SampleNum", patientInfo.SampleNum);
                int count = (int)ism_SqlMap.QueryForObject("WorkAreaApplyTask.QueryPatientInfoCountByNum", ht);

                if (count > 0)
                {
                    ism_SqlMap.Update("WorkAreaApplyTask." + strMethodName, patientInfo);
                }
                else
                {
                    ism_SqlMap.Insert("WorkAreaApplyTask.AddPatientInfo", patientInfo);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdatePatientInfo(string strMethodName, PatientInfo patientInfo)==" + e.ToString(), Module.DAO);
                str = "更新失败！";
            }
            return str;
        }

        public List<string> QueryApplyApartment(string strMethodName)
        {
            List<string> lstApplyDepartment = new List<string>();
            try
            {
                lstApplyDepartment = (List<string>)ism_SqlMap.QueryForList<string>("DepartmentInfo.QueryDepartmentInfo", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryApplyApartment(string strMethodName)==" + e.ToString(), Module.DAO);
            }
            return lstApplyDepartment;
        }

        public List<string> QueryApplyDoctor(string strMethodName)
        {
            List<string> lstApplyDoctor = new List<string>();
            try
            {
                List<ApplyDoctorInfo> lstDoctorInfo = (List<ApplyDoctorInfo>)ism_SqlMap.QueryForList<ApplyDoctorInfo>("DepartmentInfo.QueryApplyDoctorInfo", null);

                foreach (ApplyDoctorInfo doctorInfo in lstDoctorInfo)
                {
                    lstApplyDoctor.Add(doctorInfo.Doctor);
                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryApplyDoctor(string strMethodName)==" + e.ToString(), Module.DAO);
            }
            return lstApplyDoctor;
        }

        public List<string> QueryCheckDoctor(string strMethodName)
        {
            List<string> lstCheckDoctor = new List<string>();
            try
            {
                lstCheckDoctor = (List<string>)ism_SqlMap.QueryForList<string>("DepartmentInfo.QueryAuditPhysician", null);


            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCheckDoctor(string strMethodName)==" + e.ToString(), Module.DAO);
            }
            return lstCheckDoctor;
        }

        public List<string> QueryInspectDoctor(string strMethodName)
        {
            List<string> lstInspectDoctor = new List<string>();
            try
            {
                lstInspectDoctor = (List<string>)ism_SqlMap.QueryForList<string>("UserInfo.QueryUserName", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryInspectDoctor(string strMethodName)==" + e.ToString(), Module.DAO);
            }
            return lstInspectDoctor;
        }

        public List<PatientInfo> QueryPatientInfos(string strMethodName)
        {
            List<PatientInfo> lstPatientInfo = new List<PatientInfo>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());
                lstPatientInfo = (List<PatientInfo>)ism_SqlMap.QueryForList<PatientInfo>("WorkAreaApplyTask." + strMethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryPatientInfos(string strMethodName)==" + e.ToString(), Module.DAO);
            }

            return lstPatientInfo;
        }

        /// <summary>
        /// 常规任务：查询所有数据审核是否通过
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="sampleInfo"></param>
        /// <param name="strFilter"></param>
        /// <returns></returns>
        public List<SampleInfoForResult> QueryCommonSampleData(string strMethodName, SampleInfoForResult sampleInfo, string strFilter)
        {
            List<SampleInfoForResult> lstSampleInfo = new List<SampleInfoForResult>();
            try
            {
                Hashtable ht = new Hashtable();
                string[] strFilters = strFilter.Split(',');
                Dictionary<string, bool> dctFilter = new Dictionary<string,bool>();
                foreach (string str in strFilters)
                {
                    dctFilter.Add(str.Substring(0, str.IndexOf(':')), System.Convert.ToBoolean(str.Substring(str.IndexOf(':') + 1)));
                }

                if (dctFilter["FilterSwitch"])
                {
                    if (dctFilter["Completed"])
                        ht.Add("Completed", 2);
                    else
                        ht.Add("Completed", -1);
                    if (dctFilter["Starting"])
                        ht.Add("Starting", 1);
                    else
                        ht.Add("Starting",-1);
                    if (dctFilter["NoStart"])
                        ht.Add("NoStart", 0);
                    else
                        ht.Add("NoStart", -1);
                }
                else
                {
                    ht.Add("Completed", 2);
                    ht.Add("Starting", 1);
                    ht.Add("NoStart", 0);
                }
                ht.Add("SampleNum", sampleInfo.SampleNum);
                ht.Add("PatientName", sampleInfo.PatientName);
                ht.Add("SampleID", sampleInfo.SampleID);
                ht.Add("StartTime", sampleInfo.StartTime.ToShortDateString());
                ht.Add("EndTime", sampleInfo.EndTime.ToShortDateString());
                string[] sampleTypes = sampleInfo.SampleType.Split(',');
                Dictionary<string, bool> dctsampleTypes = new Dictionary<string,bool>();
                foreach (string str in sampleTypes)
                {
                    dctsampleTypes.Add(str.Substring(0, str.IndexOf(':')), System.Convert.ToBoolean(str.Substring(str.IndexOf(':') + 1)));
                }

                string strSampleTypes = string.Empty;
                if (dctsampleTypes["常规样本"])
                    ht.Add("常规样本", 0);
                else
                    ht.Add("常规样本", -1);
                if (dctsampleTypes["急诊"])
                    ht.Add("急诊", 1);
                else
                    ht.Add("急诊", -1);

                lstSampleInfo = (List<SampleInfoForResult>)ism_SqlMap.QueryForList<SampleInfoForResult>("CommonDataCheck." + strMethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCommonSampleData(string strMethodName)==" + e.ToString(), Module.DAO);
            }

            return lstSampleInfo;
        }

        public List<SampleResultInfo> QueryProjectResultBySampleNum(string strMethodName, string[] strConditions)
        {
            List<SampleResultInfo> lstSampleResultInfo = new List<SampleResultInfo>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", strConditions[0]);
                ht.Add("StartDateTime",strConditions[1]);
                ht.Add("SampleType", strConditions[2]);
                //ht.Add("StartDateTime", System.Convert.ToDateTime(strConditions[1]).Date);
                //ht.Add("EndDateTime", System.Convert.ToDateTime(strConditions[1]).AddDays(1).Date);

                lstSampleResultInfo = (List<SampleResultInfo>)ism_SqlMap.QueryForList<SampleResultInfo>("CommonDataCheck.QueryProjectResultBySampleNum", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProjectResultBySampleNum(string strMethodName, string[] strConditions)==" + e.ToString(), Module.DAO);
            }

            return lstSampleResultInfo;
        }

        public string DeleteCommonSampleBySampleNum(string strMethodName, string[] strConditions)
        {
            string strResult = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", strConditions[0]);
                ht.Add("CreateTime", strConditions[1]);
                int intRes = ism_SqlMap.Update("CommonDataCheck." + strMethodName, ht);

                ht.Add("TaskState", "4");
                ism_SqlMap.Update("CommonDataCheck.UpdateTaskStateBySampleNum", ht);

                if (intRes > 0)
                {
                    strResult = "删除成功！";
                }
                else
                    strResult = "删除失败！";
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteCommonSampleBySampleNum(string strMethodName, string[] strConditions)" + e.ToString(), Module.DAO);
                strResult = "删除失败！";
            }

            return strResult;
        }

        public string ReviewCheck(string strMethodName, string[] strConditions)
        {
            string strResult = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", strConditions[0]);
                ht.Add("ApplyTime", strConditions[1]);
                ht.Add("ProjectName", strConditions[2]);

                TaskInfo taskInfo = (TaskInfo)ism_SqlMap.QueryForObject("WorkAreaApplyTask.QueryCheckProjectTaskState", ht);
                if (taskInfo.TaskState == 2 && taskInfo.CreateDate > DateTime.Now.Date && taskInfo.CreateDate < DateTime.Now.AddDays(1).Date)
                {
                    taskInfo.TaskState = 0;
                    taskInfo.CreateDate = DateTime.Now;
                    ht.Add("IsResurvey", 1);
                    ism_SqlMap.Update("CommonDataCheck.UpdateIsResurvey", ht);
                    ism_SqlMap.Insert("WorkAreaApplyTask.AddTask", taskInfo);
                    ht.Add("SampleCreateTime", taskInfo.CreateDate);
                    ism_SqlMap.Insert("WorkAreaApplyTask.AddSampleResult", ht);
                    ht.Add("SampleState", 5);
                    ism_SqlMap.Update("WorkAreaApplyTask.UpdateSampleState", ht);
                }
                else
                {
                    strResult = "复查任务添加失败！";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ReviewCheck(string strMethodName, string[] strConditions)==" + e.ToString(), Module.DAO);
                strResult = "复查任务添加失败！";
            }

            return strResult;
        }

        public string AuditSampleTest(string strMethodName, string[] strConditions)
        {
            string strResult = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", strConditions[0]);
                ht.Add("ApplyTime", strConditions[1]);
                ht.Add("IsAudit",1);

                int intRes = ism_SqlMap.Update("CommonDataCheck." + strMethodName, ht);

                if (intRes > 0)
                {
                    strResult = "审核成功！";
                }
                else
                {
                    strResult = "审核失败！";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AuditSampleTest(string strMethodName, string[] strConditions)==" + e.ToString(), Module.DAO);
                strResult = "审核失败！";
            }

            return strResult;
        }

        public TimeCourseInfo QueryCommonTaskReaction(string strMethodName, SampleResultInfo sampleResInfo)
        {
            string sampleResultTCNO = "";
            TimeCourseInfo sampleReacInto = new TimeCourseInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", sampleResInfo.ProjectName);
                ht.Add("SampleNum", sampleResInfo.SampleNum);
                ht.Add("SampleType", sampleResInfo.SampleType);
                ht.Add("SampleCreateTime", sampleResInfo.SampleCreateTime);
                sampleResultTCNO = (string)ism_SqlMap.QueryForObject("PLCDataInfo.QuerySampleResultTCNO", ht);
                if (sampleResultTCNO != null && sampleResultTCNO != string.Empty)
                {
                    ht.Clear();
                    ht.Add("TimeCourseNO", sampleResultTCNO);
                    ht.Add("BeginTime", sampleResInfo.SampleCreateTime.ToShortDateString());
                    ht.Add("EndTime", sampleResInfo.SampleCreateTime.AddDays(1).ToShortDateString());
                    sampleReacInto = ism_SqlMap.QueryForObject("PLCDataInfo." + strMethodName, ht) as TimeCourseInfo;
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCommonTaskReaction(string strMethodName, SampleResultInfo sampleResInfo)==" + e.ToString(), Module.DAO);
            }

            return sampleReacInto;
        }

        public string BatchAuditSampleTest(string strMethodName, List<string[]> lstBatchAuditParam)
        {
            string strResult = "审核成功！";
            try
            {
                foreach (string[] param in lstBatchAuditParam)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("SampleNum", param[0]);
                    ht.Add("ApplyTime", param[1]);
                    ht.Add("IsAudit", 1);

                    int intRes = ism_SqlMap.Update("CommonDataCheck.AuditSampleTest", ht);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("BatchAuditSampleTest(string strMethodName, List<string[]> lstBatchAuditParam)==" + e.ToString(), Module.DAO);
                strResult = "审核失败！";
            }

            return strResult;
        }

        public string ConfirmCommonTask(string strMethodName, List<string[]> lstConfirmInfo)
        {
            string strResult = "确认成功！";
            try
            {
                foreach (string[] strs in lstConfirmInfo)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ProjectName", strs[1]);
                    ht.Add("SampleNum", strs[0]);
                    ht.Add("ApplyTime", strs[2]);
                    ht.Add("StartApplyTime", System.Convert.ToDateTime(strs[2]).Date);
                    ht.Add("EndApplyTime", System.Convert.ToDateTime(strs[2]).AddDays(1).Date);
                    ism_SqlMap.Update("CommonDataCheck.UnConfirmCommonTask", ht);
                    ism_SqlMap.Update("CommonDataCheck." + strMethodName, ht);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ConfirmCommonTask(string strMethodName, List<string[]> lstConfirmInfo)==" + e.ToString(), Module.DAO);
                strResult = "确认失败！";
            }

            return strResult;
        }

        public bool CalibCurveBeExistByProNameAndType(string strMethodName, string[] strParams)
        {
            bool bExist = false;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", strParams[0]);
                ht.Add("SampleType", strParams[1]);
                AssayProjectCalibrationParamInfo calParam = ism_SqlMap.QueryForObject("AssayProjectInfo.GetAssayProjectCalParamInfo", ht) as AssayProjectCalibrationParamInfo;
                if (calParam.CalibrationMethod != null)
                {
                    if (calParam.CalibrationMethod == "K系数法")
                    {
                        bExist = true;
                    }
                    else
                    {
                        int i = (int)ism_SqlMap.QueryForObject("WorkAreaApplyTask." + strMethodName, ht);

                        if (i > 0)
                            bExist = true;
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("CalibCurveBeExistByProNameAndType(string strMethodName, string[] strParams)==" + e.ToString(), Module.DAO);
            }

            return bExist;
        }
        /// <summary>
        /// 查询校准参数表
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="strParams"></param>
        /// <returns>校准方法名称</returns>
        public string CalibParamInfoByProNameAndType(string strMethodName, string[] strParams)
        {
            string calbMethod = null;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", strParams[0]);
                ht.Add("SampleType", strParams[1]);
                calbMethod = ism_SqlMap.QueryForObject("AssayProjectInfo." + strMethodName, ht) as string;
                     
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("" + e.ToString(), Module.DAO);
            }
            return calbMethod;
        }
        
        public SampleInfo QuerySampleInfoByPosAndPanel(string strMethodName, string[] paramInfos)
        {
            SampleInfo sampleInfo = new SampleInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("PanelNum", paramInfos[0]);
                ht.Add("SamplePos", paramInfos[1]);
                ht.Add("starttime", DateTime.Now.Date);
                ht.Add("endtime", DateTime.Now.AddDays(1).Date);
                sampleInfo = ism_SqlMap.QueryForObject("WorkAreaApplyTask." + strMethodName, ht) as SampleInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QuerySampleInfoByPosAndPanel(string strMethodName, string[] paramInfos)==" + e.ToString(), Module.DAO);
            }

            return sampleInfo;
        }

        public SampleInfo GetSample(int sampleNum, DateTime sampleCreateTime)
        {
            SampleInfo sampleInfo = new SampleInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("sampleNum", sampleNum);
                ht.Add("sampleCreateTime", sampleCreateTime);
                sampleInfo = ism_SqlMap.QueryForObject("WorkAreaApplyTask.GetSample", ht) as SampleInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSample(int sampleNum, DateTime sampleCreateTime)==" + e.ToString(), Module.DAO);
            }

            return sampleInfo;
        }

        public string GetSampleTaskDilutionType(SampleResultInfo samResultInfo)
        {
            string dilutionType = string.Empty;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", samResultInfo.SampleNum);
                ht.Add("SampleCreateTime", samResultInfo.SampleCreateTime);
                ht.Add("ProjectName", samResultInfo.ProjectName);
                ht.Add("SampleType", samResultInfo.SampleType);

                dilutionType = ism_SqlMap.QueryForObject("WorkAreaApplyTask.GetSampleTaskDilutionType", ht) as string;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSampleTaskDilutionType(SampleResultInfo samResultInfo)==" + e.ToString(), Module.DAO);
            }

            return dilutionType;
        }

        public void UpdateCurrentNORResult(SampleResultInfo samResultInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", samResultInfo.ProjectName);
                ht.Add("SampleType", samResultInfo.SampleType);
                ht.Add("SampleCreateTime", samResultInfo.SampleCreateTime);
                ht.Add("SampleNum", samResultInfo.SampleNum);
                ht.Add("AbsValue", samResultInfo.AbsValue);
                ht.Add("ConcResult", samResultInfo.ConcResult);

                ism_SqlMap.QueryForObject("WorkAreaApplyTask.UpdateCurrentNORResult", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCurrentNORResult(SampleResultInfo samResultInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateNorTaskState(string ProjectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", ProjectName);
                ht.Add("SampleType", sampleType);
                ism_SqlMap.Update("WorkAreaApplyTask.UpdateNorTaskState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateNorTaskState(string ProjectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateNORResultRunLog(SampleResultInfo samResInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", samResInfo.SampleNum);
                ht.Add("ProjectName", samResInfo.ProjectName);
                ht.Add("TCNO", samResInfo.TCNO);
                ht.Add("Remarks", samResInfo.Remarks);
                ht.Add("SampleCreateTime", samResInfo.SampleCreateTime);
                ism_SqlMap.Update("WorkAreaApplyTask.UpdateNORResultRunLog", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateNORResultRunLog(SampleResultInfo samResInfo)==" + e.ToString(), Module.DAO);
            }
        }
    }
}
