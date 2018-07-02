using BioA.Common.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BioA.Common.Machine
{
    public class CommandService
    {
        public static  Command GetCommand(string str)
        {
            try
            {
                return (Command)XmlUtility.Deserialize(typeof(Command), str);
            }
            catch
            {
                return null;
            }
        }
        public static MachineState GetMachineState(string str)
        {
            return (MachineState)XmlUtility.Deserialize(typeof(MachineState), str);
        }

    }
}
