using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Reflection;
using BioA.Common.IO;
using BioA.PLCController.Interface;
using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using BioA.UI;

namespace BioA.PLCController
{
    public class CommandProtocol
    {
        public byte[] Protocol { get; set; }
        public string Encoder { get; set; }
        public string EncodingObject { get; set; }
        public int Offset { get; set; }
        public bool IsAdjustNode { get; set; }
    }
    public class CommandFlow
    {
        public string Command { get; set; }
        public string PreCommand { get; set; }
        public string PrePreCommand { get; set; }
        public string Node { get; set; }
    }
    public class ScheduleTASK
    {
        public int WN { get; set; }
        public TASK T { get; set; }
    }
    #region 处理任务给下位机执行的数据
    public class TASK
    {
        /// <summary>
        /// 样本编号
        /// </summary>
        public string SMPNO { get; set; }
        /// <summary>
        /// 测试项目
        /// </summary>
        public string ASSAY { get; set; }
        /// <summary>
        /// 样本类型(血清 尿液)标志
        /// </summary>
        public string SAMPLETYPE { get; set; }
        /// <summary>
        /// 样本体积类型
        /// </summary>
        public string VOLTYPE { get; set; }
        /// <summary>
        /// 样本管架号
        /// </summary>
        public string RACK { get; set; }
        /// <summary>
        /// 样本盘号
        /// </summary>
        public string DISK { get; set; }
        /// <summary>
        /// 样本位置
        /// </summary>
        public string SMPPOS { get; set; }
        /// <summary>
        /// 样本识别标识
        /// </summary>
        public int PT { get; set; }
        /// <summary>
        /// 防止交叉污染编号
        /// </summary>
        public int PPNO { get; set; }
        /// <summary>
        /// 稀释液位置
        /// </summary>
        public int DPOS { get; set; }
        /// <summary>
        /// 主波长
        /// </summary>
        public int PW { get; set; }
        /// <summary>
        /// 次波长
        /// </summary>
        public int SW { get; set; }
        /// <summary>
        /// 容器类型
        /// </summary>
        public int CT { get; set; }
        /// <summary>
        /// 校准品名称
        /// </summary>
        public string CALIBNAME { get; set; }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime CalibDate { get; set; }
        /// <summary>
        /// 试剂1位置
        /// </summary>
        public int R1POS { get; set; }
        /// <summary>
        /// 试剂1体积
        /// </summary>
        public int R1VOL { get; set; }
        /// <summary>
        /// 试剂2位置
        /// </summary>
        public int R2POS { get; set; }
        /// <summary>
        /// 试剂2体积
        /// </summary>
        public int R2VOL { get; set; }
        /// <summary>
        /// 稀释前体积
        /// </summary>
        public int PV { get; set; }
        /// <summary>
        /// 反应体积
        /// </summary>
        public int V { get; set; }
        /// <summary>
        /// 稀释液体积
        /// </summary>
        public int DV { get; set; }
        //public string ASSAYTYPE { get; set; }//项目识别标志(生化 离子)
        /// <summary>
        /// 项目搅拌1强度
        /// </summary>
        public int SF1 { get; set; }
        /// <summary>
        /// 项目搅拌2强度
        /// </summary>
        public int SF2 { get; set; }
    }
    #endregion

    public class Machine
    {
        string SendCommandFlag = null;

        string MachineNumber = null;

        // 数据库
        MyBatis myBatis = new MyBatis();

        //设备接收命令函数
        public void ReceiveCommand(Command cmd)
        {
            if (cmd == null)
            {
                return;
            }

            if (this.MachineState.Command != null)
            {
                switch (this.MachineState.Command.Name)
                {
                    case "IgnoreKeyDoRun":
                    case "StartSchedule":
                    case "PauseSchedule":
                    case "AbortSchedule":
                        if (cmd.Name == "IgnoreKeyDoRun" || cmd.Name == "StartSchedule" || cmd.Name == "PauseSchedule" || cmd.Name == "AbortSchedule" || cmd.Name == "AbortDorun")
                        {
                            break;
                        }
                        else
                        {
                            if (this.MachineState.Command.State == 1)
                            {
                                this.MachineState.State = "正在进行测试任务...";
                                return;
                            }
                        }
                        break;
                    case "PhotometerManualCheck":
                    case "PhotometerManualCheckStop":
                        break;
                    case "WashCuvette":
                        if (cmd.Name == "PauseWashCuvette")
                        {
                            break;
                        }
                        else
                        {
                            if (this.MachineState.Command.State == 1)
                            {
                                this.MachineState.State = "执行中...";
                                return;
                            }
                        }
                        break;
                    //default:
                    //    if (this.MachineState.Command.State == 1)
                    //    {
                    //        this.MachineState.State = "执行中...";
                    //        return;
                    //    }
                    //    break;
                }
            }
             
            this.MachineState.Fired = null;
            this.MachineState.Command = cmd;
            this.MachineState.State = null;
          
            switch (this.MachineState.Command.Name)
            {
                case "CheckCommunication":
                    this.Sending("CheckCommunication");
                    break;
                case "IgnoreKeyDoRun"://忽略即将到期的激活码
                    this.MachineState.Command.Name = "StartSchedule";//命令跳变
                    StartSchedule();
                    break;
                case "StartSchedule":
                    if (this.IsPauseSchedule == true)
                    {
                        this.IsPauseSchedule = false;
                        TaskQueueSignal.Set();
                    }
                    else
                    {
                        this.MachineState.State = "准备执行测试";
                        this.Sending("ReadTempt");
                        this.MachineState.State = "扫描设备状态";
                    }
                    break;
                case "PauseSchedule":
                    this.MachineState.State = "暂停测试";
                    PauseSchedule();
                    break;
                case "AbortSchedule":
                    this.MachineState.State = "准备放弃测试";//在请求指令下发送放弃指令，不能直接发送放弃指令！
                    break;
                case "WashByDetergent":// 清洗系统
                    this.MachineState.State = "开始系统清洗";
                    HasCleared = false;
                    StartWashByDetergent();
                    break;
                case "ExChangeWater"://水交换操作
                    this.SendCommandFlag = null;
                    this.Sending("ReadTempt");
                    this.MachineState.State = "扫描状态...";
                    break;
                case "ReadTempt":
                    this.Sending("ReadTempt");
                    this.MachineState.State = "扫描状态...";
                    break;
                case "AbortDorun":
                    this.MachineState.Fired = null;
                    this.MachineState.Command = null;
                    this.MachineState.State = "测试操作放弃";
                    break;
                case "StopWashByDetergent":
                    this.MachineState.State = "即将停止自动清洗";
                    break;
                default:
                    this.MachineState.State = "执行...";
                    Sending(this.MachineState.Command.Name);
                    break;
                
            }
        }
        //设备状态
        MachineState _MachineState;
        public MachineState MachineState
        {
            get { return _MachineState; }
            set
            {
                _MachineState = value;
            }
        }
        //串口控制器
        CTRCOMPort SerialPort = null;
        //命令字典
        Dictionary<string, CommandProtocol> CommandProtocol = new Dictionary<string, CommandProtocol>();
        //解析服务
        Dictionary<byte, IParse> ParseDictionary = new Dictionary<byte, IParse>();
        //编码服务
        Dictionary<string, IEncode> EncodeDictionary = new Dictionary<string, IEncode>();
        //运动节点码表
        Dictionary<string, byte[]> MoveNodeDictionary = new Dictionary<string, byte[]>();
        //机器启动状态

        //构造函数
        public Machine()
        {
            this.MachineState = new MachineState();
            this.MachineState.State = "开始...";

            XmlNode MachineXmlNode = GetMachineXmlNode();

            InitSerialPort(MachineXmlNode);

            InitCommandProtocol(MachineXmlNode);

            InitParseDictionary(MachineXmlNode);

            InitEncodeDictionary(MachineXmlNode);

            LoadMoveNode(MachineXmlNode);

            LoadCommandFlows(MachineXmlNode);

            Thread TaskQueueServiceThread = new Thread(new ThreadStart(TaskQueueService));
            TaskQueueServiceThread.Priority = ThreadPriority.Lowest;
            TaskQueueServiceThread.Start();

            Thread MachineMessageBagServiceThread = new Thread(new ThreadStart(MachineMessageBagService));
            MachineMessageBagServiceThread.Priority = ThreadPriority.Highest;
            MachineMessageBagServiceThread.Start();

            Thread ParseAnalyzeDataServiceThread = new Thread(new ThreadStart(ParseAnalyzeDataService));
            ParseAnalyzeDataServiceThread.Priority = ThreadPriority.BelowNormal;
            ParseAnalyzeDataServiceThread.Start();
            
            this.InitMachineData();
            

        }


        void OnSerialPortTimeout(object sender)
        {
            if (this.MachineState.Command != null)
            {
                this.MachineState.Command.State = 4;
            }
            this.MachineState.State = "超时";
        }
        /// <summary>
        /// 获取串口名
        /// </summary>
        /// <returns></returns>
        XmlNode GetMachineXmlNode()
        {
           // string type = MachineInfo.Type;

            string file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"HRD-800Command.xml";
            return XMLHelper.GetNode(file, "MachineConfigure");
        }
        /// <summary>
        /// 初始化串口
        /// </summary>
        /// <param name="machineXmlNode"></param>
        void InitSerialPort(XmlNode machineXmlNode)
        {
            XmlNode SerialPortNode = XMLHelper.GetNode(machineXmlNode, "SerialPort");

            SerialPort = new CTRCOMPort();

            SerialPort.ToAnalyzeEvent += new CTRCOMPort.CTRCOMPortHandler(OnMachineMessageBagQueue);
            SerialPort.SendTimeoutEvent += new CTRCOMPort.CTRCOMPortHandler(OnSerialPortTimeout);
            SerialPort.SetupService(MachineControlProtocol.BEGIN, MachineControlProtocol.END, SerialPortNode);
        }
        /// <summary>
        /// 获取命令字典节点
        /// </summary>
        /// <param name="machineXmlNode"></param>
        void InitCommandProtocol(XmlNode machineXmlNode)
        {
            XmlNode CMDNode = XMLHelper.GetNode(machineXmlNode, "CommandProtocol");
            XmlNodeList nodelist = CMDNode.SelectNodes("Command");
            foreach (XmlElement element in nodelist)
            {
                string n = XMLHelper.Read(element, "Name");
                string v = XMLHelper.Read(element, "Value");
                string e = XMLHelper.Read(element, "Encoder");
                string o = XMLHelper.Read(element, "Offset");
                string eo = XMLHelper.Read(element, "EncodingObject");
                string i = XMLHelper.Read(element, "IsAdjustNode");
                try
                {
                    CommandProtocol cp = new CommandProtocol();

                    cp.Encoder = e;
                    cp.EncodingObject = eo;
                    if (!string.IsNullOrEmpty(v) && !string.IsNullOrWhiteSpace(v))
                    {
                        cp.Protocol = MachineControlProtocol.HexStringToByteArray(v, ',');
                    }
                    if (o == "")
                        cp.Offset = 0;
                    else
                        cp.Offset = int.Parse(o);
                    if (i == "")
                        cp.IsAdjustNode = false;
                    else
                        cp.IsAdjustNode = bool.Parse(i);
                    //try
                    //{
                    //    cp.Offset = int.Parse(o);
                    //}
                    //catch
                    //{
                    //    cp.Offset = 0;
                    //}
                    //try
                    //{
                    //    cp.IsAdjustNode = bool.Parse(i);
                    //}
                    //catch
                    //{
                    //    cp.IsAdjustNode = false;
                    //}

                    CommandProtocol.Add(n, cp);
                }
                catch (System.Exception ex)
                {
                    string info = string.Format("{0}:{1} {2}", ex.Message, n, v);
                    Console.WriteLine(info);
                    continue;
                }
            }
        }
        /// <summary>
        /// 解码字典节点
        /// </summary>
        /// <param name="machineXmlNode"></param>
        void InitParseDictionary(XmlNode machineXmlNode)
        {
            XmlNode ParseDictionaryNode = XMLHelper.GetNode(machineXmlNode, "ParseDictionary");
            string dataAssembly = XMLHelper.Read(ParseDictionaryNode, "DataAssembly");

            XmlNodeList nodelist = ParseDictionaryNode.SelectNodes("ParseInterface");
            foreach (XmlElement element in nodelist)
            {
                try
                {
                    string key = XMLHelper.Read(element, "Key").Trim();
                    string ImplementClass = XMLHelper.Read(element, "ImplementClass");

                    IParse IParse = (IParse)Assembly.Load(dataAssembly).CreateInstance(ImplementClass);
                    ParseDictionary.Add(Convert.ToByte(key, 16), IParse);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
        /// <summary>
        /// 获取编码字典节点
        /// </summary>
        /// <param name="machineXmlNode"></param>
        void InitEncodeDictionary(XmlNode machineXmlNode)
        {
            XmlNode ParseDictionaryNode = XMLHelper.GetNode(machineXmlNode, "EncodeDictionary");
            string dataAssembly = XMLHelper.Read(ParseDictionaryNode, "DataAssembly");

            XmlNodeList nodelist = ParseDictionaryNode.SelectNodes("EncodeInterface");
            foreach (XmlElement element in nodelist)
            {
                try
                {
                    string key = XMLHelper.Read(element, "Key").Trim();
                    string ImplementClass = XMLHelper.Read(element, "ImplementClass");

                    IEncode IEncode = (IEncode)Assembly.Load(dataAssembly).CreateInstance(ImplementClass);
                    EncodeDictionary.Add(key, IEncode);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
        /// <summary>
        /// 获取校准节点
        /// </summary>
        /// <param name="machineXmlNode"></param>
        void LoadMoveNode(XmlNode machineXmlNode)
        {
            XmlNode MoveNode = XMLHelper.GetNode(machineXmlNode, "AdjustNode");
            XmlNodeList nodelist = MoveNode.SelectNodes("Node");
            foreach (XmlElement element in nodelist)
            {
                string name = XMLHelper.Read(element, "Name");
                string nodeCode = XMLHelper.Read(element, "Value").Trim();
                try
                {
                    MoveNodeDictionary.Add(name, MachineControlProtocol.HexStringToByteArray(nodeCode, ','));
                }
                catch (System.Exception ex)
                {
                    string info = string.Format("{0}:{1} {2:X}", ex.Message, name, nodeCode);
                    Console.WriteLine(info);
                    continue;
                }
            }
        }
        /// <summary>
        /// 初始化机器数据
        /// </summary>
        void InitMachineData()
        {
            Console.WriteLine(@"Initializing data...");

            Thread.Sleep(1000);
            
            this.IsPauseSchedule = false;
            myBatis.SetNorResultNA();//标记状态为1，把Rmarks字段改为（ "任务测试中断'）
            InitSDTSchedule();
            myBatis.ClearNotTodaySchedule();
            myBatis.SetUnfinishedScheduleContinue();

            if (myBatis.GetRunningDate() != DateTime.Now.Date.ToString())
            {
                //SampleSer.BackUpYesterdaySamples();
                //SampleSer.ClearYesterdaySamples();

                //SMPPatientSer.BackUpYesterdaySMPPatients();
                //SMPPatientSer.ClearYesterdaySMPPatients();

                //ResultSer.BackUpYesterdayNorResults();
                //ResultSer.ClearYesterdayNorResults();

                myBatis.BackUpYesterdayTimeCourses();
                myBatis.ClearYesterdayTimeCourses();

                //ISETimecourseService.BackUpYesterdayTimeCourses();
                //ISETimecourseService.ClearYesterdayTimeCourses();

                //if (SampleSer.GetSampleCount() == 0)
                //{
                //SMPPOSMgr.ClearAllSMPPositions();
                myBatis.InitRunningState();
                //}
            }

            myBatis.ClearRTData();

            myBatis.UpdateIsRunning(false);

            myBatis.InitSMPCalItems();

            //for (int i = 0; i < 5; i++)
            //{
            //    Thread.Sleep(100);
            //    new ScheduleService().SetSchedulePerformFalseFlag();
            //}
            CheckSDTTableItemValidDay();
            CheckDrugValidDay();

            //new RGTPOSManager().MakeAllRgtMutiPositionEnable();

            //if (MachineInfo.ISEEnable == true)
            //{
            //    new ISEItemSDTTableService().DeleteNewISEItemSDTTable();
            //    foreach (ISEItem e in new ISEItemService().GetALL())
            //    {
            //        new RunAssaySQService().Delete(e.Name);
            //        RunAssaySQ r = new RunAssaySQ();
            //        r.AssayName = e.Name;
            //        r.RunSQ = new RunAssaySQService().GetALL().Count;
            //        new RunAssaySQService().Save(r);
            //    }
            //}

            myBatis.UpdateWorkingDisk(1);

            //new AssayRunParaService().DeleteinvalidDisplaySQ();

            Console.WriteLine(@"Finish initializing data!");
        }
        /// <summary>
        /// 检查定标曲线有效期
        /// </summary>
        public void CheckSDTTableItemValidDay()
        {
            ProcessInvalid(myBatis.QuerySDTTableItemTb());
        }

        void ProcessInvalid(List<SDTTableItem> SDTTableItems)
        {
            foreach (SDTTableItem e in SDTTableItems)
            {
                AssayProjectCalibrationParamInfo calibParam = myBatis.QueryCalibParamByProNameAndType("QueryCalibParamByProNameAndType", new AssayProjectInfo() { ProjectName = e.ProjectName, SampleType = e.SampleType });
                if (calibParam == null)
                {
                    myBatis.DeleteSDTTableItemByProject(e.ProjectName, e.SampleType);
                }
                else
                {
                    if (e.IsUsed == true && DateTime.Today > e.CalibDate + new TimeSpan(calibParam.CalibCurveValidDay, 0, 0, 0))
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"00009";
                        trouble.TroubleType = TROUBLETYPE.WARN;
                        trouble.TroubleUnit = @"标准";
                        trouble.TroubleInfo = string.Format(e.ProjectName + "定标曲线过期，建议该项目定标. ");
                        myBatis.TroubleLogSave("TroubleLogSave", trouble);
                    }
                }
            }
        }
        /// <summary>
        /// 检查药物（试剂1、试剂2、清洗液、校准品、质控品）有效期
        /// </summary>
        private void CheckDrugValidDay()
        {
            // 1.试剂过期检查
            List<ReagentSettingsInfo> lstReagentInfo1 = myBatis.QueryReagentSettingsInfo("QueryReagentSetting1", null);
            List<ReagentSettingsInfo> lstReagentInfo2 = myBatis.QueryReagentSettingsInfo("QueryReagentSetting2", null);
            foreach (ReagentSettingsInfo reagentInfo1 in lstReagentInfo1)
            {
                if (reagentInfo1.ReagentType == "清洗液" || reagentInfo1.ReagentType == "稀释液")
                {

                }
                else
                {
                    if (DateTime.Today > reagentInfo1.ValidDate)
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"00009";
                        trouble.TroubleType = TROUBLETYPE.WARN;
                        trouble.TroubleUnit = @"试剂";
                        trouble.TroubleInfo = string.Format("试剂1中" + reagentInfo1.ReagentName + "过期. ");
                        myBatis.TroubleLogSave("TroubleLogSave", trouble);
                    }
                }
            }
            foreach (ReagentSettingsInfo ReagentInfo2 in lstReagentInfo2)
            {
                if (ReagentInfo2.ReagentType == "清洗液" || ReagentInfo2.ReagentType == "稀释液")
                {

                }
                else
                {
                    if (DateTime.Today > ReagentInfo2.ValidDate)
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleCode = @"00009";
                        trouble.TroubleType = TROUBLETYPE.WARN;
                        trouble.TroubleUnit = @"试剂";
                        trouble.TroubleInfo = string.Format("试剂2中" + ReagentInfo2.ReagentName + "过期. ");
                        myBatis.TroubleLogSave("TroubleLogSave", trouble);
                    }
                }
            }
            // 2.质控品过期检查
            List<QualityControlInfo> lstQCInfo = myBatis.QueryQCAllInfo("QueryQCAllInfo");
            foreach (QualityControlInfo qcInfo in lstQCInfo)
            {
                if (DateTime.Today > qcInfo.InvalidDate)
                {
                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleCode = @"00009";
                    trouble.TroubleType = TROUBLETYPE.WARN;
                    trouble.TroubleUnit = @"质控";
                    trouble.TroubleInfo = string.Format("质控品" + qcInfo.QCName + "过期. ");
                    myBatis.TroubleLogSave("TroubleLogSave", trouble);
                }
            }
            // 3.校准品过期检查
            List<Calibratorinfo> lstCalibInfo = myBatis.QueryCalibratorinfo("QueryCalibrationMaintain", null);
            foreach (Calibratorinfo calibInfo in lstCalibInfo)
            {
                if (DateTime.Today > calibInfo.InvalidDate)
                {
                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleCode = @"00009";
                    trouble.TroubleType = TROUBLETYPE.WARN;
                    trouble.TroubleUnit = @"标准";
                    trouble.TroubleInfo = string.Format("标准品" + calibInfo.CalibName + "过期. ");
                    myBatis.TroubleLogSave("TroubleLogSave", trouble);
                }
            }
        }
        /// <summary>
        /// 删除校准任务
        /// </summary>
        void InitSDTSchedule()
        {
            Console.WriteLine(@"initializing the sdt schedule......");

            List<SDTTableItem> SDTTableItems = myBatis.GetAllNewSDTTable();
            foreach (SDTTableItem s in SDTTableItems)
            {
                switch (s.CalibState)
                {
                    case CalibRemarks.NEW:
                        // 今天之前的，删除
                        if (s.DrawDate < DateTime.Now.Date)
                            myBatis.DeleteSDTTableItemByProAndDate(s);
                        break;
                    case CalibRemarks.CALI:
                        myBatis.SetSDTTabelFailedState(s.ProjectName, s.SampleType);
                        myBatis.DeleteSDTSchedule(s.BlkItem, s.ProjectName, s.SampleType);
                        myBatis.DeleteSDTSchedule(s.Calib1Item, s.ProjectName, s.SampleType);
                        myBatis.DeleteSDTSchedule(s.Calib2Item, s.ProjectName, s.SampleType);
                        myBatis.DeleteSDTSchedule(s.Calib3Item, s.ProjectName, s.SampleType);
                        myBatis.DeleteSDTSchedule(s.Calib4Item, s.ProjectName, s.SampleType);
                        myBatis.DeleteSDTSchedule(s.Calib5Item, s.ProjectName, s.SampleType);
                        myBatis.DeleteSDTSchedule(s.Calib6Item, s.ProjectName, s.SampleType);
                        break;
                }
            }
        }

        //void OnDataEvent(object sender)
        //{
        //    Console.WriteLine(sender as string);
        //}
        //获取运动当前节点名称
        List<CommandFlow> CommandFlows = new List<CommandFlow>();
        void LoadCommandFlows(XmlNode machineXmlNode)
        {
            XmlNode MoveNode = XMLHelper.GetNode(machineXmlNode, "CommandFlows");
            XmlNodeList nodelist = MoveNode.SelectNodes("CommandFlow");
            foreach (XmlElement element in nodelist)
            {
                string currentName = XMLHelper.Read(element, "Command");
                string preName = XMLHelper.Read(element, "PreCommand");
                string prepreName = XMLHelper.Read(element, "PrePreCommand");
                string nodeName = XMLHelper.Read(element, "Node");

                CommandFlow e = new CommandFlow();

                e.Command = currentName;
                e.PreCommand = preName;
                e.PrePreCommand = prepreName;
                e.Node = nodeName;

                CommandFlows.Add(e);
            }
        }
        List<CommandFlow> GetCommandFlowByCommand(string cmdName)
        {
            List<CommandFlow> _CFList = new List<CommandFlow>();
            foreach (CommandFlow c in CommandFlows)
            {
                if (c.Command == cmdName)
                {
                    _CFList.Add(c);
                }
            }
            return _CFList;
        }
        List<CommandFlow> GetCommandFlowByCommandAndPreCmd(string cmdName, string precmdName)
        {
            List<CommandFlow> _CFList = new List<CommandFlow>();
            foreach (CommandFlow c in CommandFlows)
            {
                if (precmdName == null)
                {
                    if (c.Command == cmdName && c.PreCommand == "")
                    {
                        _CFList.Add(c);
                    }
                }
                else
                {
                    if (c.Command == cmdName && c.PreCommand == precmdName)
                    {
                        _CFList.Add(c);
                    }
                }
            }
            return _CFList;
        }
        List<CommandFlow> GetCommandFlowByCommandAndPreCmdAndPreCmd(string cmdName, string precmdName, string pprecmdName)
        {
            List<CommandFlow> _CFList = new List<CommandFlow>();
            foreach (CommandFlow c in CommandFlows)
            {
                if (c.Command == cmdName && c.PreCommand == precmdName && c.PrePreCommand == pprecmdName)
                {
                    _CFList.Add(c);
                }
            }
            return _CFList;
        }

        byte[] GetAdjustSaveKey(string cmd, string precmd, string preprecmd)
        {
            //LogService.Log(string.Format("GetAdjustSaveKey({0},{1},{2})", cmd, precmd, preprecmd), LogType.Debug);

            List<CommandFlow> CommandFlow = GetCommandFlowByCommand(cmd);
            if (CommandFlow.Count > 1)
            {
                CommandFlow = GetCommandFlowByCommandAndPreCmd(cmd, precmd);
            }
            if (CommandFlow.Count > 1)
            {
                CommandFlow = GetCommandFlowByCommandAndPreCmdAndPreCmd(cmd, precmd, preprecmd);
            }
            CommandFlow cf = null;
            switch (CommandFlow.Count)
            {
                case 0:
                    //LogService.Log(string.Format("GetAdjustSaveKey({0},{1},{2}) not existing ", cmd, precmd, preprecmd), LogType.Debug);
                    break;
                case 1:
                    //LogService.Log(string.Format("GetAdjustSaveKey({0},{1},{2}) existing ", cmd, precmd, preprecmd), LogType.Debug);
                    cf = CommandFlow[0];
                    break;
                default:
                    //LogService.Log(string.Format("GetAdjustSaveKey({0},{1},{2}) error ", cmd, precmd, preprecmd), LogType.Debug);
                    break;
            }

            if (cf != null)
            {
                //LogService.Log(string.Format("cf.Command={0},cf.PreCommand={1},cf.PrePreCommand={2},cf.Node={3}", cf.Command, cf.PreCommand, cf.PrePreCommand, cf.Node), LogType.Debug);
                return this.MoveNodeDictionary[cf.Node];
            }
            //LogService.Log(string.Format("cf == null"), LogType.Debug);
            return null;
        }

        bool IsPauseSchedule = false;
        bool IsCheckPhotometer = true;
        void StartSchedule()
        {
            if (this.IsPauseSchedule == false)
            {
                if (IsCheckPhotometer)
                {
                    this.Sending("PhotometerAutoCheck");
                }
                else
                {
                    if (this.MachineState.Command.Name == "StartSchedule")
                    {
                        myBatis.UpdateIsRunning(true);
                    }

                    this.RequestWorkCount = 0;
                    this.WorkNum = 1;
                    this.TaskSumCount = 0;
                    lock (TaskQueue)
                    {
                        TaskQueue.Clear();
                    }

                    this.AssaySchedule = null;
                    this.AssayScheduleEncodingData = null;
                    this.IsAssayScheduleSent = false;
                    this.LoadTask();
                }
            }
            else
            {
                this.MachineState.State = "开始恢复测试";
                this.IsPauseSchedule = false;
                TaskQueueSignal.Set();
            }
        }

        //开始系统清洗
        void StartWashByDetergent()
        {
            this.RequestWorkCount = 0;
            this.WorkNum = 1;
            this.TaskSumCount = 0;
            lock (TaskQueue)
            {
                TaskQueue.Clear();
            }
            this.AssaySchedule = null;
            this.AssayScheduleEncodingData = null;
            this.IsAssayScheduleSent = false;
            this.LoadTask();
            this.Sending("RunSchedule");
        }

        void StopSchedule()
        {
            this.Sending("AbortSchedule");

            //new DetergentVolService().UpdateDetergentUsingFinishingTime(DateTime.Now);
        }
        void PauseSchedule()
        {
            this.IsPauseSchedule = true;
            TaskQueueSignal.Reset();
        }

        bool IsCheckSumRight(List<byte> data)
        {
            long bytessum = 0;
            for (int i = 0; i < data.Count - 2; i++)
            {
                bytessum += (int)data[i];
            }

            byte[] checksum = MachineControlProtocol.CheckSum(bytessum);
            if (checksum[0] == data[data.Count - 2] && checksum[1] == data[data.Count - 1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void OnMachineMessageBagQueue(object sender)
        {
            List<byte> data = new List<byte>(sender as List<byte>);
            if (data != null)
            {
                lock (MachineMessageBagQueue)
                {
                    MachineMessageBagQueue.Enqueue(data);
                }
            }

            if (MachineMessageBagQueue.Count > 0)
            {
                MachineMessageBagSignal.Set();
            }
        }
        ManualResetEvent MachineMessageBagSignal = new ManualResetEvent(false);
        Queue<List<byte>> MachineMessageBagQueue = new Queue<List<byte>>();
        void MachineMessageBagService()
        {
            while (true)
            {
                MachineMessageBagSignal.WaitOne();
                if (MachineMessageBagQueue.Count > 0)
                {
                    List<byte> data = null;
                    lock (MachineMessageBagQueue)
                    {
                        data = MachineMessageBagQueue.Dequeue();
                    }
                    OnParseSerialPortData(data);
                    data = null;
                }
                if (MachineMessageBagQueue.Count == 0)
                {
                    MachineMessageBagSignal.Reset();
                }
            }
        }

        ManualResetEvent AnalyzeDataSignal = new ManualResetEvent(false);
        Queue<List<byte>> AnalyzeDataBagQueue = new Queue<List<byte>>();
        void ParseAnalyzeDataService()
        {
            while (true)
            {
                AnalyzeDataSignal.WaitOne();
                if (AnalyzeDataBagQueue.Count > 0)
                {
                    List<byte> data = null;
                    lock (AnalyzeDataBagQueue)
                    {
                        data = AnalyzeDataBagQueue.Dequeue();
                    }

                    switch (this.MachineState.Command.Name)
                    {
                        case "WashByDetergent": ParseDictionary[0x07].Parse(data); break;
                        default: 
                            string state = ParseDictionary[0x08].Parse(data);
                            if (state == "ME" && this.MachineState.Command != null)
                            {
                                this.MachineState.State = "液路异常，测试无法进行";
                                this.MachineState.Command.Name = "AbortSchedule";
                            } 
                            break;
                    }

                    data = null;
                }

                if (AnalyzeDataBagQueue.Count == 0)
                {
                    AnalyzeDataSignal.Reset();
                }
            }
        }

        //分析包数据放入队列
        void OnAnalyzeDataBag(List<byte> sender)
        {
            List<byte> data = new List<byte>(sender);
            if (data != null)
            {
                lock (AnalyzeDataBagQueue)
                {
                    AnalyzeDataBagQueue.Enqueue(data);
                }
            }

            if (AnalyzeDataBagQueue.Count > 0)
            {
                AnalyzeDataSignal.Set();
            }
        }
        //任务次数
        private int _TasksNumber = 0; 

        long TaskSumCount = 0;

        #region 处理下位机数据信息
        void OnParseSerialPortData(List<byte> data)
        {
            //string a = "";
            //foreach (byte b in data)
            //{
            //    a += b.ToString() + " ";
            //}
            //LogInfo.WriteProcessLog(a, Common.Module.QualityControl);


            byte Key = data[1];

            // 检查校验和是否正确
            if (IsCheckSumRight(data) == true)
            {
                if (Key != 0x41 && Key != 0x46 && Key != 0x27)
                {
                    Sending("CheckSumRight");
                }
            }
            else
            {
                Sending("CheckSumWrong");
                this.MachineState.State = "接收错误数据";
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = "设备";
                trouble.TroubleCode = @"20011";
                trouble.TroubleInfo = this.MachineState.State;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
                return;
            }
            switch (data[1])
            {
                case 0x01:
                    switch (data[2])
                    {
                        case 0x23:
                            // 烧录码验证成功
                            this.MachineState.Fired = AnalyzeEvent.COMPLETED_READ_ACTIVEKEY;
                            this.MachineState.Command.State = 2;
                            this.MachineNumber = this.MachineState.StateValue;//设备SN号
                            this.MachineState.State = "完成机器身份验证！";
                            break;
                        case 0x24:
                            // 烧录码验证失败
                            this.MachineState.Fired = AnalyzeEvent.WORKSTATION_NOMATCHING;
                            this.MachineState.Command.State = 3;
                            this.MachineNumber = this.MachineState.StateValue;//设备SN号
                            this.MachineState.State = "机器身份验证失败！";
                            break;
                        case 0x25:
                            // 下位机不存在烧录码
                            this.MachineState.Fired = AnalyzeEvent.WORKSTATION_NOMATCHING;
                            this.MachineState.Command.State = 3;
                            this.MachineNumber = this.MachineState.StateValue;//设备SN号
                            this.MachineState.State = "机器不存在烧录码！";
                            break;
                        default:
                            // 下位机故障
                            break;
                    }
                    break;
                case 0x0E://温度
                    //接收6
                    
                    string tempstr = ParseDictionary[0x0E].Parse(data);
                    this.MachineState.StateValue = tempstr;
                    this.MachineState.Temp = tempstr;
                    if (this.MachineState.Command != null && this.MachineState.Command.Name == "ReadTempt")
                    {
                        this.MachineState.Fired = AnalyzeEvent.COMPLETE_READ_TEM;
                        this.MachineState.Command.State = 2;
                        this.MachineState.State = MachineReturnState.Machine17 + tempstr;
                    }
                    if (this.MachineState.Command != null && this.MachineState.Command.Name == "StartSchedule")
                    {
                        if (IsWaterExchangeEnable(data[2], data[3]) == false)
                        {
                            this.MachineState.Fired = AnalyzeEvent.MACHINE_STATE_ER;
                            this.MachineState.State = MachineReturnState.Machine18;
                            return;
                        }

                        float tempoffset = myBatis.GetTempOffset("GetTempOffset");
                        float ct = float.Parse(tempstr);
                        if (ct > (37.2f + tempoffset) || ct < (36.8f - tempoffset))
                        {
                            this.MachineState.Fired = AnalyzeEvent.SCAN_Temp_INVALID;
                            this.MachineState.State = MachineReturnState.Machine17 + tempstr;
                        }
                        else
                        {
                            this.MachineState.State = MachineReturnState.Machine19;
                            StartSchedule();
                        }
                    }
                    if (this.MachineState.Command != null && this.MachineState.Command.Name == "AbortSchedule")
                    {
                        this.StopLoadTask();
                        this.InitMachineData();
                        this.MachineState.State = MachineReturnState.Machine20;
                    }
                    if (this.MachineState.Command != null && this.MachineState.Command.Name == "ExChangeWater")
                    {
                        if (this.SendCommandFlag == "RunExChangeWater")
                        {
                            if (IsWaterExchangeEnable(data[2], data[3]) == true)
                            {
                                this.MachineState.Command.State = 2;
                                this.MachineState.State = MachineReturnState.Machine21;
                            }
                            else
                            {
                                this.MachineState.Command.State = 2;

                                this.MachineState.State = MachineReturnState.Machine22;
                            }
                        }
                        else
                        {
                            if (IsWaterExchangeEnable(data[2], data[3]) == true)
                            {
                                this.MachineState.State = MachineReturnState.Machine23;
                                Sending("ReadDRgtLevel");
                            }
                            else
                            {
                                this.MachineState.Command.State = 2;
                                this.MachineState.Fired = AnalyzeEvent.EXXCHANGE_WARER_FAILED;
                                this.MachineState.State = MachineReturnState.Machine24;
                            }
                        }
                    }
                    this.MachineState.Fired = null;//消息驱动置空
                    break;
                case 0x21://R1交叉污染
                    this.MachineState.State = "设置试剂针1防交叉污染";
                    this.SerialPort.SendBytesData(EncodeDictionary["0x21"].Encode(0x21));
                    break;
                case 0x22://R2交叉污染
                    this.MachineState.State = "设置试剂针2防交叉污染";
                    this.SerialPort.SendBytesData(EncodeDictionary["0x21"].Encode(0x22));
                    this.IsRunningSchedule = true;
                    //记录测试开始时间
                    myBatis.UpdateDetergentUsingStartingTime(DateTime.Now);
                    break;
                case 0x23://CUV交叉污染
                    this.MachineState.State = "设置比色杯防交叉污染";
                    this.SerialPort.SendBytesData(EncodeDictionary["0x21"].Encode(0x23));
                    this.IsRunningSchedule = true;
                    //记录测试开始时间
                    //new DetergentVolService().UpdateDetergentUsingStartingTime(DateTime.Now);
                    break;
                case 0x25://
                    ParseDictionary[0x25].Parse(data);
                    break;
                case 0x1F:
                    this.StopLoadTask();
                    this.MachineState.Fired = AnalyzeEvent.MACHINE_WILL_FINIFSHSCHEDULE;
                    this._TasksNumber = 0;
                    this.MachineState.State = "该批次任务即将完成"; 
                    break;
                case 0x26://任务测试结束
                    if (this.MachineState.Command != null)
                    {
                        switch (this.MachineState.Command.Name)
                        {
                            case "PauseSchedule":
                            case "StartSchedule":
                                this.MachineState.Fired = AnalyzeEvent.COMPLETED_SCHEDULE;
                                this.MachineState.Command = null;
                                this.MachineState.State = "该批次任务完成";
                                break;
                            case "WashByDetergent":
                                this.MachineState.State = "系统清洗完成";
                                break;
                        }

                        this.IsPauseSchedule = false;
                        this.MachineState.Fired = null;//消息驱动置空
                        this.StopLoadTask();//冗余设计
                        myBatis.UpdateIsRunning(false);
                        this.InitMachineData();////冗余设计,防止项目死锁
                        
                    }
                    //记录测试结束时间
                    //new DetergentVolService().UpdateDetergentUsingFinishingTime(DateTime.Now);
                    break;
                case 0x46://发送数据错误 被确认
                    this.MachineState.State = "发送异常";
                    TroubleLog trouble = new TroubleLog();
                    trouble.TroubleType = TROUBLETYPE.ERR;
                    trouble.TroubleUnit = "设备";
                    trouble.TroubleCode = @"20000";
                    trouble.TroubleInfo = "发送异常数据";// + MachineControlProtocol.ByteArrayToHexString(this.MachineState.SentData); ;
                    myBatis.TroubleLogSave("TroubleLogSave", trouble);
                    break;
                case 0x41://发送数据正确 被确认
                    if (this.IsRunningSchedule == true)
                    {
                        if (this.IsAssayScheduleSent == true)
                        {
                            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " assay task sent successfully，test number:" + this.AssaySchedule.WN);
                            this.ConfirmScheduleTASK(this.AssaySchedule);
                            this.AssaySchedule = null;

                        }
                        InitAssaySchedule();
                        this.IsAssayScheduleSent = false;

                        //任务计数器
                        this.TaskSumCount++;
                        //LogService.Log(string.Format("sent task count：{0}", this.TaskSumCount), LogType.Trace, "TaskCount.lg");
                    }
                    break;
                case 0x1A://初始化程序
                    this.StopLoadTask();
                    this.InitMachineData();
                    this.IsRunningSchedule = false;
                    if (this.MachineState.Command != null && this.MachineState.Command.Name != "Initialize")
                    {
                        this.MachineState.Command = null;
                    }
                    break;
                case 0xE2://光度计检测
                    switch (ParseDictionary[0xE2].Parse(data))
                    {
                        case "PhotometerWrong":
                            switch (this.MachineState.Command.Name)
                            {
                                case "WashByDetergent":
                                case "StartSchedule":
                                    this.MachineState.Command = null;
                                    this.MachineState.State = "光度计异常，任务终止";
                                    myBatis.UpdateIsRunning(false);
                                    break;
                                case "PhotometerAutoCheck":
                                    this.MachineState.Fired = AnalyzeEvent.COMPLETE_PHOTOMETER_AUTOCHECK;
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.State = "光度计异常";
                                    break;
                            }

                            return;
                        case "PhotometerWarn":
                        case "PhotometerRight":
                            if (this.MachineState.Command != null)
                            {
                                this.MachineState.State = "光度计自检通过";
                                switch (this.MachineState.Command.Name)
                                {
                                    case "AbortSchedule":
                                    case "PauseSchedule":
                                    case "WashByDetergent":
                                    case "StartSchedule":
                                        if (this.MachineState.Command.Name == "StartSchedule")
                                        {
                                            myBatis.UpdateIsRunning(true);//工作队列运行标志
                                        }

                                        this.RequestWorkCount = 0;
                                        this.WorkNum = 1;
                                        this.TaskSumCount = 0;
                                        lock (TaskQueue)
                                        {
                                            TaskQueue.Clear();
                                        }

                                        this.AssaySchedule = null;
                                        this.AssayScheduleEncodingData = null;
                                        this.IsAssayScheduleSent = false;
                                        this.LoadTask();
                                        this.Sending("RunSchedule");
                                        break;
                                    case "PhotometerAutoCheck":
                                        this.MachineState.Fired = AnalyzeEvent.COMPLETE_PHOTOMETER_AUTOCHECK;
                                        this.MachineState.Command.State = 2;
                                        this.MachineState.State = "光度计自动校准完成";
                                        break;
                                    default:
                                        break;
                                }
                            }

                            break;
                    }
                    break;
                case 0x08://光栅数据解析
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 0x08 Analyzing!");
                 
                    OnAnalyzeDataBag(data);
                    break;
                case 0x10://请求生化任务或者ISE任务
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff")+" 0x10 ask for task!");
                    this._TasksNumber += 1;
                    if (this.MachineState.Command!=null && this.MachineState.Command.Name == "AbortSchedule")//发送紧急停止
                    {
                        StopSchedule();

                        this.MachineState.State = "放弃测试，请稍等...";
                        this.StopLoadTask();
                        this.InitMachineData();
                        this.IsPauseSchedule = false;
                        this.IsRunningSchedule = false;
                        this.MachineState.Command = null;
                        this.MachineState.Fired = null;//消息驱动置空
                        this.MachineState.State = "完成放弃测试任务";

                        return;
                    }
                    if (this.IsPauseSchedule == true)
                    {
                        this.SerialPort.SendBytesData(EncodeDictionary["0x04"].Encode(null));
                        this.MachineState.State = "测试暂停中...";
                    }
                    else
                    {
                        //if (this.ISESchedule != null)
                        //{
                        //    this.SerialPort.SendBytesData(this.ISEScheduleEncodingData);
                        //    if (this.IsISEScheduleSent == false)
                        //    {
                        //        this.IsISEScheduleSent = true;
                        //        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 0x10 发送ISE任务: " + this.ISESchedule.WN);
                        //    }
                        //    else
                        //    {
                        //        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 警告 0x10 重新发送ISE任务!!!!" + this.ISESchedule.WN);
                        //    }
                        //    RunScheduleState(this.ISESchedule);
                        //}
                        //else
                        //{
                            if (this.AssaySchedule != null)
                            {
                                this.SerialPort.SendBytesData(this.AssayScheduleEncodingData);
                                if (this.IsAssayScheduleSent == false)
                                {
                                    this.IsAssayScheduleSent = true;
                                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 0x10 发送生化任务：" + this.AssaySchedule.WN);
                                }
                                else
                                {
                                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 警告 0x10 重新发送生化任务!!!!" + this.AssaySchedule.WN);
                                }
                                RunScheduleState(this.AssaySchedule);
                            }
                            else
                            {
                                this.SerialPort.SendBytesData(EncodeDictionary["0x04"].Encode(null));
                                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 0x10 发送空包!!!!!");
                                if (this.MachineState.Command != null)
                                {
                                    switch (this.MachineState.Command.Name)
                                    {
                                        case "StartSchedule": this.MachineState.State = "等待测试任务...";
                                            break;
                                        case "WashByDetergent": this.MachineState.State = "系统清洗即将完成"; break;
                                    }
                                }
                            }
                        //}
                    }
                    break;
                case 0x04://请求生化任务
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff")+" 0x04 请求任务!");
                    this.RequestWorkCount += 1;
                    if (this.MachineState.Command!=null && this.MachineState.Command.Name == "AbortSchedule")//发送紧急停止
                    {
                        StopSchedule();

                        this.MachineState.State = this.MachineState.State = "放弃测试，请稍等...";
                        this.StopLoadTask();
                        this.InitMachineData();
                        this.IsPauseSchedule = false;
                        this.IsRunningSchedule = false;
                        this.MachineState.Command = null;
                        this.MachineState.Fired = null;//消息驱动置空
                        this.MachineState.State = "完成放弃测试任务";

                        return;
                    }
                    //发送数据
                    if (this.IsPauseSchedule == true)
                    {
                        this.SerialPort.SendBytesData(EncodeDictionary["0x04"].Encode(null));
                        this.MachineState.State = "测试暂停中...";
                    }
                    else
                    {
                        if (this.AssaySchedule != null)
                        {
                            this.SerialPort.SendBytesData(this.AssayScheduleEncodingData);
                            if (this.IsAssayScheduleSent == false)
                            {
                                this.IsAssayScheduleSent = true;
                                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 0x10 发送生化任务：" + this.AssaySchedule.WN);
                            }
                            else
                            {
                                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 警告 0x04 重新发送生化任务!!!!" + this.AssaySchedule.WN);
                            }
                            RunScheduleState(this.AssaySchedule);
                        }
                        else
                        {
                            this.SerialPort.SendBytesData(EncodeDictionary["0x04"].Encode(null));
                            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 0x04 发送空包!!!!");
                            this.MachineState.State = "等待测试任务...";
                        }
                    }
                    break;
                case 0x1C:
                    this.Sending("ReadTempt");
                    string ercode = ParseDictionary[0x1C].Parse(data);
                    switch (ercode)
                    {
                        case "0E00002": 
                            this.MachineState.Fired = AnalyzeEvent.WATER_REPLE;
                            this.MachineState.Command.State = 3;
                            this.MachineState.State = "正在初始化孵育环境";
                            break;
                    }
                    if (this.MachineState.Command == null)
                    {
                    }
                    else
                    {
                        switch (this.MachineState.Command.Name)
                        {
                            case "Initialize":
                                if (data[2] == 0x45 && data[3] == 0x37)//系统优先启动发送初始化命令
                                {
                                    this.MachineState.Command.State = 3;
                                    this.MachineState.State = "设备复位发生错误";
                                }
                                break;
                            case "WashSampleProbe":
                                if (data[2] == 0x42 && data[3] == 0x30)
                                {
                                    this.MachineState.Command.State = 3;
                                    this.MachineState.State = "清洗样本针发生错误";
                                }
                                break;
                            case "WashCuvette":
                                if (data[2] == 0x45 && data[3] == 0x31)
                                {
                                    this.MachineState.Command.State = 3;
                                    this.MachineState.State = "清洗比色杯发生错误";
                                }
                                break;

                            case "ExChangeWater":
                                if (this.SendCommandFlag == "RunExChangeWater")
                                {
                                    if (data[2] == 0x32 && data[3] == 0x44)
                                    {
                                        this.Sending("ReadTempt");
                                        this.MachineState.State = "检查状态...";
                                    }
                                }
                                break;
                            case "WashByDetergent":
                                if (data[2] == 0x45 && data[3] == 0x35)
                                {
                                    this.MachineState.Command.State = 3;
                                    this.MachineState.State = "系统清洗发生错误";
                                    Console.WriteLine(this.MachineState.State);
                                }
                                break;
                            case "WaterSaved":
                                if (data[2] == 0x30 && data[3] == 0x42)
                                {
                                    this.MachineState.Command.State = 3;
                                    this.MachineState.State = "水保护发生错误";
                                }
                                break;
                            case "CheckWashingLiquid":
                                string code = ercode.Substring(0,5);
                                switch (code)
                                {
                                    case "EE004":
                                         this.MachineState.Fired = AnalyzeEvent.COMPLETE_CHECK_WASHLIQUID;
                                         this.MachineState.Command.State = 3;
                                         this.MachineState.State = "完成检查清洗液";
                                        break;
                                    case "EE005":
                                        this.MachineState.Fired = AnalyzeEvent.COMPLETE_CHECK_WASHLIQUID;
                                        this.MachineState.Command.State = 3;
                                        this.MachineState.State = "完成检查清洗液";
                                        break;
                                    case "EE006":
                                        this.MachineState.Fired = AnalyzeEvent.COMPLETE_CHECK_WASHLIQUID;
                                        this.MachineState.Command.State = 3;
                                        this.MachineState.State = "完成检查清洗液";
                                        break;
                                }
                                break;
                            default:
                                if (this.MachineState.Command.Name == "IgnoreKeyDoRun" || this.MachineState.Command.Name == "StartSchedule" || this.MachineState.Command.Name == "PauseSchedule" || this.MachineState.Command.Name == "AbortSchedule")
                                {

                                }
                                else
                                {
                                    this.MachineState.Command.State = 3;
                                    this.MachineState.State = "发生错误";
                                    
                                }
                                break;
                        }
                    }
                    break;
                case 0x0A://返回比色杯空白
                    string cuvstr = ParseDictionary[0x0A].Parse(data);
                    this.MachineState.State = MachineReturnState.Machine57 + cuvstr;//string.Format("清洗至{0}号比色杯. ", cuvstr);
                    if (cuvstr.Equals("1") == true)
                    {
                        myBatis.UpdateDetergentUsingStartingTime(DateTime.Now);
                    }
                    
                    if (this.MachineState.Command != null && this.MachineState.Command.Name == "PauseWashCuvette")
                    {
                        Sending("AbortSchedule");
                        this.MachineState.State = MachineReturnState.Machine96;
                    }
                    break;
                case 0xE1:
                    //switch (ParseDictionary[0xE1].Parse(data))
                    //{
                    //    case "Finished Washing Cuv":
                    //        this.MachineState.State = MyResources.Instance.FindResource("Machine58").ToString();
                    //        //记录比色杯清洗结时间
                    //        new DetergentVolService().UpdateDetergentUsingFinishingTime(DateTime.Now);
                    //        break;
                    //}
                    break;
                case 0x2B://试剂扫描液面
                    if (this.MachineState.Command != null && this.MachineState.Command.Name == "ExChangeWater")
                    {
                        this.MachineState.State = "完成清洗剂D检查";
                        if (ParseDictionary[0x3B].Parse(data) == null)
                        {
                            this.SendCommandFlag = "RunExChangeWater";
                            this.MachineState.State = "开始水交换...";
                            Sending("RunExChangeWater");
                        }
                        else
                        {
                            this.MachineState.Fired = AnalyzeEvent.EXXCHANGE_WARER_FAILED;
                            this.MachineState.Command.State = 3;
                            this.MachineState.State = "清洗剂D不足水交换未完成";
                        }
                    }
                    else
                    {
                        ParseDictionary[0x2B].Parse(data);
                        this.MachineState.Command.State = 2;
                        this.MachineState.State = "完成试剂余量检查";
                    }
                    break;
                case 0x1D:
                    if (this.MachineState.Command == null)
                    {
                        if (data[2] == 0x45 && data[3] == 0x37)//设备优先启动
                        {
                            this.MachineState.Fired = AnalyzeEvent.MACHINE_RESET;
                            this.MachineState.State = "设备完成复位";
                        }
                    }
                    else
                    {
                        switch (this.MachineState.Command.Name)
                        {
                            case "Initialize":
                                if (data[2] == 0x45 && data[3] == 0x37)//系统优先启动发送初始化命令
                                {
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.Fired = AnalyzeEvent.MACHINE_RESET;
                                    this.MachineState.State = "完成复位";
                                }
                                break;
                            case "WashSampleProbe":
                                if (data[2] == 0x42 && data[3] == 0x30)
                                {
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.State = "完成清洗样本针";
                                }
                                break;
                            case "WashCuvette":
                                if (data[2] == 0x45 && data[3] == 0x31)
                                {
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.State = "完成清洗比色杯";
                                    //记录比色杯清洗结时间
                                    myBatis.UpdateDetergentUsingFinishingTime(DateTime.Now);
                                }
                                break;

                            case "ExChangeWater":
                                if (this.SendCommandFlag == "RunExChangeWater")
                                {
                                    if (data[2] == 0x32 && data[3] == 0x44)
                                    {
                                        this.Sending("ReadTempt");
                                        this.MachineState.State = "检查状态...";
                                    }
                                }
                                break;
                            case "WashByDetergent":
                                if (data[2] == 0x45 && data[3] == 0x35)
                                {
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.State = "完成系统清洗";
                                    Console.WriteLine(this.MachineState.State);
                                }
                                break;
                            case "WaterSaved":
                                if (data[2] == 0x30 && data[3] == 0x42)
                                {
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.State = "完成水保护";
                                }
                                break;
                            case "CheckWashingLiquid":
                                //接收5
                                if (data[2] == 0x45 && data[3] == 0x45)
                                {
                                    this.MachineState.Fired = AnalyzeEvent.COMPLETE_CHECK_WASHLIQUID;
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.State = "完成检查清洗液";
                                }
                                break;
                            default:
                                if (this.MachineState.Command.Name == "IgnoreKeyDoRun" || this.MachineState.Command.Name == "StartSchedule" || this.MachineState.Command.Name == "PauseSchedule" || this.MachineState.Command.Name == "AbortSchedule")
                                {

                                }
                                else
                                {
                                    this.MachineState.Command.State = 2;
                                    this.MachineState.State = "完成执行";
                                }
                                break;
                        }
                    }
                    break;
                case 0x27:
                    ParseDictionary[0x27].Parse(data);
                    this.MachineState.Command.State = 2;
                    this.MachineState.State = "扫描光栅中...";
                    break;
                case 0x09://读取机器码
                    switch (data[2])
                    {
                        case 0x31:
                            //接收4
                            this.MachineState.StateValue = null;
                            this.MachineState.StateValue = ParseDictionary[0x090].Parse(data);
                            this.MachineState.Fired = AnalyzeEvent.COMPLETED_READ_ACTIVEKEY;
                            this.MachineState.Command.State = 2;
                            this.MachineNumber = this.MachineState.StateValue;//设备SN号
                            this.MachineState.State = "完成读取激活码.";
                            //this.Sending("CheckWashingLiquid");
                            break;
                        case 0x33:
                            // 接收3
                            this.MachineNumber = null;
                            this.MachineState.StateValue = null;
                            this.MachineState.StateValue = ParseDictionary[0x092].Parse(data);
                            this.MachineState.Fired = AnalyzeEvent.COMPLETED_READ_SN;
                            this.MachineState.Command.State = 2;
                            this.MachineNumber = this.MachineState.StateValue;//设备SN号
                            this.MachineState.State = "完成读取SN码.";
                            //this.Sending("ReadActivityCode");
                            break;
                        case 0x35:
                            //接收2
                            string msn = ParseDictionary[0x094].Parse(data);
                            string csn = msn;//WMShut.GetLocalMACValue();
                            if (msn == csn)
                            {
                                this.MachineState.Fired = AnalyzeEvent.WORKSTATION_MATCHING;
                                this.MachineState.Command.State = 2;
                                this.MachineState.State = "系统校验通过.";
                                //this.Sending("ReadSN");
                            }
                            else
                            {
                                Console.WriteLine("msn:" + msn + " csn:" + csn);
                                this.MachineState.Fired = AnalyzeEvent.WORKSTATION_NOMATCHING;
                                this.MachineState.Command.State = 2;
                                this.MachineState.State = "系统校验失败.";
                            }
                            break;
                        case 0x3f:
                            // 接收1
                            string vsr = ParseDictionary[0x95].Parse(data);
                            this.MachineState.StateValue = vsr;
                            this.MachineState.Fired = AnalyzeEvent.COMPLETED_SCAN_HWVersion;
                            this.MachineState.Command.State = 2;
                            this.MachineState.State = "获取硬件信息.";
                            this.MachineState.Fired = null;//消息驱动置空
                            //this.Sending("ReadLicense");
                            break;
                        //case 0x3c:
                        //    string str3c = ParseDictionary[0x096].Parse(data);
                        //    this.MachineState.StateValue = str3c;
                        //    this.MachineState.Fired = AnalyzeEvent.COMPLETED_SCAN_MSTATE;
                        //    this.MachineState.Command.State = 2;
                        //    this.MachineState.State = MyResources.Instance.FindResource("Machine97").ToString();
                        //    this.MachineState.Fired = null;
                        //    break;
                    }
                    break;
                case 0x03:
                    //if (this.MachineState.Command != null && this.MachineState.Command.Name == "StopWashByDetergent")
                    //{
                    //    this.Sending("StopWashByDetergent");
                    //}
                    //this.MachineState.State = MyResources.Instance.FindResource("Machine98").ToString();
                    break;
                case 0xC5://试剂条码
                    //string str = ParseDictionary[0xC5].Parse(data);
                    //string[] sv = str.Split('|');
                    //this.MachineState.StateValue = str;
                    //this.MachineState.Command.State = 2;
                    //this.MachineState.Fired = AnalyzeEvent.COMPLETED_SCAN_RGTBarcode;
                    //string strrgt = MyResources.Instance.FindResource("Machine81").ToString() + sv[1] + MyResources.Instance.FindResource("Machine82").ToString();
                    //this.MachineState.State = strrgt;
                    //this.MachineState.Fired = null;//消息驱动置空
                    break;
                case 0x45://样本条码
                    //string s1 = ParseDictionary[0x45].Parse(data);
                    //string[] s1v = s1.Split('|');
                    //this.MachineState.StateValue = s1;
                    //this.MachineState.Fired = AnalyzeEvent.COMPLETED_SCAN_SMPBarcode;
                    //this.MachineState.Command.State = 2;
                    //string strsmpbar = MyResources.Instance.FindResource("Machine83").ToString() + s1v[1] + MyResources.Instance.FindResource("Machine82").ToString();
                    //this.MachineState.State = strsmpbar;
                    //this.MachineState.Fired = null;//消息驱动置空
                    break;
                case 0xAB://ISE模块分析参数设置
                    //ISECalParaSet ISECalParaSet = null;
                    //switch (data[2])
                    //{
                    //    case 0x31:
                    //        ISECalParaSet = new ISECalParaSetService().GetISECalParaSet("S");
                    //        break;//血清
                    //    case 0x32:
                    //        ISECalParaSet = new ISECalParaSetService().GetISECalParaSet("U");
                    //        break;//尿液
                    //}
                    //this.SerialPort.SendBytesData(EncodeDictionary["0xAB"].Encode(ISECalParaSet));
                    break;
                case 0xAA://初始化ISE模块校准参数设置
                    //ISEItemSDTTable ISEItemSDTTable = null;
                    //switch (data[2])
                    //{
                    //    case 0x31:
                    //        ISEItemSDTTable = new ISEItemSDTTableService().GetUsingISEItemSDTTable("S");
                    //        break;//血清
                    //    case 0x32:
                    //        ISEItemSDTTable = new ISEItemSDTTableService().GetUsingISEItemSDTTable("U");
                    //        break;//尿液
                    //}
                    //this.SerialPort.SendBytesData(EncodeDictionary["0xAA"].Encode(ISEItemSDTTable));
                    break;
                case 0xAD://ISE校准数据解析
                    //ParseDictionary[0xAD].Parse(data);
                    //ISERGTService TService = new ISERGTService();
                    //TService.UpdateBufferSolutionCount(TService.GetBufferSolutionCount() - ISERGTService.BufferUsingVolume);
                    //TService.UpdateInternalStandardCount(TService.GetInternalStandardCount() - ISERGTService.InternalStandardUsingVolume);
                    break;
                case 0xA9://ISE模块校准数据解析
                    ParseDictionary[0xA9].Parse(data);
                    break;
                case 0x0B:
                    ParseDictionary[0x0B].Parse(data);
                    break;
                case 0x11://比色杯脏
                    this.MachineState.State = ParseDictionary[0x11].Parse(data);
                    break;
                default:
                    Console.WriteLine("Key:" + string.Format(@"{0,2:X}", Key) + "is unkown!");
                    break;
            }
        }
        #endregion


        private bool IsWaterExchangeEnable(int state1, int state2)
        {
            //高位状态码
            int s1 = state1 - 0x30;

            //纯水槽低位浮球
            if ((s1 & 0x01) == 0x01)
            {
                return false;
            }

            //反应槽液位
            if ((s1 & 0x02) == 0x02)
            {
                return false;
            }

            //溢流罐液位报警
            if ((s1 & 0x04) == 0x04)
            {
                return false;
            }

            //真空罐液位报警
            if ((s1 & 0x08) == 0x08)
            {
                return false;
            }

            //低位状态码
            int s2 = state2 - 0x30;

            //恒温槽浮球错误
            if ((s2 & 0x04) == 0x04)
            {
                return false;
            }

            //纯水槽高位浮球
            if ((s2 & 0x08) == 0x08)
            {
                return false;
            }

            return true;
        }

        void RunScheduleState(ScheduleTASK scheduleTask)
        {
            if (this.MachineState.Command == null) return;

            switch (this.MachineState.Command.Name)
            {
                case "WashByDetergent":
                    if (scheduleTask.WN == 0)
                    {
                        this.MachineState.State = "系统清洗即将完成...";
                    }
                    else
                    {
                        this.MachineState.State = "系统正清洗中...";
                    }
                    break;
                case "StartSchedule":
                    if (scheduleTask == null)
                    {
                        this.MachineState.State = "等待任务...";
                        return;
                    }

                    if (scheduleTask.WN == 0)
                    {
                        this.MachineState.State = "等待任务...";
                    }
                    else
                    {
                        switch (scheduleTask.T.PT)
                        {
                            case 0: this.MachineState.State = "常规" + string.Format("{0} {1}", scheduleTask.T.SMPNO, scheduleTask.T.ASSAY); break;
                            case 1: this.MachineState.State = "急诊" + string.Format("{0} {1}", scheduleTask.T.SMPNO, scheduleTask.T.ASSAY); break;
                            case 2: this.MachineState.State = "空白" + string.Format("{0} {1}", scheduleTask.T.SMPNO, scheduleTask.T.ASSAY); break;
                            case 3: this.MachineState.State = "标准" + string.Format("{0} {1}", scheduleTask.T.SMPNO, scheduleTask.T.ASSAY); break;
                            case 4: this.MachineState.State = "质控" + string.Format("{0} {1}", scheduleTask.T.SMPNO, scheduleTask.T.ASSAY); break;
                            case 5: this.MachineState.State = "离子" + string.Format("{0} {1}", scheduleTask.T.SMPNO, scheduleTask.T.ASSAY); break;
                        }
                    }
                    break;
                case "PauseSchedule":
                    this.MachineState.State = "测试暂停...";
                    break;
            }
        }

        //部件移动前后命令
        string CurAdjustNodeCommand = null;
        string PreAdjustNodeCommand = null;
        string PrePreAdjustNodeCommand = null;
        int CommandOffsetCount = 0;


        void Sending(string name)
        {
            try
            {
                CommandProtocol cp = CommandProtocol[name];
                switch (cp.Offset)
                {
                    case 1:
                    case -1:
                        this.CommandOffsetCount += cp.Offset;
                        //GW
                        if (CommandOffsetCount > 15)
                            CommandOffsetCount = 15;
                        if (CommandOffsetCount < -15)
                            CommandOffsetCount = -15;
                        //
                        break;
                }
                if (cp.IsAdjustNode == true)
                {
                    this.PrePreAdjustNodeCommand = this.PreAdjustNodeCommand;
                    this.PreAdjustNodeCommand = this.CurAdjustNodeCommand;
                    this.CurAdjustNodeCommand = name;
                    //当在可以校准节点，校准记录器置零
                    this.CommandOffsetCount = 0;
                }

                if (string.IsNullOrEmpty(cp.Encoder) || string.IsNullOrWhiteSpace(cp.Encoder))
                {
                    this.MachineState.SentData = CheckSum(cp.Protocol);
                }
                else
                {
                    switch (cp.EncodingObject)
                    {
                        //case "Communication":
                        //    this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(null);
                        //    break;
                        case "AdjustNode":
                            AdjustNode an = new AdjustNode();
                            an.OffsetCount = this.CommandOffsetCount;
                            an.NodeCode = GetAdjustSaveKey(CurAdjustNodeCommand, PreAdjustNodeCommand, PrePreAdjustNodeCommand);
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(an);
                            this.CommandOffsetCount = 0;//置零。
                            break;
                        case "Offset":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(null);
                            break;
                        case "RGTPanel1Positions":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "RGTPanel2Positions":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "SMPPanelPositions":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "Key":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode("key");
                            break;
                        case "SN":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "License":
                            //this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(WMShut.GetLocalMACValue());
                            break;
                        case "ISESMPType":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "Temp":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "ISEMNT":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "WashBySetting":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "AdjustSave":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                        case "AdjustNotSave":
                            this.MachineState.SentData = EncodeDictionary[cp.Encoder].Encode(this.MachineState.Command.Para);
                            break;
                    }
                }

                this.SerialPort.SendBytesData(this.MachineState.SentData);
            }
            
            catch (Exception e)
            {
                Console.WriteLine(@"COMMAND[{0}]:" + e.Message, name);
                return;
            }
        }
        byte[] CheckSum(byte[] c)
        {
            byte[] bytes = c;
            if (bytes.Count() > 2)
            {
                byte[] checksum = MachineControlProtocol.CheckSum(bytes);
                bytes[bytes.Count() - 2] = checksum[0];
                bytes[bytes.Count() - 1] = checksum[1];
            }
            return bytes;
        }
        //记录请求任务量
        long _RequestWorkCount = 0;
        long RequestWorkCount
        {
            get { return _RequestWorkCount; }
            set
            {
                _RequestWorkCount = value;
                if (_RequestWorkCount==20)
                {
                    //LogService.Log("RequestWorkCount==20", LogType.Trace, "detergentvol.lg");

                    //new DetergentVolService().UpdateDetergentUsingFinishingTime(DateTime.Now);
                    //new DetergentVolService().UpdateDetergentUsingStartingTime(DateTime.Now);
                    _RequestWorkCount = 0;

                    
                }
            }
        }
        //任务调度服务
        int WorkNum = 0;
        bool IsRunningSchedule = false;
        Queue<TASK> TaskQueue = new Queue<TASK>();
        //ISE任务队列
        //Queue<TASK> ISETaskQueue = new Queue<TASK>();
        ManualResetEvent TaskQueueSignal = new ManualResetEvent(false);
        //待发送的生化任务
        bool IsAssayScheduleSent = false;
        byte[] AssayScheduleEncodingData = null;
        ScheduleTASK AssaySchedule = null;
        void InitAssaySchedule()
        {
            //生化任务数据
            if (this.AssaySchedule == null)
            {
                if (this.TaskQueue.Count > 0)
                {
                    TASK T = null;
                    lock (this.TaskQueue)
                    {
                        T = this.TaskQueue.Dequeue();
                    }
                    //修正试剂位，应用于多试剂位
                    bool R1Flag = true;
                    bool R2Flag = true;

                    
                    int v = 0;
                    // 获取试剂状态信息、项目参数信息、最小试剂体积、报警试剂体积
                    AssayProjectParamInfo assayProParam = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = T.ASSAY, SampleType = T.SAMPLETYPE });
                    ReagentStateInfoR1R2 reagentState = myBatis.QueryReagentStateInfoByProjectName("QueryReagentStateInfoByProjectName", new ReagentSettingsInfo() { ProjectName = T.ASSAY, ReagentType = T.SAMPLETYPE });
                    float warningReaVol = myBatis.GetRgtWarnCount();
                    float LeastReaVol = myBatis.GetRgtLeastCount();

                    ReagentSettingsInfo R1SettingInfo = myBatis.GetReagentSettingsInfo(T.ASSAY, T.SAMPLETYPE);
                    if (R1SettingInfo != null && reagentState != null && assayProParam != null)
                    {
                        v = System.Convert.ToInt32(R1SettingInfo.ReagentContainer.Substring(0, R1SettingInfo.ReagentContainer.IndexOf("ml"))) * reagentState.ValidPercent * 1000 / 100;

                        T.R1POS = int.Parse(reagentState.Pos);

                        int c1 = assayProParam.Reagent1VolSettings == 0 ? 0 : v / assayProParam.Reagent1VolSettings;
                        if (c1 < LeastReaVol)
                        {
                            R1Flag = false;
                        }

                        if (R1SettingInfo.Locked == true)
                        {
                            R1Flag = false;
                        }
                    }

                    //试剂2具有指令延迟效应，所以多试剂判读不同于R1
                    ReagentSettingsInfo R2SettingInfo = myBatis.GetReagentSettingsInfo2(T.ASSAY, T.SAMPLETYPE);
                    if (R2SettingInfo != null && reagentState != null && assayProParam != null)
                    {
                        T.R2POS = int.Parse(reagentState.Pos2);
                        int c2 = reagentState.ValidPercent2;//试剂2液体

                        if (R2SettingInfo.Locked == true)
                        {
                            R2Flag = false;//试剂2
                        }

                        if (c2 < 5 && c2 > 3)
                        {
                            TroubleLog trouble = new TroubleLog();
                            trouble.TroubleCode = @"0000775";
                            trouble.TroubleType = TROUBLETYPE.WARN;
                            trouble.TroubleUnit = @"设备";
                            trouble.TroubleInfo = string.Format("试剂位{0}项目{1}:试剂2余量即将耗尽. ", T.R2POS, T.ASSAY);
                            myBatis.TroubleLogSave("TroubleLogSave", trouble);
                        }
                        //项目量小于最小量
                        if (c2 < 3)
                        {
                            R2Flag = false;//试剂2

                            if (myBatis.BAutoFreezeTaskByReagentVolWarning() == true)
                            {
                                R2SettingInfo.Locked = true;
                                myBatis.UpdateLockState("R2", R2SettingInfo);

                                TroubleLog trouble = new TroubleLog();
                                trouble.TroubleCode = @"0000773";
                                trouble.TroubleType = TROUBLETYPE.WARN;
                                trouble.TroubleUnit = @"试剂";
                                trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂2由于余量不足将锁定其对应的工作表. ", T.R2POS, R2SettingInfo.ProjectName);
                                myBatis.TroubleLogSave("TroubleLogSave", trouble);
                            }
                            else
                            {
                                TroubleLog trouble = new TroubleLog();
                                trouble.TroubleCode = @"0000773";
                                trouble.TroubleType = TROUBLETYPE.ERR;
                                trouble.TroubleUnit = @"试剂";
                                trouble.TroubleInfo = string.Format("试剂位{0}项目{1}试剂2余量不足. ", T.R2POS, R2SettingInfo.ProjectName);
                                myBatis.TroubleLogSave("TroubleLogSave", trouble);
                            }

                            //切换多试剂位...
                            //if (new RunService().IsMutiRgtEnable() == true)
                            //{
                            //    RGTPosition mrgt = RGTPOSMgr.GetEnableMutiRgtPosition(rgtp);
                            //    if (mrgt != null)
                            //    {
                            //        RGTPOSMgr.BetweenMutiRgtPositionAndRgtPositionChange(mrgt, rgtp);

                            //        TroubleLog trouble = new TroubleLog();
                            //        trouble.TroubleCode = @"0000773";
                            //        trouble.TroubleType = TROUBLETYPE.WARN;
                            //        trouble.TroubleUnit = @"试剂";
                            //        trouble.TroubleInfo = string.Format("试剂2试剂位{0}项目{1}由于余量不足开始启用其多试剂位{2}. ", T.R2POS, rgtp.Assay, mrgt.Position);
                            //        new TroubleLogService().Save(trouble);
                            //    }
                            //}
                        }

                        if (R2SettingInfo.Locked == true)
                        {
                            R2Flag = false;
                        }

                        //由于消耗提前,更新R2试剂余量
                        Reagent2UsingCountInfo items = myBatis.GetReagent2UsingCountInfo(assayProParam.ProjectName, 2);
                        if (items == null)
                        {
                            myBatis.Insert(assayProParam.ProjectName, 2, 1);
                        }
                        else
                        {
                            myBatis.UpdateReagent2UsingCount(items.Count + 1, assayProParam.ProjectName);
                            double d1 = (double)(items.Count * assayProParam.Reagent2VolSettings);
                            double d2 = (double)(System.Convert.ToInt32(R2SettingInfo.ReagentContainer.Substring(0, R2SettingInfo.ReagentContainer.IndexOf("ml"))) * 1000);
                            double f1 = d1 / d2;
                            int z = (int)(f1 * 100);
                            if (z > 0)
                            {
                                //更新R2余量
                                myBatis.UpdateReagentValidPercent(c2 - z, 2, T.R2POS);
                                myBatis.UpdateReagent2UsingCount(0, assayProParam.ProjectName);
                            }
                            
                        }
                    }
                    //校验任务队列试剂余量数据
                    if (R1Flag == false || R2Flag == false)
                    {
                        if (this.TaskQueue.Count > 0)
                        {
                            if (this.TaskQueue.First().ASSAY == T.ASSAY)
                            {
                                lock (this.TaskQueue)
                                {
                                    this.TaskQueue.Clear();
                                }

                                Console.WriteLine("试剂余量不足,封闭状态下清除队列");
                            }
                        }
                    }

                    this.AssaySchedule = new ScheduleTASK();
                    this.AssaySchedule.WN = this.WorkNum;
                    this.AssaySchedule.T = T;
                    this.AssayScheduleEncodingData = EncodeDictionary["0x04"].Encode(this.AssaySchedule);
                    Console.WriteLine(string.Format(DateTime.Now.ToString("HH:mm:ss.fff") + " 新建生化任务,测试编号{0}", this.AssaySchedule.WN));
                    this.WorkNum++;
                    this.WorkNum = this.WorkNum > 999 ? 1 : this.WorkNum;
                }
                else
                {
                    this.AssaySchedule = null;
                }
            }
        }
        
        int ScheduleSignalCount = 0;
        void TaskQueueService()
        {
            List<TASK> tasks = null;
            while (true)
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " The schedule is start to running....");

                TaskQueueSignal.WaitOne();
               
                bool IsHavingNORTask = false;
                               
                tasks = LoadSDTSchedule();
                foreach (TASK e in tasks)
                {
                    lock (this.TaskQueue)
                    {
                        this.TaskQueue.Enqueue(e);
                    }
                }

                if (this.TaskQueue.Count == 0)
                {
                    tasks = LoadQCTask(1);
                    foreach (TASK e in tasks)
                    {
                        lock (this.TaskQueue)
                        {
                            this.TaskQueue.Enqueue(e);
                        }
                    }
                }

                if (this.TaskQueue.Count ==0)
                {
                    tasks = LoadEMGTask(1);
                    foreach (TASK e in tasks)
                    {
                        lock (this.TaskQueue)
                        {
                            this.TaskQueue.Enqueue(e);
                            IsHavingNORTask = true;
                        }
                    }
                }
                tasks = null;


                if (this.TaskQueue.Count == 0)
                {
                    tasks = LoadNorTask(1);
                    foreach (TASK e in tasks)
                    {
                        lock (this.TaskQueue)
                        {
                            this.TaskQueue.Enqueue(e);
                            IsHavingNORTask = true;
                        }
                    }
                }
                tasks = null;


                if (this.TaskQueue.Count> 0)
                {
                    ScheduleSignalCount = 0;
                }
                else
                {
                    ScheduleSignalCount++;
                }
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + string.Format(" 当前生化任务量：{0}", this.TaskQueue.Count));
                
                if (this.WorkNum == 1)
                {
                    InitAssaySchedule();
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 初始化生化任务。");
                }

                
                tasks = null;


                //自动切换逻辑盘
                //if (IsHavingNORTask == true)
                //{
                //    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 自动切换逻辑盘....");
                //    int curdisk = myBatis.GetWorkingDisk();
                //    int count = myBatis.GetNOStartTaskByWorkDisk(curdisk);
                //    if (count <= 0)
                //    {
                //        curdisk = curdisk >= 20 ? 1 : curdisk + 1;
                //        myBatis.UpdateWorkingDisk(curdisk);
                //        if (this.MachineState.Command != null)
                //        {
                //            switch (this.MachineState.Command.Name)
                //            {
                //                case "StartSchedule":
                //                    this.MachineState.State = "工作盘符切换至:" + curdisk.ToString();//string.Format(@"工作盘符切换至{0}", curdisk);
                //                    break;
                //            }
                //        }
                //    }
                //}else

                //手动切换逻辑盘
                if (IsHavingNORTask == false)
                {
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " 提醒切换逻辑盘...." + ScheduleSignalCount);

                    //检查其他样本
                    // int timeout = new WorkDiskSetService().GetWorkDiskExchangeTimeout();
                    if (ScheduleSignalCount > 1000 * 2)
                    {
                        int curdisk = myBatis.GetWorkingDisk();
                        List<int> disks = myBatis.GetHasSchedulesWorkDisk(curdisk);
                        if (disks.Count > 0)
                        {
                            string diskstr = "";
                            foreach (int e in disks)
                            {
                                diskstr += e + "|";
                            }
                            this.MachineState.StateValue = diskstr;
                            this.MachineState.Fired = AnalyzeEvent.HAS_SCHEDULE_WARNNING;
                            this.MachineState.State = @"切换需要的工作盘";

                            this.MachineState.Fired = null;//消息驱动置空
                        }

                        ScheduleSignalCount = 0;
                    }
                }
                
            }
        }
        /// <summary>
        /// 处理下校准任务的数据
        /// </summary>
        /// <returns>返回校准任务数据</returns>
        List<TASK> LoadSDTSchedule()
        {
            //SDTTableItemService SDTTableItemSer = new SDTTableItemService();
            AssayProjectCalibrationParamInfo sdttable = myBatis.GetCalibParamBySDTTask();
            if (sdttable == null)
            {
                List<TASK> ListTasks = new List<TASK>();
                List<TASK> ResultListTasks = new List<TASK>();
                List<CalibratorinfoTask> calibrationTasks = myBatis.GetCalibInfoTaskByCalibTaskState();
                if (calibrationTasks.Count == 0)
                {
                    return ResultListTasks;
                }
                else
                {
                    foreach (CalibratorinfoTask Tasks in calibrationTasks)
                    {
                        ListTasks = LoadSDTTask(Tasks);
                    }
                    foreach (TASK resultTask in ListTasks)
                    {
                        ResultListTasks.Add(resultTask);
                    }
                    return ResultListTasks;
                }
            }

            List<CalibratorinfoTask> calibTasks = myBatis.GetCalibTasksByProject(sdttable.ProjectName, sdttable.SampleType);

            List<TASK> SdtblkTasks = new List<TASK>();
            List<TASK> SdtS1Tasks = new List<TASK>();
            List<TASK> SdtS2Tasks = new List<TASK>();
            List<TASK> SdtS3Tasks = new List<TASK>();
            List<TASK> SdtS4Tasks = new List<TASK>();
            List<TASK> SdtS5Tasks = new List<TASK>();
            List<TASK> SdtS6Tasks = new List<TASK>();

            //条件判断，相等就进， 不相等就过
            if (calibTasks.Exists(x => sdttable.CalibName0 == x.CalibName))
            {
                //处理任务数据

                SdtblkTasks = LoadSDTTask(calibTasks.Find(x => sdttable.CalibName0 == x.CalibName));
            }
            if (calibTasks.Exists(x => sdttable.CalibName1 == x.CalibName))
            {
                SdtS1Tasks = LoadSDTTask(calibTasks.Find(x => sdttable.CalibName1 == x.CalibName));
            }
            if (calibTasks.Exists(x => sdttable.CalibName2 == x.CalibName))
            {
                SdtS2Tasks = LoadSDTTask(calibTasks.Find(x => sdttable.CalibName2 == x.CalibName));
            }
            if (calibTasks.Exists(x => sdttable.CalibName3 == x.CalibName))
            {
                SdtS3Tasks = LoadSDTTask(calibTasks.Find(x => sdttable.CalibName3 == x.CalibName));
            }
            if (calibTasks.Exists(x => sdttable.CalibName4 == x.CalibName))
            {
                SdtS4Tasks = LoadSDTTask(calibTasks.Find(x => sdttable.CalibName4 == x.CalibName));
            }
            if (calibTasks.Exists(x => sdttable.CalibName5 == x.CalibName))
            {
                SdtS5Tasks = LoadSDTTask(calibTasks.Find(x => sdttable.CalibName5 == x.CalibName));
            }
            if (calibTasks.Exists(x => sdttable.CalibName6 == x.CalibName))
            {
                SdtS6Tasks = LoadSDTTask(calibTasks.Find(x => sdttable.CalibName6 == x.CalibName));
            }

            //修改校准拟合表中的状态为（CALIBRATING）
            myBatis.SetCalibratingCurveState(sdttable.ProjectName, sdttable.SampleType);

            List<TASK> tasks = new List<TASK>();
            foreach (TASK e in SdtblkTasks)
            {
                tasks.Add(e);
            }
            foreach (TASK e in SdtS1Tasks)
            {
                tasks.Add(e);
            }
            foreach (TASK e in SdtS2Tasks)
            {
                tasks.Add(e);
            }
            foreach (TASK e in SdtS3Tasks)
            {
                tasks.Add(e);
            }
            foreach (TASK e in SdtS4Tasks)
            {
                tasks.Add(e);
            }
            foreach (TASK e in SdtS5Tasks)
            {
                tasks.Add(e);
            }
            foreach (TASK e in SdtS6Tasks)
            {
                tasks.Add(e);
            }

            return tasks;
        }
        List<TASK> LoadSDTTask(CalibratorinfoTask SdtSchedule)
        {
            if (SdtSchedule != null)
            {
                myBatis.UpdateSDTSchedulePerform(SdtSchedule, TaskState.START);
                myBatis.UpdateSDTResultState(SdtSchedule, TaskState.START);
                return CreatCalibTaskList(SdtSchedule);
            }
            return new List<TASK>();
        }
        List<TASK> CreatCalibTaskList(CalibratorinfoTask S)
        {
            List<TASK> tasks = new List<TASK>();
            try
            {
                TASK t = new TASK();

                AssayProjectParamInfo assayProParam = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = S.ProjectName, SampleType = S.SampleType });
                t.PW = assayProParam.MainWaveLength;
                t.SW = assayProParam.SecWaveLength;
                //t.SF1 = System.Convert.ToInt32(assayProParam.Stirring1Intensity);//搅拌强度
                //t.SF2 = System.Convert.ToInt32(assayProParam.Stirring2Intensity);//搅拌强度
                switch (assayProParam.Stirring1Intensity) // 搅拌1强度
                {
                    case "低":
                        t.SF1 = 1;
                        break;
                    case "中":
                        t.SF1 = 2;
                        break;
                    case "高":
                        t.SF1 = 3;
                        break;
                    default:
                        t.SF1 = 0;
                        break;
                }
                switch (assayProParam.Stirring2Intensity) // 搅拌2强度
                {
                    case "低":
                        t.SF2 = 1;
                        break;
                    case "中":
                        t.SF2 = 2;
                        break;
                    case "高":
                        t.SF2 = 3;
                        break;
                    default:
                        t.SF2 = 0;
                        break;
                }
                t.PPNO = myBatis.QueryProjectRunSequenceByProject(new AssayProjectInfo() { ProjectName = S.ProjectName, SampleType = S.SampleType}).RunSequence;
                t.R1VOL = assayProParam.Reagent1VolSettings;
                t.R2VOL = assayProParam.Reagent2VolSettings;

                t.CALIBNAME = S.CalibName;
                t.SMPNO = S.SampleNum;
                t.ASSAY = assayProParam.ProjectName;
                t.SAMPLETYPE = assayProParam.SampleType;
                t.CalibDate = S.CreateDate;
                t.R1POS = System.Convert.ToInt32(assayProParam.Reagent1Pos == "" ? "0" : assayProParam.Reagent1Pos);
                t.R2POS = System.Convert.ToInt32(assayProParam.Reagent2Pos == "" ? "0" : assayProParam.Reagent2Pos);
                
                t.DPOS = System.Convert.ToInt32(myBatis.GetRGTDilutePosition());

                t.DISK = myBatis.GetWorkingDisk().ToString();
                t.SMPPOS = myBatis.QueryCalib("QueryCalib", S.CalibName)[0].Pos;
                if (t.SMPPOS.Substring(0, 1) == "B")
                    t.PT = 2;
                else
                    t.PT = 3;
                t.CT = (myBatis.SMPContainerType(myBatis.GetSDTSMPContainerType())).Code;

                t.PV = (int)(assayProParam.CalibStosteVol * 10);
                t.V = (int)(assayProParam.CalibSamVol * 10);
                t.DV = (int)(assayProParam.CalibDilutionVol);
                t.VOLTYPE = VOLTYPE.SV;

                for (int i = 1; i <= S.InspectTimes - S.SendTimes; i++)
                {
                    tasks.Add(t);
                }
            }
            catch(Exception e)
            {

            }

            return tasks;
        }
        TASK CreateDetergentASchedule()
        {
            TASK t = new TASK();

            t.PW = 340;
            t.SW = 0;
            t.PPNO = 0;
            t.R1VOL = 180;
            t.R2VOL = 160;

            t.SMPNO = "A";
            t.ASSAY = "A";

            t.R1POS = 29;
            t.R2POS = 29;
            t.DPOS = 0;

            t.PT = 0;
            t.DISK = "1";
            t.SMPPOS = "1";//碱性溶液
            t.RACK = null;

            t.CT = 1;

            t.PV = 10;
            t.V = 0;
            t.DV = 0;
            t.VOLTYPE = VOLTYPE.NV;

            return t;
        }
        TASK CreateDetergentBSchedule()
        {
            TASK t = new TASK();

            t.PW = 340;
            t.SW = 0;
            t.PPNO = 0;
            t.R1VOL = 180;
            t.R2VOL = 160;

            t.SMPNO = "B";
            t.ASSAY = "B";

            t.R1POS = 28;
            t.R2POS = 28;
            t.DPOS = 0;

            t.PT = 0;
            t.DISK = "1";
            t.SMPPOS = "2";//酸性溶液
            t.RACK = null;

            t.CT = 1;

            t.PV = 10;
            t.V = 0;
            t.DV = 0;
            t.VOLTYPE = VOLTYPE.NV;

            return t;

        }
        ////
        bool HasCleared = false;
        void LoadClearSchedule()
        {
            if (TaskQueue.Count > 0 || HasCleared == true)
            {
                return;
            }

            TASK t = this.CreateDetergentASchedule();
            for (int i = 0; i < RunConfigureUtility.CUVCount; i++)
            {
                lock (TaskQueue)
                {
                    TaskQueue.Enqueue(t);
                }
            }

            t = this.CreateDetergentBSchedule();
            for (int i = 0; i < RunConfigureUtility.CUVCount; i++)
            {
                lock (TaskQueue)
                {
                    TaskQueue.Enqueue(t);
                }
            }

            HasCleared = true;
        }
        /// <summary>
        /// 处理下质控任务数据
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        List<TASK> LoadQCTask(int count)
        {
            List<TASK> Tasks = new List<TASK>();

            List<QCTaskInfo> QCSchedules = myBatis.GetQCParamByQCTask(count);
            foreach (QCTaskInfo e in QCSchedules)
            {
                myBatis.UpdateQCSchedulePerform(e, TaskState.START);
                List<TASK> etasks = CreatQCTaskList(e);
                foreach (TASK t in etasks)
                {
                    Tasks.Add(t);
                }
            }

            return Tasks;
        }
        List<TASK> CreatQCTaskList(QCTaskInfo qcTask)
        {
            List<TASK> tasks = new List<TASK>();
            try
            {
                TASK t = new TASK();

                AssayProjectParamInfo assayProParam = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = qcTask.ProjectName, SampleType = qcTask.SampleType });
                t.PW = assayProParam.MainWaveLength;
                t.SW = assayProParam.SecWaveLength;
                //t.SF1 = System.Convert.ToInt32(assayProParam.Stirring1Intensity);//搅拌强度
                //t.SF2 = System.Convert.ToInt32(assayProParam.Stirring2Intensity);//搅拌强度
                switch (assayProParam.Stirring1Intensity) // 搅拌1强度
                {
                    case "低":
                        t.SF1 = 1;
                        break;
                    case "中":
                        t.SF1 = 2;
                        break;
                    case "高":
                        t.SF1 = 3;
                        break;
                    default:
                        t.SF1 = 0;
                        break;
                }
                switch (assayProParam.Stirring2Intensity) // 搅拌2强度
                {
                    case "低":
                        t.SF2 = 1;
                        break;
                    case "中":
                        t.SF2 = 2;
                        break;
                    case "高":
                        t.SF2 = 3;
                        break;
                    default:
                        t.SF2 = 0;
                        break;
                }
                t.PPNO = myBatis.QueryProjectRunSequenceByProject(new AssayProjectInfo() { ProjectName = qcTask.ProjectName, SampleType = qcTask.SampleType }).RunSequence;
                t.R1VOL = assayProParam.Reagent1VolSettings;
                t.R2VOL = assayProParam.Reagent2VolSettings;

                t.SMPNO = qcTask.SampleNum;
                t.ASSAY = assayProParam.ProjectName;
                t.SAMPLETYPE = assayProParam.SampleType;
                t.CalibDate = qcTask.CreateDate;
                t.R1POS = System.Convert.ToInt32(assayProParam.Reagent1Pos == "" ? "0" : assayProParam.Reagent1Pos);
                t.R2POS = System.Convert.ToInt32(assayProParam.Reagent2Pos == "" ? "0" : assayProParam.Reagent2Pos);

                t.DPOS = System.Convert.ToInt32(myBatis.GetRGTDilutePosition());

                t.PT = 4;
                t.DISK = myBatis.GetWorkingDisk().ToString();
                t.SMPPOS = myBatis.QueryQCInfoByQCID(qcTask.QCID).Pos.ToString();
                t.CT = (myBatis.SMPContainerType(myBatis.GetSDTSMPContainerType())).Code;

                t.PV = (int)(assayProParam.ComStosteVol * 10);
                t.V = (int)(assayProParam.ComSamVol * 10);
                t.DV = (int)(assayProParam.DecDilutionVol);
                t.VOLTYPE = VOLTYPE.NV;

                for (int i = 1; i <= qcTask.InspectTimes - qcTask.SendTimes; i++)
                {
                    tasks.Add(t);
                }
            }
            catch(Exception e)
            {

            }

            return tasks;
        }
        /// <summary>
        /// 获取急症任务
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        List<TASK> LoadEMGTask(int count)
        {
            List<TASK> Tasks = new List<TASK>();

            List<TaskInfo> EMGSchedules = myBatis.GetSingleWorkingEmgAssayScheduleNoRgtLock(myBatis.GetWorkingDisk(), count);
            foreach (TaskInfo e in EMGSchedules)
            {
                myBatis.UpdateTaskStatePerform(e, TaskState.START);
                List<TASK> etasks = CreatTaskList(e, true);
                foreach (TASK t in etasks)
                {
                    Tasks.Add(t);
                }
            }
            return Tasks;

        }

        List<TASK> CreatTaskList(TaskInfo taskInfo, bool bEMG)
        {
            List<TASK> tasks = new List<TASK>();
            try
            {
                TASK t = new TASK();
                //获取项目参数信息
                AssayProjectParamInfo assayProParam = myBatis.GetAssayProjectParamInfoByNameAndType("GetAssayProjectParamInfoByNameAndType", new AssayProjectInfo() { ProjectName = taskInfo.ProjectName, SampleType = taskInfo.SampleType });
                t.PW = assayProParam.MainWaveLength;
                t.SW = assayProParam.SecWaveLength;
                switch (assayProParam.Stirring1Intensity) // 搅拌1强度
                {
                    case "低":
                        t.SF1 = 1;
                        break;
                    case "中":
                        t.SF1 = 2;
                        break;
                    case  "高":
                        t.SF1 = 3;
                        break;
                    default:
                        t.SF1 = 0;
                        break;
                }
                switch (assayProParam.Stirring2Intensity) // 搅拌2强度
                {
                    case "低":
                        t.SF2 = 1;
                        break;
                    case "中":
                        t.SF2 = 2;
                        break;
                    case "高":
                        t.SF2 = 3;
                        break;
                    default:
                        t.SF2 = 0;
                        break;
                }
                t.PPNO = myBatis.QueryProjectRunSequenceByProject(new AssayProjectInfo() { ProjectName = taskInfo.ProjectName, SampleType = taskInfo.SampleType }).RunSequence;
                t.R1VOL = assayProParam.Reagent1VolSettings;
                t.R2VOL = assayProParam.Reagent2VolSettings;

                t.SMPNO = taskInfo.SampleNum.ToString();
                t.ASSAY = assayProParam.ProjectName;
                t.SAMPLETYPE = assayProParam.SampleType;
                t.CalibDate = taskInfo.CreateDate;
                t.R1POS = System.Convert.ToInt32(assayProParam.Reagent1Pos == "" ? "0" : assayProParam.Reagent1Pos);
                t.R2POS = System.Convert.ToInt32(assayProParam.Reagent2Pos == "" ? "0" : assayProParam.Reagent2Pos);

                t.DPOS = System.Convert.ToInt32(myBatis.GetRGTDilutePosition());
                if (bEMG)
                    t.PT = 1;
                else
                    t.PT = 0;
                //盘号
                t.DISK = myBatis.GetWorkingDisk().ToString();
                SampleInfo sampleInfo = myBatis.QuerySampleInfoByQCID(taskInfo.SampleNum);
                t.SMPPOS = sampleInfo.SamplePos.ToString();


                t.CT = (myBatis.SMPContainerType(sampleInfo.SamContainer)).Code;

                if (taskInfo.SampleDilute == "增量体积")
                {
                    t.PV = (int)(assayProParam.IncStosteVol * 10);
                    t.V = (int)(assayProParam.IncSamVol * 10);
                    t.DV = (int)(assayProParam.IncDilutionVol);
                    t.VOLTYPE = VOLTYPE.IV;
                }
                if (taskInfo.SampleDilute == "常规体积")
                {
                    t.PV = (int)(assayProParam.ComStosteVol * 10);
                    t.V = (int)(assayProParam.ComSamVol * 10);
                    t.DV = (int)(assayProParam.ComDilutionVol);
                    t.VOLTYPE = VOLTYPE.NV;
                }
                if (taskInfo.SampleDilute == "减量体积")
                {
                    t.PV = (int)(assayProParam.DecStosteVol * 10);
                    t.V = (int)(assayProParam.DecSamVol * 10);
                    t.DV = (int)(assayProParam.DecDilutionVol);
                    t.VOLTYPE = VOLTYPE.DV;
                }

                //项目类型识别标志
                //t.ASSAYTYPE = S.AssayType;//生化 离子


                for (int i = 1; i <= taskInfo.InspectTimes - taskInfo.SendTimes; i++)
                {
                    tasks.Add(t);
                }
            }
            catch
            {

            }

            return tasks;
        }
        /// <summary>
        /// 处理下定标/普通任务数据
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        List<TASK> LoadNorTask(int count)
        {
            List<TASK> Tasks = new List<TASK>();

            List<TaskInfo> NorSchedules = myBatis.GetSingleWorkingNorAssayScheduleNoRgtLock(myBatis.GetWorkingDisk(), count);
            foreach (TaskInfo e in NorSchedules)
            {
                myBatis.UpdateTaskStatePerform(e, TaskState.START);
                myBatis.UpdateSampleStatePerform(e, TaskState.START);
                List<TASK> etasks = CreatTaskList(e, false);
                foreach (TASK t in etasks)
                {
                    Tasks.Add(t);
                }
            }

            return Tasks;
        }



        void LoadTask()
        {
            if (this.MachineState.Command == null)
            {
                return;
            }

            if (this.MachineState.Command.Name == "WashByDetergent")
            {
                LoadClearSchedule();
            }
            else
            {
                if (this.IsPauseSchedule == true)
                {
                    TaskQueueSignal.Reset();
                }
                else
                {
                    TaskQueueSignal.Set();
                }
            }
        }
        void StopLoadTask()
        {
            TaskQueueSignal.Reset();
        }

        void ConfirmScheduleTASK(ScheduleTASK T)
        {
            if (T.T.SAMPLETYPE == "血清" || T.T.SAMPLETYPE =="尿液" || T.T.SAMPLETYPE == "")
            {
                RealTimeCUVDataInfo rtd = new RealTimeCUVDataInfo();

                rtd.WorkNo = T.WN;
                rtd.TC = myBatis.GetLastestTC() + 1;
                rtd.SmpNo = T.T.SMPNO;
                rtd.Assay = T.T.ASSAY;
                rtd.CuvNo = 0;
                rtd.CUVPoint = 0;
                switch (T.T.PT)
                {
                    case 0:
                        rtd.WorkType = WORKTYPE.N;
                        break;
                    case 1:
                        rtd.WorkType = WORKTYPE.E;
                        break;
                    case 2:
                        rtd.WorkType = WORKTYPE.B;
                        break;
                    case 3:
                        rtd.WorkType = WORKTYPE.S;
                        break;
                    case 4:
                        rtd.WorkType = WORKTYPE.C;
                        break;
                }
                myBatis.DeleteRealTimeCUVData(rtd);
                myBatis.SaveRealTimeCUVData(rtd);

                TimeCourseInfo TC = new TimeCourseInfo();
                TC.TimeCourseNo = rtd.TC;
                TC.DrawDate = rtd.DrawDate;
                TC.CUVNO = 0;
                //myBatis.DeleteTimeCourseByTCNO(TC.TimeCourseNo);
                myBatis.SaveTimeCourseByTCNO(TC);

                myBatis.UpdateLatestTC(TC.TimeCourseNo);

                //new ResultService().CreateResult(T.T, rtd);是在创建任务是创建结果还是在发送任务是创建结果，待定。目前是在创建任务时创建结果
                //修改任务发送次数和结果进程编号
                UpdateScheduleSendCount(T.T, TC.TimeCourseNo);
                
                
                
            }
        }
        private void UpdateScheduleSendCount(TASK T, int TimeCourseNo)
        {
            switch (T.PT)
            {
                case 0:
                case 1:
                    myBatis.AddSampleResultInfo(T.SMPNO,T.CalibDate,T.ASSAY,T.SAMPLETYPE,TimeCourseNo);
                    myBatis.UpdateSMPScheduleSendCount(T.SMPNO, T.ASSAY, T.SAMPLETYPE, myBatis.GetSMPScheduleSendCount(T.SMPNO, T.ASSAY, T.SAMPLETYPE) + 1);
                    break;
                case 2:
                case 3:
                    CalibrationCurveInfo c = myBatis.QueryCalibCurveInfoByCalibNameAndProName(T.SMPPOS, T.ASSAY, T.SAMPLETYPE, T.CALIBNAME);
                    myBatis.AddSDTResult(c, T.CalibDate, TimeCourseNo, T.SMPNO);
                    //myBatis.UpdateSDTResultTCNO(T.SMPNO, T.ASSAY, T.SAMPLETYPE, T.CALIBNAME, T.CalibDate, TimeCourseNo);
                    myBatis.UpdateSDTScheduleSendCount(T.SMPNO, T.ASSAY, T.SAMPLETYPE, T.CALIBNAME, myBatis.GetSDTScheduleSendCount(T.SMPNO, T.ASSAY, T.SAMPLETYPE, T.CALIBNAME) + 1);
                    break;
                case 4:
                    myBatis.UpdateQualityControlResultTCNO(T.SMPNO, T.ASSAY, T.SAMPLETYPE, T.CalibDate, TimeCourseNo);
                    myBatis.UpdateQCScheduleSendCount(T.SMPNO, T.ASSAY, T.SAMPLETYPE, myBatis.GetQCScheduleSendCount(T.SMPNO, T.ASSAY, T.SAMPLETYPE) + 1);
                    break;
            }
        }
    }
}
