using BioA.Common;
using IBatisNet.DataMapper;
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
        /// <summary>
        /// 查找条数通过项目name和项目类型
        /// </summary>
        /// <param name="strAccessDBMethod">方法名</param>
        /// <param name="assayProject">参数</param>
        /// <returns></returns>
        public int SelectAssayProCountByNameAndType(string strAccessDBMethod, AssayProjectInfo assayProject)
        {
            int resultCout = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ProjectName", assayProject.ProjectName);
                hashTable.Add("SampleType", assayProject.SampleType);
                resultCout = (int)ism_SqlMap.QueryForObject("AssayProjectInfo." + strAccessDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return resultCout;
        }
        public int DeleteAssayProCountByNameAndType(string strAccessDBMethod, AssayProjectInfo assayProject)
        {
            int deletecount = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("proName", assayProject.ProjectName);
                hashTable.Add("proType", assayProject.SampleType);
                deletecount = (int)ism_SqlMap.QueryForObject("AssayProjectInfo.DeleteAssayProject", hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return deletecount;
        }
        public int UpdateAssayProCountByNameAndType(string strAccessDBMethod, AssayProjectInfo assayProInfoOld, AssayProjectInfo assayProject2)
        {
            int iResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ProjectName", assayProject2.ProjectName);
                hashTable.Add("SampleType", assayProject2.SampleType);
                if ((int)ism_SqlMap.QueryForObject("AssayProjectInfo.SelectAssayProCountByPrimarykey", hashTable) > 0
                        && assayProInfoOld.ProjectName != assayProject2.ProjectName)
                {
                    iResult = 0;
                    return iResult;
                }

                Hashtable hash = new Hashtable();
                if (assayProInfoOld.ProjectName == assayProject2.ProjectName && assayProInfoOld.SampleType == assayProject2.SampleType)
                {
                    hash.Add("proModifyName", assayProject2.ProjectName);
                    hash.Add("proModifyType", assayProject2.SampleType);
                    hash.Add("proFullName", assayProject2.ProFullName);
                    hash.Add("channelNum", assayProject2.ChannelNum);
                    ism_SqlMap.Update("AssayProjectInfo.EditAssayProject", hash);
                }
                else
                {
                    hash.Add("proModifyName", assayProject2.ProjectName);
                    hash.Add("proModifyType", assayProject2.SampleType);
                    hash.Add("proFullName", assayProject2.ProFullName);
                    hash.Add("channelNum", assayProject2.ChannelNum);
                    hash.Add("proOldName", assayProInfoOld.ProjectName);
                    hash.Add("proOldType", assayProInfoOld.SampleType);
                    //ism_SqlMap.Update("AssayProjectInfo.UpdateProjectNameForParam", hash);
                    //ism_SqlMap.Update("AssayProjectInfo.UpdateProjectNameForCalibParam", hash);
                    //ism_SqlMap.Update("AssayProjectInfo.UpdateProjectNameForRangeParam", hash);
                    //ism_SqlMap.Update("AssayProjectInfo.UpdateProjectRunSequence", hash);
                    iResult = (int)ism_SqlMap.QueryForObject("AssayProjectInfo.UpdateAssayProject", hash);

                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return iResult;
        }
        /// <summary>
        /// 添加生化项目
        /// </summary>
        /// <param name="strAccessDBMethod">访问数据库方法名</param>
        /// <param name="assayProject">添加项目信息</param>
        /// <returns></returns>
        public AssayProjectParamInfo AddAssayProject(string strAccessDBMethod, AssayProjectInfo assayProject)
        {
            int resultCount = 0;
            AssayProjectParamInfo assayProjectParamInfo = null;
            Hashtable hashtable = new Hashtable();
            hashtable.Add("proName", assayProject.ProjectName);
            hashtable.Add("samType", assayProject.SampleType);
            hashtable.Add("projectFullName", assayProject.ProFullName);
            hashtable.Add("NCHAN", assayProject.ChannelNum);
            try
            {
                resultCount = (int)ism_SqlMap.QueryForObject("AssayProjectInfo.SaveAssayProjectInfoAll" , hashtable);
                if (resultCount == 0)
                {
                    assayProjectParamInfo = ism_SqlMap.QueryForObject("AssayProjectInfo.GetAssayProjectParamInfoByNameAndType", assayProject) as AssayProjectParamInfo;
                }
                else
                    return null;
                //ism_SqlMap.Insert("AssayProjectInfo.AddAssayProjectparamInfo", assayProject);
                //ism_SqlMap.Insert("AssayProjectInfo.AddCalibrationParam", assayProject);
                //ism_SqlMap.Insert("AssayProjectInfo.AddRangeParam", assayProject);
                //ism_SqlMap.Insert("AssayProjectInfo.AddProRunSequenceTb",assayProject);
            }
            catch(Exception e)
            {
                LogInfo.WriteErrorLog("AssayProjectParamInfo AddAssayProject(string strAccessDBMethod, AssayProjectInfo assayProject) ==" + e.Message.ToString(), Module.DAO);
            }
            return assayProjectParamInfo;
        }
        /// <summary>
        /// 获取生化项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public List<AssayProjectInfo> QueryAssayProAllInfo(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
            List<int> lstInt = new List<int>();
            List<AssayProjectInfo> lstProjectInfo= new List<AssayProjectInfo>();
            List<AssayProjectInfo> lstNotSortProjectName = new List<AssayProjectInfo>();
            try
            {
                if (assayProInfo == null)
                {
                    lstAssayProInfos = (List<AssayProjectInfo>)ism_SqlMap.QueryForList<AssayProjectInfo>("AssayProjectInfo." + strDBMethod, null);
                }
                else
                {
                    Hashtable hash = new Hashtable();
                    if (assayProInfo.ProjectName != string.Empty)
                    {
                        hash.Add("ProjectName", assayProInfo.ProjectName);
                    }
                    else if (assayProInfo.SampleType != string.Empty)
                    {
                        hash.Add("SampleType", assayProInfo.SampleType);
                    }
                    else if (assayProInfo.ProFullName != string.Empty)
                    {
                        hash.Add("ProFullName", assayProInfo.ProFullName);
                    }
                    else if (assayProInfo.ChannelNum != string.Empty)
                    {
                        hash.Add("ChannelNum", assayProInfo.ChannelNum);
                    }

                    lstAssayProInfos = (List<AssayProjectInfo>)ism_SqlMap.QueryForList<AssayProjectInfo>("AssayProjectInfo." + strDBMethod, hash);
                }
                foreach (AssayProjectInfo assayProjectInfo in lstAssayProInfos)
                {
                    int s = assayProjectInfo.ProjectName.IndexOf('.');
                    if (s < 0)
                    {
                        lstNotSortProjectName.Add(assayProjectInfo);
                        continue;
                    }
                    lstInt.Add(Convert.ToInt32(assayProjectInfo.ProjectName.Substring(0,s)));
                }
                lstInt.Sort();
                foreach (int i in lstInt)
                {
                    foreach (AssayProjectInfo assayProjectInfo in lstAssayProInfos)
                    {
                        int s = assayProjectInfo.ProjectName.IndexOf('.');
                        if (s < 0)
                        {
                            continue;
                        }
                        if (i == Convert.ToInt32(assayProjectInfo.ProjectName.Substring(0, s)))
                        {
                            lstProjectInfo.Add(assayProjectInfo);
                        }
                    }
                }
                lstProjectInfo.AddRange(lstNotSortProjectName);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SettingsAccess.cs_QueryAssayProAllInfo(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }
            LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.DAO);
            return lstProjectInfo;
        }

        /// <summary>
        /// 获取生化项目参数信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public AssayProjectParamInfo GetAssProParamInfo(string strDBMethod, string projectName, string sampleType)
        {
            AssayProjectParamInfo assayProParam = null;
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("ProjectName", projectName);
                hash.Add("SampleType", sampleType);

                assayProParam = (AssayProjectParamInfo)ism_SqlMap.QueryForObject("AssayProjectInfo." + strDBMethod, hash);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetAssProParamInfo(string strDBMethod, AssayProjectInfo assayProInfo) == " + ex.ToString(), Module.Setting);
            }
            return assayProParam;
        }

        /// <summary>
        /// 获取生化项目参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns>项目参数实体类</returns>
        public AssayProjectParamInfo GetAssayProjectParamInfoByNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            AssayProjectParamInfo assayProParam = new AssayProjectParamInfo();
            
            List<ReagentStateInfoR1R2> proReagentState = new List<ReagentStateInfoR1R2>();
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("ProjectName", assayProInfo.ProjectName);
                hash.Add("SampleType", assayProInfo.SampleType);

                assayProParam = (AssayProjectParamInfo)ism_SqlMap.QueryForObject("AssayProjectInfo." + strDBMethod, assayProInfo);

                ReagentSettingsInfo reagentSettings = (ReagentSettingsInfo)ism_SqlMap.QueryForObject("ReagentInfo.GetReagentSettingsInfo", hash);
                ReagentSettingsInfo reagentSettings2 = (ReagentSettingsInfo)ism_SqlMap.QueryForObject("ReagentInfo.GetReagentSettingsInfo2", hash);

                if (reagentSettings != null)
                {
                    assayProParam.Reagent1Name = reagentSettings.ReagentName;
                    assayProParam.Reagent1Pos = reagentSettings.Pos;
                    assayProParam.Reagent1ValidDate = reagentSettings.ValidDate;
                }
                if (reagentSettings2 != null)
                {
                    assayProParam.Reagent2Name = reagentSettings2.ReagentName;
                    assayProParam.Reagent2Pos = reagentSettings2.Pos;
                    assayProParam.Reagent2ValidDate = reagentSettings2.ValidDate;
                }
                
                proReagentState = (List<ReagentStateInfoR1R2>)ism_SqlMap.QueryForList<ReagentStateInfoR1R2>("ReagentInfo.GetReagentStateInfo", assayProInfo.ProjectName);
                foreach (ReagentStateInfoR1R2 reaState in proReagentState)
                {
                    if (reaState.ReagentName == assayProParam.Reagent1Name)
                    {
                        assayProParam.Reagent1Vol = reaState.ReagentResidualVol;
                    }
                    if (reaState.ReagentName2 == assayProParam.Reagent2Name)
                    {
                        assayProParam.Reagent2Vol = reaState.ReagentResidualVol2;
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SettingsAccess_GetAssayProjectParamInfoByNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }
            return assayProParam;
        }
        /// <summary>
        /// 获取所有生化项目参数信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<AssayProjectParamInfo> QueryAssayProjectParamInfoAll(string strDBMethod,List<ResultSetInfo> lstResultInfo)
        {
            List<AssayProjectParamInfo> lstAssayProParamsResult = new List<AssayProjectParamInfo>();

            List<ReagentStateInfoR1R2> proReagentState = new List<ReagentStateInfoR1R2>();
            try
            {
                //获取所有生化项目参数信息
                List<AssayProjectParamInfo> lstAssayProParam = (List<AssayProjectParamInfo>)ism_SqlMap.QueryForList<AssayProjectParamInfo>("AssayProjectInfo." + strDBMethod,null);
                //获取所有试剂1信息
                List<ReagentSettingsInfo> lstReagentSettings = (List<ReagentSettingsInfo>)ism_SqlMap.QueryForList<ReagentSettingsInfo>("ReagentInfo.QueryReagentSetting1", null);
                //获取所有试剂2信息
                List<ReagentSettingsInfo> lstReagentSettings2 = (List<ReagentSettingsInfo>)ism_SqlMap.QueryForList<ReagentSettingsInfo>("ReagentInfo.QueryReagentSetting2", null);
                //获取所有试剂1 OR 2信息
                proReagentState = (List<ReagentStateInfoR1R2>)ism_SqlMap.QueryForList<ReagentStateInfoR1R2>("ReagentInfo.QueryReagentStateInfoAll", null);
                foreach (AssayProjectParamInfo assayProParam in lstAssayProParam)
                {
                    ReagentSettingsInfo reagent1 = lstReagentSettings.SingleOrDefault(r1 => r1.ProjectName == assayProParam.ProjectName);
                    if (reagent1 != null)
                    {
                        assayProParam.Reagent1Name = reagent1.ReagentName;
                        assayProParam.Reagent1Pos = reagent1.Pos;
                        assayProParam.Reagent1ValidDate = reagent1.ValidDate;
                    }
                    ReagentSettingsInfo reagent2 = lstReagentSettings.SingleOrDefault(r2 => r2.ProjectName == assayProParam.ProjectName);
                    if (reagent2 != null)
                    {
                        assayProParam.Reagent2Name = reagent2.ReagentName;
                        assayProParam.Reagent2Pos = reagent2.Pos;
                        assayProParam.Reagent2ValidDate = reagent2.ValidDate;
                    }
                    ReagentStateInfoR1R2 reagentr1r2 = proReagentState.SingleOrDefault(r1r2 => r1r2.ProjectName == assayProParam.ProjectName);
                    if (reagentr1r2 != null)
                    {
                        if (reagentr1r2.ReagentName == assayProParam.Reagent1Name)
                        {
                            assayProParam.Reagent1Vol = reagentr1r2.ValidPercent;
                        }
                        if (reagentr1r2.ReagentName == assayProParam.Reagent2Name)
                        {
                            assayProParam.Reagent2Vol = reagentr1r2.ValidPercent2;
                        }
                    }
                    ResultSetInfo resultSetInfo = lstResultInfo.SingleOrDefault(resultInfo => resultInfo.ProjectName == assayProParam.ProjectName);
                    if (resultSetInfo != null)
                    {
                        assayProParam.ResultUnit = resultSetInfo.Unit;
                        assayProParam.ResultDecimal = resultSetInfo.RadixPointNum;
                    }
                    lstAssayProParamsResult.Add(assayProParam);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SettingsAccess_GetAssayProjectParamInfoByNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }
            return lstAssayProParamsResult;
        }
        /// <summary>
        /// 获取结果单位
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public List<string> QueryProjectResultUnits(string strDBMethod)
        {
            List<string> lstUnits = new List<string>();
            try
            {
                lstUnits = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SettingsAccess_QueryProjectResultUnits(string strDBMethod, object sender)==" + e.ToString(), Module.DAO);
            }

            return lstUnits;
        }
        /// <summary>
        /// 更新项目参数信息
        /// </summary>
        /// <returns></returns>
        public int UpdateAssayProjectParamInfo(string strDBMethod, AssayProjectParamInfo assayProParamInfo)
        {
            int i = 0;
            try
            {
                i = ism_SqlMap.Update("AssayProjectInfo." + strDBMethod, assayProParamInfo);
                if (i > 0)
                {
                    ism_SqlMap.Update("AssayProjectInfo.UpdateResultSetInfo", string.Format("update resultSetTb set Unit = '{0}', RadixPointNum = {1} where ProjectName = '{2}'", assayProParamInfo.ResultUnit, assayProParamInfo.ResultDecimal, assayProParamInfo.ProjectName));
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateAssayProjectParamInfo(string strDBMethod, AssayProjectParamInfo assayProParamInfo)==" + e.ToString(), Module.DAO);
            }
            return i;
        }
        /// <summary>
        /// 通过项目名称和项目类型获取项目校准参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public AssayProjectCalibrationParamInfo QueryCalibParamByProNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            AssayProjectCalibrationParamInfo calibParamInfo = new AssayProjectCalibrationParamInfo();

            try
            {
                calibParamInfo = (AssayProjectCalibrationParamInfo)ism_SqlMap.QueryForObject("AssayProjectInfo." + strDBMethod, assayProInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibParamByProNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return calibParamInfo;
        }
        /// <summary>
        /// 获取所有项目校准参数信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public List<AssayProjectCalibrationParamInfo> QueryCalibParamInfoAll(string strDBMethod)
        {
            List<AssayProjectCalibrationParamInfo> lstCalibParamInfo = new List<AssayProjectCalibrationParamInfo>();

            try
            {
                lstCalibParamInfo = (List<AssayProjectCalibrationParamInfo>)ism_SqlMap.QueryForList<AssayProjectCalibrationParamInfo>("AssayProjectInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibParamByProNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return lstCalibParamInfo;
        }

        /// <summary>
        /// 通过项目名称和项目类型更新项目校准参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public int UpdateCalibParamByProNameAndType(string strDBMethod, AssayProjectCalibrationParamInfo assayProInfo)
        {
            int intResult = 0;
            try
            {
                intResult = ism_SqlMap.Update("AssayProjectInfo." + strDBMethod, assayProInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalibParamByProNameAndType(string strDBMethod, AssayProjectCalibrationParamInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        /// <summary>
        /// 获取所有生化项目范围参数信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public List<AssayProjectRangeParamInfo> QueryRangeParam(string strDBMethod)
        {
            List<AssayProjectRangeParamInfo> lstRangeParamInfo = new List<AssayProjectRangeParamInfo>();

            try
            {
                lstRangeParamInfo = (List<AssayProjectRangeParamInfo>)ism_SqlMap.QueryForList<AssayProjectRangeParamInfo>("AssayProjectInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryRangeParam(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return lstRangeParamInfo;
        }
        /// <summary>
        /// 获取范围参数
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public AssayProjectRangeParamInfo GetRangeParamInfo(string projectName, string sampleType)
        {
            AssayProjectRangeParamInfo assayRangeParam = null;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName",projectName);
                ht.Add("SampleType",sampleType);
                assayRangeParam = (AssayProjectRangeParamInfo)ism_SqlMap.QueryForObject("AssayProjectInfo.QueryRangeByProject", ht);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetRangeParamInfo(string projectName, string sampleType) ==" + ex.ToString(), Module.Setting);
            }
            return assayRangeParam;
        }
        /// <summary>
        /// 通过项目名称和项目类型更新项目范围参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public int UpdateRangeParamByProNameAndType(string strDBMethod, AssayProjectRangeParamInfo assayProInfo)
        {
            int intResult = 0;
            try
            {
                intResult = (int)ism_SqlMap.Update("AssayProjectInfo." + strDBMethod, assayProInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateRangeParamByProNameAndType(string strDBMethod, AssayProjectRangeParamInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 通过去重项目名称获取所有生化项目信息,包括计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="ObjParam"></param>
        /// <returns></returns>
        public List<string> QueryAssayProAllInfoByDistinctProName(string strDBMethod)
        {
            List<string> assayProInfos = new List<string>();

            try
            {
                assayProInfos = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo." + strDBMethod, null);

                List<CalcProjectInfo> calcProInfos = (List<CalcProjectInfo>)ism_SqlMap.QueryForList<CalcProjectInfo>("CalcProjectInfo.QueryCalcProjectAllInfo", null);

                foreach (CalcProjectInfo calcProInfo in calcProInfos)
                {
                    assayProInfos.Add(calcProInfo.CalcProjectName);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryAssayProAllInfoByDistinctProName(string strDBMethod, object ObjParam)==" + e.ToString(), Module.DAO);
            }

            return assayProInfos;
        }

        /// <summary>
        /// 通过去重项目名称获取生化项目名称for计算项目页
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="ObjParam"></param>
        /// <returns></returns>
        public List<string> ProjectPageinfoForCalc(string strDBMethod, string sampleType)
        {
            List<string> assayProInfos = new List<string>();

            try
            {
                assayProInfos = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo." + strDBMethod, sampleType);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ProjectPageinfoForCalc(string strDBMethod, object ObjParam)==" + e.ToString(), Module.DAO);
            }

            return assayProInfos;
        }

        /// <summary>
        /// 获取所有组合项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<CombProjectInfo> QueryCombProjectNameAllInfo(string strDBMethod)
        {
            List<CombProjectInfo> lstCombProInfos = new List<CombProjectInfo>();

            try
            {
                ArrayList combProNameInfos = (ArrayList)ism_SqlMap.QueryForList("CombProjectInfo." + strDBMethod, null);

                foreach (ArrayList arrayLst in combProNameInfos)
                {
                    CombProjectInfo combProInfo = new CombProjectInfo();

                    combProInfo.CombProjectName = arrayLst[0].ToString();
                    combProInfo.CombProjectCount = System.Convert.ToInt32(arrayLst[1].ToString());
                    combProInfo.Remarks = arrayLst[2].ToString();

                    lstCombProInfos.Add(combProInfo);
                }
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCombProjectAllInfo(string strDBMethod)==" + e.ToString(), Module.DAO);
            }

            return lstCombProInfos;
        }

        /// <summary>
        /// 通过组合项目名称获取组合项目对应的项目列表
        /// </summary>
        /// <param name="strDBMethod">访问数据库方法名</param>
        /// <param name="CombProName">组合项目名称</param>
        /// <returns>对应组合项目的项目list</returns>
        public List<string> QueryProjectByCombProName(string strDBMethod, string CombProName)
        {
            List<string> lstProNames = new List<string>();
            try
            {
                lstProNames = (List<string>)ism_SqlMap.QueryForList<string>("CombProjectInfo." + strDBMethod, CombProName);

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProjectByCombProName(string strDBMethod, string CombProName)==" + e.ToString(), Module.DAO);
            }

            return lstProNames;
        }
        /// <summary>
        /// 获取所有组合项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<CombProjectInfo> QueryProjectAndCombProName(string strDBMethod)
        {
            List<CombProjectInfo> lstProjectAndCombProName = new List<CombProjectInfo>();
            try
            {
                lstProjectAndCombProName = (List<CombProjectInfo>)ism_SqlMap.QueryForList<CombProjectInfo>("CombProjectInfo." + strDBMethod, null);

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProjectByCombProName(string strDBMethod, string CombProName)==" + e.ToString(), Module.DAO);
            }

            return lstProjectAndCombProName;
        }

        /// <summary>
        /// 添加组合项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        public int AddCombProjectName(string strDBMethod, CombProjectInfo combProjectInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("CombProjectName", combProjectInfo.CombProjectName);
                hash.Add("CombProjectCount", combProjectInfo.CombProjectCount);
                LogInfo.WriteErrorLog(combProjectInfo.CombProjectCount.ToString(), Module.DAO);
                hash.Add("Remarks", combProjectInfo.Remarks);
                ism_SqlMap.Insert("CombProjectInfo." + strDBMethod, combProjectInfo);
                foreach (string strPro in combProjectInfo.ProjectNames)
                {
                    Hashtable ht = new Hashtable();

                    ht.Add("CombProjectName", combProjectInfo.CombProjectName);
                    ht.Add("ProjectName", strPro);

                    ism_SqlMap.Insert("CombProjectInfo.AddCombProject", ht);
                }
                intResult = 1;
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddCombProject(string strDBMethod, CombProjectInfo combProjectInfo)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }
        /// <summary>
        /// 通过组合项目名称获取条数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="strCombProName"></param>
        /// <returns></returns>
        public int CombProjectCountByCombProName(string strDBMethod, string strCombProName)
        {
            int intResult = 0;
            try
            {
                intResult = (int)ism_SqlMap.QueryForObject("CombProjectInfo." + strDBMethod, strCombProName);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("CombProjectCountByCombProName(string strDBMethod, string strCombProName)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 删除组合项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public int DeleteCombProjectName(string strDBMethod, List<CombProjectInfo> combProjectInfos)
        {
            int intResult = 0;
            try
            {
                foreach (CombProjectInfo proInfo in combProjectInfos)
                {
                    intResult += ism_SqlMap.Delete("CombProjectInfo." + strDBMethod, proInfo.CombProjectName);
                    ism_SqlMap.Delete("CombProjectInfo.DeleteCombProject", proInfo.CombProjectName);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteCombProject(string strDBMethod, CombProjectInfo combProjectInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 更新组合项目
        /// </summary>
        /// <param name="strDBMethod">访问数据库方法名</param>
        /// <param name="combProjectInfo">更新参数</param>
        /// <returns>更新条数</returns>
        public int UpdateCombProjectName(string strDBMethod, string combProjectInfoOld, CombProjectInfo combProInfoNew)
        {
            int intResult = 0;
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("CombProjectNameOld", combProjectInfoOld);
                hash.Add("CombProjectNameNew", combProInfoNew.CombProjectName);
                hash.Add("CombProjectCountNew", combProInfoNew.CombProjectCount);
                hash.Add("RemarksNew", combProInfoNew.Remarks);

                intResult = ism_SqlMap.Update("CombProjectInfo." + strDBMethod, hash);

                // 删除组合项目对应项目列表
                ism_SqlMap.Delete("CombProjectInfo.DeleteCombProject", combProjectInfoOld);
                // 插入组合项目对应项目列表
                foreach (string strProInfo in combProInfoNew.ProjectNames)
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable.Add("CombProjectName", combProInfoNew.CombProjectName);
                    hashtable.Add("ProjectName", strProInfo);
                    ism_SqlMap.Insert("CombProjectInfo.AddCombProject", hashtable);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCombProject(string strDBMethod, CombProjectInfo combProjectInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        /// <summary>
        /// 获取所有计算项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<CalcProjectInfo> QueryCalcProjectAllInfo(string strDBMethod)
        {
            List<CalcProjectInfo> lstCalcProInfos = new List<CalcProjectInfo>();

            try
            {
                lstCalcProInfos = (List<CalcProjectInfo>)ism_SqlMap.QueryForList<CalcProjectInfo>("CalcProjectInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalcProjectAllInfo(string strDBMethod)==" + e.ToString(), Module.DAO);
            }

            return lstCalcProInfos;
        }

        /// <summary>
        /// 添加计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        public void AddCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfo)
        {
            try
            {
                ism_SqlMap.Insert("CalcProjectInfo." + strDBMethod, calcProjectInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }
        }

        /// <summary>
        /// 删除计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public int DeleteCalcProject(string strDBMethod, List<CalcProjectInfo> calcProjectInfos)
        {
            int intResult = 0;
            try
            {
                foreach (CalcProjectInfo proInfo in calcProjectInfos)
                {
                    intResult += ism_SqlMap.Delete("CalcProjectInfo." + strDBMethod, proInfo);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteCalcProject(string strDBMethod, List<CalcProjectInfo> calcProjectInfos)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 更新计算项目
        /// </summary>
        /// <param name="strDBMethod">访问数据库方法名</param>
        /// <param name="combProjectInfo">更新参数</param>
        /// <returns>更新条数</returns>
        public int UpdateCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfoOld, CalcProjectInfo calcProInfoNew)
        {
            int intResult = 0;
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("CalcProjectNameOld", calcProjectInfoOld.CalcProjectName);
                hash.Add("SampleTypeOld", calcProjectInfoOld.SampleType);
                hash.Add("CalcProjectName", calcProInfoNew.CalcProjectName);
                hash.Add("CalcProjectFullName", calcProInfoNew.CalcProjectFullName);
                hash.Add("Unit", calcProInfoNew.Unit);
                hash.Add("SampleType", calcProInfoNew.SampleType);
                hash.Add("ReferenceRangeLow", calcProInfoNew.ReferenceRangeLow);
                hash.Add("ReferenceRangeHigh", calcProInfoNew.ReferenceRangeHigh);
                hash.Add("CalcFormula", calcProInfoNew.CalcFormula);

                intResult = ism_SqlMap.Update("CalcProjectInfo." + strDBMethod, hash);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfoOld, CalcProjectInfo calcProInfoNew)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 获取计算项目条数通过计算项目名称
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="calcProjectInfo"></param>
        /// <returns></returns>
        public int ProjectCountByCalcProName(string strDBMethod, CalcProjectInfo calcProjectInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("CalcProjectName", calcProjectInfo.CalcProjectName);
                hash.Add("SampleType", calcProjectInfo.SampleType);

                List<int> iRes = (List<int>)ism_SqlMap.QueryForList<int>("CalcProjectInfo." + strDBMethod, hash);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ProjectCountByCalcProName(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 更新环境参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="environmentParamInfo"></param>
        /// <returns></returns>
        public int UpdateEnvironmentParamInfo(string strDBMethod, EnvironmentParamInfo environmentParamInfo, RunningStateInfo running)
        {
            int intResult = 0;
            try
            {
                intResult = (int)ism_SqlMap.Update("EnvironmentParamInfoml." + strDBMethod, environmentParamInfo);
                Hashtable ht = new Hashtable();
                ht.Add("QCSMPContainerType", running.QCSMPContainerType);
                ht.Add("SDTSMPContainerType", running.SDTSMPContainerType);
                ht.Add("CUVBlkMax", environmentParamInfo.CuvetteBlankHigh);
                ht.Add("CUVBlkMin", environmentParamInfo.CuvetteBlankLow);
                ht.Add("RgtWarnCount", environmentParamInfo.ReagentSurplus);
                ht.Add("RgtLeastCount", environmentParamInfo.ReagentLeastVol);
                ht.Add("TempOffset", running.TempOffset);
                
                ism_SqlMap.Update("PLCDataInfo.updateRunningStateInfo", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateEnvironmentParamInfo(string strDBMethod, EnvironmentParamInfo environmentParamInfo, RunningStateInfo running)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        public List<LISCommunicateNetworkInfo> QueryLISCommunicateInfo(string strDBMethod)
        {
            List<LISCommunicateNetworkInfo> lstLISCommunicateInfos = new List<LISCommunicateNetworkInfo>();

            try
            {
                lstLISCommunicateInfos = (List<LISCommunicateNetworkInfo>)ism_SqlMap.QueryForList<LISCommunicateNetworkInfo>("LISCommunicateInfo." + strDBMethod, null);

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SettingsAccess.cs_QueryAssayProAllInfo(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }
            LogInfo.WriteProcessLog(lstLISCommunicateInfos.Count.ToString(), Module.DAO);
            return lstLISCommunicateInfos;
        }
        /// <summary>
        /// 获取串口参数信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<SerialCommunicationInfo> QuerySerialCommunicationInfo(string strDBMethod)
        {
            List<SerialCommunicationInfo> lstLISCommunicateInfos = new List<SerialCommunicationInfo>();

            try
            {
                lstLISCommunicateInfos = (List<SerialCommunicationInfo>)ism_SqlMap.QueryForList<SerialCommunicationInfo>("LISCommunicateInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SettingsAccess.cs_QueryAssayProAllInfo(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }
            LogInfo.WriteProcessLog(lstLISCommunicateInfos.Count.ToString(), Module.DAO);
            return lstLISCommunicateInfos;
        }

        public int UpdateLISCommunicateNetworkInfo(string strDBMethod, LISCommunicateNetworkInfo lISCommunicateInfo)
        {
            int intResult = 0;
            try
            {
                List<LISCommunicateNetworkInfo> lstLISCommunicateInfos = QueryLISCommunicateInfo("NetworkLISCommunicate");
                if (lstLISCommunicateInfos.Count > 0)
                    intResult = (int)ism_SqlMap.Update("LISCommunicateInfo." + strDBMethod, lISCommunicateInfo);
                else
                {
                    ism_SqlMap.Insert("LISCommunicateInfo.AddNetworkLISCommunicate", lISCommunicateInfo);
                    intResult = 1;
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLISCommunicateInfo(string strDBMethod, EnvironmentParamInfo environmentParamInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        public int UpdateLISCommunicateSerialInfo(string strDBMethod, SerialCommunicationInfo serialCommunicationInfo)
        {
            int intResult = 0;
            try
            {
                List<SerialCommunicationInfo> lstLISCommunicateInfos = QuerySerialCommunicationInfo("SerialLISCommunicate");
                if (lstLISCommunicateInfos.Count > 0)
                    intResult = (int)ism_SqlMap.Update("LISCommunicateInfo." + strDBMethod, serialCommunicationInfo);
                else
                {
                    ism_SqlMap.Insert("LISCommunicateInfo.AddSerialLISCommunicate", serialCommunicationInfo);
                    intResult = 1;
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLISCommunicateInfo(string strDBMethod, EnvironmentParamInfo environmentParamInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        /// <summary>
        /// 获取所有数据结果单位信息 
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<string> QueryDataConfig(string strDBMethod)
        {

            List<string> lstQueryDataConfig = new List<string>();
            try
            {
                lstQueryDataConfig = (List<string>)ism_SqlMap.QueryForList<string>("DataConfig." + strDBMethod, null);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }
           
            return lstQueryDataConfig;
        }

        public void DataConfigadd(string strDBMethod, string dataConfig)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("dataConfig", dataConfig);
            try
            {
                ism_SqlMap.Insert("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }
            
        }

        public int SelectDataConfig(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("dataConfig", dataConfig);
                intResult= (int)ism_SqlMap.QueryForObject("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int UpdateDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("dataConfig", dataConfig);
                hashTable.Add("dataConfigOld", dataConfigOld);

                intResult = (int)ism_SqlMap.Update("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        public int DeleteDataConfig(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("dataConfig", dataConfig);
                intResult = (int)ism_SqlMap.Delete("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int SelectReagentNeedle(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ReagentNeedle", reagentNeedleAntifoulingStrategyInfo.ReagentNeedle);
                hashTable.Add("PolluteSourcePro", reagentNeedleAntifoulingStrategyInfo.PolluteProName);
                hashTable.Add("PolluteProType", reagentNeedleAntifoulingStrategyInfo.PolluteProType);
                hashTable.Add("BePollutedPro", reagentNeedleAntifoulingStrategyInfo.BePollutedProName);
                hashTable.Add("BePollutedProType", reagentNeedleAntifoulingStrategyInfo.BePollutedProType);
                hashTable.Add("CleaningLiquidName", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidName);
                hashTable.Add("CleaningLiquidUseVol", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidUseVol);
                hashTable.Add("CleanTimes", reagentNeedleAntifoulingStrategyInfo.CleanTimes);
                intResult = (int)ism_SqlMap.QueryForObject("ReagentNeedleInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectReagentNeedle(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public void ReagentNeedleadd(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("ReagentNeedle", reagentNeedleAntifoulingStrategyInfo.ReagentNeedle);
            hashTable.Add("PolluteSourcePro", reagentNeedleAntifoulingStrategyInfo.PolluteProName);
            hashTable.Add("PolluteProType", reagentNeedleAntifoulingStrategyInfo.PolluteProType);
            hashTable.Add("BePollutedPro", reagentNeedleAntifoulingStrategyInfo.BePollutedProName);
            hashTable.Add("BePollutedProType", reagentNeedleAntifoulingStrategyInfo.BePollutedProType);
            hashTable.Add("CleaningLiquidName", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidName);
            hashTable.Add("CleaningLiquidUseVol", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidUseVol);
            hashTable.Add("CleanTimes", reagentNeedleAntifoulingStrategyInfo.CleanTimes);
            try
            {
                ism_SqlMap.Insert("ReagentNeedleInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public void ReagentNeedleadd(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)==" + e.ToString(), Module.DAO);
            }
            
        }
        /// <summary>
        /// 获取所有防污试剂针
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<ReagentNeedleAntifoulingStrategyInfo> QueryReagentNeedle(string strDBMethod)
        {
            List<ReagentNeedleAntifoulingStrategyInfo> lstQueryReagentNeedle = new List<ReagentNeedleAntifoulingStrategyInfo>();
            try
            {
                lstQueryReagentNeedle = (List<ReagentNeedleAntifoulingStrategyInfo>)ism_SqlMap.QueryForList<ReagentNeedleAntifoulingStrategyInfo>("ReagentNeedleInfo." + strDBMethod,null);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryReagentNeedle;
        }

        public List<ReagentNeedleAntifoulingStrategyInfo> QueryReagentNeedleByR1R2(string strDBMethod, string reagentNeedleName)
        {
            List<ReagentNeedleAntifoulingStrategyInfo> lstQueryReagentNeedle = new List<ReagentNeedleAntifoulingStrategyInfo>();
            try
            {
                lstQueryReagentNeedle = (List<ReagentNeedleAntifoulingStrategyInfo>)ism_SqlMap.QueryForList<ReagentNeedleAntifoulingStrategyInfo>("ReagentNeedleInfo." + strDBMethod, reagentNeedleName);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryReagentNeedle;
        }

        public int DeleteReagentNeedle(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ReagentNeedle", reagentNeedleAntifoulingStrategyInfo.ReagentNeedle);
                hashTable.Add("PolluteSourcePro", reagentNeedleAntifoulingStrategyInfo.PolluteProName);
                hashTable.Add("PolluteProType", reagentNeedleAntifoulingStrategyInfo.PolluteProType);
                hashTable.Add("BePollutedPro", reagentNeedleAntifoulingStrategyInfo.BePollutedProName);
                hashTable.Add("BePollutedProType", reagentNeedleAntifoulingStrategyInfo.BePollutedProType);
                hashTable.Add("CleaningLiquidName", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidName);
                hashTable.Add("CleaningLiquidUseVol", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidUseVol);
                hashTable.Add("CleanTimes", reagentNeedleAntifoulingStrategyInfo.CleanTimes);
                intResult = (int)ism_SqlMap.Delete("ReagentNeedleInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public string UpdateReagentNeedle(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfoOld)
        {
            string strResult = "";
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("ReagentNeedle", reagentNeedleAntifoulingStrategyInfo.ReagentNeedle);
                hash.Add("PolluteSourcePro", reagentNeedleAntifoulingStrategyInfo.PolluteProName);
                hash.Add("PolluteProType", reagentNeedleAntifoulingStrategyInfo.PolluteProType);
                hash.Add("BePollutedPro", reagentNeedleAntifoulingStrategyInfo.BePollutedProName);
                hash.Add("BePollutedProType", reagentNeedleAntifoulingStrategyInfo.BePollutedProType);
                hash.Add("CleaningLiquidName", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidName);
                hash.Add("CleaningLiquidUseVol", reagentNeedleAntifoulingStrategyInfo.CleaningLiquidUseVol);
                hash.Add("CleanTimes", reagentNeedleAntifoulingStrategyInfo.CleanTimes);
                hash.Add("ReagentNeedleOld", reagentNeedleAntifoulingStrategyInfoOld.ReagentNeedle);
                hash.Add("PolluteSourceProOld", reagentNeedleAntifoulingStrategyInfoOld.PolluteProName);
                hash.Add("PolluteProTypeOld", reagentNeedleAntifoulingStrategyInfoOld.PolluteProType);
                hash.Add("BePollutedProOld", reagentNeedleAntifoulingStrategyInfoOld.BePollutedProName);
                hash.Add("BePollutedProTypeOld", reagentNeedleAntifoulingStrategyInfoOld.BePollutedProType);

                int i = SelectReagentNeedle("QueryReagentNeedleAdd", reagentNeedleAntifoulingStrategyInfo);
                if (i > 0)
                {
                    strResult = "该试剂针防污策略已存在，保存失败！";
                    return strResult;
                }
                int iUpdate = ism_SqlMap.Update("ReagentNeedleInfo." + strDBMethod, hash);

                if (iUpdate > 0)
                {
                    strResult = "保存成功！";
                }
                else
                {
                    strResult = "保存失败！";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateReagentNeedle(string strDBMethod, CalcProjectInfo calcProjectInfoOld, CalcProjectInfo calcProInfoNew)==" + e.ToString(), Module.DAO);
            }

            return strResult;
        }

        public List<EnvironmentParamInfo> QueryEnvironmentParamInfo(string strDBMethod)
        {
            List<EnvironmentParamInfo> lstEnvironmentInfos = new List<EnvironmentParamInfo>();
            try
            {
                lstEnvironmentInfos = (List<EnvironmentParamInfo>)ism_SqlMap.QueryForList<EnvironmentParamInfo>("EnvironmentParamInfoml." + strDBMethod, null);                
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryEnvironmentParamInfo(string strDBMethod)" + e.ToString(), Module.DAO);
            }

            return lstEnvironmentInfos;
        }
        public RunningStateInfo QueryRuningSateInfo(string strDBMethod)
        {
            RunningStateInfo runningstateinfo = new RunningStateInfo();
            try
            {
                runningstateinfo = (RunningStateInfo)ism_SqlMap.QueryForObject("EnvironmentParamInfoml." + strDBMethod, null);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryRuningSateInfo(string strDBMethod)" + e.ToString(), Module.DAO);
            }

            return runningstateinfo;
        }
        /// <summary>
        /// 获取所有稀释比例信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<string> QueryDilutionRatio(string strDBMethod)
        {
            List<string> lstQueryDilutionRatio = new List<string>();
            try
            {
                lstQueryDilutionRatio = (List<string>)ism_SqlMap.QueryForList<string>("DataConfig.QueryDilutionRatio", null);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryDilutionRatio;
        }

        public int SelectDilutionRatio(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("DilutionRatio", dataConfig);
                intResult = (int)ism_SqlMap.QueryForObject("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }
        /// <summary>
        /// 添加稀释比例参数信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="dataConfig"></param>
        public void DilutionRatioadd(string strDBMethod, string dataConfig)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("DilutionRatio", dataConfig);
            try
            {
                ism_SqlMap.Insert("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public int UpdataDilutionRatio(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("DilutionRatio", dataConfig);
                hashTable.Add("DilutionRatioOld", dataConfigOld);

                intResult = (int)ism_SqlMap.Update("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        public int DeleteDilutionRatio(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("DilutionRatio", dataConfig);
                intResult = (int)ism_SqlMap.Delete("DataConfig." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

    
        public List<UserAuthorization> UserAuthorization(string strDBMethod, string dataConfig)
        {
            List<UserAuthorization> lstQueryUserManagement = new List<UserAuthorization>();
            try
            {
                if (dataConfig == null)
                {
                    lstQueryUserManagement = (List<UserAuthorization>)ism_SqlMap.QueryForList<UserAuthorization>("UserInfo." + strDBMethod, dataConfig);
                }
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryUserManagement;
        }

        public string QueryUnitAndRangeByProject(string strDBMethod, Hashtable ht)
        {
            string unitAndRange = string.Empty;
            try
            {
                //ht.Add("SampleNum", strConditions[0]);
                //ht.Add("DateTime", strConditions[1]);
                //ht.Add("ProjectName", sampleResInfo.ProjectName);
                //ht.Add("SampleType", strConditions[2]);
                string unit = (string)ism_SqlMap.QueryForObject("AssayProjectInfo.QueryUnitByProject", ht);
                unit = unit == null ? "" : unit;
                

                Hashtable hash = new Hashtable();
                hash.Add("SampleNum", ht["SampleNum"]);
                hash.Add("starttime", System.Convert.ToDateTime(ht["DateTime"]).ToShortDateString());
                hash.Add("endtime", System.Convert.ToDateTime(ht["DateTime"]).AddDays(1).ToShortDateString());

                PatientInfo patientInfo = (PatientInfo)ism_SqlMap.QueryForObject("WorkAreaApplyTask.QueryPatientInfoBySampleNum", hash);

                patientInfo = patientInfo == null ? new PatientInfo() : patientInfo;

                AssayProjectRangeParamInfo projectRangeInfo = (AssayProjectRangeParamInfo)ism_SqlMap.QueryForObject("AssayProjectInfo.QueryRangeByProject", ht);

                projectRangeInfo = projectRangeInfo == null ? new AssayProjectRangeParamInfo() : projectRangeInfo;

                // 年龄、性别 判断范围
                int age = patientInfo.Age;
                string sex = patientInfo.Sex;

                float lowestValue = -100000000, highest = 100000000;
                if (age >= projectRangeInfo.AgeLow1 && age <= projectRangeInfo.AgeHigh1)
                {
                    if (sex == "男")
                    {
                        lowestValue = projectRangeInfo.ManConsLow1;
                        highest = projectRangeInfo.ManConsHigh1;
                    }
                    else if (sex == "女")
                    {
                        lowestValue = projectRangeInfo.WomanConsLow1;
                        highest = projectRangeInfo.WomanConsHigh1;
                    }
                }
                else if (projectRangeInfo.AgeLow2 > -100000000)
                {
                    if (age >= projectRangeInfo.AgeLow2 && age <= projectRangeInfo.AgeHigh2)
                    {
                        if (sex == "男")
                        {
                            lowestValue = projectRangeInfo.ManConsLow2;
                            highest = projectRangeInfo.ManConsHigh2;
                        }
                        else if (sex == "女")
                        {
                            lowestValue = projectRangeInfo.WomanConsLow2;
                            highest = projectRangeInfo.WomanConsHigh2;
                        }
                    }
                }
                else if (projectRangeInfo.AgeLow3 > -100000000)
                {
                    if (age >= projectRangeInfo.AgeLow3 && age <= projectRangeInfo.AgeHigh3)
                    {
                        if (sex == "男")
                        {
                            lowestValue = projectRangeInfo.ManConsLow3;
                            highest = projectRangeInfo.ManConsHigh3;
                        }
                        else if (sex == "女")
                        {
                            lowestValue = projectRangeInfo.WomanConsLow3;
                            highest = projectRangeInfo.WomanConsHigh3;
                        }
                    }
                }
                else if (projectRangeInfo.AgeLow4 > -100000000)
                {
                    if (age >= projectRangeInfo.AgeLow4 && age <= projectRangeInfo.AgeHigh4)
                    {
                        if (sex == "男")
                        {
                            lowestValue = projectRangeInfo.ManConsLow4;
                            highest = projectRangeInfo.ManConsHigh4;
                        }
                        else if (sex == "女")
                        {
                            lowestValue = projectRangeInfo.WomanConsLow4;
                            highest = projectRangeInfo.WomanConsHigh4;
                        }
                    }
                }


                if (lowestValue != -100000000 && highest != 100000000)
                {
                    unitAndRange = unit + "(" + lowestValue.ToString() + "—" + highest.ToString() + ")";
                }
                else
                {
                    unitAndRange = unit;
                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryUnitAndRangeByProject(string strDBMethod, Hashtable ht)" + e.ToString(), Module.DAO);
            }

            return unitAndRange;
        }
        /// <summary>
        /// 该主函数没有被调用
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public List<CalibrationCurveInfo> QueryCalibrationCurve(string strDBMethod, string p2)
        {
            List<CalibrationCurveInfo> lstQueryCalibrationCurve = new List<CalibrationCurveInfo>();
            try
            {
                Hashtable hashTable = new Hashtable();

                hashTable.Add("ProjectName", p2);
                lstQueryCalibrationCurve = (List<CalibrationCurveInfo>)ism_SqlMap.QueryForList<CalibrationCurveInfo>("Calibrator." + strDBMethod, hashTable);

                LogInfo.WriteProcessLog(lstQueryCalibrationCurve + "zhuszihe22", Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.DAO);
            }


            return lstQueryCalibrationCurve;
        }
        /// <summary>
        /// 获取所有清洗液信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<string> QueryWashingLiquid(string strDBMethod)
        {
            List<string> lstResult = new List<string>();
            try
            {
                lstResult = (List<string>)ism_SqlMap.QueryForList<string>("ReagentNeedleInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {

            }
            return lstResult;
        }
       
        public List<SampleInfo> QuerySamplePanelState(string strMethodName, string Panel)
        {
            List<SampleInfo> lstSampleInfo = new List<SampleInfo>();
            try
            {
                Hashtable ht = new Hashtable();
                //ht.PanelNum=#PanelNum# and CreateTime between #starttime# and #endtime#
                ht.Add("PanelNum", Panel);
                ht.Add("starttime", DateTime.Now.Date);
                ht.Add("endtime", DateTime.Now.AddDays(1).Date);
                lstSampleInfo = (List<SampleInfo>)ism_SqlMap.QueryForList<SampleInfo>("WorkAreaApplyTask." + strMethodName, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QuerySamplePanelState(string strMethodName, string Panel)==" + e.ToString(), Module.DAO);
            }

            return lstSampleInfo;
        }

        public void UpdateRunningTaskWorDisk(string strMethodName, string panelNum)
        {
            try
            {
                ism_SqlMap.Update("WorkAreaApplyTask." + strMethodName, panelNum);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateRunningTaskWorDisk(string strMethodName, string panelNum)==" +e.ToString(), Module.DAO);
            }
        }

        public void BackupLastestToHistory()
        {
            try
            {
                ism_SqlMap.Delete("EquipmentManage.BackupLastestToHistory", null);
                this.SavePreCuvBlkOfWave(this.GetLatestCuvBlkOfWave());
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("BackupLastestToHistory()==" + e.ToString(), Module.DAO);
            }
        }

        private void SavePreCuvBlkOfWave(List<CuvetteBlankInfo> blks)
        {
            try
            {
                foreach (CuvetteBlankInfo blk in blks)
                {
                    int count = (int)ism_SqlMap.QueryForObject("EquipmentManage.QueryOldCuvBlkByWave", blk.WaveLength);
                    if (count > 0)
                    {
                        ism_SqlMap.Update("EquipmentManage.UpdateOldCuvBlkByWave", blk);
                    }
                    else
                    {
                        ism_SqlMap.Insert("EquipmentManage.InsertOldCuvBlkByWave", blk);
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SavePreCuvBlkOfWave(List<CuvetteBlankInfo> blks) ==" + e.ToString(), Module.DAO);
            }
        }

        public List<CuvetteBlankInfo> GetLatestCuvBlkOfWave()
        {
            List<CuvetteBlankInfo> blks = new List<CuvetteBlankInfo>();

            blks = (List<CuvetteBlankInfo>)ism_SqlMap.QueryForList<CuvetteBlankInfo>("EquipmentManage.GetLatestCuvBlkOfWave", null);

            return blks;
        }

        public void ClearupCuvNewBlk()
        {
            try
            {
                ism_SqlMap.Delete("EquipmentManage.ClearupCuvNewBlk", null);
            }
            catch(Exception e)
            {
                LogInfo.WriteErrorLog("ClearupCuvNewBlk()==" + e.ToString(), Module.DAO);
            }

        }


        public void SaveLatestCuvBlkOfWaveAndCuvNO(int w, int cuv, float blk)
        {
            try
            {
                int count = (int)ism_SqlMap.QueryForObject("EquipmentManage.QueryNewCuvBlkByWave", w);
                if (count > 0)
                {
                    Hashtable ht = new Hashtable();
                    string str = string.Format("Cuv{0}Blk", cuv.ToString());
                    ht.Add("WaveLength", w);
                    ht.Add(str, blk);

                    ism_SqlMap.Update("EquipmentManage.UpdateNewCuvBlkByWave", ht);
                }
                else
                {
                    ism_SqlMap.Insert("EquipmentManage.InsertNewCuvBlkByWave", w);

                    Hashtable ht = new Hashtable();
                    string str = string.Format("Cuv{0}Blk", cuv.ToString());
                    ht.Add("WaveLength", w);
                    ht.Add(str, blk);

                    ism_SqlMap.Update("EquipmentManage.UpdateNewCuvBlkByWave", ht);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SaveLatestCuvBlkOfWaveAndCuvNO(int w, int cuv, float blk)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 获取试剂报警余量
        /// </summary>
        /// <returns></returns>
        public float GetRgtWarnCount()
        {
            float fRgtWarnCount = 0;
            try
            {
                fRgtWarnCount = (float)ism_SqlMap.QueryForObject("EnvironmentParamInfoml.GetRgtWarnCount", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetRgtWarnCount()==" + e.ToString(), Module.DAO);
            }

            return fRgtWarnCount;        }
        /// <summary>
        /// 获取试剂最小余量
        /// </summary>
        /// <returns></returns>
        public float GetRgtLeastCount()
        {
            float fRgtLeastCount = 0;
            try
            {
                fRgtLeastCount = (float)ism_SqlMap.QueryForObject("EnvironmentParamInfoml.GetRgtLeastCount", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetRgtLeastCount()==" + e.ToString(), Module.DAO);
            }

            return fRgtLeastCount;
        }
        /// <summary>
        /// 获取项目测试顺序表中的所有数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<ProjectRunSequenceInfo> QueryAllProjectRunSequenceInfo(string strDBMethod)
        {
            List<ProjectRunSequenceInfo> lstProjectName = new List<ProjectRunSequenceInfo>();
            try
            {
                lstProjectName = (List<ProjectRunSequenceInfo>)ism_SqlMap.QueryForList<ProjectRunSequenceInfo>("PLCDataInfo." + strDBMethod, null);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("List<ProjectRunSequenceInfo> QueryAllProjectRunSequenceInfo(string strDBMethod) == " + ex.ToString(), Module.Setting);
            }
            return lstProjectName;
        }
        /// <summary>
        /// 删除项目测试顺序表所有数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        public void DeleteAllProjectRunSequenceInfo(string strDBMethod)
        {
            try
            {
                ism_SqlMap.Delete("PLCDataInfo." + strDBMethod, null);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("void DeleteAllProjectRunSequenceInfo(string strDBMethod) == " + ex.ToString(), Module.Setting);
            }
        }
        /// <summary>
        /// 保存项目测试顺序信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="lstProSunSequence"></param>
        public void SaveProjectRunSequenceInfo(string strDBMethod, ProjectRunSequenceInfo ProSunSequence)
        {
            Hashtable ht = new Hashtable();
            ht.Add("projectName", ProSunSequence.ProjectName);
            ht.Add("sampleType", ProSunSequence.SampleType);
            ht.Add("runSequence", ProSunSequence.RunSequence);
            try
            {
                ism_SqlMap.Insert("PLCDataInfo." + strDBMethod, ht);
                
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("SaveProjectRunSequenceInfo(string strDBMethod, List<ProjectRunSequenceInfo> lstProSunSequence)", Module.Setting);
            }
        }
    }
     
}

