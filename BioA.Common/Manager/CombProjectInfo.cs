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
        private string projectName;
        /// <summary>
        /// 组合项目名称
        /// </summary>
        public string CombProjectName
        {
            get { return combProjectName; }
            set { combProjectName = value; }
        }
        /// <summary>
        /// 集合项目名称
        /// </summary>
        public List<string> ProjectNames
        {
            get { return projectNames; }
            set { projectNames = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        /// <summary>
        /// 组合项目对应项目的个数
        /// </summary>
        public int CombProjectCount
        {
            get { return combProjectCount; }
            set { combProjectCount = value; }
        }
    }
}
