using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class Productor
    {
        public Productor()
        {
        }
        //生产商
        string _FactoryName;
        public string FactoryName
        {
            get { return _FactoryName; }
            set { _FactoryName = value; }
        }
        //批号
        string _BatchNO;
        public string BatchNO
        {
            get { return _BatchNO; }
            set { _BatchNO = value; }
        }
        //有效日期
        DateTime _ExpireDay = DateTime.Now;
        public DateTime ExpireDay
        {
            get { return _ExpireDay; }
            set { _ExpireDay = value; }
        }
    }
}
