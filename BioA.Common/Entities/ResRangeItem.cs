using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BioA.Common.Entities
{
    public class ResRangeItem : CLItem
    {
        public ResRangeItem()
        {
        }
        public ResRangeItem(string name)
            : base(name)
        {
        }
       
        //样本类型
        string _SampleType;
        public string SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }
        //低龄范围
        RefRange _YoungeRefRange = new RefRange();
        public RefRange YoungeRefRange
        {
            get { return _YoungeRefRange; }
            set { _YoungeRefRange = value; }
        }
        //正常范围
        RefRange _NormalReRange = new RefRange();
        public RefRange NormalReRange
        {
            get { return _NormalReRange; }
            set { _NormalReRange = value; }
        }
        //高龄范围
        RefRange _OldRefRange = new RefRange();
        public RefRange OldRefRange
        {
            get { return _OldRefRange; }
            set { _OldRefRange = value; }
        }
    }
}
