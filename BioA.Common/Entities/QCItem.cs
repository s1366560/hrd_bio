using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BioA.Common.Entities
{
    public class AssayQCVaule
    {
        string _Assay;
        public string Assay
        {
            get { return _Assay; }
            set { _Assay = value; }
        }
        float _MEAN =0;
        public float MEAN
        {
            get { return _MEAN; }
            set { _MEAN = value; }
        }
        float _SD = 0;
        public float SD
        {
            get { return _SD; }
            set { _SD = value; }
        }
    }
    public class QCItem : CLItem
    {
        public QCItem()
        {
        }

        public QCItem(string name)
            :base(name)
        {
        }
        //质控水平
        string _QCLevel;
        public string QCLevel
        {
            get { return _QCLevel; }
            set { _QCLevel = value; }
        }
        //质控产品
        Productor _QCProductor = new Productor();
        public Productor QCProductor
        {
            get { return _QCProductor; }
            set { _QCProductor = value; }
        }
        //质控测试名称
        List<AssayQCVaule> _QCAssays = new List<AssayQCVaule>();
        public List<AssayQCVaule> QCAssays
        {
            get { return _QCAssays; }
            set { _QCAssays = value; }
        }
        //质控位置
        string _QCPosition;
        public string QCPosition
        {
            get { return _QCPosition; }
            set { _QCPosition = value; }
        }
        //质控的样本类型
        string _QCSMPType;
        public string QCSMPType
        {
            get { return _QCSMPType; }
            set { _QCSMPType = value; }
        }
        
    }
}
