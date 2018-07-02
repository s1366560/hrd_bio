using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class RealTimeCUVDataInfo
    {
        public RealTimeCUVDataInfo()
        {
            workNo = 0;
            cuvNo = 0;
            smpNo = "";
            assay = "";
            tC = 0;
            cUVPoint = 0;
            workType = "";
            drawDate = DateTime.Now;
        }

        private int workNo;
        private int cuvNo;
        private string smpNo;
        private string assay;
        private int tC;
        private int cUVPoint;
        private string workType;
        private DateTime drawDate;
        /// <summary>
        /// 工作编号——黑框程序中计算录入
        /// </summary>
        public int WorkNo
        {
            get { return workNo; }
            set { workNo = value; }
        }
        /// <summary>
        /// 比色杯编号
        /// </summary>
        public int CuvNo
        {
            get { return cuvNo; }
            set { cuvNo = value; }
        }
        /// <summary>
        /// 样本编号
        /// </summary>
        public string SmpNo
        {
            get { return smpNo; }
            set { smpNo = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Assay
        {
            get { return assay; }
            set { assay = value; }
        }
        /// <summary>
        /// timecourse编号，对应TimeCourseInfo表
        /// </summary>
        public int TC
        {
            get { return tC; }
            set { tC = value; }
        }
        /// <summary>
        /// 比色杯已检测到多少点（共44个点）
        /// </summary>
        public int CUVPoint
        {
            get { return cUVPoint; }
            set { cUVPoint = value; }
        }
        /// <summary>
        /// 工作类型：急诊、常规、质控、校准
        /// </summary>
        public string WorkType
        {
            get { return workType; }
            set { workType = value; }
        }
        /// <summary>
        /// 产生时间 同TimeCourse，NorRResult的创建是同步的
        /// </summary>
        public DateTime DrawDate
        {
            get { return drawDate; }
            set { drawDate = value; }
        }
    }
}
