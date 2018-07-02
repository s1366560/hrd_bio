using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 比色杯防污策略
    /// </summary>
    public class CuvetteAntifoulingStrategyInfo
    {
        public CuvetteAntifoulingStrategyInfo()
        {
            polluteSourcePro = string.Empty;
            r1CleanLiquidType = string.Empty;
            r1CleanLiquidUserVol = 0;
            r1CleanTimes = 0;
            r2CleanLiquidType = string.Empty;
            r2CleanLiquidUserVol = 0;
            r2CleanTimes = 0;
        }

        private string polluteSourcePro;
        private string r1CleanLiquidType;
        private float r1CleanLiquidUserVol;
        private int r1CleanTimes;
        private string r2CleanLiquidType;
        private float r2CleanLiquidUserVol;
        private int r2CleanTimes;
        /// <summary>
        /// 污染源项目
        /// </summary>
        public string PolluteSourcePro
        {
            get { return polluteSourcePro; }
            set { polluteSourcePro = value; }
        }
        /// <summary>
        /// 试剂1清洗类型
        /// </summary>
        public string R1CleanLiquidType
        {
            get { return r1CleanLiquidType; }
            set { r1CleanLiquidType = value; }
        }
        /// <summary>
        /// 试剂1清洗液使用量
        /// </summary>
        public float R1CleanLiquidUserVol
        {
            get { return r1CleanLiquidUserVol; }
            set { r1CleanLiquidUserVol = value; }
        }
        /// <summary>
        /// 试剂1清洗次数
        /// </summary>
        public int R1CleanTimes
        {
            get { return r1CleanTimes; }
            set { r1CleanTimes = value; }
        }
        /// <summary>
        /// 试剂2清洗液类型
        /// </summary>
        public string R2CleanLiquidType
        {
            get { return r2CleanLiquidType; }
            set { r2CleanLiquidType = value; }
        }
        /// <summary>
        /// 试剂2清洗液使用量
        /// </summary>
        public float R2CleanLiquidUserVol
        {
            get { return r2CleanLiquidUserVol; }
            set { r2CleanLiquidUserVol = value; }
        }
        /// <summary>
        /// 试剂2清洗次数
        /// </summary>
        public int R2CleanTimes
        {
            get { return r2CleanTimes; }
            set { r2CleanTimes = value; }
        }
    }
}
