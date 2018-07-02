using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class RGTPosition:CLItem
    {
        //试剂盘号
        int _Disk = 1;
        public int Disk
        {
            get { return _Disk; }
            set { _Disk = value;}
        }
        //试剂位置
        string _Position = "";
        public string Position
        {
            get { return _Position; }
            set { _Position = value; }
        }
        //试剂对应项目名称
        string _Assay;
        public string Assay
        {
            get { return _Assay; }
            set { _Assay = value; }
        }
        //多试剂项目参数
        string _AssayPara = "";
        public string AssayPara
        {
            get { return _AssayPara; }
            set { _AssayPara = value; }
        }
        //试剂瓶类型
        RGTContainerType _CType = new RGTContainerType();
        public RGTContainerType CType
        {
            get { return _CType; }
            set { _CType = value; }
        }
        //剩余百分比比例
        private int _ValidPercent = 0;
        public int ValidPercent
        {
            get { return _ValidPercent; }
            set { _ValidPercent = value; }
        }
        //产品
        Productor _RGTProductor = new Productor();
        public Productor RGTProductor
        {
            get { return _RGTProductor; }
            set { _RGTProductor = value; }
        }
        //试剂条码
        private string _BarCode;
        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }
        //锁定
        bool _IsLocked = false;
        public bool IsLocked
        {
            get { return _IsLocked; }
            set { _IsLocked = value; }
        }
        //多试剂位是否可以标志
        bool _IsMutiPositionEnable = true;
        public bool IsMutiPositionEnable
        {
            get { return _IsMutiPositionEnable; }
            set { _IsMutiPositionEnable = value; }
        }
    }
}
