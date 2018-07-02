using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class CalibratorProjectinfo
    {
       public CalibratorProjectinfo()
       {
           projectName = string.Empty;
           sampleType = string.Empty;
       }

        string sampleType;
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }

        string projectName;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        string calibName;
        /// <summary>
        /// 校准品名
        /// </summary>
        public string CalibName
        {
            get { return calibName; }
            set { calibName = value; }
        }
        float calibConcentration;
        /// <summary>
        /// 浓度
        /// </summary>
        public float CalibConcentration
        {
            get { return calibConcentration; }
            set { calibConcentration = value; }
        }
        string pos;
       /// <summary>
       /// 校准品位置
       /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        float calibAbs;
        /// <summary>
        /// 校准ABS
        /// </summary>
        public float CalibAbs
        {
            get { return calibAbs; }
            set { calibAbs = value; }
        }

    }
}
