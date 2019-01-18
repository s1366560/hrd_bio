using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 试剂2使用余量计算的次数
    /// </summary>
    public class Reagent2UsingCountInfo
    {
        public Reagent2UsingCountInfo()
        {

        }

        private string _ProjectName;
        private int _ReagentTray;
        private int _Count;
        /// <summary>
        /// 试剂2对应的项目名称
        /// </summary>
        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; }
        }
        /// <summary>
        /// 试剂盘号
        /// </summary>
        public int ReagentTray
        {
            get { return _ReagentTray; }
            set { _ReagentTray = value; }
        }
        /// <summary>
        /// 总次数
        /// </summary>
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }
    }
}
