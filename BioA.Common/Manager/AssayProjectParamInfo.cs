using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class AssayProjectParamInfo
    {
        public AssayProjectParamInfo()
        {
            projectName = "";
            sampleType = "";
            analysisMethod = "";
            measureLightDot1 = 0;
            measureLightDot2 = 0;
            measureLightDot3 = 0;
            measureLightDot4 = 0;
            resultDecimal = 100000000;
            resultUnit = string.Empty;
            mainWaveLength = 0;
            secWaveLength = 0;
            instrumentFactorA = 100000000;
            instrumentFactorB = 100000000;
            comStosteVol = 100000000;
            comSamVol = 100000000;
            comDilutionVol = 100000000;
            decStosteVol = 100000000;
            decSamVol = 100000000;
            decDilutionVol = 100000000;
            incStosteVol = 100000000;
            incSamVol = 100000000;
            incDilutionVol = 100000000;
            calibStosteVol = 100000000;
            calibSamVol = 100000000;
            calibDilutionVol = 100000000;
            Reagent1Name = string.Empty;
            Reagent1Pos = string.Empty;
            Reagent1Vol = 100000000;
            Reagent1ValidDate = new DateTime();
            Reagent2Name = string.Empty;
            Reagent2Pos = string.Empty;
            Reagent2Vol = 100000000;
            Reagent2ValidDate = new DateTime();
            dilutionType = "";
            firstSlope = 100000000;
            firstSlopeHigh = 100000000;
            proLowestBound = 100000000;
            limitValue = 100000000;
            reactionDirection = "";
            stirring1Intensity = "";
            stirring2Intensity = "";
            serumCriticalMinimum = 100000000;
            serumCriticalMaximum = 100000000;
            reagentBlankMinimum = 100000000;
            reagentBlankMaximum = 100000000;
        }
        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        private string sampleType;

        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }
        private string analysisMethod;

        public string AnalysisMethod
        {
            get { return analysisMethod; }
            set { analysisMethod = value; }
        }
        private int measureLightDot1;

        public int MeasureLightDot1
        {
            get { return measureLightDot1; }
            set { measureLightDot1 = value; }
        }
        private int measureLightDot2;

        public int MeasureLightDot2
        {
            get { return measureLightDot2; }
            set { measureLightDot2 = value; }
        }
        private int measureLightDot3;

        public int MeasureLightDot3
        {
            get { return measureLightDot3; }
            set { measureLightDot3 = value; }
        }
        private int measureLightDot4;

        public int MeasureLightDot4
        {
            get { return measureLightDot4; }
            set { measureLightDot4 = value; }
        }
        private int resultDecimal;

        public int ResultDecimal
        {
            get { return resultDecimal; }
            set { resultDecimal = value; }
        }

        private string resultUnit;
        /// <summary>
        /// 结果单位
        /// </summary>
        public string ResultUnit
        {
            get { return resultUnit; }
            set { resultUnit = value; }
        }
        private int mainWaveLength;
        /// <summary>
        /// 主波长
        /// </summary>
        public int MainWaveLength
        {
            get { return mainWaveLength; }
            set { mainWaveLength = value; }
        }
        private int secWaveLength;
        /// <summary>
        /// 次波长
        /// </summary>
        public int SecWaveLength
        {
            get { return secWaveLength; }
            set { secWaveLength = value; }
        }
        private float instrumentFactorA;

        public float InstrumentFactorA
        {
            get { return instrumentFactorA; }
            set { instrumentFactorA = value; }
        }
        private float instrumentFactorB;

        public float InstrumentFactorB
        {
            get { return instrumentFactorB; }
            set { instrumentFactorB = value; }
        }
        private float comStosteVol;
        /// <summary>
        /// 原液体积
        /// </summary>
        public float ComStosteVol
        {
            get { return comStosteVol; }
            set { comStosteVol = value; }
        }
        private float comSamVol;
        /// <summary>
        /// 稀释液和原液稀释后体积
        /// </summary>
        public float ComSamVol
        {
            get { return comSamVol; }
            set { comSamVol = value; }
        }
        private float comDilutionVol;
        /// <summary>
        /// 稀释体积
        /// </summary>
        public float ComDilutionVol
        {
            get { return comDilutionVol; }
            set { comDilutionVol = value; }
        }
        private float decStosteVol;

        public float DecStosteVol
        {
            get { return decStosteVol; }
            set { decStosteVol = value; }
        }
        private float decSamVol;

        public float DecSamVol
        {
            get { return decSamVol; }
            set { decSamVol = value; }
        }
        private float decDilutionVol;

        public float DecDilutionVol
        {
            get { return decDilutionVol; }
            set { decDilutionVol = value; }
        }
        private float incStosteVol;

        public float IncStosteVol
        {
            get { return incStosteVol; }
            set { incStosteVol = value; }
        }
        private float incSamVol;

        public float IncSamVol
        {
            get { return incSamVol; }
            set { incSamVol = value; }
        }
        private float incDilutionVol;

        public float IncDilutionVol
        {
            get { return incDilutionVol; }
            set { incDilutionVol = value; }
        }
        private float calibStosteVol;

        public float CalibStosteVol
        {
            get { return calibStosteVol; }
            set { calibStosteVol = value; }
        }
        private float calibSamVol;

        public float CalibSamVol
        {
            get { return calibSamVol; }
            set { calibSamVol = value; }
        }
        private float calibDilutionVol;

        public float CalibDilutionVol
        {
            get { return calibDilutionVol; }
            set { calibDilutionVol = value; }
        }
        
        private string reagent1Name;

        public string Reagent1Name
        {
            get { return reagent1Name; }
            set { reagent1Name = value; }
        }

        private string reagent1Pos;

        public string Reagent1Pos
        {
            get { return reagent1Pos; }
            set { reagent1Pos = value; }
        }
        private float reagent1Vol;
        /// <summary>
        /// 试剂1余量体积
        /// </summary>
        public float Reagent1Vol
        {
            get { return reagent1Vol; }
            set { reagent1Vol = value; }
        }
        private DateTime reagent1ValidDate;

        public DateTime Reagent1ValidDate
        {
            get { return reagent1ValidDate; }
            set { reagent1ValidDate = value; }
        }
        private string reagent2Name;

        public string Reagent2Name
        {
            get { return reagent2Name; }
            set { reagent2Name = value; }
        }
        private string reagent2Pos;

        public string Reagent2Pos
        {
            get { return reagent2Pos; }
            set { reagent2Pos = value; }
        }
        private float reagent2Vol;
        /// <summary>
        /// 试剂2余量体积
        /// </summary>
        public float Reagent2Vol
        {
            get { return reagent2Vol; }
            set { reagent2Vol = value; }
        }
        private DateTime reagent2ValidDate;

        public DateTime Reagent2ValidDate
        {
            get { return reagent2ValidDate; }
            set { reagent2ValidDate = value; }
        }

        private string dilutionType;

        public string DilutionType
        {
            get { return dilutionType; }
            set { dilutionType = value; }
        }
        private float firstSlope;
        /// <summary>
        /// 第一线性界限值
        /// </summary>
        public float FirstSlope
        {
            get { return firstSlope; }
            set { firstSlope = value; }
        }
        private float firstSlopeHigh;
        /// <summary>
        /// 第一线性最高值
        /// </summary>
        public float FirstSlopeHigh
        {
            get { return firstSlopeHigh; }
            set { firstSlopeHigh = value; }
        }
        private float proLowestBound;
        /// <summary>
        /// 前区上下界限百分比值
        /// </summary>
        public float ProLowestBound
        {
            get { return proLowestBound; }
            set { proLowestBound = value; }
        }
        private float limitValue;
        /// <summary>
        /// 吸光度界限值
        /// </summary>
        public float LimitValue
        {
            get { return limitValue; }
            set { limitValue = value; }
        }
        private string reactionDirection;
        /// <summary>
        /// 反应方向
        /// </summary>
        public string ReactionDirection
        {
            get { return reactionDirection; }
            set { reactionDirection = value; }
        }
        private string stirring1Intensity;
        /// <summary>
        /// 搅拌强度
        /// </summary>
        public string Stirring1Intensity
        {
            get { return stirring1Intensity; }
            set { stirring1Intensity = value; }
        }
        private string stirring2Intensity;
        /// <summary>
        /// 搅拌强度
        /// </summary>
        public string Stirring2Intensity
        {
            get { return stirring2Intensity; }
            set { stirring2Intensity = value; }
        }

        private int reagent1VolSettings;
        /// <summary>
        /// 试剂1体积设定
        /// </summary>
        public int Reagent1VolSettings
        {
            get { return reagent1VolSettings; }
            set { reagent1VolSettings = value; }
        }

        private int reagent2VolSettings;
        /// <summary>
        /// 试剂2体积设定
        /// </summary>
        public int Reagent2VolSettings
        {
            get { return reagent2VolSettings; }
            set { reagent2VolSettings = value; }
        }
        private float serumCriticalMinimum;
        /// <summary>
        /// 血清临界最小值
        /// </summary>
        public float SerumCriticalMinimum
        {
            get { return serumCriticalMinimum; }
            set { serumCriticalMinimum = value; }
        }
        private float serumCriticalMaximum;
        /// <summary>
        /// 血清临界最大值
        /// </summary>
        public float SerumCriticalMaximum
        {
            get { return serumCriticalMaximum; }
            set { serumCriticalMaximum = value; }
        }
        private float reagentBlankMinimum;
        /// <summary>
        /// 试剂空白最小值
        /// </summary>
        public float ReagentBlankMinimum
        {
            get { return reagentBlankMinimum; }
            set { reagentBlankMinimum = value; }
        }
        private float reagentBlankMaximum;
        /// <summary>
        /// 试剂空白最大值
        /// </summary>
        public float ReagentBlankMaximum
        {
            get { return reagentBlankMaximum; }
            set { reagentBlankMaximum = value; }
        }
    }
}
