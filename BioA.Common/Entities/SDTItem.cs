using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BioA.Common.Entities
{
    public class SDTConcVal
    {
        string _Assay;
        public string Assay
        {
            get { return _Assay; }
            set { _Assay = value; }
        }
        float _SDTConc = 0;
        public float SDTConc
        {
            get { return _SDTConc; }
            set { _SDTConc = value; }
        }
        float _SDTExpireAbs = 0;
        public float SDTExpireAbs
        {
            get { return _SDTExpireAbs; }
            set { _SDTExpireAbs = value; }
        }
    }
    public class SDTItemValue
    {
        string _SDTItemName;
        public string SDTItemName
        {
            get { return _SDTItemName; }
            set { _SDTItemName = value; }
        }
        float _SDTConc = 0;
        public float SDTConc
        {
            get { return _SDTConc; }
            set { _SDTConc = value; }
        }
        float _SDTExpireAbs = 0;
        public float SDTExpireAbs
        {
            get { return _SDTExpireAbs; }
            set { _SDTExpireAbs = value; }
        }
    }
    public class SDTItem : CLItem
    {
        public SDTItem()
        {
        }

        public SDTItem(string name)
            :base(name)
        {
        }

        //产品
        Productor _SDTProductor = new Productor();
        public Productor SDTProductor
        {
            get { return _SDTProductor; }
            set { _SDTProductor = value; }
        }
        //标准测试名称
        List<SDTConcVal> _SDTAssays = new List<SDTConcVal>();
        public List<SDTConcVal> SDTAssays
        {
            get { return _SDTAssays; }
            set { _SDTAssays = value; }
        }
        //定标位置
        string _SDTPosition;
        public string SDTPosition
        {
            get { return _SDTPosition; }
            set { _SDTPosition = value; }
        }
    }
}
