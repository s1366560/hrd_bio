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
        /// <summary>
        /// 新增质控品
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="qcInfo"></param>
        /// <param name="lstQCRelationProInfo"></param>
        /// <returns></returns>
        public string AddQualityControl(string strDBMethod, QualityControlInfo qcInfo, List<QCRelationProjectInfo> lstQCRelationProInfo)
        {
            string strResult = "";
            try
            {
                // 1.判断质控品是否存在
                Hashtable ht = new Hashtable();
                ht.Add("QCName", qcInfo.QCName);
                ht.Add("LotNum", qcInfo.LotNum);
                ht.Add("HorizonLevel", qcInfo.HorizonLevel);
                ht.Add("Manufacturer", qcInfo.Manufacturer);
                int count = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCCountByUnique", ht);

                if (count > 0)
                {
                    strResult = "该质控品已存在，无法添加！";
                }
                else
                {
                    ism_SqlMap.Insert("QCMaintainInfo.AddQualityControl", qcInfo);

                    QualityControlInfo getQCInfo = ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCInfoByUnique", ht) as QualityControlInfo;
                    if (getQCInfo == null)
                    {
                        strResult = "质控品添加失败，请联系管理员！";
                    }
                    else
                    {
                        foreach (QCRelationProjectInfo qcRelationProInfo in lstQCRelationProInfo)
                        {
                            qcRelationProInfo.QCID = getQCInfo.QCID;
                            ism_SqlMap.Insert("QCMaintainInfo.AddQCRelationProject", qcRelationProInfo);
                        }

                        strResult = "已成功添加质控品信息！" + getQCInfo.QCID;
                    }

                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddQualityControl(string strDBMethod, QualityControlInfo qcInfo, List<QCRelationProjectInfo> lstQCRelationProInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }
        /// <summary>
        /// 修改质控品信息和检测项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="oldQCInfo"></param>
        /// <param name="newQCInfo"></param>
        /// <param name="lstQCRelationProInfo"></param>
        /// <returns></returns>
        public string EditQualityControl(string strDBMethod, QualityControlInfo oldQCInfo, QualityControlInfo newQCInfo, List<QCRelationProjectInfo> lisNewQCProjectinfo, List<QCRelationProjectInfo> lisOldQCProjectinfo)
        {
            string strResult = "质控品和项目信息修改成功！";
            ///修改过的项目靶值、SD信息
            List<QCRelationProjectInfo> lstEditCalibProinfo = new List<QCRelationProjectInfo>();
            //新的项目靶值、SD信息
            List<QCRelationProjectInfo> lstNewCalibProinfo = new List<QCRelationProjectInfo>();
            Hashtable hashtable = new Hashtable();
            try
            {
                int intQCID = newQCInfo.QCID;
                int i = 0;
                int count = 0;
                foreach (var item in lisNewQCProjectinfo)
                {
                    if (lisOldQCProjectinfo.Exists(x => x.ProjectName == item.ProjectName && x.SampleType == item.SampleType && x.TargetMean == item.TargetMean && x.TargetSD == item.TargetSD))
                        i++;
                    else if (lisOldQCProjectinfo.Exists(x => x.ProjectName == item.ProjectName && x.SampleType == item.SampleType))
                        lstEditCalibProinfo.Add(item);//编辑的靶值和SD
                    else
                        lstNewCalibProinfo.Add(item);//新增的靶值和SD
                }
                count += (int)ism_SqlMap.QueryForObject("QCMaintainInfo.GetQCTaskCountByCondition", intQCID);
                if (count > 0)
                {
                    return "该质控品或项目已在任务列表中，不能对其进行修改！";
                }

                if (newQCInfo.QCName == oldQCInfo.QCName && newQCInfo.HorizonLevel == oldQCInfo.HorizonLevel &&
                    newQCInfo.LotNum == oldQCInfo.LotNum && newQCInfo.Pos == oldQCInfo.Pos &&
                    newQCInfo.Manufacturer == oldQCInfo.Manufacturer && newQCInfo.InvalidDate == oldQCInfo.InvalidDate && i == lisOldQCProjectinfo.Count)//只修改了靶值和SD
                {
                    foreach (var item in lstNewCalibProinfo)
                    {
                        // values (#ProjectName#, #SampleType#, #QCID#, #TargetMean#, #TargetSD#, #Target2SD#, #Target3SD#)
                        item.QCID = intQCID;
                        ism_SqlMap.Insert("QCMaintainInfo.AddQCRelationProject", item);
                    }
                    return strResult;
                }
                else
                {
                    if (newQCInfo.QCName == oldQCInfo.QCName)
                    {

                        hashtable.Clear();
                        hashtable.Add("QCName", newQCInfo.QCName);
                        hashtable.Add("Pos", newQCInfo.Pos);
                        hashtable.Add("Manufacturer", newQCInfo.Manufacturer);
                        hashtable.Add("LotNum", newQCInfo.LotNum);
                        hashtable.Add("InvalidDate", newQCInfo.InvalidDate);
                        hashtable.Add("HorizonLevel", newQCInfo.HorizonLevel);
                        hashtable.Add("OldQCID", intQCID);
                        //修改质控品信息
                        ism_SqlMap.Update("QCMaintainInfo.EditQualityControl", hashtable);
                    }
                    else
                    {
                        int rows = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.GetQCByCondition", newQCInfo.QCName);
                        if (rows > 0)
                        {
                            return strResult = "质控品名称已存在！";
                        }
                        else
                        {
                            hashtable.Clear();
                            hashtable.Add("QCName", newQCInfo.QCName);
                            hashtable.Add("Pos", newQCInfo.Pos);
                            hashtable.Add("Manufacturer", newQCInfo.Manufacturer);
                            hashtable.Add("LotNum", newQCInfo.LotNum);
                            hashtable.Add("InvalidDate", newQCInfo.InvalidDate);
                            hashtable.Add("HorizonLevel", newQCInfo.HorizonLevel);
                            hashtable.Add("OldQCID", intQCID);
                            //修改质控品信息
                            ism_SqlMap.Update("QCMaintainInfo.EditQualityControl", hashtable);
                        }
                    }

                    if (lstEditCalibProinfo.Count > 0)
                    {

                        foreach (var item in lstEditCalibProinfo)
                        {
                            hashtable.Clear();
                            hashtable.Add("TargetMean", item.TargetMean);
                            hashtable.Add("TargetSD", item.TargetSD);
                            hashtable.Add("Target2SD", item.Target2SD);
                            hashtable.Add("Target3SD", item.Target3SD);
                            hashtable.Add("ProjectName", item.ProjectName);
                            hashtable.Add("QCID", intQCID);
                            //修改校准品项目信息对应的校准品浓度
                            ism_SqlMap.Update("QCMaintainInfo.UpdateQCProjectInfo", hashtable);
                        }
                    }
                    if (lstNewCalibProinfo.Count > 0)
                    {
                        foreach (var item in lstNewCalibProinfo)
                        {

                            item.QCID = intQCID;
                            ism_SqlMap.Insert("QCMaintainInfo.AddQCRelationProject", item);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("EditQualityControl(string strDBMethod, QualityControlInfo oldQCInfo, QualityControlInfo newQCInfo, List<QCRelationProjectInfo> lstQCRelationProInfo)==" + e.ToString(), Module.QualityControl);
                return strResult = "质控品和项目信息修改失败！";
            }
            return strResult;


        }
        /// <summary>
        /// 根据项目名称和样本类型获取质控任务
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="QCTask"></param>
        /// <returns></returns>
        public int QueryQCTaskByProjectAndSamType(string strMethodName, int qcId)
        {
            int QCTaskCount = 0;
            try
            {
                QCTaskCount = (int)ism_SqlMap.QueryForObject("QCTaskInfo." + strMethodName, qcId);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCTaskByProjectAndSamType(string strMethodName, QCinfoTask QCTask)==" + e.ToString(), Module.QualityControl);
            }
            return QCTaskCount;
        }
        /// <summary>
        /// 获取所有质控品项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<string> QueryQCPosition(string strDBMethod)
        {
            List<string> lstQCPosition = new List<string>();

            try
            {
                lstQCPosition = (List<string>)ism_SqlMap.QueryForList<string>("QCMaintainInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCPosition(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstQCPosition;
        }

        public List<QualityControlInfo> QueryQCAllInfo(string strDBMethod)
        {
            List<QualityControlInfo> lstQCInfos = new List<QualityControlInfo>();
            try
            {
                lstQCInfos = (List<QualityControlInfo>)ism_SqlMap.QueryForList<QualityControlInfo>("QCMaintainInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCAllInfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }
            return lstQCInfos;
        }
        /// <summary>
        /// 获取所有校准品对应的所有生化项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="qcInfo"></param>
        /// <returns></returns>
        public List<QCRelationProjectInfo> QueryRelativelyProjectByQCInfo(string strDBMethod, string qcInfo)
        {
            List<QCRelationProjectInfo> qCRelatePros = new List<QCRelationProjectInfo>();
            try
            {
                //Hashtable ht = new Hashtable();
                //ht.Add("QCName", qcInfo.QCName);
                //ht.Add("LotNum", qcInfo.LotNum);
                //ht.Add("HorizonLevel", qcInfo.HorizonLevel);
                //ht.Add("Manufacturer", qcInfo.Manufacturer);
                //QualityControlInfo getQCInfo = ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCInfoByUnique", ht) as QualityControlInfo;
                //if (getQCInfo != null)
                qCRelatePros = (List<QCRelationProjectInfo>)ism_SqlMap.QueryForList<QCRelationProjectInfo>("QCMaintainInfo." + strDBMethod, qcInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryRelativelyProjectByQCInfo(string strDBMethod, QualityControlInfo qcInfo)==" + e.ToString(), Module.QualityControl);
            }
            return qCRelatePros;
        }



        public int EditQCRelateProInfo(string strDBMethod, QualityControlInfo QCInfo, List<QCRelationProjectInfo> lstQCRelationProInfo)
        {
            int strResult = 0;
            try
            {
                int intQCID = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQualityControlQCID", QCInfo);
                // 1.删除之前的数据
                int deleteProInfo = ism_SqlMap.Delete("QCMaintainInfo.DeleteQCRelateProInfoByQCID", intQCID);
                // 2.新增新的数据
                foreach (QCRelationProjectInfo qcProInfo in lstQCRelationProInfo)
                {
                    qcProInfo.QCID = intQCID;
                    ism_SqlMap.Insert("QCMaintainInfo.AddQCRelationProject", qcProInfo);
                }

                strResult = 1;
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("EditQCRelateProInfo(string strDBMethod, QualityControlInfo QCInfo, List<QCRelationProjectInfo> lstQCRelationProInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }

        public int LockQualityControl(string strDBMethod, QualityControlInfo QCInfo)
        {
            int strResult = 0;
            try
            {
                strResult = ism_SqlMap.Update("QCMaintainInfo." + strDBMethod, QCInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("LockQualityControl(string strDBMethod, QualityControlInfo QCInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }

        public int UnLockQualityControl(string strDBMethod, QualityControlInfo QCInfo)
        {
            int strResult = 0;
            try
            {
                strResult = ism_SqlMap.Update("QCMaintainInfo." + strDBMethod, QCInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UnLockQualityControl(string strDBMethod, QualityControlInfo QCInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }
        /// <summary>
        /// 删除质控品信息和对应的检测项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="QCInfo"></param>
        /// <returns></returns>
        public string DeleteQualityControl(string strDBMethod, QualityControlInfo QCInfo)
        {
            string strResult = string.Empty;

            try
            {
                List<int> lstReturnValues = (List<int>)ism_SqlMap.QueryForList<int>("QCMaintainInfo.QueryQCBeUsed", QCInfo);
                int intReturnValue = 0;

                foreach (int value in lstReturnValues)
                {
                    intReturnValue += value;
                }


                if (intReturnValue > 0)
                {
                    strResult = "该质控品已被使用，无法删除！";
                }
                else
                {
                    //删除质控品对应的生化项目
                    int deleteAssayInfo = ism_SqlMap.Delete("QCMaintainInfo.DeleteQCRelateProInfoByQCID", QCInfo.QCID);
                    if (deleteAssayInfo == 0)
                    {
                        return "删除失败！";
                    }
                    intReturnValue = ism_SqlMap.Delete("QCMaintainInfo.LogicalDeleteQC", QCInfo);

                    if (intReturnValue > 0)
                    {
                        strResult = "删除成功！";
                    }
                    else
                    {
                        strResult = "删除失败！";
                    }
                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteQualityControl(string strDBMethod, QualityControlInfo QCInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }
        /// <summary>
        /// 根据传递的条件输出质控品对应的项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="qcProjectInfo"></param>
        /// <returns></returns>
        public int DeleteQCProjectInfo(string strDBMethod, QCRelationProjectInfo qcProjectInfo)
        {
            int count = 0;
            try
            {
                count = (int)ism_SqlMap.QueryForObject("QCTaskInfo.QueryTaskTbCount" , string.Format("select count(*) from qctasktb where QCID= {0} and ProjectName = '{1}'", qcProjectInfo.QCID, qcProjectInfo.ProjectName));
                if (count > 0)
                {
                    return 0;
                }
                else
                {
                    count = ism_SqlMap.Delete("QCMaintainInfo." + strDBMethod, string.Format("delete from qcrelationprojecttb where QCID= {0} and ProjectName = '{1}'", qcProjectInfo.QCID, qcProjectInfo.ProjectName));
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("DeleteQCProjectInfo(string strDBMethod, QCReactionProcessInfo qcProjectInfo) == " + ex.ToString(), Module.QualityControl);
            }
            return count;
        }
        /// <summary>
        /// 获取质控结果状态信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="qCResForUIInfo"></param>
        /// <returns></returns>
        public List<QCResultForUIInfo> QueryQCResultInfo(string strDBMethod, QCResultForUIInfo qCResForUIInfo)
        {
            List<QCResultForUIInfo> lstQCResInfos = new List<QCResultForUIInfo>();
            List<QCResultForUIInfo> lstQCResInfosForManual = new List<QCResultForUIInfo>();
            List<int> lstInt = new List<int>();
            List<QCResultForUIInfo> lstQcStateInfo = new List<QCResultForUIInfo>();
            List<QCResultForUIInfo> lstNotSortQcProjectName = new List<QCResultForUIInfo>();
            try
            {
                lstQCResInfos = (List<QCResultForUIInfo>)ism_SqlMap.QueryForList<QCResultForUIInfo>("QCResultInfo." + strDBMethod, qCResForUIInfo);

                lstQCResInfosForManual = (List<QCResultForUIInfo>)ism_SqlMap.QueryForList<QCResultForUIInfo>("QCResultInfo.QueryQCResultInfoForManual", qCResForUIInfo);
                lstQCResInfos.AddRange(lstQCResInfosForManual);
                lstInt.AddRange(lstQCResInfos.Select(r => Convert.ToInt32((r.ProjectName == null ? "" : r.ProjectName.Substring(0, r.ProjectName.IndexOf(".") < 0 ? 0 : r.ProjectName.IndexOf(".")))
                     == "" ? "10000000" : r.ProjectName.Substring(0, r.ProjectName.IndexOf(".")) )).ToList());
                lstInt.Sort();
                List<int> lstResult = lstInt.Union(lstInt).ToList<int>();
                foreach (int i in lstResult)
                {
                    List<QCResultForUIInfo> qcResult = lstQCResInfos.FindAll(x => Convert.ToInt32(
                        (x.ProjectName == null ? "" : x.ProjectName.Substring(0, x.ProjectName.IndexOf('.') < 0 ? 0 : x.ProjectName.IndexOf('.') ) ) == "" ? "10000000" : x.ProjectName.Substring(0, x.ProjectName.IndexOf('.'))  ) == i);
                    if (qcResult != null)
                    {
                        lstQcStateInfo.AddRange(qcResult);
                    }
                }
                lstQcStateInfo.AddRange(lstNotSortQcProjectName);


            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCResultInfo(string strDBMethod, QCResultForUIInfo qCResForUIInfo)==" + e.ToString(), Module.QualityControl);
            }

            return lstQcStateInfo;
        }

        public List<QualityControlInfo> QueryQCInfosForAddQCResult(string strDBMethod)
        {
            List<QualityControlInfo> lstQCInfos = new List<QualityControlInfo>();

            try
            {
                lstQCInfos = (List<QualityControlInfo>)ism_SqlMap.QueryForList<QualityControlInfo>("QCMaintainInfo.QueryQCAllInfo", null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCInfosForAddQCResult(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstQCInfos;
        }

        public List<string> QueryProjectName(string strDBMethod)
        {
            List<string> lstProName = new List<string>();
            List<int> lstInt = new List<int>();
            List<string> lstProjectNames = new List<string>();
            List<string> lstNotSortProjectName = new List<string>();
            try
            {
                lstProName = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo.QueryAssayProAllInfoByDistinctProName", null);
                foreach (string projectName in lstProName)
                {
                    int s = projectName.IndexOf('.');
                    if (s < 0)
                    {
                        lstNotSortProjectName.Add(projectName);
                        continue;
                    }
                    lstInt.Add(Convert.ToInt32(projectName.Substring(0, s)));
                }
                lstInt.Sort();
                foreach (int i in lstInt)
                {
                    foreach (string proName in lstProName)
                    {
                        int s = proName.IndexOf('.');
                        if (s < 0)
                        {
                            continue;
                        }
                        if (i == Convert.ToInt32(proName.Substring(0, s)))
                        {
                            lstProjectNames.Add(proName);
                        }
                    }
                }
                lstProjectNames.AddRange(lstNotSortProjectName);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProjectInfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstProjectNames;
        }

        public string EditQCResultForManual(string strDBMethod, QCResultForUIInfo qcResOldInfo, QCResultForUIInfo qcResNewInfo)
        {
            string strResult = "添加成功！";

            try
            {
                QualityControlInfo qcInfo = new QualityControlInfo();
                qcInfo.QCName = qcResNewInfo.QCName;
                qcInfo.LotNum = qcResNewInfo.LotNum;
                qcInfo.HorizonLevel = qcResNewInfo.HorizonLevel;
                qcInfo.Manufacturer = qcResNewInfo.Manufacturer;
                int qCID = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQualityControlQCID", qcInfo);

                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", qcResNewInfo.ProjectName);
                ht.Add("SampleType", qcResNewInfo.SampleType);
                ht.Add("QCID", qCID);
                ht.Add("StartTime", qcResNewInfo.SampleCreateTime);

                string SampleNum = (string)ism_SqlMap.QueryForObject("QCResultInfo.QueryQCSampleNum", ht);

                int i = ism_SqlMap.Update("QCResultInfo.EditQCResultForLogicalEdit", ht);

                ht.Add("SampleNum", SampleNum);
                ht.Add("ConcResult", qcResNewInfo.ConcResult);

                if (i > 0)
                {
                    ism_SqlMap.Insert("QCResultInfo.EditQCResultForManual", ht);
                }
                else
                {
                    i = ism_SqlMap.Update("QCResultInfo.EditQCResultForLogicalEditForManual", ht);
                    if (i > 0)
                    {
                        ism_SqlMap.Insert("QCResultInfo.EditQCResultForManual", ht);
                    }
                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("EditQCResultForManual(string strDBMethod, QCResultForUIInfo qcResOldInfo, QCResultForUIInfo qcResNewInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }

        public string AddQCResultForManual(string strDBMethod, QCResultForUIInfo qcResNewInfo)
        {
            string strResult = "";
            try
            {
                // 1.判断质控品是否存在
                Hashtable ht = new Hashtable();
                ht.Add("QCName", qcResNewInfo.QCName);
                ht.Add("LotNum", qcResNewInfo.LotNum);
                ht.Add("HorizonLevel", qcResNewInfo.HorizonLevel);
                ht.Add("Manufacturer", qcResNewInfo.Manufacturer);
                int intQC = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCCountByUnique", ht);

                if (intQC <= 0)
                {
                    strResult = "此质控品不存在，请使用已存在的质控品！";
                }
                else
                {
                    // 2.判断质控品是否包含质控项目
                    QualityControlInfo qcInfo = new QualityControlInfo();
                    qcInfo.QCName = qcResNewInfo.QCName;
                    qcInfo.LotNum = qcResNewInfo.LotNum;
                    qcInfo.HorizonLevel = qcResNewInfo.HorizonLevel;
                    qcInfo.Manufacturer = qcResNewInfo.Manufacturer;
                    int qCID = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQualityControlQCID", qcInfo);

                    ht.Clear();
                    ht.Add("QCID", qCID);
                    ht.Add("ProjectName", qcResNewInfo.ProjectName);
                    ht.Add("SampleType", qcResNewInfo.SampleType);
                    int projectCount = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.JudgeQCIsContainProject", ht);//JudgeQCResultForUserIsExist

                    if (projectCount <= 0)
                    {
                        strResult = "此质控品不包含对应的项目，请使用质控品包含的项目！";
                    }
                    else
                    {
                        // 3.判断质控品结果数据是否已存在
                        ht.Clear();
                        ht.Add("ProjectName", qcResNewInfo.ProjectName);
                        ht.Add("SampleType", qcResNewInfo.SampleType);
                        ht.Add("QCID", qCID);
                        ht.Add("StartTime", qcResNewInfo.SampleCreateTime);
                        int QCCount = (int)ism_SqlMap.QueryForObject("QCResultInfo.JudgeQCResultIsExist", ht);
                        QCCount += (int)ism_SqlMap.QueryForObject("QCResultInfo.JudgeQCResultForUserIsExist", ht);
                        if (QCCount > 0)
                        {
                            strResult = "此质控信息已存在，请重新修改信息并提交！";
                        }
                        else
                        {
                            // 4.Add
                            ht.Clear();
                            ht.Add("SampleNum", "");
                            ht.Add("ProjectName", qcResNewInfo.ProjectName);
                            ht.Add("SampleType", qcResNewInfo.SampleType);
                            ht.Add("QCID", qCID);
                            ht.Add("ConcResult", qcResNewInfo.ConcResult);
                            ht.Add("StartTime", qcResNewInfo.SampleCreateTime);
                            ism_SqlMap.Insert("QCResultInfo.EditQCResultForManual", ht);
                            strResult = "添加成功！";
                        }
                    }

                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddQCResultForManual(string strDBMethod, QCResultForUIInfo qcResNewInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }

        public string DeleteQCResult(string strDBMethod, QCResultForUIInfo qcResInfo)
        {
            string strResult = "";
            try
            {
                QualityControlInfo qcInfo = new QualityControlInfo();
                qcInfo.QCName = qcResInfo.QCName;
                qcInfo.LotNum = qcResInfo.LotNum;
                qcInfo.HorizonLevel = qcResInfo.HorizonLevel;
                qcInfo.Manufacturer = qcResInfo.Manufacturer;
                int qCID = (int)ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQualityControlQCID", qcInfo);


                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", qcResInfo.ProjectName);
                ht.Add("SampleType", qcResInfo.SampleType);
                ht.Add("QCID", qCID);
                ht.Add("StartTime", qcResInfo.SampleCreateTime);

                int i = ism_SqlMap.Update("QCResultInfo.DeleteQCResult", ht);

                if (i > 0)
                {
                    strResult = "删除成功！";
                }
                else
                {
                    i = ism_SqlMap.Update("QCResultInfo.DeleteQCResultForManual", ht);
                    if (i > 0)
                    {
                        strResult = "删除成功！";
                    }
                    else
                    {
                        strResult = "删除失败！";
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteQCResult(string strDBMethod, QCResultForUIInfo qcResInfo)==" + e.ToString(), Module.QualityControl);
            }

            return strResult;
        }

        public TimeCourseInfo QueryTimeCourseByQCInfo(string strDBMethod, QCResultForUIInfo qcResInfo, string dateTime)
        {
            TimeCourseInfo qCTimeCourse =null;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", qcResInfo.ProjectName);
                ht.Add("SampleType", qcResInfo.SampleType);
                ht.Add("SampleCreateTime", dateTime);
                int TCNO = 0;
                TCNO = (int)ism_SqlMap.QueryForObject("QCResultInfo.QueryQualityControlResultTCNO", ht);
                if (TCNO > 0)
                {
                    qCTimeCourse = (TimeCourseInfo)ism_SqlMap.QueryForObject("PLCDataInfo." + strDBMethod,
                       string.Format("select * from timecoursetb where  TimeCourseNO={0} and CONVERT(varchar(50),DrawDate, 120) like '%{1}%'", TCNO, dateTime.Substring(0, 10)));
                    if (qCTimeCourse == null)
                    { 
                        qCTimeCourse = (TimeCourseInfo)ism_SqlMap.QueryForObject("PLCDataInfo." + strDBMethod,
                        string.Format("select * from TimeCourseBackUpTb where  TimeCourseNO={0} and CONVERT(varchar(50),DrawDate, 120) like '%{1}%'", TCNO, dateTime.Substring(0,10)));
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryTimeCourseByQCInfo(string strDBMethod, QCResultForUIInfo qcResInfo)==" + e.ToString(), Module.QualityControl);
            }

            return qCTimeCourse;
        }
        /// <summary>
        /// 获取质控项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<QCRelationProjectInfo> GetQCRelationProjectInfo(string strDBMethod)
        {
            List<QCRelationProjectInfo> lstQCRelationProjects = null;
            try
            {
                lstQCRelationProjects = (List<QCRelationProjectInfo>)ism_SqlMap.QueryForList<QCRelationProjectInfo>("QCMaintainInfo." + strDBMethod, null);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetQCRelationProjectInfo(string strDBMethod) == " + ex.ToString(), Module.QualityControl);
            }
            return lstQCRelationProjects;
        }

        public List<QCResultForUIInfo> QueryQCResultForQCGraphics(string strDBMethod, QCResultForUIInfo qcResForUIInfo)
        {
            List<QCResultForUIInfo> lstQCResForUIInfo = new List<QCResultForUIInfo>();

            try
            {
                lstQCResForUIInfo = (List<QCResultForUIInfo>)ism_SqlMap.QueryForList<QCResultForUIInfo>("QCResultInfo." + strDBMethod, qcResForUIInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("Dictionary<string, List<float>> QueryQCResultForQCGraphics(string strDBMethod, QCResultForUIInfo qcResForUIInfo)==" + e.ToString(), Module.QualityControl);
            }

            return lstQCResForUIInfo;
        }
        public List<QCTaskInfo> QueryBigestQCTaskInfoForToday(string strDBMethod)
        {
            //int intSampleNum = 0;
            List<QCTaskInfo> lstQCTaskInfo = null;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CreateDateStart", DateTime.Now.Date);
                ht.Add("CreateDateEnd", DateTime.Now.Date.AddDays(1));
                lstQCTaskInfo = (List<QCTaskInfo>)ism_SqlMap.QueryForList<QCTaskInfo>("QCTaskInfo." + strDBMethod, ht);
                //List<int> lstNums = new List<int>();
                //foreach (string sampleNum in lstSampleNums)
                //{
                //    lstNums.Add(System.Convert.ToInt32(sampleNum.Substring(1)));
                //}
                //lstNums.Sort();
                //if (lstNums.Count > 0)
                //    intSampleNum = lstNums[lstNums.Count - 1];
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryBigestQCSampleNumForToday(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstQCTaskInfo;
        }

        public List<string> QueryProjectNameInfoByQC(string strDBMethod, QualityControlInfo qcInfo, string strSampleType,out int qcId)
        {
            List<string> lstResults = new List<string>();
            QualityControlInfo quanlityControlInfo = new QualityControlInfo();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("QCName", qcInfo.QCName);
                ht.Add("LotNum", qcInfo.LotNum);
                ht.Add("HorizonLevel", qcInfo.HorizonLevel);
                ht.Add("Manufacturer", qcInfo.Manufacturer);
                quanlityControlInfo = ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCInfoByUnique", ht) as QualityControlInfo;

                ht.Clear();
                ht.Add("QCID", quanlityControlInfo.QCID);
                ht.Add("SampleType", strSampleType);

                lstResults = (List<string>)ism_SqlMap.QueryForList<string>("QCMaintainInfo." + strDBMethod, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProjectNameInfoByQC(string strDBMethod, QualityControlInfo qcInfo)==" + e.ToString(), Module.QualityControl);
            }
            qcId = quanlityControlInfo.QCID;
            return lstResults;
        }

        public string AddQCTask(string strDBMethod, List<QCTaskInfo> lstQCTaskInfos)
        {
            string strResult = "添加质控任务成功！";

            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("Pos", lstQCTaskInfos[0].Position);

                //QCName=#QCName# and LotNum=#LotNum# and HorizonLevel=#HorizonLevel# and Manufacturer=#Manufacturer#
                QualityControlInfo getQCInfo = ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCInfoByPos", ht) as QualityControlInfo;

                foreach (QCTaskInfo qcTaskInfo in lstQCTaskInfos)
                {
                    qcTaskInfo.QCID = getQCInfo.QCID;
                    ism_SqlMap.Insert("QCTaskInfo." + strDBMethod, qcTaskInfo);

                    ht.Clear();
                    ht.Add("SampleNum", qcTaskInfo.SampleNum);
                    ht.Add("ProjectName", qcTaskInfo.ProjectName);
                    ht.Add("SampleType", qcTaskInfo.SampleType);
                    ht.Add("QCID", qcTaskInfo.QCID);
                    ht.Add("StateTime", DateTime.Now);
                    ht.Add("EndTime", DateTime.Now.AddDays(1));
                    int result = (int)ism_SqlMap.QueryForObject("QCResultInfo.ConditionalQuery", ht);
                    if (result > 0)
                    {
                        return strResult;
                    }
                    else
                    {
                        ht.Clear();
                        ht.Add("SampleNum", qcTaskInfo.SampleNum);
                        ht.Add("ProjectName", qcTaskInfo.ProjectName);
                        ht.Add("SampleType", qcTaskInfo.SampleType);
                        ht.Add("QCID", qcTaskInfo.QCID);
                        ht.Add("SampleCreateTime", qcTaskInfo.CreateDate);
                        ism_SqlMap.Insert("QCResultInfo.AddQualityControlResult", ht);
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddQCTask(string strDBMethod, List<QCTaskInfo> lstQCTaskInfos)==" + e.ToString(), Module.QualityControl);
                strResult = "添加质控任务失败！";
            }

            return strResult;
        }

        public List<QCTaskInfo> QueryQCTaskForLstv(string strDBMethod)
        {
            List<QCTaskInfo> lstQCTaskInfos = new List<QCTaskInfo>();

            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("starttime", DateTime.Now.ToShortDateString());
                ht.Add("endtime", DateTime.Now.AddDays(1).ToShortDateString());

                lstQCTaskInfos = (List<QCTaskInfo>)ism_SqlMap.QueryForList<QCTaskInfo>("QCTaskInfo." + strDBMethod, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCTaskForLstv(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstQCTaskInfos;
        }

        public QCTaskInfoQuery QueryNextQCTaskBySampleNum(string strDBMethod, string SampleNum)
        {
            List<QCTaskInfo> lstQCTaskInfos = new List<QCTaskInfo>();
            QCTaskInfoQuery qcTaskInfoQuery = new QCTaskInfoQuery();

            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("starttime", DateTime.Now.Date);
                ht.Add("endtime", DateTime.Now.AddDays(1).Date);
                ht.Add("SampleNum", SampleNum);

                lstQCTaskInfos = (List<QCTaskInfo>)ism_SqlMap.QueryForList<QCTaskInfo>("QCTaskInfo." + strDBMethod, ht);


                QualityControlInfo qcInfo = new QualityControlInfo();
                if (lstQCTaskInfos.Count > 0)
                {
                    ht.Clear();
                    ht.Add("QCID", lstQCTaskInfos[0].QCID);
                    qcInfo = (QualityControlInfo)ism_SqlMap.QueryForObject("QCMaintainInfo.QueryQCInfoByQCID", ht);
                    ht.Add("SampleType", lstQCTaskInfos[0].SampleType);
                    List<string> qcRelativePros = (List<string>)ism_SqlMap.QueryForList<string>("QCMaintainInfo.QueryProjectNameInfoByQC", ht);



                    qcTaskInfoQuery.QCName = qcInfo.QCName;
                    qcTaskInfoQuery.SampleType = lstQCTaskInfos[0].SampleType;
                    qcTaskInfoQuery.SampleNum = lstQCTaskInfos[0].SampleNum;
                    qcTaskInfoQuery.Position = lstQCTaskInfos[0].Position;
                    qcTaskInfoQuery.Manufacturer = qcInfo.Manufacturer;
                    qcTaskInfoQuery.LotNum = qcInfo.LotNum;
                    qcTaskInfoQuery.LevelConc = qcInfo.HorizonLevel;
                    qcTaskInfoQuery.QCRelativePros = qcRelativePros;
                    foreach (QCTaskInfo qcTask in lstQCTaskInfos)
                    {
                        qcTaskInfoQuery.Projects.Add(qcTask.ProjectName);
                    }
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryNextQCTaskBySampleNum(string strDBMethod, string SampleNum)==" + e.ToString(), Module.QualityControl);
            }

            return qcTaskInfoQuery;
        }

        public List<string> QueryAssayProNameAllInfo(string strDBMethod, string sampleType)
        {
            List<string> lstProName = new List<string>();
            List<int> lstInt = new List<int>();
            List<string> lstProjectNames = new List<string>();
            List<string> lstNotSortProjectName = new List<string>();
            try
            {
                lstProName = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo.ProjectPageinfoBySampleType", sampleType);
                foreach (string projectName in lstProName)
                {
                    int s = projectName.IndexOf('.');
                    if (s < 0)
                    {
                        lstNotSortProjectName.Add(projectName);
                        continue;
                    }
                    lstInt.Add(Convert.ToInt32(projectName.Substring(0, s)));
                }
                lstInt.Sort();
                foreach (int i in lstInt)
                {
                    foreach (string proName in lstProName)
                    {
                        int s = proName.IndexOf('.');
                        if (s < 0)
                        {
                            continue;
                        }
                        if (i == Convert.ToInt32(proName.Substring(0, s)))
                        {
                            lstProjectNames.Add(proName);
                        }
                    }
                }
                lstProjectNames.AddRange(lstNotSortProjectName);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryAssayProNameAllInfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstProjectNames;
        }

        /// <summary>
        /// 根据样本类型获取所有的校准品项目名称
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="sampleType"></param>
        /// <returns></returns>
        public List<string> QueryProjectNameInfoByCalib(string strDBMethod, string sampleType)
        {
            List<string> lstProName = new List<string>();
            List<int> lstInt = new List<int>();
            List<string> lstProjectNames = new List<string>();
            List<string> lstNotSortProjectName = new List<string>();
            try
            {
                lstProName = (List<string>)ism_SqlMap.QueryForList<string>("AssayProjectInfo.ProjectPageinfoBySampleType", sampleType);
                foreach (string projectName in lstProName)
                {
                    int s = projectName.IndexOf('.');
                    if (s < 0)
                    {
                        lstNotSortProjectName.Add(projectName);
                        continue;
                    }
                    lstInt.Add(Convert.ToInt32(projectName.Substring(0, s)));
                }
                lstInt.Sort();
                foreach (int i in lstInt)
                {
                    foreach (string proName in lstProName)
                    {
                        int s = proName.IndexOf('.');
                        if (s < 0)
                        {
                            continue;
                        }
                        if (i == Convert.ToInt32(proName.Substring(0, s)))
                        {
                            lstProjectNames.Add(proName);
                        }
                    }
                }
                lstProjectNames.AddRange(lstNotSortProjectName);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryAssayProNameAllInfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstProjectNames;
        }

        public int QueryBigestCalibCTaskInfoForToday(string strDBMethod)
        {
            int intSampleNum = 0;
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CreateDateStart", DateTime.Now.Date);
                ht.Add("CreateDateEnd", DateTime.Now.Date.AddDays(1));
                List<string> lstsampleNum = (List<string>)ism_SqlMap.QueryForList<string>("Calibrator." + strDBMethod, ht);
                List<int> lstNums = new List<int>();
                foreach (string sampleNum in lstsampleNum)
                {
                    lstNums.Add(System.Convert.ToInt32(sampleNum.Substring(1)));
                }
                lstNums.Sort();
                if (lstNums.Count > 0)
                {
                    intSampleNum = lstNums[lstNums.Count - 1];
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteProcessLog("QueryBigestCalibCTaskInfoForToday(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }
            return intSampleNum;
        }

        public List<QualityControlInfo> QueryQCAllInfoForUnLocked(string strDBMethod)
        {
            List<QualityControlInfo> lstQCInfos = new List<QualityControlInfo>();
            try
            {
                lstQCInfos = (List<QualityControlInfo>)ism_SqlMap.QueryForList<QualityControlInfo>("QCMaintainInfo." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryQCAllInfoForUnLocked(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstQCInfos;
        }

        //public void InitMachineUpdateQCTaskState(string strDBMethod)
        //{
        //    try
        //    {
        //        Hashtable ht = new Hashtable();
        //        ht.Add("SendTimes", 0);
        //        ht.Add("FinishTimes", 0);
        //        ht.Add("TaskState", 0);
        //        ism_SqlMap.Update("PLCDataInfo.UpdateTaskState", ht);
        //        try
        //        {
        //            ht.Clear();
        //            ht.Add("SendTimes", 0);
        //            ht.Add("FinishTimes", 0);
        //            ht.Add("TaskState", 0);
        //            ism_SqlMap.Update("QCTaskInfo." + strDBMethod, ht);

        //            ht.Clear();
        //            ht.Add("SendTimes", 0);
        //            ht.Add("FinishTimes", 0);
        //            ht.Add("TaskState", 0);
        //            ism_SqlMap.Update("Calibrator.UpdateCalibTaskState", ht);
        //        }
        //        catch (Exception e)
        //        {
        //            LogInfo.WriteErrorLog("InitMachineUpdateQCTaskState(string strDBMethod, string Empty)==" + e.ToString(), Module.QualityControl);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        LogInfo.WriteErrorLog("InitMachineUpdateQCTaskState(string strDBMethod, string Empty)" + e.ToString(), Module.QualityControl);
        //    }
        //}
        
        public void DeletereagentStateInfoR2(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo)
        {
            try
            {
                Hashtable hashTable = new Hashtable();

                hashTable.Add("ReagentName", DeletereagentSettingsInfo.ReagentName);

                int count = ism_SqlMap.Delete("ReagentInfo." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.QualityControl);
            }
        }

        public List<Calibratorinfo> QueryCalibratorinfo(string strDBMethod, string p2)
        {
            List<Calibratorinfo> lstCalibratorinfo = new List<Calibratorinfo>();
            try
            {
                lstCalibratorinfo = (List<Calibratorinfo>)ism_SqlMap.QueryForList<Calibratorinfo>("Calibrator." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod, string p2)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibratorinfo;
        }

        public List<Calibratorinfo> QueryCalibPos(string strDBMethod, string p2)
        {
            List<Calibratorinfo> lstCalibratorinfo = new List<Calibratorinfo>();
            try
            {
                lstCalibratorinfo = (List<Calibratorinfo>)ism_SqlMap.QueryForList<Calibratorinfo>("Calibrator." + strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibPos(string strDBMethod, string p2)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibratorinfo;
        }

        public List<CalibratorProjectinfo> QueryCalibratorProjectinfo(string strDBMethod, string calibratorinfo)
        {
            List<CalibratorProjectinfo> lstCalibratorProjectinfo = new List<CalibratorProjectinfo>();
            try
            {
                //Hashtable hashTable = new Hashtable();

                //hashTable.Add("CalibName", calibratorinfo);
                lstCalibratorProjectinfo = (List<CalibratorProjectinfo>)ism_SqlMap.QueryForList<CalibratorProjectinfo>("Calibrator." + strDBMethod, calibratorinfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibratorProjectinfo;
        }

        public List<CalibratorProjectinfo> QueryProjectItemsByCalibration(string strDBMethod, string calibratorinfo)
        {
            List<CalibratorProjectinfo> lstCalibratorProjectinfo1 = new List<CalibratorProjectinfo>();
            try
            {
                Hashtable hashTable = new Hashtable();

                hashTable.Add("CalibName", calibratorinfo);
                lstCalibratorProjectinfo1 = (List<CalibratorProjectinfo>)ism_SqlMap.QueryForList<CalibratorProjectinfo>("Calibrator." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryProjectItemsByCalibration(string strDBMethod, string calibratorinfo)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibratorProjectinfo1;
        }

        /// <summary>
        ///     校准品维护界面：
        ///         新增校准品和项目信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库名</param>
        /// <param name="dataConfig">访问数据库参数</param>
        /// <param name="dataConfig1">访问数据库参数</param>
        /// <returns>返回成功或者失败信息</returns>
        public string AddCalibratorinfo(string strDBMethod, Calibratorinfo dataConfig, List<CalibratorProjectinfo> dataConfig1)
        {
            string strResult = "添加校准品任务成功！";
            List<CalibratorProjectinfo> lstCalibratorProjectinfo1 = new List<CalibratorProjectinfo>();
            try
            {
                Hashtable hashTable = new Hashtable();

                hashTable.Add("CalibName", dataConfig.CalibName);
                lstCalibratorProjectinfo1 = (List<CalibratorProjectinfo>)ism_SqlMap.QueryForList<CalibratorProjectinfo>("Calibrator." + "QueryCalibratorProjectinfoByCalibName", hashTable);
                //1.判断校准品名称是否存在
                if (lstCalibratorProjectinfo1.Count == 0)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("CalibName", dataConfig.CalibName);
                    ht.Add("InvalidDate", dataConfig.InvalidDate);
                    ht.Add("LotNum", dataConfig.LotNum);
                    ht.Add("Manufacturer", dataConfig.Manufacturer);
                    ht.Add("Pos", dataConfig.Pos);
                    ism_SqlMap.Insert("Calibrator." + strDBMethod, ht);

                    for (int i = 0; i < dataConfig1.Count; i++)
                    {
                        ht.Clear();
                        ht.Add("CalibConcentration", dataConfig1[i].CalibConcentration);
                        ht.Add("SampleType", dataConfig1[i].SampleType);
                        ht.Add("CalibName", dataConfig1[i].CalibName);
                        ht.Add("ProjectName", dataConfig1[i].ProjectName);
                        ism_SqlMap.Insert("Calibrator." + "AddCalibratorProjectinfo", ht);
                    }
                }
                else
                {

                    strResult = "你添加的校准品名称已存在！";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddCalibratorinfo(string strDBMethod, Calibratorinfo dataConfig, List<CalibratorProjectinfo> dataConfig1)==" + e.ToString(), Module.QualityControl);
                strResult = "添加校准品失败！";
            }

            return strResult;
        }

        /// <summary>
        ///     校准品维护界面：
        ///         修改项目信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库名</param>
        /// <param name="editCalibratorProjectinfo">参数</param>
        /// <returns>返回成功或者失败信息</returns>
        //public string AddCalibratorProjectinfo(string strDBMethod, List<CalibratorProjectinfo> editCalibratorProjectinfo)
        //{
        //    string strResult = "修改校准任务成功！";

        //    try
        //    {
        //        for (int i = 0; i < editCalibratorProjectinfo.Count; i++)
        //        {
        //        Hashtable ht = new Hashtable();
        //        ht.Add("CalibConcentration", editCalibratorProjectinfo[i].CalibConcentration);
        //        ht.Add("SampleType", editCalibratorProjectinfo[i].SampleType);
        //        ht.Add("CalibName", editCalibratorProjectinfo[i].CalibName);
        //        ht.Add("ProjectName", editCalibratorProjectinfo[i].ProjectName);          
        //        ism_SqlMap.Insert("Calibrator." + strDBMethod, ht);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        LogInfo.WriteErrorLog("EditCalibratorProjectinfo(string strDBMethod, List<CalibratorProjectinfo> dataConfig)==" + e.ToString(), Module.QualityControl);
        //        strResult = "修改校准任务失败！";
        //    }

        //    return strResult;
        //}

        /// <summary>
        ///     校准品维护界面:
        ///         删除校准品/项目信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库名</param>
        /// <param name="p2">参数</param>
        /// <returns>返回删成功或者失败</returns>
        public string DeleteCalibrationMaintain(string strDBMethod, List<CalibratorProjectinfo> lstCalibProjectInfo)
        {
            string strReturn = null;
            int strResult = 0;
            try
            {
                Hashtable has = new Hashtable();
                foreach (CalibratorProjectinfo calibPro in lstCalibProjectInfo)
                {
                    has.Clear();
                    has.Add("ProjectName", calibPro.ProjectName);
                    has.Add("SampleType", calibPro.SampleType);
                    strResult += (int)ism_SqlMap.QueryForObject("Calibrator.QueryCalibTaskCountByProName", has);

                }
                if (strResult == 0)
                {
                    for (int i = 0; i < lstCalibProjectInfo.Count; i++)
                    {
                        has.Clear();
                        has.Add("NumTime", i);
                        has.Add("calibratorName", lstCalibProjectInfo[i].CalibName);
                        has.Add("proName", lstCalibProjectInfo[i].ProjectName);
                        has.Add("samType", lstCalibProjectInfo[i].SampleType);
                        strResult += (int)ism_SqlMap.QueryForObject("Calibrator.DeleteCalibratorProjectRI", has);
                    }
                    if (strResult > 0)
                        strReturn = "删除成功！";
                    else
                        strReturn = "删除失败！";
                }
                else strReturn = "删除的校准品项目正在执行中！不能删除！";
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteCalibrationMaintain(string strDBMethod, List<CalibratorProjectinfo> lstCalibProjectInfo)==" + e.ToString(), Module.QualityControl);
                strReturn = "删除失败！";
            }
            return strReturn;
        }

        /// <summary>
        ///     校准品维护界面：
        ///         更新校准品和项目信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库名</param>
        /// <param name="Editcalibratorinfo">访问数据库校准品参数</param>
        /// <param name="p2">校准品名称</param>
        /// <param name="lisEditCalibratorProjectinfo">访问数据库项目信息参数</param>
        /// <returns>返回成功或者失败</returns>

        public string EditCalibratorinfo(string strDBMethod, Calibratorinfo newCalibinfo, Calibratorinfo oldCalibinfo, List<CalibratorProjectinfo> lisNewCalibratorProjectinfo, List<CalibratorProjectinfo> lisOldCalibratorProjectinfo)
        {
            string updateCalibResult = "校准品和项目信息修改成功！";
            ///修改过的项目浓度信息
            List<CalibratorProjectinfo> lstEditCalibProinfo = new List<CalibratorProjectinfo>();
            //新的项目浓度信息
            List<CalibratorProjectinfo> lstNewCalibProinfo = new List<CalibratorProjectinfo>();
            Hashtable hashtable = new Hashtable();
            try
            {
                int i = 0;
                int count = 0;
                bool IsUpdateCalibName = false;
                foreach (var item in lisNewCalibratorProjectinfo)
                {
                    if (lisOldCalibratorProjectinfo.Exists(x => x.ProjectName == item.ProjectName && x.SampleType == item.SampleType && x.CalibConcentration == item.CalibConcentration))
                        i++;
                    else if (lisOldCalibratorProjectinfo.Exists(x => x.ProjectName == item.ProjectName && x.SampleType == item.SampleType))
                        lstEditCalibProinfo.Add(item);//编辑的浓度
                    else
                        lstNewCalibProinfo.Add(item);//新增的浓度
                }
                if (newCalibinfo.CalibName == oldCalibinfo.CalibName && newCalibinfo.Pos == oldCalibinfo.Pos &&
                    newCalibinfo.InvalidDate == oldCalibinfo.InvalidDate && newCalibinfo.LotNum == oldCalibinfo.LotNum &&
                    newCalibinfo.Manufacturer == oldCalibinfo.Manufacturer && i == lisOldCalibratorProjectinfo.Count)//只修改了浓度
                {
                    foreach (var item in lstNewCalibProinfo)
                    {
                        hashtable.Clear();
                        hashtable.Add("ProjectName", item.ProjectName);
                        hashtable.Add("SampleType", item.SampleType);
                        hashtable.Add("CalibName", newCalibinfo.CalibName);
                        hashtable.Add("CalibConcentration", item.CalibConcentration);
                        ism_SqlMap.Insert("Calibrator.AddCalibratorProjectinfo", hashtable);
                    }
                    return updateCalibResult;
                }
                else
                {
                    hashtable.Clear();
                    hashtable.Add("CalibName", oldCalibinfo.CalibName);
                    count += (int)ism_SqlMap.QueryForObject("Calibrator.GetCalibTaskCountByCondition", hashtable);
                    if (count > 0)
                    {
                        return "该校准品或项目已在任务列表中，不能对其进行修改！";
                    }
                    else
                    {
                        if (newCalibinfo.CalibName == oldCalibinfo.CalibName && newCalibinfo.Pos == oldCalibinfo.Pos &&
                            (newCalibinfo.LotNum != oldCalibinfo.LotNum || newCalibinfo.InvalidDate != oldCalibinfo.InvalidDate || newCalibinfo.Manufacturer != oldCalibinfo.Manufacturer))
                        {
                            hashtable.Clear();
                            hashtable.Add("CalibName", oldCalibinfo.CalibName);
                            hashtable.Add("NewCalibName", oldCalibinfo.CalibName);
                            hashtable.Add("Pos", oldCalibinfo.Pos);
                            hashtable.Add("InvalidDate", newCalibinfo.InvalidDate);
                            hashtable.Add("LotNum", newCalibinfo.LotNum);
                            hashtable.Add("Manufacturer", newCalibinfo.Manufacturer);
                            //修改校准品信息
                            ism_SqlMap.Update("Calibrator.UpdateCalibrationInfo", hashtable);
                        }
                        else if (newCalibinfo.CalibName != oldCalibinfo.CalibName || newCalibinfo.Pos != oldCalibinfo.Pos)
                        {
                            if (newCalibinfo.CalibName != oldCalibinfo.CalibName)
                            {
                                int rows = (int)ism_SqlMap.QueryForObject("Calibrator.GetCalibCountByCondition", newCalibinfo.CalibName);
                                if (rows > 0)
                                {
                                    return updateCalibResult = "校准品名称已存在！";
                                }
                            }
                            IsUpdateCalibName = true;
                            hashtable.Clear();
                            hashtable.Add("CalibName", oldCalibinfo.CalibName);
                            hashtable.Add("NewCalibName", newCalibinfo.CalibName);
                            hashtable.Add("Pos", newCalibinfo.Pos);
                            hashtable.Add("InvalidDate", newCalibinfo.InvalidDate);
                            hashtable.Add("LotNum", newCalibinfo.LotNum);
                            hashtable.Add("Manufacturer", newCalibinfo.Manufacturer);
                            int Number = ism_SqlMap.Update("Calibrator.UpdateCalibrationInfo", hashtable);
                            if (Number > 0)
                            {
                                hashtable.Clear();
                                hashtable.Add("CalibName", oldCalibinfo.CalibName);
                                hashtable.Add("NewCalibName", newCalibinfo.CalibName);
                                //修改校准品项目信息对应的校准品名
                                int updateResult = ism_SqlMap.Update("Calibrator.UpdateCalibNameAll", hashtable);
                                if (updateResult > 0)
                                {
                                    UpdateCalibractionParamInfo(lisOldCalibratorProjectinfo, oldCalibinfo);
                                }
                            }
                        }
                        if (lstEditCalibProinfo.Count > 0)
                        {
                            if (IsUpdateCalibName == false)
                            {
                                UpdateCalibractionParamInfo(lstEditCalibProinfo, oldCalibinfo);
                            }
                            foreach (var item in lstEditCalibProinfo)
                            {
                                hashtable.Clear();
                                hashtable.Add("ProjectName", item.ProjectName);
                                hashtable.Add("CalibName", newCalibinfo.CalibName);
                                hashtable.Add("CalibConcentration", item.CalibConcentration);
                                //修改校准品项目信息对应的校准品浓度
                                ism_SqlMap.Update("Calibrator.UpdateCalibProjectInfo", hashtable);
                            }
                        }
                        if (lstNewCalibProinfo.Count > 0)
                        {

                            foreach (var item in lstNewCalibProinfo)
                            {
                                hashtable.Clear();
                                hashtable.Add("ProjectName", item.ProjectName);
                                hashtable.Add("SampleType", item.SampleType);
                                hashtable.Add("CalibName", newCalibinfo.CalibName);
                                hashtable.Add("CalibConcentration", item.CalibConcentration);
                                ism_SqlMap.Insert("Calibrator.AddCalibratorProjectinfo", hashtable);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("EditCalibratorinfo(string strDBMethod, Calibratorinfo Editcalibratorinfo, string p2, List<CalibratorProjectinfo> lisEditCalibratorProjectinfo)==" + e.ToString(), Module.QualityControl);
                return updateCalibResult = "校准品和项目信息修改失败！";
            }
            return updateCalibResult;
        }
        /// <summary>
        /// 修改校准品项目参数信息
        /// </summary>
        /// <param name="lstCalibProInfo"></param>
        /// <param name="oldCalibinfo"></param>
        private void UpdateCalibractionParamInfo(List<CalibratorProjectinfo> lstCalibProInfo, Calibratorinfo oldCalibinfo)
        {
            Hashtable hashtable = new Hashtable();
            try
            {
                foreach (var item in lstCalibProInfo)
                {
                    hashtable.Clear();
                    hashtable.Add("ProjectName", item.ProjectName);
                    hashtable.Add("SampleType", item.SampleType);
                    AssayProjectCalibrationParamInfo calibrationParamInfo = ism_SqlMap.QueryForObject("Calibrator.GetCalibParamProInfo", hashtable) as AssayProjectCalibrationParamInfo;
                    if (calibrationParamInfo != null && calibrationParamInfo.CalibrationMethod != null && calibrationParamInfo.CalibrationMethod != "")
                    {
                        hashtable.Clear();
                        hashtable.Add("ProjectName", item.ProjectName);
                        hashtable.Add("SampleType", item.SampleType);
                        if (calibrationParamInfo.CalibrationMethod == "K系数法")
                        {
                            if (calibrationParamInfo.CalibName0 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName0", "");
                                hashtable.Add("CalibPos0", "");
                                hashtable.Add("CalibConcentration0", 0);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else if (calibrationParamInfo.CalibrationMethod == "2点线性法")
                        {
                            if (calibrationParamInfo.CalibName0 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName0", "");
                                hashtable.Add("CalibPos0", "");
                                hashtable.Add("CalibConcentration0", 0);
                            }
                            else if (calibrationParamInfo.CalibName1 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName1", "");
                                hashtable.Add("CalibPos1", "");
                                hashtable.Add("CalibConcentration1", 0);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (calibrationParamInfo.CalibName0 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName0", "");
                                hashtable.Add("CalibPos0", "");
                                hashtable.Add("CalibConcentration0", 0);
                            }
                            else if (calibrationParamInfo.CalibName1 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName1", "");
                                hashtable.Add("CalibPos1", "");
                                hashtable.Add("CalibConcentration1", 0);
                            }
                            else if (calibrationParamInfo.CalibName2 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName2", "");
                                hashtable.Add("CalibPos2", "");
                                hashtable.Add("CalibConcentration2", 0);
                            }
                            else if (calibrationParamInfo.CalibName3 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName3", "");
                                hashtable.Add("CalibPos3", "");
                                hashtable.Add("CalibConcentration3", 0);
                            }
                            else if (calibrationParamInfo.CalibName4 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName4", "");
                                hashtable.Add("CalibPos4", "");
                                hashtable.Add("CalibConcentration4", 0);
                            }
                            else if (calibrationParamInfo.CalibName5 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName5", "");
                                hashtable.Add("CalibPos5", "");
                                hashtable.Add("CalibConcentration5", 0);
                            }
                            else if (calibrationParamInfo.CalibName6 == oldCalibinfo.CalibName)
                            {
                                hashtable.Add("CalibName6", "");
                                hashtable.Add("CalibPos6", "");
                                hashtable.Add("CalibConcentration6", 0);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        int resultCount = ism_SqlMap.Update("AssayProjectInfo.UpdateCalibParamInfo", hashtable);
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("UpdateCalibractionParamInfo(List<CalibratorProjectinfo> lstCalibProInfo, Calibratorinfo oldCalibinfo)==" + ex.ToString(), Module.QualityControl);
            }
        }

        //public int EditCalibratorProjectinfo (string strDBMethod, List<CalibratorProjectinfo> lisEditCalibratorProjectinfo, string p2)
        //{
        //    int intUpdate = 0;

        //    try
        //    {
        //        for (int i = 0; i < lisEditCalibratorProjectinfo.Count; i++)
        //        {
        //            Hashtable ht = new Hashtable();
        //            ht.Add("ProjectName", lisEditCalibratorProjectinfo[i].ProjectName);
        //            ht.Add("CalibConcentration", lisEditCalibratorProjectinfo[i].CalibConcentration);
        //            ht.Add("CalibName", lisEditCalibratorProjectinfo[i].CalibName);
        //            ht.Add("CalibNameOld", p2);
        //            intUpdate = ism_SqlMap.Update("Calibrator." + strDBMethod, ht);
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        LogInfo.WriteErrorLog("EditQCResultForManual(string strDBMethod, QCResultForUIInfo qcResOldInfo, QCResultForUIInfo qcResNewInfo)==" + e.ToString(), Module.QualityControl);
        //    }
        //    return intUpdate;
        //}

        /// <summary>
        /// 获取所有的校准结果信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库</param>
        /// <param name="p2">参数</param>
        /// <returns></returns>
        public List<CalibrationResultinfo> QueryCalibrationState(string strDBMethod, string p2)
        {
            List<CalibrationResultinfo> lstCalibrationResultinfo = new List<CalibrationResultinfo>();
            List<CalibrationResultinfo> lstCalibrationResult = new List<CalibrationResultinfo>();
            try
            {
                //ism_SqlMap.QueryForList<CalibrationResultinfo>("Calibrator." + strDBMethod, null);
                lstCalibrationResultinfo = (List<CalibrationResultinfo>)ism_SqlMap.QueryForList<CalibrationResultinfo>("Calibrator." + strDBMethod, null);
                List<int> lstint = new List<int>();
                List<CalibrationResultinfo> lststring = new List<CalibrationResultinfo>();
                foreach(CalibrationResultinfo c in lstCalibrationResultinfo)
                {
                    if (c.ProjectName.Contains('.'))
                    {
                        lstint.Add(int.Parse(c.ProjectName.Split('.')[0]));
                    }
                    else
                    {
                        lststring.Add(c);
                    }
                    lstint.Distinct();
                    lstint.Sort();
                }
                foreach(int i in lstint)
                {
                    CalibrationResultinfo calibration = lstCalibrationResultinfo.Find(x =>  x.ProjectName.Substring(0,i.ToString().Length) == i.ToString());
                    lstCalibrationResult.Add(calibration);
                }
                lstCalibrationResult.AddRange(lststring);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }
            return lstCalibrationResult;
        }
        /// <summary>
        /// 获取校准品对应的项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="ProjectName"></param>
        /// <param name="SampleType"></param>
        /// <returns></returns>
        public List<CalibratorProjectinfo> QueryCalibratorProinfo(string strDBMethod, string ProjectName, string SampleType)
        {
            List<CalibratorProjectinfo> lstCalibratorProjectinfo = new List<CalibratorProjectinfo>();
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ProjectName", ProjectName);
                hashTable.Add("SampleType", SampleType);
                lstCalibratorProjectinfo = (List<CalibratorProjectinfo>)ism_SqlMap.QueryForList<CalibratorProjectinfo>("Calibrator." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorProinfo(string strDBMethod, string ProjectName, string SampleType)==" + e.ToString(), Module.QualityControl);
            }
            return lstCalibratorProjectinfo;
        }

        public List<Calibratorinfo> QueryCalib(string strDBMethod, string p2)
        {
            List<Calibratorinfo> lstCalibratorinfo = new List<Calibratorinfo>();
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("CalibName", p2);
                lstCalibratorinfo = (List<Calibratorinfo>)ism_SqlMap.QueryForList<Calibratorinfo>("Calibrator." + strDBMethod, hashTable);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibratorinfo;
        }
        /// <summary>
        /// 根据项目信息删除校准曲线表数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="calibrationCurveInfo"></param>
        /// <returns></returns>
        public string DeleteCalibrationCurveInfo(string strDBMethod, List<CalibrationCurveInfo> calibrationCurveInfo)
        {
            string success = "删除成功！";
            try
            {
                Hashtable hashTable = new Hashtable();
                hashTable.Add("ProjectName", calibrationCurveInfo[0].ProjectName);
                //hashTable.Add("CalibType", calibrationCurveInfo[0].CalibType);
                hashTable.Add("SampleType", calibrationCurveInfo[0].SampleType);
                ism_SqlMap.Delete("Calibrator." + strDBMethod, hashTable);

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DeleteDataConfig(string strDBMethod, string dataConfig)==" + e.ToString(), Module.QualityControl);
                return "删除失败！";
            }
            return success;
        }
        /// <summary>
        /// 跟新校准参数和添加校准曲线
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="calibrationCurveInfo"></param>
        /// <returns></returns>
        public string UpdateCalibParamerterAndAddCalibCurveInfo(string strDBMethod, AssayProjectCalibrationParamInfo assayProInfo, List<CalibrationCurveInfo> calibrationCurveInfo)
        {
            string strResult = "校准参数信息保存成功！";

            try
            {
                int count = 0;
                ism_SqlMap.BeginTransaction();
                count = ism_SqlMap.Update("AssayProjectInfo." + strDBMethod, assayProInfo);
                if (count > 0)
                {
                    Hashtable ht = new Hashtable();
                    if (calibrationCurveInfo.Count == 1)
                    {
                        ht.Add("CalibConcentration", calibrationCurveInfo[0].CalibConcentration);
                        ht.Add("CalibName", calibrationCurveInfo[0].CalibName);
                        ht.Add("ProjectName", calibrationCurveInfo[0].ProjectName);
                        ht.Add("SampleType", calibrationCurveInfo[0].SampleType);
                        ht.Add("Pos", calibrationCurveInfo[0].Pos);
                        ht.Add("CalibTime", calibrationCurveInfo[0].CalibTime);
                        ht.Add("CalibType", calibrationCurveInfo[0].CalibType);
                        ht.Add("Factor", calibrationCurveInfo[0].Factor);

                        ism_SqlMap.Insert("Calibrator.AddCalibrationIsKCurveInfo", ht);
                    }
                    else
                    {
                        for (int i = 0; i < calibrationCurveInfo.Count; i++)
                        {
                            ht.Clear();
                            ht.Add("CalibConcentration", calibrationCurveInfo[i].CalibConcentration);
                            ht.Add("CalibName", calibrationCurveInfo[i].CalibName);
                            ht.Add("ProjectName", calibrationCurveInfo[i].ProjectName);
                            ht.Add("SampleType", calibrationCurveInfo[i].SampleType);
                            ht.Add("Pos", calibrationCurveInfo[i].Pos);
                            ht.Add("CalibTime", calibrationCurveInfo[i].CalibTime);
                            ht.Add("CalibType", calibrationCurveInfo[i].CalibType);
                            //ht.Add("Factor", calibrationCurveInfo[i].Factor);

                            ism_SqlMap.Insert("Calibrator." + "AddCalibrationCurveInfo", ht);
                        }
                    }
                }
                else
                    strResult = "校准参数保存失败！";
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateCalibParamerterAndAddCalibCurveInfo(string strDBMethod, AssayProjectRangeParamInfo assayProInfo, List<CalibrationCurveInfo> calibrationCurveInfo)==" + e.Message, Module.QualityControl);
                strResult = "校准参数保存失败！";
                ism_SqlMap.RollBackTransaction();
            }
            ism_SqlMap.CommitTransaction();
            return strResult;
        }

        public List<SDTTableItem> QueryCalibrationCurveInfo(string strDBMethod, CalibrationCurveInfo calibrationResult)
        {
            List<SDTTableItem> lstCalibratorinfo = new List<SDTTableItem>();
            try
            {
                if (calibrationResult.CalibType != "K系数法")
                {
                    Hashtable hashTable = new Hashtable();

                    hashTable.Add("ProjectName", calibrationResult.ProjectName);
                    hashTable.Add("SampleType", calibrationResult.SampleType);
                    hashTable.Add("CalibType", calibrationResult.CalibType);
                    hashTable.Add("SUCC", CalibRemarks.SUCC);
                    lstCalibratorinfo = (List<SDTTableItem>)ism_SqlMap.QueryForList<SDTTableItem>("Calibrator." + strDBMethod, hashTable);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibratorinfo;
        }

        /// <summary>
        /// 根据样本类型和项目名称查询校准参数信息 
        ///     把校准参数相关的数据保存到校准任务表中
        /// </summary>
        /// <param name="strDBMethod">访问数据库名</param>
        /// <param name="calibratorinfoTask">参数</param>
        /// <returns>返回校准任务数据</returns>
        public List<CalibratorinfoTask> QueryListCalibrationCurveInfo(string strDBMethod, List<CalibratorinfoTask> calibratorinfoTask)
        {
            List<AssayProjectCalibrationParamInfo> lstCalibrationParamInfos = new List<AssayProjectCalibrationParamInfo>();

            if (calibratorinfoTask != null)
            {

                //从校准参数中获取相关的校准任务信息
                try
                {
                    for (int i = 0; i < calibratorinfoTask.Count; i++)
                    {
                        Hashtable hashTable = new Hashtable();
                        hashTable.Add("ProjectName", calibratorinfoTask[i].ProjectName);
                        hashTable.Add("SampleType", calibratorinfoTask[i].SampleType);
                        AssayProjectCalibrationParamInfo lstCalibrationParamInfo = ism_SqlMap.QueryForObject("Calibrator.SelectCalibrationParamInfo", hashTable) as AssayProjectCalibrationParamInfo;
                        lstCalibrationParamInfos.Add(lstCalibrationParamInfo);
                    }
                }
                catch (Exception e)
                {
                    LogInfo.WriteErrorLog("SelectCalibrationCurveInfo" + e.ToString(), Module.QualityControl);
                }

                try
                {
                    for (int i = 0; i < lstCalibrationParamInfos.Count; i++)
                    {
                        List<CalibrationCurveInfo> calibPorInfo = new List<CalibrationCurveInfo>();
                        //存储校准任务和校准结果信息对象
                        AssayProjectCalibrationParamInfo assyCalibInfo = new AssayProjectCalibrationParamInfo();
                        assyCalibInfo.ProjectName = lstCalibrationParamInfos[i].ProjectName;
                        assyCalibInfo.SampleType = lstCalibrationParamInfos[i].SampleType;
                        assyCalibInfo.CalibrationTimes = lstCalibrationParamInfos[i].CalibrationTimes;
                        assyCalibInfo.CalibrationMethod = lstCalibrationParamInfos[i].CalibrationMethod;
                        assyCalibInfo.CalibrationTimes = lstCalibrationParamInfos[i].CalibrationTimes;
                        //根据校准名称存储校准任务和校准结果信息
                        CalibrationResultinfo calibResultInfo;

                        //插入校准任务中
                        Hashtable ht = new Hashtable();
                        ht.Add("ProjectName", lstCalibrationParamInfos[i].ProjectName);
                        ht.Add("SampleType", lstCalibrationParamInfos[i].SampleType);
                        calibPorInfo = ism_SqlMap.QueryForList<CalibrationCurveInfo>("Calibrator.QueryCalibCurveByPorjectName", ht) as List<CalibrationCurveInfo>;
                        ////校准编号
                        //string s = calibratorinfoTask[i].SampleNum.Substring(0,1);
                        //int number = 0;
                        //try
                        //{
                        //    number = int.Parse(calibratorinfoTask[i].SampleNum.Substring(1).ToString());
                        //}
                        //catch (Exception e)
                        //{

                        //}


                        foreach (CalibrationCurveInfo c in calibPorInfo)
                        {
                            if (lstCalibrationParamInfos[i].CalibName0 == c.CalibName)
                            {

                                calibResultInfo = new CalibrationResultinfo();
                                //根据校准名称存储校准任务和校准结果信息
                                calibResultInfo.SampleNum = calibratorinfoTask[0].SampleNum;
                                calibResultInfo.CalibratorName = c.CalibName;
                                calibResultInfo.Pos = c.Pos;
                                calibResultInfo.CalibrationDT = calibratorinfoTask[0].CreateDate;
                                calibResultInfo.CalibConcentration = c.CalibConcentration;
                                //保存校准任务和结果方法
                                CreateTaskInfo(assyCalibInfo, calibResultInfo);
                                calibResultInfo = null;

                            }
                            else if (lstCalibrationParamInfos[i].CalibName1 == c.CalibName)
                            {
                                calibResultInfo = new CalibrationResultinfo();
                                calibResultInfo.CalibratorName = c.CalibName;
                                calibResultInfo.SampleNum = calibratorinfoTask[0].SampleNum;
                                calibResultInfo.Pos = c.Pos;
                                calibResultInfo.CalibrationDT = calibratorinfoTask[0].CreateDate;
                                calibResultInfo.CalibConcentration = c.CalibConcentration;
                                //保存校准任务和结果方法
                                CreateTaskInfo(assyCalibInfo, calibResultInfo);
                                calibResultInfo = null;
                            }
                            else if (lstCalibrationParamInfos[i].CalibName2 == c.CalibName)
                            {
                                calibResultInfo = new CalibrationResultinfo();
                                calibResultInfo.SampleNum = calibratorinfoTask[0].SampleNum;
                                calibResultInfo.CalibratorName = c.CalibName;
                                calibResultInfo.Pos = c.Pos;
                                calibResultInfo.CalibrationDT = calibratorinfoTask[0].CreateDate;
                                calibResultInfo.CalibConcentration = c.CalibConcentration;
                                //保存校准任务和结果方法
                                CreateTaskInfo(assyCalibInfo, calibResultInfo);
                                calibResultInfo = null;
                            }
                            else if (lstCalibrationParamInfos[i].CalibName3 == c.CalibName)
                            {
                                calibResultInfo = new CalibrationResultinfo();
                                calibResultInfo.SampleNum = calibratorinfoTask[0].SampleNum;
                                calibResultInfo.CalibratorName = c.CalibName;
                                calibResultInfo.Pos = c.Pos;
                                calibResultInfo.CalibrationDT = calibratorinfoTask[0].CreateDate;
                                calibResultInfo.CalibConcentration = c.CalibConcentration;
                                //保存校准任务和结果方法
                                CreateTaskInfo(assyCalibInfo, calibResultInfo);
                                calibResultInfo = null;
                            }
                            else if (lstCalibrationParamInfos[i].CalibName4 == c.CalibName)
                            {
                                calibResultInfo = new CalibrationResultinfo();
                                calibResultInfo.SampleNum = calibratorinfoTask[0].SampleNum;
                                calibResultInfo.CalibratorName = c.CalibName;
                                calibResultInfo.Pos = c.Pos;
                                calibResultInfo.CalibrationDT = calibratorinfoTask[0].CreateDate;
                                calibResultInfo.CalibConcentration = c.CalibConcentration;
                                //保存校准任务和结果方法
                                CreateTaskInfo(assyCalibInfo, calibResultInfo);
                                calibResultInfo = null;
                            }
                            else if (lstCalibrationParamInfos[i].CalibName5 == c.CalibName)
                            {
                                calibResultInfo = new CalibrationResultinfo();
                                calibResultInfo.SampleNum = calibratorinfoTask[0].SampleNum;
                                calibResultInfo.CalibratorName = c.CalibName;
                                calibResultInfo.Pos = c.Pos;
                                calibResultInfo.CalibrationDT = calibratorinfoTask[0].CreateDate;
                                calibResultInfo.CalibConcentration = c.CalibConcentration;
                                //保存校准任务和结果方法
                                CreateTaskInfo(assyCalibInfo, calibResultInfo);
                                calibResultInfo = null;
                            }
                            else if (lstCalibrationParamInfos[i].CalibName6 == c.CalibName)
                            {
                                calibResultInfo = new CalibrationResultinfo();
                                calibResultInfo.SampleNum = calibratorinfoTask[0].SampleNum;
                                calibResultInfo.CalibratorName = c.CalibName;
                                calibResultInfo.Pos = c.Pos;
                                calibResultInfo.CalibrationDT = calibratorinfoTask[0].CreateDate;
                                calibResultInfo.CalibConcentration = c.CalibConcentration;
                                //保存校准任务和结果方法
                                CreateTaskInfo(assyCalibInfo, calibResultInfo);
                                calibResultInfo = null;
                            }
                        }


                        //添加到校准拟合表
                        if (lstCalibrationParamInfos[i].CalibrationMethod != "K系数法")
                        {
                            Hashtable ht2 = new Hashtable();
                            ht2.Add("ProjectName", lstCalibrationParamInfos[i].ProjectName);
                            ht2.Add("CalibMethod", lstCalibrationParamInfos[i].CalibrationMethod);
                            ht2.Add("SampleType", lstCalibrationParamInfos[i].SampleType);
                            ht2.Add("BlkConc", lstCalibrationParamInfos[i].CalibConcentration0);
                            ht2.Add("SDT1Conc", lstCalibrationParamInfos[i].CalibConcentration1);
                            ht2.Add("SDT2Conc", lstCalibrationParamInfos[i].CalibConcentration2);
                            ht2.Add("SDT3Conc", lstCalibrationParamInfos[i].CalibConcentration3);
                            ht2.Add("SDT4Conc", lstCalibrationParamInfos[i].CalibConcentration4);
                            ht2.Add("SDT5Conc", lstCalibrationParamInfos[i].CalibConcentration5);
                            ht2.Add("SDT6Conc", lstCalibrationParamInfos[i].CalibConcentration6);
                            ht2.Add("BlkItem", lstCalibrationParamInfos[i].CalibName0);
                            ht2.Add("Calib1Item", lstCalibrationParamInfos[i].CalibName1);
                            ht2.Add("Calib2Item", lstCalibrationParamInfos[i].CalibName2);
                            ht2.Add("Calib3Item", lstCalibrationParamInfos[i].CalibName3);
                            ht2.Add("Calib4Item", lstCalibrationParamInfos[i].CalibName4);
                            ht2.Add("Calib5Item", lstCalibrationParamInfos[i].CalibName5);
                            ht2.Add("Calib6Item", lstCalibrationParamInfos[i].CalibName6);
                            ht2.Add("DrawDate", calibratorinfoTask[0].CreateDate);
                            ht2.Add("IsUsed", false);
                            ht2.Add("CalibState", CalibRemarks.NEW);
                            ism_SqlMap.Insert("Calibrator.AddSDTTableItem", ht2);
                        }
                    }

                }
                catch (Exception e)
                {
                    LogInfo.WriteErrorLog("AddCalibratorinfoTask" + e.ToString(), Module.QualityControl);

                }
            }
            //获取校准任务信息
            List<CalibratorinfoTask> listcalibratorinfoTask = new List<CalibratorinfoTask>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("CreateDate", DateTime.Now.Date);
                ht.Add("SystemTime", DateTime.Now.Date.AddDays(1));
                listcalibratorinfoTask = (List<CalibratorinfoTask>)ism_SqlMap.QueryForList<CalibratorinfoTask>("Calibrator." + strDBMethod, ht);


            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryListCalibrationCurveInfo(string strDBMethod, List<CalibratorinfoTask> calibratorinfoTask)==" + e.ToString(), Module.QualityControl);
            }

            return listcalibratorinfoTask;
        }
        /// <summary>
        /// 保存校准任务
        /// </summary>
        /// <param name="assyCalibInfo">不变的校准参数</param>
        /// <param name="calibResultInfo">有变化的校准参数</param>
        public void CreateTaskInfo(AssayProjectCalibrationParamInfo assyCalibInfo, CalibrationResultinfo calibResultInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                //插入校准任务中
                ht.Add("SampleNum", calibResultInfo.SampleNum);
                ht.Add("CalibName", calibResultInfo.CalibratorName);
                ht.Add("ProjectName", assyCalibInfo.ProjectName);
                ht.Add("SampleType", assyCalibInfo.SampleType);
                ht.Add("Pos", calibResultInfo.Pos);
                ht.Add("InspectTimes", assyCalibInfo.CalibrationTimes);
                ht.Add("CreateDate", calibResultInfo.CalibrationDT);
                ism_SqlMap.Insert("Calibrator.AddCalibratorinfoTask", ht);
                //Hashtable ht1 = new Hashtable();
                ////插入校准结果表
                //ht1.Add("SampleNum", calibResultInfo.SampleNum);
                //ht1.Add("ProjectName", assyCalibInfo.ProjectName);
                //ht1.Add("SampleType", assyCalibInfo.SampleType);    
                //ht1.Add("CalibMethod", assyCalibInfo.CalibrationMethod);
                //ht1.Add("CalibratorName", calibResultInfo.CalibratorName);
                //ht1.Add("CalibrationDT", calibResultInfo.CalibrationDT);
                //ht1.Add("CalibConcentration", calibResultInfo.CalibConcentration);
                //ism_SqlMap.Insert("Calibrator.AddCalibrationResultInfo", ht1);


            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("CreateTaskInfo(AssayProjectCalibrationParamInfo assyCalibInfo, CalibrationResultinfo calibResultInfo)==" + e.ToString(), Module.QualityControl);
            }
        }

        public List<CalibratorProjectinfo> QueryCalibProjectInfo(string strDBMethod, CalibratorProjectinfo lstCalibProinfo)
        {
            List<CalibratorProjectinfo> lstResults = new List<CalibratorProjectinfo>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", lstCalibProinfo.ProjectName);
                ht.Add("SampleType", lstCalibProinfo.SampleType);
                lstResults = (List<CalibratorProjectinfo>)ism_SqlMap.QueryForList<CalibratorProjectinfo>("Calibrator." + strDBMethod, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibProjectInfo(string strDBMethod, string lstCalibProinfo, string strSampleType)==" + e.ToString(), Module.QualityControl);
            }
            return lstResults;
        }

        /// <summary>
        /// 校准任务：
        ///     获取所有校准任务信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public List<CalibratorinfoTask> QueryAssayProNameAll(string strDBMethod, string p2, string systime)
        {
            List<CalibratorinfoTask> listcalibratorinfoTask = new List<CalibratorinfoTask>();
            try
            {
                Hashtable ht = new Hashtable();

                ht.Add("CreateDate", p2);
                ht.Add("SystemTime", systime);
                listcalibratorinfoTask = (List<CalibratorinfoTask>)ism_SqlMap.QueryForList<CalibratorinfoTask>("Calibrator." + strDBMethod, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(strDBMethod + e.ToString(), Module.QualityControl);
            }


            return listcalibratorinfoTask;
        }

        /// <summary>
        ///     校准状态：
        ///         校准曲线（保存最符合要求的一条校准曲线）
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="dataConfig"></param>
        /// <returns></returns>
        public string AddSDTTableItem(string strDBMethod, SDTTableItem dataConfig)
        {
            string strResult = "校准曲线保存成功！";
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", dataConfig.ProjectName);
                ht.Add("SampleType", dataConfig.SampleType);
                ht.Add("CalibMethod", dataConfig.CalibMethod);
                ht.Add("DrawDate", dataConfig.DrawDate);
                ht.Add("IsUsed", dataConfig.IsUsed);
                int updateResult = ism_SqlMap.Update("Calibrator.SaveSDTTableItem", ht);
                if (updateResult > 0)
                {
                    ism_SqlMap.Update("Calibrator.BeforeUpdateSDTTableItemIsUsedState", ht);
                }
                else
                    strResult = "校准曲线保存失败！";
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddSDTTableItem(string strDBMethod, SDTTableItem dataConfig)==" + e.ToString(), Module.QualityControl);
                return strResult = "校准曲线保存异常！";
            }
            return strResult;

            //try
            //{
            //    //Hashtable ht = new Hashtable();
            //    //ht.Add("ProjectName", dataConfig.ProjectName);
            //    //ht.Add("CalibMethod", dataConfig.CalibMethod);
            //    //ht.Add("BlkAbs", dataConfig.BlkAbs);
            //    //ht.Add("BlkConc", dataConfig.BlkConc);
            //    //ht.Add("SDT1Abs", dataConfig.SDT1Abs);
            //    //ht.Add("SDT1Conc", dataConfig.SDT1Conc);
            //    //ht.Add("SDT2Abs", dataConfig.SDT2Abs);
            //    //ht.Add("SDT2Conc", dataConfig.SDT2Conc);
            //    //ht.Add("SDT3Abs", dataConfig.SDT3Abs);
            //    //ht.Add("SDT3Conc", dataConfig.SDT3Conc);
            //    //ht.Add("SDT4Abs", dataConfig.SDT4Abs);
            //    //ht.Add("SDT4Conc", dataConfig.SDT4Conc);
            //    //ht.Add("SDT5Abs", dataConfig.SDT5Abs);
            //    //ht.Add("SDT5Conc", dataConfig.SDT5Conc);
            //    //ht.Add("SDT6Abs", dataConfig.SDT6Abs);
            //    //ht.Add("SDT6Conc", dataConfig.SDT6Conc);
            //    //ht.Add("SampleType", dataConfig.SampleType);
            //    //ism_SqlMap.Insert("Calibrator." + strDBMethod, ht);
            //    if (dataConfig.CalibMethod == "K系数法")
            //    {
            //        Hashtable ht2 = new Hashtable();
            //        ht2.Add("ProjectName", dataConfig.ProjectName);
            //        ht2.Add("CalibMethod", dataConfig.CalibMethod);
            //        ht2.Add("SampleType", dataConfig.SampleType);
            //        ht2.Add("BlkAbs", dataConfig.BlkAbs);
            //        ht2.Add("BlkConc", dataConfig.BlkConc);
            //        ht2.Add("CalibDate", dataConfig.CalibDate);
            //        ism_SqlMap.Insert("Calibrator." + strDBMethod, ht2);
            //    }
            //    else
            //    {
            //        Hashtable ht = new Hashtable();
            //        ht.Add("ProjectName", dataConfig.ProjectName);
            //        ht.Add("CalibMethod", dataConfig.CalibMethod);
            //        ht.Add("BlkAbs", dataConfig.BlkAbs);
            //        ht.Add("BlkConc", dataConfig.BlkConc);
            //        ht.Add("SDT1Abs", dataConfig.SDT1Abs);
            //        ht.Add("SDT1Conc", dataConfig.SDT1Conc);
            //        ht.Add("SDT2Abs", dataConfig.SDT2Abs);
            //        ht.Add("SDT2Conc", dataConfig.SDT2Conc);
            //        ht.Add("SDT3Abs", dataConfig.SDT3Abs);
            //        ht.Add("SDT3Conc", dataConfig.SDT3Conc);
            //        ht.Add("SDT4Abs", dataConfig.SDT4Abs);
            //        ht.Add("SDT4Conc", dataConfig.SDT4Conc);
            //        ht.Add("SDT5Abs", dataConfig.SDT5Abs);
            //        ht.Add("SDT5Conc", dataConfig.SDT5Conc);
            //        ht.Add("SDT6Abs", dataConfig.SDT6Abs);
            //        ht.Add("SDT6Conc", dataConfig.SDT6Conc);
            //        ht.Add("SampleType", dataConfig.SampleType);
            //        ht.Add("CalibDate", dataConfig.CalibDate);

            //        ism_SqlMap.Insert("Calibrator." + strDBMethod, ht);
            //    }

            //}
            //catch (Exception e)
            //{
            //    LogInfo.WriteErrorLog("AddQCTask(string strDBMethod, List<QCTaskInfo> lstQCTaskInfos)==" + e.ToString(), Module.QualityControl);
            //    return strResult = "校准曲线保存失败！";
            //}


        }

        /// <summary>
        /// 校准状态：
        ///     根据条件获取校准结果信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="calibrationResultinfo"></param>
        /// <returns></returns>
        public List<CalibrationResultinfo> QueryCalibrationResultinfo(string strDBMethod, CalibrationResultinfo calibrationResultinfo)
        {
            List<CalibrationResultinfo> lstCalibrationResultinfo = new List<CalibrationResultinfo>();
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", calibrationResultinfo.ProjectName);
                ht.Add("CalibMethod", calibrationResultinfo.CalibMethod);
                ht.Add("SampleType", calibrationResultinfo.SampleType);
                lstCalibrationResultinfo = (List<CalibrationResultinfo>)ism_SqlMap.QueryForList<CalibrationResultinfo>("Calibrator." + strDBMethod, ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibrationResultinfo;
        }

        /// <summary>
        /// 校准状态：
        ///     获取校准进程信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="calibrationReactionProcess"></param>
        /// <returns></returns>
        public TimeCourseInfo QueryCalibrationReactionProcess(string strDBMethod, int tcno, string calibrationDT)
        {
            TimeCourseInfo timeCourseInfoResult = null;
            try
            {
                string str = string.Format("select * from timecoursetb where TimeCourseNO = '{0}' and CONVERT(varchar(50),DrawDate, 120) like '%{1}%'", tcno, calibrationDT);
                timeCourseInfoResult = (TimeCourseInfo)ism_SqlMap.QueryForObject("PLCDataInfo." + strDBMethod,
                    str);
                if (timeCourseInfoResult != null)
                {
                    return timeCourseInfoResult;
                }
                timeCourseInfoResult = (TimeCourseInfo)ism_SqlMap.QueryForObject("PLCDataInfo.QueryTimeCourseBackUpInfo",
                    string.Format("select * from timecourseBackUptb where TimeCourseNO = '{0}' and CONVERT(varchar(50),DrawDate, 120) like '%{1}%'", tcno, calibrationDT));
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibratorinfo(string strDBMethod)==" + e.ToString(), Module.QualityControl);
            }

            return timeCourseInfoResult;
        }

        /// <summary>
        /// 获取校准结果信息和比色杯编号
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="calibrationReactionProcess"></param>
        /// <returns></returns>
        public List<CalibrationResultinfo> QueryCalibrationResultInfoAndTimeCUVNO(string strDBMethod, CalibrationResultinfo calibrationResultinfoAndTimeCUVNO)
        {
            List<CalibrationResultinfo> lstCalibrationResultinfoAndTimeCUVNO = new List<CalibrationResultinfo>();
            try
            {
                if (calibrationResultinfoAndTimeCUVNO.CalibMethod != "K系数法")
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ProjectName", calibrationResultinfoAndTimeCUVNO.ProjectName);
                    ht.Add("SampleType", calibrationResultinfoAndTimeCUVNO.SampleType);
                    ht.Add("CalibMethod", calibrationResultinfoAndTimeCUVNO.CalibMethod);
                    lstCalibrationResultinfoAndTimeCUVNO = (List<CalibrationResultinfo>)ism_SqlMap.QueryForList<CalibrationResultinfo>("Calibrator." + strDBMethod, ht);
                }
                else
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("ProjectName", calibrationResultinfoAndTimeCUVNO.ProjectName);
                    ht.Add("SampleType", calibrationResultinfoAndTimeCUVNO.SampleType);
                    ht.Add("CalibMethod", calibrationResultinfoAndTimeCUVNO.CalibMethod);
                    lstCalibrationResultinfoAndTimeCUVNO = (List<CalibrationResultinfo>)ism_SqlMap.QueryForList<CalibrationResultinfo>("Calibrator.QueryKMethodCalibResultInfoOrTimeCUVNO", ht);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("QueryCalibrationResultInfoAndTimeCUVNO(string strDBMethod, CalibrationResultinfo calibrationResultinfoAndTimeCUVNO)==" + e.ToString(), Module.QualityControl);
            }

            return lstCalibrationResultinfoAndTimeCUVNO;
        }


        //public QualityControlResultInfo GetQCResult(RealTimeCUVDataInfo realTimeData)
        //{
        //    QualityControlResultInfo result = new QualityControlResultInfo();
        //    try
        //    {
        //        Hashtable ht = new Hashtable();
        //        ht.Add("TCNO", realTimeData.TC);
        //        ht.Add("StartTime", DateTime.Now.ToShortDateString());
        //        ht.Add("EndTime", DateTime.Now.AddDays(1).ToShortDateString());
        //        result = ism_SqlMap.QueryForObject("QCResultInfo.GetQCResult", ht) as QualityControlResultInfo;
        //    }
        //    catch (Exception e)
        //    {
        //        LogInfo.WriteErrorLog("GetQCResult(RealTimeCUVDataInfo realTimeData)==" + e.ToString(), Module.QualityControl);
        //    }

        //    return result;
        //}
        /// <summary>
        /// 修改质控结果表吸光度和浓度
        /// </summary>
        /// <param name="qcResInfo"></param>
        public void UpdateQCResult(QualityControlResultInfo qcResInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("AbsValue", qcResInfo.AbsValue);
                ht.Add("ConcResult", qcResInfo.ConcResult);
                ht.Add("SampleNum", qcResInfo.SampleNum);
                ht.Add("ProjectName", qcResInfo.ProjectName);
                ht.Add("SampleType", qcResInfo.SampleType);
                ht.Add("TCNO", qcResInfo.TCNO);
                ht.Add("SampleCreateTime", qcResInfo.SampleCreateTime);

                ism_SqlMap.Update("QCResultInfo.UpdateQCResult", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQCResult(QualityControlResultInfo qcResInfo)==" + e.ToString(), Module.QualityControl);
            }
        }

        public void UpdateQCTaskState(string ProjectName, string sampleType)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("ProjectName", ProjectName);
                ht.Add("SampleType", sampleType);
                ism_SqlMap.Update("QCTaskInfo.UpdateQCTaskState", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQCTaskState(string ProjectName, string sampleType)==" + e.ToString(), Module.QualityControl);
            }
        }

        public void UpdateQCResultRunLog(QualityControlResultInfo QCResInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("SampleNum", QCResInfo.SampleNum);
                ht.Add("ProjectName", QCResInfo.ProjectName);
                ht.Add("TCNO", QCResInfo.TCNO);
                ht.Add("SampleCreateTime", QCResInfo.SampleCreateTime);
                ht.Add("Remarks", QCResInfo.Remarks);
                ism_SqlMap.Update("QCResultInfo.UpdateQCResultRunLog", ht);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("UpdateQCResultRunLog(QualityControlResultInfo QCResInfo)==" + e.ToString(), Module.QualityControl);
            }
        }
        /// <summary>
        /// 根据项目名称判断是否已下校准任务
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <returns></returns>
        public int IsExsitCalibrationTask(string ProjectName)
        {
            return (int)ism_SqlMap.QueryForObject("Calibrator.GetCalibTaskByCondition", ProjectName);
        }
    }
}
