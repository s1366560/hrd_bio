using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class ReagentBarcodeParam
    {
        //条码
        public string Barcode { get; set; }
        //剩余百分比比例
        public int ValidPercent { get; set; }
        //交换时间
        public DateTime ExchangeDatetime { get; set; }
    }
}
