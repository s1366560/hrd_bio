using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    //项目类型 
    public class ITEMTYPE
    {
        public const string ASSAY = "ASSAY";
        public const string ISE = "ISE";
        public const string SI = "SI";
        public const string COMX = "COMB";
        public const string CALX = "CAlC";
    }
    //项目状态
    public class ITEMSTATE
    {
        public const string EMPTY = "NULL";
        //申请
        public const string APPL = "WAITING";
        //进入测试队列
        public const string START = "STARTING";
        //项目测试未完毕
        public const string INCOMP = "INCOMPLETING";
        //项目测试完毕
        public const string COMP = "COMPLETING";
        //项目重测
        public const string REDO = "RERUN";
    }
    //项目执行体积类型
    public class VOLTYPE
    {
        public const string NA = "NULL";
        public const string NV = "NORMALVOL";
        public const string DV = "DECREAVOL";
        public const string IV = "INCREAVOL";
        public const string SV = "SDTVOL";
    }
    public class SMPItem : CLItem
    {
        //样本编号
        private string _SMPNO;
        public string SMPNO
        {
            get { return _SMPNO; }
            set { _SMPNO = value; }
        }
        //项目名称
        string _ItemName;
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
        //项目产生时间
        DateTime _DrawDateTime = DateTime.Now;
        public DateTime DrawDateTime
        {
            get { return _DrawDateTime; }
            set { _DrawDateTime = value; }
        }
    }
}
