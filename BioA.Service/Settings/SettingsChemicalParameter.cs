using BioA.Common;
using System;
using System.Collections;
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

            return lstAssayProInfos;
        }


        public List<LISCommunicateNetworkInfo> QueryLISCommunicateInfo(string strDBMethod)
        {
            List<LISCommunicateNetworkInfo> lstLISCommunicateInfo = myBatis.QueryLISCommunicateInfo(strDBMethod);

            return lstLISCommunicateInfo;
        }

        public List<SerialCommunicationInfo> QuerySerialCommunicationInfo(string strDBMethod)
        {
            List<SerialCommunicationInfo> serialCommunicationInfo = myBatis.QuerySerialCommunicationInfo(strDBMethod);

            return serialCommunicationInfo;
        }

        /// <summary>
        /// 添加生化项目访问数据库
        /// </summary>
        /// <param name="strDBMethod">方法名</param>
        /// <param name="assayProInfo">生化项目参数</param>
        public AssayProjectParamInfo AddAssayProject(string strDBMethod, AssayProjectInfo assayProInfo)
        {
            return myBatis.AddAssayProject(strDBMethod, assayProInfo);
        }

        /// <summary>
        /// 获取生化项目参数信息
        /// </summary>
        /// <param name="strDBMethod">方法名</param>
        /// <param name="assayProInfo">生化项目参数</param>
        public List<AssayProjectParamInfo> QueryAssayProjectParamInfoAll(string strDBMethod)
        {
            List<AssayProjectParamInfo> lstAssayProInfos = myBatis.QueryAssayProjectParamInfoAll(strDBMethod);
            //LogInfo.WriteProcessLog("获取" + lstAssayProInfos.ProjectName + "+" + lstAssayProInfos.SampleType + "项目参数成功", Module.WindowsService);

            return lstAssayProInfos;
        }

        /// <summary>
        /// 获取结果单位
        /// </summary>
        /// <param name="strDBMethod">方法名</param>
        /// <param name="sender">生化项目参数</param>
        public List<string> QueryProjectResultUnits(string strDBMethod)
        {
            List<string> lstUnits = myBatis.QueryProjectResultUnits(strDBMethod);

            return lstUnits;
        }

        /// <summary>
        /// 根据项目名称更新改项目参数信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="strProName"></param>
        public int UpdateAssayProjectParamInfo(string strDBMethod, AssayProjectParamInfo assayProParamInfo)
        {
            return myBatis.UpdateAssayProjectParamInfo(strDBMethod, assayProParamInfo);
             
        }
        /// <summary>
        /// 编辑生化项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfo1"></param>
        /// <param name="assayProInfo2"></param>
        public int EditAssayProject(string strDBMethod, AssayProjectInfo assayProInfoOld, AssayProjectInfo assayProInfo2)
        {

            int Updatecount = 0;
            Updatecount = myBatis.UpdateAssayProCountByNameAndType(strDBMethod, assayProInfoOld, assayProInfo2);
            
            //LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            

            return Updatecount;

        }
        /// <summary>
        /// 删除生化项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="assayProInfos"></param>
        /// <returns>返回删除条数</returns>
        public int AssayProjectDelete(string strDBMethod, AssayProjectInfo assayProInfos)
        {

            return myBatis.DeleteAssayProCountByNameAndType(strDBMethod, assayProInfos);
        }
        /// <summary>
        /// 获取项目所有校准参数
        /// </summary>
        /// <returns></returns>
        public List<AssayProjectCalibrationParamInfo> QueryCalibParamInfoAll(string strDBMethod)
        {
            return myBatis.QueryCalibParamInfoAll(strDBMethod);
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
        /// <summary>
        /// 获取所有生化项目信息，包括项目和计算项目
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="ObjParam"></param>
        /// <returns></returns>
        public List<string> QueryAssayProAllInfoByDistinctProName(string strDBMethod)
        {
            List<string> assayProInfos = new List<string>();
            assayProInfos = myBatis.QueryAssayProAllInfoByDistinctProName(strDBMethod);

            return assayProInfos;
        }

        public int NetworkUpDate(string strDBMethod, LISCommunicateNetworkInfo lISCommunicateInfo)
        {
            return myBatis.UpdateLISCommunicateNetworkInfo(strDBMethod, lISCommunicateInfo);
        }

        public int SerialUpDate(string strDBMethod, SerialCommunicationInfo serialCommunicationInfo)
        {
            return myBatis.UpdateLISCommunicateSerialInfo(strDBMethod, serialCommunicationInfo);
        }

        public List<CalibratorProjectinfo> QueryCalibratorProjectinfo(string strDBMethod, string ProjectName, string SampleType)
        {
           
            return myBatis.QueryCalibratorProinfo(strDBMethod, ProjectName ,SampleType);
        }

        public List<Calibratorinfo> QueryCalib(string strDBMethod, string p2)
        {
            List<Calibratorinfo> lstQueryCalib = new List<Calibratorinfo>();
            lstQueryCalib = myBatis.QueryCalib(strDBMethod, p2);
            return lstQueryCalib;
        }

        public string AddCalibrationCurveInfo(string strDBMethod, List<CalibrationCurveInfo> calibrationCurveInfo)
        {
            string str = myBatis.DeleteCalibrationCurveInfo("DeleteCalibrationCurveInfo", calibrationCurveInfo);
           
              return  myBatis.AddCalibrationCurveInfo(strDBMethod, calibrationCurveInfo);

            
        }

        public List<CalibrationCurveInfo> QueryCalibrationCurveInfo(string strDBMethod, string p2)
        {
            List<CalibrationCurveInfo> lstQueryCalib = new List<CalibrationCurveInfo>();
            try
            {
                lstQueryCalib = myBatis.QueryCalibrationCurve(strDBMethod, p2);

            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryCalib;
        }
    }
}
