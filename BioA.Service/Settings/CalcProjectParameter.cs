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
            return myBatis.QueryCalcProjectAllInfo(strDBMethod);
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
                strResult = myBatis.AddCalcProject(strDBMethod, calcProjectInfo);
            }
            else
            {
                strResult = "该项目名称已存在！";
            }
            return strResult;
        }
        /// <summary>
        /// 删除计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfos"></param>
        /// <returns></returns>
        public string DeleteCalcProject(string strDBMethod, List<CalcProjectInfo> calcProjectInfos)
        {
            return myBatis.DeleteCalcProject(strDBMethod, calcProjectInfos);
        }
        /// <summary>
        /// 更新计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="combProjectInfo"></param>
        /// <returns></returns>
        public string UpdateCalcProject(string strDBMethod, CalcProjectInfo calcProjectInfoOld, CalcProjectInfo calcProInfoNew)
        {
            return myBatis.UpdateCalcProject(strDBMethod, calcProjectInfoOld, calcProInfoNew);
        }

        /// <summary>
        /// 获取所有生化项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="ObjParam"></param>
        /// <returns></returns>
        public List<string> ProjectPageinfoForCalc(string strDBMethod, string sampleType)
        {
            return myBatis.ProjectPageinfoForCalc(strDBMethod, sampleType);
        }
    }
}
