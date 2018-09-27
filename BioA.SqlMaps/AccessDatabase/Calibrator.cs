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
        public SDTTableItem GetAssayUsingTable(string projectName, string sampleType)
        {
            SDTTableItem sdtItem = new SDTTableItem();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                sdtItem = ism_SqlMap.QueryForObject("Calibrator.GetAssayUsingTable", ht) as SDTTableItem;

                if (sdtItem == null)
                    sdtItem = new SDTTableItem();
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetAssayUsingTable(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return sdtItem;
        }
        /// <summary>
        /// 根据项目名称和样本类型查询校准参数实体类
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public AssayProjectCalibrationParamInfo GetAssayProjectCalParamInfo(string projectName, string sampleType)
        {
            AssayProjectCalibrationParamInfo calParam = new AssayProjectCalibrationParamInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                calParam = ism_SqlMap.QueryForObject("AssayProjectInfo.GetAssayProjectCalParamInfo", ht) as AssayProjectCalibrationParamInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetAssayProjectCalParamInfo(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
            return calParam;
        }

        public List<SDTTableItem> QuerysDTTableItem(string strDBMethod, string p2)
        {
            List<SDTTableItem> lstCalibrationReactionProcess = new List<SDTTableItem>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", p2);

                lstCalibrationReactionProcess = (List<SDTTableItem>)ism_SqlMap.QueryForList<SDTTableItem>("Calibrator." + strDBMethod, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.DAO);
            }

            return lstCalibrationReactionProcess;
        }

        public CalibrationResultinfo QueryCalibResultInfoByTCNO(RealTimeCUVDataInfo realTimeDataInfo)
        {
            CalibrationResultinfo calibResInfo = new CalibrationResultinfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("TCNO", realTimeDataInfo.TC);
                ht.Add("BeginTime", DateTime.Now.ToShortDateString());
                ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
                calibResInfo = ism_SqlMap.QueryForObject("Calibrator.QueryCalibResultInfoByTCNO", ht) as CalibrationResultinfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibResultInfoByTCNO(RealTimeCUVDataInfo realTimeDataInfo)==" + e.ToString(), Module.DAO);
            }

            return calibResInfo;
        }
        /// <summary>
        /// 修改校准任务结果的吸光度
        /// </summary>
        /// <param name="calibResInfo"></param>
        public void UpdateSDTResult(CalibrationResultinfo calibResInfo)
        {
            try
            {
                ism_SqlMap.Update("Calibrator.UpdateSDTResult", calibResInfo);
                Hashtable ht = new Hashtable();
                ht.Add("SampleType", calibResInfo.SampleType);
                ht.Add("ProjectName", calibResInfo.ProjectName);
                ht.Add("CalibDate", DateTime.Now.ToString());
                ht.Add("DrawDate", calibResInfo.CalibrationDT);
                ht.Add("CalibMethod", calibResInfo.CalibMethod);
                int count = (int)ism_SqlMap.QueryForObject("Calibrator.QueryCalibCurveByProject", ht);
                if (count == 0)
                {
                    ism_SqlMap.Insert("Calibrator.AddCalibCurveByProject", ht);
                }
                else
                {
                    ism_SqlMap.Update("Calibrator.UpdateCalibCurveByProject", ht);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateSDTResult(CalibrationResultinfo calibResInfo)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 修改校准曲线表的吸光度
        /// </summary>
        /// <param name="calibResInfo"></param>
        public void UpdateCalibratingCurve(CalibrationResultinfo calibResInfo)
        {
            try
            {
                // 1.获取原来项目对应校准曲线数据
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", calibResInfo.ProjectName);
                ht.Add("SampleType", calibResInfo.SampleType);
                ht.Add("CalibMethod", calibResInfo.CalibMethod);
                ht.Add("DrawDate", calibResInfo.CalibrationDT);
                SDTTableItem sdtItem = ism_SqlMap.QueryForObject("Calibrator.QuerySDTTableItemByCalibrating", ht) as SDTTableItem;

                // 2.更新对应校准品校准测得的吸光度
                if (sdtItem != null)
                {
                    float fAbsNew = 0;
                    if (calibResInfo.CalibratorName == sdtItem.BlkItem)
                    {
                        
                        if (sdtItem.BlkAbs == 0)
                        {
                            fAbsNew = calibResInfo.CalibAbs;
                        }
                        else
                        {
                            fAbsNew = (sdtItem.BlkAbs + calibResInfo.CalibAbs) / 2;
                        }
                        ht.Add("BlkAbs", fAbsNew);
                    }
                    else if (calibResInfo.CalibratorName == sdtItem.Calib1Item)
                    {
                        if (sdtItem.SDT1Abs == 0)
                        {
                            fAbsNew = calibResInfo.CalibAbs;
                        }
                        else
                        {
                            fAbsNew = (sdtItem.SDT1Abs + calibResInfo.CalibAbs) / 2;
                        }
                        ht.Add("SDT1Abs", fAbsNew);
                    }
                    else if (calibResInfo.CalibratorName == sdtItem.Calib2Item)
                    {
                        if (sdtItem.SDT2Abs == 0)
                        {
                            fAbsNew = calibResInfo.CalibAbs;
                        }
                        else
                        {
                            fAbsNew = (sdtItem.SDT2Abs + calibResInfo.CalibAbs) / 2;
                        }
                        ht.Add("SDT2Abs", fAbsNew);
                    }
                    else if (calibResInfo.CalibratorName == sdtItem.Calib3Item)
                    {
                        if (sdtItem.SDT3Abs == 0)
                        {
                            fAbsNew = calibResInfo.CalibAbs;
                        }
                        else
                        {
                            fAbsNew = (sdtItem.SDT3Abs + calibResInfo.CalibAbs) / 2;
                        }
                        ht.Add("SDT3Abs", fAbsNew);
                    }
                    else if (calibResInfo.CalibratorName == sdtItem.Calib4Item)
                    {
                        if (sdtItem.SDT4Abs == 0)
                        {
                            fAbsNew = calibResInfo.CalibAbs;
                        }
                        else
                        {
                            fAbsNew = (sdtItem.SDT4Abs + calibResInfo.CalibAbs) / 2;
                        }
                        ht.Add("SDT4Abs", fAbsNew);
                    }
                    else if (calibResInfo.CalibratorName == sdtItem.Calib5Item)
                    {
                        if (sdtItem.SDT5Abs == 0)
                        {
                            fAbsNew = calibResInfo.CalibAbs;
                        }
                        else
                        {
                            fAbsNew = (sdtItem.SDT5Abs + calibResInfo.CalibAbs) / 2;
                        }
                        ht.Add("SDT5Abs", fAbsNew);
                    }
                    else if (calibResInfo.CalibratorName == sdtItem.Calib6Item)
                    {
                        if (sdtItem.SDT6Abs == 0)
                        {
                            fAbsNew = calibResInfo.CalibAbs;
                        }
                        else
                        {
                            fAbsNew = (sdtItem.SDT6Abs + calibResInfo.CalibAbs) / 2;
                        }
                        ht.Add("SDT6Abs", fAbsNew);
                    }
                    ism_SqlMap.Update("Calibrator.UpdateSDTTableItem", ht);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalibratingCurve(CalibrationResultinfo calibResInfo)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 获取所有校准曲线状态为（CALIBRATING）的数据
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public SDTTableItem QuerySDTTableItemByCalibrating(string projectName, string sampleType, DateTime calibrationDT, string calibMethod)
        {
            SDTTableItem sdtItem = new SDTTableItem();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("DrawDate", calibrationDT);
                ht.Add("CalibMethod", calibMethod);
                ht.Add("CalibState", CalibRemarks.CALI);
                sdtItem = ism_SqlMap.QueryForObject("Calibrator.QuerySDTTableItemByCalibrating", ht) as SDTTableItem;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QuerySDTTableItemByCalibrating(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return sdtItem;
        }
        /// <summary>
        /// 根据项目信息查询校准任务对应的任务状态是否已完成
        /// </summary>
        /// <param name="calibResInfo"></param>
        /// <returns></returns>
        public bool IsHasSDTSchedule(CalibrationResultinfo calibResInfo)
        {
            bool bExist = false;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", calibResInfo.SampleNum);
                ht.Add("ProjectName", calibResInfo.ProjectName);
                ht.Add("SampleType", calibResInfo.SampleType);
                ht.Add("CalibratorName", calibResInfo.CalibratorName);
                ht.Add("DrawDate", calibResInfo.CalibrationDT);
                int succes = (int)ism_SqlMap.QueryForObject("Calibrator.QueryCalibTaskCountByProject", ht);

                if (succes != TaskState.SUCC)
                    bExist = true;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("IsHasSDTSchedule(CalibrationResultinfo calibResInfo)==" + e.ToString(), Module.DAO);
            }

            return bExist;

        }

        public CalibrationResultinfo GetSDTResultByProject(string projectName, string sampleType, DateTime drawDate, string calibName)
        {
            CalibrationResultinfo calibResInfo = new CalibrationResultinfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("CreateDate", drawDate);
                ht.Add("CalibName", calibName);
                calibResInfo = ism_SqlMap.QueryForObject("Calibrator.GetSDTResultByProject", ht) as CalibrationResultinfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("IsHasSDTSchedule(CalibrationResultinfo calibResInfo)==" + e.ToString(), Module.DAO);
            }

            return calibResInfo;
        }
        /// <summary>
        /// 修改校准曲线状态
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        public void SetSDTTabelSuccessfulState(string projectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("CalibStateOld", CalibRemarks.CALI);
                ht.Add("CalibStateNew", CalibRemarks.SUCC);

                ism_SqlMap.Update("Calibrator.UpdateCalibStateByProject", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SetSDTTabelSuccessfulState(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 修改该曲线是否可以使用
        /// </summary>
        /// <param name="t"></param>
        public void SetSDTUsingFlag(SDTTableItem t)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("IsUsed", false);
                ht.Add("ProjectName", t.ProjectName);
                ht.Add("SampleType", t.SampleType);
                ism_SqlMap.Update("Calibrator.UpdateCalibCurveIsNotUsed", ht);

                ht.Clear();
                ht.Add("IsUsed", true);
                ht.Add("ProjectName", t.ProjectName);
                ht.Add("SampleType", t.SampleType);
                ht.Add("DrawDate", t.DrawDate);
                ism_SqlMap.Update("Calibrator.UpdateCalibCurveIsUsed", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SetSDTUsingFlag(SDTTableItem t)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 任务失败，修改校准曲线状态
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        public void SetSDTTabelFailedState(string projectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ht.Add("CalibDate", DateTime.Now);
                ht.Add("CalibStateOld", CalibRemarks.CALI);
                ht.Add("CalibStateNew", CalibRemarks.FAIL);

                ism_SqlMap.Update("Calibrator.UpdateCalibStateByProject", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SetSDTTabelFailedState(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }

        /// <summary>
        /// 根据项目信息获取校准任务
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="calibTask"></param>
        /// <returns></returns>
        public int QueryCalibTaskByProjectAndSamType(string strMethodName, CalibratorinfoTask calibTask)
        {
            int calibTaskCount = 0;
            try
            {
                calibTaskCount = (int)ism_SqlMap.QueryForObject("Calibrator." + strMethodName, calibTask);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibTaskByProjectAndSamType(string strMethodName, CalibratorinfoTask calibTask)==" + e.ToString(), Module.DAO);
            }
            return calibTaskCount;
        }


        public void UpdateCalibTaskState(string ProjectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", ProjectName);
                ht.Add("SampleType", sampleType);
                ht.Add("SendTimes", 0);
                ht.Add("FinishTimes", 0);
                ht.Add("TaskState", 0);
                ism_SqlMap.Update("Calibrator.UpdateCalibTaskState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalibTaskState(string ProjectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateCalibCurveState(string ProjectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", ProjectName);
                ht.Add("SampleType", sampleType);
                ism_SqlMap.Update("Calibrator.UpdateCalibCurveState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalibCurveState(string ProjectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateCalibResultRunLog(CalibrationResultinfo calibResInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", calibResInfo.SampleNum);
                ht.Add("ProjectName", calibResInfo.ProjectName);
                ht.Add("TCNO", calibResInfo.TCNO);
                ht.Add("Remarks", calibResInfo.Remarks);
                ht.Add("CalibrationDT", calibResInfo.CalibrationDT);
                ism_SqlMap.Update("Calibrator.UpdateCalibResultRunLog", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalibResultRunLog(CalibrationResultinfo calibResInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public List<SDTTableItem> GetAllNewSDTTable()
        {
            List<SDTTableItem> lstSDTItem = new List<SDTTableItem>();
            try
            {
                lstSDTItem = ism_SqlMap.QueryForList<SDTTableItem>("Calibrator.GetAllNewSDTTable", null) as List<SDTTableItem>;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetAllNewSDTTable()==" + e.ToString(), Module.DAO);
            }

            return lstSDTItem;            
        }

        public void DeleteSDTTableItemByProAndDate(SDTTableItem s)
        {
            try
            {
                ism_SqlMap.Delete("Calibrator.DeleteSDTTableItemByProAndDate", s);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteSDTTableItemByProAndDate(SDTTableItem s)==" + e.ToString(), Module.DAO);
            }
        }

        public void DeleteSDTSchedule(string calibMethod, string projectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CalibName", calibMethod);
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ism_SqlMap.Delete("Calibrator.DeleteSDTSchedule", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteSDTSchedule(string calibMethod, string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }

        public void DeleteSDTTableItemByProject(string projectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("SampleType", sampleType);
                ism_SqlMap.Delete("Calibrator.DeleteSDTTableItemByProject", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteSDTTableItemByProject(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }
        }

        public List<SDTTableItem> QuerySDTTableItemTb()
        {
            List<SDTTableItem> lstSDTItems = new List<SDTTableItem>();
            try
            {
                lstSDTItems = ism_SqlMap.QueryForList<SDTTableItem>("Calibrator.QuerySDTTableItemTb", null) as List<SDTTableItem>;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QuerySDTTableItemTb()==" + e.ToString(), Module.DAO);
            }

            return lstSDTItems;
        }
    }
}
