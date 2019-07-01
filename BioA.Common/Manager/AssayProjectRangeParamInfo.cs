using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 范围参数实体类
    /// </summary>
    public class AssayProjectRangeParamInfo
    {
        public AssayProjectRangeParamInfo()
        {
            projectName = string.Empty;
            sampleType = string.Empty;
            autoRerun = false;
            repeatLimitLow = 0;
            repeatLimitHigh = 0;
            ageLow1 = -100000000;
            ageHigh1 = 100000000;
            manConsLow1 = -1000000000;
            manConsHigh1 = 100000000;
            womanConsLow1 = -100000000;
            womanConsHigh1 = 100000000;
            ageLow2 = -100000000;
            ageHigh2 = 100000000;
            manConsLow2 = -100000000;
            manConsHigh2 = 100000000;
            womanConsLow2 = -100000000;
            womanConsHigh2 = 100000000;
            ageLow3 = -100000000;
            ageHigh3 = 100000000;
            manConsLow3 = -100000000;
            manConsHigh3 = 100000000;
            womanConsLow3 = -100000000;
            womanConsHigh3 = 100000000;
            ageLow4 = -100000000;
            ageHigh4 = 100000000;
            manConsLow4 = -100000000;
            manConsHigh4 = 100000000;
            womanConsLow4 = -100000000;
            womanConsHigh4 = 100000000;
        }
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
        /// 是否自动重测
        /// </summary>
        public bool AutoRerun
        {
            get { return autoRerun; }
            set { autoRerun = value; }
        }
        /// <summary>
        /// 重测检查限制最低
        /// </summary>
        public int RepeatLimitLow
        {
            get { return repeatLimitLow; }
            set { repeatLimitLow = value; }
        }
        /// <summary>
        /// 重测检查限制最高
        /// </summary>
        public int RepeatLimitHigh
        {
            get { return repeatLimitHigh; }
            set { repeatLimitHigh = value; }
        }
        
        public string UnitAge1
        {
            get { return unitAge1; }
            set { unitAge1 = value; }
        }

        /// <summary>
        /// 血液年龄low1
        /// </summary>
        public int AgeLow1
        {
            get { return ageLow1; }
            set { ageLow1 = value; }
        }
        /// <summary>
        /// 血液年龄High1
        /// </summary>
        public int AgeHigh1
        {
            get { return ageHigh1; }
            set { ageHigh1 = value; }
        }
        /// <summary>
        /// 血液男人浓度Low1
        /// </summary>
        public float ManConsLow1
        {
            get { return manConsLow1; }
            set { manConsLow1 = value; }
        }
        /// <summary>
        /// 血液男人浓度Hight1
        /// </summary>
        public float ManConsHigh1
        {
            get { return manConsHigh1; }
            set { manConsHigh1 = value; }
        }
        /// <summary>
        /// 血液女人浓度Low1
        /// </summary>
        public float WomanConsLow1
        {
            get { return womanConsLow1; }
            set { womanConsLow1 = value; }
        }
        /// <summary>
        /// 血液女人浓度High1
        /// </summary>
        public float WomanConsHigh1
        {
            get { return womanConsHigh1; }
            set { womanConsHigh1 = value; }
        }

        public string UnitAge2
        {
            get { return unitAge2; }
            set { unitAge2 = value; }
        }

        public int AgeLow2
        {
            get { return ageLow2; }
            set { ageLow2 = value; }
        }
        public int AgeHigh2
        {
            get { return ageHigh2; }
            set { ageHigh2 = value; }
        }
        public float ManConsLow2
        {
            get { return manConsLow2; }
            set { manConsLow2 = value; }
        }
        public float ManConsHigh2
        {
            get { return manConsHigh2; }
            set { manConsHigh2 = value; }
        }
        public float WomanConsLow2
        {
            get { return womanConsLow2; }
            set { womanConsLow2 = value; }
        }
        public float WomanConsHigh2
        {
            get { return womanConsHigh2; }
            set { womanConsHigh2 = value; }
        }

        public string UnitAge3
        {
            get { return unitAge3; }
            set { unitAge3 = value; }
        }

        public int AgeLow3
        {
            get { return ageLow3; }
            set { ageLow3 = value; }
        }
        public int AgeHigh3
        {
            get { return ageHigh3; }
            set { ageHigh3 = value; }
        }
        public float ManConsLow3
        {
            get { return manConsLow3; }
            set { manConsLow3 = value; }
        }
        public float ManConsHigh3
        {
            get { return manConsHigh3; }
            set { manConsHigh3 = value; }
        }
        public float WomanConsLow3
        {
            get { return womanConsLow3; }
            set { womanConsLow3 = value; }
        }
        public float WomanConsHigh3
        {
            get { return womanConsHigh3; }
            set { womanConsHigh3 = value; }
        }

        public string UnitAge4
        {
            get { return unitAge4; }
            set { unitAge4 = value; }
        }

        public int AgeLow4
        {
            get { return ageLow4; }
            set { ageLow4 = value; }
        }
        public int AgeHigh4
        {
            get { return ageHigh4; }
            set { ageHigh4 = value; }
        }
        public float ManConsLow4
        {
            get { return manConsLow4; }
            set { manConsLow4 = value; }
        }
        public float ManConsHigh4
        {
            get { return manConsHigh4; }
            set { manConsHigh4 = value; }
        }
        public float WomanConsLow4
        {
            get { return womanConsLow4; }
            set { womanConsLow4 = value; }
        }
        public float WomanConsHigh4
        {
            get { return womanConsHigh4; }
            set { womanConsHigh4 = value; }
        }

        private string projectName;
        private string sampleType;
        private bool autoRerun;
        private int repeatLimitLow;
        private int repeatLimitHigh;
        private string unitAge1;
        private int ageLow1;
        private int ageHigh1;
        private float manConsLow1;
        private float manConsHigh1;
        private float womanConsLow1;
        private float womanConsHigh1;
        private string unitAge2;
        private int ageLow2;
        private int ageHigh2;
        private float manConsLow2;
        private float manConsHigh2;
        private float womanConsLow2;
        private float womanConsHigh2;
        private string unitAge3;
        private int ageLow3;
        private int ageHigh3;
        private float manConsLow3;
        private float manConsHigh3;
        private float womanConsLow3;
        private float womanConsHigh3;
        private string unitAge4;
        private int ageLow4;
        private int ageHigh4;
        private float manConsLow4;
        private float manConsHigh4;
        private float womanConsLow4;
        private float womanConsHigh4;
    }
}       