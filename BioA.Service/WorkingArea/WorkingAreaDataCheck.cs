using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;
using System.Collections;

namespace BioA.Service
{
    public class WorkingAreaDataCheck : DataTransmit
    {
        public List<SampleInfoForResult> QueryCommonSampleData(string strMethodName, SampleInfoForResult sampleInfo, string strFilter)
        {
            return myBatis.QueryCommonSampleData(strMethodName, sampleInfo, strFilter);
        }

        public List<SampleResultInfo> QueryProjectResultBySampleNum(string strMethodName, string[] strConditions)
        {
            List<SampleResultInfo> lstSampleResultInfo = new List<SampleResultInfo>();
            lstSampleResultInfo = myBatis.QueryProjectResultBySampleNum(strMethodName, strConditions);
            string projectName = null;
            string[] UnitAndRangeParameter = new string[2];
            foreach (SampleResultInfo sampleResInfo in lstSampleResultInfo)
            {
                if (projectName == null || projectName != sampleResInfo.ProjectName)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("SampleNum", strConditions[0]);
                    ht.Add("DateTime", strConditions[1]);
                    ht.Add("ProjectName", sampleResInfo.ProjectName);
                    ht.Add("SampleType", strConditions[2]);
                    UnitAndRangeParameter = myBatis.QueryUnitAndRange("QueryUnitAndRangeByProject", ht);
                }
                sampleResInfo.UnitAndRange = UnitAndRangeParameter[0];
                sampleResInfo.RangeParameter = UnitAndRangeParameter[1];
            }
            return lstSampleResultInfo;

        }
        /// <summary>
        /// 删除样本数据
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="strConditions"></param>
        /// <returns></returns>
        public string DeleteCommonSampleBySampleNum(string strMethodName, string[] strConditions)
        {
            return myBatis.DeleteCommonSampleBySampleNum(strMethodName, strConditions);
        }
        /// <summary>   
        /// 复查
        /// </summary>
        /// <param name="strMethodName"></param>
        /// <param name="strConditions"></param>
        /// <returns></returns>
        public string ReviewCheck(string strMethodName, string[] strConditions)
        {
            //return myBatis.ReviewCheck(strMethodName, strConditions);
            return null;
        }

        public string AuditSampleTest(string strMethodName, string[] strConditions)
        {
            return myBatis.AuditSampleTest(strMethodName, strConditions);
        }

        public TimeCourseInfo QueryCommonTaskReaction(string strMethodName, SampleResultInfo sampleResInfo)
        {
            return myBatis.QueryCommonTaskReaction(strMethodName, sampleResInfo);
        }

        public string BatchAuditSampleTest(string strMethodName, List<string[]> lstBatchAuditParam)
        {
            return myBatis.BatchAuditSampleTest(strMethodName, lstBatchAuditParam);
        }

        public string ConfirmCommonTask(string strMethodName, List<string[]> lstConfirmInfo)
        {
            return myBatis.ConfirmCommonTask(strMethodName, lstConfirmInfo);
        }
        /// <summary>
        /// 获取病人样本结果信息
        /// </summary>
        /// <param name="samp"></param>
        /// <param name="dateTime"></param>
        /// <param name="samplePatientInfo"></param>
        /// <returns></returns>
        public List<SampleResultInfo> GetSmpPrintValues(string samp, DateTime dateTime, out SampleInfoForResult samplePatientInfo)
        {
            return myBatis.GetSmpPrintValues(samp, dateTime, out samplePatientInfo);
        }
    }
}
