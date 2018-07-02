using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class QCResultForUIInfo : QualityControlResultInfo
    {
        public QCResultForUIInfo()
        {
            qCName = string.Empty;
            lotNum = string.Empty;
            sampleType = string.Empty;
            pos = string.Empty;
            targetMean = 0;
            horizonLevel = string.Empty;
            targetSD = 0;
            manufacturer = string.Empty;
            qCTimeStartTS = DateTime.Now.Date;
            qCTimeEndTS = DateTime.Now.Date;
        }

        private string qCName;
        private string lotNum;
        private string sampleType;
        private string pos;
        private float targetMean;
        private string horizonLevel;
        private float targetSD;
        private string manufacturer;
        private DateTime qCTimeStartTS;
        private DateTime qCTimeEndTS;
        /// <summary>
        /// 质控品名称
        /// </summary>
        public string QCName
        {
            get { return qCName; }
            set { qCName = value; }
        }
        /// <summary>
        /// 批号
        /// </summary>
        public string LotNum
        {
            get { return lotNum; }
            set { lotNum = value; }
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
        /// 位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        /// <summary>
        /// 目标平均值
        /// </summary>
        public float TargetMean
        {
            get { return targetMean; }
            set { targetMean = value; }
        }
        /// <summary>
        /// 水平浓度
        /// </summary>
        public string HorizonLevel
        {
            get { return horizonLevel; }
            set { horizonLevel = value; }
        }
        /// <summary>
        /// 目标标准差
        /// </summary>
        public float TargetSD
        {
            get { return targetSD; }
            set { targetSD = value; }
        }
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        /// <summary>
        /// 质控结果查询起始时间
        /// </summary>
        public DateTime QCTimeStartTS
        {
            get { return qCTimeStartTS; }
            set { qCTimeStartTS = value; }
        }
        /// <summary>
        /// 质控结果查询结束时间
        /// </summary>
        public DateTime QCTimeEndTS
        {
            get { return qCTimeEndTS; }
            set { qCTimeEndTS = value; }
        }
    }
}
