using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    class SettingsReagentNeedle : DataTransmit
    {

        public string AddsettingsReagentNeedle(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectReagentNeedle("QueryReagentNeedleAdd", reagentNeedleAntifoulingStrategyInfo);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.ReagentNeedleadd(strDBMethod, reagentNeedleAntifoulingStrategyInfo);
                    count = myBatis.SelectReagentNeedle("QueryReagentNeedleAdd", reagentNeedleAntifoulingStrategyInfo);
                    if (count > 0)
                    {
                        strInfo = "试剂针防污策略创建成功！";
                    }
                    else
                    {
                        strInfo = "试剂针防污策略创建失败，请联系管理员！";
                    }
                }
                else
                {
                    strInfo = "该试剂针防污策略存在，请重新录入。";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }

            return strInfo;
        }

        public List<ReagentNeedleAntifoulingStrategyInfo> QueryReagentNeedle(string strDBMethod)
        {
            List<ReagentNeedleAntifoulingStrategyInfo> lstQueryReagentNeedle = new List<ReagentNeedleAntifoulingStrategyInfo>();
            lstQueryReagentNeedle = myBatis.QueryReagentNeedle(strDBMethod);
            
            return lstQueryReagentNeedle;
        }

        public int ReagentNeedleDelete(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)
        {
          
            return myBatis.DeleteReagentNeedle(strDBMethod, reagentNeedleAntifoulingStrategyInfo);
        
        }



        public string ReagentNeedleUpDate(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfoOld)
        {
            string strResult = myBatis.UpdateReagentNeedle(strDBMethod, reagentNeedleAntifoulingStrategyInfo, reagentNeedleAntifoulingStrategyInfoOld);
            LogInfo.WriteProcessLog("public int UpdateCombProject(string strDBMethod, CombProjectInfo combProjectInfoOld, CombProjectInfo combProInfoNew) == " + strResult.ToString(), Module.WindowsService);

            return strResult;
        }

        public List<string> QueryWashingLiquid(string strDBMethod)
        {
            return myBatis.QueryWashingLiquid(strDBMethod);
        }
    }
}
