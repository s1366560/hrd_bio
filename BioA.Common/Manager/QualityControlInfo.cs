using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class QualityControlInfo
    {
        public QualityControlInfo()
        {
            qCName = string.Empty;
            lotNum = string .Empty;
            pos = string.Empty;
            invalidDate = DateTime.Now;
            horizonLevel = string.Empty;
            manufacturer = string.Empty;
            qCID = -1;
            isLocked = false;
            isLogicalDelete = false;
        }


        private string qCName;
        private string lotNum;
        private string pos;
        private DateTime invalidDate;
        private string horizonLevel;
        private string manufacturer;
        private int qCID;
        private bool isLocked;
        private bool isLogicalDelete;
        /// <summary>
        /// 质控名称
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
        /// 位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime InvalidDate
        {
            get { return invalidDate; }
            set { invalidDate = value; }
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
        /// 生产厂家
        /// </summary>
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        /// <summary>
        /// ID
        /// </summary>
        public int QCID
        {
            get { return qCID; }
            set { qCID = value; }
        }
        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        public bool IsLogicalDelete
        {
            get { return isLogicalDelete; }
            set { isLogicalDelete = value; }
        }
    }
}
