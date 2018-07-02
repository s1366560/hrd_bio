using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 计算项目实体类
    /// </summary>
    public class CalcProjectInfo
    {
        public CalcProjectInfo()
        {
            calcProjectName = string.Empty;
            calcProjectFullName = string.Empty;
            unit = string.Empty;
            sampleType = string.Empty;
            referenceRangeLow = 100000000;
            referenceRangeHigh = 100000000;
            calcFormula = string.Empty;
        }

        /// <summary>
        /// 计算项目名称
        /// </summary>
        public string CalcProjectName
        {
            get { return calcProjectName; }
            set { calcProjectName = value; }
        }
        /// <summary>
        /// 报告名称
        /// </summary>
        public string CalcProjectFullName
        {
            get { return calcProjectFullName; }
            set { calcProjectFullName = value; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }
        /// <summary>
        /// 参考范围
        /// </summary>
        public float ReferenceRangeLow
        {
            get { return referenceRangeLow; }
            set { referenceRangeLow = value; }
        }

        /// <summary>
        /// 参考范围
        /// </summary>
        public float ReferenceRangeHigh
        {
            get { return referenceRangeHigh; }
            set { referenceRangeHigh = value; }
        }
        /// <summary>
        /// 计算公式
        /// </summary>
        public string CalcFormula
        {
            get { return calcFormula; }
            set { calcFormula = value; }
        }

        private string calcProjectName;
        private string calcProjectFullName;
        private string unit;
        private string sampleType;
        private float referenceRangeLow;
        private float referenceRangeHigh;
        private string calcFormula;
    }
}
