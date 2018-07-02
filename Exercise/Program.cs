using BioA.Common;
using BioA.SqlMaps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBatis myBatis = new MyBatis();
            //Hashtable hashTable = new Hashtable();
            //hashTable.Add("proName", "1");
            //hashTable.Add("proType", "2");
            //myBatis.ism_SqlMap.QueryForObject("AssayProjectInfo.DeleteAssayProject", null);



    //        <statement id="QueryQCResultForQCGraphics" parameterClass="QCResultForUIInfo" resultClass="QCResultForUIInfo">
    //  select t2.HorizonLevel,t1.StartTime,t1.ConcResult,t3.TargetMean,t3.TargetSD 
    //      from QualityControlResultTb t1 
    //        left join QualityControlTb t2 on t1.QCID = t2.QCID 
    //        left join QCRelationProjectTb t3 on t1.QCID = t3.QCID and t1.ProjectName = t3.ProjectName and t1.SampleType = t3.SampleType
    //        where t2.QCName=#QCName# and t3.ProjectName=#ProjectName# and t2.LotNum=#LotNum# and t1.IsLogicalDelete=0 and t1.IsLogicalEdit=0 and t1.StartTime between #QCTimeStartTS# and #QCTimeEndTS#
    //  union
    //  select t2.HorizonLevel,t1.StartTime,t1.ConcResult,t3.TargetMean,t3TargetSD 
    //      from QualityControlResultForUserTb t1 
    //        left join QualityControlTb t2 on t1.QCID = t2.QCID 
    //        left join QCRelationProjectTb t3 on t1.QCID = t3.QCID and t1.ProjectName = t3.ProjectName and t1.SampleType = t3.SampleType      
    //        where t2.QCName=#QCName# and t3.ProjectName=#ProjectName# and t2.LotNum=#LotNum# and t1.IsLogicalDelete=0 and t1.IsLogicalEdit=0 and t1.StartTime between #QCTimeStartTS# and #QCTimeEndTS#
    //</statement>
        }
    }
}
