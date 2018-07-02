using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class RealTimeData : CLItem
    {
        //执行编号
        int _WORKNO;
        public int WORKNO
        {
            get { return _WORKNO; }
            set { _WORKNO = value; }
        }
        //比色杯号
        int _CUVNO;
        public int CUVNO
        {
            get { return _CUVNO; }
            set { _CUVNO = value; }
        }
        //反应进程
        long _TC;
        public long TC
        {
            get { return _TC; }
            set { _TC = value; }
        }
        //样本
        string _SMPNO;
        public string SMPNO
        {
            get { return _SMPNO; }
            set { _SMPNO = value; }
        }
        //项目
        string _ASSAY;
        public string ASSAY
        {
            get { return _ASSAY; }
            set { _ASSAY = value; }
        }
        //当前测点
        int _CURPoint;
        public int CURPoint
        {
            get { return _CURPoint; }
            set { _CURPoint = value; }
        }
        //任务类型
        string _WorkType;
        public string WorkType
        {
            get { return _WorkType; }
            set { _WorkType = value; }
        }
        //产生时间 同TimeCourse，NorRResult的创建是同步的
        private DateTime _DrawDate = DateTime.Now;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }
    }
}
