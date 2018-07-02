using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class EnvironmentParameter : DataTransmit
    {
        public int UpdateEnvironmentParamInfo(string strDBMethod, EnvironmentParamInfo environmentParamInfo, RunningStateInfo running)
        {
            int intResult = myBatis.UpdateEnvironmentParamInfo(strDBMethod, environmentParamInfo, running);
            LogInfo.WriteProcessLog("UpdateEnvironmentParamInfo(string strDBMethod, EnvironmentParamInfo environmentParamInfo, RunningStateInfo running)" + intResult.ToString(), Module.WindowsService);

            return intResult;
        }

        internal List<EnvironmentParamInfo> QueryEnvironmentParamInfo(string strDBMethod)
        {
            List<EnvironmentParamInfo> lstEnvironmentInfos = myBatis.QueryEnvironmentParamInfo(strDBMethod);
            LogInfo.WriteProcessLog("public List<CalcProjectInfo> QueryCalcProjectAllInfo(string strDBMethod) == " + lstEnvironmentInfos.Count.ToString(), Module.WindowsService);

            return lstEnvironmentInfos;
        }
    }
}
