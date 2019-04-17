using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
   public class ReagentSetting : DataTransmit
    {

        public List<ReagentSettingsInfo> QueryReagentSettingsInfo(string strDBMethod, string p2)
        {
            List<ReagentSettingsInfo> lstReagentSettingsInfo = new List<ReagentSettingsInfo>();
            try
            {
                lstReagentSettingsInfo = myBatis.QueryReagentSettingsInfo(strDBMethod, null);
                //LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstReagentSettingsInfo, Module.WindowsService);
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
                //LogInfo.WriteProcessLog(strDBMethod + "zhuszihe33" + lstReagentSettingsInfo, Module.WindowsService);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstReagentSettingsInfo;
        }
       /// <summary>
       /// 保存试剂设置信息和试剂状态信息
       /// </summary>
       /// <param name="disk"></param>
       /// <param name="rs"></param>
       /// <returns></returns>
        public string AddreagentSettingInfo(int disk, ReagentSettingsInfo rs)
        {
            //保存试剂信息
            return myBatis.AddreagentSettingInfo(disk, rs);
        }

        /// <summary>
        /// 删除试剂1表中对应的数据，修改或者删除试剂R1R2表中对应的数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="DeletereagentSettingsInfo"></param>
        /// <returns></returns>
        public int DeletereagentSettingsInfo(string strDBMethod, ReagentSettingsInfo DeletereagentSettingsInfo)
        {
                if (DeletereagentSettingsInfo.ReagentType == "清洗剂")
                {
                    return myBatis.DeletereagentSettingsInfoAndStateInfo("R1", DeletereagentSettingsInfo);
                }
                ReagentStateInfoR1R2 reagentR1AndR2 = myBatis.SelectReagentStateForR1R2("SelectReagentStateForR1R2", DeletereagentSettingsInfo);
                // 判断试剂2设置是否存在同一项目的试剂，如果存在，更新试剂状态表，如果不存在，删除试剂表对应数据
                if ((reagentR1AndR2.ReagentName2 == null && reagentR1AndR2.ReagentType2 == null) ||
                    (reagentR1AndR2.ReagentName2 == "" && reagentR1AndR2.ReagentType2 == ""))
                {
                    //根据删除试剂R1R2表中试剂2对应的数据
                    myBatis.DeletereagentStateInfoR1R2("DeletereagentStateInfoR1R2", DeletereagentSettingsInfo);
                }
                else
                {
                    //根据项目名称修改试剂R1R2表中试剂1对应的数据
                    myBatis.UpdateReagentStateForR1R2CorrespondenceR1("UpdateReagentStateForR1R2CorrespondenceR1", DeletereagentSettingsInfo);
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
            if (DeletereagentSettingsInfo.ReagentType == "清洗剂")
            {
                return myBatis.DeletereagentSettingsInfoAndStateInfo("R2", DeletereagentSettingsInfo);
            }
            ReagentStateInfoR1R2 reagentR1AndR2 = myBatis.SelectReagentStateForR1R2("SelectReagentStateForR1R2", DeletereagentSettingsInfo);
            // 判断试剂1设置是否存在同一项目的试剂，如果存在，更新试剂状态表，如果不存在，删除试剂表对应数据
            if ((reagentR1AndR2.ReagentName == null  && reagentR1AndR2.ReagentType == null) ||
                (reagentR1AndR2.ReagentName == "" && reagentR1AndR2.ReagentType == ""))
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
            //根据项目名称删除试剂2表对应数据
            return myBatis.DeletereagentSettingsInfo2(strDBMethod, DeletereagentSettingsInfo);
        }
    }
}
