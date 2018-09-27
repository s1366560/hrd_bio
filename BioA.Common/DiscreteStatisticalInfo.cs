using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 离散统计模块
    /// </summary>
    public class DiscreteStatisticalInfo
    {
        public DiscreteStatisticalInfo(string sampleNum, List<float> values)
        {
            this.SampleNum = sampleNum;
            StatValue sv = StatDatas.GetStateValue(values);

            this.Count = sv.N.ToString();
            this.Average = sv.MEAN.ToString("#0.0000");
            this.StandardDeviation = sv.SD.ToString("#0.0000");
            this.CVValue = (sv.CV * 100).ToString("#0.00") + "%";
            this.Range = sv.R.ToString("#0.0000");
        }

        /// <summary>
        /// 样本编号
        /// </summary>
        public string SampleNum { get; set; }
        /// <summary>
        /// 次数统计
        /// </summary>
        public string Count { get; set; }
        /// <summary>
        /// 平均值
        /// </summary>
        public string Average { get; set; }
        /// <summary>
        /// 标准差
        /// </summary>
        public string StandardDeviation { get; set; }
        /// <summary>
        /// CV值
        /// </summary>
        public string CVValue { get; set; }
        /// <summary>
        /// 极差
        /// </summary>
        public string Range { get; set; }
    }
}
