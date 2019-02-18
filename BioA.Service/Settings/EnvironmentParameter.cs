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

        public List<EnvironmentParamInfo> QueryEnvironmentParamInfo(string strDBMethod)
        {
            List<EnvironmentParamInfo> lstEnvironmentInfos = myBatis.QueryEnvironmentParamInfo(strDBMethod);
            LogInfo.WriteProcessLog("public List<CalcProjectInfo> QueryCalcProjectAllInfo(string strDBMethod) == " + lstEnvironmentInfos.Count.ToString(), Module.WindowsService);

            return lstEnvironmentInfos;
        }
        /// <summary>
        /// 获取质控、校准默认容器及孵育槽温控值
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public RunningStateInfo QueryRuningSateInfo(string strDBMethod)
        {
            RunningStateInfo runningstateinfo = myBatis.QueryRuningSateInfo(strDBMethod);
            return runningstateinfo;
        }
    }
}
