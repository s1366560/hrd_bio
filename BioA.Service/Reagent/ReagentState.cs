using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    class ReagentState : DataTransmit
    {
        public List<ReagentStateInfoR1R2> QueryReagentStateInfo(string strDBMethod, string p2)
        {
            return myBatis.QueryReagentStateInfo(strDBMethod);
        }

        public List<ReagentStateInfoR1R2> UpdataReagentStateInfo(string strDBMethod, List<ReagentStateInfoR1R2> ReagentStateInfo)
        {
            
            return myBatis.UpdataReagentStateInfo(strDBMethod, ReagentStateInfo);
           
        }

        public List<ReagentStateInfoR1R2> UpdataUnlockReagentState(string strDBMethod, List<ReagentStateInfoR1R2> ReagentStateInfo)
        {
            return myBatis.UpdataReagentStateInfo(strDBMethod, ReagentStateInfo);
        }
    }
}
