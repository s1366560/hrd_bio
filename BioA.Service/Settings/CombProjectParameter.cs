using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    /// <summary>
    /// 设置——组合项目 数据库交互类
    /// </summary>
    public class CombProjectParameter : DataTransmit
    {
        /// <summary>
        /// 获取所有组合项目访问数据库
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        public List<CombProjectInfo> QueryCombProjectNameAllInfo(string strDBMethod)
        {
            List<CombProjectInfo> lstCombProInfos = myBatis.QueryCombProjectNameAllInfo(strDBMethod);
            LogInfo.WriteProcessLog("public List<CombProjectInfo> QueryCombProjectAllInfo(string strDBMethod) == " + lstCombProInfos.Count.ToString(), Module.WindowsService);

            return lstCombProInfos;
        }
        /// <summary>
        /// 通过组合项目名称获取组合项目对应的项目列表
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="CombProName"></param>
        /// <returns></returns>
        public List<string> QueryProjectByCombProName(string strDBMethod, string CombProName)
        {
            List<string> lstProNames = myBatis.QueryProjectByCombProName(strDBMethod, CombProName);
            LogInfo.WriteProcessLog("QueryProjectByCombProName(string strDBMethod, string CombProName) == " + lstProNames.Count.ToString(), Module.WindowsService);

            return lstProNames;
        }
        /// <summary>
        /// 获取所有组合项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<CombProjectInfo> QueryProjectAndCombProName(string strDBMethod)
        {
            List<CombProjectInfo> lstProNames = myBatis.QueryProjectAndCombProName(strDBMethod);
            LogInfo.WriteProcessLog("QueryProjectAndCombProName(string strDBMethod) == " + lstProNames.Count.ToString(), Module.WindowsService);

            return lstProNames;
        }

        /// <summary>
        /// 添加组合项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public string AddCombProjectName(string strDBMethod, CombProjectInfo combProjectInfo)
        {
            return myBatis.AddCombProjectName(strDBMethod, combProjectInfo);
        }
        /// <summary>
        /// 删除组合项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfos"></param>
        /// <returns></returns>
        public string DeleteCombProjectName(string strDBMethod, List<CombProjectInfo> combProjectInfos)
        {
            return myBatis.DeleteCombProjectName(strDBMethod, combProjectInfos);
        }
        /// <summary>
        /// 更新组合项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public string UpdateCombProjectName(string strDBMethod, string combProjectInfoOld, CombProjectInfo combProInfoNew)
        {
            return myBatis.UpdateCombProjectName(strDBMethod, combProjectInfoOld, combProInfoNew);
        }
    }
}
