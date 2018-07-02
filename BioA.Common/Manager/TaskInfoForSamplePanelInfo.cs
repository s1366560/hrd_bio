using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class TaskInfoForSamplePanelInfo
    {
        public TaskInfoForSamplePanelInfo()
        {
            sampleNum = 0;
            sampleType = string.Empty;
            sampleState = 0;
            inspectInfos = string.Empty;
            samplePos = string.Empty;
        }

        private int sampleNum;
        private string sampleType;
        private int sampleState;
        private string inspectInfos;
        private string samplePos;

        public int SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }

        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }

        public int SampleState
        {
            get { return sampleState; }
            set { sampleState = value; }
        }

        public string InspectInfos
        {
            get { return inspectInfos; }
            set { inspectInfos = value; }
        }

        public string SamplePos
        {
            get { return samplePos; }
            set { samplePos = value; }
        }
    }
}
