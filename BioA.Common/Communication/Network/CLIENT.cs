using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System.Xml;
using BioA.Common.IO;

namespace BioA.Common.Communication
{
    public class CLIENT
    {
        private bool isLive = false;
        private TcpClient client;
        private NetworkStream ns;

        private ManualResetEvent allDone = new ManualResetEvent(false);

       
        public delegate void DataArriveHandler(object sender);
        public event DataArriveHandler DataArriveEvent;
        void OnDataArrive(object sender)
        {
            if (this.DataArriveEvent != null)
            {
                this.DataArriveEvent(sender);
            }
        }
        public event DataArriveHandler ConnectFailedEvent;
        void OnConnectFailed(object sender)
        {
            if (this.ConnectFailedEvent != null)
            {
                this.ConnectFailedEvent(sender);
            }
        }
        public event DataArriveHandler ConnectSuccessEvent;
        void OnConnectSuccess(object sender)
        {
            if (this.ConnectSuccessEvent != null)
            {
                this.ConnectSuccessEvent(sender);
            }
        }
        public event DataArriveHandler ClientErrorEvent;
        void OnClientError(object sender)
        {
            if (this.ClientErrorEvent != null)
            {
                this.ClientErrorEvent(sender);
            }
        }

        public void ConnectServer()
        {
            Console.WriteLine(DateTime.Now.Ticks);
            try
            {
                string file = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"DataConfigure.xml";
                XmlNode tcpServerNode = XMLHelper.GetNode(file, "PLCServer");

                string hostIP = XMLHelper.Read(tcpServerNode, "IP");
                string portNo = XMLHelper.Read(tcpServerNode, "Port");

                client = new TcpClient(AddressFamily.InterNetwork);
                //IPAddress[] IP = Dns.GetHostAddresses(Dns.GetHostName());
                AsyncCallback connectCallBack = new AsyncCallback(ConnectCallBack);
                allDone.Reset();
                client.BeginConnect(hostIP, System.Convert.ToInt32(portNo), connectCallBack, client);
                allDone.WaitOne();
            }
            catch (Exception e)
            {
                OnClientError(e.Message);
            }
            Console.WriteLine(DateTime.Now.Ticks);
        }
        public void ConnectServer(string ip)
        {
            client = new TcpClient(AddressFamily.InterNetwork);
            IPAddress IP = IPAddress.Parse(ip);
            AsyncCallback connectCallBack = new AsyncCallback(ConnectCallBack);
            allDone.Reset();
            client.BeginConnect(IP, 51888, connectCallBack, client);
            allDone.WaitOne();
        }
        private void ConnectCallBack(IAsyncResult iar)
        {
            allDone.Set();
            try
            {
                client = (TcpClient)iar.AsyncState;
                client.EndConnect(iar);
                ns = client.GetStream();
                DataRead dataRead = new DataRead(ns, client.ReceiveBufferSize);
                ns.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);

                OnConnectSuccess("Connecting OK");
            }
            catch (Exception e)
            {
                OnConnectFailed(e.Message);
                return;
            }
        }
        private void ReadCallBack(IAsyncResult iar)
        {
            try
            {
                DataRead dataRead = (DataRead)iar.AsyncState;
                int recv = dataRead.ns.EndRead(iar);
                string d = Encoding.UTF8.GetString(dataRead.msg, 0, recv);
                DataArriveEvent(d);
                if (isLive == false)
                {
                    dataRead = new DataRead(ns, client.ReceiveBufferSize);
                    ns.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);
                }
            }
            catch (Exception e)
            {
                OnClientError(e.Message);
                return;
            }
        }
        public void SendData(string str)
        {
            try
            {
                byte[] bytesdata = Encoding.UTF8.GetBytes(str + "\r\n");
                ns.BeginWrite(bytesdata, 0, bytesdata.Length, new AsyncCallback(SendCallBack), ns);
                ns.Flush();
            }
            catch (Exception e)
            {
                OnClientError(e.Message);
                return;
            }
        }
        private void SendCallBack(IAsyncResult iar)
        {
            try
            {
                ns.EndWrite(iar);
            }
            catch (Exception e)
            {
                OnClientError(e.Message);
                return;
            }
        }

    }
}
