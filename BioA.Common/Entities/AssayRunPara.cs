using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class AssayRunPara : CLItem
    {
        public AssayRunPara()
        {
        }
        public AssayRunPara(string name)
            :base(name)
        {
        }
        //分析方法
        string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }
        //分析方法
        string _AnalyzeMethod = "1Point";
        public string AnalyzeMethod
        {
            get { return _AnalyzeMethod; }
            set { _AnalyzeMethod = value; }
        }
        //测试次数
        int _DoCount = 1;
        public int DoCount
        {
            get { return _DoCount; }
            set { _DoCount = value; }
        }
        //质控间隔
        int _QCSpace = 0;
        public int QCSpace
        {
            get { return _QCSpace; }
            set { _QCSpace = value; }
        }
        //定标次数
        int _SDTCount = 1;
        public int SDTCount
        {
            get { return _SDTCount; }
            set { _SDTCount = value; }
        }
        //测试序列
        int _DoWorkSQ = 0;
        public int DoWorkSQ
        {
            get { return _DoWorkSQ; }
            set { _DoWorkSQ = value; }
        }
        //防止污染序号
        int _UnPollutedSQ = 0;
        public int UnPollutedSQ
        {
            get { return _UnPollutedSQ; }
            set { _UnPollutedSQ = value; }
        }
        //第一测试点S
        int _FirstPointS  = 0;
        public int FirstPointS
        {
            get { return _FirstPointS; }
            set { _FirstPointS = value; }
        }
        //第一测试点E
        int _FirstPointE = 0;
        public int FirstPointE
        {
            get { return _FirstPointE; }
            set { _FirstPointE = value; }
        }
        //第二测试点S
        int _SecondPointS = 1;
        public int SecondPointS
        {
            get { return _SecondPointS; }
            set { _SecondPointS = value; }
        }
        //第二测试点E
        int _SecondPointE = 1;
        public int SecondPointE
        {
            get { return _SecondPointE; }
            set { _SecondPointE = value; }
        }
        //主波长
        int _MainWaveLength = 340;
        public int MainWaveLength
        {
            get { return _MainWaveLength; }
            set { _MainWaveLength = value; }
        }
        //次波长
        int _SubWaveLength = 0;
        public int SubWaveLength
        {
            get { return _SubWaveLength; }
            set { _SubWaveLength = value; }
        }
        //试剂是否开放
        bool _IsOpen = true;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { _IsOpen = value; }
        }
        //试剂1体积
        int _R1Vol = 160;
        public int R1Vol
        {
            get { return _R1Vol; }
            set { _R1Vol = value; }
        }
        //试剂2体积
        int _R2Vol = 0;
        public int R2Vol
        {
            get { return _R2Vol; }
            set { _R2Vol = value; }
        }
        //试剂3体积
        int _R3Vol = 0;
        public int R3Vol
        {
            get { return _R3Vol; }
            set { _R3Vol = value; }
        }
        //试剂4体积
        int _R4Vol = 0;
        public int R4Vol
        {
            get { return _R4Vol; }
            set { _R4Vol = value; }
        }
        //试剂5体积
        int _R5Vol = 0;
        public int R5Vol
        {
            get { return _R5Vol; }
            set { _R5Vol = value; }
        }
        //试剂5体积
        int _R6Vol = 0;
        public int R6Vol
        {
            get { return _R6Vol; }
            set { _R6Vol = value; }
        }
        //血清增量体积
        SMPVolRf _SerumIncreaseVol = new SMPVolRf();
        public SMPVolRf SerumIncreaseVol
        {
            get { return _SerumIncreaseVol; }
            set { _SerumIncreaseVol = value; }
        }
        //血清常规体积
        SMPVolRf _SerumNormalVol = new SMPVolRf();
        public SMPVolRf SerumNormalVol
        {
            get { return _SerumNormalVol; }
            set { _SerumNormalVol = value; }
        }
        //血清减量体积
        SMPVolRf _SerumDecreaseVol = new SMPVolRf();
        public SMPVolRf SerumDecreaseVol
        {
            get { return _SerumDecreaseVol; }
            set { _SerumDecreaseVol = value; }
        }
        //尿液增量体积
        SMPVolRf _UrineIncreaseVol = new SMPVolRf();
        public SMPVolRf UrineIncreaseVol
        {
            get { return _UrineIncreaseVol; }
            set { _UrineIncreaseVol = value; }
        }
        //尿液常规体积
        SMPVolRf _UrineNormalVol = new SMPVolRf();
        public SMPVolRf UrineNormalVol
        {
            get { return _UrineNormalVol; }
            set { _UrineNormalVol = value; }
        }
        //尿液减量体积
        SMPVolRf _UrineDecreaseVol = new SMPVolRf();
        public SMPVolRf UrineDecreaseVol
        {
            get { return _UrineDecreaseVol; }
            set { _UrineDecreaseVol = value; }
        }
        //定标体积
        SMPVolRf _SDTVol = new SMPVolRf();
        public SMPVolRf SDTVol
        {
            get { return _SDTVol; }
            set { _SDTVol = value; }
        }
        //反应方向
        private int _ReacteDirect=1;
        public int ReacteDirect
        {
            get { return _ReacteDirect; }
            set { _ReacteDirect = value; }
        }
        //搅拌1强度
        private int _Stiring1Force = 0;
        public int Stiring1Force
        {
            get { return _Stiring1Force; }
            set { _Stiring1Force = value; }
        }
        //搅拌1强度
        private int _Stiring2Force = 0;
        public int Stiring2Force
        {
            get { return _Stiring2Force; }
            set { _Stiring2Force = value; }
        }
    }
}
