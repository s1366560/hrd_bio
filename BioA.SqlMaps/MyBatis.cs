    // ================================================================================================
//
// 文件名（File Name）：              MyBatis.cs
//
// 功能描述（Description）：          加载sqlmap.config，创建ISqlMapper对象，可以使用ibatis
//
// 数据表（Tables）：                 none
//
// 作者（Author）：                   冯旗
//
// 日期（Create Date）：              2017-6-21
//
// 修改记录（Revision History）：
//      R1:
//          修改人：
//          修改日期：
//          修改理由：
//
// ================================================================================================
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BioA.Common;
using IBatisNet.Common.Utilities;

namespace BioA.SqlMaps
{
    public partial class MyBatis
    {
        /// <summary>
        /// 访问数据库对象
        /// </summary>
        //public ISqlMapper ism_SqlMap;
        public MyBatis()
        {

        }

        private static readonly object SqlMapperLock = new object();
        /// <summary>
        /// 全局唯一静态，重用这个变量
        /// </summary>
        private static volatile ISqlMapper _ISqlMapper;
        /// <summary>
        /// 公开的静态方法提供对象实例
        /// </summary>
        /// <returns></returns>
        public static ISqlMapper ISqlMapper()
        {
            try
            {
                if (_ISqlMapper == null)
                {
                    lock (SqlMapperLock)
                    {
                        if (_ISqlMapper == null)
                        {
                            // 加载sqlmap.config文件
                            Assembly assembly = Assembly.Load("BioA.SqlMaps");
                            Stream stream = assembly.GetManifestResourceStream("BioA.SqlMaps.SqlMap.config");

                            // 初始化访问数据库对象
                            DomSqlMapBuilder builder = new DomSqlMapBuilder();
                            _ISqlMapper = builder.Configure(stream);
                            //CreateTables();
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("MyBatis.cs_MyBatis()==" + e.ToString(), Common.Module.DAO);
            }
            return _ISqlMapper;
        }

        public void CreateTables()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"..\..\..\BioA.SqlMaps\CreateDatabase\TableList.xml");

            XmlNode xmlNode = doc.SelectSingleNode("TableList").SelectSingleNode("statements");//.SelectSingleNode("statements")

            XmlNodeList xmlList = xmlNode.ChildNodes;

            try
            {
                foreach (XmlNode xn in xmlList)
                {
                    string strTableName = xn.Attributes["id"].Value;
                    object i = ism_SqlMap.QueryForObject("CreateTables.SelectTableInfo", strTableName.Substring(6));
                    if ((int)i <= 0)
                        ism_SqlMap.Update("CreateTables." + strTableName, null);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("MyBatis.cs_CreateTables()==" + e.ToString(), Common.Module.DAO);
            }
        }

        public void InitialDatabase()
        {
            try
            {
                int i = (int)ism_SqlMap.QueryForObject("InitialDatabase.GetEnvironmentParamTbCount", null);

                if (i == 0)
                {
                    ism_SqlMap.Insert("InitialDatabase.InitialEnvironmentParamTb", null);
                }
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("MyBatis.cs_InitialDatabase()==" + e.ToString(), Common.Module.DAO);
            }
        }
    }
}
