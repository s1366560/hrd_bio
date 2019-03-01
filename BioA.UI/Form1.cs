using BioA.Common;
using BioA.Common.Communication;
using BioA.Common.IO;
using BioA.Common.Machine;
using BioA.Service;
using BioA.SqlMaps;
using BioA.UI.ServiceReference1;
using BioA.UI.Uicomponent;
using BioA.UI.Uicomponent.Analog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace BioA.UI
{

    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DataCheck dadtCheck;
        CalibDataCheck calibDataCheck;
        ApplyTask applyTask;
        MissionInspection missionInspection;
        ReagentState reagentState;
        ReagentSetting reagentSetting;
        CalibMaintain calibMaintain;
        CalibControlTask calibControlTask;
        lstvCalibrationState calibrationState;
        QCMaintain qCMaintain;
        QualityControlState qualityControlState;

        ChemicalParameter chemicalParameter;
        CombProject combProject;
        CalcProject computationlItem;
        EnvironmentData environments;
        //CrossPollution crossPollution;
        LISCommunicate lISCommunicate;
        DataConfig dataConfig;
        RMThirdMenu rMThirdMenu;
        TestEquipment testEquipment;
        UserManagement userManagement;
        DepartmentManage departmentManage;
        Configure configure;
        VersionInformation versionInformation;
        Log log;
        ReagentNeedle reagentNeedle;
        QualityControlGraphs qualityControlProfile;
        ApplyQCTask applyQCTask;
        InitializationLoad initializationLoad;
        TextBox txtPrompt;
        LoginInterface login;
        UserInfo userInfo = new UserInfo();

        private MyBatis myBatis = new MyBatis();

        public float temp;
        //获取当前运行程序的项目路径
        private static string _FileName = (Directory.GetCurrentDirectory()).Substring(0, Directory.GetCurrentDirectory().IndexOf("b"));

        //加载该项目下的图片
        private Image images = System.Drawing.Image.FromFile(_FileName + "Resources\\Image\\zhixiang.png");
        /// <summary>
        /// 初始化一个子控件元素集合
        /// </summary>
        private List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
        
        BioAServiceClient serviceClient = CommunicationUI.ServiceClient;
        // 与下位机网口通信
        CLIENT CLClient;
        // 判断系统启动状态，完成读取温度置为false，代表启动完成
        private bool IsStarting = true;
        public Form1()
        {
            InitializeComponent();
            
            this.WindowState = FormWindowState.Maximized;  //窗口最大化
            this.FormBorderStyle = FormBorderStyle.None; //状态栏没有
            this.CloseBox = false;                      //关闭按钮
            this.MaximizeBox = false;                   //最大化按钮
            this.MinimizeBox = false;                   //最小化按钮
        }

        public void Init()
        {
            CLClient = new CLIENT();
            this.CLClient.DataArriveEvent += ConsoleDataArriveEvent;
            this.CLClient.ConnectSuccessEvent += OnConnectSuccessEvent;
            this.CLClient.ConnectFailedEvent += OnConnectFailedEvent;
            this.CLClient.ClientErrorEvent += OnClientErrorEvent;
            //应该就是这个方法了
           // this.CLClient.ConnectServer();

           var connThread =  new Thread(CLClient.ConnectServer);//.Start();
           connThread.IsBackground = true;
           connThread.Start();

            txtPrompt = new TextBox();
            txtPrompt.Font = new System.Drawing.Font("宋体", 14f);
            txtPrompt.ReadOnly = true;
            txtPrompt.BackColor = Color.FromArgb(209, 203, 182);
            txtPrompt.Top = 5;
            txtPrompt.Width = 1720;

            //login = new Login();
            //login.LoginEvent += login_LoginEvent;
            //CommunicationUI.notifyCallBack.LoginDataTransferEvent += login.DataTransfer_Event;
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("InitMachineUpdateQCTaskState",null)));
            CommunicationUI.notifyCallBack.StartTestTaskDataTransferEvent += DataTransfer_Event;
            //var loginThread = new Thread(StartLogin);
            //loginThread.IsBackground = true;
            //loginThread.Start();

            var analyzerDataQueueServiceThread = new Thread(AnalyzerDataQueueService);
            analyzerDataQueueServiceThread.IsBackground = true;
            analyzerDataQueueServiceThread.Start();
            this.OPID = 0;


            var machineTroubleThread = new Thread(this.MachineIsTrouble) { IsBackground = true };
            machineTroubleThread.Start();
            //异步连接LIS服务器
            this.AsyncConnectLis();

            this.barButtonItem17.AllowDrawArrow = true;
            pictureBox1.BackColor = Color.FromArgb(251, 248, 240);
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.ApplyTask)
                _Elements.Add(this.WorkingAreaApplyTaskElement1);
            if (userInfo.DataCheck)
                _Elements.Add(this.WorkingAreaDataCheckElement2);
            _Elements.Add(this.WorkingAreaMissionInspectionElement3);
            BeginInvoke(new Action(()=> {
                 this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            }));
            if (userInfo.ApplyTask)
            {
                if (pcThirdArea.Controls.Equals(applyTask) == false)
                {
                    this.WorkingAreaApplyTaskElement1.Image = this.images;
                    pcThirdArea.Controls.Clear();
                    if (CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent != null)
                        CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent -= applyTask.DataTransfer_Event;

                    applyTask = new ApplyTask();                    
                    applyTask.getopid += getOPIDEvent;
                    CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent += applyTask.DataTransfer_Event;
                    txtPrompt.Text = "您当前的操作：工作区——任务申请";
                    //initializationLoad = new InitializationLoad();

                    BeginInvoke(new Action(() => {
                        pcThirdArea.Controls.Add(txtPrompt);
                        //pcThirdArea.Controls.Add(initializationLoad);
                        pcThirdArea.Controls.Add(applyTask);
                    }));
                }
            }
        }


        #region // 与下位机通讯设置
        ManualResetEvent ConsoleDataArriveSignal = new ManualResetEvent(false);  // 控制下位机指令处理线程
        Queue<string> ConsoleDataQueue = new Queue<string>();   // 存储下位机发的指令
        private void ConsoleDataArriveEvent(object sender)
        {
            string consoledatastr = sender as string;
            if (!string.IsNullOrEmpty(consoledatastr) && !string.IsNullOrWhiteSpace(consoledatastr))
            {
                consoledatastr = consoledatastr.TrimEnd('\r', '\n');
                lock (ConsoleDataQueue)
                {
                    ConsoleDataQueue.Enqueue(consoledatastr);
                }
            }
            if (ConsoleDataQueue.Count > 0)
            {
                ConsoleDataArriveSignal.Set();
            }
        }

        private void OnConnectSuccessEvent(object sender)
        {
            Thread.Sleep(300);
            SendCommand("CheckCommunication");
            //this.Invoke(new EventHandler(delegate
            //    {
            //        txtInfoPrompt.Text = "连接成功！";
            //    }));
        }

        private void OnConnectFailedEvent(object sender)
        {
            //this.Invoke(new EventHandler(delegate
            //    {
            //        txtInfoPrompt.Text = "连接失败！";
            //    }));
        }

        private void OnClientErrorEvent(object sender)
        {
            //this.Invoke(new EventHandler(delegate
            //{
            //    txtInfoPrompt.Text = "连接出错！";
            //}));

        }

        public void SendCommand(string cmdName)
        {

            Command c = MachineInfo.GetCommandByName(cmdName);
            c.State = 1;
            this.CLClient.SendData(XmlUtility.Serializer(typeof(Command), c));
            //txtInfoPrompt.Text = c.FullName;
        }
        public void SendCommand(Command command)
        {
            this.CLClient.SendData(XmlUtility.Serializer(typeof(Command), command));
            //txtInfoPrompt.Text = c.FullName;
        }
        #endregion

        public void OnHasSchedulesWarn(string v)
        {
            new Thread(new ParameterizedThreadStart(CreateHasSchedulesWarnDlg)).Start(v);

        }
        SetScheduleWork setScheduleWork;
        void CreateHasSchedulesWarnDlg(object v)
        {
            this.Invoke(new EventHandler(delegate
            {
                if (setScheduleWork == null || setScheduleWork.IsDisposed)
                {
                    setScheduleWork = new SetScheduleWork(v);
                    setScheduleWork.ShowDialog();//未打开，直接打开。  
                }
                else
                {
                    setScheduleWork.Activate();//已打开，获得焦点，置顶。  
                }
            }));
        }
        #region 处理下位机发来的信息
        /// <summary>
        /// 处理下位机发来的信息
        /// </summary>
        private void AnalyzerDataQueueService()
        {
            while (true)
            {
                ConsoleDataArriveSignal.WaitOne();
                if (ConsoleDataQueue.Count > 0)
                {
                    string cmdstr = null;
                    lock (ConsoleDataQueue)
                    {
                        cmdstr = ConsoleDataQueue.Dequeue();
                    }
                    if (cmdstr == "CONNECTING")
                    {
                        //this.RunningSer.CtrlNetStopConnect();
                        //this.RunningSer.CtrlNetBtToolTip = "成功连接...";
                    }
                    else
                    {
                        MachineState machineState = CommandService.GetMachineState(cmdstr);
                        if (machineState != null)
                        {
                            this.Invoke(new EventHandler(delegate
                            {
                                if (machineState.Temp != null)
                                {
                                    if (machineState.State == "超时")
                                    {
                                        lblSampleContainer.Text = "孵育温度" + temp + "°C";
                                    }
                                    else
                                    {
                                        try
                                        {
                                            temp = float.Parse(machineState.Temp);
                                        }
                                        catch (Exception e)
                                        {

                                        }
                                        lblSampleContainer.Text = "孵育温度" + temp + "°C";
                                    }
                                }
                                if (machineState.Fired == AnalyzeEvent.MACHINE_WILL_FINIFSHSCHEDULE)
                                {
                                    labfinishTime.Text = "";//任务即将结束时清除预计完成时间提示                                            
                                }
                                txtInfoPrompt.Text = machineState.State;
                            }));
                            if (machineState.Command != null)
                            {
                                HandleTaskCommand(machineState.Command);

                                switch (machineState.Command.State)
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        //DeleteCompletedCommand(machineState.Command); 
                                        break;//命令成功执行
                                    case 3:
                                        //this.CommandNameList.Clear(); 
                                        break;
                                    case 4:
                                        //if (machineState.Command.Name == "ReadVersion")
                                        //{
                                        //    this.IsReadVersionTimeout = true;
                                        //}
                                        break;
                                    default:
                                        break;

                                }

                            }
                            switch (machineState.Fired)
                            {
                                //处理x接收4
                                case AnalyzeEvent.COMPLETED_READ_ACTIVEKEY://完成读取激活码
                                    //  this.ActiveKey = machineState.StateValue;
                                    //发送4
                                    SendCommand("CheckWashingLiquid");
                                    break;
                                case AnalyzeEvent.MACHINE_RESET://生化部件复位
                                    this.OPID = 0;
                                    if (this.barButtonItem13.Caption != "启动操作")
                                    {
                                        this.barButtonItem13.LargeGlyph = Firing;
                                        this.barButtonItem13.Caption = "启动操作";
                                    }
                                    //this.CommandNameList.Clear();
                                    //OnMachineCompletedResetEvent(null);
                                    break;
                                case AnalyzeEvent.WATER_REPLE://孵育盘正在补水
                                    ShowMachineInfo("正在初始化孵育环境，请稍等.");
                                    break;
                                //处理x接收6
                                case AnalyzeEvent.MACHINE_STATE_ER://设备状态发生错误
                                    //发送6
                                    ShowMachineInfo("设备液路环境没有准备好，测试无法进行");
                                    break;
                                //处理x接收6
                                case AnalyzeEvent.SCAN_Temp_INVALID://孵育盘温度不在理想范围
                                    //发送6
                                    HandleSCAN_Temp_INVALID();
                                    break;
                                case AnalyzeEvent.COMPLETE_CHECK_WASHLIQUID://完成清洗液探测
                                    // 处理x接收5
                                    if (this.IsStarting == true)
                                    {
                                        // 发送5
                                        SendCommand("ReadTempt");
                                    }
                                    break;
                                //处理x接收6
                                case AnalyzeEvent.COMPLETE_READ_TEM://完成温度读取
                                    if (this.IsStarting == true)
                                    {
                                        this.IsStarting = false;
                                    }
                                    break;
                                case AnalyzeEvent.ACTIVEDCODE_WILLEXPIRED://激活码即将失效
                                    break;
                                //case AnalyzeEvent.ACTIVEDCODE_EXPIRED://激活码失效
                                //    OnDisplayKeyInputDlgEvent(null);
                                //    break;
                                //case AnalyzeEvent.ACTIVEDCODE_INVALID:
                                //    OnDisplayInValidAKeyDlgEvent(null);
                                //    break;
                                case AnalyzeEvent.COMPLETED_READ_SN://读取SN码
                                    //OnReadSNCompletedEvent(machineState.StateValue);
                                    if (this.IsStarting == true)
                                    {
                                        SendCommand("ReadActivityCode");
                                    }
                                    break;
                                case AnalyzeEvent.WORKSTATION_MATCHING://工作站有效
                                    if (this.IsStarting == true)
                                    {
                                        SendCommand("ReadSN");
                                    }
                                    break;
                                //case AnalyzeEvent.WORKSTATION_NOMATCHING://工作站失效!
                                //    OnInvalidUserEvent(null);
                                //    break;
                                //case AnalyzeEvent.COMPLETED_SCAN_SMPBarcode://样本条码扫描完成!;
                                //    OnProcessSampleBarcode(machineState.StateValue);
                                //    break;
                                //case AnalyzeEvent.COMPLETED_SCAN_RGTBarcode://试剂条码扫描完成
                                //    OnProcessRgtBarcode(machineState.StateValue);
                                //    break;
                                case AnalyzeEvent.COMPLETED_SCHEDULE://工作队列执行完成
                                    //this.CMDManager.OPMgr.OPCMDStr = null;
                                    //this.CMDManager.OPMgr.OPBTReset();
                                    this.OPID = 0;
                                    this.barButtonItem13.LargeGlyph = Firing;
                                    this.barButtonItem13.Caption = "启动操作";

                                    break;
                                case AnalyzeEvent.MACHINE_WILL_FINIFSHSCHEDULE://工作队列即将结束
                                    if (this.OPID == 1)
                                    {
                                        var tasksStatusThread = new Thread(this.TaskStatusDeletection) { IsBackground = true };
                                        tasksStatusThread.Start();
                                    }
                                    break;
                                case AnalyzeEvent.COMPLETED_SCAN_HWVersion://获取版本号
                                    //this.HardwareVersion = machineState.StateValue;
                                    if (this.IsStarting == true)
                                    {
                                        SendCommand("ReadLicense");
                                    }
                                    break;
                                //case AnalyzeEvent.COMPLETED_SCAN_MSTATE:
                                //    OnScanMacheStateEvent(machineState.StateValue);
                                //    break;
                                //case AnalyzeEvent.EXXCHANGE_WARER_FAILED:
                                //    this.CommandNameList.Clear();
                                //    break;
                                case AnalyzeEvent.HAS_SCHEDULE_WARNNING:
                                    OnHasSchedulesWarn(machineState.StateValue);
                                    break;
                                case AnalyzeEvent.TASK_STATUS_DETECTION:
                                    if (this.OPID == 1)
                                    {
                                        var tasksStatusThread = new Thread(this.TaskStatusDeletection) { IsBackground = true };
                                        tasksStatusThread.Start();
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    ConsoleDataArriveSignal.Reset();
                }
            }
        }
        #endregion
        /// <summary>
        /// 获取任务状态
        /// </summary>
        private void TaskStatusDeletection()
        {
            try
            {
                if (this.txtPrompt.Text == "您当前的操作：质控——质控任务")
                    BeginInvoke(new Action(applyQCTask.QueryTasksStatus));

                else if (this.txtPrompt.Text == "您当前的操作：校准——校准任务")
                    BeginInvoke(new Action(calibControlTask.QueryTasksStatus));

                else if (this.txtPrompt.Text == "您当前的操作：工作区——任务申请")
                    BeginInvoke(new Action(applyTask.QueryTasksStatus));
            }
            catch(Exception ex)
            {
                LogInfo.WriteErrorLog("TaskStatusDeletection() == "+ ex.ToString(), Module.FramUI);
            }
            
        }

        // 任务状态，0——无任务；1——开始；2——暂停；3——紧急停止
        private int OPID = 0;

        //private bool IsLockedView = false;
        /// <summary>
        /// 处理做任务指令
        /// </summary>
        /// <param name="command"></param>
        private void HandleTaskCommand(Command command)
        {

            switch (command.Name)
            {
                case "IgnoreKeyDoRun":
                    break;
                case "StartSchedule":
                    this.OPID = 1;
                    this.barButtonItem13.LargeGlyph = Suspend;
                    this.barButtonItem13.Caption = "暂停操作";
                    break;
                case "PauseSchedule":
                    this.OPID = 2;
                    break;
                case "AbortSchedule":
                    this.OPID = 3;
                    break;
                default:
                    this.OPID = 0;
                    break;
            }
        }

        private void ShowMachineInfo(string machineInfo)
        {
                BeginInvoke(new EventHandler(delegate
                {
                    txtInfoPrompt.Text = machineInfo;
                }));
        }

        private void HandleSCAN_Temp_INVALID()
        {
            if (MessageBoxDraw.ShowMsg("孵育盘当前温度不够理想，是否继续测试？", MsgType.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.SendCommand("IgnoreKeyDoRun");
            }
            else
            {
                this.SendCommand("AbortDorun");
                this.OPID = 0;
                this.barButtonItem13.LargeGlyph = Firing;
                this.barButtonItem13.Caption = "启动操作";
            }
        }


        private void login_LoginEvent(object sender)
        {
            userInfo = sender as UserInfo;
        }

        private void StartLogin()
        {
            //login.ShowDialog();
            SendCommand("CheckCommunication");
        }
        /// <summary>
        /// 初始化改线程为阻塞
        /// </summary>
        ManualResetEvent ErrorFaultSignal = new ManualResetEvent(false);
        /// <summary>
        /// 报警提示
        /// </summary>
        private void DisplayHavingError()
        {
            while (true)
            {
                this.ErrorFaultSignal.WaitOne();

                this.Invoke(new Action(() => { this.pictureBox2.Image = this.pictureBox2.Image; }));
                Thread.Sleep(450);

                this.Invoke(new Action(() => { this.pictureBox2.Image = this.pictureBox2.ErrorImage; }));
                Thread.Sleep(450);

                this.Invoke(new Action(() => { this.pictureBox2.Image = System.Drawing.Image.FromFile(_FileName + "Resources\\Image\\WarnUn.png"); }));
                Thread.Sleep(450);
                if (this.IsWarningInfoUIActivity == true)
                {
                    this.Invoke(new Action(() => { this.pictureBox2.Image = this.pictureBox2.InitialImage; }));
                }
            }
        }
        /// <summary>
        /// 启动LIS服务
        /// </summary>
        public void AsyncConnectLis()
        {
            new Thread(new ThreadStart(ConnectLisServer)).Start();
        }

        void ConnectLisServer()
        {
            //this.RunningSer.LISToolTip = Application.Current.FindResource("ViewModeMAINMainWindowViewModel9").ToString();

            //this.LISSer.ConnectSuccessEvent -= new LISService.LISServiceHandler(OnLISConnectSuccessEvent);
            //this.LISSer.LisErrorEvent -= new LISService.LISServiceHandler(OnLISSerLisErrorEvent);

            //this.LISSer.SendLisDataEvent -= new LISService.LISServiceHandler(OnLISSerSendLisDataEvent);
            //this.LISSer.NotHasLisDataEvent -= new LISService.LISServiceHandler(OnLISSerNotHasLisDataEvent);
            //this.LISSer.SMPCodeBarQueryEvent -= new LISService.LISServiceHandler(OnLISSerSMPCodeBarQueryEvent);
            //this.LISSer.ApplySampleSuccessEvent -= new LISService.LISServiceHandler(OnLISSerApplySampleSuccessEvent);
            //this.LISSer.SendLisResultDataFailedEvent -= new LISService.LISServiceHandler(OnLISSerSendLisResultDataFailedEvent);
            //this.LISSer.SendLisResultDataOKEvent -= new LISService.LISServiceHandler(OnLISSerSendLisResultDataOKEvent);
            //this.LISSer.SendLisResultDataRunningEvent -= new LISService.LISServiceHandler(OnLISSerSendLisResultDataRunningEvent);

            //this.RunningSer.LISToolTip = Application.Current.FindResource("ViewModeMAINMainWindowViewModel10").ToString();
            //this.LISSer.StopService();
            //Thread.Sleep(1000 * 2);

            //this.LISSer.ConnectSuccessEvent += new LISService.LISServiceHandler(OnLISConnectSuccessEvent);
            //this.LISSer.LisErrorEvent += new LISService.LISServiceHandler(OnLISSerLisErrorEvent);

            ////this.LISSer.SendLisDataEvent += new LISService.LISServiceHandler(OnLISSerSendLisDataEvent);
            //this.LISSer.SendLisResultDataFailedEvent += new LISService.LISServiceHandler(OnLISSerSendLisResultDataFailedEvent);
            //this.LISSer.SendLisResultDataOKEvent += new LISService.LISServiceHandler(OnLISSerSendLisResultDataOKEvent);
            //this.LISSer.SendLisResultDataRunningEvent += new LISService.LISServiceHandler(OnLISSerSendLisResultDataRunningEvent);
            //this.LISSer.NotHasLisDataEvent += new LISService.LISServiceHandler(OnLISSerNotHasLisDataEvent);
            //this.LISSer.SMPCodeBarQueryEvent += new LISService.LISServiceHandler(OnLISSerSMPCodeBarQueryEvent);
            //this.LISSer.ApplySampleSuccessEvent += new LISService.LISServiceHandler(OnLISSerApplySampleSuccessEvent);

            //this.RunningSer.LISStartWork();
            //this.RunningSer.LISRunning();
            //Thread.Sleep(10000);
            //this.LISSer.StartService();
        } 

        private void MachineIsTrouble()
        {
            int i = 1;
            while (true)
            {
                bool bol = myBatis.TroubleLogInfo();
                if(bol == true)
                {
                    if (this.IsWarningInfoUIActivity == true)
                    {

                    }
                    else
                        ErrorFaultSignal.Set();
                }
                else
                {
                    if(i == 1)
                    {
                        ErrorFaultSignal.Set();
                        i++;
                    }
                }
            }
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // BeginInvoke(new Action(Init));
            userInfo = Program.userInfo;
            this.labUserName.Text = userInfo.UserName;

            var displayThread = new Thread(DisplayHavingError);
            displayThread.IsBackground = true;
            displayThread.Start();

            var initThread = new Thread(Init);
            initThread.IsBackground = true;
            initThread.Start();

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelClickSign();
            this.barButtonItem2.AllowDrawArrow = true;
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.ReagentState)
                _Elements.Add(this.ReagentStateElement4);
            if (userInfo.ReagentSetting)
                _Elements.Add(this.ReagentSettingElement5);
            this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            if (userInfo.ReagentState)
            {
                if (pcThirdArea.Controls.Equals(reagentState) == false)
                {
                    this.FeatureListTagIcon(_Elements);
                    this.ReagentStateElement4.Image = images;
                    pcThirdArea.Controls.Clear();
                    if (reagentState != null)
                    {
                        reagentState.SendNetworkCommandEvent -= SendCommand;
                        CommunicationUI.notifyCallBack.ReagentStateDataTransferEvent -= reagentState.DataTransfer_Event;
                    }

                    reagentState = new ReagentState();
                    CommunicationUI.notifyCallBack.ReagentStateDataTransferEvent += reagentState.DataTransfer_Event;
                    reagentState.SendNetworkCommandEvent += SendCommand;
                    txtPrompt.Text = "您当前的操作：试剂——试剂状态";
                    pcThirdArea.Controls.Add(txtPrompt);
                    pcThirdArea.Controls.Add(reagentState);
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelClickSign();
            this.barButtonItem3.AllowDrawArrow = true;
            this.CalibTaskElement26.Image = this.images;
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.CalibTask)
                _Elements.Add(this.CalibTaskElement26);
            if (userInfo.CalibState)
                _Elements.Add(this.CalibrationStateElement6);
            if (userInfo.CalibMaintain)
                _Elements.Add(this.CalibrationMaintainElement7);
            BeginInvoke(new Action(() =>
            {
                this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            }));
            if (userInfo.CalibTask)
            {
                if (pcThirdArea.Controls.Equals(calibControlTask) == false)
                {
                    this.FeatureListTagIcon(_Elements);
                    this.CalibTaskElement26.Image = images;
                    pcThirdArea.Controls.Clear();
                    if (CommunicationUI.notifyCallBack.CalibControlTaskDataTransferEvent != null)
                        CommunicationUI.notifyCallBack.CalibControlTaskDataTransferEvent -= calibControlTask.DataTransfer_Event;

                    calibControlTask = new CalibControlTask();
                    calibControlTask.getopid += getOPIDEvent;
                    CommunicationUI.notifyCallBack.CalibControlTaskDataTransferEvent += calibControlTask.DataTransfer_Event;
                    txtPrompt.Text = "您当前的操作：校准——校准任务";
                    BeginInvoke(new Action(() =>
                    {
                        pcThirdArea.Controls.Add(txtPrompt);
                        pcThirdArea.Controls.Add(calibControlTask);
                    }));
                }
            }
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelClickSign();
            this.barButtonItem17.AllowDrawArrow = true;
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.ApplyTask)
                _Elements.Add(this.WorkingAreaApplyTaskElement1);
            if (userInfo.DataCheck)
                _Elements.Add(this.WorkingAreaDataCheckElement2);
            _Elements.Add(this.WorkingAreaMissionInspectionElement3);
            this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            if (userInfo.ApplyTask)
            {
                if (pcThirdArea.Controls.Equals(applyTask) == false)
                {
                    this.FeatureListTagIcon(_Elements);
                    this.WorkingAreaApplyTaskElement1.Image = images;
                    pcThirdArea.Controls.Clear();
                    if (CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent != null)
                        CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent -= applyTask.DataTransfer_Event;
                    applyTask = new ApplyTask();
                    applyTask.getopid += getOPIDEvent;
                    CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent += applyTask.DataTransfer_Event;
                    txtPrompt.Text = "您当前的操作：工作区——任务申请";
                    pcThirdArea.Controls.Add(txtPrompt);
                    pcThirdArea.Controls.Add(applyTask);
                }
            }
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(dadtCheck) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.WorkingAreaDataCheckElement2.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.CommonSampleDataEvent != null)
                    CommunicationUI.notifyCallBack.CommonSampleDataEvent -= dadtCheck.DataTransfer_Event;

                dadtCheck = new DataCheck();
                CommunicationUI.notifyCallBack.CommonSampleDataEvent += dadtCheck.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：工作区——任务结果";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(dadtCheck);
            }
        }
        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(applyTask) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.WorkingAreaApplyTaskElement1.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent -= applyTask.DataTransfer_Event;
                applyTask = new ApplyTask();
                applyTask.getopid += getOPIDEvent;
                CommunicationUI.notifyCallBack.ApplyTaskDataTransferEvent += applyTask.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：工作区——任务申请";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(applyTask);

            }
        }
        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            this.FeatureListTagIcon(_Elements);
            this.WorkingAreaMissionInspectionElement3.Image = this.images;
            pcThirdArea.Controls.Clear();
            missionInspection = new MissionInspection();
            missionInspection.GetOpidEvent += this.getOPIDEvent;
            txtPrompt.Text = "您当前的操作：工作区——任务核查";
            pcThirdArea.Controls.Add(txtPrompt);
            pcThirdArea.Controls.Add(missionInspection);
        }
        //private void accordionControlElement3_Click(object sender, EventArgs e)
        //{
        //    if (pcThirdArea.Controls.Equals(calibDataCheck) == false)
        //    {
        //        pcThirdArea.Controls.Clear();
        //        calibDataCheck = new CalibDataCheck();
        //        pcThirdArea.Controls.Add(calibDataCheck);

        //    }   
        //}
        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(reagentState) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.ReagentStateElement4.Image = images;
                pcThirdArea.Controls.Clear();

                if (reagentState != null)
                {
                    reagentState.SendNetworkCommandEvent -= SendCommand;
                    CommunicationUI.notifyCallBack.ReagentStateDataTransferEvent -= reagentState.DataTransfer_Event;
                }
                reagentState = new ReagentState();
                reagentState.SendNetworkCommandEvent += SendCommand;
                CommunicationUI.notifyCallBack.ReagentStateDataTransferEvent += reagentState.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：试剂——试剂状态";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(reagentState);

            }
        }
        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(reagentSetting) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.ReagentSettingElement5.Image = images;
                pcThirdArea.Controls.Clear();

                if (CommunicationUI.notifyCallBack.ReagentSettingsDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.ReagentSettingsDataTransferEvent -= reagentSetting.DataTransfer_Event;
                reagentSetting = new ReagentSetting();
                pcThirdArea.Controls.Add(reagentSetting);
                CommunicationUI.notifyCallBack.ReagentSettingsDataTransferEvent += reagentSetting.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：试剂——试剂设置";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(reagentSetting);

            }
        }
        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(calibMaintain) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.CalibrationMaintainElement7.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.CalibMaintainDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.CalibMaintainDataTransferEvent -= calibMaintain.DataTransfer_Event;
                calibMaintain = new CalibMaintain();
                CommunicationUI.notifyCallBack.CalibMaintainDataTransferEvent += calibMaintain.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：校准——校准品维护";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(calibMaintain);

            }
        }

        private void accordionControlElement26_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(calibControlTask) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.CalibTaskElement26.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.CalibControlTaskDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.CalibControlTaskDataTransferEvent -= calibControlTask.DataTransfer_Event;
                calibControlTask = new CalibControlTask();
                calibControlTask.getopid += getOPIDEvent;
                CommunicationUI.notifyCallBack.CalibControlTaskDataTransferEvent += calibControlTask.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：校准——校准任务";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(calibControlTask);
            }
        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(calibrationState) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.CalibrationStateElement6.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.CalibrationStateDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.CalibrationStateDataTransferEvent -= calibrationState.DataTransfer_Event;
                calibrationState = new lstvCalibrationState();
                CommunicationUI.notifyCallBack.CalibrationStateDataTransferEvent += calibrationState.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：校准——校准状态";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(calibrationState);
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelClickSign();
            this.barButtonItem4.AllowDrawArrow = true;
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.QCTask)
                _Elements.Add(this.QCTaskElement25);
            if (userInfo.QCState)
                _Elements.Add(this.QCStateElement8);
            if (userInfo.QCMaintain)
                _Elements.Add(this.QCMaintainElement9);
            if (userInfo.QCGraphic)
                _Elements.Add(this.QCGraphicElement24);
            this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            if (userInfo.QCTask)
            {
                if (pcThirdArea.Controls.Equals(applyQCTask) == false)
                {
                    this.FeatureListTagIcon(_Elements);
                    this.QCTaskElement25.Image = images;
                    pcThirdArea.Controls.Clear();
                    applyQCTask = new ApplyQCTask();
                    applyQCTask.getopid += getOPIDEvent;                   
                    if (CommunicationUI.notifyCallBack.QCTaskDataTransferEvent != null)
                        CommunicationUI.notifyCallBack.QCTaskDataTransferEvent -= applyQCTask.DataTransfer_Event;
                    CommunicationUI.notifyCallBack.QCTaskDataTransferEvent += applyQCTask.DataTransfer_Event;
                    txtPrompt.Text = "您当前的操作：质控——质控任务";
                    pcThirdArea.Controls.Add(txtPrompt);
                    pcThirdArea.Controls.Add(applyQCTask);

                }
            }
            
        }
        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(qualityControlState) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.QCStateElement8.Image = images;
                pcThirdArea.Controls.Clear();
                qualityControlState = new QualityControlState();
                if (CommunicationUI.notifyCallBack.QCResultDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.QCResultDataTransferEvent -= qualityControlState.DataTransfer_Event;
                CommunicationUI.notifyCallBack.QCResultDataTransferEvent += qualityControlState.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：质控——质控状态";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(qualityControlState);

            }
        }
        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(qCMaintain) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.QCMaintainElement9.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.QCMaintainDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.QCMaintainDataTransferEvent -= qCMaintain.DataTransfer_Event;
                qCMaintain = new QCMaintain();
                CommunicationUI.notifyCallBack.QCMaintainDataTransferEvent += qCMaintain.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：质控——质控品维护";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(qCMaintain);

            }
        }
        private void accordionControlElement24_Click(object sender, System.EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(qualityControlProfile) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.QCGraphicElement24.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.QCGraphicsDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.QCGraphicsDataTransferEvent -= qualityControlProfile.DataTransfer_Event;
                qualityControlProfile = new QualityControlGraphs();
                CommunicationUI.notifyCallBack.QCGraphicsDataTransferEvent += qualityControlProfile.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：质控——质控图";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(qualityControlProfile);

            }
        }
        private void accordionControlElement25_Click(object sender, System.EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(applyQCTask) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.QCTaskElement25.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.QCTaskDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.QCTaskDataTransferEvent -= applyQCTask.DataTransfer_Event;
                applyQCTask = new ApplyQCTask();
                applyQCTask.getopid += getOPIDEvent;
                CommunicationUI.notifyCallBack.QCTaskDataTransferEvent += applyQCTask.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：质控——质控任务";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(applyQCTask);

            }
        }
        private void accordionControlElement10_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(chemicalParameter) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.ChemicalParameterElement10.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.ChemicalParamDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.ChemicalParamDataTransferEvent -= chemicalParameter.DataTransfer_Event;
                chemicalParameter = new ChemicalParameter();
                CommunicationUI.notifyCallBack.ChemicalParamDataTransferEvent += chemicalParameter.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——化学参数";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(chemicalParameter);
            }
        }
        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(combProject) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.CombProjectElement11.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.CombProjectDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.CombProjectDataTransferEvent -= combProject.DataTransfer_Event;
                combProject = new CombProject();
                CommunicationUI.notifyCallBack.CombProjectDataTransferEvent += combProject.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——组合项目";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(combProject);

            }
        }
        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(computationlItem) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.CalcProjectElement12.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.CalcProjectDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.CalcProjectDataTransferEvent -= computationlItem.CalcProjectDataTransfer_Event;
                computationlItem = new CalcProject();

                CommunicationUI.notifyCallBack.CalcProjectDataTransferEvent += computationlItem.CalcProjectDataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——计算项目";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(computationlItem);

            }
        }
        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(environments) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.EnvironmentElement13.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.EnvironmentDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.EnvironmentDataTransferEvent -= environments.DataTransfer_Event;
                environments = new EnvironmentData();
                CommunicationUI.notifyCallBack.EnvironmentDataTransferEvent += environments.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——环境参数";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(environments);

            }
        }
        private void accordionControlElement14_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(reagentNeedle) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.CrossPollutionElement14.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.ReagentNeedleDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.ReagentNeedleDataTransferEvent -= reagentNeedle.DataTransfer_Event;
                reagentNeedle = new ReagentNeedle();
                CommunicationUI.notifyCallBack.ReagentNeedleDataTransferEvent += reagentNeedle.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——防污策略";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(reagentNeedle);

            }
        }
        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(dataConfig) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.DataConfigElement15.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.DataConfigDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.DataConfigDataTransferEvent -= dataConfig.DataTransfer_Event;
                dataConfig = new DataConfig();
                CommunicationUI.notifyCallBack.DataConfigDataTransferEvent += dataConfig.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——数据配置";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(dataConfig);

            }
        }
        private void accordionControlElement16_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(lISCommunicate) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.LISCommunicateElement16.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.LISCommunicateDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.LISCommunicateDataTransferEvent -= lISCommunicate.DataTransfer_Event;
                lISCommunicate = new LISCommunicate();
                CommunicationUI.notifyCallBack.LISCommunicateDataTransferEvent += lISCommunicate.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——LIS通讯";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(lISCommunicate);

            }
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CuvettePanel cuvettePanel = new CuvettePanel();
            cuvettePanel.ShowDialog();
        }
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelClickSign();
            this.barButtonItem11.AllowDrawArrow = true;
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.ChemistryParam)
                _Elements.Add(this.ChemicalParameterElement10);
            if (userInfo.CombProject)
                _Elements.Add(this.CombProjectElement11);
            if (userInfo.CalcProject)
                _Elements.Add(this.CalcProjectElement12);
            if (userInfo.EnvironmentParam)
                _Elements.Add(this.EnvironmentElement13);
            if (userInfo.CrossPollute)
                _Elements.Add(this.CrossPollutionElement14);
            if (userInfo.DataConfiguration)
                _Elements.Add(this.DataConfigElement15);
            if (userInfo.LISCommunicate)
                _Elements.Add(this.LISCommunicateElement16);
            this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            if (pcThirdArea.Controls.Equals(chemicalParameter) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.ChemicalParameterElement10.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.ChemicalParamDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.ChemicalParamDataTransferEvent -= chemicalParameter.DataTransfer_Event;
                chemicalParameter = new ChemicalParameter();
                CommunicationUI.notifyCallBack.ChemicalParamDataTransferEvent += chemicalParameter.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：系统设置——化学参数";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(chemicalParameter);

            }

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.CancelClickSign();
            this.barButtonItem12.AllowDrawArrow = true;
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.RouMaintain)
                _Elements.Add(this.MaintenanceElement17);
            if (userInfo.EquipDebug)
                _Elements.Add(this.EquipmentManageElement18);
            if (userInfo.UserManage)
                _Elements.Add(this.UserManagementElement19);
            if (userInfo.DepartManage)
                _Elements.Add(this.DepartmentManageElement20);
            if (userInfo.LogCheck)
                _Elements.Add(this.LogCheckElement22);
            if (userInfo.VersionInfo)
                _Elements.Add(this.VersionInfomationElement23);
            this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            if (userInfo.RouMaintain)
            {
                if (pcThirdArea.Controls.Equals(rMThirdMenu) == false)
                {
                    this.FeatureListTagIcon(_Elements);
                    this.MaintenanceElement17.Image = images;
                    pcThirdArea.Controls.Clear();
                    if (rMThirdMenu != null)
                    {
                        rMThirdMenu.SendNetworkEvent -= SendCommand;
                        CommunicationUI.notifyCallBack.SystemMaintenanceDataTransferEvent -= rMThirdMenu.DataTransfer_Event;
                    }
                    rMThirdMenu = new RMThirdMenu(userInfo.UserName);
                    rMThirdMenu.SendNetworkEvent += SendCommand;
                    CommunicationUI.notifyCallBack.SystemMaintenanceDataTransferEvent += rMThirdMenu.DataTransfer_Event;
                    txtPrompt.Text = "您当前的操作：安全管理——常规保养";
                    pcThirdArea.Controls.Add(txtPrompt);
                    pcThirdArea.Controls.Add(rMThirdMenu);

                }
            }
        }
        private void accordionControlElement17_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(rMThirdMenu) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.MaintenanceElement17.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (rMThirdMenu != null)
                    rMThirdMenu.SendNetworkEvent -= SendCommand;
                CommunicationUI.notifyCallBack.SystemMaintenanceDataTransferEvent -= rMThirdMenu.DataTransfer_Event;
                rMThirdMenu = new RMThirdMenu(userInfo.UserName);
                rMThirdMenu.SendNetworkEvent += SendCommand;
                CommunicationUI.notifyCallBack.SystemMaintenanceDataTransferEvent += rMThirdMenu.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：安全管理——常规保养";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(rMThirdMenu);

            }
        }

        private void accordionControlElement18_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(testEquipment) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.EquipmentManageElement18.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (testEquipment != null)
                {
                    testEquipment.SendNetworkEvent -= SendCommand;
                    CommunicationUI.notifyCallBack.SystemTestEquipmentEvent -= testEquipment.DataTransfer_Event;
                }
                testEquipment = new TestEquipment();
                testEquipment.SendNetworkEvent += SendCommand;
                CommunicationUI.notifyCallBack.SystemTestEquipmentEvent += testEquipment.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：安全管理——设备调试";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(testEquipment);

            }
        }

        private void accordionControlElement19_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(userManagement) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.UserManagementElement19.Image = images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.UserManagementDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.UserManagementDataTransferEvent -= userManagement.DataTransfer_Event;
                userManagement = new UserManagement();
                CommunicationUI.notifyCallBack.UserManagementDataTransferEvent += userManagement.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：安全管理——用户管理";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(userManagement);

            }
        }

        private void accordionControlElement20_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(departmentManage) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.DepartmentManageElement20.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.DepartmentManageDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.DepartmentManageDataTransferEvent -= departmentManage.DataTransfer_Event;
                departmentManage = new DepartmentManage();
                CommunicationUI.notifyCallBack.DepartmentManageDataTransferEvent += departmentManage.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：安全管理——科室管理";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(departmentManage);
            }
        }

        private void accordionControlElement22_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(log) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.LogCheckElement22.Image = this.images;
                pcThirdArea.Controls.Clear();
                if (CommunicationUI.notifyCallBack.LogDataTransferEvent != null)
                    CommunicationUI.notifyCallBack.LogDataTransferEvent -= log.DataTransfer_Event;
                log = new Log();
                log.UserName = labUserName.Text;
                CommunicationUI.notifyCallBack.LogDataTransferEvent += log.DataTransfer_Event;
                txtPrompt.Text = "您当前的操作：安全管理——日志查看";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(log);

            }
        }

        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(versionInformation) == false)
            {
                this.FeatureListTagIcon(_Elements);
                this.VersionInfomationElement23.Image = this.images;
                pcThirdArea.Controls.Clear();
                versionInformation = new VersionInformation();
                txtPrompt.Text = "您当前的操作：安全管理——版本信息";
                pcThirdArea.Controls.Add(txtPrompt);
                pcThirdArea.Controls.Add(versionInformation);

            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.OPID == 1)
            {
                if (MessageBoxDraw.ShowMsg("确认暂停任务吗？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    SendCommand("PauseSchedule");
                    this.barButtonItem13.LargeGlyph = Firing;
                    this.barButtonItem13.Caption = "启动操作";
                }
            }
            else if (this.OPID == 2)
            {
                if (MessageBoxDraw.ShowMsg("确定恢复样本测试吗？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    SendCommand("StartSchedule");
                    this.barButtonItem13.LargeGlyph = Suspend;
                    this.barButtonItem13.Caption = "暂停操作";
                }
            }
            else if (this.OPID == 3)
            {
                MessageBoxDraw.ShowMsg("设备正在清洗紧急停止中占用的比色杯...", MsgType.Warning);
                return;
            }
            else
            {                
                int lstResult = myBatis.GetAllTasksCount("GetAllTasksCount");
                if (lstResult != 0)
                {                  
                    if (this.OPID == 0)
                    {
                        if (MessageBoxDraw.ShowMsg("确定开始样本测试吗？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
                        {
                            double time = (getFinishTime() - 1) * 4.5 + 720;
                            labfinishTime.Text = "预计完成时间:" + DateTime.Now.AddSeconds(time).ToString();
                            SendCommand("StartSchedule");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("没有测试任务！");
                    return;
                }
                //BeginInvoke(new Action(() =>
                //{
                //    CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.MainTain, new Dictionary<string, object[]> { { "GetAllTasksCount", null } });
                //}));
            }

        }

        //暂停
        private Image Suspend = System.Drawing.Image.FromFile(_FileName + "Resources\\Image\\Tempstoping.png");
        //启动
        private Image Firing = System.Drawing.Image.FromFile(_FileName +"Resources\\Image\\Execing.png");

        /// <summary>
        /// 启动按钮
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "GetAllTasksCount":
                    List<int> lstResult = (List<int>)sender;
                    if (lstResult[0] != 0)
                    {
                        if (this.OPID == 0)
                        {
                            if (MessageBoxDraw.ShowMsg("确定开始样本测试吗？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
                            {                               
                                SendCommand("StartSchedule");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有测试任务！");
                        return;
                    }
                    break;
            }
        }
        /// <summary>
        /// 紧急暂停按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.OPID == 3 && this.OPID == 0)
            {
                return;
            }
            if (MessageBoxDraw.ShowMsg("现在确定要紧急停止吗？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
            {
                SendCommand("AbortSchedule");
            }
        }

       

        private void accordionControlElement21_Click(object sender, EventArgs e)
        {
            if (pcThirdArea.Controls.Equals(configure) == false)
            {
                pcThirdArea.Controls.Clear();
                configure = new Configure();
                pcThirdArea.Controls.Add(configure);
            }
        }
        

        private void pcThirdArea_Paint(object sender, PaintEventArgs e)
        {

        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }

        private void Timer1_Timer(object sender, EventArgs e)
        {
            textEdit2.Text = DateTime.Now.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            this.Close();
            System.Environment.Exit(System.Environment.ExitCode);
        }
        /// <summary>
        /// 故障日志点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.pictureBox2.MouseClick += this.ChangeiconEvent;
            this.accordionControl1.Elements.Clear();
            //List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements = new List<DevExpress.XtraBars.Navigation.AccordionControlElement>();
            this._Elements.Clear();
            if (userInfo.LogCheck)
                _Elements.Add(this.LogCheckElement22);
            this.accordionControl1.Elements.AddRange(_Elements.ToArray());
            if (userInfo.RouMaintain)
            {
                if (pcThirdArea.Controls.Equals(log) == false)
                {
                    pcThirdArea.Controls.Clear();
                    if (CommunicationUI.notifyCallBack.LogDataTransferEvent != null)
                        CommunicationUI.notifyCallBack.LogDataTransferEvent -= log.DataTransfer_Event;
                    log = new Log();
                    log.UserName = labUserName.Text;
                    CommunicationUI.notifyCallBack.LogDataTransferEvent += log.DataTransfer_Event;
                    txtPrompt.Text = "您当前的操作：安全管理——日志查看";
                    pcThirdArea.Controls.Add(txtPrompt);
                    pcThirdArea.Controls.Add(log);
                }
            }
        }
        //是否在故障界面信息上
        private bool IsWarningInfoUIActivity = false;
        private void ChangeiconEvent(object sender, MouseEventArgs e)
        {
            
            this.IsWarningInfoUIActivity = true;
            this.Invoke(new Action(() => { this.pictureBox2.Image = this.pictureBox2.InitialImage; }));
            ErrorFaultSignal.Reset();
        }
        /// <summary>
        /// 取消主功能按钮标记
        /// </summary>
        private void CancelClickSign()
        {
            this.barButtonItem2.AllowDrawArrow = false;
            this.barButtonItem3.AllowDrawArrow = false;
            this.barButtonItem4.AllowDrawArrow = false;
            this.barButtonItem17.AllowDrawArrow = false;
            this.barButtonItem11.AllowDrawArrow = false;
            this.barButtonItem12.AllowDrawArrow = false;
            if (this.IsWarningInfoUIActivity == true)
            {
                this.IsWarningInfoUIActivity = false;
                this.Invoke(new Action(() => { this.pictureBox2.Image = System.Drawing.Image.FromFile(_FileName + "Resources\\Image\\WarnUn.png"); }));
            }
        }
        /// <summary>
        /// 取消功能列表标记图标
        /// </summary>
        private void FeatureListTagIcon(List<DevExpress.XtraBars.Navigation.AccordionControlElement> _Elements)
        {
            foreach(DevExpress.XtraBars.Navigation.AccordionControlElement accrodionElement in _Elements)
            {
                accrodionElement.Image = null;
            }
        }
        /// <summary>
        /// 获取机器状态
        /// </summary>
        /// <returns></returns>
        public bool getOPIDEvent()
        {
            if (OPID == 0 || OPID == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// LIS设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LISSetting LIS = new LISSetting();
            LIS.StartPosition = FormStartPosition.CenterScreen;
            LIS.ShowDialog();
        }
        /// <summary>
        /// 获取预计完成时间
        /// </summary>
        /// <returns></returns>
        public int getFinishTime()
        {
            return myBatis.getFinishTime();
        }
        // private void ribbonControl1_Click(object sender, EventArgs e)
        // {

        //}

        //private void ribbonPageGroup1_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
        //{
        // MessageBox.Show("hahahaha");
        //        }

        //    private void ribbonPageGroup1_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
        //   {
        //        MessageBox.Show("test");
        //  }
        /*  protected override void WndProc(ref Message m)
          {
              if (m.Msg == 0x112)
              {
                  switch ((int)m.WParam)
                  {
                      //禁止双击标题栏关闭窗体
                      case 0xF063:
                      case 0xF093:
                          m.WParam = IntPtr.Zero;
                          break;

                      //禁止拖拽标题栏还原窗体
                      case 0xF012:
                      case 0xF010:
                          m.WParam = IntPtr.Zero;
                          break;

                      //禁止双击标题栏
                      case 0xf122:
                          m.WParam = IntPtr.Zero;
                          break;

                      //禁止关闭按钮
                      case 0xF060:
                          m.WParam = IntPtr.Zero;
                          break;

                      //禁止最大化按钮
                      case 0xf020:
                          m.WParam = IntPtr.Zero;
                          break;

                      //禁止最小化按钮
                      case 0xf030:
                          m.WParam = IntPtr.Zero;
                          break;

                      //禁止还原按钮
                      case 0xf120:
                          m.WParam = IntPtr.Zero;
                          break;
                  }
              }
              base.WndProc(ref m);
          }*/
    }
}
