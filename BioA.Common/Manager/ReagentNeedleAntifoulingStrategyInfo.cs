using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 试剂针防污策略
    /// </summary>
    public class ReagentNeedleAntifoulingStrategyInfo
    {
        public ReagentNeedleAntifoulingStrategyInfo()
        {
            reagentNeedle = string.Empty;
            polluteProName = string.Empty;
            polluteProType = string.Empty;
            bePollutedProName = string.Empty;
            bePollutedProType = string.Empty;
            cleaningLiquidName = string.Empty;
            cleaningLiquidUseVol = 0;
            cleanTimes = 0;
        }

        private string reagentNeedle;
        private string polluteProName;
        private string polluteProType;
        private string bePollutedProName;
        private string bePollutedProType;
        private string cleaningLiquidName;
        private float cleaningLiquidUseVol;
        private int cleanTimes;
        /// <summary>
        /// 试剂针
        /// </summary>
        public string ReagentNeedle
        {
            get { return reagentNeedle; }
            set { reagentNeedle = value; }
        }
        /// <summary>
        /// 污染源项目
        /// </summary>
        public string PolluteProName
        {
            get { return polluteProName; }
            set { polluteProName = value; }
        }
        /// <summary>
        /// 污染源项目类型
        /// </summary>
        public string PolluteProType
        {
            get { return polluteProType; }
            set { polluteProType = value; }
        }
        /// <summary>
        /// 被污染源项目
        /// </summary>
        public string BePollutedProName
        {
            get { return bePollutedProName; }
            set { bePollutedProName = value; }
        }
        /// <summary>
        /// 被污染源项目类型
        /// </summary>
        public string BePollutedProType
        {
            get { return bePollutedProType; }
            set { bePollutedProType = value; }
        }
        /// <summary>
        /// 清洗液
        /// </summary>
        public string CleaningLiquidName
        {
            get { return cleaningLiquidName; }
            set { cleaningLiquidName = value; }
        }
        /// <summary>
        /// 清洗液使用量
        /// </summary>
        public float CleaningLiquidUseVol
        {
            get { return cleaningLiquidUseVol; }
            set { cleaningLiquidUseVol = value; }
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
