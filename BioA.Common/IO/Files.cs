/********************************************************************************
** auth： Spener
** date： 8/5/2017 4:04:40 PM
** desc： 尚未编写描述
** Ver.:  V1.0.0
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BioA.Common.IO
{
    public class Files
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">路径如：c:\tool</param>
        /// <returns>true  建立目录成功</returns>
        /// <returns>false 建立目录失败</returns>
         public static bool CreatDirectory(string path)
         {
             if (!Directory.Exists(path))
             {
                 Directory.CreateDirectory(path);
                 return true ;
             }
             return false;
         }
         /// <summary>
         /// 创建目录
         /// </summary>
         /// <param name="path">路径如：c:\tool</param>
         /// <returns>true  建立目录成功</returns>
         /// <returns>false 建立目录失败</returns>
         public static bool CreateFileEmptyXml(string path)
         {
             if (!System.IO.File.Exists(path))
             {
                 XmlDocument doc = new XmlDocument();
                 XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "GB2312", null);
                 doc.AppendChild(dec);
                 XmlElement root = doc.CreateElement("nodes");
                 doc.AppendChild(root);
                 doc.Save(path);
                 return true;
             }
             else

                 return false;
         }
    }
}
