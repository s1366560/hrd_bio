using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class SampleInfoForResult:PatientInfo
    {
        public SampleInfoForResult()
        {
            //sampleNum = 0;
            //sampleID = string.Empty;
            sampleType = string.Empty;
            //patientName = string.Empty;
            //sex = string.Empty;
            //age = 0;
            isAudit = false;
            printState = string.Empty;
            isOperateDilution = false;
            startTime = DateTime.Now;
            endTime = DateTime.Now;
            sampleState = 0;
           
        }

        //private int sampleNum;
        //private string sampleID;
        private string sampleType;
        //private string patientName;
        //private string sex;
        //private int age;
        private DateTime createTime;
        private bool isAudit;
        private string printState;
        private bool isOperateDilution;
        private DateTime startTime;
        private DateTime endTime;
        private int sampleState;
        private int sampPos;
        /// <summary>
        /// 样本编号
        /// </summary>
        //public int SampleNum
        //{
        //    get { return sampleNum; }
        //    set { sampleNum = value; }
        //}
        ///// <summary>
        ///// 样本ID
        ///// </summary>
        //public string SampleID
        //{
        //    get { return sampleID; }
        //    set { sampleID = value; }
        //}
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }
        /// <summary>
        /// 病人信息
        /// </summary>
        //public string PatientName
        //{
        //    get { return patientName; }
        //    set { patientName = value; }
        //}
        ///// <summary>
        ///// 性别
        ///// </summary>
        //public string Sex
        //{
        //    get { return sex; }
        //    set { sex = value; }
        //}
        /// <summary>
        /// 年龄
        /// </summary>
        //public int Age
        //{
        //    get { return age; }
        //    set { age = value; }
        //}
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public bool IsAudit
        {
            get { return isAudit; }
            set { isAudit = value; }
        }
        /// <summary>
        /// 打印状态
        /// </summary>
        public string PrintState
        {
            get { return printState; }
            set { printState = value; }
        }
        /// <summary>
        /// 手动稀释
        /// </summary>
        public bool IsOperateDilution
        {
            get { return isOperateDilution; }
            set { isOperateDilution = value; }
        }
        /// <summary>
        /// 查询-起始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        /// <summary>
        /// 查询-结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        /// <summary>
        /// 样本状态
        /// </summary>
        public int SampleState
        {
            get { return sampleState; }
            set { sampleState = value; }
        }
        /// <summary>
        /// 样本位置
        /// </summary>
        public int SampPos
        {
            get { return sampPos; }
            set { sampPos = value; }
        }

        private List<SampleResultInfo> sampResultList;
        /// <summary>
        /// 一个样本对应多个结果
        /// </summary>
        public List<SampleResultInfo> SampResultList
        {
            get { return sampResultList; }
            set { sampResultList = value; }
        }
    }
}
