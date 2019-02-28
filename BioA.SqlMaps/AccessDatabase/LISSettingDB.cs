using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.SqlMaps
{
    public partial class MyBatis
    {
        /// <summary>
        /// 获取LIS常规设置信息
        /// </summary>
        /// <returns></returns>
        public object QueryLISSettingInfo()
        {
            object[] LISSetting = new object[3];
            try
            {
                LISSetting[0] = ism_SqlMap.QueryForObject("LISCommunicateInfo.QueryLISSettingInfo", string.Format("select * from LISSettingtb"));
                LISSetting[1] = ism_SqlMap.QueryForObject("LISCommunicateInfo.QueryLISSerialCommunicationInfo", string.Format("select * from serialcommunicationtb"));
                LISSetting[2] = ism_SqlMap.QueryForObject("LISCommunicateInfo.QueryLISCommunicateNetworkInfo", string.Format("select * from networkcommunicationtb"));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("object QueryLISSettingInfo() == " + ex.ToString(), Module.LISSetting);
            }
            return LISSetting;
        }
        /// <summary>
        /// 更新LIS常规设置信息和网络设置信息或者串口设置信息
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateLISSetingAndNetworkORSerialInfo(object[] obj)
        {
            LISSettingInfo lisSet = obj[0] as LISSettingInfo;
            SerialCommunicationInfo serial = obj[1] as SerialCommunicationInfo;
            LISCommunicateNetworkInfo network = obj[2] as LISCommunicateNetworkInfo;
            try
            {
                //修改LIS常规设置信息
                ism_SqlMap.Update("LISCommunicateInfo.updateLISInfo", string.Format("update LISSettingtb set CommunicationMode = '{0}',CommunicationDirection = '{1}',CommunicationOverTime = {2},RealTiimeSampleResults = '{3}'", lisSet.CommunicationMode, lisSet.CommunicationDirection, lisSet.CommunicationOverTime, lisSet.RealTiimeSampleResults));
                if(serial != null)
                    //修改串口设置信息
                    ism_SqlMap.Update("LISCommunicateInfo.updateLISInfo", string.Format("update SerialCommunicationTb set SerialName = '{0}',BaudRate = {1},DataBits = {2},StopBits = '{3}',Parity = '{4}'", serial.SerialName, serial.BaudRate, serial.DataBits, serial.StopBits, serial.Parity));
                else
                    //修改网络设置信息
                    ism_SqlMap.Update("LISCommunicateInfo.updateLISInfo", string.Format("update NetworkCommunicationTb set IPAddress = '{0}',NetworkPort = '{1}'", network.IPAddress, network.NetworkPort));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("void UpdateLISSetingAndNetworkORSerialInfo(object[] obj) ==" + ex.ToString(), Module.LISSetting);
            }
        }
        /// <summary>
        /// 根据项目名和类型获取结果设置信息
        /// </summary>
        /// <returns></returns>
        public ResultSetInfo GetResultSetInfo(string projectName, string type)
        {
            ResultSetInfo ResultSetInfo = null;
            try
            {
                ism_SqlMap.QueryForObject("AssayProjectInfo.QueryResultSetInfo", string.Format("select * from resultSetTb where ProjectName = '{0}' and SampleType = '{1}'", projectName, type));
            }
            catch (Exception ex)
            {

            }
            return ResultSetInfo;
        }
        /// <summary>
        /// 获取实时发送的样本结果数据
        /// </summary>
        /// <returns></returns>
        public SampleResultInfo GetSampleResultInfo()
        {
            SampleResultInfo SampleResultInfo = null;
            try
            {
                SampleResultInfo = ism_SqlMap.QueryForObject("CommonDataCheck.GetActualTimeSampResult", string.Format("select top(1) * from sampleresulttb where IsSend = 'false' and SampleCompletionStatus = 2 order by SampleCreateTime")) as SampleResultInfo;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetSampleResultInfo() == " + ex.ToString(), Module.LISSetting);
            }
            return SampleResultInfo;
        }
        /// <summary>
        /// 根据样本编号获取当天时间的患者信息
        /// </summary>
        /// <param name="sampleNum"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public PatientInfo AccordingSampNumGetPatientInfo(int sampleNum, DateTime dt)
        {
            PatientInfo PatientInfo = null;
            try
            {
                PatientInfo = ism_SqlMap.QueryForObject("WorkAreaApplyTask.AccordingSampNumGetPatientInfo", string.Format("select * from patientinfotb where SampleNum={0} and DATEDIFF(DD,InputTime,'{1}') = 0", sampleNum, dt)) as PatientInfo;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("AccordingSampNumGetPatientInfo(string sampleNum, DateTime dt) ==" + ex.ToString(), Module.LISSetting);
            }
            return PatientInfo;
        }
        /// <summary>
        /// 根据样本条码获取样本信息
        /// </summary>
        /// <param name="sampleBarCode"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public SampleInfo GetSampleByBarcode(string sampleBarCode, DateTime dt)
        {
            SampleInfo SampleInfo = null;
            try
            {
                SampleInfo = ism_SqlMap.QueryForObject("WorkAreaApplyTask.QuerySampleInfo", string.Format("select * from sampletb where Barcode ='{0}' and DateDiff(dd,CreateTime,'{1}')=0", sampleBarCode, dt)) as SampleInfo;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetSampleByBarcode(string sampleBarCode, DateTime dt) == " + ex.ToString(),Module.LISSetting);
            }
            return SampleInfo;
        }
        /// <summary>
        /// 获取机最新的样本编号
        /// </summary>
        /// <returns></returns>
        public int GetLastestSMPNO()
        {
            int SampNum = 0;
            try
            {
                SampNum = (int)ism_SqlMap.QueryForObject("WorkAreaApplyTask.GetLastestSMPNO", string.Format("select LastestSMPNO from runningstatetb"));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetLastestSMPNO() == " + ex.ToString(), Module.LISSetting);
            }
            return SampNum;
        }
        /// <summary>
        /// 更新运行机器状态表中的最新样本编号
        /// </summary>
        /// <param name="sampNum"></param>
        public void UpdateLastestSMPNum(int sampNum)
        {
            try
            {
                ism_SqlMap.Update("WorkAreaApplyTask.NoReturnValueGeneralID", string.Format("update runningstatetb set LastestSMPNO = {0}",sampNum));   
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("UpdateLastestSMPNum(int sampNum) ==" + ex.ToString(), Module.LISSetting);
            }
        }
        /// <summary>
        /// 根据项目名和样本编号获取任务数据
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="sampNumber"></param>
        /// <returns></returns>
        public List<TaskInfo> GetTaskByProjectNameAndSampNum(string projectName, int sampNumber)
        {
            List<TaskInfo> taskList = null;
            try
            {
                taskList = ism_SqlMap.QueryForList<TaskInfo>("WorkAreaApplyTask.GetTaskInfo", string.Format("select * from tasktb where ProjectName = '{0}' and SampleNum = {1} ", projectName, sampNumber)) as List<TaskInfo>;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetTaskByProjectNameAndSampNum(string projectName, int sampNumber) == " + ex.Message, Module.LISSetting);
            }
            return taskList;
        }
        /// <summary>
        /// LIS新增一条任务数据
        /// </summary>
        /// <param name="taskInfo"></param>
        public void InsertTaskInfo(TaskInfo taskInfo)
        {
            try
            {
                ism_SqlMap.Insert("WorkAreaApplyTask.AddTask", taskInfo);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("InsertTaskInfo(TaskInfo taskInfo) == " + ex.Message, Module.LISSetting);
            }
        }
        /// <summary>
        /// 获取生化项目参数信息
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="samplType"></param>
        /// <returns></returns>
        public AssayProjectParamInfo GetAssayProjectParamInfo(string projectName, string  samplType)
        {
            AssayProjectParamInfo assayProjectParamInfo = null;
            try
            {
                assayProjectParamInfo = ism_SqlMap.QueryForObject("AssayProjectInfo.GetAssayProjectParamInfoByNameAndType", new AssayProjectParamInfo() { ProjectName = projectName, SampleType = samplType }) as AssayProjectParamInfo;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("GetAssayProjectParamInfo(string projectName, string  samplType) == " + ex.Message , Module.LISSetting);
            }
            return assayProjectParamInfo;
        }
        /// <summary>
        /// 根据样本编号删除当天样本信息
        /// </summary>
        /// <param name="sampleNumber"></param>
        public void DeleteSampleInfo(int sampleNumber)
        {
            try
            {
                ism_SqlMap.Delete("WorkAreaApplyTask.DeleteTaskAndSampleInfo", string.Format("delete from sampletb where SampleNum = {0} and DATEDIFF(DD, CreateTime,'{1}') = 0",sampleNumber, DateTime.Now.Date));
            }
            catch(Exception ex)
            {
                LogInfo.WriteErrorLog("DeleteSampleInfo(int sampleNumber) ==" + ex.Message, Module.LISSetting);
            }
        }
        /// <summary>
        /// 根据样本编号删除当天患者信息
        /// </summary>
        /// <param name="sampleNumber"></param>
        public void DeletePatientInfo(int sampleNumber)
        {
            try
            {
                ism_SqlMap.Delete("WorkAreaApplyTask.DeleteTaskAndSampleInfo", string.Format("delete from patientinfotb where SampleNum = {0} and DATEDIFF(DD, InputTime,'{1}') = 0", sampleNumber, DateTime.Now.Date));
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("DeletePatientInfo(int sampleNumber) == " + ex.Message, Module.LISSetting);
            }
        }
        /// <summary>
        /// 保存样本信息
        /// </summary>
        /// <param name="sample"></param>
        public void SaveSampleInfo(SampleInfo sample)
        {
            try
            {
                ism_SqlMap.QueryForObject("WorkAreaApplyTask.AddSample", sample);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("SaveSampleInfo(SampleInfo sample) ==" + ex.Message, Module.LISSetting);
            }
        }
        /// <summary>
        /// 保存患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SavePatientInfo(PatientInfo patientInfo)
        {
            try
            {
                ism_SqlMap.Insert("WorkAreaApplyTask.AddPatientInfo", patientInfo);
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("SavePatientInfo(PatientInfo patientInfo) ==" + ex.Message, Module.LISSetting);
            }
        }
    }
}
