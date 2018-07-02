using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using BioA.Common.IO;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       RunConfigureUtility
 * 机器名称：       WENSION
 * 命名空间：       NAD.Common
 * 文 件 名：       RunConfigureUtility
 * 创建时间：       4/27/2012 5:06:05 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.Common
{
    /// <summary>
    /// 进程信息
    /// </summary>
    public class ProcessInfo
    {
        public string Name { get; set; }
        public string ProcessName { get; set; }
        public int RunLevel { get; set; }
    }
    public class RunConfigureUtility
    {
        static XmlNode _RunConfigureNode;
        public static XmlNode RunConfigureNode
        {
            get
            {
                if (_RunConfigureNode == null)
                {
                    _RunConfigureNode = XMLHelper.GetNode(RunConfigureFile, "RunSetting");
                }
                return _RunConfigureNode;
            }
        }
        static string _RunConfigureFile;
        public static string RunConfigureFile
        {
            get
            {
                if (string.IsNullOrEmpty(_RunConfigureFile) || string.IsNullOrWhiteSpace(_RunConfigureFile))
                {
                    CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
                    switch (currentCultureInfo.Name)
                    {
                        case "zh-CN":
                            _RunConfigureFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"RunConfigure.xml";
                            break;
                        default:
                            break;
                    }
                }

                return _RunConfigureFile;
            }
        }


        static List<ProcessInfo> _ProcessPathList;
        /// <summary>
        /// 获取启动进程列表
        /// </summary>
        public static List<ProcessInfo> ProcessPathList
        {
            get
            {
                if (_ProcessPathList == null)
                {
                    _ProcessPathList = new List<ProcessInfo>();
                    XmlNode ProcessesNode = XMLHelper.GetNode(RunConfigureNode, "Processes");
                    XmlNodeList nodelist = ProcessesNode.SelectNodes("Process");
                    foreach (XmlElement element in nodelist)
                    {
                        ProcessInfo p = new ProcessInfo();
                        p.Name = XMLHelper.Read(element, "Name");
                        p.ProcessName = XMLHelper.Read(element, "Path");
                        try
                        {
                            p.RunLevel = int.Parse(XMLHelper.Read(element, "RunLevel"));
                        }
                        catch
                        {
                            p.RunLevel = 0;
                        }

                        _ProcessPathList.Add(p);
                    }
                }

                return _ProcessPathList;
            }
        }

        static List<string> _AbsCalcMethodList;
        /// <summary>
        /// 获取吸光度计算方法
        /// </summary>
        public static List<string> AbsCalcMethodList
        {
            get
            {
                if (_AbsCalcMethodList == null)
                {
                    _AbsCalcMethodList = new List<string>();
                    XmlNode AbsCalcMethods = XMLHelper.GetNode(RunConfigureNode, "AbsCalcMethods");
                    XmlNodeList nodelist = AbsCalcMethods.SelectNodes("Method");

                    foreach (XmlElement element in nodelist)
                    {
                        _AbsCalcMethodList.Add(XMLHelper.Read(element, "Name"));
                    }
                }

                return _AbsCalcMethodList;
            }
        }

        static List<string> _DecimalList;
        /// <summary>
        /// 获取小数位数
        /// </summary>
        public static List<string> DecimalList
        {
            get
            {
                if (_DecimalList == null)
                {
                    _DecimalList = new List<string>();
                    XmlNode decimals = XMLHelper.GetNode(RunConfigureNode, "Decimals");
                    string strDecimals = XMLHelper.Read(decimals, "Values");
                    while (strDecimals.IndexOf(",") >= 0)
                    {
                        _DecimalList.Add(strDecimals.Substring(0, strDecimals.IndexOf(",")));
                        strDecimals = strDecimals.Substring(strDecimals.IndexOf(",") + 1);
                    }

                    _DecimalList.Add(strDecimals);
                }

                return _DecimalList;
            }
        }

        static List<string> _WaveLengthList;
        /// <summary>
        /// 获取波长
        /// </summary>
        public static List<string> WaveLengthList
        {
            get
            {
                if (_WaveLengthList == null)
                {
                    _WaveLengthList = new List<string>();
                    XmlNode waveLengths = XMLHelper.GetNode(RunConfigureNode, "WaveLengths");
                    string strWaveLength = XMLHelper.Read(waveLengths, "Values");

                    while (strWaveLength.IndexOf(",") >= 0)
                    {
                        _WaveLengthList.Add(strWaveLength.Substring(0, strWaveLength.IndexOf(",")));
                        strWaveLength = strWaveLength.Substring(strWaveLength.IndexOf(",") + 1);
                    }

                    _WaveLengthList.Add(strWaveLength);
                }

                return _WaveLengthList;
            }
        }

        static List<string> _StirStrengthList;
        /// <summary>
        /// 获取搅拌强度
        /// </summary>
        public static List<string> StirStrengthList
        {
            get
            {
                if (_StirStrengthList == null)
                {
                    _StirStrengthList = new List<string>();
                    XmlNode stirStrengths = XMLHelper.GetNode(RunConfigureNode, "StirStrengths");
                    string strStirStrength = XMLHelper.Read(stirStrengths, "Values");

                    while (strStirStrength.IndexOf(",") >= 0)
                    {
                        _StirStrengthList.Add(strStirStrength.Substring(0, strStirStrength.IndexOf(",")));
                        strStirStrength = strStirStrength.Substring(strStirStrength.IndexOf(",") + 1);
                    }

                    _StirStrengthList.Add(strStirStrength);
                }

                return _StirStrengthList;
            }
        }
    }
}
