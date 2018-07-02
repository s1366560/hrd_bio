using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BioA.Common.IO
{
    public static class XMLHelper
    {
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(string path, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_string Read(string path, string node, string attribute)==" + e.ToString(), Module.Common);
            }
            return value;
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Read(path, "/Node", "")
         * XmlHelper.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        public static string Read(XmlNode node, string attribute)
        {
            string value = "";
            try
            {
                value = (attribute.Equals("") ? node.InnerText : node.Attributes[attribute].Value);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_Read(XmlNode node, string attribute)==" + e.ToString(), Module.Common);
            }
            return value;
        }
        public static XmlNode GetNode(string path, string node)
        {
            XmlNode xn = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                xn = doc.SelectSingleNode(node);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_GetNode(string path, string node)==" + e.ToString(), Module.Common);
            }
            return xn;
        }
        public static XmlNode GetNode(XmlNode node, string childnode)
        {
            XmlNode xn = null;
            try
            {
                xn = node.SelectSingleNode(childnode);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_GetNode(XmlNode node, string childnode)==" + e.ToString(), Module.Common);
            }
            return xn;
        }

        // 获取多个节点
        public static XmlNodeList GetNodes(string path, string node)
        {
            XmlNodeList xn = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                xn = doc.SelectNodes(node);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_GetNodes(string path, string node)==" + e.ToString(), Module.Common);
            }
            return xn;
        }
        public static XmlNodeList GetNodes(XmlNode node, string childnode)
        {
            XmlNodeList xn = null;
            try
            {
                xn = node.SelectNodes(childnode);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_GetNodes(XmlNode node, string childnode)==" + e.ToString(), Module.Common);
            }
            return xn;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "Element", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Element", "Attribute", "Value")
         * XmlHelper.Insert(path, "/Node", "", "Attribute", "Value")
         ************************************************/
        public static void Insert(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                doc.Save(path);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_Insert(string path, string node, string element, string attribute, string value)==" + e.ToString(), Module.Common);
            }
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Insert(path, "/Node", "", "Value")
         * XmlHelper.Insert(path, "/Node", "Attribute", "Value")
         ************************************************/
        public static void Update(string path, string attribute, string value, string node)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_Update(string path, string attribute, string value, string node)==" + e.ToString(), Module.Common);
            }
        }
        public static void Update(string path, string attribute, string value, params string[] node)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNode xn = doc.SelectSingleNode(node[0]);
                for (int i = 1; i < node.Count(); i++)
                {
                    xn = xn.SelectSingleNode(node[i]);
                }

                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_Update(string path, string attribute, string value, params string[] node)==" + e.ToString(), Module.Common);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.Delete(path, "/Node", "")
         * XmlHelper.Delete(path, "/Node", "Attribute")
         ************************************************/
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_Delete(string path, string node, string attribute)==" + e.ToString(), Module.Common);
            }
        }
        public static void UpdateNamespaceManagerNode(string path, string attribute, string value, params string[] node)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNamespaceManager nsMgr = new XmlNamespaceManager(doc.NameTable);
                nsMgr.AddNamespace("ns", "http://java.sun.com/xml/ns/j2ee");

                XmlNode xn = doc.SelectSingleNode("/ns:web-app/ns:context-param/ns:param-value", nsMgr);

                //XmlNode xn = doc.SelectSingleNode("/ns:" + node[0], nsMgr);
                //for (int i = 1; i < node.Count(); i++)
                //{
                //    xn = xn.SelectSingleNode(node[i]);
                //}
                //xn = XMLHelper.GetNode(xn, @"context-param");
                // = XMLHelper.GetNode(xn, @"param-value");

                XmlElement xe = (XmlElement)xn;
                xe.InnerText = value;
                //if (attribute.Equals(""))
                //    xe.InnerText = value;
                //else
                //    xe.SetAttribute(attribute, value);
                doc.Save(path);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_UpdateNamespaceManagerNode(string path, string attribute, string value, params string[] node)==" + e.ToString(), Module.Common);
            }
        }
        public static XmlNode GetTomcatNode(string path, string node)
        {

            XmlNode xn = null;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNamespaceManager nsMgr = new XmlNamespaceManager(doc.NameTable);
                nsMgr.AddNamespace("ns", "http://java.sun.com/xml/ns/j2ee");


                xn = doc.SelectSingleNode("/ns:web-app", nsMgr);
            }
            catch (Exception e)
            {
                LogInfo.WriteErrorLog("XMLHelper.cs_GetTomcatNode(string path, string node)==" + e.ToString(), Module.Common);
            }
            return xn;
        }
    }
}
