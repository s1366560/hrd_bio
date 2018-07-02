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
        internal List<ReagentStateInfoR1R2> QueryReagentStateInfo(string strDBMethod, string p2)
        {
            List<ReagentStateInfoR1R2> lstQueryReagentStateInfo = new List<ReagentStateInfoR1R2>();
            try
            {
                lstQueryReagentStateInfo = myBatis.QueryReagentStateInfo(strDBMethod, null);
              
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryReagentStateInfo;
        }

        internal int UpdataReagentStateInfo(string strDBMethod, List<ReagentStateInfoR1R2> ReagentStateInfo)
        {
            int a=0;
            for (int i = 0; i < ReagentStateInfo.Count;i++ )
            {
                a+= myBatis.UpdataReagentStateInfo(strDBMethod, ReagentStateInfo[i]);
            }
            return a;
           
        }

        internal int UpdataUnlockReagentState(string strDBMethod, List<ReagentStateInfoR1R2> ReagentStateInfo)
        {
            return myBatis.UpdataUnlockReagentStateInfo(strDBMethod, ReagentStateInfo);
        }
    }
}
