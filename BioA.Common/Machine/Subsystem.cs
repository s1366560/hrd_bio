using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Machine
{
    public class Subsystem
    {
        public bool IsDynLoad { get; set; }
        public bool IsNavi { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }
        public List<Componet> ComponetList { get; set; }
    }
}
