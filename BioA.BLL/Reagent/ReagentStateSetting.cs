using BioA.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Service;
using BioA.Common;

namespace BioA.BLL
{
    public class ReagentStateSetting : IReagentStateSetting
    {

        /// <summary>
        /// 获取试剂状态设置信息
        /// </summary>
        /// <returns></returns>
        public ReagentStateInfo ReagentNumbersList()
        {
            List<int> reagentNumbers = new List<int>();
            ReagentStateInfo rs = new ReagentState().IGetReagentStateInfo();
            if (rs != null)
            {
                if ((rs.ReagentChannelNum1 & 1) == 1)
                {
                    reagentNumbers.Add(1);
                }
                if ((rs.ReagentChannelNum1 & 2) == 2)
                {
                    reagentNumbers.Add(2);
                }
                if ((rs.ReagentChannelNum1 & 4) == 4)
                {
                    reagentNumbers.Add(3);
                }
                if ((rs.ReagentChannelNum1 & 8) == 8)
                {
                    reagentNumbers.Add(4);
                }
                if ((rs.ReagentChannelNum1 & 16) == 16)
                {
                    reagentNumbers.Add(5);
                }

                //reagentChannleNum2
                if ((rs.ReagentChannelNum2 & 1) == 1)
                {
                    reagentNumbers.Add(6);
                }
                if ((rs.ReagentChannelNum2 & 2) == 2)
                {
                    reagentNumbers.Add(7);
                }
                if ((rs.ReagentChannelNum2 & 4) == 4)
                {
                    reagentNumbers.Add(8);
                }
                if ((rs.ReagentChannelNum2 & 8) == 8)
                {
                    reagentNumbers.Add(9);
                }
                if ((rs.ReagentChannelNum2 & 16) == 16)
                {
                    reagentNumbers.Add(10);
                }
                rs.ReagentNumberList = reagentNumbers;

            }
            return rs;
        }


        /// <summary>
        /// 根据试剂开放的通道号获取对应的项目名称
        /// </summary>
        /// <param name="reagentNumbers"></param>
        /// <returns></returns>
        public Dictionary<string,List<string>> Get(List<int> reagentNumbers)
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            string reagentNUmber = "";
            foreach (var r in reagentNumbers)
            {
                reagentNUmber += r + ",";
            }
            string s = reagentNUmber.Trim(',');
            List<AssayProjectInfo> assProejctList = new ReagentState().IGetProjectNameByChannleNum(s);
            List<string> proSNameList = new List<string>();
            List<string> proUNameList = new List<string>();
            List<string> proNNameList = new List<string>();
            if (assProejctList != null || assProejctList.Count > 0)
            {
                try
                {
                    foreach (var item in assProejctList)
                    {
                        if (item.SampleType == "血清")
                        {
                            proSNameList.Add(item.ProjectName);
                            continue;
                        }
                        else if (item.SampleType == "尿液")
                        {
                            proSNameList.Add(item.ProjectName);
                            continue;
                        }
                        else if (item.SampleType == "")
                        {
                            proSNameList.Add(item.ProjectName);
                            continue;
                        }
                    }

                    if (proSNameList.Count > 0)
                    {
                        dic.Add("血清",proSNameList);
                    }
                    if (proUNameList.Count > 0)
                    {
                        dic.Add("尿液", proUNameList);
                    }
                    if (proNNameList.Count > 0)
                    {
                        dic.Add("", proNNameList);
                    }
                }
                catch (Exception ex)
                {
                    LogInfo.WriteErrorLog("" + ex.Message, Module.Reagent);
                }
            }
            return dic;
        }
    }
}
