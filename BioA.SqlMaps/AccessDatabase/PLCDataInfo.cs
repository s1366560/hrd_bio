﻿using BioA.Common;
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
        public int GetWorkingDisk()
        {
            int d = 1;
            try
            {
                d = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetWorkingDisk", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetWorkingDisk()==" + e.ToString(), Module.DAO);
            }

            return d;
        }

        public List<int> GetHasSchedulesWorkDisk(int disk)
        {
            List<int> disks = new List<int>();
            try
            {

                Hashtable ht = new Hashtable();
                ht.Add("BeginTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                ht.Add("PanelNum", disk);
                disks = (List<int>)ism_SqlMap.QueryForList<int>("PLCDataInfo.GetHasSmpWorkDisk",ht);
            }
            catch (Exception)
            {

            }
            return disks;
        }

        public List<TaskInfo> GetSingleWorkingEmgAssayScheduleNoRgtLock(int disk, int count)
        {
            List<TaskInfo> Schedules = new List<TaskInfo>();
         
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("disk", disk);
                ht.Add("count", count);
                ht.Add("CreateDateBegin", DateTime.Now.ToShortDateString());
                ht.Add("CreateDateEnd", DateTime.Now.AddDays(1).ToShortDateString());

                Schedules = ism_SqlMap.QueryForList<TaskInfo>("WorkAreaApplyTask.GetSingleWorkingEmgAssayScheduleNoRgtLock", ht) as List<TaskInfo>;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSingleWorkingEmgAssayScheduleNoRgtLock(int disk, int count)==" + e.ToString(), Module.DAO);
            }

            return Schedules;
        }

        public List<TaskInfo> GetSingleWorkingNorAssayScheduleNoRgtLock(int disk, int count)
        {
            List<TaskInfo> Schedules = new List<TaskInfo>();
         
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("disk", disk);
                ht.Add("count", count);
                ht.Add("taskstate", TaskState.NEW);
                ht.Add("CreateDateBegin", DateTime.Now.ToShortDateString());
                ht.Add("CreateDateEnd", DateTime.Now.AddDays(1).ToShortDateString());

                Schedules = ism_SqlMap.QueryForList<TaskInfo>("WorkAreaApplyTask.GetSingleWorkingNorAssayScheduleNoRgtLock", ht) as List<TaskInfo>;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSingleWorkingNorAssayScheduleNoRgtLock(int disk, int count)==" + e.ToString(), Module.DAO);
            }

            return Schedules;
        }

        public void TroubleLogSave(string strMethodName, TroubleLog troubleLog)
        {
            try
            {
                //#DrawDate#, #TroubleCode#, #TroubleInfo#, #TroubleType#, #TroubleUnit#, #IsConfirm#
                Hashtable ht = new Hashtable();
                ht.Add("DrawDate", troubleLog.DrawDT);
                ht.Add("TroubleCode", troubleLog.TroubleCode);
                ht.Add("TroubleInfo", troubleLog.TroubleInfo);
                ht.Add("TroubleType", troubleLog.TroubleType);
                ht.Add("TroubleUnit", troubleLog.TroubleUnit);
                ht.Add("IsConfirm", troubleLog.IsComfirm);
                ism_SqlMap.Insert("PLCDataInfo." + strMethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("TroubleLogSave(string strMethodName, TroubleLog troubleLog)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateLatestWaterState(string strMethodName, byte[] bInfos)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("State1", bInfos[0]);
                ht.Add("State2", bInfos[1]);
                ism_SqlMap.Update("PLCDataInfo." + strMethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLatestWaterState(string strMethodName, byte[] bInfos)==" + e.ToString(), Module.DAO);
            }
        }
        public void UpdateLatestCUVPanelTemperature(string strMethodName, float t)
        {
            try
            {
                ism_SqlMap.Update("PLCDataInfo." + strMethodName, t);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLatestWaterState(string strMethodName, byte[] bInfos)==" + e.ToString(), Module.DAO);
            }
        }

        public float GetTempOffset(string strMethodName)
        {
            float r = 0;

            try
            {
                r = (float)ism_SqlMap.QueryForObject("PLCDataInfo." + strMethodName, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetTempOffset(string strMethodName)==" + e.ToString(), Module.DAO);
            }

            return r;
        }

        public void UpdateLatestCUVPanelTemperature(float t)
        {
            try
            {
                ism_SqlMap.Update("PLCDataInfo.UpdateLatestCUVPanelTemperature", t);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLatestCUVPanelTemperature(float t)==" + e.ToString(), Module.DAO);
            }
        }

        public void SaveCuvNumber(int wn, int cuvno)
        {
            try
            {
                // 1.更新实时比色杯数据中比色杯编号
                Hashtable ht = new Hashtable();
                ht.Add("WORKNO", wn);
                ht.Add("CUVNO", cuvno);
                ism_SqlMap.Update("PLCDataInfo.UpdateCuvNumber", ht);
                // 2.更新进程比色杯编号
                RealTimeCUVDataInfo realTImeData = ism_SqlMap.QueryForObject("PLCDataInfo.QueryRealTimeCUVDataTC", wn) as RealTimeCUVDataInfo;
                if (realTImeData != null)
                {
                    ht.Clear();
                    ht.Add("CUVNO", cuvno);
                    ht.Add("TimeCourseNO", realTImeData.TC);
                    ism_SqlMap.Update("PLCDataInfo.UpdateTimeCourseCuvNum", ht);
                }
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SaveCuvNumber(int wn, int cuvno)==" + e.ToString(), Module.DAO);
            }
        }

        public RealTimeCUVDataInfo QueryRealTimeCUVDataTC(int wn)
        {
            RealTimeCUVDataInfo realTImeData = new RealTimeCUVDataInfo();
            try
            {
                realTImeData = ism_SqlMap.QueryForObject("PLCDataInfo.QueryRealTimeCUVDataTC", wn) as RealTimeCUVDataInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryRealTimeCUVDataTC(int wn)==" + e.ToString(), Module.DAO);
            }

            return realTImeData;
        }

        public CalibrationResultinfo QueryCalibResultInfo(string assay, string sampleNum, int TCNO)
        {
            CalibrationResultinfo calibResult = new CalibrationResultinfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", assay);
                ht.Add("SampleNum", sampleNum);
                ht.Add("TCNO", TCNO);
                calibResult = ism_SqlMap.QueryForObject("Calibrator.QueryCalibResultInfo", ht) as CalibrationResultinfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibResultInfo(string assay, string sampleType, int TCNO)==" + e.ToString(), Module.DAO);
            }
            return calibResult;
        }

        public void UpdateRealTimeState(int workNo, int cuvPoint)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CUVPoint", cuvPoint);
                ht.Add("WORKNO", workNo);
                ism_SqlMap.Update("PLCDataInfo.UpdateRealTimeState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateRealTimeState(int cuvPoint, int workNo)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateBlkABSData(long t, float pw, float sw)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TimeCourseNO", t);
                ht.Add("CuvBlkWm", pw);
                ht.Add("CuvBlkWs", sw);

                ism_SqlMap.Update("PLCDataInfo.UpdateBlkABSData", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateBlkABSData(long t, float pw, float sw)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateABSData(long t, int p, float pw, float sw)
        {
            try
            {
                Hashtable ht = new Hashtable();
                string strWm = string.Format("Cuv{0}Wm", p.ToString());
                string strWs = string.Format("Cuv{0}Ws", p.ToString());
                ht.Add(strWm, pw);
                ht.Add(strWs, sw);
                ht.Add("TimeCourseNO", t);

                ism_SqlMap.Update("PLCDataInfo.UpdateABSData", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateABSData(long t, int p, float pw, float sw)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateSMPScheduleFinishCount(string smp, string assay, int count)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("FinishTimes", count);
                ht.Add("ProjectName", assay);
                ht.Add("SampleNum", smp);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                ism_SqlMap.Update("PLCDataInfo.UpdateSMPScheduleFinishCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSMPScheduleFinishCount(string smp, string assay, int count)==" + e.ToString(), Module.DAO);
            }
        }

        public int GetSMPScheduleFinishCount(string smp, string assay)
        {
            int FinishCount = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", assay);
                ht.Add("SampleNum", smp);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());

                object obj = ism_SqlMap.QueryForObject("PLCDataInfo.GetSMPScheduleFinishCount", ht);
                if (obj != null)
                    FinishCount = (int)obj;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSMPScheduleFinishCount(string smp, string assay)==" + e.ToString(), Module.DAO);
            }

            return FinishCount;
        }

        public TaskInfo GetSMPSchedule(string smp, string assay)
        {
            TaskInfo taskInfo = new TaskInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", smp);
                ht.Add("ProjectName", assay);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                taskInfo = ism_SqlMap.QueryForObject("WorkAreaApplyTask.GetSMPSchedule", ht) as TaskInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSMPSchedule(string smp, string assay)==" + e.ToString(), Module.DAO);
            }

            return taskInfo;
        }

        public void UpdteTaskState(string sn, string assay)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sn);
                ht.Add("ProjectName", assay);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                ism_SqlMap.Delete("PLCDataInfo.UpdteTaskState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdteTaskState(string sn, string assay)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 获取校准任务完成次数
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="calibratorName"></param>
        /// <param name="calibrationDT"></param>
        /// <returns></returns>
        public int GetSDTScheduleFinishCount(string sampleNum, string projectName, string calibratorName, DateTime calibrationDT)
        {
            int FinishCount = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("CalibratorName", calibratorName);
                ht.Add("CreateDate", calibrationDT);
                FinishCount = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetSDTScheduleFinishCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSDTScheduleFinishCount(string smp, string assay)==" + e.ToString(), Module.DAO);
            }

            return FinishCount;
        }
        /// <summary>
        /// 修改校准任务完成次数
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="calibratorName"></param>
        /// <param name="count"></param>
        public void UpdateSDTScheduleFinishCount(string sampleNum, string projectName, string calibratorName, DateTime calibrationDT, int count)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("CalibratorName", calibratorName);
                ht.Add("CreateDate", calibrationDT);
                ht.Add("FinishTimes", count);
                ism_SqlMap.Update("PLCDataInfo.UpdateSDTScheduleFinishCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSDTScheduleFinishCount(string smp, string assay, int count)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 获取校准任务状态
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleNum"></param>
        /// <param name="calibName"></param>
        /// <returns></returns>
        public CalibratorinfoTask GetSDTSchedule(string projectName, string sampleNum, string calibName, DateTime calibrationDT)
        {
            CalibratorinfoTask taskInfo = new CalibratorinfoTask();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleNum", sampleNum);
                ht.Add("CalibName", calibName);
                ht.Add("CreateDate", calibrationDT);
                taskInfo = ism_SqlMap.QueryForObject("Calibrator.GetSDTSchedule", ht) as CalibratorinfoTask;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSDTSchedule(string smp, string assay)==" + e.ToString(), Module.DAO);
            }

            return taskInfo;
        }

        //public void ClearSDTSchedules(string sn, string assay)
        //{
        //    try
        //    {
        //        Hashtable ht = new Hashtable();
        //        ht.Add("SampleNum", sn);
        //        ht.Add("ProjectName", assay);
        //        ht.Add("StartTime", DateTime.Now.ToShortDateString());
        //        ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
        //        ism_SqlMap.QueryForObject("PLCDataInfo.ClearSDTSchedules", ht);
        //    }
        //    catch (Exception e)
        //    {
        //        LogInfo.WriteErrorLog("ClearSDTSchedules(string sn, string assay)==" + e.ToString(), Module.DAO);
        //    }
        //}
        /// <summary>
        /// 修改校准任务完成状态
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        public void UpdateSDTTaskState(string sampleNum, string projectName,string CalibratorName, DateTime calibrationDT, int taskState)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("CalibratorName", CalibratorName);
                ht.Add("CalibrationDT", calibrationDT);
                ht.Add("TaskState", taskState);
                ism_SqlMap.Update("PLCDataInfo.UpdateSDTTaskState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSDTTaskState(string sampleNum, string projectName)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateQCScheduleFinishCount(string c, string assay, int count)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("FinishTimes", count);
                ht.Add("ProjectName", assay);
                ht.Add("SampleNum", c);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                ism_SqlMap.Update("PLCDataInfo.UpdateQCScheduleFinishCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQCScheduleFinishCount(string smp, string assay, int count)==" + e.ToString(), Module.DAO);
            }
        }

        public int GetQCScheduleFinishCount(string sdt, string assay)
        {
            int FinishCount = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", assay);
                ht.Add("SampleNum", sdt);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                FinishCount = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetQCScheduleFinishCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetQCScheduleFinishCount(string smp, string assay)==" + e.ToString(), Module.DAO);
            }

            return FinishCount;
        }

        public QCTaskInfo GetQCSchedule(string smp, string assay)
        {
            QCTaskInfo taskInfo = new QCTaskInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", smp);
                ht.Add("ProjectName", assay);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                taskInfo = ism_SqlMap.QueryForObject("QCTaskInfo.GetQCSchedule", ht) as QCTaskInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetQCSchedule(string smp, string assay)==" + e.ToString(), Module.DAO);
            }

            return taskInfo;
        }
        /// <summary>
        /// 删除已完成的质控任务
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="assay"></param>
        public void ClearQCSchedules(string sn, string assay)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sn);
                ht.Add("ProjectName", assay);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                ism_SqlMap.QueryForObject("PLCDataInfo.ClearQCSchedules", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ClearQCSchedules(string sn, string assay)==" + e.ToString(), Module.DAO);
            }
        }

        /// <summary>
        /// 修改质控任务状态
        /// </summary>
        /// <param name="SampleNum"></param>
        /// <param name="ProjectName"></param>
        public void UpdateQCTaksState(string SampleNum, string ProjectName)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", SampleNum);
                ht.Add("ProjectName", ProjectName);
                ht.Add("BginTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                ism_SqlMap.Update("PLCDataInfo.UpdateQCTaskState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQCTaksState(string SampleNum, string ProjectName)==" + e.ToString(), Module.DAO);
            }

        }

        public void DeleteRealTimeCUVData(int w)
        {
            try
            {
                ism_SqlMap.Delete("PLCDataInfo.DeleteRealTimeCUVData", w);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteRealTimeCUVData(int w)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateLatestWaterState(int state1, int state2)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("State1", state1);
                ht.Add("State2", state2);
                ism_SqlMap.Update("PLCDataInfo.UpdateLatestWaterState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLatestWaterState(int state1, int state2)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 查询普通任务结果
        /// </summary>
        /// <param name="realTimeData"></param>
        /// <returns></returns>
        public SampleResultInfo GetNORResult(RealTimeCUVDataInfo realTimeData)
        {
            SampleResultInfo result = new SampleResultInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TCNO", realTimeData.TC);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                result = ism_SqlMap.QueryForObject("CommonDataCheck.GetNORResult", ht) as SampleResultInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetNORResult(RealTimeCUVDataInfo realTimeData)==" + e.ToString(), Module.DAO);
            }

            return result;
        }
        /// <summary>
        /// 查询质控结果
        /// </summary>
        /// <param name="realTimeData"></param>
        /// <returns></returns>
        public QualityControlResultInfo GetQCResult(RealTimeCUVDataInfo realTimeData)
        {
            QualityControlResultInfo result = new QualityControlResultInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TCNO", realTimeData.TC);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                result = ism_SqlMap.QueryForObject("QCResultInfo.QueryQCResult", ht) as QualityControlResultInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetQCResult(RealTimeCUVDataInfo realTimeData)==" + e.ToString(), Module.DAO);
            }

            return result;
        }

        public TimeCourseInfo GetTimeCourse(int TCNO, DateTime createTime)
        {
            TimeCourseInfo tcInfo = new TimeCourseInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TCNO", TCNO);
                ht.Add("StartTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                tcInfo = ism_SqlMap.QueryForObject("PLCDataInfo.GetTimeCourse", ht) as TimeCourseInfo;

                if (tcInfo != null)
                {
                    tcInfo.CuvXWmList.AddRange(new List<float> { tcInfo.Cuv1Wm, tcInfo.Cuv2Wm, tcInfo.Cuv3Wm, tcInfo.Cuv4Wm, tcInfo.Cuv5Wm, tcInfo.Cuv6Wm, tcInfo.Cuv7Wm, tcInfo.Cuv8Wm, tcInfo.Cuv9Wm, tcInfo.Cuv10Wm, 
                                                                 tcInfo.Cuv11Wm, tcInfo.Cuv12Wm, tcInfo.Cuv13Wm, tcInfo.Cuv14Wm, tcInfo.Cuv15Wm, tcInfo.Cuv16Wm, tcInfo.Cuv17Wm, tcInfo.Cuv18Wm, tcInfo.Cuv19Wm, tcInfo.Cuv20Wm,
                                                                 tcInfo.Cuv21Wm, tcInfo.Cuv22Wm, tcInfo.Cuv23Wm, tcInfo.Cuv24Wm, tcInfo.Cuv25Wm, tcInfo.Cuv26Wm, tcInfo.Cuv27Wm, tcInfo.Cuv28Wm, tcInfo.Cuv29Wm, tcInfo.Cuv30Wm,
                                                                 tcInfo.Cuv31Wm, tcInfo.Cuv32Wm, tcInfo.Cuv33Wm, tcInfo.Cuv34Wm, tcInfo.Cuv35Wm, tcInfo.Cuv36Wm, tcInfo.Cuv37Wm, tcInfo.Cuv38Wm, tcInfo.Cuv39Wm, tcInfo.Cuv40Wm, 
                                                                 tcInfo.Cuv41Wm, tcInfo.Cuv42Wm, tcInfo.Cuv43Wm });

                    tcInfo.CuvXWsList.AddRange(new List<float> { tcInfo.Cuv1Ws, tcInfo.Cuv2Ws, tcInfo.Cuv3Ws, tcInfo.Cuv4Ws, tcInfo.Cuv5Ws, tcInfo.Cuv6Ws, tcInfo.Cuv7Ws, tcInfo.Cuv8Ws, tcInfo.Cuv9Ws, tcInfo.Cuv10Ws, 
                                                                 tcInfo.Cuv11Ws, tcInfo.Cuv12Ws, tcInfo.Cuv13Ws, tcInfo.Cuv14Ws, tcInfo.Cuv15Ws, tcInfo.Cuv16Ws, tcInfo.Cuv17Ws, tcInfo.Cuv18Ws, tcInfo.Cuv19Ws, tcInfo.Cuv20Ws,
                                                                 tcInfo.Cuv21Ws, tcInfo.Cuv22Ws, tcInfo.Cuv23Ws, tcInfo.Cuv24Ws, tcInfo.Cuv25Ws, tcInfo.Cuv26Ws, tcInfo.Cuv27Ws, tcInfo.Cuv28Ws, tcInfo.Cuv29Ws, tcInfo.Cuv30Ws,
                                                                 tcInfo.Cuv31Ws, tcInfo.Cuv32Ws, tcInfo.Cuv33Ws, tcInfo.Cuv34Ws, tcInfo.Cuv35Ws, tcInfo.Cuv36Ws, tcInfo.Cuv37Ws, tcInfo.Cuv38Ws, tcInfo.Cuv39Ws, tcInfo.Cuv40Ws, 
                                                                 tcInfo.Cuv41Ws, tcInfo.Cuv42Ws, tcInfo.Cuv43Ws });
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetTimeCourse(int TCNO, DateTime createTime)==" + e.ToString(), Module.DAO);
            }

            return tcInfo;
        }

        public RealTimeCUVDataInfo GetRealTimeCUVDataByWorkNo(int workNo)
        {
            RealTimeCUVDataInfo realCUVData = new RealTimeCUVDataInfo();
            try
            {
                realCUVData = ism_SqlMap.QueryForObject("PLCDataInfo.GetRealTimeCUVDataByWorkNo", workNo) as RealTimeCUVDataInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetRealTimeCUVDataByWorkNo(int workNo)==" + e.ToString(), Module.DAO);
            }

            return realCUVData;
        }
        /// <summary>
        /// 获取所有校准任务曲线表数据（只获取曲线状态为：WAITING）
        /// </summary>
        /// <returns></returns>
        public AssayProjectCalibrationParamInfo GetCalibParamBySDTTask()
        {
            AssayProjectCalibrationParamInfo CalibParamInfo = new AssayProjectCalibrationParamInfo();
            try
            {
                CalibParamInfo = ism_SqlMap.QueryForObject("AssayProjectInfo.GetCalibParamBySDTTask", null) as AssayProjectCalibrationParamInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AssayProjectCalibrationParamInfo GetCalibParamBySDTTask()==" + e.ToString(), Module.DAO);
            }

            return CalibParamInfo;
        }
        /// <summary>
        /// 获取所有的校准任务信息
        /// </summary>
        /// <returns></returns>
        public List<CalibratorinfoTask> GetCalibInfoTaskByCalibTaskState()
        {
            List<CalibratorinfoTask> calibTasks = new List<CalibratorinfoTask>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("BeginTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                calibTasks = ism_SqlMap.QueryForList<CalibratorinfoTask>("Calibrator.GetCalibInfoTaskByCalibTaskState", ht) as List<CalibratorinfoTask>;
            }
            catch(Exception e)
            {
                LogInfo.WriteErrorLog("GetCalibInfoTaskByCalibTaskState()==" + e.ToString(), Module.DAO);
            }
            return calibTasks;
        }

        /// <summary>
        /// 下校准任务：
        ///             根据项目名称和样本类型获取所有的校准任务信息
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public List<CalibratorinfoTask> GetCalibTasksByProject(string projectName, string sampleType)
        {
            List<CalibratorinfoTask> calibTasks = new List<CalibratorinfoTask>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);

                calibTasks = ism_SqlMap.QueryForList<CalibratorinfoTask>("Calibrator.GetCalibTasksByProject", ht) as List<CalibratorinfoTask>;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetCalibTasksByProject(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return calibTasks;
        }
        /// <summary>
        /// 修改校准任务状态（1）
        /// </summary>
        /// <param name="calibTask">校准任务信息</param>
        /// <param name="calibState">状态码</param>
        public void UpdateSDTSchedulePerform(CalibratorinfoTask calibTask, int calibState)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", calibTask.SampleNum);
                ht.Add("ProjectName", calibTask.ProjectName);
                ht.Add("SampleType", calibTask.SampleType);
                ht.Add("CalibName", calibTask.CalibName);
                ht.Add("CalibTaskState", calibState);
                ism_SqlMap.Update("PLCDataInfo.UpdateSDTSchedulePerform", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSDTSchedulePerform(CalibratorinfoTask calibTask, CalibState calibState)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 修改校准结果状态（检测中（1），已完成（2））
        /// </summary>
        /// <param name="calibResult"></param>
        /// <param name="calibState"></param>
        public void UpdateSDTResultState(CalibratorinfoTask calibResult, int calibState)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", calibResult.SampleNum);
                ht.Add("ProjectName", calibResult.ProjectName);
                ht.Add("SampleType", calibResult.SampleType);
                ht.Add("CalibName", calibResult.CalibName);
                ht.Add("CalibrationDT", calibResult.CreateDate);
                ht.Add("CalibResultState", calibState);
                ism_SqlMap.Update("PLCDataInfo.UpdateSDTResultState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSDTResultState(CalibratorinfoTask calibResult, int calibState)==" + e.ToString(), Module.DAO);
            }
        }

        public ProjectRunSequenceInfo QueryProjectRunSequenceByProject(AssayProjectInfo assayProInfo)
        {
            ProjectRunSequenceInfo proRunSequence = new ProjectRunSequenceInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", assayProInfo.ProjectName);
                ht.Add("SampleType", assayProInfo.SampleType);

                proRunSequence = ism_SqlMap.QueryForObject("PLCDataInfo.QueryProjectRunSequenceByProject", ht) as ProjectRunSequenceInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProjectRunSequenceByProject(AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return proRunSequence;
        }

        public string GetRGTDilutePosition()
        {
            string pos = "";
            try
            {
                pos = ism_SqlMap.QueryForObject("PLCDataInfo.GetRGTDilutePosition", null) as string;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetRGTDilutePosition()==" + e.ToString(), Module.DAO);
            }

            return pos;
        }
        public string GetSDTSMPContainerType()
        {
            string container = "";
            try
            {
                container = ism_SqlMap.QueryForObject("PLCDataInfo.GetSDTSMPContainerType", null) as string;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSDTSMPContainerType()==" + e.ToString(), Module.DAO);
            }

            return container;
        }

        public SMPContainerTypeInfo SMPContainerType(string container)
        {
            SMPContainerTypeInfo containerTypeInfo = new SMPContainerTypeInfo();
            try
            {
                containerTypeInfo = ism_SqlMap.QueryForObject("PLCDataInfo.SMPContainerType", container) as SMPContainerTypeInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SMPContainerType(string container)==" + e.ToString(), Module.DAO);
            }

            return containerTypeInfo;
        }
        /// <summary>
        /// 修改SDTtableItem表的状态（CALIBRATING）
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        public void SetCalibratingCurveState(string projectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("CalibStateOld", CalibRemarks.NEW);
                ht.Add("CalibStateNew", CalibRemarks.CALI);
                ism_SqlMap.Update("PLCDataInfo.SetCalibratingCurveState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SetCalibratingCurveState(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }

        public List<QCTaskInfo> GetQCParamByQCTask(int count)
        {
            List<QCTaskInfo> lstQCTaskInfo = new List<QCTaskInfo>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("count", count);
                ht.Add("CreateDateBegin", DateTime.Now.ToShortDateString());
                ht.Add("CreateDateEnd", DateTime.Now.AddDays(1).ToShortDateString());
                lstQCTaskInfo = ism_SqlMap.QueryForList<QCTaskInfo>("QCTaskInfo.GetQCParamByQCTask", ht) as List<QCTaskInfo>;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetQCParamByQCTask(int count)==" + e.ToString(), Module.DAO);
            }
            return lstQCTaskInfo;
        }

        public void UpdateQCSchedulePerform(QCTaskInfo qcTask, int taskState)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", qcTask.ProjectName);
                ht.Add("SampleType", qcTask.SampleType);
                ht.Add("SampleNum", qcTask.SampleNum);
                ht.Add("TaskState", taskState);
                ism_SqlMap.Update("PLCDataInfo.UpdateQCSchedulePerform", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQCSchedulePerform(QCTaskInfo qcTask, TaskState taskState)==" + e.ToString(), Module.DAO);
            }
        }
        public QualityControlInfo QueryQCInfoByQCID(int qcID)
        {
            QualityControlInfo qcInfo = new QualityControlInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("QCID", qcID);
                qcInfo = ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCInfoByQCID", ht) as QualityControlInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCInfoByQCID(int qcID)==" + e.ToString(), Module.DAO);
            }

            return qcInfo;
        }
        public void UpdateTaskStatePerform(TaskInfo task, int taskState)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", task.ProjectName);
                ht.Add("SampleType", task.SampleType);
                ht.Add("SampleNum", task.SampleNum);
                ht.Add("TaskState", taskState);
                ism_SqlMap.Update("PLCDataInfo.UpdateTaskStatePerform", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateTaskStatePerform(TaskInfo qcTask, TaskState taskState)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateSampleStatePerform(TaskInfo task, int taskState)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", task.SampleNum);
                ht.Add("CreateTime", task.CreateDate);
                ht.Add("TaskState", taskState);
                ism_SqlMap.Update("PLCDataInfo.UpdateSampleStatePerform", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSampleStatePerform(TaskInfo qcTask, TaskState taskState)==" + e.ToString(), Module.DAO);
            }
        }
        
        public SampleInfo QuerySampleInfoByQCID(int sampleNum)
        {
            SampleInfo sampleInfo = new SampleInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("CreateTimeStart", DateTime.Now.ToShortDateString());
                ht.Add("CreateTimeEnd", DateTime.Now.AddDays(1).ToShortDateString());
                sampleInfo = ism_SqlMap.QueryForObject("WorkAreaApplyTask.QuerySampleInfoByQCID", ht) as SampleInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QuerySampleInfoByQCID(int sampleNum)==" + e.ToString(), Module.DAO);
            }

            return sampleInfo;
        }

        public bool BAutoFreezeTaskByReagentVolWarning()
        {
            bool bFreezeTask = false;
            try
            {
                bFreezeTask = (bool)ism_SqlMap.QueryForObject("PLCDataInfo.BAutoFreezeTaskByReagentVolWarning", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("BAutoFreezeTaskByReagentVolWarning()==" + e.ToString(), Module.DAO);
            }

            return bFreezeTask;
        }

        public int GetNOStartTaskByWorkDisk(int curdisk)
        {
            int count = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CreateTimeStart", DateTime.Now.ToShortDateString());
                ht.Add("CreateTimeEnd", DateTime.Now.AddDays(1).ToShortDateString());
                ht.Add("PanelNum", curdisk);
                count = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetNOStartTaskByWorkDisk", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetNOStartTaskByWorkDisk(int curdisk)==" + e.ToString(), Module.DAO);
            }

            return count;
        }

        public void UpdateWorkingDisk(int curDisk)
        {
            try
            {
                ism_SqlMap.Update("PLCDataInfo.UpdateWorkingDisk", curDisk);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateWorkingDisk(int curDisk)==" + e.ToString(), Module.DAO);
            }
        }

        public void ClearNotTodaySchedule()
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TargetDate", DateTime.Now.ToShortDateString());

                ism_SqlMap.Delete("PLCDataInfo.DeleteNotTodayCalibTask", ht);
                ism_SqlMap.Delete("PLCDataInfo.DeleteNotTodayQCTask", ht);
                ism_SqlMap.Delete("PLCDataInfo.DeleteNotTodayTask", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ClearNotTodaySchedule()==" + e.ToString(), Module.DAO);
            }
        }

        public void SetUnfinishedScheduleContinue()
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SendTimes",0);
                ht.Add("FinishTimes",0);
                ht.Add("TaskState",0);
                ism_SqlMap.Update("PLCDataInfo.UpdateTaskState",ht);
                try
                {
                    Hashtable ht1 = new Hashtable();
                    ht1.Add("SendTimes", 0);
                    ht1.Add("FinishTimes", 0);
                    ht1.Add("TaskState", 0);
                    ism_SqlMap.Update("QCTaskInfo.InitMachineUpdateQCTaskState", ht1);

                    ht1.Clear();
                    ht1.Add("SendTimes", 0);
                    ht1.Add("FinishTimes", 0);
                    ht1.Add("TaskState", 0);
                    ism_SqlMap.Update("Calibrator.UpdateCalibTaskState", ht1);
                }
                catch (Exception e)
                {
                    LogInfo.WriteErrorLog("SetUnfinishedScheduleContinue()==" + e.ToString(), Module.DAO);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SetUnfinishedScheduleContinue()==" + e.ToString(), Module.DAO);
            }
        }

        public DateTime GetRunningDate()
        {
            DateTime dateTime = new DateTime();
            try
            {
                dateTime = (DateTime)ism_SqlMap.QueryForObject("PLCDataInfo.GetRunningDate", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetRunningDate()==" + e.ToString(), Module.DAO);
            }

            return dateTime;
        }

        public void InitRunningState()
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("LastestSMPNO", 0);
                ht.Add("DrawDate", DateTime.Now.Date);
                ht.Add("TargetDate", DateTime.Now.ToShortDateString());
                ism_SqlMap.Update("PLCDataInfo.InitRunningState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("InitRunningState()==" + e.ToString(), Module.DAO);
            }

        }

        public void ClearRTData()
        {
            try
            {
                ism_SqlMap.Delete("PLCDataInfo.ClearRTData", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ClearRTData()==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateIsRunning(bool bRunning)
        {
            try
            {
                ism_SqlMap.Update("PLCDataInfo.UpdateIsRunning", bRunning);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateIsRunning(bool bRunning)==" + e.ToString(), Module.DAO);
            }
        }

        public void InitSMPCalItems()
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TargetDate", DateTime.Now.Date);
                ism_SqlMap.Delete("PLCDataInfo.InitSMPCalItems", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("InitSMPCalItems()==" + e.ToString(), Module.DAO);
            }
        }
        public int GetLastestTC()
        {
            int t = 0;
            try
            {
                t = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetLastestTC", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetLastestTC()==" + e.ToString(), Module.DAO);
            }

            return t;
        }

        public void DeleteRealTimeCUVData(RealTimeCUVDataInfo realTimeInfo)
        {
            try
            {
                ism_SqlMap.Delete("PLCDataInfo.DeleteRealTimeCUVData", realTimeInfo.WorkNo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteRealTimeCUVData(RealTimeCUVDataInfo realTimeInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public void SaveRealTimeCUVData(RealTimeCUVDataInfo realTimeInfo)
        {
            try
            {
                ism_SqlMap.Insert("PLCDataInfo.SaveRealTimeCUVData", realTimeInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SaveRealTimeCUVData(RealTimeCUVDataInfo realTimeInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public void DeleteTimeCourseByTCNO(int TCNO)
        {
            try
            {
                ism_SqlMap.Delete("PLCDataInfo.DeleteTimeCourseByTCNO", TCNO);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteTimeCourseByTCNO(int TCNO)==" + e.ToString(), Module.DAO);
            }
        }

        public void SaveTimeCourseByTCNO(TimeCourseInfo tcInfo)
        {
            try
            {
                //(#TimeCourseNO#,#DrawDate#,#CUVNO#)
                Hashtable ht = new Hashtable();
                ht.Add("TimeCourseNO", tcInfo.TimeCourseNo);
                ht.Add("DrawDate", tcInfo.DrawDate);
                ht.Add("CUVNO", tcInfo.CUVNO);
                ism_SqlMap.Insert("PLCDataInfo.SaveTimeCourseByTCNO", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SaveTimeCourseByTCNO(TimeCourseInfo tcInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateLatestTC(int t)
        {
            string SQL = string.Format(@"update RunningStateTb set LastestTC='{0}'", t);
            try
            {
                ism_SqlMap.Update("PLCDataInfo.UpdateLatestTC", t);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLatestTC(int t)==" + e.ToString(), Module.DAO);
            }
        }

        public int GetSMPScheduleSendCount(string sampleNum, string projectName, string sampleType)
        {
            int count = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                count = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetSMPScheduleSendCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSMPScheduleSendCount(int sampleNum, string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return count;
        }
        /// <summary>
        /// 查询校准任务发送的次数
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <param name="samplePos"></param>
        /// <returns></returns>
        public int GetSDTScheduleSendCount(string sampleNum, string projectName, string sampleType, string calibratorName)
        {
            int count = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("CalibratorName", calibratorName);
                count = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetSDTScheduleSendCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetSDTScheduleSendCount(int sampleNum, string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return count;
        }

        public int GetQCScheduleSendCount(string sampleNum, string projectName, string sampleType)
        {
            int count = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                count = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetQCScheduleSendCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetQCScheduleSendCount(int sampleNum, string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return count;
        }
        /// <summary>
        /// 修改常规任务发送次数
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <param name="sendtimes"></param>
        public void UpdateSMPScheduleSendCount(string sampleNum, string projectName, string sampleType, int sendtimes)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("SendTimes", sendtimes);
                ism_SqlMap.Update("PLCDataInfo.UpdateSMPScheduleSendCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSMPScheduleSendCount(int sampleNum, string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

        }
        /// <summary>
        /// 修改校准任务发送的次数
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <param name="sendtimes"></param>
        public void UpdateSDTScheduleSendCount(string sampleNum, string projectName, string sampleType, string calibratorName, int sendtimes)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("CalibratorName", calibratorName);
                ht.Add("SendTimes", sendtimes);
                ism_SqlMap.Update("PLCDataInfo.UpdateSDTScheduleSendCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSDTScheduleSendCount(int sampleNum, string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

        }
        /// <summary>
        /// 修改质控任务发送次数
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <param name="sendtimes"></param>
        public void UpdateQCScheduleSendCount(string sampleNum, string projectName, string sampleType, int sendtimes)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("SendTimes", sendtimes);
                ism_SqlMap.Update("PLCDataInfo.UpdateQCScheduleSendCount", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQCScheduleSendCount(int sampleNum, string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

        }

        public int QueryRunSequenceByProject(string projectName, string SampleType)
        {
            int iSequence = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", SampleType);
                iSequence = (int)ism_SqlMap.QueryForObject("PLCDataInfo.QueryRunSequenceByProject", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryRunSequenceByProject(string projectName, string SampleType)==" + e.ToString(), Module.DAO);
            }

            return iSequence;
        }
        /// <summary>
        /// 更新电压值
        /// </summary>
        /// <param name="v"></param>
        public void UpdateVoltageValue(float v)
        {
            string SQL = string.Format("update  ManuOffsetGainTb set Voltage='{0}'", v);
            try
            {
                ism_SqlMap.Update("PLCDataInfo.UpdateVoltageValue", v);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateVoltageValue(float v)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 修改样本结果中进程编号（TCNO）
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <param name="TCNO"></param>
        public void UpdateSampleResultTCNO(string sampleNum, string projectName, string sampleType, DateTime sampleCreateTime, int TCNO)
        {//T.SMPNO, T.ASSAY, T.SAMPLETYPE,
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("SampleCreateTime",sampleCreateTime );
                ht.Add("TCNO", TCNO);

                ism_SqlMap.Update("PLCDataInfo.UpdateSampleResultTCNO", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSampleResultTCNO(string sampleNum, string projectName, string sampleType, int TCNO)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 修改质控结果中的进程编号（TCNO）
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <param name="TCNO"></param>
        public void UpdateQualityControlResultTCNO(string sampleNum, string projectName, string sampleType, DateTime CalibDate, int TCNO)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("SampleCreateTime", CalibDate);
                ht.Add("TCNO", TCNO);
                ism_SqlMap.Update("PLCDataInfo.UpdateQualityControlResultTCNO", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQualityControlResultTCNO(string sampleNum, string projectName, string sampleType, int TCNO)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 修改校准结果表的进程编号（TCNO）
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <param name="TCNO"></param>
        public void UpdateSDTResultTCNO(string sampleNum, string projectName, string sampleType, string calibName, DateTime calibDate, int TCNO)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampleNum);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("CalibName", calibName);
                ht.Add("CalibrationDT", calibDate);
                ht.Add("TCNO", TCNO);
                ism_SqlMap.Update("PLCDataInfo.UpdateSDTResultTCNO", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSDTResultTCNO(string sampleNum, string projectName, string sampleType, int TCNO)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 根据任务信息查询校准曲线表信息
        /// </summary>
        /// <param name="sampNO"></param>
        /// <param name="projectName"></param>
        /// <param name="sampType"></param>
        /// <param name="calibName"></param>
        /// <returns></returns>
        public CalibrationCurveInfo QueryCalibCurveInfoByCalibNameAndProName(string sampNO, string projectName, string sampType, string calibName)
        {
            CalibrationCurveInfo calibCurveInfo = new CalibrationCurveInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Pos", sampNO);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampType);
                ht.Add("CalibName", calibName);
                calibCurveInfo = ism_SqlMap.QueryForObject("Calibrator.QueryCalibCurveInfoByCalibNameAndProName", ht) as CalibrationCurveInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibCurveInfoByCalibNameAndProName(string sampNO, string projectName, string sampType, string calibName)==" + e.ToString(), Module.DAO);
            }
            return calibCurveInfo;
        }
        /// <summary>
        /// 保存校准结果
        /// </summary>
        /// <param name="c"></param>
        /// <param name="calibDate"></param>
        /// <param name="TCNO"></param>
        /// <param name="sampNum"></param>
        public void AddSDTResult(CalibrationCurveInfo c, DateTime calibDate, int TCNO, string sampNum)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", sampNum);
                ht.Add("ProjectName", c.ProjectName);
                ht.Add("SampleType", c.SampleType);
                ht.Add("TCNO", TCNO);
                ht.Add("CalibMethod", c.CalibType);
                ht.Add("CalibratorName", c.CalibName);
                ht.Add("CalibrationState", TaskState.START);
                ht.Add("CalibrationDT", calibDate);
                ht.Add("CalibConcentration", c.CalibConcentration);
                ism_SqlMap.Insert("Calibrator.AddSDTResult", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddSDTResult(CalibrationCurveInfo c, DateTime calibDate, int TCNO, string sampNum)" + e.ToString(), Module.DAO);
            }
        }


        public int GetAllTaskCount()
        {
            int task= 0;
            try
            {
                task = (int)ism_SqlMap.QueryForObject("PLCDataInfo.GetAllTaskCount", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetAllTaskCount()==" + e.ToString(), Module.DAO);
            }
            return task;
        }
    }
}