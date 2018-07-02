using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BioA.SqlMaps
{
    public class Class1
    {
        public string da = null;
        public Class1()
        {
            //string fileName = "sqlMap.config";
            //DomSqlMapBuilder builder = new DomSqlMapBuilder();
            //ISqlMapper mapper = builder.Configure(fileName);


            Assembly assembly = Assembly.Load("BioA.SqlMaps");
            Stream stream = assembly.GetManifestResourceStream("BioA.SqlMaps.SqlMap.config");

            DomSqlMapBuilder builder = new DomSqlMapBuilder();
            ISqlMapper mapper = builder.Configure(stream);

   
        }
    }
}
