using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BioA.Common.IO
{
    public class XmlUtility
    {
        public static object Deserialize(Type type, string xml)
        {
            object reBack = new object();
            if (xml.Trim() != string.Empty)
            {
                try
                {
                    using (StringReader sr = new StringReader(xml))
                    {
                        XmlSerializer xmldes = new XmlSerializer(type);
                        reBack = xmldes.Deserialize(sr);
                    }
                }
                catch (Exception e)
                {
                    LogInfo.WriteErrorLog("XmlUtility.cs_Deserialize(Type type, string xml)==" + e.ToString(), Module.Common);
                    return null;
                }
            }
            else
            {
                reBack = null;
            }

            return reBack;
        }
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);

            return xmldes.Deserialize(stream);
        }
        public static string Serializer(Type type, object obj1,object obj2)
        {

            MemoryStream Stream1 = new MemoryStream();
            MemoryStream Stream2 = new MemoryStream();

            //创建序列化对象 
            try
            {
                XmlSerializer xml = new XmlSerializer(type);
                //序列化对象 

                xml.Serialize(Stream1, obj1);
                xml.Serialize(Stream2, obj2);

            }

            catch (InvalidOperationException)
            {

                throw;

            }

            Stream1.Position = 0;
            Stream2.Position = 0;

            StreamReader sr1 = new StreamReader(Stream1);
            StreamReader sr2 = new StreamReader(Stream2);

            
            string str1 = sr1.ReadToEnd();
            string str2 = sr2.ReadToEnd();
            string str = str1 + "|" + str2;

            return str;

        }
        public static string Serializer(Type type, object obj)
        {
            Console.WriteLine("serializer begin " + DateTime.Now.Ticks);
            MemoryStream Stream = new MemoryStream();

            //创建序列化对象 
            try
            {
                XmlSerializer xml = new XmlSerializer(type);
                //序列化对象 

                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException e)
            {

                LogInfo.WriteErrorLog("XmlUtility.cs_Deserialize(Type type, string xml)==" + e.ToString(), Module.Common);

            }

            Stream.Position = 0;

            StreamReader sr = new StreamReader(Stream);

            string str = sr.ReadToEnd();
            Console.WriteLine("serializer end   " + DateTime.Now.Ticks);
            return str;

        }
        public static string XmlAnalysis(string stringRoot, string xml)
        {
            if (stringRoot.Equals("") == false)
            {
                try
                {
                    XmlDocument XmlLoad = new XmlDocument();
                    XmlLoad.LoadXml(xml);
                    return XmlLoad.DocumentElement.SelectSingleNode(stringRoot).InnerXml.Trim();
                }
                catch (Exception ex)
                {
                }

            }
            return "";

        }
    }
}
