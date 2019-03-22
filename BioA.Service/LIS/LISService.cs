using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using BioA.HL7.V231;
using BioA.Common.Machine;
using BioA.SqlMaps;

namespace BioA.Service
{

    public class LISSet
    {
        public LISSettingInfo lisSettingInfo { get; set; }

        public LISCommunicateNetworkInfo lisNetworkInfo { get; set; }

        public SerialCommunicationInfo serialCommInfo { get; set; }
    }

    class TcpClientDataRead
    {
        public NetworkStream ns;
        public byte[] msg;
        public TcpClientDataRead(NetworkStream ns, int buffersize)
        {
            this.ns = ns;
            msg = new byte[buffersize];
        }
    }

    public class LISService
    {
        MyBatis MyBatis = new MyBatis();
        bool IsConnecting = false;//连通标志
        long LISMSGID = 0;
        const byte SB = 0x0B;
        const byte EB = 0x1C;
        const byte CR = 0x0D;
        public LISSet LisSet = new LISSet();

        /// <summary>
        /// 发送结果数据集合
        /// </summary>
        List<object> SendObjectBuffer = new List<object>();
        /// <summary>
        /// 接收串口/网络数据的队列集合
        /// </summary>
        Queue<string> LisDataRevBuff = new Queue<string>();
        object SendObject = null;
        /// <summary>
        /// 开启LIS服务
        /// </summary>
        public void StartService()
        {
            ReadLisSet();
            InitConnect();

            Thread.Sleep(2000);

            //new Thread(new ThreadStart(SendData)).Start();
            //new Thread(new ThreadStart(ParseLisDataRevBuff)).Start();
            Task.Run(() => { SendData(); });
            Task.Run(() => { ParseLisDataRevBuff(); });

            if (this.LISRevEvent == null)
            {
                this.LISRevEvent += new LISServiceHandler(OnLISDataRevService);
            }
        }
        /// <summary>
        /// 清除数据后，停止LIS服务
        /// </summary>
        public void StopService()
        {
            if (this.LISRevEvent != null)
            {
                this.LISRevEvent -= new LISServiceHandler(OnLISDataRevService);
            }

            if (this.SerialPort != null && this.SerialPort.IsOpen == true)
            {
                this.SerialPort.Close();
            }
            if (this.TcpClient != null && this.TcpClient.Connected == true)
            {
                this.TcpClient.Client.Close();
                this.TcpClient.Close();
            }

            this.IsConnecting = false;
            Thread.Sleep(300);
            this.HavingSentObjectSignal.Set();

            lock (LisDataRevBuff)
            {
                LisDataRevBuff.Clear();
            }
            lock (SendObjectBuffer)
            {
                SendObjectBuffer.Clear();
            }
        }
        /// <summary>
        /// 获取LIS设置所有信息
        /// </summary>
        public void ReadLisSet()
        {
            object[] lis = new LISSettingService().QueryLISSettingInfo() as object[];
            if (lis[0] as LISSettingInfo != null)
            {
                LisSet.lisSettingInfo = lis[0] as LISSettingInfo;
            }
            if (lis[1] as SerialCommunicationInfo != null)
            {
                LisSet.serialCommInfo = lis[1] as SerialCommunicationInfo;
            }

            if (lis[2] as LISCommunicateNetworkInfo != null)
            {
                LisSet.lisNetworkInfo = lis[2] as LISCommunicateNetworkInfo;
            }
        }

        /// <summary>
        /// 初始化连接
        /// </summary>
        public void InitConnect()
        {
            if (LisSet.lisSettingInfo.CommunicationMode == "串口")
            {
                InitSerialPort();
            }
            else
                ConnectServer();
        }

        /// <summary>
        /// 接收样本结果信息
        /// </summary>
        /// <param name="rd"></param>
        public void ReceiveSampleResultEvent_Event(ResultData rd)
        {
            this.AddData(rd);
        }

        /// <summary>
        /// 处理发送给LIS服务数据的线程，（信号决定线程状态：set 执行线程，waitOne 阻塞，reset 重置）
        /// </summary>
        ManualResetEvent HavingSentObjectSignal = new ManualResetEvent(false);
        /// <summary>
        /// 发送的结果数据和条型码扫码存入队列中
        /// </summary>
        /// <param name="d"></param>
        public void AddData(object d)
        {
            lock (this.SendObjectBuffer)
            {
                this.SendObjectBuffer.Add(d);
            }
            HavingSentObjectSignal.Set();
        }

        SerialPort SerialPort = new SerialPort();
        /// <summary>
        /// 初始化串口连接
        /// </summary>
        public void InitSerialPort()
        {
            try
            {
                if (this.SerialPort.IsOpen == true)
                    this.SerialPort.Close();
                this.SerialPort.PortName = LisSet.serialCommInfo.SerialName;
                this.SerialPort.BaudRate = LisSet.serialCommInfo.BaudRate;
                this.SerialPort.DataBits = LisSet.serialCommInfo.DataBits;
                switch (LisSet.serialCommInfo.StopBits)
                {
                    case "无": this.SerialPort.StopBits = StopBits.None; break;
                    case "1": this.SerialPort.StopBits = StopBits.One; break;
                    case "1.5": this.SerialPort.StopBits = StopBits.Two; break;
                    case "2": this.SerialPort.StopBits = StopBits.OnePointFive; break;
                }
                switch (this.LisSet.serialCommInfo.Parity)
                {
                    case "None": this.SerialPort.Parity = Parity.None; break;
                    case "Even": this.SerialPort.Parity = Parity.Even; break;
                    case "Odd": this.SerialPort.Parity = Parity.Odd; break;
                    case "Mark": this.SerialPort.Parity = Parity.Mark; break;
                    case "Space": this.SerialPort.Parity = Parity.Space; break;
                }
                this.SerialPort.ReadBufferSize = 2048;
                this.SerialPort.ReadTimeout = 2000;
                this.SerialPort.ReceivedBytesThreshold = 1;
                this.SerialPort.DataReceived += new SerialDataReceivedEventHandler(OnSerialPortDataReceived);

                this.SerialPort.Open();
                OnConnectSuccess(null);
            }
            catch (Exception ex)
            {
                OnLisError(this.LisSet.serialCommInfo.SerialName + "连接失败....");
            }
        }
        /// <summary>
        /// 接收串口数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] readBuffer = new byte[this.SerialPort.ReadBufferSize + 1];
                int count = this.SerialPort.Read(readBuffer, 0, this.SerialPort.ReadBufferSize);

                byte[] GetData = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    GetData[i] = readBuffer[i];
                }
                OnLISRev(Encoding.UTF8.GetString(GetData, 0, count));
            }
            catch (Exception ex)
            {
                OnLisError(this.LisSet.serialCommInfo.SerialName + ex.Message);
            }
        }
        /// <summary>
        /// 处理接收到的LIS数据线程（根据信号来决定线程状态：set 执行线程，waitOne 阻塞，reset 重置）
        /// </summary>
        ManualResetEvent RevSignal = new ManualResetEvent(false);
        /// <summary>
        /// 执行串口委托事件数据服务
        /// </summary>
        /// <param name="sender"></param>
        void OnLISDataRevService(object sender)
        {
            string d = sender as string;

            if (d.Length > 0)
            {
                lock (LisDataRevBuff)
                {
                    LisDataRevBuff.Enqueue(d);
                }
                RevSignal.Set();
            }
        }

        object parselocker = new object();
        /// <summary>
        /// 接收LIS数据进行解析
        /// </summary>
        void ParseLisDataRevBuff()
        {
            lock (parselocker)
            {
                string Msg = "";
                while (this.IsConnecting == true)
                {
                    //Msg = "";
                    RevSignal.WaitOne();
                    if (LisDataRevBuff.Count > 0)
                    {
                        string revstr = null;
                        lock (LisDataRevBuff)
                        {
                            revstr = LisDataRevBuff.Dequeue();
                        }

                        if (revstr != null)
                        {

                            foreach (char c in revstr)
                            {
                                if (c == SB)
                                {
                                    Msg = "";
                                    Msg += c;
                                }
                                else
                                {
                                    Msg += c;
                                }
                                if (Msg[Msg.Length - 1] == CR && Msg[Msg.Length - 2] == EB)
                                {
                                    Msg = Msg.TrimStart((char)SB);
                                    Msg = Msg.TrimEnd(new char[2] { (char)EB, (char)CR });
                                    //消息解析
                                    try
                                    {
                                        ParseMessage(Msg);
                                    }
                                    catch
                                    {
                                        TroubleLog TroubleLog = new TroubleLog();
                                        TroubleLog.TroubleCode = "1777772";
                                        TroubleLog.TroubleType = TROUBLETYPE.WARN;
                                        TroubleLog.TroubleUnit = @"LIS";
                                        TroubleLog.TroubleInfo = "接受非法格式数据";
                                        MyBatis.TroubleLogSave("TroubleLogSave", TroubleLog);
                                    }
                                }
                            }
                        }
                        //再次判断内存还有数据
                        if (LisDataRevBuff.Count > 0)
                        {
                            RevSignal.Set();
                        }
                    }
                    else
                    {
                        RevSignal.Reset();
                    }
                }
            }
        }
        /// <summary>
        /// 解析信息
        /// </summary>
        /// <param name="message"></param>
        void ParseMessage(string message)
        {
            //LogService.Log("LIS Rev:->" + message, LogType.Debug, "ParseMessage.lg");

            string[] SegmentArray = MessageAnalyzer.GetSegments(message);//从消息包解析出消息段

            List<Dictionary<int, string>> FieldsList = new List<Dictionary<int, string>>();
            foreach (string segment in SegmentArray)
            {
                Dictionary<int, string> Fields = ParseSegments(segment);//从消息段解析出消息序号和信息
                FieldsList.Add(Fields);//从消息包解析出消息字典
            }

            List<Dictionary<int, string>> fdlist = new List<Dictionary<int, string>>();
            List<Dictionary<int, string>> MSHfdlist = GetFieldDictionary("MSH", FieldsList);
            string MsgType = MSHfdlist[0][8];
            switch (MsgType)
            {
                case "ACK^R01"://确认发送的结果数据
                    fdlist = GetFieldDictionary("MSA", FieldsList);
                    if (long.Parse(fdlist[0][2]) == this.LISMSGID)
                    {
                        switch (fdlist[0][1])
                        {
                            case "AA":
                                if ((this.SendObject is LISData) == true)
                                {
                                    UpdateResultsSendFlag((SendObject as LISData).Results);
                                }
                                if ((this.SendObject is string) == true)
                                {

                                }
                                break;//表示接受
                            case "AE":
                                this.OnLisError(fdlist[0][3]);
                                break;//表示错误
                            case "AR":
                                this.OnLisError(fdlist[0][3]);
                                break;//表示拒绝
                        }
                        this.IsTimeOut = false;
                        this.ACKResultSignal.Set();
                    }
                    break;
                case "QCK^Q02"://确认查询的样本状态
                    fdlist = GetFieldDictionary("QAK", FieldsList);
                    switch (fdlist[0][2])
                    {
                        case "OK": //查询到数据
                            this.IsSampleBarcodeExsiting = true;
                            break;
                        case "NF"://没有查到数据
                            this.IsSampleBarcodeExsiting = false;
                            this.IsTimeOut = false;
                            this.ACKResultSignal.Set();
                            break;
                        default: //没有查询到样本条码对应的信息
                            break;
                    }
                    break;
                case "DSR^Q03"://样本的任务信息
                    ProcessDSRQ03Message(FieldsList);
                    break;
            }
        }
        /// <summary>
        /// 从信息段中解析信息序号和信息
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        Dictionary<int, string> ParseSegments(string segment)
        {
            Dictionary<int, string> Fields = new Dictionary<int, string>();

            string[] FieldArray = MessageAnalyzer.GetFields(segment);
            for (int i = 0; i < FieldArray.Length; i++)
            {
                Fields.Add(i, FieldArray[i]);
            }

            return Fields;
        }
        /// <summary>
        /// 根据传入字符串去匹配字典中Key想同的对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="FieldsList"></param>
        /// <returns></returns>
        List<Dictionary<int, string>> GetFieldDictionary(string name, List<Dictionary<int, string>> FieldsList)
        {
            List<Dictionary<int, string>> FieldDictionaryList = new List<Dictionary<int, string>>();
            foreach (Dictionary<int, string> e in FieldsList)
            {
                if (e[0] == name)
                {
                    FieldDictionaryList.Add(e);
                }
            }
            return FieldDictionaryList;
        }
        /// <summary>
        /// 解析样本任务信息
        /// </summary>
        /// <param name="MessageDictionary"></param>
        void ProcessDSRQ03Message(List<Dictionary<int, string>> MessageDictionary)
        {
            //LogService.Log("LIS Rev:-> ProcessDSRQ03Message", LogType.Debug, "ParseMessage.lg");

            List<Dictionary<int, string>> fdlist = GetFieldDictionary("DSP", MessageDictionary);
            string SampleBarcode = this.SendObject as string;
            SampleInfo Sample = MyBatis.GetSampleByBarcode(SampleBarcode, DateTime.Now);
            if (Sample == null)//由手持条码枪申请样本
            {
                Sample = new SampleInfo();

                int newSampNum = MyBatis.GetLastestSMPNO() + 1;
                MyBatis.UpdateLastestSMPNum(newSampNum);

                Sample.SampleNum = newSampNum;

            }

            PatientInfo Patient = MyBatis.AccordingSampNumGetPatientInfo(Sample.SampleNum, DateTime.Now);
            if (Patient == null)
            {
                Patient = new PatientInfo();
                Patient.SampleNum = Sample.SampleNum;
            }

            List<string> AssayList = new List<string>();
            foreach (Dictionary<int, string> e in fdlist)
            {
                try
                {
                    switch (int.Parse(e[1]))
                    {
                        case 0: break;
                        case 1: Patient.MedicalRecordNum = e[3]; break;
                        case 2: Patient.BedNum = e[3]; break;
                        case 3: Patient.PatientName = e[3]; break;
                        case 4: Patient.BirthDate = DateTime.Parse(e[3]); break;
                        case 5: 
                            switch (e[3])
                            {
                                case "M":
                                    Patient.Sex = "男";
                                    break;
                                case "F":
                                    Patient.Sex = "女";
                                    break;
                                default:
                                    Patient.Sex = "--";
                                    break;
                            }
                            break;
                        case 6: break;//Patient.BloodType = e[3]; break; 没有该字段（患者血型）
                        case 7: break;
                        case 8: break;
                        case 9: break;
                        case 10: break;
                        case 11: break;
                        case 12: break;
                        case 13: break;
                        case 14: break;
                        case 15: Patient.PatientType = e[3]; break;
                        case 16: break;
                        case 17: break;//Patient.ChargeType = e[3]; break;  没有该字段（收费类型）
                        case 18: break;
                        case 19: break;
                        case 20: break;
                        case 21: Sample.Barcode = e[3]; break;//样本条码
                        case 22: break;
                        case 23: Patient.InspectTime = DateTime.Parse(e[3]); break;
                        case 24:
                            switch (e[3])
                            {
                                case "Y": Sample.IsEmergency = true; break;
                                default: Sample.IsEmergency = false; break;
                            }
                            break;
                        case 25://样本容器0:样本杯,1:样本管
                            try
                            {
                                int ic = int.Parse(e[3]);
                                //获取容器类型
                                //SMPContainerLisSetData contype = new SMPContainerLisSetDataService().GetSMPContainerLisSetData(ic);
                                //if (contype != null)
                                //{
                                //    Sample.ContainerType = contype.SampleContainer;
                                //}
                                //else
                                //{
                                //    List<CLItem> types = new SMPContainerTypeService().GetALL();
                                //    Sample.ContainerType = (types[0] as SMPType).Name;
                                //}
                                switch (ic)
                                {
                                    case 0:
                                        Sample.SamContainer = "样本杯";
                                        break;
                                    case 1:
                                        Sample.SamContainer = "样本管";
                                        break;
                                    default:
                                        Sample.SamContainer = "样本管";
                                        break;
                                }
                            }
                            catch
                            {
                                //List<CLItem> types = new SMPContainerTypeService().GetALL();
                                //Sample.ContainerType = (types[0] as SMPContainerType).Name;
                                Sample.SamContainer = "样本管";
                            }
                            break;
                        case 26:
                            try
                            {
                                int ic = int.Parse(e[3]);
                                //获取样本类型
                                //SMPTypeLisSetData smptype = new SMPTypeLisSetDataService().GetSMPTypeLisSetData(ic);
                                //if (smptype != null)
                                //{
                                //    Sample.SampleType = smptype.SampleType;
                                //}
                                //else
                                //{
                                //    List<CLItem> types = new SMPTypeService().GetALL();
                                //    Sample.SampleType = (types[0] as SMPType).Name;
                                //}
                                switch (ic)
                                {
                                    case 0:
                                        Sample.SampleType = "血清";
                                        break;
                                    case 1:
                                        Sample.SampleType = "尿液";
                                        break;
                                    default:
                                        Sample.SampleType = "血清";
                                        break;
                                }
                            }
                            catch
                            {
                                //List<CLItem> types = new SMPTypeService().GetALL();
                                //Sample.SampleType = (types[0] as SMPType).Name;
                                Sample.SamContainer = "血清";
                            }
                            break;//样本类型:serum;urine
                        case 27: Patient.InspectDoctor = e[3]; break;//送检医师
                        case 28: Patient.ApplyDepartment = e[3]; break;//送检科室
                        default:
                            string[] astrs = e[3].Split('^');
                            foreach (string a in astrs)
                            {
                                if (!string.IsNullOrEmpty(a) && !string.IsNullOrWhiteSpace(a))
                                {
                                    AssayList.Add(a);
                                }
                            }
                            break;
                    }
                }
                catch(Exception ex)
                {
                    LogInfo.WriteErrorLog("ProcessDSRQ03Message(List<Dictionary<int, string>> MessageDictionary) ==" + ex.Message, Module.LISService);
                    continue;
                }
            }


            foreach (string a in AssayList)
            {
                //Schedule S = new Schedule();
                TaskInfo t = new TaskInfo();
                t.SampleNum = Sample.SampleNum;
                t.ProjectName = a;
                t.SampleType = Sample.SampleType;
                //if (Sample.IsEmergency == true)
                //{
                //    S.WorkType = WORKTYPE.E;
                //}
                //else
                //{
                //    S.WorkType = WORKTYPE.N;
                //}
                //S.VolType = VOLTYPE.NV;
                t.SendTimes = 0;
                t.FinishTimes = 0;
                t.InspectTimes = 1;
                //if (a == "K" || a == "Na" || a == "CL")
                //{
                //    S.AssayType = ITEMTYPE.ISE;
                //}
                //else
                //{
                //    S.AssayType = ITEMTYPE.ASSAY;
                //}
                if ( MyBatis.GetTaskByProjectNameAndSampNum(t.ProjectName, Sample.SampleNum) == null)
                {
                    MyBatis.InsertTaskInfo(t);
                        
                    //LogService.Log("LIS Rev:-> new ScheduleService().Save(S)", LogType.Debug, "ParseMessage.lg");
                }

                if (a == "K" || a == "Na" || a == "CL")
                {
                }
                else
                {
                    AssayProjectParamInfo  assay = MyBatis.GetAssayProjectParamInfo(t.ProjectName,t.SampleType);
                    if (assay == null)
                    {
                        TroubleLog trouble = new TroubleLog();
                        trouble.TroubleType = TROUBLETYPE.ERR;
                        trouble.TroubleUnit = @"LIS服务";
                        trouble.TroubleCode = "000004";
                        trouble.TroubleInfo = string.Format(@"项目：{0}设备无法识别,请确认设备和LIS系统的项目通道设置. ", a);
                        MyBatis.TroubleLogSave("TroubleLogSave", trouble);
                    }
                }

            }
            //样本位置表没有，只能先注释，以后有需求在加上
            //SMPPosition p = new SMPPOSManager().Get(Sample.SMPNO) as SMPPosition;
            //if (p == null)
            //{
            //    p = new SMPPosition();
            //    p.SMPNO = Sample.SMPNO;

            //    int sn = int.Parse(Sample.SMPNO);
            //    if (sn % MachineInfo.SMPPositionCount == 0)
            //    {
            //        p.Disk = sn / MachineInfo.SMPPositionCount;
            //        p.Position = MachineInfo.SMPPositionCount.ToString();
            //    }
            //    else
            //    {
            //        p.Disk = sn / MachineInfo.SMPPositionCount + 1;
            //        p.Position = (sn % MachineInfo.SMPPositionCount).ToString();
            //    }
            //    new SMPPOSManager().Save(p);
            //}
            int sn = Sample.SampleNum;
            //样本盘最大位置
            int samplePosit = Convert.ToInt32(RunConfigureUtility.SamplePosition[119]);
            if (sn % samplePosit == 0)
            {
                Sample.PanelNum = sn / samplePosit;
                Sample.SamplePos = samplePosit;
            }
            else
            {
                Sample.PanelNum = sn / samplePosit + 1;
                Sample.SamplePos = sn % samplePosit;
            }
            MyBatis.DeleteSampleInfo(Sample.SampleNum);
            MyBatis.SaveSampleInfo(Sample);

            MyBatis.DeletePatientInfo(Sample.SampleNum);
            MyBatis.SavePatientInfo(Patient);

            fdlist = GetFieldDictionary("MSA", MessageDictionary);
            if (long.Parse(fdlist[0][2]) == this.LISMSGID)
            {
                fdlist = GetFieldDictionary("MSH", MessageDictionary);
                string ACKQ03String = ACKQ03HL7Encode(fdlist[0][9]);
                if (this.LisSet.lisSettingInfo.CommunicationMode == "串口")
                {
                    this.SerialPortSend(ACKQ03String);
                }
                else
                {
                    this.TcpClientSend(ACKQ03String);
                }

                //LogService.Log("LIS Send:->" + ACKQ03String, LogType.Debug, "lissent.lg");
                LogInfo.WriteProcessLog("LIS Send:->" + ACKQ03String, Module.LISService);
            }
            else
            {
                LogInfo.WriteProcessLog(string.Format("LIS Rev:消息ID错误；long.Parse(fdlist[0][2])={0}，this.LISMSGID={1}", fdlist[0][2], this.LISMSGID), Module.LISService);
            }

            this.IsTimeOut = false;
            this.ACKResultSignal.Set();

            LogInfo.WriteProcessLog("LIS Rev:-> this.IsTimeOut = false this.ACKResultSignal.Set()", Module.LISService);
        }
        /// <summary>
        /// 将数据编码成HL7格式
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        string ACKQ03HL7Encode(string mid)
        {
            MSH MSH = new MSH();
            MSH.Fields[0] = @"|";
            MSH.Fields[1] = @"^~\&";
            MSH.Fields[2] = @"NAD";
            MSH.Fields[3] = MachineInfo.Type;
            MSH.Fields[4] = @"";
            MSH.Fields[5] = @"";
            MSH.Fields[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MSH.Fields[7] = @"";
            MSH.Fields[8] = @"ACK^Q03";
            MSH.Fields[9] = this.LISMSGID.ToString();// 消息控制ID
            MSH.Fields[10] = @"P";
            MSH.Fields[11] = @"2.3.1";
            MSH.Fields[12] = @"";
            MSH.Fields[13] = @"";
            MSH.Fields[14] = @"";
            MSH.Fields[15] = @"";
            MSH.Fields[16] = @"";
            MSH.Fields[17] = @"ASCII";
            MSH.Fields[18] = @"";
            MSH.Fields[19] = @"";

            MSA MSA = new MSA();
            MSA.Fields[0] = @"AA";
            MSA.Fields[1] = mid;
            MSA.Fields[2] = @"Message accepted";
            MSA.Fields[3] = @"";
            MSA.Fields[4] = @"";
            MSA.Fields[5] = @"0";

            ERR ERR = new ERR();
            ERR.Fields[0] = "0";

            string Str = MSH.GetString() + (char)CR + MSA.GetString() + (char)CR + ERR.GetString() + (char)CR;

            return (char)SB + Str + (char)EB + (char)CR;
        }

        bool isLive = false;
        /// <summary>
        /// 网路数据流
        /// </summary>
        NetworkStream networkStream;
        /// <summary>
        /// 为TCP网路服务提供客户端连接
        /// </summary>
        TcpClient TcpClient;
        /// <summary>
        /// 处理网络连接获取LIS数据线程，（根据信号来决定线程状态：set 执行线程，waitOne 阻塞，reset 重置）
        /// </summary>
        ManualResetEvent TcpClientSignal = new ManualResetEvent(false);
        /// <summary>
        /// 初始化网络连接
        /// </summary>
        public void ConnectServer()
        {
            try
            {
                TcpClient = new TcpClient(AddressFamily.InterNetwork);
                IPAddress IP = IPAddress.Parse(this.LisSet.lisNetworkInfo.IPAddress);
                AsyncCallback connectCallBack = new AsyncCallback(ConnectCallBack);
                TcpClientSignal.Reset();
                TcpClient.BeginConnect(IP, int.Parse(this.LisSet.lisNetworkInfo.NetworkPort), connectCallBack, TcpClient);
                TcpClientSignal.WaitOne();
            }
            catch
            {
                OnLisError(this.LisSet.lisNetworkInfo.IPAddress + ":" + this.LisSet.lisNetworkInfo.NetworkPort + ":" + "连接失败.... ");
            }
        }
        void ConnectCallBack(IAsyncResult iar)
        {
            TcpClientSignal.Set();
            try
            {

                TcpClient = (TcpClient)iar.AsyncState;
                TcpClient.EndConnect(iar);
                OnConnectSuccess(null);
                networkStream = TcpClient.GetStream();
                TcpClientDataRead dataRead = new TcpClientDataRead(networkStream, TcpClient.ReceiveBufferSize);
                networkStream.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);
            }
            catch (Exception e)
            {
                OnLisError(this.LisSet.lisNetworkInfo.IPAddress + ":" + this.LisSet.lisNetworkInfo.NetworkPort + ":" + "连接失败.... ");
            }
        }
        /// <summary>
        /// 接收网络数据
        /// </summary>
        /// <param name="iar"></param>
        void ReadCallBack(IAsyncResult iar)
        {
            try
            {
                TcpClientDataRead dataRead = (TcpClientDataRead)iar.AsyncState;
                int recv = dataRead.ns.EndRead(iar);
                OnLISRev(Encoding.UTF8.GetString(dataRead.msg, 0, recv));

                if (isLive == false)
                {
                    dataRead = new TcpClientDataRead(networkStream, TcpClient.ReceiveBufferSize);
                    networkStream.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);
                }
            }
            catch (Exception e)
            {
                OnLisError("从LIS服务器获取网络数据失败. ");
            }
        }
        /// <summary>
        /// 通过网络发送数据
        /// </summary>
        /// <param name="str"></param>
        void TcpClientSend(string str)
        {
            try
            {
                byte[] bytesdata = Encoding.UTF8.GetBytes(str);
                networkStream.BeginWrite(bytesdata, 0, bytesdata.Length, new AsyncCallback(SendCallBack), networkStream);
                networkStream.Flush();
            }
            catch (Exception e)
            {
                OnLisError("通过网络向LIS服务器发送数据失败. ");
            }
        }
        void SendCallBack(IAsyncResult iar)
        {
            try
            {
                networkStream.EndWrite(iar);
            }
            catch (Exception e)
            {
                OnLisError("通过网络向LIS服务器发送数据失败2. ");
            }
        }
        /// <summary>
        /// 通过串口发送数据
        /// </summary>
        /// <param name="txt"></param>
        void SerialPortSend(string txt)
        {
            try
            {
                this.SerialPort.Write(txt);
            }
            catch (Exception e)
            {
                OnLisError(this.LisSet.serialCommInfo.SerialName + e.Message);
            }
        }
        /// <summary>
        /// 修改法送结果状态
        /// </summary>
        /// <param name="results"></param>
        void UpdateResultsSendFlag(List<SampleResultInfo> results)
        {
            //ResultService ResultSer = new ResultService();

            foreach (SampleResultInfo r in results)
            {
                r.IsSend = true;

                //Result R = ResultSer.GetNORResult(e.TCNO, e.DrawDate);
                //if ( = null)
                //{
                MyBatis.UpdateSendSMPResultStatus(r);
                    //continue;
                //}
                //R = ResultSer.GetQCResult(e.TCNO, e.DrawDate);
                //if (R != null)
                //{
                //    ResultSer.UpdateQCResult(e);
                //    continue;
                //}

                //R = ResultSer.GetSDTResult(e.TCNO, e.DrawDate);
                //if (R != null)
                //{
                //    ResultSer.UpdateSDTResult(e);
                //    continue;
                //}

            }
        }

        public delegate void LISServiceHandler(object sender);

        event LISServiceHandler LISRevEvent;
        /// <summary>
        /// 收到LIS服务返回数据的委托事件
        /// </summary>
        /// <param name="sender"></param>
        void OnLISRev(object sender)
        {
            if (this.LISRevEvent != null)
            {
                this.LISRevEvent(sender);
            }
        }

        public event LISServiceHandler LisErrorEvent;
        /// <summary>
        /// 网络连接或者发送失败
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnLisError(object sender)
        {
            if (this.LisErrorEvent != null)
            {
                if (this.LisSet.lisSettingInfo.CommunicationMode == "网络")
                {
                }
                else
                {
                    this.IsConnecting = false;
                }

                this.LisErrorEvent(sender);
            }
        }

        bool IsTimeOut = false;
        ManualResetEvent ACKResultSignal = new ManualResetEvent(false);
        bool IsSampleBarcodeExsiting = false;
        object sendlocker = new object();
        /// <summary>
        /// 发送数据
        /// </summary>
        void SendData()
        {
            lock (sendlocker)
            {
                while (this.IsConnecting == true)
                {
                    //有发送任务
                    if (this.SendObjectBuffer.Count > 0)
                    {
                        OnSendLisResultDataRunning("正在发送数据....");

                        this.SendObject = null;
                        lock (this.SendObjectBuffer)
                        {
                            this.SendObject = this.SendObjectBuffer.Last();
                        }

                        string sendString = null;
                        //测试结果发送
                        if ((this.SendObject is LISData) == true)
                        {
                            sendString = ResultDataHL7Encode(this.SendObject as LISData);
                        }

                        //条码任务请求发送
                        if ((this.SendObject is string) == true)
                        {
                            this.IsSampleBarcodeExsiting = false;
                            sendString = QRYQ02HL7Encode(this.SendObject as string);
                        }

                        if (this.LisSet.lisSettingInfo.CommunicationMode == "串口")
                        {
                            this.SerialPortSend(sendString);
                        }
                        else
                        {
                            this.TcpClientSend(sendString);
                        }

                        //LogService.Log("LIS Send:->" + sendString, LogType.Debug, "lissent.lg");

                        //等待服务应答
                        this.IsTimeOut = true;//发送超时标志；
                        ACKResultSignal.WaitOne(this.LisSet.lisSettingInfo.CommunicationOverTime * 1000);
                        if (this.IsTimeOut == true)
                        {
                            //LogService.Log("LIS Send:Time Out!", LogType.Debug, "lissent.lg");
                            //结果发送超时
                            if ((this.SendObject is LISData) == true)
                            {
                                lock (this.SendObjectBuffer)
                                {
                                    this.SendObjectBuffer.Remove(this.SendObject);
                                }
                                //OnSendLisData("样本结果数据发送超时. ");
                                OnSendLisResultDataFailed("结果数据发送超时.... ");
                            }
                            //样本请求超时
                            if ((this.SendObject is string) == true)
                            {
                                lock (this.SendObjectBuffer)
                                {
                                    this.SendObjectBuffer.Remove(this.SendObject);
                                }
                                OnSMPCodeBarQueryEvent("样本条码：" + (SendObject as string) + "向LIS服务器发出申请超时.... ");
                            }
                        }
                        else//服务给出了应答信息
                        {
                            //LogService.Log("LIS Send:OK!", LogType.Debug, "lissent.lg");
                            //结果发送成功确认
                            if ((this.SendObject is LISData) == true)
                            {
                                UpdateResultsSendFlag((SendObject as LISData).Results);
                                //OnSendLisData("样本结果数据发送成功. ");
                                OnSendLisResultDataOK("结果数据发送成功.... ");
                            }
                            //样本发送成功确认
                            if ((this.SendObject is string) == true)
                            {
                                if (this.IsSampleBarcodeExsiting == true)
                                {
                                    //OnSendLisData("样本任务请求成功. ");
                                    OnApplySampleSuccessEvent(this.SendObject);
                                }
                                else
                                {
                                    OnSMPCodeBarQueryEvent("样本条码：" + (SendObject as string) + " 没有查询到，请确认条码.... ");
                                }

                            }

                            lock (this.SendObjectBuffer)
                            {
                                this.SendObjectBuffer.Remove(this.SendObject);
                            }

                            this.LISMSGID++;
                        }
                        ACKResultSignal.Reset();
                    }

                    if (this.SendObjectBuffer.Count == 0 && this.LisSet.lisSettingInfo.RealTiimeSampleResults == true)
                    {
                        this.GetSMPLatestResult();
                    }

                    if (this.SendObjectBuffer.Count == 0)
                    {
                        OnNotHasLisDataEvent("暂时没有数据需要发送.... ");
                        HavingSentObjectSignal.WaitOne(1000 * 5);
                        HavingSentObjectSignal.Reset();
                    }
                }
            }
        }
        /// <summary>
        /// 获取最新的样本结果
        /// </summary>
        void GetSMPLatestResult()
        {
            //ResultService ResultSer = new ResultService();

            SampleResultInfo smpr = MyBatis.GetSampleResultInfo();
            if (smpr != null )
            {
                //LogService.Log("Lis Send:|" + "smpr != null && HasResultFlag(SMP) == true", LogType.Debug, "lisRTSend.lg");

                ResultData rd = new ResultData();
                rd.SampleInfo = MyBatis.GetSample(smpr.SampleNum, DateTime.Now);
                rd.PatientInfo = MyBatis.AccordingSampNumGetPatientInfo(smpr.SampleNum, DateTime.Now);
                rd.Results = new List<SampleResultInfo>();
                rd.Results.Add(smpr);
                AddData(rd);
                return;
            }
        }

        /// <summary>
        /// 测试的样本结果发送
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        string ResultDataHL7Encode(LISData Data)
        {
            PatientInfo P = null;
            SampleInfo S = null;
            //SDTItem SDT = null;
            //QCItem QC = null;
            List<SampleResultInfo> Results = null;

            MSH MSH = new MSH();
            MSH.Fields[0] = @"|";
            MSH.Fields[1] = @"^~\&";
            MSH.Fields[2] = @"NAD";
            MSH.Fields[3] = MachineInfo.Type;
            MSH.Fields[4] = @"";
            MSH.Fields[5] = @"";
            MSH.Fields[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MSH.Fields[7] = @"";
            MSH.Fields[8] = @"ORU^R01";
            MSH.Fields[9] = this.LISMSGID.ToString();// 消息控制ID
            MSH.Fields[10] = @"P";
            MSH.Fields[11] = @"2.3.1";
            MSH.Fields[12] = @"";
            MSH.Fields[13] = @"";
            MSH.Fields[14] = @"";
            if ((Data as ResultData) != null)
            {
                MSH.Fields[15] = @"0";
                P = (Data as ResultData).PatientInfo;
                S = (Data as ResultData).SampleInfo;
                Results = (Data as ResultData).Results;
            }
            //if ((Data as SDTResultData) != null)
            //{
            //    MSH.Fields[15] = @"1";
            //    SDT = (Data as SDTResultData).SDTItem;
            //    Results = (Data as SDTResultData).Results;
            //}
            //if ((Data as QCResultData) != null)
            //{
            //    MSH.Fields[15] = @"2";
            //    QC = (Data as QCResultData).QCItem;
            //    Results = (Data as QCResultData).Results;
            //}
            MSH.Fields[16] = @"";
            MSH.Fields[17] = @"ASCII";
            MSH.Fields[18] = @"";
            MSH.Fields[19] = @"";

            PID PID = new PID();
            if (P != null)
            {
                PID.Fields[0] = @"1";
                PID.Fields[1] = P.SampleID;
                PID.Fields[2] = P.MedicalRecordNum;//PID.Fields[2] = P.MedicalRecordNum; 预留病历号
                PID.Fields[3] = P.BedNum;
                PID.Fields[4] = P.PatientName;
                PID.Fields[5] = @"";
                PID.Fields[6] = P.Age.ToString();
                PID.Fields[7] = P.Sex;
                PID.Fields[8] = P.AuditDoctor;//PID.Fields[8] = P.Operator; 预留操作人姓名
                PID.Fields[9] = @"";
                PID.Fields[10] = @"";
                PID.Fields[11] = @"";
                PID.Fields[12] = @"";
                PID.Fields[13] = @"";
                PID.Fields[14] = @"";
                PID.Fields[15] = @"";
                PID.Fields[16] = @"";
                PID.Fields[17] = @"";
                PID.Fields[18] = @"";
                PID.Fields[19] = @"";
                PID.Fields[20] = @"";
                PID.Fields[21] = @"";
                PID.Fields[22] = @"";
                PID.Fields[23] = @"";
                PID.Fields[24] = @"";
                PID.Fields[25] = @"";
                PID.Fields[26] = @"";
                PID.Fields[27] = @"";
                PID.Fields[28] = @"";
                PID.Fields[29] = @"";
            }

            OBR OBR = new OBR();
            if (S != null)
            {
                OBR.Fields[0] = @"1";
                OBR.Fields[1] = S.Barcode;
                OBR.Fields[2] = S.SampleNum.ToString();
                OBR.Fields[3] = @"NAD^NT";
                if (S.IsEmergency == true)
                {
                    OBR.Fields[4] = @"Y";
                }
                else
                {
                    OBR.Fields[4] = @"N";
                }

                OBR.Fields[5] = S.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                OBR.Fields[6] = @"";
                OBR.Fields[7] = @"";
                OBR.Fields[8] = @"";
                OBR.Fields[9] = @"";
                OBR.Fields[10] = @"";
                OBR.Fields[11] = @"";
                OBR.Fields[12] = @"";
                OBR.Fields[13] = @"";
                OBR.Fields[14] = S.SampleType;
                OBR.Fields[15] = @"";
                OBR.Fields[16] = @"";
                OBR.Fields[17] = @"";
                OBR.Fields[18] = @"";
                OBR.Fields[19] = @"";
                OBR.Fields[20] = @"";
                OBR.Fields[21] = @"";
                OBR.Fields[22] = @"";
                OBR.Fields[23] = @"";
                OBR.Fields[24] = @"";
                OBR.Fields[25] = @"";
                OBR.Fields[26] = @"";
                OBR.Fields[27] = @"";
                OBR.Fields[28] = @"";
                OBR.Fields[29] = @"";
                OBR.Fields[30] = @"";
                OBR.Fields[31] = @"";
                OBR.Fields[32] = @"";
                OBR.Fields[33] = @"";
                OBR.Fields[34] = @"";
                OBR.Fields[35] = @"";
                OBR.Fields[36] = @"";
                OBR.Fields[37] = @"";
                OBR.Fields[38] = @"";
                OBR.Fields[39] = @"";
                OBR.Fields[40] = @"";
                OBR.Fields[41] = @"";
                OBR.Fields[42] = @"";
                OBR.Fields[43] = @"";
                OBR.Fields[44] = @"";
                OBR.Fields[45] = @"";
                OBR.Fields[46] = @"";
            }
            //if (SDT != null)
            //{
            //    OBR.Fields[0] = @"1";
            //    OBR.Fields[1] = @"";
            //    OBR.Fields[2] = SDT.Name;
            //    OBR.Fields[3] = @"NAD^NT";
            //    OBR.Fields[4] = @"";
            //    OBR.Fields[5] = @"";
            //    OBR.Fields[6] = @"";
            //    OBR.Fields[7] = @"";
            //    OBR.Fields[8] = @"";
            //    OBR.Fields[9] = @"";
            //    OBR.Fields[10] = @"";
            //    OBR.Fields[11] = @"";
            //    OBR.Fields[12] = @"";
            //    OBR.Fields[13] = @"";
            //    OBR.Fields[14] = @"";
            //    OBR.Fields[15] = @"";
            //    OBR.Fields[16] = @"";
            //    OBR.Fields[17] = @"";
            //    OBR.Fields[18] = @"";
            //    OBR.Fields[19] = @"";
            //    OBR.Fields[20] = @"";
            //    OBR.Fields[21] = @"";
            //    OBR.Fields[22] = @"";
            //    OBR.Fields[23] = @"";
            //    OBR.Fields[24] = @"";
            //    OBR.Fields[25] = @"";
            //    OBR.Fields[26] = @"";
            //    OBR.Fields[27] = @"";
            //    OBR.Fields[28] = @"";
            //    OBR.Fields[29] = @"";
            //    OBR.Fields[30] = @"";
            //    OBR.Fields[31] = @"";
            //    OBR.Fields[32] = @"";
            //    OBR.Fields[33] = @"";
            //    OBR.Fields[34] = @"";
            //    OBR.Fields[35] = @"";
            //    OBR.Fields[36] = @"";
            //    OBR.Fields[37] = @"";
            //    OBR.Fields[38] = @"";
            //    OBR.Fields[39] = @"";
            //    OBR.Fields[40] = @"";
            //    OBR.Fields[41] = @"";
            //    OBR.Fields[42] = @"";
            //    OBR.Fields[43] = @"";
            //    OBR.Fields[44] = @"";
            //    OBR.Fields[45] = @"";
            //    OBR.Fields[46] = @"";
            //}
            //if (QC != null)
            //{
            //    OBR.Fields[0] = @"1";
            //    OBR.Fields[1] = @"";
            //    OBR.Fields[2] = QC.Name;
            //    OBR.Fields[3] = @"NAD^NT";
            //    OBR.Fields[4] = @"";
            //    OBR.Fields[5] = @"";
            //    OBR.Fields[6] = @"";
            //    OBR.Fields[7] = @"";
            //    OBR.Fields[8] = @"";
            //    OBR.Fields[9] = @"";
            //    OBR.Fields[10] = @"";
            //    OBR.Fields[11] = @"";
            //    OBR.Fields[12] = @"";
            //    OBR.Fields[13] = @"";
            //    OBR.Fields[14] = @"";
            //    OBR.Fields[15] = @"";
            //    OBR.Fields[16] = @"";
            //    OBR.Fields[17] = @"";
            //    OBR.Fields[18] = @"";
            //    OBR.Fields[19] = @"";
            //    OBR.Fields[20] = @"";
            //    OBR.Fields[21] = @"";
            //    OBR.Fields[22] = @"";
            //    OBR.Fields[23] = @"";
            //    OBR.Fields[24] = @"";
            //    OBR.Fields[25] = @"";
            //    OBR.Fields[26] = @"";
            //    OBR.Fields[27] = @"";
            //    OBR.Fields[28] = @"";
            //    OBR.Fields[29] = @"";
            //    OBR.Fields[30] = @"";
            //    OBR.Fields[31] = @"";
            //    OBR.Fields[32] = @"";
            //    OBR.Fields[33] = @"";
            //    OBR.Fields[34] = @"";
            //    OBR.Fields[35] = @"";
            //    OBR.Fields[36] = @"";
            //    OBR.Fields[37] = @"";
            //    OBR.Fields[38] = @"";
            //    OBR.Fields[39] = @"";
            //    OBR.Fields[40] = @"";
            //    OBR.Fields[41] = @"";
            //    OBR.Fields[42] = @"";
            //    OBR.Fields[43] = @"";
            //    OBR.Fields[44] = @"";
            //    OBR.Fields[45] = @"";
            //    OBR.Fields[46] = @"";
            //}


            List<OBX> OBXs = new List<OBX>();
            for (int i = 0; i < Results.Count; i++)
            {
                SampleResultInfo e = Results[i];
                OBX OBX = new OBX();

                OBX.Fields[0] = (i + 1).ToString();
                OBX.Fields[1] = @"NM";
                OBX.Fields[2] = @"";
                OBX.Fields[3] = e.ProjectName;
                OBX.Fields[4] = e.ConcResult.ToString();

                ResultSetInfo rset = null;
                if (S != null)
                {
                    rset = MyBatis.GetResultSetInfo(e.ProjectName, S.SampleType) as ResultSetInfo;
                }
                //if (QC != null)
                //{
                //    rset = new ResultSetService().Get(e.ItemName, QC.QCSMPType) as ResultSet;
                //}
                if (rset != null)
                {
                    OBX.Fields[5] = rset.Unit;
                }
                else
                {
                    OBX.Fields[5] = @"";
                }
                if (S != null && P != null)
                {
                    AssayProjectRangeParamInfo range = null;
                    range = MyBatis.GetRangeParamInfo(e.ProjectName, e.SampleType);
                    if (range != null && range.AgeLow1 != -100000000)
                    {
                        if (P.Age >= range.AgeLow1 && P.Age <= range.AgeHigh1)
                        {
                            if(P.Sex == "男")
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.ManConsLow1, range.ManConsHigh1);
                            else
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.WomanConsLow1, range.WomanConsHigh1);
                        }
                        else if (range.AgeLow2 != 100000000 && P.Age >= range.AgeLow2 && P.Age <= range.AgeHigh2)
                        {
                            if (P.Sex == "男")
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.ManConsLow2, range.ManConsHigh2);
                            else
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.WomanConsLow2, range.WomanConsHigh2);
                        }
                        else if (range.AgeLow3 != 100000000 && P.Age >= range.AgeLow3 && P.Age <= range.AgeHigh3)
                        {
                            if (P.Sex == "男")
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.ManConsLow3, range.ManConsHigh3);
                            else
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.WomanConsLow3, range.WomanConsHigh3);
                        }
                        else if (range.AgeLow4 != 100000000 && P.Age >= range.AgeLow4 && P.Age <= range.AgeHigh4)
                        {
                            if (P.Sex == "男")
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.ManConsLow4, range.ManConsHigh4);
                            else
                                OBX.Fields[6] = string.Format(@"{0}--{1}", range.WomanConsLow4, range.WomanConsHigh4);
                        }
                        //OBX.Fields[6] = string.Format(@"{0}--{1}", range.ValueMin, range.ValueMax);
                        if (string.IsNullOrEmpty(e.ConcResult.ToString()) || string.IsNullOrWhiteSpace(e.ConcResult.ToString()))
                        {
                            OBX.Fields[7] = @"";
                        }
                        else
                        {
                            try
                            {
                                if (e.ConcResult > float.Parse(OBX.Fields[6].Split('-')[2]))
                                {
                                    OBX.Fields[7] = @"H";
                                }
                                else if (e.ConcResult < float.Parse(OBX.Fields[6].Split('-')[0]))
                                {
                                    OBX.Fields[7] = @"L";
                                }
                            }
                            catch
                            {
                                OBX.Fields[7] = @"";
                            }
                        }
                    }
                    else
                    {
                        OBX.Fields[7] = @"";
                    }
                }
                else
                {
                    OBX.Fields[7] = @"";
                }
                OBX.Fields[8] = @"";
                OBX.Fields[9] = @"";
                OBX.Fields[10] = @"";
                OBX.Fields[11] = @"";
                OBX.Fields[12] = @"";
                OBX.Fields[13] = e.SampleCreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                OBX.Fields[14] = @"";
                OBX.Fields[15] = @"";
                OBX.Fields[16] = @"";

                OBXs.Add(OBX);
            }
            string Str = MSH.GetString() + (char)0x0D + PID.GetString() + (char)0x0D + OBR.GetString() + (char)0x0D;
            string OBXStr = "";
            foreach (OBX e in OBXs)
            {
                string str1 = e.GetString() + (char)0x0D;
                OBXStr += str1;
            }
            //OBXStr = OBXStr.TrimEnd((char)0x0D);

            return (char)0x0B + Str + OBXStr + (char)0x1C + (char)0x0D;
        }
        /// <summary>
        /// 将数据编码成HL7
        /// </summary>
        /// <param name="SMPBarcode"></param>
        /// <returns></returns>
        string QRYQ02HL7Encode(string SMPBarcode)
        {
            MSH MSH = new MSH();
            MSH.Fields[0] = @"|";
            MSH.Fields[1] = @"^~\&";
            MSH.Fields[2] = @"NAD";
            MSH.Fields[3] = MachineInfo.Type;
            MSH.Fields[4] = @"";
            MSH.Fields[5] = @"";
            MSH.Fields[6] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MSH.Fields[7] = @"";
            MSH.Fields[8] = @"QRY^Q02";
            MSH.Fields[9] = this.LISMSGID.ToString();// 消息控制ID
            MSH.Fields[10] = @"P";
            MSH.Fields[11] = @"2.3.1";
            MSH.Fields[12] = @"";
            MSH.Fields[13] = @"";
            MSH.Fields[14] = @"";
            MSH.Fields[15] = @"";
            MSH.Fields[16] = @"";
            MSH.Fields[17] = @"ASCII";
            MSH.Fields[18] = @"";
            MSH.Fields[19] = @"";

            QRD QRD = new QRD();
            QRD.Fields[0] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            QRD.Fields[1] = @"R";
            QRD.Fields[2] = @"D";
            QRD.Fields[3] = @"";
            QRD.Fields[4] = @"";
            QRD.Fields[5] = @"";
            QRD.Fields[6] = @"RD";
            QRD.Fields[7] = SMPBarcode;
            QRD.Fields[8] = @"OTH";
            QRD.Fields[9] = @"";
            QRD.Fields[10] = @"";
            QRD.Fields[11] = @"T";

            QRF QRF = new QRF();
            QRF.Fields[0] = MachineInfo.Type;
            QRF.Fields[1] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            QRF.Fields[2] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            QRF.Fields[3] = @"";
            QRF.Fields[4] = @"";
            QRF.Fields[5] = @"RCT";
            QRF.Fields[6] = @"COR";
            QRF.Fields[7] = @"ALL";
            QRF.Fields[8] = @"";

            string Str = MSH.GetString() + (char)CR + QRD.GetString() + (char)CR + QRF.GetString() + (char)CR;

            return (char)SB + Str + (char)EB + (char)CR;
        }

        public event LISServiceHandler ConnectSuccessEvent;
        /// <summary>
        /// 连接成功，改变连通状态
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnConnectSuccess(object sender)
        {
            if (this.ConnectSuccessEvent != null)
            {
                this.IsConnecting = true;

                this.ConnectSuccessEvent(sender);
            }
        }
        /// <summary>
        /// 样本条码请求成功事件
        /// </summary>
        public event LISServiceHandler ApplySampleSuccessEvent;
        protected virtual void OnApplySampleSuccessEvent(object sender)
        {
            if (this.ApplySampleSuccessEvent != null)
            {
                this.ApplySampleSuccessEvent(sender);
            }
        }
        /// <summary>
        /// 扫码枪异常提示事件
        /// </summary>
        public event LISServiceHandler SMPCodeBarQueryEvent;
        protected virtual void OnSMPCodeBarQueryEvent(object sender)
        {
            if (this.SMPCodeBarQueryEvent != null)
            {
                this.SMPCodeBarQueryEvent(sender);
            }
        }
        /// <summary>
        /// 没有数据发送
        /// </summary>
        public event LISServiceHandler NotHasLisDataEvent;
        protected virtual void OnNotHasLisDataEvent(object sender)
        {
            if (this.NotHasLisDataEvent != null)
            {
                this.NotHasLisDataEvent(sender);
            }
        }

        public event LISServiceHandler SendLisResultDataRunningEvent;
        /// <summary>
        /// 正在发送数据
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSendLisResultDataRunning(object sender)
        {
            if (this.SendLisResultDataRunningEvent != null)
            {
                this.SendLisResultDataRunningEvent(sender);
            }
        }

        public event LISServiceHandler SendLisResultDataOKEvent;
        /// <summary>
        /// 结果数据发送成功
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSendLisResultDataOK(object sender)
        {
            if (this.SendLisResultDataOKEvent != null)
            {
                this.SendLisResultDataOKEvent(sender);
            }
        }

        public event LISServiceHandler SendLisResultDataFailedEvent;
        /// <summary>
        /// 发送结果超时
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSendLisResultDataFailed(object sender)
        {
            if (this.SendLisResultDataFailedEvent != null)
            {
                this.SendLisResultDataFailedEvent(sender);
            }
        }
    }

    public abstract class LISData
    {
        public List<SampleResultInfo> Results { get; set; }
    }
    public class ResultData : LISData
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        public PatientInfo PatientInfo { get; set; }
        /// <summary>
        /// 样本信息
        /// </summary>
        public SampleInfo SampleInfo { get; set; }
    }
}
