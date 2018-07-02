using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class CalibrationCurveInfo
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
        string pos;
        /// <summary>
        /// 位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
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

        string projectName;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
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
        string sampleType;
       /// <summary>
        /// 样本类型
       /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }

        string calibType;
        /// <summary>
        /// 校准类型
        /// </summary>
        public string CalibType
        {
            get { return calibType; }
            set { calibType = value; }
        }
      
       

        private int calibTime;
       /// <summary>
       /// 重复次数
       /// </summary>
        public int CalibTime
        {
            get { return calibTime; }
            set { calibTime = value; }
        }

        float factor;
       /// <summary>
       /// 因子
       /// </summary>
        public float Factor
        {
            get { return factor; }
            set { factor = value; }
        }
        
    }
}
