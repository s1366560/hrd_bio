using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class QCGraphics : DataTransmit
    {
        public List<string> QueryProjectName(string strDBMethod)
        {
            return myBatis.QueryProjectName(strDBMethod);
        }

        public List<QualityControlInfo> QueryQCAllInfo(string strDBMethod)
        {
            return myBatis.QueryQCAllInfo(strDBMethod);
        }

        /// <summary>
        /// 获取质控项目信息
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <returns></returns>
        public List<QCRelationProjectInfo> GetQCRelationProjectInfo(string strDBMethod)
        {
            return myBatis.GetQCRelationProjectInfo(strDBMethod);
        }
        /// <summary>
        /// 获取质控图结果数据
        /// </summary>
        /// <param name="strDBMethod"></param>
        /// <param name="qcResForUIInfo"></param>
        /// <returns></returns>
        public List<QCResultForUIInfo> QueryQCResultForQCGraphics(string strDBMethod, QCResultForUIInfo qcResForUIInfo)
        {
            return myBatis.QueryQCResultForQCGraphics(strDBMethod, qcResForUIInfo);
        }
    }
}
