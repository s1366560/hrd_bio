using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class AnalyzeEvent
    {
        public const string COMPLETED_SCAN_SMPBarcode = "1";
        public const string COMPLETED_SCAN_RGTBarcode = "2";
        public const string COMPLETED_SCAN_RGT2Barcode = "3";
        public const string COMPLETED_READ_SN = "4";
        public const string ACTIVEDCODE_EXPIRED = "5";
        public const string WORKSTATION_NOMATCHING = "6";
        public const string COMPLETED_SCHEDULE = "7";
        public const string ACTIVEDCODE_WILLEXPIRED = "8";
        public const string COMPLETED_SCAN_HWVersion = "9";
        public const string COMPLETED_SCAN_Temp = "10";
        public const string SCAN_Temp_INVALID = "11";
        public const string ACTIVEDCODE_INVALID = "12";
        public const string WATER_REPLE  = "13";//补水
        public const string WORKSTATION_MATCHING = "14";
        public const string COMPLETED_READ_ACTIVEKEY = "15";
        public const string MACHINE_RESET = "16";
        public const string COMPLETE_READ_TEM = "17";
        public const string COMPLETE_PHOTOMETER_AUTOCHECK = "18";
        public const string COMPLETE_ISE_CHECK = "19";
        public const string EXXCHANGE_WARER_FAILED = "20";
        public const string MACHINE_STATE_ER = "21";
        public const string MACHINE_WILL_FINIFSHSCHEDULE = "22";
        public const string COMPLETE_CHECK_WASHLIQUID = "23";
        public const string COMPLETED_SCAN_MSTATE = "24";
        public const string HAS_SCHEDULE_WARNNING = "25";
    }


    public class WORKTYPE
    {
        public const string E = "EMG";
        public const string N = "NORMAL";
        public const string S = "SDT";
        public const string B = "BLANK";
        public const string C = "QC";
        public const string I = "ISE";
    }
    public class SMPSTATE
    {
        public const string EMPTY = "EMPTY";
        //申请样本
        public const string WAIT = "WAITING";
        //准备样本
        //public const string PREP = "PREPARING";
        //样本项目测试，项目结果没有产生
        public const string TEST = "RUNNING";
        //项目结果产生，但是没有全部产生
        public const string NOCO = "INCOMPLETE";
        //样本测试完成
        public const string COMP = "COMPLETE";
        //样本没有测试任务
        public const string NOITEM = "NOITEM";
    }
    public class Sample:CLItem
    {
        public Sample()
        {
        }
        public Sample(string smpno)
        {
            this._SMPNO = smpno;
        }
        //样本编号
        private string _SMPNO;
        public string SMPNO
        {
            get { return _SMPNO; }
            set { _SMPNO = value; }
        }
        //样本备注
        private string _SampleRemarks;
        public string SampleRemarks
        {
            get { return _SampleRemarks; }
            set { _SampleRemarks = value; }
        }
        //条码信息
        private string _BarCode;
        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }
        //样本状态 
        private string _SMPState = SMPSTATE.EMPTY;
        public string SMPState
        {
            get { return _SMPState; }
            set { _SMPState = value; }
        }
        //急诊状态标识
        private bool _IsEmergency;
        public bool IsEmergency
        {
            get { return _IsEmergency; }
            set { _IsEmergency = value; }
        }
        //样本登记时间
        private DateTime _DrawDate = DateTime.Now;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }
        //样本容器类型
        private string _ContainerType;
        public string ContainerType
        {
            get { return _ContainerType; }
            set { _ContainerType = value; }
        }
        //样本类型 血清 尿液
        private string _SampleType;
        public string SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }
        //打印标识
        private bool _IsPrinted = false;
        public bool IsPrinted
        {
            get { return _IsPrinted; }
            set { _IsPrinted = value; }
        }
        //发送标识
        private bool _IsSent = false;
        public bool IsSent
        {
            get { return _IsSent; }
            set { _IsSent = value; }
        }
    }
}
