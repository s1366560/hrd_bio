using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    class SettingsDataConfig : DataTransmit
    {
        public List<string> QueryDataConfig(string strDBMethod, string dataConfig)
        {
            List<string> lstQueryDataConfig = new List<string>();
            try
            {
                lstQueryDataConfig = myBatis.QueryDataConfig(strDBMethod, dataConfig);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryDataConfig;
        }

        internal string DataConfigAdd(string strDBMethod, string dataConfig)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectDataConfig("QueryDataConfigAdd", dataConfig);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.DataConfigadd(strDBMethod, dataConfig);
                    count = myBatis.SelectDataConfig("QueryDataConfigAdd", dataConfig);
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
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }

            return strInfo;
        }

        internal int UpdataDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            return myBatis.UpdateDataConfig(strDBMethod, dataConfig, dataConfigOld);
        }



        internal int DeleteDataConfig(string strDBMethod, string dataConfig)
        {
            return myBatis.DeleteDataConfig(strDBMethod, dataConfig);
        }

        internal List<string> QueryDilutionRatio(string strDBMethod, string dataConfig)
        {
            List<string> lstQueryDilutionRatio = new List<string>();
            try
            {
                lstQueryDilutionRatio = myBatis.QueryDilutionRatio(strDBMethod, null);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog(e.ToString(), Module.WindowsService);
            }
            return lstQueryDilutionRatio;
        }

        internal string DilutionRatioAdd(string strDBMethod, string dataConfig)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectDilutionRatio("QueryDilutionRatioAdd", dataConfig);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    myBatis.DilutionRatioadd(strDBMethod, dataConfig);
                    count = myBatis.SelectDilutionRatio("QueryDilutionRatioAdd", dataConfig);
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
                LogInfo.WriteErrorLog("DataConfigAdd(string strDBMethod, string dataConfig)==" + e.ToString(), Module.WindowsService);
            }

            return strInfo;
        }

        internal int UpdataDilutionRatio(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            return myBatis.UpdataDilutionRatio(strDBMethod, dataConfig, dataConfigOld);
        }

        internal int DeleteDilutionRatio(string strDBMethod, string dataConfig)
        {
            return myBatis.DeleteDilutionRatio(strDBMethod, dataConfig);
        }
    }
}
