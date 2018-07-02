using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class TimeCourse:CLItem
    {
        public TimeCourse()
        {
        }
        //反应进程
        private long _TCNO;
        public long TCNO
        {
            get { return _TCNO; }
            set { _TCNO = value; }
        }
        int _CUVNO = 0;
        public int CUVNO
        {
            get { return _CUVNO; }
            set { _CUVNO = value; }
        }
        //空白
        private float _CuvBlkWm;
        public float CuvBlkWm
        {
            get { return _CuvBlkWm; }
            set { _CuvBlkWm = value; }
        }
        private float _CuvBlkWs;
        public float CuvBlkWs
        {
            get { return _CuvBlkWs; }
            set { _CuvBlkWs = value; }
        }
        //产生时间 同TimeCourse，Result的创建是同步的
        private DateTime _DrawDate = DateTime.Now;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }

        public List<float> CuvXWmList = new List<float>();
        public List<float> CuvXWsList = new List<float>();

    }
}
