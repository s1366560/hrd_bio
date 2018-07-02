using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using BioA.Common.IO;

namespace BioA.Common.Communication
{
    public class SERVER : IDisposable
    {
        //接受队列
        Queue<string> RecvStringQueue = new Queue<string>();
        //监听状态
        bool _isExit = false;
        public bool IsExit
        {
            get { return _isExit; }
            set { _isExit = value; }
        }
        //监听器
        TcpListener _Listener;
        //同步信号
        ManualResetEvent _AllDone = new ManualResetEvent(false);

        System.Collections.ArrayList clientlist = new System.Collections.ArrayList();

     
        string _HostIP;
        public string HostIP
        {
            get { return _HostIP; }
        }
        string _PortNo;
        public string PortNo
        {
            get { return _PortNo; }
        }
        public void SetupService()
        {
            try
            {
                string file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"DataConfigure.xml";
                XmlNode tcpServerNode = XMLHelper.GetNode(file, "PLCServer");

                this._HostIP = XMLHelper.Read(tcpServerNode,"IP");
                this._PortNo = XMLHelper.Read(tcpServerNode, "Port");
            
                this.DataArriveEvent += new SERVERHandler(OnDataArriveEvent);

                new Thread(new ThreadStart(AcceptConnection)).Start();
                new Thread(new ThreadStart(ArrivedDataService)).Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]:" + e.Message);
            }
        }
        public void CloseService()
        {
            for (int i = 0; i < clientlist.Count;i++ )
            {
                DataReadWrite e = clientlist[i] as DataReadWrite;

                SendString(e, "I am closing");

                clientlist.RemoveAt(i);
            }

            _isExit = true;

            _AllDone.Set();

        }
        private void AcceptConnection()
        {
            IPAddress IP = IPAddress.Parse(this.HostIP);
            this._Listener = new TcpListener(IP, int.Parse(this.PortNo));
            try
            {
                this._Listener.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]:" + this.HostIP + ":" + this.PortNo + e.Message);
                return;
            }
            Console.WriteLine("SERVER:"+this.HostIP + ":" + this.PortNo);
            while (this._isExit == false)
            {
                try
                {
                    this._AllDone.Reset();
                    AsyncCallback callback = new AsyncCallback(AcceptTcpClientCallBack);
                    this._Listener.BeginAcceptTcpClient(callback, this._Listener);
                    this._AllDone.WaitOne();
                }
                catch (Exception e)
                {
                    Console.WriteLine("[NET SERVER]:" + e.Message);
                    break;
                }
            }
            this._Listener.Stop();
        }
        private void AcceptTcpClientCallBack(IAsyncResult iar)
        {
            try
            {
                this._AllDone.Set();
                TcpListener mylistener = (TcpListener)iar.AsyncState;
                TcpClient client = mylistener.EndAcceptTcpClient(iar);

                Console.WriteLine("connecting：" + client.Client.RemoteEndPoint);

                DataReadWrite datareadwrite = new DataReadWrite(client);
                clientlist.Add(datareadwrite);

                SendString(datareadwrite, "CONNECTING");
                datareadwrite.ns.BeginRead(datareadwrite.read, 0, datareadwrite.read.Length, ReadCallBack, datareadwrite);
                if (AnalyeConnectedEvent != null)
                {
                    AnalyeConnectedEvent(null);
                }
                else
                {
                    Console.WriteLine("AnalyeConnectedEvent: is null!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("网络服务器:" + e.Message);
                return;
            }
        }
        private void ReadCallBack(IAsyncResult iar)
        {
            DataReadWrite datareadwrite = null;
            try
            {
                datareadwrite = (DataReadWrite)iar.AsyncState;
                int recv = datareadwrite.ns.EndRead(iar);

                DataArriveEvent(Encoding.UTF8.GetString(datareadwrite.read, 0, recv));


                if (this._isExit == false)
                {
                    datareadwrite.InitReadArray();
                    datareadwrite.ns.BeginRead(datareadwrite.read, 0, datareadwrite.read.Length, ReadCallBack, datareadwrite);
                }
            }
            catch (Exception e)
            {
                clientlist.Remove(datareadwrite);
                Console.WriteLine("[NET SERVER]:" + e.Message + "disconnecting：" + datareadwrite.client.Client.RemoteEndPoint);
            }
        }
        //发送
        private void SendString(DataReadWrite datareadwrite, string str)
        {
            try
            {
                datareadwrite.write = Encoding.UTF8.GetBytes(str + "\r\n");
                datareadwrite.ns.BeginWrite(datareadwrite.write, 0, datareadwrite.write.Length, new AsyncCallback(SendCallBack), datareadwrite);
                datareadwrite.ns.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]:" + e.Message);
            }
        }
        private void SendCallBack(IAsyncResult iar)
        {
            DataReadWrite datareadwrite = (DataReadWrite)iar.AsyncState;
            try
            {
                datareadwrite.ns.EndWrite(iar);
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]:" + e.Message);
            }
        }
        public void SendCMD(string cmd)
        {
            try
            {
                foreach (DataReadWrite datareadwrite in clientlist)
                {
                    SendString(datareadwrite, cmd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]:" + e.Message);
            }
        }
        
        public void Dispose()
        {
            Console.WriteLine("正在释放资源");

            this._isExit = true;
        }

        ManualResetEvent DataArrivedSignal = new ManualResetEvent(false);
        void OnDataArriveEvent(object sender)
        {
            string str = sender as string;

            if (!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str))
            {
                str = str.TrimEnd('\r', '\n');
                lock (RecvStringQueue)
                {
                    RecvStringQueue.Enqueue(str);
                }
            }
            if (RecvStringQueue.Count > 0)
            {
                DataArrivedSignal.Set();
            }
        }
        void ArrivedDataService()
        {
            while (true)
            {
                DataArrivedSignal.WaitOne();
                if (RecvStringQueue.Count > 0)
                {
                    string str = null;
                    lock (RecvStringQueue)
                    {
                        str = RecvStringQueue.Dequeue();
                    }
                    if (!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str))
                    {
                        if (AnalyeEvent != null)
                        {
                            AnalyeEvent(str);
                        }
                    }
                }
                else
                {
                    DataArrivedSignal.Reset();
                }
            }
        }

        public delegate void SERVERHandler(object sender);
        event SERVERHandler DataArriveEvent;

        //public event SERVERHandler GeneratedErrorEvent;
        public event SERVERHandler AnalyeEvent;
        public event SERVERHandler AnalyeConnectedEvent;
    }
}
