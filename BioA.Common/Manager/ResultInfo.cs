using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class ResultInfo
    {
        public ResultInfo()
        {
            absValue = 0;
            concResult = 0;
            //sampleNum = 0;
            projectName = string.Empty;
            sampleType = string.Empty;
            tCNO = 0;
            remarks = string.Empty;
            sampleCreateTime = DateTime.Now;
        }

        //private int sampleNum;
        private string projectName;
        private string sampleType;
        private int tCNO;
        private string remarks;
        private float absValue;
        private float concResult;
        private DateTime sampleCreateTime;
        private DateTime sampleCompletionTime;
        /// <summary>
        /// 样本号
        /// </summary>
        //public int SampleNum
        //{
        //    get { return sampleNum; }
        //    set { sampleNum = value; }
        //}
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }

        /// <summary>
        /// 进程编号
        /// </summary>
        public int TCNO
        {
            get { return tCNO; }
            set { tCNO = value; }
        }
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        /// <summary>
        /// 样本创建时间
        /// </summary>
        public DateTime SampleCreateTime
        {
            get { return sampleCreateTime; }
            set { sampleCreateTime = value; }
        }
        /// <summary>
        /// 样本完成时间
        /// </summary>
        public DateTime SampleCompletionTime
        {
            get { return sampleCompletionTime; }
            set { sampleCompletionTime = value; }
        }
        /// <summary>
        /// 吸光度
        /// </summary>
        public float AbsValue
        {
            get { return absValue; }
            set { absValue = value; }
        }
        /// <summary>
        /// 浓度结果
        /// </summary>
        public float ConcResult
        {
            get { return concResult; }
            set { concResult = value; }
        }
    }
}
