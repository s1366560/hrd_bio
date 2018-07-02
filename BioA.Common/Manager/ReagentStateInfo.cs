using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 试剂状态信息
    /// </summary>
    public class ReagentStateInfo
    {
        private string reagentPos;
        private string reagentChamber;
        private string reagentName;
        private string projectName;
        private float reagentUsedVol;
        private float reagentSurplusVol;

        /// <summary>
        /// 试剂1位置
        /// </summary>
        public string ReagentPos
        {
            get { return reagentPos; }
            set { reagentPos = value; }
        }
        /// <summary>
        /// 试剂1通道
        /// </summary>
        public string ReagentChamber
        {
            get { return reagentChamber; }
            set { reagentChamber = value; }
        }
        /// <summary>
        /// 试剂1名称
        /// </summary>
        public string ReagentName
        {
            get { return reagentName; }
            set { reagentName = value; }
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
        /// 已用容量
        /// </summary>
        public float ReagentUsedVol
        {
            get { return reagentUsedVol; }
            set { reagentUsedVol = value; }
        }
        /// <summary>
        /// 剩余容量
        /// </summary>
        public float ReagentSurplusVol
        {
            get { return reagentSurplusVol; }
            set { reagentSurplusVol = value; }
        }
    }
}
