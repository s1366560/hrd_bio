using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common
{
    public class TROUBLETYPE
    {
        public const string WARN = "WARNING";
        public const string ERR = "ERROR";
    }
    public class TroubleLog : CLItem
    {
        DateTime _DrawDT = DateTime.Now;
        /// <summary>
        /// 故障信息创建时间
        /// </summary>
        public DateTime DrawDate
        {
            get { return _DrawDT; }
            set { _DrawDT = value; }
        }
        string _TroubleType;
        /// <summary>
        /// 故障信息类型
        /// </summary>
        public string TroubleType
        {
            get { return _TroubleType; }
            set { _TroubleType = value; }
        }
        string _TroubleUnit;
        /// <summary>
        /// 故障单元（故障是那一个模块引发的）
        /// </summary>
        public string TroubleUnit
        {
            get { return _TroubleUnit; }
            set { _TroubleUnit = value; }
        }
        string _TroubleCode;
        /// <summary>
        /// 故障编码
        /// </summary>
        public string TroubleCode
        {
            get { return _TroubleCode; }
            set { _TroubleCode = value; }
        }
        string _TroubleInfo;
        /// <summary>
        /// 故障信息详情
        /// </summary>
        public string TroubleInfo
        {
            get { return _TroubleInfo; }
            set { _TroubleInfo = value; }
        }

        bool _IsConfirm = false;
        /// <summary>
        /// 是否确认故障
        /// </summary>
        public bool IsConfirm 
        {
            get { return _IsConfirm; }
            set { _IsConfirm = value; } 
        }
    }
}
