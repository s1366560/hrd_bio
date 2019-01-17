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
        public int DeletereagentSettingsInfoAndStateInfo(string reagentNum, ReagentSettingsInfo DeletereagentSettingsInfo)
        {
            int result = 0;
            try
            {
                if (reagentNum == "R1")
                {
                    ism_SqlMap.QueryForObject("ReagentInfo.deleteReagentStatInfo", string.Format("delete from ReagentStateInfoR1R2Tb where ReagentName = '{0}'", DeletereagentSettingsInfo.ReagentName));
                    result =(int)ism_SqlMap.Delete("ReagentInfo.deleteReagentSettingInfo", string.Format("delete from ReagentSettingsTb where ReagentName = '{0}'", DeletereagentSettingsInfo.ReagentName));
                }
                else
                {
                    ism_SqlMap.QueryForObject("ReagentInfo.deleteReagentStatInfo", string.Format("delete from ReagentStateInfoR1R2Tb where ReagentName2 = '{0}'", DeletereagentSettingsInfo.ReagentName));
                    result = (int)ism_SqlMap.Delete("ReagentInfo.deleteReagentSettingInfo", string.Format("delete from ReagentSettingsTbR2 where ReagentName = '{0}'", DeletereagentSettingsInfo.ReagentName));
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
        /// <summary>
        ///  修改试剂解锁状态：
        ///     1.如果（strResult > 0）：修改成功，然后再重新获取所有试剂状态信息
        ///     2.如果（strResult = 0）：修改失败，直接返回空对象。
        /// </summary>
        /// <param name="time"></param>
        //public List<ReagentStateInfoR1R2> UpdataUnlockReagentStateInfo(string strDBMethod, List<ReagentStateInfoR1R2> lstReagentStateInfo)
        //{
        //    List<ReagentStateInfoR1R2> lstResultRegaentInfoR1R2 = new List<ReagentStateInfoR1R2>();
        //    int strResult = 0;
        //    try
        //    {
        //        foreach(ReagentStateInfoR1R2 reagentStateInfo in lstReagentStateInfo) 
        //        {
        //            Hashtable hashTable = new Hashtable();

        //            if (reagentStateInfo[i].ReagentType == "清洗剂" || reagentStateInfo.ReagentType2 == "清洗剂")
        //            {
        //                hashTable.Add("ReagentType", reagentStateInfo.ReagentType);
        //                hashTable.Add("ReagentType2", reagentStateInfo.ReagentType2);
        //                hashTable.Add("ReagentName", reagentStateInfo.ReagentName);
        //                hashTable.Add("ReagentName2", reagentStateInfo.ReagentName2);
        //            }
        //            else
        //            {
        //                hashTable.Add("ReagentType", reagentStateInfo.ReagentType);
        //                hashTable.Add("ReagentType2", reagentStateInfo.ReagentType2);
        //                hashTable.Add("ReagentName", "");
        //                hashTable.Add("ReagentName2", "");
        //            }

        //            hashTable.Add("ProjectName", reagentStateInfo.ProjectName);
        //            strResult += ism_SqlMap.Update("ReagentInfo." + strDBMethod, hashTable);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        LogInfo.WriteErrorLog("LockQualityControl(string strDBMethod, QualityControlInfo QCInfo)==" + e.ToString(), Module.DAO);
        //    }

        //    return strResult;
        //}

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
                Hashtable ht = new Hashtable();
                ht.Add("ValidPercent", vol);
                ht.Add("Pos", position);
                ht.Add("ResidualQuantity", this.ReagentVolumeNumber(vol, panel, position));
                if (panel == 1)
                {
                    ism_SqlMap.Update("ReagentInfo.UpdateValidPercent1", ht);
                }
                else
                {
                    ism_SqlMap.Update("ReagentInfo.UpdateValidPercent2", ht);
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
    }
}
