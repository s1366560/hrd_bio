using BioA.Common;
using BioA.Common.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    /// <summary>
    /// 工作区后台处理
    /// </summary>
    public class WorkAreaApplyTask : DataTransmit
    {
        public int QueryMaxSampleNum(string StrmethodName)
        {
            return myBatis.QueryMaxSampleNum(StrmethodName);
        }

        public List<string[]> QueryProNameForApplyTask(string StrmethodName, string sampleType)
        {
            List<string[]> lstProjectsInfo = new List<string[]>();

            List<string> lstProjects = myBatis.QueryProNameForApplyTask(StrmethodName, sampleType);

            foreach (string project in lstProjects)
            {
                string[] projectInfo = new string[5];
                projectInfo[0] = project;
                // 1.判断项目参数是否有效
                AssayProjectParamInfo assayProParam = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = project, SampleType = sampleType });
                // 2.判断试剂是否存在
                ReagentStateInfoR1R2 reagentState = myBatis.QueryReagentStateInfoByProjectName("QueryReagentStateInfoByProjectName", new ReagentSettingsInfo() { ProjectName = project, ReagentType = sampleType });
                // 3.判断校准曲线是否可用
                bool bExist = myBatis.CalibCurveBeExistByProNameAndType("CalibCurveBeExistByProNameAndType", new string[]{ project, sampleType });
                if (assayProParam != null)
                {
                    if (assayProParam.AnalysisMethod == "" || assayProParam.AnalysisMethod == null)
                    {
                        projectInfo[2] = "该项目参数录入有误！";
                    }

                    if (reagentState == null)
                    {
                        projectInfo[3] = "该项目没有对应试剂！";
                    }
                    else if (reagentState.Locked == true)
                    {
                        projectInfo[3] = "该项目对应试剂被锁定，无法使用！";
                    }
                    else if (reagentState.ReagentName != "" && reagentState.ReagentName != null && reagentState.ValidPercent < 5)
                    {
                        projectInfo[3] = "此项目对应的试剂1余量不足！";
                    }
                    else if (reagentState.ReagentName2 != "" && reagentState.ReagentName2 != null && reagentState.ValidPercent2 < 5)
                    {
                        projectInfo[3] = "此项目对应的试剂2余量不足！";
                    }

                    if (bExist == false)
                    {
                        projectInfo[4] = "该项目没有对应的较准曲线";
                    }


                    if (projectInfo[2] != null || projectInfo[3] != null || projectInfo[4] != null)
                    {
                        projectInfo[1] = "false";                        
                    }
                    else
                    {
                        projectInfo[1] = "true";
                    }

                }
                else
                {
                    projectInfo[1] = "false";
                    projectInfo[2] = "该项目参数录入有误！";
                    projectInfo[4] = "该项目没有对应的较准曲线";
                    if (reagentState == null)
                    {
                        projectInfo[3] = "该项目没有对应试剂！";
                    }
                    else if (reagentState.Locked == true)
                    {
                        projectInfo[3] = "该项目对应试剂被锁定，无法使用！";
                    }
                    else if (reagentState.ReagentName != "" && reagentState.ValidPercent < 5)
                    {
                        projectInfo[3] = "此项目对应的试剂1余量不足！";
                    }
                    else if (reagentState.ReagentName2 != "" && reagentState.ValidPercent2 < 5)
                    {
                        projectInfo[3] = "此项目对应的试剂2余量不足！";
                    }

                }
                lstProjectsInfo.Add(projectInfo);
            }
            return lstProjectsInfo;
        }

        /// <summary>
        /// 获取组合项目所有信息
        /// </summary>
        /// <param name="StrmethodName"></param>
        /// <returns></returns>
        public List<string> QueryCombProjectNameAllInfo(string StrmethodName)
        {
            List<string> lstCombProjectName = new List<string>();
            List<CombProjectInfo> lstCombProInfo = myBatis.QueryCombProjectNameAllInfo(StrmethodName);

            foreach (CombProjectInfo combProjectInfo in lstCombProInfo)
            {
                lstCombProjectName.Add(combProjectInfo.CombProjectName);
            }

            return lstCombProjectName;
        }

        public List<SampleInfo> QueryApplyTaskLsvt(string StrmethodName)
        {
            return myBatis.QueryApplyTaskLsvt(StrmethodName);
        }
        /// <summary>
        /// 保存定标任务/普通任务、急诊任务
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="sampleInfo"></param>
        /// <param name="lstSampleInfo"></param>
        /// <returns></returns>
        public string AddTask(string strMethodName, SampleInfo sampleInfo, List<TaskInfo> lstSampleInfo)
        {
            return myBatis.AddTask(strMethodName, sampleInfo, lstSampleInfo);
        }

        public List<TaskInfo> QueryTaskInfoBySampleNum(string strMethodName, string SampleNum)
        {
            return myBatis.QueryTaskInfoBySampleNum(strMethodName, SampleNum);
        }

        public PatientInfo QueryPatientInfoBySampleNum(string strMethodName, int sampleNum)
        {
            return myBatis.QueryPatientInfoBySampleNum(strMethodName, sampleNum);
        }

        public string UpdatePatientInfo(string strMethodName, PatientInfo patientInfo)
        {
            return myBatis.UpdatePatientInfo(strMethodName, patientInfo);
        }

        public List<string> QueryDepartmentInfo(string strMethodName)
        {
            return myBatis.QueryDepartmentInfo(strMethodName);
        }

        public List<string> QueryApplyDoctor(string strMethodName)
        {
            return myBatis.QueryApplyDoctor(strMethodName);
        }

        public List<string> QueryCheckDoctor(string strMethodName)
        {
            return myBatis.QueryCheckDoctor(strMethodName);
        }

        public List<string> QueryInspectDoctor(string strMethodName)
        {
            return myBatis.QueryInspectDoctor(strMethodName);
        }

        public List<PatientInfo> QueryPatientInfos(string strMethodName)
        {
            return myBatis.QueryPatientInfos(strMethodName);
        }

        public TaskInfoForSamplePanelInfo QueryTaskInfoForSamplePanel(string strMethodName, string[] paramInfos)
        {
            // 1.获取样本编号、样本状态、样本类型
            SampleInfo sampleInfo = myBatis.QuerySampleInfoByPosAndPanel("QuerySampleInfoByPosAndPanel", paramInfos);
            // 2.获取检测项目信息
            List<SampleResultInfo> lstSampleResultInfo = new List<SampleResultInfo>();
            lstSampleResultInfo = myBatis.QueryProjectResultBySampleNum(strMethodName, new string[]{sampleInfo.SampleNum.ToString(), sampleInfo.CreateTime.ToShortDateString()});


            TaskInfoForSamplePanelInfo taskInfoForSamPanel = new TaskInfoForSamplePanelInfo();
            taskInfoForSamPanel.SampleNum = sampleInfo.SampleNum;
            taskInfoForSamPanel.SampleState = sampleInfo.SampleState;
            taskInfoForSamPanel.SampleType = sampleInfo.SampleType;
            taskInfoForSamPanel.SamplePos = string.Format("{0}-{1}", sampleInfo.PanelNum, sampleInfo.SamplePos);
            

            List<SampleResultInfo> lstTaskResInfos = new List<SampleResultInfo>();
            foreach (SampleResultInfo taskInfo in lstSampleResultInfo)
            {
                SampleResultInfo s = new SampleResultInfo();

                s.ConcResult = taskInfo.ConcResult;
                s.ProjectName = taskInfo.ProjectName;

                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", taskInfo.SampleNum);
                ht.Add("DateTime", taskInfo.SampleCreateTime);
                ht.Add("ProjectName", taskInfo.ProjectName);
                ht.Add("SampleType", sampleInfo.SampleType);

                s.UnitAndRange = myBatis.QueryUnitAndRangeByProject("QueryUnitAndRangeByProject", ht);
                lstTaskResInfos.Add(s);
            }
            taskInfoForSamPanel.InspectInfos = XmlUtility.Serializer(typeof(List<SampleResultInfo>), lstTaskResInfos);
            return taskInfoForSamPanel;
        }

        public List<SampleInfo> QuerySamplePanelState(string strMethodName, string Panel)
        {
            return myBatis.QuerySamplePanelState(strMethodName, Panel);
        }

        public void UpdateRunningTaskWorDisk(string strMethodName, string panelNum)
        {
            myBatis.UpdateRunningTaskWorDisk(strMethodName, panelNum);
        }
        /// <summary>
        /// 删除选中的样本结果
        /// </summary>
        /// <param name="sampleResultInfo"></param>
        /// <returns></returns>
        public int DeleteSampleResult(List<SampleResultInfo> sampleResultInfo)
        {
            return myBatis.DeleteSampleResult(sampleResultInfo);
        }
        /// <summary>
        /// 获取结果设置表信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<ResultSetInfo> QueryResultSetInfos(string strDBMethod)
        {
            return myBatis.QueryResultSetInfos(strDBMethod);
        }
    }
}
