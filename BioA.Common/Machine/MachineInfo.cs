using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using BioA.Common.IO;

namespace BioA.Common.Machine
{
    public class MachineInfo
    {
        static string _Type;
        public static string Type
        {
            get
            {
                if (string.IsNullOrEmpty(_Type) || string.IsNullOrWhiteSpace(_Type))
                {
                    _Type = XMLHelper.Read(MachineNode, "Type");
                }

                return _Type.Trim();
            }
        }
        static string _FullName;
        public static string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_FullName) || string.IsNullOrWhiteSpace(_FullName))
                {
                    _FullName = XMLHelper.Read(MachineNode, "FullName");
                }

                return _FullName.Trim();
            }
        }

        static XmlNode _MachineNode;
        public static XmlNode MachineNode
        {
            get
            {
                if (_MachineNode == null)
                {
                    _MachineNode = XMLHelper.GetNode(MachineFile, "Machine");
                }
                return _MachineNode;
            }
        }

        static string _MachineFile;
        public static string MachineFile
        {
            get
            {
                if (string.IsNullOrEmpty(_MachineFile) || string.IsNullOrWhiteSpace(_MachineFile))
                {
                    _MachineFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Machine.xml";
                    
                }

                return _MachineFile;
            }
        }


        static List<Subsystem> _SubsystemList = null;
        public static List<Subsystem> SubsystemList
        {
            get
            {
                if (_SubsystemList == null)
                {
                    _SubsystemList = new List<Subsystem>();
                    XmlNodeList subsystemNodes = XMLHelper.GetNode(MachineNode, "MachineCommand").SelectNodes("Subsystem");
                    foreach (XmlElement element in subsystemNodes)
                    {
                        _SubsystemList.Add(GetSubsystem(element));
                    }
                }
                return _SubsystemList;
            }
        }

        public static Componet GetComponet(XmlNode compoentNode)
        {
            Componet c = new Componet();
            try
            {
                c.Name = XMLHelper.Read(compoentNode, "Name");
                string IsAdjuststr = XMLHelper.Read(compoentNode, "IsAdjust");
                if (string.IsNullOrEmpty(IsAdjuststr) || string.IsNullOrWhiteSpace(IsAdjuststr))
                {
                    c.IsAdjust = false;
                }
                else
                {
                    c.IsAdjust = bool.Parse(IsAdjuststr);
                }

                c.Flag = XMLHelper.Read(compoentNode, "Flag");

                List<Command> CommandList = new List<Command>();
                XmlNodeList nodes = compoentNode.SelectNodes("Command");
                foreach (XmlElement element in nodes)
                {
                    Command C = new Command();
                    C.Name = XMLHelper.Read(element, "Name");
                    C.FullName = XMLHelper.Read(element, "FullName");
                    string IsAdjustID = XMLHelper.Read(element, "AdjustID");
                    if (string.IsNullOrEmpty(IsAdjustID) || string.IsNullOrWhiteSpace(IsAdjustID))
                    {
                        C.AdjustID = 0;
                    }
                    else
                    {
                        C.AdjustID = int.Parse(IsAdjustID);
                    }
                    CommandList.Add(C);
                }

                c.CommandList = CommandList;
            }
            catch(Exception ex)
            {
                LogInfo.WriteErrorLog("Class MachineInfo{ public static Componet GetComponet(XmlNode compoentNode)}" + ex.ToString(), Module.Common );
            }
            return c;
        }
        public static Subsystem GetSubsystem(XmlNode subsystemNode)
        {
            Subsystem s = new Subsystem();
            try
            {
                s.Name = XMLHelper.Read(subsystemNode, "Name");
                s.ID = XMLHelper.Read(subsystemNode, "SID");
                s.IsNavi = XMLHelper.Read(subsystemNode, "DebugNavi") != "" ? bool.Parse(XMLHelper.Read(subsystemNode, "DebugNavi")) : false;
                s.IsDynLoad = XMLHelper.Read(subsystemNode, "DynicLoad") != "" ? bool.Parse(XMLHelper.Read(subsystemNode, "DynicLoad")) : false;
                List<Componet> ComponetList = new List<Componet>();
                XmlNodeList nodes = subsystemNode.SelectNodes("Componet");
                foreach (XmlElement element in nodes)
                {
                    Componet c = GetComponet(element);
                    ComponetList.Add(c);
                }
                s.ComponetList = ComponetList;
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_Read(XmlNode subsystemNode)==" + ex.ToString(), Module.Common);
            }
            return s;
        }

        public static Command GetCommandByName(string cmdName)
        {
            foreach (Subsystem s in SubsystemList)
            {
                foreach (Componet c in s.ComponetList)
                {
                    foreach (Command cmd in c.CommandList)
                    {
                        if (cmd.Name == cmdName)
                        {
                            return cmd;
                        }
                    }
                }
            }
            return null;
        }
        public static Command GetCommandByFullName(string subName, string compName,string commandName)
        {
            foreach (Subsystem s in SubsystemList)
            {
                if (subName == s.Name)
                {
                    foreach (Componet c in s.ComponetList)
                    {
                        if (compName == c.Name)
                        {
                            foreach (Command cmd in c.CommandList)
                            {
                                if (cmd.Name == commandName)
                                {
                                    return cmd;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

      
    }
}
