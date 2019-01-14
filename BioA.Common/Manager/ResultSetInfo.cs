using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 项目结果设置表对应的信息
    /// </summary>
    public class ResultSetInfo
    {
        public ResultSetInfo()
        {

        }

        private string projectName;
        private string sampleType;
        private string unit;
        private int radixPointNum;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
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
        /// 结果单位
        /// </summary>
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        /// <summary>
        /// 显示结果的小数位
        /// </summary>
        public int RadixPointNum
        {
            get { return radixPointNum; }
            set { radixPointNum = value; }
        }
    }
}
