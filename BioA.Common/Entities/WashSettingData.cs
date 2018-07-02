using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class WashSettingData
    {
        public string SampleContainerType { get; set; }

        public int ACount { get; set; }
        public int ASMPPosition { get; set; }
        public float ASMPVolume { get; set; }
        public int ARGTPosition1 { get; set; }
        public int ARGTVolume1 { get; set; }
        public int ARGTPosition2 { get; set; }
        public int ARGTVolume2 { get; set; }

        public int BCount { get; set; }
        public int BSMPPosition { get; set; }
        public float BSMPVolume { get; set; }
        public int BRGTPosition1 { get; set; }
        public int BRGTVolume1 { get; set; }
        public int BRGTPosition2 { get; set; }
        public int BRGTVolume2 { get; set; }
    }
}
