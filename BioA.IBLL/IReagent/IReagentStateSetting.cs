using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.IBLL
{
    public interface IReagentStateSetting
    {
        /// <summary>
        /// 试剂状态设置信息
        /// </summary>
        /// <returns></returns>
        ReagentStateInfo ReagentNumbersList();

        Dictionary<string, List<string>> Get(List<int> reagentNumbers);
    }
}
