using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class Calibrator : DataTransmit
    {

        public List<Calibratorinfo> QueryCalibratorinfo(string strDBMethod, string p2)
        {
            List<Calibratorinfo> lstCalibratorinfo = new List<Calibratorinfo>();

            lstCalibratorinfo = myBatis.QueryCalibratorinfo(strDBMethod, null);

            return lstCalibratorinfo;
        }

        public List<CalibratorProjectinfo> QueryCalibratorProjectinfo(string strDBMethod, string p2)
        {
            List<CalibratorProjectinfo> lstCalibratorProjectinfo = new List<CalibratorProjectinfo>();

            lstCalibratorProjectinfo = myBatis.QueryCalibratorProjectinfo(strDBMethod, p2);

            return lstCalibratorProjectinfo;
        }

        public List<CalibratorProjectinfo> QueryProjectItemsByCalibration(string strDBMethod, string p2)
        {
            List<CalibratorProjectinfo> lstCalibratorProjectinfo1 = new List<CalibratorProjectinfo>();
         
            lstCalibratorProjectinfo1 = myBatis.QueryProjectItemsByCalibration(strDBMethod, p2);

     
            return lstCalibratorProjectinfo1;
        }

        public List<Calibratorinfo> QueryCalibPos(string strDBMethod, string p2)
        {
            List<Calibratorinfo> lstCalibratorinfo = new List<Calibratorinfo>();         
            lstCalibratorinfo = myBatis.QueryCalibPos(strDBMethod, null);
       
            return lstCalibratorinfo;
        }

        public string AddCalibratorinfo(string strDBMethod, Calibratorinfo dataConfig, List<CalibratorProjectinfo> dataConfig1)
        {
            return myBatis.AddCalibratorinfo(strDBMethod, dataConfig, dataConfig1);
        }

        /// <summary>
        ///     校准品维护界面:
        ///         删除校准品/项目信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库名</param>
        /// <param name="p2">参数</param>
        public string DeleteCalibrationMaintain(string strDBMethod, List<CalibratorProjectinfo> lstCalibProjectInfo)
        {
            return myBatis.DeleteCalibrationMaintain(strDBMethod, lstCalibProjectInfo);
        }

        public string EditCalibratorinfo(string strDBMethod, Calibratorinfo newCalibratorinfo, Calibratorinfo oldCalibratorinfo, List<CalibratorProjectinfo> lisNewCalibratorProjectinfo, List<CalibratorProjectinfo> lisOldCalibratorProjectinfo)
        {
            //myBatis.DeleteCalibratorProjectinfo("DeleteCalibratorProjectinfo", p2);
            return myBatis.EditCalibratorinfo(strDBMethod, newCalibratorinfo, oldCalibratorinfo, lisNewCalibratorProjectinfo, lisOldCalibratorProjectinfo);
        }

        /// <summary>
        /// 获取所有的校准结果信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public List<CalibrationResultinfo> QueryCalibrationState(string strDBMethod, string p2)
        {
            List<CalibrationResultinfo> lstCalibrationResultinfo = new List<CalibrationResultinfo>();
         
            lstCalibrationResultinfo = myBatis.QueryCalibrationState(strDBMethod, p2);

            return lstCalibrationResultinfo;
        }

        public List<SDTTableItem> QueryCalibrationCurveInfo(string strDBMethod, CalibrationCurveInfo calibrationCurveInfo)
        {
            return myBatis.QueryCalibrationCurveInfo(strDBMethod, calibrationCurveInfo);
        }

        /// <summary>
        /// 根据样本类型和项目名称查询校准参数信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库名</param>
        /// <param name="CalibratorinfoTask">参数</param>
        /// <returns>返回校准任务信息</returns>
        public List<CalibratorinfoTask> QueryListCalibrationCurveInfo(string strDBMethod, List<CalibratorinfoTask> CalibratorinfoTask)
        {
            List<CalibratorinfoTask> lstCalibrationResultinfo = new List<CalibratorinfoTask>();

            return lstCalibrationResultinfo = myBatis.QueryListCalibrationCurveInfo(strDBMethod, CalibratorinfoTask);

            //int intSampleNum = myBatis.QueryBigestCalibCTaskInfoForToday("QueryBigestCalibCTaskInfoForToday");
       
             
        }

        /// <summary>
        /// 获取所有项目 
        ///     对所有项目进行校验，如果校验通过字体就显示为黑色，不通过就显示为橙色（提示警告信息），或者该项目没有对应的校准品，就显示灰色（不可用）
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public List<string[]> QueryProjectNameInfoByCalib(string strDBMethod, string sampleType)
        {
            List<string> lstProjectByCalib = myBatis.QueryProjectNameInfoByCalib(strDBMethod, sampleType);
            List<string[]> lstProjectName = new List<string[]>();
            foreach (string project in lstProjectByCalib)
            {
                string[] projectInfo = new string[6];
                projectInfo[0] = project;
                //1.项目参数信息
                AssayProjectParamInfo assayProParam = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = project, SampleType = sampleType });
                //2.校准品信息
                List<CalibratorProjectinfo> calib = myBatis.QueryCalibProjectInfo("QueryCalibProjectInfo", new CalibratorProjectinfo() { ProjectName = project, SampleType = sampleType });
                //3.试剂信息
                ReagentStateInfoR1R2 reagentState = myBatis.QueryReagentStateInfoByProjectName("QueryReagentStateInfoByProjectName", new ReagentSettingsInfo() { ProjectName = project, ReagentType = sampleType });
                // 4.判断校准曲线是否可用 
                string calibMethod = myBatis.CalibParamInfoByProNameAndType("CalibParamInfoByProNameAndType", new string[] { project, sampleType });
                //5.判断该项目下的任务是否已完成
                int calibTaskCout = myBatis.QueryCalibTaskByProjectAndSamType("QueryCalibTaskByProjectAndSamType", new CalibratorinfoTask() { ProjectName = project, SampleType = sampleType });
                //5. 判断该项校准方法中是否有校准品为空
                string strResult = myBatis.CalibProParamInfo_CalibNameIsEmpty(project, sampleType);
                if (calib.Count == 0)
                {
                    projectInfo[2] = "此项目没有对应的校准品！";
                    lstProjectName.Add(projectInfo);
                }
                else
                {
                    if (assayProParam != null)
                    {
                        if (assayProParam.AnalysisMethod == "" || assayProParam.AnalysisMethod == null)
                        {
                            projectInfo[2] = "此项目参数录入有误！";
                        }

                        if (reagentState == null)
                        {
                            projectInfo[3] = "此项目没有对应试剂！";
                        }
                        else if (reagentState.Locked == true)
                        {
                            projectInfo[3] = "此项目对应试剂被锁定，无法使用！";
                        }
                        else if (reagentState.ReagentName != null && reagentState.ReagentName != "" && reagentState.ValidPercent < 5)
                        {
                            projectInfo[3] = "此项目对应的试剂1余量不足！";
                        }
                        else if (reagentState.ReagentName2 != null && reagentState.ReagentName2 != "" && reagentState.ValidPercent2 < 5)
                        {
                            projectInfo[3] = "此项目对应的试剂2余量不足！";
                        }
                        if (calibMethod == null)
                        {
                            projectInfo[4] = "此项目没有对应的较准方法";
                        }
                        if (calibTaskCout != 0)
                        {
                            projectInfo[5] = "此项目已下任务,请做完此项目任务后才能继续下该项目任务！";
                        }
                        else if (strResult != "")
                        {
                            projectInfo[5] = strResult;
                        }

                        if (projectInfo[2] == null && projectInfo[3] == null && projectInfo[4] == null && projectInfo[5] == null)
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
                        projectInfo[2] = "此项目参数录入有误！";
                        projectInfo[4] = "此项目没有对应的较准曲线";

                        if (reagentState == null)
                        {
                            projectInfo[3] = "此项目没有对应试剂！";
                        }
                        else if (reagentState.Locked == true)
                        {
                            projectInfo[3] = "此项目对应试剂被锁定，无法使用！";
                        }

                    }
                    lstProjectName.Add(projectInfo);
                }
            }            
            return lstProjectName;
        }  

        public List<CalibratorinfoTask> QueryAssayProNameAll(string strDBMethod, string p2, string systime)
        {
            List<CalibratorinfoTask> lstCalibratorProjectinfo = new List<CalibratorinfoTask>();

            return lstCalibratorProjectinfo = myBatis.QueryAssayProNameAll(strDBMethod, p2, systime);
        }

        /// <summary>
        /// 顺序号
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public int QueryBigestCalibCTaskInfoForToday(string strDBMethod)
        {
            return myBatis.QueryBigestCalibCTaskInfoForToday(strDBMethod);
        }

        public string AddSDTTableItem(string strDBMethod, SDTTableItem dataConfig)
        {
            return myBatis.AddSDTTableItem(strDBMethod, dataConfig);
        }

        public List<CalibrationResultinfo> QueryCalibrationResultinfo(string strDBMethod, CalibrationResultinfo calibrationResultinfo)
        {
            List<CalibrationResultinfo> lstCalibrationResultinfo = new List<CalibrationResultinfo>();

            return lstCalibrationResultinfo = myBatis.QueryCalibrationResultinfo(strDBMethod, calibrationResultinfo);
        }

        public TimeCourseInfo QueryCalibrationReactionProcess(string strDBMethod, TimeCourseInfo timeCourse)
        {
            TimeCourseInfo lstCalibrationReactionProcess = new TimeCourseInfo();

            return lstCalibrationReactionProcess = myBatis.QueryCalibrationReactionProcess(strDBMethod, timeCourse);
               
        }
        /// <summary>
        /// 获取校准结果和比色杯编号
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="calibrationReactionProcess"></param>
        /// <returns></returns>
        public List<CalibrationResultinfo> QueryCalibrationResultInfoAndTimeCUVNO(string strDBMethod, CalibrationResultinfo calibrationResultinfoAndTimeCUVNO)
        {
            return myBatis.QueryCalibrationResultInfoAndTimeCUVNO(strDBMethod, calibrationResultinfoAndTimeCUVNO);
        }
       

        public List<SDTTableItem> QuerysDTTableItem(string strDBMethod, string p2)
        {
            List<SDTTableItem> lstCalibrationReactionProcess = new List<SDTTableItem>();

            lstCalibrationReactionProcess = myBatis.QuerysDTTableItem(strDBMethod, p2);

            return lstCalibrationReactionProcess;
        }
    }
}
