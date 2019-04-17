using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class SettingsReagentNeedle : DataTransmit
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
                    strInfo = myBatis.ReagentNeedleadd(strDBMethod, reagentNeedleAntifoulingStrategyInfo);
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

        public string ReagentNeedleDelete(string strDBMethod, ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo)
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
        /// <summary>
        /// 获取项目测试顺序表中的所有数据
        /// </summary>
        /// <returns></returns>
        public List<ProjectRunSequenceInfo> QueryAllProjectRunSequenceInfo(string strDBMethod)
        {
            return myBatis.QueryAllProjectRunSequenceInfo(strDBMethod);
        }
        /// <summary>
        /// 删除项目测试顺序表所有数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        public void DeleteAllProjectRunSequenceInfo(string strDBMethod)
        {
            myBatis.DeleteAllProjectRunSequenceInfo(strDBMethod);
        }
        /// <summary>
        /// 保存项目测试顺序信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="lstProSunSequence"></param>
        public string SaveProjectRunSequenceInfo(string strDBMethod, List<ProjectRunSequenceInfo> lstProSunSequence)
        {
            ProjectRunSequenceInfo projectRunSequ = new ProjectRunSequenceInfo();
            int resultCount = 0;
            for (int i = 0; i < lstProSunSequence.Count; i++)
            {
                projectRunSequ.ProjectName = lstProSunSequence[i].ProjectName;
                projectRunSequ.SampleType = lstProSunSequence[i].SampleType;
                projectRunSequ.RunSequence = i + 1;
                resultCount += myBatis.SaveProjectRunSequenceInfo(strDBMethod, projectRunSequ);
            }
            if (resultCount == lstProSunSequence.Count)
            {
                return "调整项目测试顺序保存成功！";
            }
            else
                return null;
        }
    }
}
