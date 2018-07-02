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
        /// <summary>
        /// 查找条数通过项目name和项目类型
        /// </summary>
        /// <param name="strAccessDBMethod">方法名</param>
        /// <param name="assayProject">参数</param>
        /// <returns></returns>

        public int SelectAssayProCountByNameAndType(string strAccessDBMethod, AssayProjectInfo assayProject)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("ProjectName", assayProject.ProjectName);
            hashTable.Add("SampleType", assayProject.SampleType);
            return (int)ism_SqlMap.QueryForObject("AssayProjectInfo." + strAccessDBMethod, hashTable);
        }
        public int DeleteAssayProCountByNameAndType(string strAccessDBMethod, List<AssayProjectInfo> assayProject)
        {
            int deletecount = 0;
            for (int i = 0; i <= assayProject.Count - 1; i++)
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ProjectName", assayProject[i].ProjectName);
                hashTable.Add("SampleType", assayProject[i].SampleType);
                deletecount += (int)ism_SqlMap.Delete("AssayProjectInfo." + strAccessDBMethod, hashTable);
            }
            return deletecount;
        }
        public int UpdateAssayProCountByNameAndType(string strAccessDBMethod, AssayProjectInfo assayProject1, AssayProjectInfo assayProject2)
        {


            Hashtable hash = new Hashtable();

            hash.Add("ProShortName1", assayProject1.ProjectName);
            hash.Add("ProShortName2", assayProject2.ProjectName);
            hash.Add("SampleType1", assayProject1.SampleType);
            hash.Add("SampleType2", assayProject2.SampleType);
            hash.Add("ProLongName2", assayProject2.ProFullName);
            hash.Add("ChannelNumber2", assayProject2.ChannelNum);

            return (int)ism_SqlMap.Update("AssayProjectInfo." + strAccessDBMethod, hash);
        }
        /// <summary>
        /// 添加生化项目
        /// </summary>
        /// <param name="strAccessDBMethod">访问数据库方法名</param>
        /// <param name="assayProject">添加项目信息</param>
        /// <returns></returns>
        public void AddAssayProject(string strAccessDBMethod, AssayProjectInfo assayProject)
        {
            ism_SqlMap.Insert("AssayProjectInfo." + strAccessDBMethod, assayProject);
            ism_SqlMap.Insert("AssayProjectInfo.AddAssayProjectparamInfo", assayProject);
            LogInfo.WriteProcessLog("Insert生化项目" + assayProject.ProjectName, Module.DAO);
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

                    lstAssayProInfos = (List<AssayProjectInfo>)ism_SqlMap.QueryForList<AssayProjectInfo>(strDBMethod, hash);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SettingsAccess.cs_QueryAssayProAllInfo(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }
            LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.DAO);
            return lstAssayProInfos;
        }        /// <summary>
        /// 获取生化项目参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns>项目参数实体类</returns>
        public AssayProjectParamInfo GetAssayProjectParamInfoByNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            AssayProjectParamInfo assayProParam = new AssayProjectParamInfo();
            List<ReagentSettingsInfo> reagentSettings = new List<ReagentSettingsInfo>();
            List<ReagentStateInfo> proReagentState = new List<ReagentStateInfo>();
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("ProjectName", assayProInfo.ProjectName);
                hash.Add("SampleType", assayProInfo.SampleType);

                assayProParam = (AssayProjectParamInfo)ism_SqlMap.QueryForObject("AssayProjectInfo." + strDBMethod, assayProInfo);

                hash.Clear();
                hash.Add("ProjectName", assayProInfo.ProjectName);
                reagentSettings = (List<ReagentSettingsInfo>)ism_SqlMap.QueryForList<ReagentSettingsInfo>("ReagentInfo.GetReagentSettingsInfo", hash);

                proReagentState = (List<ReagentStateInfo>)ism_SqlMap.QueryForList<ReagentStateInfo>("ReagentInfo.GetReagentStateInfo", assayProInfo.ProjectName);

                foreach (ReagentSettingsInfo ReaInfo in reagentSettings)
                {
                    if (ReaInfo.ReagentType == "R1")
                    {
                        assayProParam.Reagent1Name = ReaInfo.ReagentName;
                        assayProParam.Reagent1Pos = ReaInfo.Pos;
                        assayProParam.Reagent1ValidDate = ReaInfo.ValidDate;
                    }
                    if (ReaInfo.ReagentType == "R2")
                    {
                        assayProParam.Reagent2Name = ReaInfo.ReagentName;
                        assayProParam.Reagent2Pos = ReaInfo.Pos;
                        assayProParam.Reagent2ValidDate = ReaInfo.ValidDate;
                    }
                }

                foreach (ReagentStateInfo reaState in proReagentState)
                {
                    if (reaState.ReagentName == assayProParam.Reagent1Name)
                    {
                        assayProParam.Reagent1Vol = reaState.ReagentSurplusVol;
                    }
                    if (reaState.ReagentName == assayProParam.Reagent2Name)
                    {
                        assayProParam.Reagent2Vol = reaState.ReagentSurplusVol;
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
        /// 获取结果单位
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public List<string> QueryProjectResultUnits(string strDBMethod, object sender)
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
                intResult = (int)ism_SqlMap.QueryForObject("AssayProjectInfo." + strDBMethod, assayProInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalibParamByProNameAndType(string strDBMethod, AssayProjectCalibrationParamInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        /// <summary>
        /// 通过项目名称和项目类型获取项目范围参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public AssayProjectRangeParamInfo QueryRangeParamByProNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            AssayProjectRangeParamInfo rangeParamInfo = new AssayProjectRangeParamInfo();

            try
            {
                rangeParamInfo = (AssayProjectRangeParamInfo)ism_SqlMap.QueryForObject("AssayProjectInfo." + strDBMethod, assayProInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryRangeParamByProNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return rangeParamInfo;
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
                intResult = (int)ism_SqlMap.QueryForObject("AssayProjectInfo." + strDBMethod, assayProInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateRangeParamByProNameAndType(string strDBMethod, AssayProjectRangeParamInfo assayProInfo)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        public List<AssayProjectInfo> QueryAssayProAllInfoByDistinctProName(string strDBMethod, object ObjParam)
        {
            List<AssayProjectInfo> assayProInfos = new List<AssayProjectInfo>();

            try
            {
                assayProInfos = (List<AssayProjectInfo>)ism_SqlMap.QueryForList<AssayProjectInfo>("AssayProjectInfo." + strDBMethod, ObjParam);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryAssayProAllInfoByDistinctProName(string strDBMethod, object ObjParam)==" + e.ToString(), Module.DAO);
            }

            return assayProInfos;
        }

    }
}

