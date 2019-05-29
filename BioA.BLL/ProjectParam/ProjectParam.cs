using BioA.Common;
using BioA.IBLL.IProjectParam;
using BioA.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.BLL.ProjectParam
{
    public class ProjectParam : DataTransmit, IProjectParam
    {
        /// <summary>
        /// 获取生化项目参数信息
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public AssayProjectParamInfo GetProjectParamInfoByNameOfType(string projectName, string type)
        {
            return myBatis.GetAssProParamInfo("GetAssayProjectParamInfoByNameAndType",projectName,type);
        }
    }
}
