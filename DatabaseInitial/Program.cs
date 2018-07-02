using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.SqlMaps;

namespace DatabaseInitial
{
    /// <summary>
    /// 数据库创建以及插入初始数据
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MyBatis myBatis = new MyBatis();
            myBatis.CreateTables();
        }
    }
}
