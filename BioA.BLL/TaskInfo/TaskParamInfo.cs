using BioA.IBLL.ITaskInfo;
using BioA.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.BLL.TaskInfo
{
    public class TaskParamInfo : DataTransmit, ITaskParamInfo
    {
        public string ReviewProject(string strMethodName, Common.TaskInfo task)
        {
            return myBatis.ReviewCheck(strMethodName, task);
        }
    }
}
