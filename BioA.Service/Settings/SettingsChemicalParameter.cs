using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    /// <summary>
    /// 设置——生化参数 数据库交互类
    /// </summary>
    public class SettingsChemicalParameter : DataTransmit
    {
        /// <summary>
        /// 获取所有生化项目访问数据库
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        public List<AssayProjectInfo> QueryAssayProAllInfo(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            List<AssayProjectInfo> lstAssayProInfos = myBatis.QueryAssayProAllInfo(strDBMethod, assayProInfo);
            LogInfo.WriteProcessLog(lstAssayProInfos.Count.ToString(), Module.WindowsService);

            return lstAssayProInfos;
        }

        /// <summary>
        /// 添加生化项目访问数据库
        /// </summary>
        /// <param name="strDBMethod">方法名</param>
        /// <param name="assayProInfo">生化项目参数</param>
        public string AddAssayProject(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectAssayProCountByNameAndType("SelectAssayProCountByPrimarykey", assayProInfo);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.AddAssayProject(strDBMethod, assayProInfo);
                    count = myBatis.SelectAssayProCountByNameAndType("SelectAssayProCountByPrimarykey", assayProInfo);
                    if (count > 0)
                    {
                        strInfo = "项目创建成功！";
                    }
                    else
                    {
                        strInfo = "项目创建失败，请联系管理员！";
                    }
                }
                else
                {
                    strInfo = "该项目已存在，请重新录入。";
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("AssayProDataTrans.cs_AddAssayProject(string strDBMethod, AssayProjectInfo assayProInfo)==" + e.ToString(), Module.WindowsService);
            }

            return strInfo;
        }

        /// <summary>
        /// 获取生化项目参数信息
        /// </summary>
        /// <param name="strDBMethod">方法名</param>
        /// <param name="assayProInfo">生化项目参数</param>
        public AssayProjectParamInfo GetAssayProjectParamInfoByNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            AssayProjectParamInfo lstAssayProInfos = myBatis.GetAssayProjectParamInfoByNameAndType(strDBMethod, assayProInfo);
            LogInfo.WriteProcessLog("获取" + lstAssayProInfos.ProjectName + "+" + lstAssayProInfos.SampleType + "项目参数成功", Module.WindowsService);

            return lstAssayProInfos;
        }

        /// <summary>
        /// 获取结果单位
        /// </summary>
        /// <param name="strDBMethod">方法名</param>
        /// <param name="sender">生化项目参数</param>
        public List<string> QueryProjectResultUnits(string strDBMethod, object sender)
        {
            List<string> lstUnits = myBatis.QueryProjectResultUnits(strDBMethod, sender);

            return lstUnits;
        }

        /// <summary>
        /// 通过项目名称更新试剂信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="strProName"></param>
        public int UpdateAssayProjectParamInfo(string strDBMethod, AssayProjectParamInfo assayProParamInfo)
        {
            int intResult = 0;
            try
            {
                intResult = myBatis.UpdateAssayProjectParamInfo(strDBMethod, assayProParamInfo);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }

            return intResult;
        }
        /// <summary>
        /// 编辑生化项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo1"></param>
        /// <param name="assayProInfo2"></param>
        public int EditAssayProject(string strDBMethod, AssayProjectInfo assayProInfo1, AssayProjectInfo assayProInfo2)
        {

            int Updatecount = 0;
            try
            {
                Updatecount = myBatis.UpdateAssayProCountByNameAndType(strDBMethod, assayProInfo1, assayProInfo2);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }

            return Updatecount;

        }
        /// <summary>
        /// 删除生化项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfos"></param>
        /// <returns>返回删除条数</returns>
        public int QueryAssayProDelete(string strDBMethod, List<AssayProjectInfo> assayProInfos)
        {

            return myBatis.DeleteAssayProCountByNameAndType(strDBMethod, assayProInfos);
        }
        /// <summary>
        /// 通过项目名称和项目类型获取项目校准参数
        /// </summary>
        /// <returns></returns>
        public AssayProjectCalibrationParamInfo QueryCalibParamByProNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            return myBatis.QueryCalibParamByProNameAndType(strDBMethod, assayProInfo);
        }

        /// <summary>
        /// 通过项目名称和项目类型更新项目校准参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public int UpdateCalibParamByProNameAndType(string strDBMethod, AssayProjectCalibrationParamInfo assayProInfo)
        {
            return myBatis.UpdateCalibParamByProNameAndType(strDBMethod, assayProInfo);
        }

        /// <summary>
        /// 通过项目名称和项目类型获取项目范围参数
        /// </summary>
        /// <returns></returns>
        public AssayProjectRangeParamInfo QueryRangeParamByProNameAndType(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            return myBatis.QueryRangeParamByProNameAndType(strDBMethod, assayProInfo);
        }

        /// <summary>
        /// 通过项目名称和项目类型更新项目范围参数
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo"></param>
        /// <returns></returns>
        public int UpdateRangeParamByProNameAndType(string strDBMethod, AssayProjectRangeParamInfo assayProInfo)
        {
            return myBatis.UpdateRangeParamByProNameAndType(strDBMethod, assayProInfo);
        }

        public List<AssayProjectInfo> QueryAssayProAllInfoByDistinctProName(string strDBMethod, object ObjParam)
        {
            List<AssayProjectInfo> assayProInfos = new List<AssayProjectInfo>();
            assayProInfos = myBatis.QueryAssayProAllInfoByDistinctProName(strDBMethod, ObjParam);

            return assayProInfos;
        }
    }
}
