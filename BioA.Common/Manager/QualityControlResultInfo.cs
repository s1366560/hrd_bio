using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class QualityControlResultInfo : ResultInfo
    {
        public QualityControlResultInfo()
        {
            qCID = 0;
            sampleNum = string.Empty;
            isLogicalDelete = false;
            isLogicalEdit = false;
        }

        private int qCID;
        private string sampleNum;
        private bool isLogicalDelete;
        private bool isLogicalEdit;

        /// <summary>
        /// 质控品ID
        /// </summary>
        public int QCID
        {
            get { return qCID; }
            set { qCID = value; }
        }
        /// <summary>
        /// 样本号
        /// </summary>
        public string SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }

        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsLogicalDelete
        {
            get { return isLogicalDelete; }
            set { isLogicalDelete = value; }
        }
        /// <summary>
        /// 是否逻辑编辑
        /// </summary>
        public bool IsLogicalEdit
        {
            get { return isLogicalEdit; }
            set { isLogicalEdit = value; }
        }
    }
}
