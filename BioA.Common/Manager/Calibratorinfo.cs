using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class Calibratorinfo
    {
        string calibName;
        /// <summary>
        /// 校准品名
        /// </summary>
        public string CalibName
        {
            get { return calibName; }
            set { calibName = value; }
        }
        string lotNum;
        /// <summary>
        /// 批号
        /// </summary>
        public string LotNum
        {
            get { return lotNum; }
            set { lotNum = value; }
        }
        string pos;
        /// <summary>
        /// 位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        DateTime invalidDate;
        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime InvalidDate
        {
            get { return invalidDate; }
            set { invalidDate = value; }
        }
        string manufacturer;
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

    }
}
