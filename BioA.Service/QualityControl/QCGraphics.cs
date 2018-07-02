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

        public List<QCResultForUIInfo> QueryQCResultForQCGraphics(string strDBMethod, QCResultForUIInfo qcResForUIInfo)
        {
            return myBatis.QueryQCResultForQCGraphics(strDBMethod, qcResForUIInfo);
        }
    }
}
