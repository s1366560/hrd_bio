using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class ResultSet:CLItem
    {
        public ResultSet()
        {
        }
        public ResultSet(string name)
            :base(name)
        {
        }

        //单位
        private string _Unit;
        public string Unit
        {
            get { return _Unit; }
            set { _Unit = value; }
        }
        //样本类型
        private string _SampleType;
        public string SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }
        //数据精度
        private int _RadixPointNum;
        public int RadixPointNum
        {
            get { return _RadixPointNum; }
            set { _RadixPointNum = value; }
        }
    }
}
