using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common.CalcMethod
{
    /// <summary>
    /// 吸光度计算类
    /// </summary>
    /// <date>2017-07-25</date>
    /// <author>冯旗</author>
    public static class AbsCalcMethod
    {
        /// <summary>
        /// 一点终点法
        /// </summary>
        /// <param name="lstAbsValues">区间测光点的吸光度值集合</param>
        /// <returns>反应吸光度</returns>
        public static float OnePointMethod(List<float> lstAbsValues)
        {
            float intAbs = 0;
            if (lstAbsValues.Count > 0)
            {
                foreach (float f in lstAbsValues)
                {
                    intAbs += f;
                }
                return intAbs / lstAbsValues.Count;
            }
            else
            {
                return intAbs;
            }
        }
        /// <summary>
        /// 两点终点法
        /// </summary>
        /// <param name="lstOneTimeAbsValues">第一时间区间测光点的吸光度值集合</param>
        /// <param name="lstTwoTimeAbsValues">第二时间区间测光点的吸光度值集合</param>
        /// <returns>反应吸光度</returns>
        public static float TwoPointMethod(List<float> lstOneTimeAbsValues, List<float> lstTwoTimeAbsValues)
        {
            float intOneTimeAbs = 0;
            float intTwoTimeAbs = 0;

            if (lstOneTimeAbsValues.Count > 0)
            {
                // 求第一时间区间测光点的和
                foreach (float f in lstOneTimeAbsValues)
                {
                    intOneTimeAbs += f;
                }
                // 求第一时间区间测光点的平均值
                intOneTimeAbs = intOneTimeAbs / lstOneTimeAbsValues.Count;
            }

            if (lstTwoTimeAbsValues.Count > 0)
            {
                // 求第二时间区间测光点的和
                foreach (float f in lstTwoTimeAbsValues)
                {
                    intTwoTimeAbs += f;
                }
                // 求第二时间区间测光点的平均值
                intTwoTimeAbs = intTwoTimeAbs / lstTwoTimeAbsValues.Count;
            }

            return intTwoTimeAbs - intOneTimeAbs;
        }
        /// <summary>
        /// 速率A法
        /// </summary>
        /// <param name="AbsList">吸光度集合</param>
        /// <param name="TimeList">时间集合</param>
        /// <returns>吸光度</returns>
        public static float ARateMethod(float[] AbsList, float[] TimeList)
        {
            float intAbs = 0;

            if (AbsList.Length != TimeList.Length)
            {
                return intAbs;
            }

            float sum_x = 0;
            float sum_y = 0;
            float sum_xx = 0;
            float sum_yy = 0;
            float sum_xy = 0;
            for (int i = 0; i < AbsList.Length; i++)
            {
                sum_x += AbsList[i];
                sum_y += TimeList[i];
                sum_xx += AbsList[i] * AbsList[i];
                sum_yy += TimeList[i] * TimeList[i];
                sum_xy += AbsList[i] * TimeList[i];
            }

            float av_x = sum_x / AbsList.Length;
            float av_y = sum_y / AbsList.Length;
            float av_xx = sum_xx / AbsList.Length;
            float av_yy = sum_yy / AbsList.Length;
            float av_xy = sum_xy / AbsList.Length;

            intAbs = (av_xy - av_x * av_y) / (av_xx - av_x * av_x);

            return intAbs;
        }
        /// <summary>
        /// 速率B法
        /// </summary>
        /// <param name="FirAbsList">第一吸光度集合</param>
        /// <param name="FirTimeList">第一时间集合</param>
        /// <param name="SecAbsList">第二吸光度集合</param>
        /// <param name="SecTimeList">第二时间集合</param>
        /// <returns>吸光度</returns>
        public static float BRateMethod(float[] FirAbsList, float[] FirTimeList, float[] SecAbsList, float[] SecTimeList)
        {
            return ARateMethod(SecAbsList, SecTimeList) - ARateMethod(FirAbsList, FirTimeList);
        }
    }
}
