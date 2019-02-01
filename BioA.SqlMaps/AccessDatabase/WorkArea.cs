using BioA.Common;
using BioA.Common.IO;
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
                ht.Add("starttime", DateTime.Now.Date);
                ht.Add("endtime", DateTime.Now.AddDays(1).Date);
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
            List<int> lstInt = new List<int>();
            List<string> lstProjectNames = new List<string>();
            List<string> lstNotSortProjectName = new List<string>();
            try
            {
                lstProName = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo.ProjectPageinfoBySampleType", sampleType);
                foreach (string projectName in lstProName)
                {
                    int s = projectName.IndexOf('.');
                    if (s < 0)
                    {
                        lstNotSortProjectName.Add(projectName);
                        continue;
                    }
                    lstInt.Add(Convert.ToInt32(projectName.Substring(0, s)));
                }
                lstInt.Sort();
                foreach (int i in lstInt)
                {
                    foreach (string proName in lstProName)
                    {
                        int s = proName.IndexOf('.');
                        if (s < 0)
                        {
                            continue;
                        }
                        if (i == Convert.ToInt32(proName.Substring(0, s)))
                        {
                            lstProjectNames.Add(proName);
                        }
                    }
                }
                lstProjectNames.AddRange(lstNotSortProjectName);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProNameForApplyTask(string StrmethodName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return lstProjectNames;
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
                ht.Add("starttime", DateTime.Now.Date);
                ht.Add("endtime", DateTime.Now.AddDays(1).Date);

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
                ht.Add("PanelNum", sampleInfo.PanelNum);
                ht.Add("starttime", DateTime.Now.Date);
                ht.Add("endtime", DateTime.Now.AddDays(1).Date);                

                int count = (int)ism_SqlMap.QueryForObject("WorkAreaApplyTask.QuerySampleCountByNumber", ht);
                if (count > 0)
                {
                    strResult = "此样本任务已经存在，请重新录入！";
                }
                else
                {
                    //保存样本信息表
                    ism_SqlMap.Insert("WorkAreaApplyTask.AddSample", sampleInfo);
                    foreach (TaskInfo t in lstSampleInfo)
                    {
                        //保存任务
                        ism_SqlMap.Insert("WorkAreaApplyTask.AddTask", t);

                        //保存样本结果表 2018 9/3
                        //ht.Clear();
                        //ht.Add("SampleNum", t.SampleNum);
                        //ht.Add("SampleCreateTime", t.CreateDate);
                        //ht.Add("ProjectName", t.ProjectName);
                        //ht.Add("SampleType", t.SampleType);
                        //ism_SqlMap.Insert("WorkAreaApplyTask.AddSampleResult", ht);
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
        /// <summary>
        /// 批量录入普通/急诊任务信息
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="lstObj"></param>
        /// <returns></returns>
        public List<string> BatchAddTask(string strMethodName, object[] lstObj)
        {
            string strResult = string.Empty;
            List<string> lstReslut = new List<string>();
            for (int i = 0; i < lstObj.Length; i++)
            {
                object[] obj = lstObj[i] as object[];
                SampleInfo sampleForBatch =  obj[0] as SampleInfo;
                strResult = AddTask(strMethodName, sampleForBatch, obj[1] as List<TaskInfo>);
                lstReslut.Add("盘号：" + sampleForBatch.PanelNum + "样本号：" + sampleForBatch.SampleNum + "~ 位置：" + sampleForBatch.SamplePos + "      " + strResult);
            }
            return lstReslut;
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
        /// <summary>
        /// 获取普通任务
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="panelNum"></param>
        /// <returns></returns>
        public List<TaskInfo> GetTaskInfo(string strMethodName, int panelNum)
        {
            List<TaskInfo> lstTaskInfos = new List<TaskInfo>();
            try
            {
                List<TaskInfo> lstTaskInfo = (List<TaskInfo>)ism_SqlMap.QueryForList<TaskInfo>("WorkAreaApplyTask." + strMethodName, string.Format("select t.*,s.Barcode from tasktb t,sampletb s where t.SampleNum = s.SampleNum and t.CreateDate = s.CreateTime and s.PanelNum = {0} and t.CreateDate between '{1}' and '{2}'", panelNum, DateTime.Now.Date, DateTime.Now.Date.AddDays(1)));
                foreach (TaskInfo task in lstTaskInfo)
                {
                    if (lstTaskInfos.Count > 0)
                    {
                        TaskInfo ta = lstTaskInfos.SingleOrDefault(x => x.SampleNum == task.SampleNum );
                        if (ta != null)
                        {
                            lstTaskInfos.RemoveAll(s => s.SampleNum == ta.SampleNum);
                            ta.ProjectName = ta.ProjectName+"," + task.ProjectName;
                            lstTaskInfos.Add(ta);
                        }
                        else
                            lstTaskInfos.Add(task);
                    }
                    else
                        lstTaskInfos.Add(task);
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetTaskInfo(string strMethodName, int panelNum) ==" + ex.ToString(), Module.WorkingArea);
            }
            return lstTaskInfos;
        }
        /// <summary>
        /// 获取生化任务信息
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public TaskInfo GetTask(string projectName, int sampleNum)
        {
            TaskInfo tasks = null;
            try
            {
                //获取生化任务信息
                tasks = (TaskInfo)ism_SqlMap.QueryForObject("WorkAreaApplyTask.GetTaskInfo", string.Format("select * from tasktb where ProjectName = '{0}' and SampleNum = {1} and TaskState < 2", projectName, sampleNum));
                if (tasks == null)
                {
                    //删除生化任务信息
                    ism_SqlMap.QueryForObject("WorkAreaApplyTask.NoReturnValueGeneralID", string.Format("delete from tasktb where ProjectName = '{0}' and SampleNum = {1} and TaskState = 2", projectName, sampleNum));
                    //修改样本信息状态
                    ism_SqlMap.QueryForObject("WorkAreaApplyTask.NoReturnValueGeneralID", string.Format("update tasktb set SampleState = 0 where SampleNum = {0} and CreateTime > '{1}'", sampleNum, DateTime.Now.Date));
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetTask(string projectName, string sampleType,string sampleNum) == " + ex.ToString(), Module.WorkingArea);
            }
            return tasks;
        }
        /// <summary>
        /// 保存重测的生化项目任务
        /// </summary>
        /// <param name="t"></param>
        public void SaveTske(TaskInfo t)
        {
            try
            {
                ism_SqlMap.Insert("WorkAreaApplyTask.NoReturnValueGeneralID", string.Format("insert into TaskTb (SampleNum, CreateDate, ProjectName, SampleType, SampleDilute, DilutedRatio, InspectTimes, SendTimes, FinishTimes, TaskState,IsReRun)values({0}, '{1}', '{2}', '{3}', '{4}', {5}, {6}, 0, 0, 0, {7})", t.SampleNum, t.CreateDate, t.ProjectName, t.SampleType, t.SampleDilute, t.DilutedRatio, t.InspectTimes, t.IsReRun));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("SaveTske(TaskInfo t) ==" + ex.ToString(), Module.WorkingArea);
            }
        }

        /// <summary>
        /// 清除普通任务和样本信息
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="lstTask"></param>
        /// <returns></returns>
        public string DeleteTaskAndSampleInfo(string strMethodName, List<TaskInfo> lstTask)
        {
            string result = null;
            int selectResult = 0;
            string success = null;
            try
            {
                foreach (TaskInfo task in lstTask)
                {
                    selectResult = (int)ism_SqlMap.QueryForObject("WorkAreaApplyTask." + strMethodName, string.Format("select count(*) from tasktb where SampleNum = {0} and CONVERT(varchar(50),CreateDate, 120) like '%{1}%' and TaskState != 0", task.SampleNum, task.CreateDate.ToString("yyyy-MM-dd")));
                    if (selectResult > 0)
                    {
                        result += task.SampleNum +",";
                    }
                    else
                    {
                        ism_SqlMap.Delete("WorkAreaApplyTask." + strMethodName, string.Format("delete s from sampletb s where s.SampleNum = {0} and CONVERT(varchar(50),s.CreateTime, 120) like '%{1}%'", task.SampleNum, task.CreateDate.ToString("yyyy-MM-dd")));
                        ism_SqlMap.Delete("WorkAreaApplyTask." + strMethodName, string.Format("delete from tasktb where SampleNum = {0} and CONVERT(varchar(50),CreateDate, 120) like '%{1}%' and TaskState = 0", task.SampleNum, task.CreateDate.ToString("yyyy-MM-dd")));
                        success += task.SampleNum +",";
                        
                    }
                }
                if (result == null)
                    result = "1";
                else if (success != null)
                    result = "2"+success.Remove(success.LastIndexOf(","),1);
                else
                    result = "3" + ":所选取的样本对应项目已完成或者正在测试中，不能清除！";
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("DeleteTaskAndSampleInfo(string strMethodName, List<TaskInfo> lstTask) ==" + ex.ToString(), Module.WorkingArea);
            }
            return result;
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
        /// <summary>
        /// 获取所有部门信息
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <returns></returns>
        public List<string> QueryDepartmentInfo(string strMethodName)
        {
            List<string> lstApplyDepartment = new List<string>();
            try
            {
                lstApplyDepartment = (List<string>)ism_SqlMap.QueryForList<string>("DepartmentInfo." + strMethodName, null);
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
        /// 常规任务：查询所有任务结果是否通过
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
                ht.Add("StartTime", sampleInfo.StartTime.Date);
                ht.Add("EndTime", sampleInfo.EndTime.Date);
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

                lstSampleResultInfo = (List<SampleResultInfo>)ism_SqlMap.QueryForList<SampleResultInfo>("CommonDataCheck." + strMethodName, ht);
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
        /// <summary>
        /// 获取普通任务结果（每个比色杯43个点的反应进程结果）
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="sampleResInfo"></param>
        /// <returns></returns>
        public TimeCourseInfo QueryCommonTaskReaction(string strMethodName, SampleResultInfo sampleResInfo)
        {
            //string sampleResultTCNO = "";
            TimeCourseInfo timeCourseInfoResult = null;
            try
            {
                //获取 TimeCourseTb 43个点进程数据
                timeCourseInfoResult = ism_SqlMap.QueryForObject("PLCDataInfo." + strMethodName,
                    string.Format("select * from TimeCourseTb where TimeCourseNO='{0}' and CONVERT(varchar(50),DrawDate, 120) like '%{1}%'", sampleResInfo.TCNO, sampleResInfo.SampleCreateTime.ToString("yyyy-MM-dd"))) as TimeCourseInfo;
                if (timeCourseInfoResult != null)
                {
                    return timeCourseInfoResult;
                }
                //如果上面没有获取到数据就去 timecourseBackUptb 获取43个点进程数据
                timeCourseInfoResult = ism_SqlMap.QueryForObject("PLCDataInfo." + strMethodName,
                    string.Format("select * from timecourseBackUptb where TimeCourseNO='{0}' and CONVERT(varchar(50),DrawDate, 120) like '%{1}%'", sampleResInfo.TCNO, sampleResInfo.SampleCreateTime.ToString("yyyy-MM-dd"))) as TimeCourseInfo;
                if (timeCourseInfoResult != null)
                {
                    return timeCourseInfoResult;
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCommonTaskReaction(string strMethodName, SampleResultInfo sampleResInfo)==" + e.ToString(), Module.DAO);
            }

            return null;
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
        /// <summary>
        /// 每完成一个任务就修改结果结果表吸光度值、浓度值、完成时间、完成状态
        /// </summary>
        /// <param name="samResultInfo"></param>
        public void UpdateCurrentNORResult(SampleResultInfo samResultInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                //修改样本结果条件不需要这么多。 2018/9/3
                //ht.Add("ProjectName", samResultInfo.ProjectName);
                //ht.Add("SampleType", samResultInfo.SampleType);
                ht.Add("TCNO", samResultInfo.TCNO);
                ht.Add("SampleCreateTime", samResultInfo.SampleCreateTime);
                //ht.Add("SampleNum", samResultInfo.SampleNum);
                ht.Add("AbsValue", samResultInfo.AbsValue);
                ht.Add("ConcResult", samResultInfo.ConcResult);
                ht.Add("SampleCompletionStatus", TaskState.SUCC);
                ht.Add("SampleCompletionTime", DateTime.Now.ToString());

                ism_SqlMap.Update("WorkAreaApplyTask.UpdateCurrentNORResult", ht);
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

        /// <summary>
        /// 删除选中的样本结果信息
        /// </summary>
        /// <param name="sampleResultInfo"></param>
        /// <returns></returns>
        public int DeleteSampleResult(List<SampleResultInfo> sampleResultInfo)
        {
            int result = 0;
            //Hashtable hashtable = new Hashtable();
            string str = "";
            for (int i = 1; i <= sampleResultInfo.Count; i++)
            {
                if (i == sampleResultInfo.Count)
                {
                    str += sampleResultInfo[i - 1].TCNO;
                }
                else
                {

                    str += (sampleResultInfo[i - 1].TCNO + ",");
                }
            }
            try
            {
                string strSql = string.Format("delete from sampleresulttb where ProjectName='{0}' and TCNO in ({1})", sampleResultInfo[0].ProjectName, str);
                //hashtable.Add("TCNO", item.TCNO);
                result = ism_SqlMap.Delete("WorkAreaApplyTask.DeleteSampleResultInfo", strSql);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("DeleteSampleResult(List<SampleResultInfo> sampleResultInfo) ==" + ex.ToString(), Module.PLCData);
            }
            return result;
        }

        /// <summary>
        /// 获取病人信息、样本信息、样本结果信息
        /// </summary>
        /// <param name="samp"></param>
        /// <param name="dateTime"></param>
        /// <param name="samplePatientInfo"></param>
        /// <returns></returns>
        public List<SampleResultInfo> GetSmpPrintValues(string samp, DateTime dateTime, out SampleInfoForResult samplePatientInfo)
        {
            List<SampleResultInfo> lstSample = null;
            SampleInfoForResult sampleInfoForResult = null;
            Hashtable hashtable = new Hashtable();
            hashtable.Add("SampleNum", samp);
            hashtable.Add("CreateTime", dateTime);
            try
            {
                //获取样本信息和病人信息
                sampleInfoForResult = (SampleInfoForResult)ism_SqlMap.QueryForObject("CommonDataCheck.GetSamplePatientInfo", hashtable);
                //获取样本结果信息
                lstSample = (List<SampleResultInfo>)ism_SqlMap.QueryForList<SampleResultInfo>("CommonDataCheck.GetSampleResultInfo", hashtable);
                //根据项目名称和样本类型移除该集合中相同的元素
                if (lstSample != null)
                {
                    List<SampleResultInfo> resultInfos = lstSample.Where((x, i) => lstSample.FindIndex(y => y.ProjectName == x.ProjectName && y.SampleType == x.SampleType) == i).ToList();
                    foreach (var item in resultInfos)
                    {
                        hashtable.Clear();
                        hashtable.Add("ProjectName", item.ProjectName);
                        hashtable.Add("SampleType", item.SampleType);
                        //获取项目的中文名称
                        string chineseName = ism_SqlMap.QueryForObject("AssayProjectInfo.GetProjectFullName", hashtable) as string;
                        //获取单位信息
                        string unit = ism_SqlMap.QueryForObject("AssayProjectInfo.GetProjectUnitInfo", hashtable) as string;
                        foreach (var s in lstSample)
                        {
                            if (s.ProjectName == item.ProjectName && s.SampleType == item.SampleType && s.SampleCreateTime == item.SampleCreateTime)
                            {
                                s.ChineseName = chineseName;
                                s.UnitAndRange = unit;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetSmpPrintValues(string samp, DateTime dateTime, out SampleInfoForResult samplePatientInfo) ==" + ex.ToString(), Module.PLCData);
                samplePatientInfo = null;
                return null;
            }
            samplePatientInfo = sampleInfoForResult;
            return lstSample;
        }
        /// <summary>
        /// 获取结果设置表信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<ResultSetInfo> QueryResultSetInfos(string strDBMethod)
        {
            List<ResultSetInfo> lstResultSetInfo = new List<ResultSetInfo>();
            try
            {
                lstResultSetInfo = (List<ResultSetInfo>)ism_SqlMap.QueryForList<ResultSetInfo>("AssayProjectInfo." + strDBMethod, null);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("List<ResultSetInfo> QueryResultSetInfos(string strDBMethod) == "+ ex.ToString(), Module.WorkingArea);
            }
            return lstResultSetInfo;
        }
    }
}
