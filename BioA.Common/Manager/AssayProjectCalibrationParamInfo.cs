using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    ///     校准参数实体类
    /// </summary>
    public class AssayProjectCalibrationParamInfo
    {
        public AssayProjectCalibrationParamInfo()
        {
            projectName = string.Empty;
            sampleType = string.Empty;
            calibrationMethod = string.Empty;
            point = 0;
            span = 0;
            absLimit = 0;
            duplicatePercent = 0;
            duplicateAbs = 0;
            sensitivityHigh = 0;
            sensityvityLow = 0;
            blankAbsHigh = 0;
            blankAbsLow = 0;
            calibrationTimes = 0;
            autoCalibration = false;
            autoCalibMask = false;
            calibLotCheck = false;
            calibValidDateCheck = false;
            reagentLotCheck = false;
            reagentValidDateCheck = false;
            qCFailed = false;
            Factor = 0;
            calibName0 = "";
            calibName1 = "";
            calibName2 = "";
            calibName3 = "";
            calibName4 = "";
            calibName5 = "";
            calibName6 = "";
            calibPos0 = "";
            calibPos1 = "";
            calibPos2 = "";
            calibPos3 = "";
            calibPos4 = "";
            calibPos5 = "";
            calibPos6 = "";
            CalibConcentration0 = 0;
            CalibConcentration1 = 0;
            CalibConcentration2 = 0;
            CalibConcentration3 = 0;
            CalibConcentration4 = 0;
            CalibConcentration5 = 0;
            CalibConcentration6 = 0;
            calibCurveValidDay = 0;
        }

        private string projectName;
        private string sampleType;
        private string calibrationMethod;
        private int point;
        private int span;
        private float absLimit;
        private float duplicatePercent;
        private float duplicateAbs;
        private float sensitivityHigh;
        private float sensityvityLow;
        private float blankAbsHigh;
        private float blankAbsLow;
        private int calibrationTimes;
        private bool autoCalibration;
        private bool autoCalibMask;
        private bool calibLotCheck;
        private bool calibValidDateCheck;
        private bool reagentLotCheck;
        private bool reagentValidDateCheck;
        private bool qCFailed;
        float factor;
        int calibCurveValidDay;

        public float Factor
        {
            get { return factor; }
            set { factor = value; }
        }
        string calibName0;

        public string CalibName0
        {
            get { return calibName0; }
            set { calibName0 = value; }
        }
        string calibPos0;

        public string CalibPos0
        {
            get { return calibPos0; }
            set { calibPos0 = value; }
        }
        float calibConcentration0;

        public float CalibConcentration0
        {
            get { return calibConcentration0; }
            set { calibConcentration0 = value; }
        }
        string calibName1;

        public string CalibName1
        {
            get { return calibName1; }
            set { calibName1 = value; }
        }
        string calibPos1;

        public string CalibPos1
        {
            get { return calibPos1; }
            set { calibPos1 = value; }
        }
        float calibConcentration1;

        public float CalibConcentration1
        {
            get { return calibConcentration1; }
            set { calibConcentration1 = value; }
        }
        string calibName2;

        public string CalibName2
        {
            get { return calibName2; }
            set { calibName2 = value; }
        }
        string calibPos2;

        public string CalibPos2
        {
            get { return calibPos2; }
            set { calibPos2 = value; }
        }
        float calibConcentration2;

        public float CalibConcentration2
        {
            get { return calibConcentration2; }
            set { calibConcentration2 = value; }
        }
        string calibName3;

        public string CalibName3
        {
            get { return calibName3; }
            set { calibName3 = value; }
        }
        string calibPos3;

        public string CalibPos3
        {
            get { return calibPos3; }
            set { calibPos3 = value; }
        }
        float calibConcentration3;

        public float CalibConcentration3
        {
            get { return calibConcentration3; }
            set { calibConcentration3 = value; }
        }
        string calibName4;

        public string CalibName4
        {
            get { return calibName4; }
            set { calibName4 = value; }
        }
        string calibPos4;

        public string CalibPos4
        {
            get { return calibPos4; }
            set { calibPos4 = value; }
        }
        float calibConcentration4;

        public float CalibConcentration4
        {
            get { return calibConcentration4; }
            set { calibConcentration4 = value; }
        }
        string calibName5;

        public string CalibName5
        {
            get { return calibName5; }
            set { calibName5 = value; }
        }
        string calibPos5;

        public string CalibPos5
        {
            get { return calibPos5; }
            set { calibPos5 = value; }
        }
        float calibConcentration5;

        public float CalibConcentration5
        {
            get { return calibConcentration5; }
            set { calibConcentration5 = value; }
        }
        string calibName6;

        public string CalibName6
        {
            get { return calibName6; }
            set { calibName6 = value; }
        }
        string calibPos6;

        public string CalibPos6
        {
            get { return calibPos6; }
            set { calibPos6 = value; }
        }
        float calibConcentration6;

        public float CalibConcentration6
        {
            get { return calibConcentration6; }
            set { calibConcentration6 = value; }
        }
        /// <summary>
        /// 校准天数
        /// </summary>
        public int CalibCurveValidDay
        {
            get { return calibCurveValidDay; }
            set { calibCurveValidDay = value; }
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
        /// 项目类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }
        /// <summary>
        /// 校准方法
        /// </summary>
        public string CalibrationMethod
        {
            get { return calibrationMethod; }
            set { calibrationMethod = value; }
        }
        /// <summary>
        /// 校准点
        /// </summary>
        public int Point
        {
            get { return point; }
            set { point = value; }
        }
        /// <summary>
        /// 量程点
        /// </summary>
        public int Span
        {
            get { return span; }
            set { span = value; }
        }
        /// <summary>
        /// 吸光度限制
        /// </summary>
        public float AbsLimit
        {
            get { return absLimit; }
            set { absLimit = value; }
        }
        /// <summary>
        /// 重复性百分比
        /// </summary>
        public float DuplicatePercent
        {
            get { return duplicatePercent; }
            set { duplicatePercent = value; }
        }
        /// <summary>
        /// 重复性吸光度
        /// </summary>
        public float DuplicateAbs
        {
            get { return duplicateAbs; }
            set { duplicateAbs = value; }
        }
        /// <summary>
        /// 灵敏度最高限制
        /// </summary>
        public float SensitivityHigh
        {
            get { return sensitivityHigh; }
            set { sensitivityHigh = value; }
        }
        /// <summary>
        /// 灵敏度最低限制
        /// </summary>
        public float SensitivityLow
        {
            get { return sensityvityLow; }
            set { sensityvityLow = value; }
        }
        /// <summary>
        /// 空白吸光度高
        /// </summary>
        public float BlankAbsHigh
        {
            get { return blankAbsHigh; }
            set { blankAbsHigh = value; }
        }
        /// <summary>
        /// 空白吸光度低
        /// </summary>
        public float BlankAbsLow
        {
            get { return blankAbsLow; }
            set { blankAbsLow = value; }
        }
        /// <summary>
        /// 校准次数
        /// </summary>
        public int CalibrationTimes
        {
            get { return calibrationTimes; }
            set { calibrationTimes = value; }
        }
        /// <summary>
        /// 是否自动校准
        /// </summary>
        public bool AutoCalibration
        {
            get { return autoCalibration; }
            set { autoCalibration = value; }
        }
        /// <summary>
        /// 是否自动屏蔽校准
        /// </summary>
        public bool AutoCalibMask
        {
            get { return autoCalibMask; }
            set { autoCalibMask = value; }
        }
        /// <summary>
        /// 校准品批号检查
        /// </summary>
        public bool CalibLotCheck
        {
            get { return calibLotCheck; }
            set { calibLotCheck = value; }
        }
        /// <summary>
        /// 校准品失效日期检查
        /// </summary>
        public bool CalibValidDateCheck
        {
            get { return calibValidDateCheck; }
            set { calibValidDateCheck = value; }
        }
        /// <summary>
        /// 试剂批号检查
        /// </summary>
        public bool ReagentLotCheck
        {
            get { return reagentLotCheck; }
            set { reagentLotCheck = value; }
        }
        /// <summary>
        /// 试剂失效日期检查
        /// </summary>
        public bool ReagentValidDateCheck
        {
            get { return reagentValidDateCheck; }
            set { reagentValidDateCheck = value; }
        }
        /// <summary>
        /// 指控失败检查
        /// </summary>
        public bool QCFailed
        {
            get { return qCFailed; }
            set { qCFailed = value; }
        }
    }
}
