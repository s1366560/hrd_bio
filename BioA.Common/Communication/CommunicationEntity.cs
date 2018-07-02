using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 传输过程中为一个方法名、一个参数实体类
    /// </summary>
    public class CommunicationEntity
    {
        public CommunicationEntity()
        {
            strMethodName = string.Empty;
            objParam = string.Empty;
        }

        public CommunicationEntity(string methodName, string sender)
        {
            strMethodName = methodName;
            objParam = sender;
        }

        private string strMethodName;
        /// <summary>
        /// 访问数据库方法名
        /// </summary>
        public string StrmethodName
        {
            get { return strMethodName; }
            set { strMethodName = value; }
        }

        private string objParam;
        /// <summary>
        /// 访问数据库参数
        /// </summary>
        public string ObjParam
        {
            get { return objParam; }
            set { objParam = value; }
        }
    }
}
