using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    /// <summary>
    /// 设置——计算项目 数据库交互类
    /// </summary>
    public class CalcProjectParameter : DataTransmit
    {
        /// <summary>
        /// 获取所有计算项目访问数据库
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        public List<CalcProjectInfo> QueryCalcProjectAllInfo(string strDBMethod)
        {
            List<CalcProjectInfo> lstCalcProInfos = myBatis.QueryCalcProjectAllInfo(strDBMethod);
            LogInfo.WriteProcessLog("public List<CalcProjectInfo> QueryCalcProjectAllInfo(string strDBMethod) == " + lstCalcProInfos.Count.ToString(), Module.WindowsService);

            return lstCalcProInfos;
        }

        /// <summary>
        /// 添加计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public string AddCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfo)
        {
            string strResult = string.Empty;

            int intResult = myBatis.ProjectCountByCalcProName("ProjectCountByCalcProName", calcProjectInfo);

            if (intResult == 0)
            {
                myBatis.AddCalcProject(strDBMethod, calcProjectInfo);
                if (intResult == 0)
                {
                    strResult = "计算项目添加失败";
                }
                else
                {
                    strResult = "计算项目添加成功";
                }
            }
            else
            {
                strResult = "该项目名称已存在！";
            }
            LogInfo.WriteProcessLog("public string AddCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfo) == " + strResult.ToString(), Module.WindowsService);

            return strResult;
        }
        /// <summary>
        /// 删除计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfos"></param>
        /// <returns></returns>
        public int DeleteCalcProject(string strDBMethod, List<CalcProjectInfo> calcProjectInfos)
        {
            int intResult = myBatis.DeleteCalcProject(strDBMethod, calcProjectInfos);
            LogInfo.WriteProcessLog("public int DeleteCalcProject(string strDBMethod, List<CalcProjectInfo> calcProjectInfos) == " + intResult.ToString(), Module.WindowsService);

            return intResult;
        }
        /// <summary>
        /// 更新计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public int UpdateCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfoOld, CalcProjectInfo calcProInfoNew)
        {
            int intResult = myBatis.UpdateCalcProject(strDBMethod, calcProjectInfoOld, calcProInfoNew);
            LogInfo.WriteProcessLog("public int UpdateCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfoOld, CalcProjectInfo calcProInfoNew) == " + intResult.ToString(), Module.WindowsService);

            return intResult;
        }

        /// <summary>
        /// 获取所有生化项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="ObjParam"></param>
        /// <returns></returns>
        public List<string> ProjectPageinfoForCalc(string strDBMethod, string sampleType)
        {
            List<string> assayProInfos = new List<string>();
            assayProInfos = myBatis.ProjectPageinfoForCalc(strDBMethod, sampleType);

            return assayProInfos;
        }
    }
}
