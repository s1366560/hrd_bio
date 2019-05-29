using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.IBLL.ITaskInfo
{
    public interface ITaskParamInfo
    {
        string ReviewProject(string strMethodName, TaskInfo task);
    }
}
