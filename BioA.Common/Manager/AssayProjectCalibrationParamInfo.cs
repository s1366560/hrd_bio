using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
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
