using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 项目对应试剂状态信息实体类
    /// </summary>
    public class ProjectReagentStateInfo
    {
        public ProjectReagentStateInfo()
        {
            projectName = string.Empty;
            r1Name = string.Empty;
            r1Pos = string.Empty;
            r1TestQuantity = 0;
            r1SurplusQuantity = 0;
            r2Name = string.Empty;
            r2Pos = string.Empty;
            r2TestQuantity = 0;
            r2SurplusQuantity = 0;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        /// <summary>
        /// 项目对应试剂1名称
        /// </summary>
        public string R1Name
        {
            get { return r1Name; }
            set { r1Name = value; }
        }
        /// <summary>
        /// 试剂1位置
        /// </summary>
        public string R1Pos
        {
            get { return r1Pos; }
            set { r1Pos = value; }
        }
        /// <summary>
        /// 试剂1已测数量
        /// </summary>
        public int R1TestQuantity
        {
            get { return r1TestQuantity; }
            set { r1TestQuantity = value; }
        }
        /// <summary>
        /// 试剂1剩余检测数量
        /// </summary>
        public int R1SurplusQuantity
        {
            get { return r1SurplusQuantity; }
            set { r1SurplusQuantity = value; }
        }
        /// <summary>
        /// 项目对应试剂2名称
        /// </summary>
        public string R2Name
        {
            get { return r2Name; }
            set { r2Name = value; }
        }
        /// <summary>
        /// 试剂2位置
        /// </summary>
        public string R2Pos
        {
            get { return r2Pos; }
            set { r2Pos = value; }
        }
        /// <summary>
        /// 试剂2已测数量
        /// </summary>
        public int R2TestQuantity
        {
            get { return r2TestQuantity; }
            set { r2TestQuantity = value; }
        }
        /// <summary>
        /// 试剂2剩余检测数量
        /// </summary>
        public int R2SurplusQuantity
        {
            get { return r2SurplusQuantity; }
            set { r2SurplusQuantity = value; }
        }

        private string      projectName;
        private string      r1Name;
        private string      r1Pos;
        private int         r1TestQuantity;
        private int         r1SurplusQuantity;
        private string      r2Name;
        private string      r2Pos;
        private int         r2TestQuantity;
        private int         r2SurplusQuantity;


    }
}
