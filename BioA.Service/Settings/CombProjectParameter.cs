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
            LogInfo.WriteProcessLog("public List<string> QueryProjectByCombProName(string strDBMethod, string CombProName) == " + lstProNames.Count.ToString(), Module.WindowsService);

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
            string strResult = string.Empty;

            int intResult = myBatis.CombProjectCountByCombProName("CombProjectCountByCombProName", combProjectInfo.CombProjectName);

            if (intResult == 0)
            {
                myBatis.AddCombProjectName(strDBMethod, combProjectInfo);
                intResult = myBatis.CombProjectCountByCombProName("CombProjectCountByCombProName", combProjectInfo.CombProjectName);
                if (intResult == 0)
                {
                    strResult = "添加失败";
                }
                else
                {
                    strResult = "添加成功";
                }
            }
            else
            {
                strResult = "项目已存在！";
            }
            LogInfo.WriteProcessLog("public string AddCombProject(string strDBMethod, CombProjectInfo combProjectInfo) == " + strResult.ToString(), Module.WindowsService);

            return strResult;
        }
        /// <summary>
        /// 删除组合项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfos"></param>
        /// <returns></returns>
        public int DeleteCombProjectName(string strDBMethod, List<CombProjectInfo> combProjectInfos)
        {
            int intResult = myBatis.DeleteCombProjectName(strDBMethod, combProjectInfos);
            LogInfo.WriteProcessLog("public int DeleteCombProject(string strDBMethod, List<CombProjectInfo> combProjectInfos) == " + intResult.ToString(), Module.WindowsService);

            return intResult;
        }
        /// <summary>
        /// 更新组合项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public int UpdateCombProjectName(string strDBMethod, CombProjectInfo combProjectInfoOld, CombProjectInfo combProInfoNew)
        {
            int intResult = myBatis.UpdateCombProjectName(strDBMethod, combProjectInfoOld, combProInfoNew);
            LogInfo.WriteProcessLog("public int UpdateCombProject(string strDBMethod, CombProjectInfo combProjectInfoOld, CombProjectInfo combProInfoNew) == " + intResult.ToString(), Module.WindowsService);

            return intResult;
        }
    }
}
