using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class ReagentStateInfoR1R2
    {
        public ReagentStateInfoR1R2()
        {
            locked = false;
            projectName = "";
            reagentName = "";
            reagentName2 = "";
            residualQuantity = 0;
            residualQuantity2 = 0;
            pos = "";
            pos2 = "";
            reagentType = "";
            reagentType2 = "";
            //batchNum = "";
            //batchNum2 = "";
            //reagentContainer = "";
            //reagentContainer2 = "";
            //reagentManufacturers = "";
            //reagentManufacturers2 = "";
            reagentResidualVol = 0;
            reagentResidualVol2 = 0;
            validPercent = 0;
            validPercent2 = 0;
        }

        private bool locked;
        /// <summary>
        /// 项目锁定
        /// </summary>
        public bool Locked
        {
            get { return locked; }
            set { locked = value; }
        }

        private string projectName;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        private string reagentName;
        /// <summary>
        /// 试剂名称1
        /// </summary>
        public string ReagentName
        {
            get { return reagentName; }
            set { reagentName = value; }
        }
        private string reagentName2;
        /// <summary>
        /// 试剂名称2
        /// </summary>
        public string ReagentName2
        {
            get { return reagentName2; }
            set { reagentName2 = value; }
        }

        private int residualQuantity;
        /// <summary>
        /// 试剂1可测数量
        /// </summary>
        public int ResidualQuantity
        {
            get { return residualQuantity; }
            set { residualQuantity = value; }
        }
        private int residualQuantity2;
        /// <summary>
        /// 试剂2可测数量
        /// </summary>
        public int ResidualQuantity2
        {
            get { return residualQuantity2; }
            set { residualQuantity2 = value; }
        }

        private string pos;
        /// <summary>
        /// 试剂1位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        private string pos2;
        /// <summary>
        /// 试剂2位置
        /// </summary>
        public string Pos2
        {
            get { return pos2; }
            set { pos2 = value; }
        }

        private string reagentType;
        /// <summary>
        /// 试剂1类型
        /// </summary>
        public string ReagentType
        {
            get { return reagentType; }
            set { reagentType = value; }
        }
        private string reagentType2;
        /// <summary>
        /// 试剂2类型
        /// </summary>
        public string ReagentType2
        {
            get { return reagentType2; }
            set { reagentType2 = value; }
        }

        //private string batchNum;
        ///// <summary>
        ///// 试剂1批号
        ///// </summary>
        //public string BatchNum
        //{
        //    get { return batchNum; }
        //    set { batchNum = value; }
        //}
        //private string batchNum2;
        ///// <summary>
        ///// 试剂2批号
        ///// </summary>
        //public string BatchNum2
        //{
        //    get { return batchNum2; }
        //    set { batchNum2 = value; }
        //}

        //private string reagentContainer;
        ///// <summary>
        ///// 试剂1容器
        ///// </summary>
        //public string ReagentContainer
        //{
        //    get { return reagentContainer; }
        //    set { reagentContainer = value; }
        //}
        //private string reagentContainer2;
        ///// <summary>
        ///// 试剂2容器
        ///// </summary>
        //public string ReagentContainer2
        //{
        //    get { return reagentContainer2; }
        //    set { reagentContainer2 = value; }
        //}

        //private string reagentManufacturers;
        ///// <summary>
        ///// 试剂1生产厂家
        ///// </summary>
        //public string ReagentManufacturers
        //{
        //    get { return reagentManufacturers; }
        //    set { reagentManufacturers = value; }
        //}
        //private string reagentManufacturers2;
        ///// <summary>
        ///// 试剂2生产厂家
        ///// </summary>
        //public string ReagentManufacturers2
        //{
        //    get { return reagentManufacturers2; }
        //    set { reagentManufacturers2 = value; }
        //}

        private float reagentResidualVol;
        /// <summary>
        /// 试剂1剩余容量
        /// </summary>
        public float ReagentResidualVol
        {
            get {return reagentResidualVol;}
            set {reagentResidualVol = value;}
        }

        private float reagentResidualVol2;
        /// <summary>
        /// 试剂2剩余容量
        /// </summary>
        public float ReagentResidualVol2
        {
            get { return reagentResidualVol2; }
            set { reagentResidualVol2 = value; }
        }

        private int validPercent;
        public int ValidPercent
        {
            get { return validPercent; }
            set { validPercent = value; }
        }

        private int validPercent2;

        public int ValidPercent2
        {
            get { return validPercent2; }
            set { validPercent2 = value; }
        }
    }
}
