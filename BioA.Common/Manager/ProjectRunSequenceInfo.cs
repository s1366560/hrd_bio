using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class ProjectRunSequenceInfo
    {
        public ProjectRunSequenceInfo()
        {
            projectName = "";
            sampleType = "";
            runSequence = -1;
        }

        private string projectName;
        private string sampleType;
        private int runSequence;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }
        /// <summary>
        /// 检测序号
        /// </summary>
        public int RunSequence
        {
            get { return runSequence; }
            set { runSequence = value; }
        }
    }
}
