using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class QCMaintian : DataTransmit
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

        public List<string> QueryQCPosition(string strDBMethod)
        {
            return myBatis.QueryQCPosition(strDBMethod);
        }

        public string AddQualityControl(string strDBMethod, QualityControlInfo qcInfo, List<QCRelationProjectInfo> lstQCRelationProInfo)
        {
            return myBatis.AddQualityControl(strDBMethod, qcInfo, lstQCRelationProInfo);

        }

        public List<QualityControlInfo> QueryQCAllInfo(string strDBMethod)
        {
            return myBatis.QueryQCAllInfo(strDBMethod);
        }

        public List<QCRelationProjectInfo> QueryRelativelyProjectByQCInfo(string strDBMethod, string QCInfo)
        {
            return myBatis.QueryRelativelyProjectByQCInfo(strDBMethod, QCInfo);
        }

        public string EditQualityControl(string strDBMethod, QualityControlInfo oldQCInfo, QualityControlInfo newQCInfo, List<QCRelationProjectInfo> lstQCRelationProInfo, List<QCRelationProjectInfo> QCRelationProInfo)
        {
            return myBatis.EditQualityControl(strDBMethod, oldQCInfo, newQCInfo, lstQCRelationProInfo, QCRelationProInfo);
        }

        public int EditQCRelateProInfo(string strDBMethod, QualityControlInfo QCInfo, List<QCRelationProjectInfo> lstQCRelationProInfo)
        {
            return myBatis.EditQCRelateProInfo(strDBMethod, QCInfo, lstQCRelationProInfo);
        }

        public int LockQualityControl(string strDBMethod, QualityControlInfo QCInfo)
        {
            return myBatis.LockQualityControl(strDBMethod, QCInfo);
        }

        public int UnLockQualityControl(string strDBMethod, QualityControlInfo QCInfo)
        {
            return myBatis.UnLockQualityControl(strDBMethod, QCInfo);
        }

        public string DeleteQualityControl(string strDBMethod, QualityControlInfo QCInfo)
        {
            return myBatis.DeleteQualityControl(strDBMethod, QCInfo);
        }
    }
}
