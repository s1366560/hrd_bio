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

        public int SelectUserInfo(string strDBMethod, UserInfo userInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("UserID", userInfo.UserID);
                intResult = (int)ism_SqlMap.QueryForObject("UserInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public void AddUserInfo(string strDBMethod, UserInfo userInfo)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("UserID", userInfo.UserID);
            hashTable.Add("UserName", userInfo.UserName);
            hashTable.Add("UserPassword", userInfo.UserPassword);
            hashTable.Add("CreateTime", userInfo.CreateTime);
            hashTable.Add("ApplyTask", userInfo.ApplyTask);
            hashTable.Add("DataCheck", userInfo.DataCheck);
            hashTable.Add("CalibDataCheck", userInfo.CalibDataCheck);
            hashTable.Add("ReagentSetting", userInfo.ReagentSetting);
            hashTable.Add("ReagentState", userInfo.ReagentState);
            hashTable.Add("CalibState", userInfo.CalibState);
            hashTable.Add("CalibMaintain", userInfo.CalibMaintain);
            hashTable.Add("QCMaintain", userInfo.QCMaintain);
            hashTable.Add("QCState", userInfo.QCState);
            hashTable.Add("ChemistryParam", userInfo.ChemistryParam);
            hashTable.Add("CombProject", userInfo.CombProject);
            hashTable.Add("CalcProject", userInfo.CalcProject);
            hashTable.Add("EnvironmentParam", userInfo.EnvironmentParam);
            hashTable.Add("CrossPollute", userInfo.CrossPollute);
            hashTable.Add("LISCommunicate", userInfo.LISCommunicate);
            hashTable.Add("DataConfiguration", userInfo.DataConfiguration);
            hashTable.Add("RouMaintain", userInfo.RouMaintain);
            hashTable.Add("EquipDebug", userInfo.EquipDebug);
            hashTable.Add("UserManage", userInfo.UserManage);
            hashTable.Add("DepartManage", userInfo.DepartManage);
            hashTable.Add("Configuration", userInfo.Configuration);
            hashTable.Add("LogCheck", userInfo.LogCheck);
            hashTable.Add("VersionInfo", userInfo.VersionInfo);

            try
            {
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe666" + userInfo.DataConfiguration, Module.WindowsService);
                ism_SqlMap.Insert("UserInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public int EditUserInfoUpDate(string strDBMethod, UserInfo userInfo, string OldUserId)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("UserID", userInfo.UserID);
                hashTable.Add("UserName", userInfo.UserName);
                hashTable.Add("UserPassword", userInfo.UserPassword);
                hashTable.Add("CreateTime", userInfo.CreateTime);
                hashTable.Add("ApplyTask", userInfo.ApplyTask);
                hashTable.Add("DataCheck", userInfo.DataCheck);
                hashTable.Add("CalibDataCheck", userInfo.CalibDataCheck);
                hashTable.Add("ReagentSetting", userInfo.ReagentSetting);
                hashTable.Add("ReagentState", userInfo.ReagentState);
                hashTable.Add("CalibState", userInfo.CalibState);
                hashTable.Add("CalibMaintain", userInfo.CalibMaintain);
                hashTable.Add("QCMaintain", userInfo.QCMaintain);
                hashTable.Add("QCState", userInfo.QCState);
                hashTable.Add("ChemistryParam", userInfo.ChemistryParam);
                hashTable.Add("CombProject", userInfo.CombProject);
                hashTable.Add("CalcProject", userInfo.CalcProject);
                hashTable.Add("EnvironmentParam", userInfo.EnvironmentParam);
                hashTable.Add("CrossPollute", userInfo.CrossPollute);
                hashTable.Add("LISCommunicate", userInfo.LISCommunicate);
                hashTable.Add("DataConfiguration", userInfo.DataConfiguration);
                hashTable.Add("RouMaintain", userInfo.RouMaintain);
                hashTable.Add("EquipDebug", userInfo.EquipDebug);
                hashTable.Add("UserManage", userInfo.UserManage);
                hashTable.Add("DepartManage", userInfo.DepartManage);
                hashTable.Add("Configuration", userInfo.Configuration);
                hashTable.Add("LogCheck", userInfo.LogCheck);
                hashTable.Add("VersionInfo", userInfo.VersionInfo);

                hashTable.Add("UserIDOld", OldUserId);

                intResult = (int)ism_SqlMap.Update("UserInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }

        public int DeleteUserInfo(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ID", dataConfig);
                intResult = (int)ism_SqlMap.Delete("UserInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public List<string> QueryDepartment(string strDBMethod)
        {
            List<string> lstQueryDepartment = new List<string>();
            try
            {
                lstQueryDepartment = (List<string>)ism_SqlMap.QueryForList<string>("DepartmentInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryDepartment;
        }
        /// <summary>
        /// 获取所有用户（检验医生）
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<UserInfo> QueryUserManagement(string strDBMethod)
        {
            List<UserInfo> lstQueryUserManagement = new List<UserInfo>();
            try
            {
                lstQueryUserManagement = (List<UserInfo>)ism_SqlMap.QueryForList<UserInfo>("UserInfo." + strDBMethod, null);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryUserManagement;
        }
        public int SelectDepartment(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Department", dataConfig);
                intResult = (int)ism_SqlMap.QueryForObject("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public void AddDepartmentInfo(string strDBMethod, string dataConfig)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("Department", dataConfig);
            try
            {
                ism_SqlMap.Insert("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public int DeleteDepartment(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Department", dataConfig);
                intResult = (int)ism_SqlMap.Delete("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int UpdataDepartment(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Department", dataConfig);
                hashTable.Add("DepartmentOld", dataConfigOld);

                intResult = (int)ism_SqlMap.Update("DepartmentInfo." + strDBMethod, hashTable);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + intResult, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 获取所有医生信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<ApplyDoctorInfo> QueryApplyDoctorInfo(string strDBMethod)
        {
            List<ApplyDoctorInfo> lstQueryApplyDoctor = new List<ApplyDoctorInfo>();
            try
            {
                lstQueryApplyDoctor = (List<ApplyDoctorInfo>)ism_SqlMap.QueryForList<ApplyDoctorInfo>("DepartmentInfo." + strDBMethod, null);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryApplyDoctor;
        }

        public void AddApplyDoctor(string strDBMethod, ApplyDoctorInfo applyDoctorInfo)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("Department", applyDoctorInfo.Department);
            hashTable.Add("Doctor", applyDoctorInfo.Doctor);
            try
            {
                ism_SqlMap.Insert("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public int SelectApplyDoctor(string strDBMethod, ApplyDoctorInfo applyDoctorInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Department", applyDoctorInfo.Department);
                hashTable.Add("Doctor", applyDoctorInfo.Doctor);
                intResult = (int)ism_SqlMap.QueryForObject("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int DeleteApplyDoctorInfo(string strDBMethod, ApplyDoctorInfo applyDoctorInfo)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Department", applyDoctorInfo.Department);
                hashTable.Add("Doctor", applyDoctorInfo.Doctor);
                intResult = (int)ism_SqlMap.Delete("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int UpdataApplyDoctorInfo(string strDBMethod, ApplyDoctorInfo applyDoctorInfo, ApplyDoctorInfo applyDoctorInfoOld)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Department", applyDoctorInfo.Department);
                hashTable.Add("Doctor", applyDoctorInfo.Doctor);
                hashTable.Add("DepartmentOld", applyDoctorInfoOld.Department);
                hashTable.Add("DoctorOld", applyDoctorInfoOld.Doctor);

                intResult = (int)ism_SqlMap.Update("DepartmentInfo." + strDBMethod, hashTable);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + intResult, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 获取所有审核医生信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<string> QueryAuditPhysician(string strDBMethod)
        {
            List<string> lstQueryAuditPhysician = new List<string>();
            try
            {
                lstQueryAuditPhysician = (List<string>)ism_SqlMap.QueryForList<string>("DepartmentInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryAuditPhysician;
        }

        public int SelectAuditPhysician(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Doctor", dataConfig);
                intResult = (int)ism_SqlMap.QueryForObject("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public void AddAuditPhysician(string strDBMethod, string dataConfig)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("Doctor", dataConfig);
            try
            {
                ism_SqlMap.Insert("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddDataConfig(string strDBMethod, CalcProjectInfo calcProjectInfo)==" + e.ToString(), Module.DAO);
            }
        }

        public int DeleteAuditPhysician(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Doctor", dataConfig);
                intResult = (int)ism_SqlMap.Delete("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int UpDataAuditPhysician(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Doctor", dataConfig);
                hashTable.Add("DoctorOld", dataConfigOld);

                intResult = (int)ism_SqlMap.Update("DepartmentInfo." + strDBMethod, hashTable);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + intResult, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)==" + e.ToString(), Module.DAO);
            }

            return intResult;
        }
        /// <summary>
        /// 获取所有保养日志信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<MaintenanceLogInfo> QueryMaintenanceLogInfo(string strDBMethod)
        {
            List<MaintenanceLogInfo> lstQueryMaintenanceLogInfo = new List<MaintenanceLogInfo>();
            try
            {
                lstQueryMaintenanceLogInfo = (List<MaintenanceLogInfo>)ism_SqlMap.QueryForList<MaintenanceLogInfo>("LogInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryMaintenanceLogInfo;
        }
        /// <summary>
        /// 获取所有操作日志信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<MaintenanceLogInfo> QueryOperationLogInfo(string strDBMethod, string startDate, string endDate)
        {
            List<MaintenanceLogInfo> lstQueryOperationLogInfo = new List<MaintenanceLogInfo>();
            Hashtable table = new Hashtable();
            try
            {
                table.Add("startDate", startDate);
                table.Add("endDate", endDate);
                lstQueryOperationLogInfo = (List<MaintenanceLogInfo>)ism_SqlMap.QueryForList<MaintenanceLogInfo>("LogInfo." + strDBMethod, table);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryOperationLogInfo;
        }
        /// <summary>
        /// 获取所有报警日志
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<AlarmLogInfo> QueryAlarmLogInfo(string strDBMethod)
        {
            List<AlarmLogInfo> lstAlarmLogInfo = new List<AlarmLogInfo>();
            try
            {
                lstAlarmLogInfo = (List<AlarmLogInfo>)ism_SqlMap.QueryForList<AlarmLogInfo>("LogInfo." + strDBMethod, null);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstAlarmLogInfo;
        }
        /// <summary>
        /// 根据时间段获取报警信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="alarmLogInfo"></param>
        /// <returns></returns>
        public List<TroubleLog> SelectTroubleLogInfoByTimeQuantum(string strDBMethod, string logStateTime, string logEnditTime)
        {
            List<TroubleLog> lstTroubleLogInfo = new List<TroubleLog>();
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("logStateTime", logStateTime);
                hashTable.Add("logEnditTime", logEnditTime);

                lstTroubleLogInfo = (List<TroubleLog>)ism_SqlMap.QueryForList<TroubleLog>("PLCDataInfo." + strDBMethod, hashTable);
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("SelectTroubleLogInfoByTimeQuantum(string strDBMethod, string logStateTime, string logEnditTime) ==" + e.ToString(), Module.DAO);
            }

            return lstTroubleLogInfo;
        }
        /// <summary>
        /// 确认故障错误和警告信息
        /// </summary>
        /// <param name="strDBMethid"></param>
        /// <param name="lstDrawDateTime"></param>
        /// <returns></returns>
        public int AffirmTroubleLogInfo(string strDBMethid, List<string> lstDrawDateTime)
        {
            int result = 0;
            try
            {
                foreach(string drawDateTime in lstDrawDateTime)
                {
                    string[] s = drawDateTime.Split('|');
                    string startTime = s[0].Substring(0,10);
                    string endTime = s[0];
                    string code = s[1];
                    Hashtable table = new Hashtable();
                    table.Add("startTime", startTime);
                    table.Add("endTime", endTime);
                    table.Add("code", code);
                    result = ism_SqlMap.Update("PLCDataInfo." + strDBMethid, table);
                }
            }
            catch(Exception ex)
            {
                LogInfo.WriteErrorLog("AffirmTroubleLogInfo(string strDBMethid, List<string> lstDrawDateTime) == " + ex.ToString(), Module.DAO);
            }
            return result;
        }
        /// <summary>
        /// 删除操作日志
        /// </summary>
        /// <param name="strDBMethid"></param>
        /// <param name="lstDrawDateTime"></param>
        /// <returns></returns>
        public int DeleteOperationLogInfo(string strDBMethid, List<string> lstDrawDateTime)
        {
            int result = 0;
            try
            {
                foreach(string drawDateTime in lstDrawDateTime)
                {
                   result = ism_SqlMap.Delete("PLCDataInfo." +strDBMethid, drawDateTime);
                }
            }
            catch(Exception ex)
            {
                LogInfo.WriteErrorLog("AffirmTroubleLogInfo(string strDBMethid, List<string> lstDrawDateTime) == " + ex.ToString(), Module.DAO);
            }
            return result;
        }      

        public List<UserInfo> QueryUserCeation(string strDBMethod, string p2)
        {
            List<UserInfo> lstQueryUserCeation = new List<UserInfo>();
            try
            {
                
                    Hashtable hashTable = new Hashtable();
                    hashTable.Add("UserID", p2);

                    lstQueryUserCeation = (List<UserInfo>)ism_SqlMap.QueryForList<UserInfo>("UserInfo." + strDBMethod, hashTable);
                    LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstQueryUserCeation, Module.WindowsService);
               
            }

            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryDataConfig(string strDBMethod, string dataConfig)" + e.ToString(), Module.DAO);
            }

            return lstQueryUserCeation;
        }
        public int SelectApplyDoctorDepartment(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Department", dataConfig);
                intResult = (int)ism_SqlMap.QueryForObject("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public int SelectDeleteAuditPhysician(string strDBMethod, string dataConfig)
        {
            int intResult = 0;
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("Doctor", dataConfig);
                intResult = (int)ism_SqlMap.QueryForObject("DepartmentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("public int SelectDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.DAO);
            }
            return intResult;
        }

        public ManuOffsetGain QueryManuOffsetGain(string strMethodName)
        {
            ManuOffsetGain manu = new ManuOffsetGain();
            try
            {
                manu = ism_SqlMap.QueryForObject("EquipmentManage." + strMethodName, null) as ManuOffsetGain;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("ManuOffsetGain QueryManuOffsetGain(string strMethodName)==" + e.ToString(), Module.DAO);
            }

            return manu;
        }

        public List<CuvetteBlankInfo> QueryWaterBlankValueByWave(string strMethodName, string waveLength)
        {
            List<CuvetteBlankInfo> lstCuvBlk = new List<CuvetteBlankInfo>();
            try
            {
                CuvetteBlankInfo cuvBlkNew = ism_SqlMap.QueryForObject("EquipmentManage.QueryWaterNewBlankValueByWave", waveLength) as CuvetteBlankInfo;
                CuvetteBlankInfo cuvBlkOld = ism_SqlMap.QueryForObject("EquipmentManage.QueryWaterOldBlankValueByWave", waveLength) as CuvetteBlankInfo;
                lstCuvBlk.Add(cuvBlkNew);
                lstCuvBlk.Add(cuvBlkOld); 
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryWaterBlankValueByWave(string strMethodName, string strWave)==" + e.ToString(), Module.DAO);
            }

            return lstCuvBlk;
        }
        /// <summary>
        /// 获取比色杯清洁程度的最大值和最小值
        /// </summary>
        /// <returns></returns>
        public string getMaxMinforCuvette()
        {
            string result = "";
            try
            {
                result = ism_SqlMap.QueryForObject("EquipmentManage.getMaxMinforCuvette", null).ToString();
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("getMaxMinforCuvette==" + e.ToString(), Module.DAO);
            }
            return result;
        }
        /// <summary>
        /// 获取最新的光度计检测值
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <returns></returns>
        public List<List<OffSetGain>> QueryNewPhotemetricValue(string strMethodName)
        {
            List<List<OffSetGain>> lstNewAndOldPhotoGain = new List<List<OffSetGain>>();
            List<OffSetGain> lstNewPhotoGain = new List<OffSetGain>();
            List<OffSetGain> lstOldPhotoGain = new List<OffSetGain>();
            try
            {
                lstNewPhotoGain = ism_SqlMap.QueryForList<OffSetGain>("EquipmentManage.QueryNewPhotemetricValue", null) as List<OffSetGain>; 
                lstOldPhotoGain = ism_SqlMap.QueryForList<OffSetGain>("EquipmentManage." + strMethodName, null) as List<OffSetGain>;
                if (lstNewPhotoGain.Count > 0 || lstOldPhotoGain.Count > 0)
                {
                    lstNewAndOldPhotoGain.Add(lstNewPhotoGain);
                    lstNewAndOldPhotoGain.Add(lstOldPhotoGain);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("List<PhotometricGainInfo> QueryPhotemetricValue(string strMethodName)==" + e.ToString(), Module.DAO);
            }

            return lstNewAndOldPhotoGain;
        }
        /// <summary>
        /// 获取历史光度计
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <returns></returns>
        //public List<OffSetGain> QueryOldPhotemetricValue(string strMethodName)
        //{
        //    List<OffSetGain> lstPhotoGain = new List<OffSetGain>();
        //    try
        //    {
        //        lstPhotoGain = ism_SqlMap.QueryForList<OffSetGain>("EquipmentManage." + strMethodName, null) as List<OffSetGain>;
        //    }
        //    catch (Exception e)
        //    {
        //        LogInfo.WriteErrorLog("QueryOldPhotemetricValue(string strMethodName)==" + e.ToString(), Module.DAO);
        //    }

        //    return lstPhotoGain;
        //}

        public OffSetGain GetLatestOffSetGain(int waveLength)
        {
            OffSetGain offSetGain = new OffSetGain();
            try
            {
                offSetGain = ism_SqlMap.QueryForObject("EquipmentManage.GetLatestOffSetGain", waveLength) as OffSetGain;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetLatestOffSetGain(int waveLength)==" + e.ToString(), Module.DAO);
            }
            return offSetGain;
        }

        public void AddLatestOffSetGain(OffSetGain offSetGain)
        {
            try
            {
                ism_SqlMap.Insert("EquipmentManage.AddLatestOffSetGain", offSetGain);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetLatestOffSetGain(int waveLength)==" + e.ToString(), Module.DAO);
            }
        }

        public void DeleteOldOffSetGain(int waveLength)
        {
            try
            {
                ism_SqlMap.Delete("EquipmentManage.DeleteOldOffSetGain", waveLength);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteOldOffSetGain(int waveLength)==" + e.ToString(), Module.DAO);
            }
        }

        public void AddOldOffSetGain(OffSetGain offSetGain)
        {
            try
            {
                ism_SqlMap.Insert("EquipmentManage.AddOldOffSetGain", offSetGain);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddOldOffSetGain(OffSetGain offSetGain)==" + e.ToString(), Module.DAO);
            }
        }

        public void DeleteNewOffSetGain(int waveLength)
        {
            try
            {
                ism_SqlMap.Delete("EquipmentManage.DeleteNewOffSetGain", waveLength);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteNewOffSetGain(int waveLength)==" + e.ToString(), Module.DAO);
            }
        }

        public int InitialPhotometerManualCheck(string strMethodName, ManuOffsetGain manuOffsetGain)
        {
            int iResult = 0;
            try
            {
                // 1.删除表中数据
                ism_SqlMap.Delete("EquipmentManage.DeleteManuOffsetGainInfo", null);
                // 2.插入需扫描数据
                ism_SqlMap.Insert("EquipmentManage.AddManuOffsetGainInfo", manuOffsetGain);

                iResult = 1;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("InitialPhotometerManualCheck(string strMethodName, ManuOffsetGain manuOffsetGain)==" + e.ToString(), Module.DAO);
            }

            return iResult;
        }

        public int GetAllTasksCount(string strMethodName)
        {
            int taskCount = 0;
            try
            {
                List<int> taskList = (List<int>)ism_SqlMap.QueryForList<int>("PLCDataInfo." + strMethodName, null);
                foreach (int t in taskList)
                {
                    taskCount += t;
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("GetAllTasksCount(string strMethodName)==" + e.ToString(), Module.DAO);
            }
            return taskCount;
        }
    }
}
