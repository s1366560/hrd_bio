using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 试剂状态信息
    /// </summary>
    public class ReagentStateInfo
    {
        //private string reagentPos;
        //private string reagentChamber;
        //private string reagentName;
        //private string projectName;
        //private float reagentUsedVol;
        private float validPercent;
        private float validPercent2;
        private int reagentVol;

        ///// <summary>
        ///// 试剂1位置
        ///// </summary>
        //public string ReagentPos
        //{
        //    get { return reagentPos; }
        //    set { reagentPos = value; }
        //}
        ///// <summary>
        ///// 试剂1通道
        ///// </summary>
        //public string ReagentChamber
        //{
        //    get { return reagentChamber; }
        //    set { reagentChamber = value; }
        //}
        ///// <summary>
        ///// 试剂1名称
        ///// </summary>
        //public string ReagentName
        //{
        //    get { return reagentName; }
        //    set { reagentName = value; }
        //}
        ///// <summary>
        ///// 项目名称
        ///// </summary>
        //public string ProjectName
        //{
        //    get { return projectName; }
        //    set { projectName = value; }
        //}
        ///// <summary>
        ///// 已用容量
        ///// </summary>
        //public float ReagentUsedVol
        //{
        //    get { return reagentUsedVol; }
        //    set { reagentUsedVol = value; }
        //}
        /// <summary>
        /// 剩余容量R1
        /// </summary>
        public float ValidPercent
        {
            get { return validPercent; }
            set { validPercent = value; }
        }
        /// <summary>
        /// 剩余容量R2
        /// </summary>
        public float ValidPercent2
        {
            get { return validPercent2; }
            set { validPercent2 = value; }
        }
        private int residualQuantity;
        // <summary>
        /// 余量可测量个数R1
        /// </summary>
        public int ResidualQuantity
        {
            get { return residualQuantity; }
            set { residualQuantity = value; }
        }
        private int residualQuantity2;
        /// <summary>
        ///余量可测量个数1R2
        /// </summary>
        public int ResidualQuantity2
        {
            get { return residualQuantity2; }
            set { residualQuantity2 = value; }
        }
        /// <summary>
        /// 试剂加样体积
        /// </summary>
        public int ReagentVol
        {
            get { return reagentVol; }
            set { reagentVol = value; }
        }
    }
}
