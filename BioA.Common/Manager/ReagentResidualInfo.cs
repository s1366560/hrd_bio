using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 试剂余量和可测数量信息
    /// </summary>
    public class ReagentResidualInfo
    {
        private int validPercent;
        private int validPercent2;
        private int reagentVol;

        /// <summary>
        /// 剩余容量R1
        /// </summary>
        public int ValidPercent
        {
            get { return validPercent; }
            set { validPercent = value; }
        }
        /// <summary>
        /// 剩余容量R2
        /// </summary>
        public int ValidPercent2
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
