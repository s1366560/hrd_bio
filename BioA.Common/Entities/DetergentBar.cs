using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class DetergentBar
    {
        //条码
        public string Barcode { get; set; }
        //剩余百分比比例
        public int Vol { get; set; }
        //交换时间
        public DateTime ExchangeDatetime { get; set; }
    }
}
