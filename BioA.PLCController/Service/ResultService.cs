using BioA.Common;
using BioA.Common.CalcMethod;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.PLCController.Service
{
    public class ResultService
    {
        MyBatis myBatis = new MyBatis();
        public float GetResultAbsValue(ResultInfo samResultInfo)
        {
            float abs = 0;

            AssayProjectParamInfo para = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = samResultInfo.ProjectName, SampleType = samResultInfo.SampleType });
            TimeCourseInfo tc = myBatis.GetTimeCourse(samResultInfo.TCNO, samResultInfo.SampleCreateTime);
            string dilutionType = "";
            if (samResultInfo.GetType().Name == "SampleResultInfo")
                dilutionType = myBatis.GetSampleTaskDilutionType(samResultInfo as SampleResultInfo);
            else
                dilutionType = "常规体积";
            //SampleInfo s = myBatis.GetSample(samResultInfo.SampleNum, samResultInfo.SampleCreateTime);

           // string SMPVOLTypeStr = null;
            //if (s != null)
            //{
            //    SMPType smptype = new SMPTypeService().Get(s.SampleType) as SMPType;
            //    if (smptype != null)
            //    {
            //        SMPVOLTypeStr = smptype.SMPVOLType;
            //    }
            //    else
            //    {
            //        SMPVOLTypeStr = "S";
            //    }
            //}

            if (para != null)
            {
                switch (para.AnalysisMethod)
                {
                    case "一点终点法":
                        abs = ABSProcess.OnePoint(tc, para, dilutionType);
                        break;
                    case "二点终点法":
                        abs = ABSProcess.TwoPoint(tc, para, dilutionType);
                        break;
                    case "速率A法":
                        abs = ABSProcess.RateA(tc, para, dilutionType);
                        break;
                    case "速率B法":
                        abs = ABSProcess.RateB(tc, para, dilutionType);
                        break;
                }
            }

            return abs;
        }
        /// <summary>
        /// 获取校准吸光度
        /// </summary>
        /// <param name="calibResultInfo"></param>
        /// <returns></returns>
        public float GetResultAbsValue(CalibrationResultinfo calibResultInfo)
        {
            float abs = 0;

            AssayProjectParamInfo para = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = calibResultInfo.ProjectName, SampleType = calibResultInfo.SampleType });
            TimeCourseInfo tc = myBatis.GetTimeCourse(calibResultInfo.TCNO, calibResultInfo.CalibrationDT);
            string dilutionType = "定标体积";
            //SampleInfo s = myBatis.GetSample(samResultInfo.SampleNum, samResultInfo.SampleCreateTime);

            // string SMPVOLTypeStr = null;
            //if (s != null)
            //{
            //    SMPType smptype = new SMPTypeService().Get(s.SampleType) as SMPType;
            //    if (smptype != null)
            //    {
            //        SMPVOLTypeStr = smptype.SMPVOLType;
            //    }
            //    else
            //    {
            //        SMPVOLTypeStr = "S";
            //    }
            //}

            if (para != null)
            {
                switch (para.AnalysisMethod)
                {
                    case "一点终点法":
                        abs = ABSProcess.OnePoint(tc, para, dilutionType);
                        break;
                    case "二点终点法":
                        abs = ABSProcess.TwoPoint(tc, para, dilutionType);
                        break;
                    case "速率A法":
                        abs = ABSProcess.RateA(tc, para, dilutionType);
                        break;
                    case "速率B法":
                        abs = ABSProcess.RateB(tc, para, dilutionType);
                        break;
                }
            }

            return abs;
        }

        public float GetResultConcValue(ResultInfo samResultInfo)
        {
            float c = 0;
            SDTTableItem table = new SDTTableItem();
            AssayProjectCalibrationParamInfo calParam = myBatis.GetAssayProjectCalParamInfo(samResultInfo.ProjectName, samResultInfo.SampleType) as AssayProjectCalibrationParamInfo;

            if (calParam.CalibrationMethod == "K系数法")
            {
                c = CalculateConc.GetKConftMethodConc(calParam, samResultInfo.AbsValue);
            }
            else
            {
                table = myBatis.GetAssayUsingTable(samResultInfo.ProjectName, samResultInfo.SampleType);
                c = CalculateConc.GetConc(table, samResultInfo.AbsValue);
            }

            //table = myBatis.GetAssayUsingTable(samResultInfo.ProjectName, samResultInfo.SampleType);
            ////曲线拟合计算样本浓度值
            //c = CalculateConc.GetConc(table, samResultInfo.AbsValue);

            //修正定标体积和样本体积差异
            AssayProjectParamInfo assayProParam = myBatis.GetAssProParamInfo("GetAssayProjectParamInfoByNameAndType", samResultInfo.ProjectName, samResultInfo.SampleType);
            if (assayProParam == null)
            {
                return 0;
            }
            
            string dilutionType = "";
            if (samResultInfo.GetType().Name == "SampleResultInfo")
                dilutionType = myBatis.GetSampleTaskDilutionType(samResultInfo as SampleResultInfo);
            else
                dilutionType = "常规体积";
            //定标体积校正
            float SampleVol = 0;
            float SdtVol = 0;
            if (assayProParam != null)
            {
                if (assayProParam.CalibSamVol == 0)
                {
                    SdtVol = assayProParam.CalibStosteVol;
                }
                else
                {
                    SdtVol = assayProParam.CalibSamVol;
                }
                
                switch (dilutionType)
                {
                    case "减量体积"://减量体积
                        if (assayProParam.DecSamVol == 0)
                        {
                            SampleVol = assayProParam.DecStosteVol;
                        }
                        else
                        {
                            SampleVol = assayProParam.DecSamVol;
                        }
                        break;
                    case "增量体积"://增量体积
                        if (assayProParam.IncSamVol == 0)
                        {
                            SampleVol = assayProParam.IncStosteVol;
                        }
                        else
                        {
                            SampleVol = assayProParam.IncSamVol;
                        }
                        break;
                    case "常规体积"://常规体积
                        if (assayProParam.ComSamVol == 0)
                        {
                            SampleVol = assayProParam.ComStosteVol;
                        }
                        else
                        {
                            SampleVol = assayProParam.ComSamVol;
                        }                   
                        break;
                    case "自定义":
                        break;
                }
            }
            if (table != null /*&& table.SDTCurve != "Absolute"*/)//
            {
                float k1 = SdtVol / (SdtVol + assayProParam.Reagent1Vol + assayProParam.Reagent2Vol) * (SampleVol + assayProParam.Reagent1Vol + assayProParam.Reagent2Vol) / SampleVol;
                c = c * k1;
            }

            //原液体积折算
            float k = 1.0f;
            if (assayProParam != null && table != null)
            {
                switch (dilutionType)
                {
                    case "减量体积":
                        k = (assayProParam.DecStosteVol + assayProParam.DecDilutionVol) / assayProParam.DecStosteVol;
                        break;
                    case "增量体积":
                        k = (assayProParam.IncStosteVol + assayProParam.IncDilutionVol) / assayProParam.IncStosteVol;                        
                        break;
                    case "常规体积":
                        k = (assayProParam.ComStosteVol + assayProParam.ComDilutionVol) / assayProParam.ComStosteVol;
                        break;
                    case "自定义":
                        break;
                }
                /*
                if (table.SDTCurve == "Absolute")
                {
                    c = c * 1.0f;
                }
                else*/
                {
                    c = c * k;
                }
            }


            if (assayProParam != null)
            {
                c = c * assayProParam.InstrumentFactorA + assayProParam.InstrumentFactorB;
            }
            
            
            return c;
        }

        public void AnalyzeResult(SampleResultInfo r)
        {
            AssayProjectParamInfo A = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = r.ProjectName, SampleType = r.SampleType });
            if (A == null)
            {
                return;
            }
            TimeCourseInfo T = myBatis.GetTimeCourse(r.TCNO, r.SampleCreateTime);
            if (T == null)
            {
                return;
            }
            SampleInfo S = myBatis.GetSample(r.SampleNum, r.SampleCreateTime);
            if (S == null)
            {
                return;
            }
            AssayProjectRangeParamInfo ARP = myBatis.GetRangeParamInfo(r.ProjectName,r.SampleType);
            if (ARP == null)
            {
                return;
            }
            ProjectRunSequenceInfo proRunSequence = myBatis.QueryProjectRunSequenceByProject(new AssayProjectInfo() { ProjectName = r.ProjectName, SampleType = r.SampleType });
            if (proRunSequence == null)
            {
                return;
            }
            //试剂吸光度判读，检测试剂是否正常
            string RgtAbsFlag = null;
            float RgtAbs = ABSProcess.GetReangentAbs(T, A);
            if (A.ReagentBlankMaximum > 0.000001 && A.ReagentBlankMaximum < RgtAbs)
            {
                RgtAbsFlag = "RgtAbsMax";
            }
            if (A.ReagentBlankMaximum > 0.000001 && A.ReagentBlankMinimum > RgtAbs)
            {
                RgtAbsFlag = "RgtAbsMin";
            }
            if (RgtAbsFlag != null)
            {
                if (string.IsNullOrEmpty(r.Remarks) || string.IsNullOrWhiteSpace(r.Remarks))
                {
                    r.Remarks = RgtAbsFlag;
                }
                else
                {
                    r.Remarks += "|" + RgtAbsFlag;
                }
                myBatis.UpdateNORResultRunLog(r);
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.WARN;
                trouble.TroubleUnit = @"试剂";
                trouble.TroubleCode = "000002";
                trouble.TroubleInfo = string.Format(@"{0}:{1}发生试剂吸光度越界,其反应进程:{2}。请检测试剂是否过期. ", r.SampleNum, r.ProjectName, r.TCNO);
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }


            //底物耗尽判断处理,
            string AbsLimFlag = null;
            float AbsLimRef = A.LimitValue; //(0--3.5)
            float AbsLim = ABSProcess.GetAbsLimAbs(T, A);//M2E abs
            if (A.AnalysisMethod == "速率A法" || A.AnalysisMethod == "速率B法")
            {
                if (A.ReactionDirection == "正反应")//正反应
                {
                    if (AbsLimRef > 0.000001 && AbsLimRef < AbsLim)
                    {
                        AbsLimFlag = "AbsLim";
                    }
                }
                if (A.ReactionDirection == "负反应")//负反应
                {
                    if (AbsLimRef > 0.000001 && AbsLimRef > AbsLim)
                    {
                        AbsLimFlag = "AbsLim";
                    }
                }

            }
            if (AbsLimFlag != null)
            {
                if (string.IsNullOrEmpty(r.Remarks) || string.IsNullOrWhiteSpace(r.Remarks))
                {
                    r.Remarks = AbsLimFlag;
                }
                else
                {
                    r.Remarks += "|" + AbsLimFlag;
                }
                myBatis.UpdateNORResultRunLog(r);

                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = @"样本";
                trouble.TroubleCode = "000003";
                trouble.TroubleInfo = string.Format(@"{0}:{1}发生底物耗尽，其反应进程:{2}", r.SampleNum, r.ProjectName, r.TCNO);
                myBatis.TroubleLogSave("TroubleLogSave", trouble);

                if (ARP.AutoRerun == true)
                {
                    //Schedule Schedule = new ScheduleService().GetSMPSchedule(r.SMPNO, r.ItemName) as Schedule;
                    TaskInfo task = myBatis.GetTask(r.ProjectName, r.SampleNum);
                    if (task == null)
                    {
                        task = new TaskInfo();
                        task.SampleNum = r.SampleNum;
                        task.ProjectName = r.ProjectName;
                        //Schedule.WorkType = S.IsEmergency == true ? WORKTYPE.E : WORKTYPE.N;
                        task.SampleDilute = "减量体积";
                        task.SendTimes = 0;
                        task.FinishTimes = 0;
                        task.InspectTimes = 1;
                        task.TaskState = 0;
                        task.IsReRun = true;
                        //Schedule.ReRun = "AbsLim";
                        if (r.ResultVolType != task.SampleDilute)//结果体积参数和计划体积参数不同才可以提交工作计划
                        {
                            //new ScheduleService().Save(Schedule);
                            myBatis.SaveTske(task);
                        }
                    }
                }
            }

            //技术 血清/尿液范围即线性范围判读
            float LineRefMin = 0.0f;//线性参考值
            float LineRefMax = 0.0f;//线性参考值
            switch (S.SampleType)
            {
                case "尿液":
                case "血清": LineRefMin = A.FirstSlope; LineRefMax = A.FirstSlopeHigh; break;
            }

            string LinFlag = null;
            try
            {
                float v = r.ConcResult;
                if (LineRefMax > 0.00001 && v > LineRefMax)
                {
                    LinFlag = "Lin.H";
                }
                if (LineRefMax > 0.00001 && v < LineRefMin)
                {
                    LinFlag = "Lin.L";
                }
            }
            catch
            {
            }
            if (LinFlag != null)
            {
                if (string.IsNullOrEmpty(r.Remarks) || string.IsNullOrWhiteSpace(r.Remarks))
                {
                    r.Remarks = LinFlag;
                }
                else
                {
                    r.Remarks += "|" + LinFlag;
                }
                myBatis.UpdateNORResultRunLog(r);
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = @"样本";
                trouble.TroubleCode = "00001";
                trouble.TroubleInfo = string.Format(@"{0}:{1}线性范围违规，其反应进程:{2}", r.SampleNum, r.ProjectName, r.TCNO);
                myBatis.TroubleLogSave("TroubleLogSave", trouble);

                if (ARP.AutoRerun == true)
                {
                    //Schedule Schedule = new ScheduleService().GetSMPSchedule(r.SMPNO, r.ItemName) as Schedule;
                    TaskInfo task = myBatis.GetTask(r.ProjectName, r.SampleNum);
                    if (task == null)
                    {
                        task = new TaskInfo();
                        task.SampleNum = r.SampleNum;
                        task.ProjectName = r.ProjectName;
                        //Schedule.WorkType = S.IsEmergency == true ? WORKTYPE.E : WORKTYPE.N;
                        switch (LinFlag)
                        {
                            case "Lin.L": task.SampleDilute = "增量体积"; break;
                            case "Lin.H": task.SampleDilute = "减量体积"; break;
                        }
                        task.SendTimes = 0;
                        task.FinishTimes = 0;
                        task.InspectTimes = 1;
                        task.TaskState = 0;
                        task.IsReRun = true;
                        //Schedule.ReRun = "AbsLim";
                        if (r.ResultVolType != task.SampleDilute)//结果体积参数和计划体积参数不同才可以提交工作计划
                        {
                            //new ScheduleService().Save(Schedule);
                            myBatis.SaveTske(task);
                        }
                    }
                }
            }

            //血清结果浓度临界判读,只对血清样本起作用。
            if (S.SampleType == "血清")
            {
                string PanicFlag = null;
                try
                {
                    float v = r.ConcResult;
                    if (A.SerumCriticalMaximum > 0.00001 && A.SerumCriticalMaximum < v)
                    {
                        PanicFlag = "Panic.H";
                    }
                    if (A.SerumCriticalMaximum > 0.00001 && A.SerumCriticalMinimum > v)
                    {
                        PanicFlag = "Panic.L";
                    }
                }
                catch
                {
                }
                if (PanicFlag != null)
                {
                    if (string.IsNullOrEmpty(r.Remarks) || string.IsNullOrWhiteSpace(r.Remarks))
                    {
                        r.Remarks = PanicFlag;
                    }
                    else
                    {
                        r.Remarks += "|" + PanicFlag;
                    }
                    myBatis.UpdateNORResultRunLog(r);

                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleType = TROUBLETYPE.ERR;
                    trouble.TroubleUnit = @"样本";
                    trouble.TroubleCode = "00001";
                    trouble.TroubleInfo = string.Format(@"{0}:{1}发生血清临界值违规，其反应进程:{2}", r.SampleNum, r.ProjectName, r.TCNO);
                    myBatis.TroubleLogSave("TroubleLogSave", trouble);

                    if (ARP.AutoRerun == true)
                    {
                        //Schedule Schedule = new ScheduleService().GetSMPSchedule(r.SMPNO, r.ItemName) as Schedule;
                        TaskInfo task = myBatis.GetTask(r.ProjectName, r.SampleNum);
                        if (task == null)
                        {
                            task = new TaskInfo();
                            task.SampleNum = r.SampleNum;
                            task.ProjectName = r.ProjectName;
                            //Schedule.WorkType = S.IsEmergency == true ? WORKTYPE.E : WORKTYPE.N;
                            switch (r.ResultVolType)
                            {
                                case VOLTYPE.IV: task.SampleDilute = "增量体积"; break;
                                case VOLTYPE.DV: task.SampleDilute = "减量体积"; break;
                                case VOLTYPE.NA: task.SampleDilute = "常规体积"; break;
                            }
                            task.SendTimes = 0;
                            task.FinishTimes = 0;
                            task.InspectTimes = 1;
                            task.TaskState = 0;
                            task.IsReRun = true;
                            //Schedule.ReRun = "AbsLim";
                            if (r.ResultVolType != task.SampleDilute)//结果体积参数和计划体积参数不同才可以提交工作计划
                            {
                                //new ScheduleService().Save(Schedule);
                                myBatis.SaveTske(task);
                            }
                        }
                    }
                }
            }

            //前驱界限，该值是个比例
            string ProzontLimitPanicFlag = null;
            float ProzontLimit = ABSProcess.GetProzontLimitValue(T, A, r.ResultVolType, S.SampleType);
            if (A.ProLowestBound > 0.000001)
            {
                if (ProzontLimit > A.ProLowestBound / 100)
                {
                    ProzontLimitPanicFlag = "P*";
                }
            }
            if (ProzontLimitPanicFlag != null)
            {
                if (string.IsNullOrEmpty(r.Remarks) || string.IsNullOrWhiteSpace(r.Remarks))
                {
                    r.Remarks = ProzontLimitPanicFlag;
                }
                else
                {
                    r.Remarks += "|" + ProzontLimitPanicFlag;
                }
                myBatis.UpdateNORResultRunLog(r);
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = @"样本";
                trouble.TroubleCode = "00001";
                trouble.TroubleInfo = string.Format(@"{0}:{1}前驱界限违规！反应进程:{2}", r.SampleNum, r.ProjectName, r.TCNO);
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
                if (ARP.AutoRerun == true)
                {
                    //Schedule Schedule = new ScheduleService().GetSMPSchedule(r.SMPNO, r.ItemName) as Schedule;
                    TaskInfo task = myBatis.GetTask(r.ProjectName, r.SampleNum);
                    if (task == null)
                    {
                        task = new TaskInfo();
                        task.SampleNum = r.SampleNum;
                        task.ProjectName = r.ProjectName;
                        //Schedule.WorkType = S.IsEmergency == true ? WORKTYPE.E : WORKTYPE.N;
                        task.SampleDilute = "减量体积";
                        task.SendTimes = 0;
                        task.FinishTimes = 0;
                        task.InspectTimes = 1;
                        task.TaskState = 0;
                        task.IsReRun = true;
                        //Schedule.ReRun = "AbsLim";
                        if (r.ResultVolType != task.SampleDilute)//结果体积参数和计划体积参数不同才可以提交工作计划
                        {
                            //new ScheduleService().Save(Schedule);
                            myBatis.SaveTske(task);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 修改校准曲线,拟合表
        /// </summary>
        /// <param name="r"></param>
        public void OnSDTCalibrateCurve(CalibrationResultinfo r)
        {
            if (IsResultRight(r.Remarks) == true)
            {
                myBatis.UpdateCalibratingCurve(r);
                myBatis.UpdateSDTResult(r);
            }

            SDTTableItem SDTTableItem_Calibrating = myBatis.QuerySDTTableItemByCalibrating(r.ProjectName, r.SampleType, r.CalibrationDT, r.CalibMethod);
            if (SDTTableItem_Calibrating != null)
            {
                bool sb = myBatis.IsHasSDTSchedule(r);
                //为true，任务失败
                if (sb == false)
                {
                    if (IsSdtCurveRight(SDTTableItem_Calibrating) == true)
                    {
                        myBatis.SetSDTTabelSuccessfulState(r.ProjectName, r.SampleType);
                        myBatis.SetSDTUsingFlag(SDTTableItem_Calibrating);
                    }
                    else
                    {
                        myBatis.SetSDTTabelFailedState(r.ProjectName, r.SampleType);
                    }
                }
            }
        }

        public bool IsSdtCurveRight(SDTTableItem curve)
        {

            CalibrationResultinfo results = null;
            bool blkrihgtflag = false;
            results = myBatis.GetSDTResultByProject(curve.ProjectName, curve.SampleType, curve.DrawDate, curve.BlkItem);
            if (results != null)
            {
                blkrihgtflag = CheckResultsHasRihgt(results);
            }
            else
            {
                blkrihgtflag = true;
            }
            bool s1rihgtflag = false;
            results = myBatis.GetSDTResultByProject(curve.ProjectName, curve.SampleType, curve.DrawDate, curve.Calib1Item);
            if (results != null)
            {
                s1rihgtflag = CheckResultsHasRihgt(results);
            }
            else
            {
                s1rihgtflag = true;
            }
            bool s2rihgtflag = false;
            results = myBatis.GetSDTResultByProject(curve.ProjectName, curve.SampleType, curve.DrawDate, curve.Calib2Item);
            if (results != null)
            {
                s2rihgtflag = CheckResultsHasRihgt(results);
            }
            else
            {
                s2rihgtflag = true;
            }
            bool s3rihgtflag = false;
            results = myBatis.GetSDTResultByProject(curve.ProjectName, curve.SampleType, curve.DrawDate, curve.Calib3Item);
            if (results != null)
            {
                s3rihgtflag = CheckResultsHasRihgt(results);
            }
            else
            {
                s3rihgtflag = true;
            }
            bool s4rihgtflag = false;
            results = myBatis.GetSDTResultByProject(curve.ProjectName, curve.SampleType, curve.DrawDate, curve.Calib4Item);
            if (results != null)
            {
                s4rihgtflag = CheckResultsHasRihgt(results);
            }
            else
            {
                s4rihgtflag = true;
            }
            bool s5rihgtflag = false;
            results = myBatis.GetSDTResultByProject(curve.ProjectName, curve.SampleType, curve.DrawDate, curve.Calib5Item);
            if (results != null)
            {
                s5rihgtflag = CheckResultsHasRihgt(results);
            }
            else
            {
                s5rihgtflag = true;
            }
            bool s6rihgtflag = false;
            results = myBatis.GetSDTResultByProject(curve.ProjectName, curve.SampleType, curve.DrawDate, curve.Calib6Item);
            if (results !=null)
            {
                s6rihgtflag = CheckResultsHasRihgt(results);
            }
            else
            {
                s6rihgtflag = true;
            }
            if (blkrihgtflag == true && s1rihgtflag == true && s2rihgtflag == true && s3rihgtflag == true && s4rihgtflag == true && s5rihgtflag == true && s6rihgtflag == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckResultsHasRihgt(CalibrationResultinfo results)
        {
            bool hasrightflag = false;
            if (IsResultRight(results.Remarks) == true)
            {
                hasrightflag = true;
            }
            return hasrightflag;
        }

        public bool IsResultRight(string remarks)
        {

            if (remarks == "" || remarks == null)
            {
                return true;
            }

            if (remarks.Contains("SMP") == true)
            {
                return false;
            }
            if (remarks.Contains("R1") == true)
            {
                return false;
            }
            if (remarks.Contains("R2") == true)
            {
                return false;
            }
            return true;
        }

        public bool GetResultBeExistFromRealTimeWorkNum(int workNo, out RealTimeCUVDataInfo rt)
        {
            bool bExist = false;

            rt = myBatis.GetRealTimeCUVDataByWorkNo(workNo);
            if (rt == null)
            {
                return bExist;
            }
                        
            switch (rt.WorkType)
            {
                case WORKTYPE.N:
                case WORKTYPE.E:
                    SampleResultInfo samResInfo = myBatis.GetNORResult(rt);
                    if (samResInfo == null)
                        bExist = false;
                    else
                        bExist = true;
                    break;
                case WORKTYPE.B:
                case WORKTYPE.S:
                    CalibrationResultinfo calibResInfo = myBatis.QueryCalibResultInfoByTCNO(rt);
                    if (calibResInfo == null)
                        bExist = false;
                    else
                        bExist = true;
                    break;
                case WORKTYPE.C:
                    QualityControlResultInfo qcResInfo = myBatis.GetQCResult(rt);
                    if (qcResInfo == null)
                    bExist = false;
                    else
                    bExist = true;
                    break;
            }
            return bExist;
        }

        
    }
}
