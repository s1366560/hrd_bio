using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 样本针防污策略
    /// </summary>
    public class SampleNeedleAntifoulingStrategyInfo
    {
        public SampleNeedleAntifoulingStrategyInfo()
        {
            polluteSourcePro = string.Empty;
            cleanLiquidType = string.Empty;
            cleanLiquidUserVol = 0;
            cleanTimes = 0;
        }

        private string polluteSourcePro;
        private string cleanLiquidType;
        private float cleanLiquidUserVol;
        private int cleanTimes;
        /// <summary>
        /// 污染源项目
        /// </summary>
        public string PolluteSourcePro
        {
            get { return polluteSourcePro; }
            set { polluteSourcePro = value; }
        }
        /// <summary>
        /// 清洗液类型
        /// </summary>
        public string CleanLiquidType
        {
            get { return cleanLiquidType; }
            set { cleanLiquidType = value; }
        }
        /// <summary>
        /// 清洗液使用量
        /// </summary>
        public float CleanLiquidUserVol
        {
            get { return cleanLiquidUserVol; }
            set { cleanLiquidUserVol = value; }
        }
        /// <summary>
        /// 清洗次数
        /// </summary>
        public int CleanTimes
        {
            get { return cleanTimes; }
            set { cleanTimes = value; }
        }
    }
}
