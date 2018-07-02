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
            resultDecimal = 0;
            resultUnit = string.Empty;
            mainWaveLength = 0;
            secWaveLength = 0;
            instrumentFactorA = 0;
            instrumentFactorB = 0;
            comStosteVol = 0;
            comSamVol = 0; 
            comDilutionVol = 0;
            decStosteVol = 0;
            decSamVol = 0;
            decDilutionVol = 0;
            incStosteVol = 0;
            incSamVol = 0;
            incDilutionVol = 0;
            Reagent1Name = string.Empty;
            Reagent1Pos = string.Empty;
            Reagent1Vol = 0;
            Reagent1ValidDate = DateTime.Now;
            Reagent2Name = string.Empty;
            Reagent2Pos = string.Empty;
            Reagent2Vol = 0;
            Reagent2ValidDate = DateTime.Now;
            dilutionType = "";
            firstSlope = 0;
            secondSlope = 0;
            firstSlopeHigh = 0;
            secondSlopeHigh = 0;
            proLowestBound= 0;
            proHighestBound = 0;
            pmp1 = 0;
            pmp2 = 0;
            pmp3 = 0;
            pmp4 = 0;
            boundDirection = "";
            limit1 = 0;
            limit2 = 0;
            limitValue = 0;
            reactionDirection = "";
            stirringIntensity = "";
            levelConcentration = "";
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

        public string ResultUnit
        {
            get { return resultUnit; }
            set { resultUnit = value; }
        }
        private int mainWaveLength;

        public int MainWaveLength
        {
            get { return mainWaveLength; }
            set { mainWaveLength = value; }
        }
        private int secWaveLength;

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

        public float ComStosteVol
        {
            get { return comStosteVol; }
            set { comStosteVol = value; }
        }
        private float comSamVol;

        public float ComSamVol
        {
            get { return comSamVol; }
            set { comSamVol = value; }
        }
        private float comDilutionVol;

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

        public float FirstSlope
        {
            get { return firstSlope; }
            set { firstSlope = value; }
        }
        private float secondSlope;

        public float SecondSlope
        {
            get { return secondSlope; }
            set { secondSlope = value; }
        }
        private float firstSlopeHigh;

        public float FirstSlopeHigh
        {
            get { return firstSlopeHigh; }
            set { firstSlopeHigh = value; }
        }
        private float secondSlopeHigh;

        public float SecondSlopeHigh
        {
            get { return secondSlopeHigh; }
            set { secondSlopeHigh = value; }
        }
        private float proLowestBound;

        public float ProLowestBound
        {
            get { return proLowestBound; }
            set { proLowestBound = value; }
        }
        private float proHighestBound;

        public float ProHighestBound
        {
            get { return proHighestBound; }
            set { proHighestBound = value; }
        }
        private int pmp1;

        public int Pmp1
        {
            get { return pmp1; }
            set { pmp1 = value; }
        }
        private int pmp2;

        public int Pmp2
        {
            get { return pmp2; }
            set { pmp2 = value; }
        }
        private int pmp3;

        public int Pmp3
        {
            get { return pmp3; }
            set { pmp3 = value; }
        }
        private int pmp4;

        public int Pmp4
        {
            get { return pmp4; }
            set { pmp4 = value; }
        }
        private string boundDirection;

        public string BoundDirection
        {
            get { return boundDirection; }
            set { boundDirection = value; }
        }
        private float limit1;

        public float Limit1
        {
            get { return limit1; }
            set { limit1 = value; }
        }
        private float limit2;

        public float Limit2
        {
            get { return limit2; }
            set { limit2 = value; }
        }
        private float limitValue;

        public float LimitValue
        {
            get { return limitValue; }
            set { limitValue = value; }
        }
        private string reactionDirection;

        public string ReactionDirection
        {
            get { return reactionDirection; }
            set { reactionDirection = value; }
        }
        private string stirringIntensity;

        public string StirringIntensity
        {
            get { return stirringIntensity; }
            set { stirringIntensity = value; }
        }
        private string levelConcentration;

        public string LevelConcentration
        {
            get { return levelConcentration; }
            set { levelConcentration = value; }
        }
    }
}
