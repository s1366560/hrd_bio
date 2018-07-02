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
            technicalLimitLow = 0;
            technicalLimitHigh = 0;
            repeatLimitLow = 0;
            repeatLimitHigh = 0;
            qCDefaultContainer = string.Empty;
            calibDefaultContainer = string.Empty;
            serumAgeLow1 = 0;
            serumAgeHigh1 = 0;
            serumManConsLow1 = 0;
            serumManConsHigh1 = 0;
            serumWomanConsLow1 = 0;
            serumWomanConsHigh1 = 0;
            urineAgeLow1 = 0;
            urineAgeHigh1 = 0;
            urineManConsLow1 = 0;
            urineManConsHigh1 = 0;
            urineWomanConsLow1 = 0;
            urineWomanConsHigh1 = 0;
            serumAgeLow2 = 0;
            serumAgeHigh2 = 0;
            serumManConsLow2 = 0;
            serumManConsHigh2 = 0;
            serumWomanConsLow2 = 0;
            serumWomanConsHigh2 = 0;
            urineAgeLow2 = 0;
            urineAgeHigh2 = 0;
            urineManConsLow2 = 0;
            urineManConsHigh2 = 0;
            urineWomanConsLow2 = 0;
            urineWomanConsHigh2 = 0;
            serumAgeLow3 = 0;
            serumAgeHigh3 = 0;
            serumManConsLow3 = 0;
            serumManConsHigh3 = 0;
            serumWomanConsLow3 = 0;
            serumWomanConsHigh3 = 0;
            urineAgeLow3 = 0;
            urineAgeHigh3 = 0;
            urineManConsLow3 = 0;
            urineManConsHigh3 = 0;
            urineWomanConsLow3 = 0;
            urineWomanConsHigh3 = 0;
            serumAgeLow4 = 0;
            serumAgeHigh4 = 0;
            serumManConsLow4 = 0;
            serumManConsHigh4 = 0;
            serumWomanConsLow4 = 0;
            serumWomanConsHigh4 = 0;
            urineAgeLow4 = 0;
            urineAgeHigh4 = 0;
            urineManConsLow4 = 0;
            urineManConsHigh4 = 0;
            urineWomanConsLow4 = 0;
            urineWomanConsHigh4 = 0;
            serumDefaultSex = string.Empty;
            serumDefaultAgeLow = 0;
            serumDefaultAgeHigh = 0;
            urineDefaultSex = string.Empty;
            urineDefaultAgeLow = 0;
            urineDefaultAgeHigh = 0;
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
        /// 稀释限制最低
        /// </summary>
        public int TechnicalLimitLow
        {
            get { return technicalLimitLow; }
            set { technicalLimitLow = value; }
        }
        /// <summary>
        /// 稀释限制最高
        /// </summary>
        public int TechnicalLimitHigh
        {
            get { return technicalLimitHigh; }
            set { technicalLimitHigh = value; }
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
        /// <summary>
        /// 质控默认容器
        /// </summary>
        public string QCDefaultContainer
        {
            get { return qCDefaultContainer; }
            set { qCDefaultContainer = value; }
        }
        /// <summary>
        /// 校准品默认容器
        /// </summary>
        public string CalibDefaultContainer
        {
            get { return calibDefaultContainer; }
            set { calibDefaultContainer = value; }
        }
        /// <summary>
        /// 血液年龄low1
        /// </summary>
        public int SerumAgeLow1
        {
            get { return serumAgeLow1; }
            set { serumAgeLow1 = value; }
        }
        /// <summary>
        /// 血液年龄High1
        /// </summary>
        public int SerumAgeHigh1
        {
            get { return serumAgeHigh1; }
            set { serumAgeHigh1 = value; }
        }
        /// <summary>
        /// 血液男人浓度Low1
        /// </summary>
        public float SerumManConsLow1
        {
            get { return serumManConsLow1; }
            set { serumManConsLow1 = value; }
        }
        /// <summary>
        /// 血液男人浓度Hight1
        /// </summary>
        public float SerumManConsHigh1
        {
            get { return serumManConsHigh1; }
            set { serumManConsHigh1 = value; }
        }
        /// <summary>
        /// 血液女人浓度Low1
        /// </summary>
        public float SerumWomanConsLow1
        {
            get { return serumWomanConsLow1; }
            set { serumWomanConsLow1 = value; }
        }
        /// <summary>
        /// 血液女人浓度High1
        /// </summary>
        public float SerumWomanConsHigh1
        {
            get { return serumWomanConsHigh1; }
            set { serumWomanConsHigh1 = value; }
        }
        public int UrineAgeLow1
        {
            get { return urineAgeLow1; }
            set { urineAgeLow1 = value; }
        }
        public int UrineAgeHigh1
        {
            get { return urineAgeHigh1; }
            set { urineAgeHigh1 = value; }
        }
        public float UrineManConsLow1
        {
            get { return urineManConsLow1; }
            set { urineManConsLow1 = value; }
        }
        public float UrineManConsHigh1
        {
            get { return urineManConsHigh1; }
            set { urineManConsHigh1 = value; }
        }
        public float UrineWomanConsLow1
        {
            get { return urineWomanConsLow1; }
            set { urineWomanConsLow1 = value; }
        }
        public float UrineWomanConsHigh1
        {
            get { return urineWomanConsHigh1; }
            set { urineWomanConsHigh1 = value; }
        }
        public int SerumAgeLow2
        {
            get { return serumAgeLow2; }
            set { serumAgeLow2 = value; }
        }
        public int SerumAgeHigh2
        {
            get { return serumAgeHigh2; }
            set { serumAgeHigh2 = value; }
        }
        public float SerumManConsLow2
        {
            get { return serumManConsLow2; }
            set { serumManConsLow2 = value; }
        }
        public float SerumManConsHigh2
        {
            get { return serumManConsHigh2; }
            set { serumManConsHigh2 = value; }
        }
        public float SerumWomanConsLow2
        {
            get { return serumWomanConsLow2; }
            set { serumWomanConsLow2 = value; }
        }
        public float SerumWomanConsHigh2
        {
            get { return serumWomanConsHigh2; }
            set { serumWomanConsHigh2 = value; }
        }
        public int UrineAgeLow2
        {
            get { return urineAgeLow2; }
            set { urineAgeLow2 = value; }
        }
        public int UrineAgeHigh2
        {
            get { return urineAgeHigh2; }
            set { urineAgeHigh2 = value; }
        }
        public float UrineManConsLow2
        {
            get { return urineManConsLow2; }
            set { urineManConsLow2 = value; }
        }
        public float UrineManConsHigh2
        {
            get { return urineManConsHigh2; }
            set { urineManConsHigh2 = value; }
        }
        public float UrineWomanConsLow2
        {
            get { return urineWomanConsLow2; }
            set { urineWomanConsLow2 = value; }
        }
        public float UrineWomanConsHigh2
        {
            get { return urineWomanConsHigh2; }
            set { urineWomanConsHigh2 = value; }
        }
        public int SerumAgeLow3
        {
            get { return serumAgeLow3; }
            set { serumAgeLow3 = value; }
        }
        public int SerumAgeHigh3
        {
            get { return serumAgeHigh3; }
            set { serumAgeHigh3 = value; }
        }
        public float SerumManConsLow3
        {
            get { return serumManConsLow3; }
            set { serumManConsLow3 = value; }
        }
        public float SerumManConsHigh3
        {
            get { return serumManConsHigh3; }
            set { serumManConsHigh3 = value; }
        }
        public float SerumWomanConsLow3
        {
            get { return serumWomanConsLow3; }
            set { serumWomanConsLow3 = value; }
        }
        public float SerumWomanConsHigh3
        {
            get { return serumWomanConsHigh3; }
            set { serumWomanConsHigh3 = value; }
        }
        public int UrineAgeLow3
        {
            get { return urineAgeLow3; }
            set { urineAgeLow3 = value; }
        }
        public int UrineAgeHigh3
        {
            get { return urineAgeHigh3; }
            set { urineAgeHigh3 = value; }
        }
        public float UrineManConsLow3
        {
            get { return urineManConsLow3; }
            set { urineManConsLow3 = value; }
        }
        public float UrineManConsHigh3
        {
            get { return urineManConsHigh3; }
            set { urineManConsHigh3 = value; }
        }
        public float UrineWomanConsLow3
        {
            get { return urineWomanConsLow3; }
            set { urineWomanConsLow3 = value; }
        }
        public float UrineWomanConsHigh3
        {
            get { return urineWomanConsHigh3; }
            set { urineWomanConsHigh3 = value; }
        }
        public int SerumAgeLow4
        {
            get { return serumAgeLow4; }
            set { serumAgeLow4 = value; }
        }
        public int SerumAgeHigh4
        {
            get { return serumAgeHigh4; }
            set { serumAgeHigh4 = value; }
        }
        public float SerumManConsLow4
        {
            get { return serumManConsLow4; }
            set { serumManConsLow4 = value; }
        }
        public float SerumManConsHigh4
        {
            get { return serumManConsHigh4; }
            set { serumManConsHigh4 = value; }
        }
        public float SerumWomanConsLow4
        {
            get { return serumWomanConsLow4; }
            set { serumWomanConsLow4 = value; }
        }
        public float SerumWomanConsHigh4
        {
            get { return serumWomanConsHigh4; }
            set { serumWomanConsHigh4 = value; }
        }
        public int UrineAgeLow4
        {
            get { return urineAgeLow4; }
            set { urineAgeLow4 = value; }
        }
        public int UrineAgeHigh4
        {
            get { return urineAgeHigh4; }
            set { urineAgeHigh4 = value; }
        }
        public float UrineManConsLow4
        {
            get { return urineManConsLow4; }
            set { urineManConsLow4 = value; }
        }
        public float UrineManConsHigh4
        {
            get { return urineManConsHigh4; }
            set { urineManConsHigh4 = value; }
        }
        public float UrineWomanConsLow4
        {
            get { return urineWomanConsLow4; }
            set { urineWomanConsLow4 = value; }
        }
        public float UrineWomanConsHigh4
        {
            get { return urineWomanConsHigh4; }
            set { urineWomanConsHigh4 = value; }
        }
        /// <summary>
        /// 血液默认性别
        /// </summary>
        public string SerumDefaultSex
        {
            get { return serumDefaultSex; }
            set { serumDefaultSex = value; }
        }
        /// <summary>
        /// 血液默认年龄low
        /// </summary>
        public int SerumDefaultAgeLow
        {
            get { return serumDefaultAgeLow; }
            set { serumDefaultAgeLow = value; }
        }
        /// <summary>
        /// 血液默认年龄High
        /// </summary>
        public int SerumDefaultAgeHigh
        {
            get { return serumDefaultAgeHigh; }
            set { serumDefaultAgeHigh = value; }
        }
        public string UrineDefaultSex
        {
            get { return urineDefaultSex; }
            set { urineDefaultSex = value; }
        }
        public int UrineDefaultAgeLow
        {
            get { return urineDefaultAgeLow; }
            set { urineDefaultAgeLow = value; }
        }
        public int UrineDefaultAgeHigh
        {
            get { return urineDefaultAgeHigh; }
            set { urineDefaultAgeHigh = value; }
        }

        private string projectName;
        private string sampleType;
        private bool autoRerun;
        private int technicalLimitLow;
        private int technicalLimitHigh;
        private int repeatLimitLow;
        private int repeatLimitHigh;
        private string qCDefaultContainer;
        private string calibDefaultContainer;
        private int serumAgeLow1;
        private int serumAgeHigh1;
        private float serumManConsLow1;
        private float serumManConsHigh1;
        private float serumWomanConsLow1;
        private float serumWomanConsHigh1;
        private int urineAgeLow1;
        private int urineAgeHigh1;
        private float urineManConsLow1;
        private float urineManConsHigh1;
        private float urineWomanConsLow1;
        private float urineWomanConsHigh1;
        private int serumAgeLow2;
        private int serumAgeHigh2;
        private float serumManConsLow2;
        private float serumManConsHigh2;
        private float serumWomanConsLow2;
        private float serumWomanConsHigh2;
        private int urineAgeLow2;
        private int urineAgeHigh2;
        private float urineManConsLow2;
        private float urineManConsHigh2;
        private float urineWomanConsLow2;
        private float urineWomanConsHigh2;
        private int serumAgeLow3;
        private int serumAgeHigh3;
        private float serumManConsLow3;
        private float serumManConsHigh3;
        private float serumWomanConsLow3;
        private float serumWomanConsHigh3;
        private int urineAgeLow3;
        private int urineAgeHigh3;
        private float urineManConsLow3;
        private float urineManConsHigh3;
        private float urineWomanConsLow3;
        private float urineWomanConsHigh3;
        private int serumAgeLow4;
        private int serumAgeHigh4;
        private float serumManConsLow4;
        private float serumManConsHigh4;
        private float serumWomanConsLow4;
        private float serumWomanConsHigh4;
        private int urineAgeLow4;
        private int urineAgeHigh4;
        private float urineManConsLow4;
        private float urineManConsHigh4;
        private float urineWomanConsLow4;
        private float urineWomanConsHigh4;
        private string serumDefaultSex;
        private int serumDefaultAgeLow;
        private int serumDefaultAgeHigh;
        private string urineDefaultSex;
        private int urineDefaultAgeLow;
        private int urineDefaultAgeHigh;
    }
}       