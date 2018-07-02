using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 试剂状态信息
    /// </summary>
    public class ReagentStateInfo
    {
        private string reagentPos;
        private string reagentChamber;
        private string reagentName;
        private string projectName;
        private float reagentUsedVol;
        private float reagentSurplusVol;

        public string ReagentPos
        {
            get { return reagentPos; }
            set { reagentPos = value; }
        }

        public string ReagentChamber
        {
            get { return reagentChamber; }
            set { reagentChamber = value; }
        }

        public string ReagentName
        {
            get { return reagentName; }
            set { reagentName = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public float ReagentUsedVol
        {
            get { return reagentUsedVol; }
            set { reagentUsedVol = value; }
        }

        public float ReagentSurplusVol
        {
            get { return reagentSurplusVol; }
            set { reagentSurplusVol = value; }
        }
    }
}
