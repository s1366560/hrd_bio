using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 试剂设置表
    /// </summary>
    public class ReagentSettingsInfo
    {
        public ReagentSettingsInfo()
        {
            pos = string.Empty;
            projectName = string.Empty;
            reagentName = string.Empty;
            reagentType = string.Empty;
            reagentChamber = string.Empty;
            validDate = DateTime.Now;
            barcode = string.Empty;
            reagentContainer = string.Empty;
            batchNum = string.Empty;
            locked = false;
            //residualQuantity = 0;
            //measuredquantity = 0;

        }
       
        //int residualQuantity;
        ///// <summary>
        ///// 剩余可测数量
        ///// </summary>
        //public int ResidualQuantity
        //{
        //    get { return residualQuantity; }
        //    set { residualQuantity = value; }
        //}
       
        //int measuredquantity;
        ///// <summary>
        ///// 已测数量
        ///// </summary>
        //public int Measuredquantity
        //{
        //    get { return measuredquantity; }
        //    set { measuredquantity = value; }
        //}
        /// <summary>
        /// 试剂位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        /// <summary>
        /// 试剂名称
        /// </summary>
        public string ReagentName
        {
            get { return reagentName; }
            set { reagentName = value; }
        }
        /// <summary>
        /// 试剂类型 "血清、尿液、稀释液、清洗液"
        /// </summary>
        public string ReagentType
        {
            get { return reagentType; }
            set { reagentType = value; }
        }
        /// <summary>
        /// 试剂通道号
        /// </summary>
        public string ReagentChamber
        {
            get { return reagentChamber; }
            set { reagentChamber = value; }
        }
        /// <summary>
        /// 有效日期
        /// </summary>
        public DateTime ValidDate
        {
            get { return validDate; }
            set { validDate = value; }
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
        /// 试剂容器
        /// </summary>
        public string ReagentContainer
        {
            get { return reagentContainer; }
            set { reagentContainer = value; }
        }
        /// <summary>
        /// 批号
        /// </summary>
        public string BatchNum
        {
            get { return batchNum; }
            set { batchNum = value; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        private string pos;
        private string projectName;
        private string reagentName;
        private string reagentType;
        private string reagentChamber;
        private DateTime validDate;
        private string barcode;
        private string reagentContainer;
        private string batchNum;
        private bool locked;
    }
}
