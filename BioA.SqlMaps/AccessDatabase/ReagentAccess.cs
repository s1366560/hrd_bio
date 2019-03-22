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
        public List<ReagentSettingsInfo> QueryReagentSettingsInfo(string strDBMethod, string dataConfig)
        {
            List<ReagentSettingsInfo> lstReagentSettingsInfo = new List<ReagentSettingsInfo>();
            try
            {
                lstReagentSettingsInfo = (List<ReagentSettingsInfo>)ism_SqlMap.QueryForList<ReagentSettingsInfo>("ReagentInfo." + strDBMethod, dataConfig);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstReagentSettingsInfo;
        }
        public List<ReagentSettingsInfo> QueryReagentSettingsInfo2(string strDBMethod, string dataConfig)
        {
            List<ReagentSettingsInfo> lstReagentSettingsInfo = new List<ReagentSettingsInfo>();
            try
            {
                if (dataConfig == null)
                {
                    lstReagentSettingsInfo = (List<ReagentSettingsInfo>)ism_SqlMap.QueryForList<ReagentSettingsInfo>("ReagentInfo." + strDBMethod, dataConfig);

                }
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstReagentSettingsInfo;
        }


        public string AddreagentSettingInfo(string strDBMethod, ReagentSettingsInfo reagentSettingsInfo)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("Barcode", reagentSettingsInfo.Barcode);
            hashTable.Add("BatchNum", reagentSettingsInfo.BatchNum);
            hashTable.Add("Pos", reagentSettingsInfo.Pos);
            hashTable.Add("ProjectName", reagentSettingsInfo.ProjectName);
            hashTable.Add("ReagentContainer", reagentSettingsInfo.ReagentContainer);
            hashTable.Add("ReagentName", reagentSettingsInfo.ReagentName);
            hashTable.Add("ValidDate", reagentSettingsInfo.ValidDate);
            hashTable.Add("ReagentType", reagentSettingsInfo.ReagentType);

            try
            { 
                ism_SqlMap.Insert("ReagentInfo." + strDBMethod, hashTable);
                return "试剂R1装载成功！";
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
                return "试剂装载失败！";

            }
        }
        public string AddreagentSettingInfo2(string strDBMethod, ReagentSettingsInfo reagentSettingsInfo)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("Barcode", reagentSettingsInfo.Barcode);
            hashTable.Add("BatchNum", reagentSettingsInfo.BatchNum);
            hashTable.Add("Pos", reagentSettingsInfo.Pos);
            hashTable.Add("ProjectName", reagentSettingsInfo.ProjectName);
            hashTable.Add("ReagentContainer", reagentSettingsInfo.ReagentContainer);
            hashTable.Add("ReagentName", reagentSettingsInfo.ReagentName);
            hashTable.Add("ValidDate", reagentSettingsInfo.ValidDate);
            hashTable.Add("ReagentType", reagentSettingsInfo.ReagentType);




            try
            {
                ism_SqlMap.Insert("ReagentInfo." + strDBMethod, hashTable);
                return "试剂R2装载成功！";
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
                return "试剂装载失败";

            }
        }

        /// <summary>
        /// 删除清洗剂
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="DeletereagentSettingsInfo"></param>
        /// <returns></returns>
        public int DeletereagentSettingsInfoAndStateInfo(string reagentNum, ReagentSettingsInfo r)
        {
            int result = 0;
            try
            {
                if (reagentNum == "R1")
                {
                    ism_SqlMap.QueryForObject("ReagentInfo.deleteReagentStatInfo", string.Format("delete from ReagentStateInfoR1R2Tb where ReagentName = '{0}' and Pos = '{1}' ", r.ReagentName,r.Pos));
                    result = (int)ism_SqlMap.Delete("ReagentInfo.deleteReagentSettingInfo", string.Format("delete from ReagentSettingsTb where ReagentName = '{0}' Pos = '{1}'", r.ReagentName, r.Pos));
                }
                else
                {
                    ism_SqlMap.QueryForObject("ReagentInfo.deleteReagentStatInfo", string.Format("delete from ReagentStateInfoR1R2Tb where ReagentName2 = '{0}' Pos2 = '{1}'", r.ReagentName,r.Pos));
                    result = (int)ism_SqlMap.Delete("ReagentInfo.deleteReagentSettingInfo", string.Format("delete from ReagentSettingsTbR2 where ReagentName = '{0}' Pos = '{1}'", r.ReagentName,r.Pos));
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("DeletereagentSettingsInfoAndStateInfo(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo) ==" + ex.ToString(), Module.Reagent);
            }
            return result;
        }

        /// <summary>
        /// 根据项目名称删除试剂1表中对应的数据 
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="DeletereagentSettingsInfo"></param>
        /// <returns></returns>
        public int DeletereagentSettingsInfo(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ProjectName", DeletereagentSettingsInfo.ProjectName);

                intResult = (int)ism_SqlMap.Delete("ReagentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeletereagentSettingsInfo(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int DeletereagentSettingsInfo2(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ProjectName", DeletereagentSettingsInfo.ProjectName);

                intResult = (int)ism_SqlMap.Delete("ReagentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }
        /// <summary>
        /// 获取所有R1、R2试剂状态信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<ReagentStateInfoR1R2> QueryReagentStateInfo(string strDBMethod)
        {
            List<ReagentStateInfoR1R2> lstReagentStateInfo = new List<ReagentStateInfoR1R2>();
            List<int> lstInt = new List<int>();
            List<ReagentStateInfoR1R2> lstReagentR1R2StateInfo = new List<ReagentStateInfoR1R2>();
            List<ReagentStateInfoR1R2> lstNotSortReagentProjectName = new List<ReagentStateInfoR1R2>();
            try
            {
                lstReagentStateInfo = (List<ReagentStateInfoR1R2>)ism_SqlMap.QueryForList<ReagentStateInfoR1R2>("ReagentInfo." + strDBMethod, null);
                foreach (ReagentStateInfoR1R2 reagentStateR1R2 in lstReagentStateInfo)
                {
                    int s = reagentStateR1R2.ProjectName.IndexOf('.');
                    if (s < 0)
                    {
                        lstNotSortReagentProjectName.Add(reagentStateR1R2);
                        continue;
                    }
                    lstInt.Add(Convert.ToInt32(reagentStateR1R2.ProjectName.Substring(0, s)));
                }
                lstInt.Sort();
                foreach (int i in lstInt)
                {
                    foreach (ReagentStateInfoR1R2 reagentStateR1R2 in lstReagentStateInfo)
                    {
                        int s = reagentStateR1R2.ProjectName.IndexOf('.');
                        if (s < 0)
                        {
                            continue;
                        }
                        if (i == Convert.ToInt32(reagentStateR1R2.ProjectName.Substring(0, s)))
                        {
                            lstReagentR1R2StateInfo.Add(reagentStateR1R2);
                        }
                    }
                }
                lstReagentR1R2StateInfo.AddRange(lstNotSortReagentProjectName);
            }                 

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }
            return lstReagentR1R2StateInfo;
        }

        public string AddreagentStateInfoR1R2(string strDBMethod, ReagentStateInfoR1R2 reagentStateInfoR1R2)
        {
            string strResult = "添加成功！";

            try
            {
                ism_SqlMap.Insert("ReagentInfo." + strDBMethod, reagentStateInfoR1R2);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
                strResult = "添加失败！";

            }

            return strResult;
        }

        public void UpdateReagent1State(string strMethodName, ReagentStateInfoR1R2 reagentState)
        {
            try
            {
                ism_SqlMap.Update("ReagentInfo." + strMethodName, reagentState);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateReagent1State(string strMethodName, ReagentStateInfoR1R2 reagentState)==" + e.ToString(), Module.DAO);
            }
        }

        public ReagentStateInfoR1R2 QueryReagentStateInfoByProjectName(string strMethodName, ReagentSettingsInfo reagentSettingsInfo)
        {
            ReagentStateInfoR1R2 reagentState = new ReagentStateInfoR1R2();
            try
            {
                reagentState = (ReagentStateInfoR1R2)ism_SqlMap.QueryForObject("ReagentInfo." + strMethodName, reagentSettingsInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryReagentStateInfoByProjectName(string strMethodName, ReagentSettingsInfo reagentSettingsInfo)==" + e.ToString(), Module.DAO);
            }
            return reagentState;
        }
        /// <summary>
        /// 修改试剂锁定/解锁状态：
        ///     1.如果（strResult > 0）：修改成功，然后再重新获取所有试剂状态信息
        ///     2.如果（strResult = 0）：修改失败，直接返回空对象。
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="lstReagentStateInfo"></param>
        /// <returns></returns>
        public List<ReagentStateInfoR1R2> UpdataReagentStateInfo(string strDBMethod, List<ReagentStateInfoR1R2> lstReagentStateInfo)
        {
            List<ReagentStateInfoR1R2> lstResultRegaentInfoR1R2 = new List<ReagentStateInfoR1R2>();
            int strResult = 0;
            try
            {
                foreach (ReagentStateInfoR1R2 reagentStateInfo in lstReagentStateInfo)
                {
                    Hashtable hashTable = new Hashtable();

                    if (reagentStateInfo.ReagentType == "清洗剂" || reagentStateInfo.ReagentType2 == "清洗剂")
                    {
                        hashTable.Add("ReagentType", reagentStateInfo.ReagentType);
                        hashTable.Add("ReagentType2", reagentStateInfo.ReagentType2);
                        hashTable.Add("ReagentName", reagentStateInfo.ReagentName);
                        hashTable.Add("ReagentName2", reagentStateInfo.ReagentName2);
                    }
                    else
                    {
                        hashTable.Add("ReagentType", reagentStateInfo.ReagentType);
                        hashTable.Add("ReagentType2", reagentStateInfo.ReagentType2);
                        hashTable.Add("ReagentName", "");
                        hashTable.Add("ReagentName2", "");
                    }

                    hashTable.Add("ProjectName", reagentStateInfo.ProjectName);
                    strResult += ism_SqlMap.Update("ReagentInfo." + strDBMethod, reagentStateInfo);
                }
                if (strResult > 0)
                {
                    lstResultRegaentInfoR1R2 = (List<ReagentStateInfoR1R2>)ism_SqlMap.QueryForList<ReagentStateInfoR1R2>("ReagentInfo.QueryReagentState", null);
                }
                else
                    return null;
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("LockQualityControl(string strDBMethod, QualityControlInfo QCInfo)==" + e.ToString(), Module.DAO);
            }

            return lstResultRegaentInfoR1R2;
        }
        public void UpdateDetergentUsingStartingTime(DateTime time)
        {
            try
            {
                ism_SqlMap.Update("ReagentInfo.UpdateDetergentUsingStartingTime", time);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDetergentUsingStartingTime(DateTime time)==" + e.ToString(), Module.DAO);
            }
        }

        public void UpdateDetergentUsingFinishingTime(DateTime time)
        {
            try
            {
                ism_SqlMap.Update("ReagentInfo.UpdateDetergentUsingFinishingTime", time);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDetergentUsingFinishingTime(DateTime time)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 获取试剂状态表中的试剂体积
        /// </summary>
        /// <param name="disk"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetValidPercent(int disk, int pos)
        {
            int percent = 0;
            try
            {
                Hashtable ht = new Hashtable();
                if (disk == 1)
                    ht.Add("Pos", pos);
                else if(disk == 2)
                    ht.Add("Pos2", pos);

                if (ht.Count > 0)
                {
                    ReagentStateInfoR1R2 reaState = ism_SqlMap.QueryForObject("ReagentInfo.GetValidPercent", ht) as ReagentStateInfoR1R2;
                    if (reaState != null)
                    {
                        if (disk == 1)
                            percent = reaState.ValidPercent;
                        if (disk == 2)
                            percent = reaState.ValidPercent2;
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetValidPercent(int disk, int pos)==" + e.ToString(), Module.DAO);
            }

            return percent;
        }

        public void UpdateValidPercent(int volume, int disk, int pos)
        {
            try
            {
                Hashtable ht = new Hashtable();

                if (disk == 1)
                {
                    ht.Add("Pos", pos);
                    ht.Add("validPercent", volume);
                    ism_SqlMap.Update("ReagentInfo.UpdateValidPercent1", ht);
                }
                if (disk == 2)
                {
                    ht.Add("Pos2", pos);
                    ht.Add("validPercent2", volume);
                    ism_SqlMap.Update("ReagentInfo.UpdateValidPercent2", ht);
                }
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateValidPercent(int volume, int disk, int pos)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 获取试剂状态表中数据
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public ReagentStateInfoR1R2 GetReagentStateInfoByPos(int panel, int pos)
        {
            ReagentStateInfoR1R2 reaStateInfo = new ReagentStateInfoR1R2();
            try
            {
                if (panel == 1)
                    reaStateInfo = ism_SqlMap.QueryForObject("ReagentInfo.GetReagent1StateInfoByPos", pos.ToString()) as ReagentStateInfoR1R2;
                else if (panel == 2)
                    reaStateInfo = ism_SqlMap.QueryForObject("ReagentInfo.GetReagent2StateInfoByPos", pos.ToString()) as ReagentStateInfoR1R2;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetReagentStateInfoByPos(int panel, int pos)==" + e.ToString(), Module.DAO);
            }

            return reaStateInfo;
        }
        /// <summary>
        /// 获取试剂设置表（R1 or R2）信息
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public ReagentSettingsInfo GetReagentSettingsInfoByPos(int panel, int pos)
        {
            ReagentSettingsInfo reaSettingInfo = new ReagentSettingsInfo();
            try
            {
                Hashtable ht = new Hashtable();
                if (panel == 1)
                {
                    ht.Add("Pos", pos.ToString());
                    reaSettingInfo = ism_SqlMap.QueryForObject("ReagentInfo.GetReagent1SettingsInfoByPos", pos.ToString()) as ReagentSettingsInfo;
                }
                else if (panel == 2)
                {
                    ht.Clear();
                    ht.Add("Pos", pos.ToString());
                    reaSettingInfo = ism_SqlMap.QueryForObject("ReagentInfo.GetReagent2SettingsInfoByPos", pos.ToString()) as ReagentSettingsInfo;

                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetReagentSettingsInfoByPos(int panel, int pos)==" + e.ToString(), Module.DAO);
            }

            return reaSettingInfo;
        }
        /// <summary>
        /// 修改试剂状态表中的试剂体积和可测量数量
        /// </summary>
        /// <param name="vol"></param>
        /// <param name="panel"></param>
        /// <param name="position"></param>
        public void UpdateReagentValidPercent(int vol, int panel, int position)
        {
            try
            {
                if (position > 0)
                {
                    int v2 = GetValidPercent(panel, position);
                    int ResidualQuantity = this.ReagentVolumeNumber(vol, panel, position);
                    Hashtable ht = new Hashtable();
                    ht.Add("ValidPercent", vol);
                    ht.Add("Pos", position);
                    ht.Add("ResidualQuantity", ResidualQuantity);
                    if (panel == 1)
                    {
                        ism_SqlMap.Update("ReagentInfo.UpdateValidPercent1", ht);
                    }
                    else
                    {
                        ism_SqlMap.Update("ReagentInfo.UpdateValidPercent2", ht);
                    }
                    if (position > 0)
                    {
                        ReagentSettingsInfo reaSettingInfo = GetReagentSettingsInfoByPos(panel, position);
                        if (reaSettingInfo != null && vol > 0 && v2 == 0)
                        {
                            UpdateNorTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                            UpdateQCTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                            UpdateCalibTaskState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                            UpdateCalibCurveState(reaSettingInfo.ProjectName, reaSettingInfo.ReagentType);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateReagentValidPercent(int vol, int panel, int position)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 计算试剂余量可测数量
        /// </summary>
        /// <param name="v"></param>
        /// <param name="panel"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private int ReagentVolumeNumber(int v, int panel, int position)
        {
            int number = 0;
            if (v <= 0)
            {
                return number;
            }
            else
            {
                ReagentSettingsInfo reagentSetting =this.GetReagentSettingsInfoByPos(panel, position);
                int reagentSettingVol = this.GetAssProParamReagentSetiingVol(panel, reagentSetting.ProjectName, reagentSetting.ReagentType);
                int microlitre = System.Convert.ToInt32(reagentSetting.ReagentContainer.Substring(0, reagentSetting.ReagentContainer.IndexOf("ml"))) * (v - 3) * 1000 / 100;
                number = reagentSettingVol == 0 ? 0 : microlitre / reagentSettingVol;
            }
            return number <= 0 ? 0: number;
        }
        /// <summary>
        /// 获取项目参数中设置的试剂体积
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public int GetAssProParamReagentSetiingVol(int panel,string projectName, string sampleType)
        {
            float vol = 0;
            try
            {
                Hashtable hash = new Hashtable();
                hash.Add("ProjectName", projectName);
                hash.Add("SampleType", sampleType);
                if (panel == 1)
                {
                    vol = (float)ism_SqlMap.QueryForObject("AssayProjectInfo.GetAssProParamReagentSetiingVol", string.Format("select Reagent1VolSettings from assayprojectparaminfotb where ProjectName = '{0}' and SampleType = '{1}'",projectName, sampleType));
                }
                else if (panel == 2)
                {
                    vol = (float)ism_SqlMap.QueryForObject("AssayProjectInfo.GetAssProParamReagentSetiingVol", string.Format("select Reagent2VolSettings from assayprojectparaminfotb where ProjectName = '{0}' and SampleType = '{1}'", projectName, sampleType));
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetAssProParamReagentSetiingVol(string projectName, string sampleType) == " + ex.ToString(), Module.Reagent);
            }
            return System.Convert.ToInt32(vol) ;
        }
        /// <summary>
        /// 获取试剂2使用次数表数据
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="reagentTray"></param>
        /// <returns></returns>
        public Reagent2UsingCountInfo GetReagent2UsingCountInfo(string projectName, int reagentTray)
        {
            Reagent2UsingCountInfo reagent2UsingCount = null;
            try
            {
                reagent2UsingCount = (Reagent2UsingCountInfo)ism_SqlMap.QueryForObject("ReagentInfo.GetReagent2UsingCountInfo", string.Format("select * from Reagent2UsingCounttb where ProjectName = '{0}' and ReagentTray = {1}", projectName, reagentTray));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("Reagent2UsingCountInfo GetReagent2UsingCountInfo(string projectName, int reagentTray) ==" + ex.ToString(), Module.Reagent);
            }
            return reagent2UsingCount;
        }
        /// <summary>
        /// 试剂2使用次数表插入数据
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="reagentTray"></param>
        /// <param name="count"></param>
        public void Insert(string projectName, int reagentTray, int count)
        {
            try
            {
                ism_SqlMap.Insert("ReagentInfo.InsertReagent2UsingCountInfo", string.Format("insert Reagent2UsingCounttb(ProjectName,ReagentTray,Count) values('{0}',{1},{2})", projectName, reagentTray, count));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("Insert(string projectName, int reagentTray, int count) ==" + ex.ToString(), Module.Reagent);
            }
        }
        /// <summary>
        /// 修改试剂2使用次数表中的次数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="projectName"></param>
        public void UpdateReagent2UsingCount(int count, string projectName)
        {
            try
            {
                ism_SqlMap.Update("ReagentInfo.InsertReagent2UsingCountInfo", string.Format("update Reagent2UsingCounttb set Count = {0} where ProjectName = '{1}'", count, projectName));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("UpdateReagent2UsingCount(int count, string projectName) ==" + ex.ToString(), Module.Reagent);
            }
        }

        public ReagentSettingsInfo GetReagentSettingsInfo(string projectName, string sampleType)
        {
            ReagentSettingsInfo reagentSettingInfo = new ReagentSettingsInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("ReagentType", sampleType);
                reagentSettingInfo = ism_SqlMap.QueryForObject("ReagentInfo.GetReagentSettingsInfo", ht) as ReagentSettingsInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetReagentSettingsInfo(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return reagentSettingInfo;
        }

        public ReagentSettingsInfo GetReagentSettingsInfo2(string projectName, string sampleType)
        {
            ReagentSettingsInfo reagentSettingInfo = new ReagentSettingsInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", projectName);
                ht.Add("ReagentType", sampleType);
                reagentSettingInfo = ism_SqlMap.QueryForObject("ReagentInfo.GetReagentSettingsInfo2", ht) as ReagentSettingsInfo;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetReagentSettingsInfo2(string projectName, string sampleType)==" + e.ToString(), Module.DAO);
            }

            return reagentSettingInfo;
        }

        public void UpdateLockState(string ReagentPanel, ReagentSettingsInfo reagentSettingInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Locked", reagentSettingInfo.Locked);
                ht.Add("Pos", reagentSettingInfo.Pos);

                if (ReagentPanel == "R1")
                {
                    ism_SqlMap.Update("ReagentInfo.UpdateReagent1LockState", ht);
                }
                else if (ReagentPanel == "R2")
                {
                    ism_SqlMap.Update("ReagentInfo.UpdateReagent2LockState", ht);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateLockState(string ReagentPanel, ReagentSettingsInfo reagentSettingInfo)==" + e.ToString(), Module.DAO);
            }
        }
        /// <summary>
        /// 获取试剂项目参数信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ReagentItem getReagentItemInfo(string code)
        {
            ReagentItem reagent = null;

            try
            {
                ism_SqlMap.QueryForObject("ReagentInfo.getReagentItemInfo", string.Format("select * from ReagentItemTb where Code ={0}", code));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("getReagentItemInfo(string code) ==" + ex.Message, Module.LISSetting);
            }
            return reagent;
        }
        /// <summary>
        /// 根据试剂条码查找试剂信息
        /// </summary>
        /// <param name="reagentBarcode"></param>
        public ReagentSettingsInfo GetAssayALLReagentByBarcode(string reagentBarcode)
        {
            ReagentSettingsInfo r = null;
            try
            {
                r = ism_SqlMap.QueryForObject("ReagentInfo.GetAssayALLReagentByBarcode", string.Format(
                                                               @"select r.*,r1r2.ValidPercent from reagentsettingstb r left join reagentstateinfor1r2tb r1r2 on r.ProjectName = r1r2.ProjectName where Barcode = '{0}' 
                                                                  union 
                                                               select r2.*,r1r2.ValidPercent2 from reagentsettingstbr2 r2 left join reagentstateinfor1r2tb r1r2 on r2.ProjectName = r1r2.ProjectName where Barcode = '{0}'", reagentBarcode)) as ReagentSettingsInfo;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetAssayALLReagentByBarcode(string reagentBarcode) ==" + ex.Message, Module.Reagent);
            }
            return r;
        }
        /// <summary>
        /// 保存试剂条码信息
        /// </summary>
        /// <param name="r"></param>
        public void InsertReagentBarcode(ReagentBarcodeParam r)
        {
            try
            {
                ism_SqlMap.Insert("ReagentInfo.InsertReagentBarcode", string.Format("insert ReagentBarcodeParamTb(Barcode,ValidPercent,ExchangeDatetime) values('{0}',{1},'{2}')", r.Barcode, r.ValidPercent, r.ExchangeDatetime));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("InsertReagentBarcode(ReagentBarcodeParam r) == " + ex.Message, Module.Reagent);
            }
        }
        /// <summary>
        /// 获取试剂条码参数信息
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public ReagentBarcodeParam GetAllReagentBarParam(string barcode)
        {
            ReagentBarcodeParam RB = null;
            try
            {
                string SQL = string.Format(@"select * from ReagentBarcodeParamTb where  Barcode='{0}' order by ExchangeDatetime desc", barcode);
                RB = ism_SqlMap.QueryForObject("ReagentInfo.GetAllReagentBarParamByBarcode", SQL) as ReagentBarcodeParam;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("ReagentBarcodeParam GetAllReagentBarParam(string barcode) ==" + ex.Message, Module.Reagent);
            }
            return RB;
        }

        /// <summary>
        /// 删除试剂信息 ReagentSettingsTb/ReagentSettingstbr2
        /// </summary>
        public void DeleteReagentInfo(int disk, string pos)
        {
            string SQL = "";
            if (disk == 1)
            {
                SQL = string.Format("delete from reagentsettingstb where Pos = '{0}'", pos);
            }
            else if (disk == 2)
            {
                SQL = string.Format("delete from reagentsettingstbr2 where Pos = '{0}'", pos);
            }
            try
            {
                ism_SqlMap.Delete("ReagentInfo.DeleteReagentInfo", SQL);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog(" DeleteReagentInfo(ReagentSettingsInfo r) == " + ex.Message, Module.Reagent);
            }
        }
        /// <summary>
        /// 根据试剂盘号和位置获取试剂信息
        /// </summary>
        /// <param name="disk"></param>
        /// <param name="pos"></param>
        public ReagentSettingsInfo GetAssayReagentByDisk(int disk, string pos)
        {
            ReagentSettingsInfo r = null;
            string SQL = "";
            if (disk == 1)
            {
                SQL = string.Format("select * from reagentsettingstb where Pos = '{0}'", pos);
            }
            else if (disk == 2)
            {
                SQL = string.Format("select * from reagentsettingstbr2 where Pos = '{0}'", pos);
            }
            try
            {
                 r = ism_SqlMap.QueryForObject("ReagentInfo.GetAssayReagentByDisk",SQL) as ReagentSettingsInfo;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetAssayReagentByDisk(int disk, string pos) ==" + ex.Message, Module.Reagent);
            }
            return r;
        }
        /// <summary>
        /// 更新试剂状态R1R2表信息
        /// </summary>
        /// <param name="disk">试剂盘号</param>
        /// <param name="r">试剂参数信息</param>
        /// <param name="MeasurableNumber">试剂剩余体积</param>
        public void UpdateReagentR1AndR2Info(int disk, ReagentSettingsInfo r, int MeasurableNumber)
        {
            string SQL = "";
            
            if (disk == 1)
            {
                SQL = string.Format("update reagentstateinfor1r2tb set ReagentName = '{0}',Pos = '{1}', ReagentType = '{2}', ValidPercent = '{3}', ResidualQuantity = '{4}' where ProjectName = '{5}'", r.ReagentName, r.Pos, r.ReagentType, r.ValidPercent, MeasurableNumber, r.ProjectName);
            }
            else if (disk == 2)
            {
                SQL = string.Format("update reagentstateinfor1r2tb set ReagentName2 = '{0}',Pos2 = '{1}', ReagentType2 = '{2}', ValidPercent2 = '{3}', ResidualQuantity2 = '{4}' where ProjectName = '{5}'", r.ReagentName, r.Pos, r.ReagentType, r.ValidPercent, MeasurableNumber, r.ProjectName);
            }
            try
            {
                 ism_SqlMap.Update("ReagentInfo.UpdateReagentR1AndR2Info",SQL);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("UpdateReagentR1AndR2Info(int disk, ReagentSettingsInfo r) == " + ex.Message, Module.Reagent);
            }
        }
        /// <summary>
        /// 保存试剂状态R1R2表信息
        /// </summary>
        /// <param name="disk">试剂盘号</param>
        /// <param name="r">试剂参数信息</param>
        /// <param name="MeasurableNumber">试剂剩余体积</param>
        public void SaveReagentR1AndR2Info(int disk, ReagentSettingsInfo r, int MeasurableNumber)
        {
            string SQL = "";
            if(disk == 1)
            {
                SQL = string.Format(@"insert into reagentstateinfor1r2Tb(ProjectName,Locked,ReagentName,ResidualQuantity,Pos,ReagentType,ValidPercent)
                                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", r.ProjectName, r.Locked, r.ReagentName, MeasurableNumber,r.Pos,r.ReagentType,r.ValidPercent);
            }
            else if(disk == 2)
            {
                SQL = string.Format(@"insert into reagentstateinfor1r2Tb(ProjectName,Locked,ReagentName2,ResidualQuantity2,Pos2,ReagentType2,ValidPercent2)
                                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", r.ProjectName, r.Locked, r.ReagentName, MeasurableNumber, r.Pos, r.ReagentType, r.ValidPercent);
            }
            try
            {
                ism_SqlMap.Insert("ReagentInfo.SaveReagentR1AndR2Info", SQL);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("SaveReagentR1AndR2Info(int disk, ReagentSettingsInfo r, int MeasurableNumber) == " + ex.Message, Module.Reagent);
            }
        }
        /// <summary>
        ///  保存试剂参数信息R1 or R2
        /// </summary>
        /// <param name="disk">试剂盘号</param>
        /// <param name="r">试剂参数信息</param>
        public void SaveReagentSettingInfo(int disk, ReagentSettingsInfo r)
        {
            string SQL = "";
            if (disk == 1)
            {
                SQL = string.Format(@"insert into ReagentSettingsTb(Pos,ProjectName,ReagentName,ValidDate,Barcode,ReagentContainer,BatchNum,ReagentType)
                                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", r.Pos, r.ProjectName, r.ReagentName, r.ValidDate, r.Barcode, r.ReagentContainer,r.BatchNum,r.ReagentType);
            }
            else if (disk == 2)
            {
                SQL = string.Format(@"insert into ReagentSettingsTbR2(Pos,ProjectName,ReagentName,ValidDate,Barcode,ReagentContainer,BatchNum,ReagentType)
                                values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", r.Pos, r.ProjectName, r.ReagentName, r.ValidDate, r.Barcode, r.ReagentContainer,r.BatchNum,r.ReagentType);
            }
            
            try
            {
                ism_SqlMap.Insert("ReagentInfo.SaveReagentR1AndR2Info", SQL);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("SaveReagentSettingInfo(ReagentSettingsInfo r, int MeasureableNumber) == " + ex.Message, Module.Reagent);
            }
        }
        /// <summary>
        /// 删除试剂状态R1R2信息
        /// </summary>
        public ReagentStateInfoR1R2 GetReagentStateInfoR1R2(int disk, string pos)
        {
            ReagentStateInfoR1R2 r = null;
            try
            {
                r = (ReagentStateInfoR1R2)ism_SqlMap.QueryForObject("ReagentInfo.GetReagentStateInfoR1R2", string.Format("select * from reagentstateinfor1r2tb where Pos ='{0}' or Pos2 = '{1}'", pos, pos));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("DeleteReagentSettingAndStateInfo(int disk, string pos) ==" + ex.Message, Module.Reagent);
            }
            return r;
        }
    }
}
