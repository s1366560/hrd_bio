using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class Reagent:CLItem
    {
        public Reagent()
        {
        }
        //试剂识别唯一ID;
        string _RGTID;
        public string RGTID
        {
            get { return _RGTID; }
            set
            {
                _RGTID = value;
            }
        }
        //试剂对应项目名称
        string _Assay;
        public string Assay
        {
            get { return _Assay; }
            set{ _Assay = value;}
        }
        //试剂条码
        private string _BarCode;
        public string BarCode
        {
            get { return _BarCode; }
            set { _BarCode = value; }
        }
        //试剂瓶类型
        RGTContainerType _CType = new RGTContainerType();
        public RGTContainerType CType
        {
            get { return _CType; }
            set { _CType = value; }
        }
        //剩余百分比比例
        private int _ValidPercent = 99;
        public int ValidPercent
        {
            get { return _ValidPercent; }
            set { _ValidPercent = value; }
        }
        //多试剂项目参数
        string _AssayPara = "R1";
        public string AssayPara
        {
            get { return _AssayPara; }
            set { _AssayPara = value; }
        }
        //产品
        Productor _RGTProductor = new Productor();
        public Productor RGTProductor
        {
            get { return _RGTProductor; }
            set { _RGTProductor = value; }
        }
      
    }
}
