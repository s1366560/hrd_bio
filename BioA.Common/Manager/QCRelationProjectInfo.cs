using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class QCRelationProjectInfo
    {
        public QCRelationProjectInfo()
        {
            projectName = string.Empty;
            sampleType = string.Empty;
            qCID = -1;
            qCName = string.Empty;
            targetMean = 0;
            targetSD = 0;
            target2SD = 0;
            target3SD = 0;
            projectID = -1;
        }

        private string projectName;
        private string sampleType;
        private int qCID;
        private string qCName;
        private float targetMean;
        private float targetSD;
        private float target2SD;
        private float target3SD;
        private int projectID;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }
        /// <summary>
        /// 质控品标识
        /// </summary>
        public int QCID
        {
            get { return qCID; }
            set { qCID = value; }
        }
        /// <summary>
        /// 质控品名称
        /// </summary>
        public string QCName
        {
            get { return qCName; }
            set { qCName = value; }
        }
        /// <summary>
        /// 目标靶值
        /// </summary>
        public float TargetMean
        {
            get { return targetMean; }
            set { targetMean = value; }
        }
        /// <summary>
        /// 一倍标准差
        /// </summary>
        public float TargetSD
        {
            get { return targetSD; }
            set { targetSD = value; }
        }
        /// <summary>
        /// 二倍标准差
        /// </summary>
        public float Target2SD
        {
            get { return target2SD; }
            set { target2SD = value; }
        }
        /// <summary>
        /// 失控-high
        /// </summary>
        public float Target3SD
        {
            get { return target3SD; }
            set { target3SD = value; }
        }
        
        /// <summary>
        /// ID标识
        /// </summary>
        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
    }
}
