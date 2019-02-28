using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       MessageAnalyzer
 * 机器名称：       WENSION
 * 命名空间：       NAD.HL7.V231
 * 文 件 名：       MessageAnalyzer
 * 创建时间：       4/17/2012 11:14:34 AM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.HL7.V231
{
    public class MessageAnalyzer
    {
        public static string TrimMessage(string msg)
        {
            msg = msg.TrimStart((char)0x0B);
            msg = msg.TrimEnd(new char[2] { (char)0x1C, (char)0x0D });
            return msg;
        }
        public static string[] GetMessages(string str)
        {
            return str.Split( (char)0x0D );
        }

        public static string[] GetSegments(string msg)
        {
            return msg.Split((char)0x0D);
        }
        public static string[] GetFields(string Segment)
        {
            return Segment.Split('|');
        }
    }
}
