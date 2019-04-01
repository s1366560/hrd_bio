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
        private int reagentStatusModule;
        private int reagentChannelNum1;
        private int reagentChannelNum2;
        /// <summary>
        /// 试剂状态模型： 0：代表什么都没有。 1：代表试剂全部开放。 2：代表试剂半封闭（可以选择1~10的开放通道号）
        /// </summary>
        public int ReagentStatusModule
        {
            get { return reagentStatusModule; }
            set { reagentStatusModule = value; }
        }
        /// <summary>
        /// 试剂开放通道号1~5
        /// </summary>
        public int ReagentChannelNum1
        {
            get { return reagentChannelNum1; }
            set { reagentChannelNum1 = value; }
        }
        /// <summary>
        /// 试剂开放通道号6~10
        /// </summary>
        public int ReagentChannelNum2
        {
            get { return reagentChannelNum2; }
            set { reagentChannelNum2 = value; }
        }
        /// <summary>
        /// 所有的试剂通道号
        /// </summary>
        public List<int> ReagentNumberList { get; set; }
    }
}
