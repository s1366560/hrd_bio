using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class QCTask : DataTransmit
    {
        public QCTask()
        {

        }

        public List<QCTaskInfo> QueryBigestQCTaskInfoForToday(string strDBMethod)
        {
            return myBatis.QueryBigestQCTaskInfoForToday(strDBMethod);
        }
        //质控品编号
        int qcId = 0;

        public List<string[]> QueryProjectNameInfoByQC(string strDBMethod, QualityControlInfo qcInfo, string strSampleType)
        {
            List<string> lstProjectByQC = myBatis.QueryProjectNameInfoByQC(strDBMethod, qcInfo, strSampleType, out qcId);

            //判断该项目下的任务是否已完成
            int QCcount = myBatis.QueryQCTaskByProjectAndSamType("QueryQCTaskByProjectAndSamType", qcId);

            List<string[]> lstProjectInfo = new List<string[]>();

            foreach (string project in lstProjectByQC)
            {
                string[] projectInfo = new string[6];
                projectInfo[0] = project;
                AssayProjectParamInfo assayProParam = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = project, SampleType = strSampleType });
                ReagentStateInfoR1R2 reagentState = myBatis.QueryReagentStateInfoByProjectName("QueryReagentStateInfoByProjectName", new ReagentSettingsInfo() { ProjectName = project, ReagentType = strSampleType });
                // 3.判断校准曲线是否可用 
                bool bExist = myBatis.CalibCurveBeExistByProNameAndType("CalibCurveBeExistByProNameAndType", new string[] { project, strSampleType });
                
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
                    else if (reagentState.ReagentName == "" && reagentState.ReagentName == null && reagentState.ValidPercent < 3)
                    {
                        projectInfo[3] = "此项目对应的试剂1余量不足！";
                    }
                    else if (reagentState.ReagentName2 == "" && reagentState.ReagentName2 == null && reagentState.ValidPercent2 < 3)
                    {
                        projectInfo[3] = "此项目对应的试剂2余量不足！";
                    }
                    else if (reagentState.Locked == true)
                    {
                        projectInfo[3] = "该项目对应试剂被锁定，无法使用！";
                    }

                    if (bExist == false)
                    {
                        projectInfo[4] = "该项目没有对应的较准曲线";
                    }
                    if (QCcount != 0)
                    {
                        projectInfo[5] = "此项目已下任务,请做完此项目任务后才能继续下该项目任务！";
                    }
                    if (projectInfo[2] == null && projectInfo[3] == null && projectInfo[4] == null && projectInfo[5]== null)
                    {
                        projectInfo[1] = "true";
                    }
                    else
                    {
                        projectInfo[1] = "false";
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

                }
                lstProjectInfo.Add(projectInfo);
            }
            return lstProjectInfo;
        }

        public List<string> QueryAssayProNameAllInfo(string strDBMethod, string sampleType)
        {
            return myBatis.QueryAssayProNameAllInfo(strDBMethod, sampleType);
        }            

        public List<string> QueryCombProjectNameAllInfo(string strDBMethod)
        {
            List<string> lstCombProjectName = new List<string>();
            List<CombProjectInfo> lstCombProInfo = myBatis.QueryCombProjectNameAllInfo(strDBMethod);

            foreach (CombProjectInfo combProjectInfo in lstCombProInfo)
            {
                lstCombProjectName.Add(combProjectInfo.CombProjectName);
            }

            return lstCombProjectName;
        }

        public string AddQCTask(string strDBMethod, List<QCTaskInfo> lstQCTaskInfos)
        {
            return myBatis.AddQCTask(strDBMethod, lstQCTaskInfos);
        }

        public List<QCTaskInfo> QueryQCTaskForLstv(string strDBMethod)
        {
            return myBatis.QueryQCTaskForLstv(strDBMethod);
        }

        public QCTaskInfoQuery QueryNextQCTaskBySampleNum(string strDBMethod, string SampleNum)
        {
            return myBatis.QueryNextQCTaskBySampleNum(strDBMethod, SampleNum);
        }

        public List<QualityControlInfo> QueryQCAllInfoForUnLocked(string strDBMethod)
        {
            return myBatis.QueryQCAllInfoForUnLocked(strDBMethod);
        }

        //public void InitMachineUpdateQCTaskState(string strDBMethod)
        //{
        //     myBatis.InitMachineUpdateQCTaskState(strDBMethod);
        //}
    }
}
