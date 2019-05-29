using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.IBLL.IProjectParam
{
    public interface IProjectParam
    {
        AssayProjectParamInfo GetProjectParamInfoByNameOfType(string projectName,string type);
    }
}
