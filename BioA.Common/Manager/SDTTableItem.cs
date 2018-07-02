using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 定标曲线
    /// </summary>
    public class SDTTableItem
    {
        public SDTTableItem()
        {
        }
        //K系数法直线斜率
        float _AbsoluteFactor;
        public float AbsoluteFactor
        {
            get { return _AbsoluteFactor; }
            set { _AbsoluteFactor = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        string sampleType;
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
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
      
        //完成定标时间
        DateTime _CalibDate;
        public DateTime CalibDate
        {
            get { return _CalibDate; }
            set { _CalibDate = value; }
        }
        //定标申请时间
        DateTime _DrawDate;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }

        float _BlkAbs;
        public float BlkAbs
        {
            get { return _BlkAbs; }
            set { _BlkAbs = value; }
        }
        float _SDT1Abs;
        public float SDT1Abs
        {
            get { return _SDT1Abs; }
            set { _SDT1Abs = value; }
        }
        float _SDT2Abs;
        public float SDT2Abs
        {
            get { return _SDT2Abs; }
            set { _SDT2Abs = value; }
        }
        float _SDT3Abs;
        public float SDT3Abs
        {
            get { return _SDT3Abs; }
            set { _SDT3Abs = value; }
        }
        float _SDT4Abs;
        public float SDT4Abs
        {
            get { return _SDT4Abs; }
            set { _SDT4Abs = value; }
        }
        float _SDT5Abs;
        public float SDT5Abs
        {
            get { return _SDT5Abs; }
            set { _SDT5Abs = value; }
        }
        float _SDT6Abs;
        public float SDT6Abs
        {
            get { return _SDT6Abs; }
            set { _SDT6Abs = value; }
        }
           
        float _BlkConc;
        public float BlkConc
        {
            get { return _BlkConc; }
            set { _BlkConc = value; }
        }
        float _SDT1Conc;
        public float SDT1Conc
        {
            get { return _SDT1Conc; }
            set { _SDT1Conc = value; }
        }
        float _SDT2Conc;
        public float SDT2Conc
        {
            get { return _SDT2Conc; }
            set { _SDT2Conc = value; }
        }
        float _SDT3Conc;
        public float SDT3Conc
        {
            get { return _SDT3Conc; }
            set { _SDT3Conc = value; }
        }
        float _SDT4Conc;
        public float SDT4Conc
        {
            get { return _SDT4Conc; }
            set { _SDT4Conc = value; }
        }
        float _SDT5Conc;
        public float SDT5Conc
        {
            get { return _SDT5Conc; }
            set { _SDT5Conc = value; }
        }
        float _SDT6Conc;
        public float SDT6Conc
        {
            get { return _SDT6Conc; }
            set { _SDT6Conc = value; }
        }
        string _BlkItem = string.Empty;
        public string BlkItem
        {
            get { return _BlkItem; }
            set { _BlkItem = value; }
        }

        string _Calib1Item = string.Empty;

        public string Calib1Item
        {
            get { return _Calib1Item; }
            set { _Calib1Item = value; }
        }

        string _Calib2Item = string.Empty;

        public string Calib2Item
        {
            get { return _Calib2Item; }
            set { _Calib2Item = value; }
        }

        string _Calib3Item = string.Empty;

        public string Calib3Item
        {
            get { return _Calib3Item; }
            set { _Calib3Item = value; }
        }
        string _Calib4Item = string.Empty;

        public string Calib4Item
        {
            get { return _Calib4Item; }
            set { _Calib4Item = value; }
        }
        string _Calib5Item = string.Empty;

        public string Calib5Item
        {
            get { return _Calib5Item; }
            set { _Calib5Item = value; }
        }
        string _Calib6Item = string.Empty;

        public string Calib6Item
        {
            get { return _Calib6Item; }
            set { _Calib6Item = value; }
        }
        bool _IsUsed = false;

        public bool IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }

        string _CalibState = string.Empty;
        /// <summary>
        /// WAITING（等待中）、CALIBRATING（校准中）、SUCCESSFUL（校准成功）、FAILED（校准失败）
        /// </summary>
        public string CalibState
        {
            get { return _CalibState; }
            set { _CalibState = value; }
        }
    }
}

