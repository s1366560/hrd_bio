using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class ReagentSettingsInfo2
    {
          public ReagentSettingsInfo2()
        {
            pos2 = string.Empty;
            projectName2 = string.Empty;
            reagentName2 = string.Empty;
            reagentType2 = string.Empty;
            reagentChamber2 = string.Empty;
            validDate2 = DateTime.Now;
            barcode2 = string.Empty;
            reagentContainer2 = string.Empty;
            batchNum2 = string.Empty;
            locked2 = false;
            residualQuantity2 = 0;
            measuredquantity2 = 0;

        }
       
        int residualQuantity2;
        /// <summary>
        /// 剩余可测数量2
        /// </summary>
        public int ResidualQuantity2
        {
            get { return residualQuantity2; }
            set { residualQuantity2 = value; }
        }
       
        int measuredquantity2;
        /// <summary>
        /// 已测数量2
        /// </summary>
        public int Measuredquantity2
        {
            get { return measuredquantity2; }
            set { measuredquantity2 = value; }
        }
        /// <summary>
        /// 试剂位置2
        /// </summary>
        public string Pos2
        {
            get { return pos2; }
            set { pos2 = value; }
        }
        /// <summary>
        /// 项目名称2
        /// </summary>
        public string ProjectName2
        {
            get { return projectName2; }
            set { projectName2 = value; }
        }
        /// <summary>
        /// 试剂名称2
        /// </summary>
        public string ReagentName2
        {
            get { return reagentName2; }
            set { reagentName2 = value; }
        }
        /// <summary>
        /// 试剂类型 "R1\R2\清洗液"2
        /// </summary>
        public string ReagentType2
        {
            get { return reagentType2; }
            set { reagentType2 = value; }
        }
        /// <summary>
        /// 试剂通道号2
        /// </summary>
        public string ReagentChamber2
        {
            get { return reagentChamber2; }
            set { reagentChamber2 = value; }
        }
        /// <summary>
        /// 有效日期2
        /// </summary>
        public DateTime ValidDate2
        {
            get { return validDate2; }
            set { validDate2 = value; }
        }
        /// <summary>
        /// 条码2
        /// </summary>
        public string Barcode2
        {
            get { return barcode2; }
            set { barcode2 = value; }
        }
        /// <summary>
        /// 试剂容器2
        /// </summary>
        public string ReagentContainer2
        {
            get { return reagentContainer2; }
            set { reagentContainer2 = value; }
        }
        /// <summary>
        /// 批号2
        /// </summary>
        public string BatchNum2
        {
            get { return batchNum2; }
            set { batchNum2 = value; }
        }
        /// <summary>
        /// 是否锁定2
        /// </summary>
        public bool Locked2
        {
            get { return locked2; }
            set { locked2 = value; }
        }

        private string pos2;
        private string projectName2;
        private string reagentName2;
        private string reagentType2;
        private string reagentChamber2;
        private DateTime validDate2;
        private string barcode2;
        private string reagentContainer2;
        private string batchNum2;
        private bool locked2;
    }
    
}
