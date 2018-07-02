using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    //工作表
    public class Schedule:CLItem
    {
        //样本编号
        private string _SMPNO;
        public string SMPNO
        {
            get { return _SMPNO; }
            set { _SMPNO = value; }
        }
        //测试项目
        private string _Assay;
        public string Assay
        {
            get { return _Assay; }
            set { _Assay = value; }
        }
        //项目类型 生化 离子
        string _AssayType;
        public string AssayType
        {
            get { return _AssayType; }
            set
            {
                _AssayType = value;
            }
        }
        //任务类型N: Normal   E:Emergengy   B:Blk  S:Std  C:Ctrl
        string _WorkType;
        public string WorkType
        {
            get { return _WorkType; }
            set { _WorkType = value; }
        }
        //测试体积类型 I D  N
        string _VolType;
        public string VolType
        {
            get { return _VolType; }
            set { _VolType = value; }
        }
        //项目测试顺序号
        int _ASYWorkNO;
        public int ASYWorkNO
        {
            get { return _ASYWorkNO; }
            set { _ASYWorkNO = value; }
        }
        //重测标识  NR：正常重测  DR ：减量体积重测 IR :增量体积重测
        string _ReRun = "NA";
        public string ReRun
        {
            get { return _ReRun; }
            set { _ReRun = value; }
        }
        //预计完成数量
        int _ExpireDoCount = 0;
        public int ExpireDoCount
        {
            get { return _ExpireDoCount; }
            set { _ExpireDoCount = value; }
        }
        //发送完成数量
        int _SendCount = 0;
        public int SendCount
        {
            get { return _SendCount; }
            set { _SendCount = value; }
        }
        //完成数量
        int _FinishCount = 0;
        public int FinishCount
        {
            get { return _FinishCount; }
            set { _FinishCount = value; }
        }
        //创建时间
        DateTime _DrawDateTime = DateTime.Now;
        public DateTime DrawDateTime
        {
            get { return _DrawDateTime; }
            set { _DrawDateTime = value; }
        }
        //是否执行
        bool _Perform = false;
        public bool Perform
        {
            get { return _Perform; }
            set { _Perform = value; }
        }
        //是否重测标志
        bool _IsReRun = false;
        public bool IsReRun
        {
            get { return _IsReRun; }
            set { _IsReRun = value; }
        }
    }
}
