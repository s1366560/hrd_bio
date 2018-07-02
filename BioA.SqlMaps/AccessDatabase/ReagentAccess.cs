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
                return "";
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
                return "";
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
                return "试剂装载失败";

            }
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
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
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

        public List<ReagentStateInfoR1R2> QueryReagentStateInfo(string strDBMethod, string dataConfig)
        {
            List<ReagentStateInfoR1R2> lstReagentStateInfo = new List<ReagentStateInfoR1R2>();
            
            try
            {
                if (dataConfig == null)
                {
                    lstReagentStateInfo = (List<ReagentStateInfoR1R2>)ism_SqlMap.QueryForList<ReagentStateInfoR1R2>("ReagentInfo." + strDBMethod, dataConfig);

                    LogInfo.WriteProcessLog(XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), lstReagentStateInfo) + "zhuszihe22", Module.WindowsService);
                }
            }                 

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

          

            return lstReagentStateInfo;
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

        public int UpdataReagentStateInfo(string strDBMethod, ReagentStateInfoR1R2 reagentStateInfo)
        {
            int strResult = 0;
            try
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
                strResult = ism_SqlMap.Update("ReagentInfo." + strDBMethod, reagentStateInfo);
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("LockQualityControl(string strDBMethod, QualityControlInfo QCInfo)==" + e.ToString(), Module.DAO);
            }

            return strResult;
        }

        public int UpdataUnlockReagentStateInfo(string strDBMethod, List<ReagentStateInfoR1R2> ReagentStateInfo)
        {
            int strResult = 0;
            try
            {
                for (int i = 0; i < ReagentStateInfo.Count; i++)
                {
                    Hashtable hashTable = new Hashtable();

                    if (ReagentStateInfo[i].ReagentType == "清洗剂" || ReagentStateInfo[i].ReagentType2 == "清洗剂")
                    {
                        hashTable.Add("ReagentType", ReagentStateInfo[i].ReagentType);
                        hashTable.Add("ReagentType2", ReagentStateInfo[i].ReagentType2);
                        hashTable.Add("ReagentName", ReagentStateInfo[i].ReagentName);
                        hashTable.Add("ReagentName2", ReagentStateInfo[i].ReagentName2);
                    }
                    else
                    {
                        hashTable.Add("ReagentType", ReagentStateInfo[i].ReagentType);
                        hashTable.Add("ReagentType2", ReagentStateInfo[i].ReagentType2);
                        hashTable.Add("ReagentName", "");
                        hashTable.Add("ReagentName2", "");
                    }

                    hashTable.Add("ProjectName", ReagentStateInfo[i].ProjectName);
                    strResult += ism_SqlMap.Update("ReagentInfo." + strDBMethod, hashTable);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("LockQualityControl(string strDBMethod, QualityControlInfo QCInfo)==" + e.ToString(), Module.DAO);
            }

            return strResult;
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

                    if (disk == 1)
                        percent = reaState.ValidPercent;
                    if (disk == 2)
                        percent = reaState.ValidPercent2;
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

        public void UpdateReagentValidPercent(int vol, int panel, int position)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ValidPercent", vol);
                ht.Add("Pos", position);
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
