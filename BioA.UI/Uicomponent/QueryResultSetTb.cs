using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;
using BioA.Service;

namespace BioA.UI
{
    public class QueryResultSetTb
    {

        static bool b;
        public QueryResultSetTb(bool bol)
        {
            b = bol;
        }

        static List<ResultSetInfo> lstResult;
        /// <summary>
        /// 获取结果设置表信息
        /// </summary>
        public static List<ResultSetInfo> QueryResultSetInfo
        {
            get 
            {
                if (lstResult == null || lstResult.Count == 0)
                {
                    lstResult = new WorkAreaApplyTask().QueryResultSetInfos("QueryResultSetInfo");
                }
                else
                {
                    if (b == false) ;
                    else
                    {
                        lstResult = new WorkAreaApplyTask().QueryResultSetInfos("QueryResultSetInfo");
                    }
                }
                return lstResult; 
            }
        }
    }
}
