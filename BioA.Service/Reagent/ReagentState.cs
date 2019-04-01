using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class ReagentState : DataTransmit
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
        /// <summary>
        /// 保存试剂条码配制信息
        /// </summary>
        public void ISaveReagentConfigInfo(ReagentConfigInfo rc)
        {
            myBatis.DeleteReagentConfigInfo();
            myBatis.SaveReagentConfigInfo(rc);
        }
        /// <summary>
        /// 修改试剂状态设置信息
        /// </summary>
        /// <param name="rs"></param>
        public void IUpdateReagentStateInfo(ReagentStateInfo rs)
        {
            myBatis.UpdateReagentStateInfo(rs);
        }
        /// <summary>
        /// 获取试剂状态设置
        /// </summary>
        /// <param name="rs"></param>
        public ReagentStateInfo IGetReagentStateInfo()
        {
            return myBatis.QueryReagentStateSettingInfo();
        }
        /// <summary>
        /// 条码配置信息
        /// </summary>
        /// <returns></returns>
        public ReagentConfigInfo IGetReagentConfigInfo()
        {
            return myBatis.GetReagentConfigInfo();
        }
        /// <summary>
        /// 根据试剂通道号获取对应的想名称
        /// </summary>
        /// <param name="reagentNumbers"></param>
        /// <returns></returns>
        public List<AssayProjectInfo> IGetProjectNameByChannleNum(string reagentNumbers) 
        {
            return myBatis.GetProjectNameByChannleNum(reagentNumbers);
        }
    }
}
