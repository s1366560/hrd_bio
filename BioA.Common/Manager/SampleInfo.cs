using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class SampleInfo
    {
        public SampleInfo()
        {
            iD = 0;
            sampleNum = 0;
            createTime = DateTime.Now;
            sampleState = 0;
            samContainer = string.Empty;
            sampleType = string.Empty;
            barcode = string.Empty;
            panelNum = 0;
            samplePos = 0;
            isOperateDilution = false;
            isEmergency = false;
            isPrinted = false;
            isSend = false;
            isAudit = false;
            printState = string.Empty;
        }
        /// <summary>
        /// 数据库ID
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        /// <summary>
        /// 样本编号
        /// </summary>
        public int SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }
        /// <summary>
        /// 样本创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
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
        /// 样本容器
        /// </summary>
        public string SamContainer
        {
            get { return samContainer; }
            set { samContainer = value; }
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
        /// 条码
        /// </summary>
        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }
        /// <summary>
        /// 样本盘号
        /// </summary>
        public int PanelNum
        {
            get { return panelNum; }
            set { panelNum = value; }
        }
        /// <summary>
        /// 样本位置
        /// </summary>
        public int SamplePos
        {
            get { return samplePos; }
            set { samplePos = value; }
        }
        /// <summary>
        /// 是否手动稀释
        /// </summary>
        public bool IsOperateDilution
        {
            get { return isOperateDilution; }
            set { isOperateDilution = value; }
        }
        /// <summary>
        /// 是否为急诊
        /// </summary>
        public bool IsEmergency
        {
            get { return isEmergency; }
            set { isEmergency = value; }
        }
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrinted
        {
            get { return isPrinted; }
            set { isPrinted = value; }
        }
        /// <summary>
        /// 是否发送LIS
        /// </summary>
        public bool IsSend
        {
            get { return isSend; }
            set { isSend = value; }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public bool IsAudit
        {
            get {return isAudit;}
            set {isAudit = value;}
        }
        /// <summary>
        /// 打印状态
        /// </summary>
        public string PrintState
        {
            get { return printState; }
            set { printState = value; }
        }

        private int iD;
        private int sampleNum;
        private DateTime createTime;
        private int sampleState;
        private string samContainer;
        private string sampleType;
        private string barcode;
        private int panelNum;
        private int samplePos;
        private bool isOperateDilution;
        private bool isEmergency;
        private bool isPrinted;
        private bool isSend;
        private bool isAudit;
        private string printState;
    }                  
}