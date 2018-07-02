using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class CommunicationEntityThreeParam : CommunicationEntity
    {
        private string objLastestParam;
        /// <summary>
        /// 访问数据库参数
        /// </summary>
        public string ObjLastestParam
        {
            get { return objLastestParam; }
            set { objLastestParam = value; }
        }

        public CommunicationEntityThreeParam()
        {
            objLastestParam = string.Empty;
        }

        public CommunicationEntityThreeParam(string methodName, string sender, string lastParam)
        {
            StrmethodName = methodName;
            ObjParam = sender;
            objLastestParam = lastParam;
        }
    }
}
