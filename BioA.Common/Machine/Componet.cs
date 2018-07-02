using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Machine
{
    public class Componet
    {
        public string Name { get; set; }
        public List<Command> CommandList { get; set; }

        public bool IsAdjust { get; set; }

        public string Flag { get; set; }

    }
}
