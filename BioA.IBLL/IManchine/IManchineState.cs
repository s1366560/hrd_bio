using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.IBLL.IManchine
{
    /// <summary>
    /// 机器业务处理层
    /// </summary>
    public interface IManchineState
    {
        /// <summary>
        /// 获取所有任务列的总行数
        /// </summary>
        /// <returns></returns>
        int GetAllTaskCount(string statementID);
        /// <summary>
        /// 定时获取机器是否有故障信息
        /// </summary>
        /// <returns></returns>
        bool GetManchineIsTroubleLogInfo();
        /// <summary>
        /// 获取所有任务测试次数
        /// </summary>
        /// <returns></returns>
        int GetAllTaskNumberTimes();
    }
}
