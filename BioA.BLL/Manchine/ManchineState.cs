using BioA.IBLL.IManchine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.BLL.Manchine
{
    public class ManchineState : IManchineState
    {
        /// <summary>
        /// 获取任务总行数
        /// </summary>
        /// <param name="statementID"></param>
        /// <returns></returns>
        public int GetAllTaskCount(string statementID)
        {
            return new MyBatis().GetAllTasksCount("GetAllTasksCount");
        }
        /// <summary>
        /// 获取机器故障信息
        /// </summary>
        /// <returns></returns>
        public bool GetManchineIsTroubleLogInfo()
        {
            return new MyBatis().TroubleLogInfo();
        }
        /// <summary>
        /// 获取所有任务测试次数
        /// </summary>
        /// <returns></returns>
        public int GetAllTaskNumberTimes()
        {
            return new MyBatis().getFinishTime();
        }
    }
}
