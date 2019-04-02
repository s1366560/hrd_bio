using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using BioA.Common.IO;
/*************************************************************************************
 * CLR版本：       
 * 类 名 称：       
 * 机器名称：       
 * 命名空间：       
 * 文 件 名：       
 * 创建时间：       
 * 作    者：       
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

        static List<string> _SampleContainerList;
        /// <summary>
        /// 获取样本容器
        /// </summary>
        public static List<string> SampleContainerList
        {
            get
            {
                if (_SampleContainerList == null)
                {
                    _SampleContainerList = new List<string>();

                    XmlNode sampleContainer = XMLHelper.GetNode(RunConfigureNode, "SampleContainers");
                    string strSampleContainers = XMLHelper.Read(sampleContainer, "Values");

                    while (strSampleContainers.IndexOf(",") >= 0)
                    {
                        _SampleContainerList.Add(strSampleContainers.Substring(0, strSampleContainers.IndexOf(",")));
                        strSampleContainers = strSampleContainers.Substring(strSampleContainers.IndexOf(",") + 1);
                    }

                    _SampleContainerList.Add(strSampleContainers);
                }
                return _SampleContainerList;
            }
        }


        static List<string> _AnalizeMethodList;
        /// <summary>
        /// 获取分析方法
        /// </summary>
        public static List<string> AnalizeMethodList
        {
            get
            {
                if (_AnalizeMethodList == null)
                {
                    _AnalizeMethodList = new List<string>();

                    XmlNode analizeMethod = XMLHelper.GetNode(RunConfigureNode, "AnalizeMethods");
                    string strAnalizeMethods = XMLHelper.Read(analizeMethod, "Values");

                    while (strAnalizeMethods.IndexOf(",") >= 0)
                    {
                        _AnalizeMethodList.Add(strAnalizeMethods.Substring(0, strAnalizeMethods.IndexOf(",")));
                        strAnalizeMethods = strAnalizeMethods.Substring(strAnalizeMethods.IndexOf(",") + 1);
                    }

                    _AnalizeMethodList.Add(strAnalizeMethods);
                }
                return _AnalizeMethodList;
            }
        }

        static List<string> _BoundDirectionList;
        /// <summary>
        /// 获取范围方向
        /// </summary>
        public static List<string> BoundDirectionList
        {
            get
            {
                if (_BoundDirectionList == null)
                {
                    _BoundDirectionList = new List<string>();

                    XmlNode boundDirection = XMLHelper.GetNode(RunConfigureNode, "BoundDirection");
                    string strboundDirections = XMLHelper.Read(boundDirection, "Values");

                    while (strboundDirections.IndexOf(",") >= 0)
                    {
                        _BoundDirectionList.Add(strboundDirections.Substring(0, strboundDirections.IndexOf(",")));
                        strboundDirections = strboundDirections.Substring(strboundDirections.IndexOf(",") + 1);
                    }

                    _BoundDirectionList.Add(strboundDirections);
                }
                return _BoundDirectionList;
            }
        }

        static List<string> _ReactionDirectionList;
        /// <summary>
        /// 获取反应方向
        /// </summary>
        public static List<string> ReactionDirectionList
        {
            get
            {
                if (_ReactionDirectionList == null)
                {
                    _ReactionDirectionList = new List<string>();

                    XmlNode reactionDirection = XMLHelper.GetNode(RunConfigureNode, "ReactionDirection");
                    string strreactionDirections = XMLHelper.Read(reactionDirection, "Values");

                    while (strreactionDirections.IndexOf(",") >= 0)
                    {
                        _ReactionDirectionList.Add(strreactionDirections.Substring(0, strreactionDirections.IndexOf(",")));
                        strreactionDirections = strreactionDirections.Substring(strreactionDirections.IndexOf(",") + 1);
                    }

                    _ReactionDirectionList.Add(strreactionDirections);
                }
                return _ReactionDirectionList;
            }
        }

        //ResultDecimal
        static List<string> _ResultDecimalList;
        /// <summary>
        /// 获取结果单位
        /// </summary>
        public static List<string> ResultDecimalList
        {
            get
            {
                if (_ResultDecimalList == null)
                {
                    _ResultDecimalList = new List<string>();

                    XmlNode resultDecimal = XMLHelper.GetNode(RunConfigureNode, "ResultDecimal");
                    string strResultDecimals = XMLHelper.Read(resultDecimal, "Values");

                    while (strResultDecimals.IndexOf(",") >= 0)
                    {
                        _ResultDecimalList.Add(strResultDecimals.Substring(0, strResultDecimals.IndexOf(",")));
                        strResultDecimals = strResultDecimals.Substring(strResultDecimals.IndexOf(",") + 1);
                    }

                    _ResultDecimalList.Add(strResultDecimals);
                }
                return _ResultDecimalList;
            }
        }

        static int _LastPoint = 0;

        public static int LastPoint
        {
            get
            {
                if (_LastPoint == 0)
                {
                    XmlNode point = XMLHelper.GetNode(RunConfigureNode, "ReactionPanel");
                    _LastPoint = System.Convert.ToInt32(XMLHelper.Read(point, "LastPoint"));
                }

                return _LastPoint;
            }
        }
        static int _BlankPoint = 0;

        public static int BlankPoint
        {
            get
            {
                if (_BlankPoint == 0)
                {
                    XmlNode point = XMLHelper.GetNode(RunConfigureNode, "ReactionPanel");
                    _BlankPoint = System.Convert.ToInt32(XMLHelper.Read(point, "BlankPoint"));
                }

                return _BlankPoint;
            }
        }

        static int _CUVCount = 0;

        public static int CUVCount
        {
            get
            {
                if (_CUVCount == 0)
                {
                    XmlNode point = XMLHelper.GetNode(RunConfigureNode, "ReactionPanel");
                    _CUVCount = System.Convert.ToInt32(XMLHelper.Read(point, "CUVCount"));
                }

                return _CUVCount;
            }
        }        static List<string> _CalibrationMethods;
        /// <summary>
        /// 获取校准方法
        /// </summary>
        public static List<string> CalibrationMethods
        {
            get
            {
                if (_CalibrationMethods == null)
                {
                    _CalibrationMethods = new List<string>();

                    XmlNode calibMethods = XMLHelper.GetNode(RunConfigureNode, "CalibrationMethods");
                    string strCalibDecimals = XMLHelper.Read(calibMethods, "Values");

                    while (strCalibDecimals.IndexOf(",") >= 0)
                    {
                        _CalibrationMethods.Add(strCalibDecimals.Substring(0, strCalibDecimals.IndexOf(",")));
                        strCalibDecimals = strCalibDecimals.Substring(strCalibDecimals.IndexOf(",") + 1);
                    }

                    _CalibrationMethods.Add(strCalibDecimals);
                }
                return _CalibrationMethods;
            }
        }

        static List<string> _CalibrationTimes;
        /// <summary>
        /// 获取校准次数
        /// </summary>
        public static List<string> CalibrationTimes
        {
            get
            {
                if (_CalibrationTimes == null)
                {
                    _CalibrationTimes = new List<string>();

                    XmlNode calibTimes = XMLHelper.GetNode(RunConfigureNode, "CalibrationTimes");
                    string strCalibTimes = XMLHelper.Read(calibTimes, "Values");

                    while (strCalibTimes.IndexOf(",") >= 0)
                    {
                        _CalibrationTimes.Add(strCalibTimes.Substring(0, strCalibTimes.IndexOf(",")));
                        strCalibTimes = strCalibTimes.Substring(strCalibTimes.IndexOf(",") + 1);
                    }

                    _CalibrationTimes.Add(strCalibTimes);
                }
                return _CalibrationTimes;
            }
        }

        static List<string> _SampleTypes;
        /// <summary>
        /// 获取样本类型
        /// </summary>
        public static List<string> SampleTypes
        {
            get
            {
                if (_SampleTypes == null)
                {
                    _SampleTypes = new List<string>();

                    XmlNode sampleTypes = XMLHelper.GetNode(RunConfigureNode, "SampleTypes");
                    string strSampleTypes = XMLHelper.Read(sampleTypes, "Values");

                    while (strSampleTypes.IndexOf(",") >= 0)
                    {
                        _SampleTypes.Add(strSampleTypes.Substring(0, strSampleTypes.IndexOf(",")));
                        strSampleTypes = strSampleTypes.Substring(strSampleTypes.IndexOf(",") + 1);
                    }

                    _SampleTypes.Add(strSampleTypes);
                }
                return _SampleTypes;
            }
        }
        static string _HospitolName;
        /// <summary>
        /// 获取医院名称
        /// </summary>
        public static string HospitolName
        {
            get 
            {
                if(_HospitolName == null)
                {
                    XmlNode hospitolname = XMLHelper.GetNode(RunConfigureNode, "HospitalName");
                    string strhospitolname = XMLHelper.Read(hospitolname, "Values");
                    _HospitolName = strhospitolname;
                }

                return _HospitolName;
            }
        }
        static List<string> _PrintPaper;
        /// <summary>
        /// 获取打印纸张类型
        /// </summary>
        public static List<string> PrintPaper
        {
            get
            {
                if (_PrintPaper == null)
                {
                    _PrintPaper = new List<string>();

                    XmlNode printPaper = XMLHelper.GetNode(RunConfigureNode, "PrintPaper");
                    string strPrintPaper = XMLHelper.Read(printPaper, "Values");

                    while (strPrintPaper.IndexOf(",") >= 0)
                    {
                        _PrintPaper.Add(strPrintPaper.Substring(0, strPrintPaper.IndexOf(",")));
                        strPrintPaper = strPrintPaper.Substring(strPrintPaper.IndexOf(",") + 1);
                    }

                    _PrintPaper.Add(strPrintPaper);
                }

                return _PrintPaper;
            }
        }
        static List<string> _QCLevelConc;
        /// <summary>
        /// 质控品水平浓度
        /// </summary>
        public static List<string> QCLevelConc
        {
            get
            {
                if (_QCLevelConc == null)
                {
                    _QCLevelConc = new List<string>();

                    XmlNode qCLevelConc = XMLHelper.GetNode(RunConfigureNode, "QCLevelConc");
                    string strQCLevelConc = XMLHelper.Read(qCLevelConc, "Values");

                    while (strQCLevelConc.IndexOf(",") >= 0)
                    {
                        _QCLevelConc.Add(strQCLevelConc.Substring(0, strQCLevelConc.IndexOf(",")));
                        strQCLevelConc = strQCLevelConc.Substring(strQCLevelConc.IndexOf(",") + 1);
                    }

                    _QCLevelConc.Add(strQCLevelConc);
                }

                return _QCLevelConc;
            }
        }

        static List<string> _QCPosition;
        /// <summary>
        /// 质控位置
        /// </summary>
        public static List<string> QCPosition
        {
            get
            {
                if (_QCPosition == null)
                {
                    _QCPosition = new List<string>();

                    XmlNode qCPosition = XMLHelper.GetNode(RunConfigureNode, "QCPosition");
                    string strQCPosition = XMLHelper.Read(qCPosition, "Values");

                    while (strQCPosition.IndexOf(",") >= 0)
                    {
                        _QCPosition.Add(strQCPosition.Substring(0, strQCPosition.IndexOf(",")));
                        strQCPosition = strQCPosition.Substring(strQCPosition.IndexOf(",") + 1);
                    }

                    _QCPosition.Add(strQCPosition);
                }

                return _QCPosition;
            }
        }

        //ReactionPoints

        static List<string> _ReactionPoints;
        /// <summary>
        /// 获取测光点
        /// </summary>
        public static List<string> ReactionPoints
        {
            get
            {
                if (_ReactionPoints == null)
                {
                    _ReactionPoints = new List<string>();

                    XmlNode reactionPoint = XMLHelper.GetNode(RunConfigureNode, "ReactionPoints");
                    string strReactionPoint = XMLHelper.Read(reactionPoint, "Values");

                    while (strReactionPoint.IndexOf(",") >= 0)
                    {
                        _ReactionPoints.Add(strReactionPoint.Substring(0, strReactionPoint.IndexOf(",")));
                        strReactionPoint = strReactionPoint.Substring(strReactionPoint.IndexOf(",") + 1);
                    }

                    _ReactionPoints.Add(strReactionPoint);
                }

                return _ReactionPoints;
            }
        }


        static List<string> _Reagentpos;
        //试剂盘1的所有位置
        public static List<string> Reagentpos
        {
            get
            {
                if (_Reagentpos == null)
                {
                    _Reagentpos = new List<string>();
                    XmlNode waveLengths = XMLHelper.GetNode(RunConfigureNode, "Reagentpos");
                    string strWaveLength = XMLHelper.Read(waveLengths, "Values");

                    while (strWaveLength.IndexOf(",") >= 0)
                    {
                        _Reagentpos.Add(strWaveLength.Substring(0, strWaveLength.IndexOf(",")));
                        strWaveLength = strWaveLength.Substring(strWaveLength.IndexOf(",") + 1);
                    }

                    _Reagentpos.Add(strWaveLength);
                }

                return _Reagentpos;
            }
        }
        //试剂盘2的所有位置
        static List<string> _Reagebtpos2;
        public static List<string> Reagentpos2
        {
            get
            {
                if (_Reagebtpos2 == null)
                {
                    _Reagebtpos2 = new List<string>();
                    XmlNode waveLengths = XMLHelper.GetNode(RunConfigureNode, "Reagentpos2");
                    string strWaveLength = XMLHelper.Read(waveLengths, "Values");

                    while (strWaveLength.IndexOf(",") >= 0)
                    {
                        _Reagebtpos2.Add(strWaveLength.Substring(0, strWaveLength.IndexOf(",")));
                        strWaveLength = strWaveLength.Substring(strWaveLength.IndexOf(",") + 1);
                    }

                    _Reagebtpos2.Add(strWaveLength);
                }

                return _Reagebtpos2;
            }
        }

        static List<string> _CalibPosition;
        /// <summary>
        ///     校准品位置
        /// </summary>
        public static List<string> CalibPosition
        {
            get
            {
                if (_CalibPosition == null)
                {
                    _CalibPosition = new List<string>();
                    XmlNode waveLengths = XMLHelper.GetNode(RunConfigureNode, "CalibPosition");
                    string strWaveLength = XMLHelper.Read(waveLengths, "Values");

                    while (strWaveLength.IndexOf(",") >= 0)
                    {
                        _CalibPosition.Add(strWaveLength.Substring(0, strWaveLength.IndexOf(",")));
                        strWaveLength = strWaveLength.Substring(strWaveLength.IndexOf(",") + 1);
                    }

                    _CalibPosition.Add(strWaveLength);
                }

                return _CalibPosition;
            }
        }
        
        static List<string> _SamplePosition;
        /// <summary>
        /// 获取样本位置
        /// </summary>
        public static List<string> SamplePosition
        {
            get
            {
                if (_SamplePosition == null)
                {
                    _SamplePosition = new List<string>();

                    XmlNode samplePosition = XMLHelper.GetNode(RunConfigureNode, "SamplePosition");
                    string strSamplePosition = XMLHelper.Read(samplePosition, "Values");

                    while (strSamplePosition.IndexOf(",") >= 0)
                    {
                        _SamplePosition.Add(strSamplePosition.Substring(0, strSamplePosition.IndexOf(",")));
                        strSamplePosition = strSamplePosition.Substring(strSamplePosition.IndexOf(",") + 1);
                    }

                    _SamplePosition.Add(strSamplePosition);
                }

                return _SamplePosition;
            }
        }


        static List<string> _SamplePanel;
        /// <summary>
        /// 获取样本盘号
        /// </summary>
        public static List<string> SamplePanel
        {
            get
            {
                if (_SamplePanel == null)
                {
                    _SamplePanel = new List<string>();

                    XmlNode samplePanel = XMLHelper.GetNode(RunConfigureNode, "SamplePanel");
                    string strSamplePanel = XMLHelper.Read(samplePanel, "Values");

                    while (strSamplePanel.IndexOf(",") >= 0)
                    {
                        _SamplePanel.Add(strSamplePanel.Substring(0, strSamplePanel.IndexOf(",")));
                        strSamplePanel = strSamplePanel.Substring(strSamplePanel.IndexOf(",") + 1);
                    }

                    _SamplePanel.Add(strSamplePanel);
                }

                return _SamplePanel;
            }
        }

        static List<string> _SampleDilute;
        /// <summary>
        /// 获取样本稀释
        /// </summary>
        public static List<string> SampleDilute
        {
            get
            {
                if (_SampleDilute == null)
                {
                    _SampleDilute = new List<string>();

                    XmlNode sampleDilute = XMLHelper.GetNode(RunConfigureNode, "SampleDilute");
                    string strSampleDilute = XMLHelper.Read(sampleDilute, "Values");

                    while (strSampleDilute.IndexOf(",") >= 0)
                    {
                        _SampleDilute.Add(strSampleDilute.Substring(0, strSampleDilute.IndexOf(",")));
                        strSampleDilute = strSampleDilute.Substring(strSampleDilute.IndexOf(",") + 1);
                    }

                    _SampleDilute.Add(strSampleDilute);
                }

                return _SampleDilute;
            }
        }


        static Dictionary<string, string> _Filters;
        /// <summary>
        /// 获取筛选开关值
        /// </summary>
        public static Dictionary<string, string> Filters
        {
            get
            {
                _Filters = new Dictionary<string, string>();

                XmlNode filters = XMLHelper.GetNode(RunConfigureNode, "Filters");

                _Filters.Add("SampleTested", XMLHelper.Read(filters, "SampleTested"));
                _Filters.Add("SampleUnTest", XMLHelper.Read(filters, "SampleUnTest"));
                _Filters.Add("SampleTesting", XMLHelper.Read(filters, "SampleTesting"));

                

                return _Filters;
            }
        }

        public static void UpdateConfigureInfo(string attribute, Dictionary<string, string> dicValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement element;
            switch (attribute)
            {
                case "Filters":
                    xmlDoc.Load(_RunConfigureFile);
                    element = (XmlElement)xmlDoc.SelectSingleNode("RunSetting/Filters");
                    element.SetAttribute("SampleTested", dicValue["SampleTested"]);
                    element.SetAttribute("SampleUnTest", dicValue["SampleUnTest"]);
                    element.SetAttribute("SampleTesting", dicValue["SampleTesting"]);
                    xmlDoc.Save(_RunConfigureFile);
                    break;
                case "CheckSampleTaskState":
                    xmlDoc.Load(_RunConfigureFile);
                    element = (XmlElement)xmlDoc.SelectSingleNode("RunSetting/CheckSampleTaskState");
                    element.SetAttribute("Completed", dicValue["Completed"]);
                    element.SetAttribute("Starting", dicValue["Starting"]);
                    element.SetAttribute("NoStart", dicValue["NoStart"]);
                    xmlDoc.Save(_RunConfigureFile);
                    break;
                case "CheckSampleTaskSwitch":
                    xmlDoc.Load(_RunConfigureFile);
                    element = (XmlElement)xmlDoc.SelectSingleNode("RunSetting/CheckSampleTaskState");
                    element.SetAttribute("FilterSwitch", dicValue["FilterSwitch"]);
                    xmlDoc.Save(_RunConfigureFile);
                    break;
                default:
                    break;
            }
        }

        static Dictionary<string, bool> _ChkSampleTaskState;

        public static Dictionary<string, bool> ChkSampleTaskState
        {
            get
            {
                _ChkSampleTaskState = new Dictionary<string, bool>();

                XmlNode sampleTaskState = XMLHelper.GetNode(RunConfigureNode, "CheckSampleTaskState");

                _ChkSampleTaskState.Add("Completed", System.Convert.ToBoolean(XMLHelper.Read(sampleTaskState, "Completed")));
                _ChkSampleTaskState.Add("Starting", System.Convert.ToBoolean(XMLHelper.Read(sampleTaskState, "Starting")));
                _ChkSampleTaskState.Add("NoStart", System.Convert.ToBoolean(XMLHelper.Read(sampleTaskState, "NoStart")));
                _ChkSampleTaskState.Add("FilterSwitch", System.Convert.ToBoolean(XMLHelper.Read(sampleTaskState, "FilterSwitch")));
                

                return _ChkSampleTaskState;
            }

        }

        static Dictionary<string, bool> _UserAuthorityInitial;

        public static Dictionary<string, bool> UserAuthorityInitial
        {
            get
            {
                if (_UserAuthorityInitial == null)
                {
                    _UserAuthorityInitial = new Dictionary<string, bool>();
                    XmlNode userAuthority = XMLHelper.GetNode(RunConfigureNode, "UserAuthorityInitial");
                    XmlNodeList nodelist = userAuthority.SelectNodes("Authority");
                    try
                    {
                        foreach (XmlElement element in nodelist)
                        {
                            _UserAuthorityInitial.Add(XMLHelper.Read(element, "Key"), System.Convert.ToBoolean(XMLHelper.Read(element, "Value")));
                        }
                    }
                    catch (Exception e)
                    {
                        LogInfo.WriteProcessLog("RunConfigure.xml中UserAuthorityInitial录入有误", Module.Common);
                    }


                }
                return _UserAuthorityInitial;
            }
        }
        static float _LightSpan = 0;

        public static float LightSpan
        {
            get
            {
                if (_LightSpan == 0)
                {
                    XmlNode xmlTimeCourse = XMLHelper.GetNode(RunConfigureNode, "TimeCourse");
                    _LightSpan = (float)System.Convert.ToDouble(XMLHelper.Read(xmlTimeCourse, "LightSpan"));
                }

                return _LightSpan;
            }
        }

        static List<float> _PTInterval;

        public static List<float> PTInterval
        {
            get
            {
                if (_PTInterval == null)
                {
                    _PTInterval = new List<float>();

                    XmlNode xmlTimeCourse = XMLHelper.GetNode(RunConfigureNode, "TimeCourse");
                    string strPTInterval = XMLHelper.Read(xmlTimeCourse, "PTInterval");
                    _PTInterval.Add((float)System.Convert.ToDouble(strPTInterval.Substring(0, 1)));
                    strPTInterval = strPTInterval.Substring(2);
                    while (strPTInterval.Contains(","))
                    {
                        _PTInterval.Add((float)System.Convert.ToDouble(strPTInterval.Substring(0, strPTInterval.IndexOf(','))));
                        strPTInterval = strPTInterval.Substring(strPTInterval.IndexOf(',') + 1);
                    }
                    _PTInterval.Add((float)System.Convert.ToDouble(strPTInterval));
                }

                return _PTInterval;
            }
        }

        public static float GetTimeCourseTime(int i)
        {
            if (i >= PTInterval.Count())
            {
                return 0;
            }

            float sum = 0;
            for (int j = 0; j <= i; j++)
            {
                sum += PTInterval[j];
            }
            return sum;
        }

        static int _R2Point = 0;

        public static int R2Point
        {
            get
            {
                if (_R2Point == 0)
                {
                    XmlNode xmlR2Point = XMLHelper.GetNode(RunConfigureNode, "R2Point");
                    _R2Point = System.Convert.ToInt32(XMLHelper.Read(xmlR2Point, "Point"));
                }

                return _R2Point;
            }
        }    }
}
