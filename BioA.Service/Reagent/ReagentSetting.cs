using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    class ReagentSetting : DataTransmit
    {

        public List<ReagentSettingsInfo> QueryReagentSettingsInfo(string strDBMethod, string p2)
        {
            List<ReagentSettingsInfo> lstReagentSettingsInfo = new List<ReagentSettingsInfo>();
            try
            {
                lstReagentSettingsInfo = myBatis.QueryReagentSettingsInfo(strDBMethod, null);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstReagentSettingsInfo, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstReagentSettingsInfo;
        }
        public List<ReagentSettingsInfo> QueryReagentSettingsInfo2(string strDBMethod, string p2)
        {
            List<ReagentSettingsInfo> lstReagentSettingsInfo = new List<ReagentSettingsInfo>();
            try
            {
                lstReagentSettingsInfo = myBatis.QueryReagentSettingsInfo2(strDBMethod, null);
                LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstReagentSettingsInfo, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstReagentSettingsInfo;
        }
        public string AddreagentSettingInfo(string strDBMethod, ReagentSettingsInfo reagentSettingsInfo)
        {
            string str = "";
            try
            {
                List<ReagentSettingsInfo> lstReagentSettingsInfo = myBatis.QueryReagentSettingsInfo("QueryReagentSetting1", null);
                
                for (int i = 0; i < lstReagentSettingsInfo.Count;i++ )
                {
                    if(reagentSettingsInfo.ProjectName==lstReagentSettingsInfo[i].ProjectName && reagentSettingsInfo.ReagentType == lstReagentSettingsInfo[i].ReagentType && reagentSettingsInfo.ReagentType != "清洗剂")
                    {
                        str= "项目名称不能重复,请重新装载试剂!";
                    }
                    else if (reagentSettingsInfo.ReagentName == lstReagentSettingsInfo[i].ReagentName)
                    {
                        str = "试剂名称不能重复,请重新装载试剂!";
                    }
                    else if (reagentSettingsInfo.Pos == lstReagentSettingsInfo[i].Pos)
                    {
                        str = "试剂位置不能重复，请重新装载试剂！";
                    }
                    else{

                    }
                  
                }
                if(str=="")
                {
                    str = myBatis.AddreagentSettingInfo(strDBMethod, reagentSettingsInfo);
                }

                // 添加或更新试剂状态
                if (reagentSettingsInfo.ReagentType != "清洗剂")
                {
                    ReagentStateInfoR1R2 reagentState = myBatis.QueryReagentStateInfoByProjectName("QueryReagentStateInfoByProjectName", reagentSettingsInfo);
                    if (reagentState == null)
                    {
                        ReagentStateInfoR1R2 addReagentState = new ReagentStateInfoR1R2();
                        addReagentState.ProjectName = reagentSettingsInfo.ProjectName;
                        addReagentState.ReagentType = reagentSettingsInfo.ReagentType;
                        addReagentState.ReagentName = reagentSettingsInfo.ReagentName;
                        addReagentState.Pos = reagentSettingsInfo.Pos;
                        myBatis.AddreagentStateInfoR1R2("reagentStateAdd", addReagentState);
                    }
                    else
                    {
                        // 更新
                        reagentState.ReagentName = reagentSettingsInfo.ReagentName;
                        reagentState.ReagentType = reagentSettingsInfo.ReagentType;
                        reagentState.Pos = reagentSettingsInfo.Pos;
                        myBatis.UpdateReagent1State("UpdateReagent1State", reagentState);
                    }
                }
                else
                {
                    ReagentStateInfoR1R2 addReagentState = new ReagentStateInfoR1R2();
                    addReagentState.ReagentType = reagentSettingsInfo.ReagentType;
                    addReagentState.ReagentName = reagentSettingsInfo.ReagentName;
                    addReagentState.Pos = reagentSettingsInfo.Pos;
                    myBatis.AddreagentStateInfoR1R2("reagentStateAdd", addReagentState);
                }
                                 
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddreagentSettingInfo(string strDBMethod, ReagentSettingsInfo reagentSettingsInfo)==" + e.ToString(), Module.WindowsService);
            }
            return str;
        }
        public string AddreagentSettingInfo2(string strDBMethod, ReagentSettingsInfo reagentSettingsInfo)
        {
            string str = "";
            try
            {
                List<ReagentSettingsInfo> lstReagentSettingsInfo = myBatis.QueryReagentSettingsInfo2("QueryReagentSetting2", null);
                
                for (int i = 0; i < lstReagentSettingsInfo.Count; i++)
                {
                    if (reagentSettingsInfo.ProjectName == lstReagentSettingsInfo[i].ProjectName && reagentSettingsInfo.ReagentType == lstReagentSettingsInfo[i].ReagentType && reagentSettingsInfo.ReagentType != "清洗剂")
                    {
                        str = "项目名称不能重复,请重新装载试剂!";
                    }
                    else if (reagentSettingsInfo.ReagentName == lstReagentSettingsInfo[i].ReagentName)
                    {
                        str = "试剂名称不能重复,请重新装载试剂!";
                    }
                    else if (reagentSettingsInfo.Pos == lstReagentSettingsInfo[i].Pos)
                    {
                        str = "试剂位置不能重复，请重新装载试剂！";
                    }
                    else
                    {

                    }

                }
                if (str == "")
                {
                    str = myBatis.AddreagentSettingInfo2(strDBMethod, reagentSettingsInfo);
                }
                // 添加或更新试剂状态
                if (reagentSettingsInfo.ReagentType != "清洗剂")
                {
                    ReagentStateInfoR1R2 reagentState = myBatis.QueryReagentStateInfoByProjectName("QueryReagentStateInfoByProjectName", reagentSettingsInfo);
                    if (reagentState == null)
                    {
                        ReagentStateInfoR1R2 addReagentState = new ReagentStateInfoR1R2();
                        addReagentState.ProjectName = reagentSettingsInfo.ProjectName;
                        addReagentState.ReagentType2 = reagentSettingsInfo.ReagentType;
                        addReagentState.ReagentName2 = reagentSettingsInfo.ReagentName;
                        addReagentState.Pos2 = reagentSettingsInfo.Pos;
                        myBatis.AddreagentStateInfoR1R2("reagentStateAdd2", addReagentState);                    }
                    else
                    {
                        // 更新
                        reagentState.ReagentName2 = reagentSettingsInfo.ReagentName;
                        reagentState.ReagentType2 = reagentSettingsInfo.ReagentType;
                        reagentState.Pos2 = reagentSettingsInfo.Pos;
                        myBatis.UpdateReagent1State("UpdateReagent2State", reagentState);
                    }
                }
                else
                {
                    ReagentStateInfoR1R2 addReagentState = new ReagentStateInfoR1R2();
                    addReagentState.ReagentType2 = reagentSettingsInfo.ReagentType;
                    addReagentState.ReagentName2 = reagentSettingsInfo.ReagentName;
                    addReagentState.Pos2 = reagentSettingsInfo.Pos;
                    myBatis.AddreagentStateInfoR1R2("reagentStateAdd", addReagentState);
                }
                               


            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AddreagentSettingInfo2(string strDBMethod, ReagentSettingsInfo2 reagentSettingsInfo)==" + e.ToString(), Module.WindowsService);
            }
            return str;
            
        }

        /// <summary>
        /// 删除试剂1表中对应的数据，修改或者删除试剂R1R2表中对应的数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="DeletereagentSettingsInfo"></param>
        /// <returns></returns>
        public int DeletereagentSettingsInfo(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo)
        {
          
            try
            {
                ReagentStateInfoR1R2 reagentR1AndR2 = myBatis.SelectReagentStateForR1R2("SelectReagentStateForR1R2", DeletereagentSettingsInfo);
                // 判断试剂2设置是否存在同一项目的试剂，如果存在，更新试剂状态表，如果不存在，删除试剂表对应数据
                if (reagentR1AndR2.ReagentName2 == null && reagentR1AndR2.ReagentType2 == null &&
                    reagentR1AndR2.ReagentName2 == "" && reagentR1AndR2.ReagentType2 == "")
                {
                    //根据删除试剂R1R2表中试剂2对应的数据
                    myBatis.DeletereagentStateInfoR1R2("DeletereagentStateInfoR1R2", DeletereagentSettingsInfo);
                }
                else
                {
                    //根据项目名称修改试剂R1R2表中试剂2对应的数据
                    myBatis.UpdateReagentStateForR1R2CorrespondenceR1("UpdateReagentStateForR1R2CorrespondenceR1", DeletereagentSettingsInfo);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AssayProDataTrans.cs_AddAssayProject(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.WindowsService);
            }

            //根据项目名称删除试剂1表对应数据
            return myBatis.DeletereagentSettingsInfo(strDBMethod, DeletereagentSettingsInfo);
        }

        /// <summary>
        /// 删除试剂2表中对应的数据，修改或者删除试剂R1R2表中对应的数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="DeletereagentSettingsInfo"></param>
        /// <returns></returns>
        public int DeletereagentSettingsInfo2(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo)
        {
            try
            {
                ReagentStateInfoR1R2 reagentR1AndR2 = myBatis.SelectReagentStateForR1R2("SelectReagentStateForR1R2", DeletereagentSettingsInfo);
                // 判断试剂2设置是否存在同一项目的试剂，如果存在，更新试剂状态表，如果不存在，删除试剂表对应数据
                if (reagentR1AndR2.ReagentName == null  && reagentR1AndR2.ReagentType == null &&
                    reagentR1AndR2.ProjectName == "" && reagentR1AndR2.ReagentType == "")
                {
                    //myBatis.DeletereagentStateInfoR2("DeletereagentStateInfoR2", DeletereagentSettingsInfo);
                    //根据删除试剂R1R2表中试剂2对应的数据
                    myBatis.DeletereagentStateInfoR1R2("DeletereagentStateInfoR1R2", DeletereagentSettingsInfo);
                }
                else
                {
                    //myBatis.UpdateReagentStateForDeleteR2("UpdateReagentStateForDeleteR2", DeletereagentSettingsInfo);
                    //根据项目名称修改试剂R1R2表中试剂2对应的数据
                    myBatis.UpdateReagentStateForR1R2("UpdateReagentStateForR1R2", DeletereagentSettingsInfo);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AssayProDataTrans.cs_AddAssayProject(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.WindowsService);
            }
            //根据项目名称删除试剂2表对应数据
            return myBatis.DeletereagentSettingsInfo2(strDBMethod, DeletereagentSettingsInfo);
        }

        public string AddreagentStateInfoR1R2(string strDBMethod, ReagentStateInfoR1R2 reagentStateInfoR1R2)
        {
            return myBatis.AddreagentStateInfoR1R2(strDBMethod, reagentStateInfoR1R2);
        }
    }
}
