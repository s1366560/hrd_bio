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
            objLastestParam = "";
        }

        public CommunicationEntity(string methodName)
        {
            strMethodName = methodName;
            objParam = "";
            objLastestParam = "";
        }


        public CommunicationEntity(string methodName, string sender)
        {
            strMethodName = methodName;
            objParam = sender;
            objLastestParam = "";
        }

        public CommunicationEntity(string methodName, string sender, string lastestSender)
        {
            strMethodName = methodName;
            objParam = sender;
            objLastestParam = lastestSender;
        }

        public CommunicationEntity(string methodName, string sender, string lastestSender, string thridSender, string fourthSender)
        {
            strMethodName = methodName;
            objParam = sender;
            objLastestParam = lastestSender;
            objThirdParam = thridSender;
            objFourthParam = fourthSender;
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

        private string objLastestParam;
        /// <summary>
        /// 访问数据库参数
        /// </summary>
        public string ObjLastestParam
        {
            get { return objLastestParam; }
            set { objLastestParam = value; }
        }

        private string objThirdParam;
        /// <summary>
        /// 访问数据库参数
        /// </summary>
        public string ObjThirdParam
        {
            get { return objThirdParam; }
            set { objThirdParam = value; }
        }

        private string objFourthParam;
        /// <summary>
        /// 访问数据库参数
        /// </summary>
        public string ObjFourthParam
        {
            get { return objFourthParam; }
            set { objFourthParam = value; }
        }
    }
}
