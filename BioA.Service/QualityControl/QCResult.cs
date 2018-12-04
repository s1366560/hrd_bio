using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class QCResult : DataTransmit
    {
        public List<QCResultForUIInfo> QueryQCResultInfo(string strDBMethod, QCResultForUIInfo qCResForUIInfo)
        {
            return myBatis.QueryQCResultInfo(strDBMethod, qCResForUIInfo);
        }

        public List<QualityControlInfo> QueryQCInfosForAddQCResult(string strDBMethod)
        {
            return myBatis.QueryQCInfosForAddQCResult(strDBMethod);
        }

        public List<string> QueryProjectName(string strDBMethod)
        {
            return myBatis.QueryProjectName(strDBMethod);
        }

        /// <summary>
        /// 用户手动修改质控结果信息
        /// </summary>
        /// <param name="strDBMethod">访问数据库方法名</param>
        /// <param name="qcResOldInfo">老数据</param>
        /// <param name="qcResNewInfo">新数据</param>
        /// <returns></returns>
        public string EditQCResultForManual(string strDBMethod, QCResultForUIInfo qcResOldInfo, QCResultForUIInfo qcResNewInfo)
        {
            return myBatis.EditQCResultForManual(strDBMethod, qcResOldInfo, qcResNewInfo);
        }

        public string AddQCResultForManual(string strDBMethod, QCResultForUIInfo qcResNewInfo)
        {
            return myBatis.AddQCResultForManual(strDBMethod, qcResNewInfo);
        }

        public string DeleteQCResult(string strDBMethod, QCResultForUIInfo qcResInfo)
        {
            return myBatis.DeleteQCResult(strDBMethod, qcResInfo);
        }

        public TimeCourseInfo QueryTimeCourseByQCInfo(string strDBMethod, QCResultForUIInfo qcResInfo,string dateTime)
        {
            return myBatis.QueryTimeCourseByQCInfo(strDBMethod, qcResInfo,dateTime);
        }
    }
}
