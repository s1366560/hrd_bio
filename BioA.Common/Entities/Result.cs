using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class Result:CLItem
    {
        //样本编号
        private string _SMPNO;
        public string SMPNO
        {
            get { return _SMPNO; }
            set { _SMPNO = value; }
        }
        //样本申请时间
        private DateTime _SampleDrawDate = DateTime.Now;
        public DateTime SampleDrawDate
        {
            get { return _SampleDrawDate; }
            set { _SampleDrawDate = value; }
        }
        //项目
        private string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        //项目类型 计算 组合 
        string _ItemType;
        public string ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        //项目状态
        string _ItemState;
        public string ItemState
        {
            get { return _ItemState; }
            set { _ItemState = value; }
        }
        //反应体积类型 
        private string _VolType;
        public string VolType
        {
            get { return _VolType; }
            set { _VolType = value; }
        }
        //结果Abs
        private string _RAbsValue;
        public string RAbsValue
        {
            get { return _RAbsValue; }
            set { _RAbsValue = value; }
        }
        //结果Conc
        private string _RConcValue;
        public string RConcValue
        {
            get { return _RConcValue; }
            set { _RConcValue = value; }
        }
        //产生时间
        private DateTime _DrawDate = DateTime.Now;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }
        //测试日志
        private string _DoTestLog;
        public string DoTestLog
        {
            get { return _DoTestLog; }
            set { _DoTestLog = value; }
        }
        //反应进程
        private long _TCNO;
        public long TCNO
        {
            get { return _TCNO; }
            set { _TCNO = value; }
        }
        //测试位置
        private int _Disk = 0;
        public int Disk
        {
            get { return _Disk; }
            set { _Disk = value; }
        }
        private string _Position;
        public string Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        private string _Rack;
        public string Rack
        {
            get { return _Rack; }
            set { _Rack = value; }
        }
        //结果复检类型
        private string _ResultType;
        public string ResultType
        {
            get { return _ResultType; }
            set { _ResultType = value; }
        }
        //是否剔除标识
        bool _IsReMoveFlag = false;
        public bool IsReMoveFlag
        {
            get { return _IsReMoveFlag; }
            set { _IsReMoveFlag = value; }
        }
        //是否已发送
        bool _IsSent = false;
        public bool IsSent
        {
            get { return _IsSent; }
            set { _IsSent = value; }
        }
        //结果状态 是否完成
        bool _IsCalculated = false;
        public bool IsCalculated
        {
            get { return _IsCalculated; }
            set { _IsCalculated = value; }
        }
        //是否是重测结果
        bool _IsReRun = false;
        public bool IsReRun
        {
            get { return _IsReRun; }
            set { _IsReRun = value; }
        }
    }
}
