using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using BioA.Common.IO;

namespace BioA.Common.Entities
{
    public class ErrorInfoManager
    {
        public Dictionary<string, string> ErrorInfoDictionary = new Dictionary<string, string>();

        public ErrorInfoManager()
        {
            this.InitErrorInfoDictionary(GetMachineXmlNode());
        }

        void InitErrorInfoDictionary(XmlNode ErrorInfoXmlNode)
        {
            this.ErrorInfoDictionary.Clear();

            XmlNode EISNode = XMLHelper.GetNode(ErrorInfoXmlNode, "EIS");
            XmlNodeList nodelist = EISNode.SelectNodes("EI");
            foreach (XmlElement element in nodelist)
            {
                try
                {
                    string k = XMLHelper.Read(element, "key");
                    string i = XMLHelper.Read(element, "Info");

                    this.ErrorInfoDictionary.Add(k, i);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                } 
            }
        }

        XmlNode GetMachineXmlNode()
        {
            string file;
            CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
            switch (currentCultureInfo.Name)
            {
                case "zh-CN":
                    file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"ErrorInfoConfigure.xml";
                    break;
                default:
                    file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"ErrorInfoConfigure-en.xml"; ;
                    break;
            }
            return XMLHelper.GetNode(file, "ErrorInfoIConfigure");
        }
    }
}
