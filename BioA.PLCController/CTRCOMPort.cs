using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Xml;
using System.Threading;
using System.Globalization;
using BioA.Common.IO;
using BioA.Common;

namespace BioA.PLCController
{
    public enum ComLogType
    {
        SEND,
        RECEIVE
    }
    public class CTRCOMPort
    {

        ManualResetEvent COMDataArriveSignal = new ManualResetEvent(false);

        SerialPort SP = new SerialPort();
        public CTRCOMPort()
        {
        }

        static XmlNode _MachineNode;
        public static XmlNode MachineNode
        {
            get
            {
                if (_MachineNode == null)
                {
                    _MachineNode = XMLHelper.GetNode(MachineFile, "Machine");
                }
                return _MachineNode;
            }
        }

        static string _MachineFile;
        public static string MachineFile
        {
            get
            {
                if (string.IsNullOrEmpty(_MachineFile) || string.IsNullOrWhiteSpace(_MachineFile))
                {
                    CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;
                    switch (currentCultureInfo.Name)
                    {
                        case "zh-CN":
                            _MachineFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Machine.xml";
                            break;
                        default:
                            _MachineFile = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"Machine-en.xml"; ;
                            break;
                    }
                }

                return _MachineFile;
            }
        }

        string _PortName;
        public string PortName
        {
            get { return _PortName; }
        }
        public void SetupService(byte begin, byte end, XmlNode SerialPortNode)
        {
            this.InitPort(SerialPortNode);

            this.SetRawDataBeginEndFlag(begin,end);

            this.SetupRecviceService();
        }
        void InitPort(XmlNode SerialPortNode)
        {
            this._PortName = XMLHelper.Read(SerialPortNode, "Port");

            if (this.SP.IsOpen == false)
            {
                try
                {
                    this.SP.PortName = this._PortName;     //串口名称
                    this.SP.BaudRate = 19200;              //串口波特率
                    this.SP.DataBits = 8;                  //串口数据位
                    this.SP.Parity = Parity.None;          //是否校验
                    this.SP.StopBits = StopBits.One;       //停止位
                    this.SP.ReadBufferSize = 2048;         //读缓存
                    this.SP.Open();                        //打开串口
                    this.SP.WriteTimeout = 1000;           //写延迟1秒
                    this.SP.ReadTimeout = 1000;            //读延迟1秒
                    this.SP.ReceivedBytesThreshold = 1;

                    Console.WriteLine(this.PortName + " is open successfully!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(this.PortName + "is opening abnormally! exception:" + e.Message);
                }
            }
            else
            {
                Console.WriteLine(this.PortName + "has been open!");
            }
        }
        byte _Begin;
        byte _End;
        void SetRawDataBeginEndFlag(byte begin,byte end)
        {
            this._Begin = begin;
            this._End = end;
        }

        void SetupRecviceService()
        {
            if (this.SP.IsOpen == false)
            {
                Console.WriteLine(this.PortName + "open error!,can not set up service");
            }
            else
            {
                this.SP.DataReceived += new SerialDataReceivedEventHandler(OnSPDataReceived);     //生产者线程
                Thread RawDataServiceThread = new Thread(new ThreadStart(SetRawDataService));     //消费者线程
                RawDataServiceThread.Priority = ThreadPriority.Highest;
                RawDataServiceThread.Start();
            }
        }

        List<byte> fragment = new List<byte>();
        Queue<byte[]> COMBuffQueue = new Queue<byte[]>();     //队列
        void SetRawDataService()
        {
            while (true)
            {
                COMDataArriveSignal.WaitOne();
                if (COMBuffQueue.Count > 0)
                {
                    byte[] buf = null;
                    lock (COMBuffQueue)
                    {
                        buf = COMBuffQueue.Dequeue();
                    }

                    foreach (byte ch in buf)
                    {
                        if (ch == this._Begin)
                        {
                            fragment.Clear();
                            fragment.Add(ch);
                        }
                        else
                        {
                            fragment.Add(ch);
                        }

                        if (fragment.Count > 2 && fragment[0] == this._Begin && fragment[fragment.Count - 3] == this._End)
                        {
                            LogInfo.Log(this.ByteArrayToString(this.ConvertListbyteArray(fragment)), LogInfo.ComLogType.RECEIVE);
                            ToAnalyzeEvent(fragment);
                            fragment.Clear();
                        }
                    }
                }
                else
                {
                    COMDataArriveSignal.Reset();
                }
            }
        }

        void OnSPDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] readBuffer = new byte[this.SP.ReadBufferSize + 1];
                int count = this.SP.Read(readBuffer, 0, this.SP.ReadBufferSize);
                this.SendTimeoutFlag = false;

                byte[] GetData = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    GetData[i] = readBuffer[i];
                }

                string a = "";
                foreach (byte b in GetData)
                {
                    a += " " + b.ToString(); 
                }
                Console.WriteLine("receive:" + a + Environment.NewLine);

                if (GetData.Length > 0)
                {
                    lock (COMBuffQueue)
                    {
                        COMBuffQueue.Enqueue(GetData);
                    }
                }
                if (COMBuffQueue.Count > 0)
                {
                    COMDataArriveSignal.Set();
                }
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
         
        byte[] ConvertListbyteArray(List<byte> fragment)
        {
            byte[] array = new byte[fragment.Count];
            for (int i = 0; i < fragment.Count;i++ )
            {
                array[i] = fragment[i];
            }
            return array;
        }
        string ByteArrayToString(byte[] Data)
        {
            if (Data.Count() == 0)
            {
                return null;
            }

            string str = "";
            foreach (byte e in Data)
            {
                string s = string.Format("{0,2:X} ", e);
                if (s.Length < 2)
                {
                    s = s.PadLeft(s.Length, '0');
                }
                str += s;
            }
            return str;
        }
        //发送字符串数据
        private void SendStringData(string txt)
        {
            if (this.SP.IsOpen == false)
            {
                Console.WriteLine("Com port is not by opend,can't send the data!");
            }
            else
            {
                this.SP.Write(txt);
            }
        }
        //发送二进制数据
        bool SendTimeoutFlag = false;
        AutoResetEvent a = new AutoResetEvent(false);
        ManualResetEvent SendTimeoutFlagSignal = new ManualResetEvent(false);
        public void SendBytesData(byte[] bytes)
        {
            if (bytes == null)
            {
                return;
            }

            try
            {
                this.SP.DiscardOutBuffer();
                this.SP.Write(bytes, 0, bytes.Length);

                string a = "";
                foreach (byte b in bytes)
                {
                    a += " " + b.ToString();
                }
                Console.WriteLine("send:" + a + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + this.SP.WriteTimeout);
            }

            if (bytes[0] != 0x06)//检测发送溢出
            {
                this.SendTimeoutFlag = true;
                new Thread(new ParameterizedThreadStart(CheckSendTimeoutFlag)).Start(bytes);
            }
            LogInfo.Log(this.ByteArrayToString(bytes), LogInfo.ComLogType.SEND);
        }
        void CheckSendTimeoutFlag(object b)
        {
            byte[] bytes = b as byte[];
            SendTimeoutFlagSignal.WaitOne(3000);
            if (this.SendTimeoutFlag == true)
            {
                SendTimeoutEvent(bytes);
            }
        }


        public delegate void CTRCOMPortHandler(object sender);

        public event CTRCOMPortHandler ToAnalyzeEvent;
        public event CTRCOMPortHandler SendTimeoutEvent;
    }
}
