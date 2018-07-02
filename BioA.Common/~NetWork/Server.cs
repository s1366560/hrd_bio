// ================================================================================================
//
// 文件名（File Name）：              Server.cs
//
// 功能描述（Description）：          系统模块Socket通讯服务器端
//
// 数据表（Tables）：                 无
//
// 作者（Author）：                   冯旗
//
// 日期（Create Date）：              2017-7-17
//
// 修改记录（Revision History）：
//      R1:
//          修改人：
//          修改日期：
//          修改理由：
//
// ================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BioA.Common.NetWork
{
    public class Server
    {
        // 接收队列
        Queue<string> RecStringQueue = new Queue<string>();
        // 监听状态
        bool _isExit = false;
        // 监听器
        TcpListener _Listener;
        // 同步信号
        ManualResetEvent _AllDone = new ManualResetEvent(false);
        ArrayList clientList = new ArrayList();
        // 主机IP
        string _HostIP;
        // 主机端口号
        string _PortNo;

        public delegate void SERVERHandler(object sender);
        // event SERVERHandler DataArriveEvent;
        public event SERVERHandler AnalyeEvent;
        public event SERVERHandler AnalyeConnectedEvent;
        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        public void SetupService(string strPort)
        {
            try
            {
                this._HostIP = NetWorkCommon.LocalIPAddress();
                this._PortNo = strPort;

                //this.DataArriveEvent += new SERVERHandler(OnDataArriveEvent);

                new Thread(new ThreadStart(AcceptConnection)).Start();
                new Thread(new ThreadStart(ArrivedDataService)).Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]: " + e.Message);
            }
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        public void CloseService()
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                DataReadWrite e = clientList[i] as DataReadWrite;
                SendString(e, "I am closing");

                clientList.RemoveAt(i);
            }

            _isExit = true;
            _AllDone.Set();
        }
        /// <summary>
        /// 接收连接服务器
        /// </summary>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        private void AcceptConnection()
        {
            IPAddress IP = IPAddress.Parse(this._HostIP);
            // 初始化监听器
            this._Listener = new TcpListener(IP, int.Parse(this._PortNo));
            try
            {
                this._Listener.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]: " + this._HostIP + ": " + this._PortNo + e.Message);
                return;
            }
            Console.WriteLine("SERVER: " + this._HostIP + ": " + this._PortNo);

            while (this._isExit == false)
            {
                try
                {
                    this._AllDone.Reset();
                    // 通信回调函数
                    AsyncCallback callback = new AsyncCallback(AcceptTcpClientCallBack);
                    // 接收客户端连接
                    this._Listener.BeginAcceptTcpClient(callback, this._Listener);
                    this._AllDone.WaitOne();
                }
                catch (Exception e)
                {
                    Console.WriteLine("[NET SERVER]: " + e.Message);
                    break;
                }
            }

            this._Listener.Stop();
        }
        /// <summary>
        /// 接收客户端连接回调函数
        /// </summary>
        /// <param name="iar"></param>
        private void AcceptTcpClientCallBack(IAsyncResult iar)
        {
            try
            {
                this._AllDone.Set();
                // 接收客户端连接返回信息
                TcpListener mylistener = (TcpListener)iar.AsyncState;
                TcpClient client = mylistener.EndAcceptTcpClient(iar);

                Console.WriteLine("connecting: " + client.Client.RemoteEndPoint);

                DataReadWrite dataReadWrite = new DataReadWrite(client);
                clientList.Add(dataReadWrite);

                SendString(dataReadWrite, "CONNECTING");
                // 接收客户端函数
                dataReadWrite.ns.BeginRead(dataReadWrite.read, 0, dataReadWrite.read.Length, ReadCallBack, dataReadWrite);
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
                Console.WriteLine("网络服务器： " + e.Message);
                return;
            }
        }
        /// <summary>
        /// 客户端信息接收回调函数
        /// </summary>
        /// <param name="iar"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        private void ReadCallBack(IAsyncResult iar)
        {
            DataReadWrite dataReadWrite = null;
            try
            {
                dataReadWrite = (DataReadWrite)iar.AsyncState;
                // 结束读取
                int recv = dataReadWrite.ns.EndRead(iar);

                OnDataArrive(Encoding.UTF8.GetString(dataReadWrite.read, 0, recv));
                //OnDataArrive(Encoding.UTF8.GetString(dataReadWrite.read, 0, recv));

                if (this._isExit == false)
                {
                    dataReadWrite.InitReadArray();
                    dataReadWrite.ns.BeginRead(dataReadWrite.read, 0, dataReadWrite.read.Length, ReadCallBack, dataReadWrite);
                }
            }
            catch (Exception e)
            {
                clientList.Remove(dataReadWrite);
                Console.WriteLine("[NET SERVER]: " + e.Message + "disconnecting: " + dataReadWrite.client.Client.RemoteEndPoint);
            }
        }
        /// <summary>
        /// 发送消息向客户端
        /// </summary>
        /// <param name="dataReadWrite">发送信息对象</param>
        /// <param name="str">待发消息</param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        private void SendString(DataReadWrite dataReadWrite, string str)
        {
            try
            {
                dataReadWrite.write = Encoding.UTF8.GetBytes(str + "\r\n");
                dataReadWrite.ns.BeginWrite(dataReadWrite.write, 0, dataReadWrite.write.Length, new AsyncCallback(SendCallBack), dataReadWrite);
                dataReadWrite.ns.Flush();
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]: " + e.Message);
            }
        }

        public void SendBytes(string strSendACK)
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                DataReadWrite e = clientList[i] as DataReadWrite;
                
                try
                {
                    e.write = System.Text.Encoding.UTF8.GetBytes(strSendACK);
                    e.ns.BeginWrite(e.write, 0, e.write.Length, new AsyncCallback(SendCallBack), e);
                    e.ns.Flush();
                }
                catch (Exception ee)
                {
                    Console.WriteLine("[NET SERVER]: " + ee.Message);
                }
            }
        }
        /// <summary>
        /// 数据发送成功回调函数
        /// </summary>
        /// <param name="iar"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        private void SendCallBack(IAsyncResult iar)
        {
            DataReadWrite dataReadWrite = (DataReadWrite)iar.AsyncState;
            try
            {
                dataReadWrite.ns.EndWrite(iar);
            }
            catch (Exception e)
            {
                Console.WriteLine("[NET SERVER]: " + e.Message);
            }
        }

        public void Dispose()
        {
            Console.WriteLine("正在释放资源");
            this._isExit = true;
        }
        // 数据接收信号
        ManualResetEvent dataArrivedSignal = new ManualResetEvent(false);
        /// <summary>
        /// 存储接收信息
        /// </summary>
        /// <param name="sender"></param>
        private void OnDataArrive(object sender)
        {
            
            string bReasult = sender as string;
            if (bReasult.Length > 0)
            {
                lock (RecStringQueue)
                {
                    RecStringQueue.Enqueue(bReasult);
                }
            }
            if (RecStringQueue.Count > 0)
            {
                dataArrivedSignal.Set();
            }
        }
        /// <summary>
        /// 处理客户端信息
        /// </summary>
        private void ArrivedDataService()
        {
            while (true)
            {
                dataArrivedSignal.WaitOne();
                if (RecStringQueue.Count > 0)
                {
                    string bReasult = null;
                    lock (RecStringQueue)
                    {
                        bReasult = RecStringQueue.Dequeue();
                    }
                    if (bReasult.Length > 0)
                    {
                        if (AnalyeEvent != null)
                        {
                            AnalyeEvent(bReasult);
                        }
                    }
                }
                else
                {
                    dataArrivedSignal.Reset();
                }
            }
        }
    }
}
