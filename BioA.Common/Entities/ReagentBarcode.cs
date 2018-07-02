using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    //更换试剂商需要重置该数据
    public class ReagentBarcode
    {
        //条码
        public string Barcode { get; set; }
        //剩余百分比比例
        public int ValidPercent { get; set; }
        //交换时间
        public DateTime ExchangeDatetime { get; set; }
    }
}
