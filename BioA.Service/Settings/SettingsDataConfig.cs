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
        public List<string> QueryDataConfig(string strDBMethod)
        {
            List<string> lstQueryDataConfig = new List<string>();
            lstQueryDataConfig = myBatis.QueryDataConfig(strDBMethod);
            
            return lstQueryDataConfig;
        }

        public string DataConfigAdd(string strDBMethod, string dataConfig)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectDataConfig("QueryDataConfigAdd", dataConfig);
                // 当count>0代表已存在此项目
                if (count == 0)
                {
                    strInfo = myBatis.DataConfigadd(strDBMethod, dataConfig);
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

        public string UpdataDataConfig(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            return myBatis.UpdateDataConfig(strDBMethod, dataConfig, dataConfigOld);
        }



        public string DeleteDataConfig(string strDBMethod, string dataConfig)
        {
            return myBatis.DeleteDataConfig(strDBMethod, dataConfig);
        }

        public List<float> QueryDilutionRatio(string strDBMethod)
        {
            return myBatis.QueryDilutionRatio(strDBMethod);
        }

        public string DilutionRatioAdd(string strDBMethod, string dataConfig)
        {
            string strInfo = string.Empty;
            try
            {
                int count = myBatis.SelectDilutionRatio("QueryDilutionRatioAdd", dataConfig);
                // 当count>0代表已存在此项目
                if (count <= 0)
                {
                    strInfo = myBatis.DilutionRatioadd(strDBMethod, dataConfig);
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

        public string UpdataDilutionRatio(string strDBMethod, string dataConfig, string dataConfigOld)
        {
            return myBatis.UpdataDilutionRatio(strDBMethod, dataConfig, dataConfigOld);
        }

        public string DeleteDilutionRatio(string strDBMethod, string dataConfig)
        {
            return myBatis.DeleteDilutionRatio(strDBMethod, dataConfig);
        }
    }
}
