using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class SDTTableSet : CLItem
    {
        public string AssayName { get; set; }
        public string SDTCurve { get; set; }
        public int ExpireDays { get; set; }

        public string BlkItem { get; set; }
        public string Sdt1Item { get; set; }
        public string Sdt2Item { get; set; }
        public string Sdt3Item { get; set; }
        public string Sdt4Item { get; set; }
        public string Sdt5Item { get; set; }

        //直线斜率
        public float Slope { get; set; }
        public float Intercept { get; set; }
    }
}
