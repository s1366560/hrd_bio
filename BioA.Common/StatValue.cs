using System;
using System.Collections.Generic;
using System.Linq;

namespace BioA.Common
{
    /// <summary>
    /// 离散统计值
    /// </summary>
    public class StatValue
    {
        
        /// <summary>
        /// 次数统计
        /// </summary>
        public int N { get; set; }
        /// <summary>
        /// 平均值
        /// </summary>
        public float MEAN { get; set; }
        /// <summary>
        /// 标准差
        /// </summary>
        public float SD { get; set; }
        /// <summary>
        /// CV值
        /// </summary>
        public float CV { get; set; }
        /// <summary>
        /// 极差
        /// </summary>
        public float R { get; set; }
    }
    public class StatDatas
    {
        public static StatValue GetStateValue(List<float> values)
        {
            StatValue V = new StatValue();

            V.N = values.Count;
            V.MEAN = 0;
            V.SD = 0;
            V.CV = 0;
            V.R = 0;

            if (values.Count == 0)
            {
                return V;
            }

            float Min = values.ElementAt(0);
            float Max = values.ElementAt(0);
            float Sum = 0;

            foreach (float value in values)
            {
                if (value > Max)
                {
                    Max = value;
                }
                if (value < Min)
                {
                    Min = value;
                }
                Sum += value;
            }
            V.MEAN = (float)(((int)(Sum / V.N * 10000 )) / 10000.0000);
            if (V.N <= 1)
            {
                V.SD = 0;
                V.CV = 0;
                V.R = 0;
            }
            else
            {
                float TSum = 0;
                foreach (float value in values)
                {
                    TSum = (float)(((int)(((float)((int)((value - V.MEAN) * 10000) / 10000.0000) * (float)((int)((value - V.MEAN) * 10000) / 10000.0000)) * 10000)) / 10000.0000) + (float)((int)(TSum * 10000) / 10000.0000);
                }
                V.SD = (float)((int)(Math.Sqrt(TSum / (V.N - 1)) * 10000) / 10000.0000);
                V.CV = V.SD / V.MEAN;
                V.R = Max - Min;
            }
            return V;
        }
    }
}