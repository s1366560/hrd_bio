using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class CalibrationResultinfo
    {
        public CalibrationResultinfo()
        {
            sampleNum = string.Empty;
            projectName = string.Empty;
            sampleType = string.Empty;
            tCNO = 0;
            remarks = string.Empty;            
        }
        private string sampleNum;
        private string projectName;
        private string sampleType;
        private int tCNO;
        private string remarks;
        private int cUVNO;

        /// <summary>
        /// 样本号
        /// </summary>
        public string SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
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
        /// 进程编号
        /// </summary>
        public int TCNO
        {
            get { return tCNO; }
            set { tCNO = value; }
        }
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        //比色杯编号
        public int CUVNO
        {
            get { return cUVNO; }
            set { cUVNO = value; }
        }

        string calibMethod;
        /// <summary>
        /// 校准方法
        /// </summary>
        public string CalibMethod
        {
            get { return calibMethod; }
            set { calibMethod = value; }
        }
        string calibratorName;
        /// <summary>
        /// 校准品名称
        /// </summary>
        public string CalibratorName
        {
            get { return calibratorName; }
            set { calibratorName = value; }
        }
        float blankAbs;
        /// <summary>
        /// 空白ABS
        /// </summary>
        public float BlankAbs
        {
            get { return blankAbs; }
            set { blankAbs = value; }
        }
        float kFactor;
        /// <summary>
        /// k因子
        /// </summary>
        public float KFactor
        {
            get { return kFactor; }
            set { kFactor = value; }
        }
        float aFactor;
        /// <summary>
        /// a因子
        /// </summary>
        public float AFactor
        {
            get { return aFactor; }
            set { aFactor = value; }
        }
        float bFactor;
        /// <summary>
        /// b因子
        /// </summary>
        public float BFactor
        {
            get { return bFactor; }
            set { bFactor = value; }
        }
        float cFactor;
        /// <summary>
        /// c因子
        /// </summary>
        public float CFactor
        {
            get { return cFactor; }
            set { cFactor = value; }
        }
        float calibAbs;
        /// <summary>
        /// 校准ABS
        /// </summary>
        public float CalibAbs
        {
            get { return calibAbs; }
            set { calibAbs = value; }
        }
        string calibrationState;
        /// <summary>
        /// 校准状态
        /// </summary>
        public string CalibrationState
        {
            get { return calibrationState; }
            set { calibrationState = value; }
        }
        DateTime calibrationDT;
        /// <summary>
        /// 校准时间
        /// </summary>
        public DateTime CalibrationDT
        {
            get { return calibrationDT; }
            set { calibrationDT = value; }
        }

        float calibConcentration;
        /// <summary>
        /// 浓度
        /// </summary>
        public float CalibConcentration
        {
            get { return calibConcentration; }
            set { calibConcentration = value; }
        }

        string pos;
        /// <summary>
        /// 样本位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        
    }
}
