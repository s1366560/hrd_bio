using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 组合项目信息
    /// </summary>
    public class CombProjectInfo
    {
        public CombProjectInfo()
        {
            combProjectName = string.Empty;
            projectNames = new List<string>();
            remarks = string.Empty;
            combProjectCount = 0;
        }

        private string combProjectName;
        private List<string> projectNames;
        private string remarks;
        private int combProjectCount;

        public string CombProjectName
        {
            get { return combProjectName; }
            set { combProjectName = value; }
        }

        public List<string> ProjectNames
        {
            get { return projectNames; }
            set { projectNames = value; }
        }
        
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        public int CombProjectCount
        {
            get { return combProjectCount; }
            set { combProjectCount = value; }
        }
    }
}
