// ================================================================================================
//
// 文件名（File Name）：              Client.cs
//
// 功能描述（Description）：          系统模块Socket通讯客户端
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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BioA.Common.NetWork
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream ns;

        private ManualResetEvent allDone = new ManualResetEvent(false);


        public delegate void DataArriveHandler(object sender);
        public event DataArriveHandler DataArriveEvent;

        // 连接失败、连接成功响应事件
        public event DataArriveHandler ConnectFailedEvent;
        public event DataArriveHandler ConnectSuccessEvent;

        /// <summary>
        /// 客户端连接服务器入口，先建立连接。
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        public void ConnectServer(string ip, int intPort)
        {
            client = new TcpClient(AddressFamily.InterNetwork); // 初始化客户端连接
            IPAddress IP = IPAddress.Parse(ip);    // 字符串IP转网络IP格式
            AsyncCallback connectCallBack = new AsyncCallback(ConnectCallBack);    // 定义连接动作完成后的回调函数
            client.BeginConnect(IP, intPort, connectCallBack, client);        // 连接（IP地址、端口号、连接动作完成后回调函数、返回的信息，可自定义类型）
        }

        /// <summary>
        /// 连接失败通知
        /// </summary>
        /// <param name="sender"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        void OnConnectFailed(object sender)
        {
            if (this.ConnectFailedEvent != null)
            {
                this.ConnectFailedEvent(sender);
            }
        }
        /// <summary>
        /// 连接失败通知
        /// </summary>
        /// <param name="sender"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        void OnConnectSuccess(object sender)
        {
            if (this.ConnectSuccessEvent != null)
            {
                this.ConnectSuccessEvent(sender);
            }
        }
        public event DataArriveHandler ClientErrorEvent;
        /// <summary>
        /// 连接错误
        /// </summary>
        /// <param name="sender"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        void OnClientError(object sender)
        {
            if (this.ClientErrorEvent != null)
            {
                this.ClientErrorEvent(sender);
            }
        }

        /// <summary>
        /// 当完成network连接后异步回调的函数
        /// </summary>
        /// <param name="ip"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        private void ConnectCallBack(IAsyncResult iar)
        {
            allDone.Set();
            try
            {
                // 完成连接动作后服务器返回的信息
                client = (TcpClient)iar.AsyncState;
                // 异步接收传入连接尝试
                client.EndConnect(iar);
                ns = client.GetStream();
                DataRead dataRead = new DataRead(ns, client.ReceiveBufferSize);
                // 异步读取
                ns.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);

                OnConnectSuccess("Connecting OK");
            }
            catch (Exception e)
            {
                OnConnectFailed(e.Message);
                return;
            }
        }
        /// <summary>
        /// 接收信息回调函数
        /// </summary>
        /// <param name="iar"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        private void ReadCallBack(IAsyncResult iar)
        {
            //try
            //{
            // 读取操作后后获取服务器返回信息
            DataRead dataRead = (DataRead)iar.AsyncState;
            // 读取接收信息结束
            int recv = dataRead.ns.EndRead(iar);
            string d = Encoding.UTF8.GetString(dataRead.msg, 0, recv);
            DataArriveEvent(d);

            dataRead = new DataRead(ns, client.ReceiveBufferSize);
            ns.BeginRead(dataRead.msg, 0, dataRead.msg.Length, ReadCallBack, dataRead);

            //}
            //catch (Exception e)
            //{
            //    OnClientError(e.Message);
            //    return;
            //}
        }
        /// <summary>
        /// 向服务器发送数据
        /// </summary>
        /// <param name="str">发送信息</param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
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
        /// <summary>
        /// 向服务器发送数据
        /// </summary>
        /// <param name="strName">key</param>
        /// <param name="strValue">value</param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        public void SendData(string strName, string strValue)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(strName).Append("|").Append(strValue);
                byte[] bytesdata = Encoding.UTF8.GetBytes(str.ToString() + "\r\n");
                ns.BeginWrite(bytesdata, 0, bytesdata.Length, new AsyncCallback(SendCallBack), ns);
                ns.Flush();
            }
            catch (Exception e)
            {
                OnClientError(e.Message);
                return;
            }
        }
        /// <summary>
        /// 向服务器发送数据
        /// </summary>
        /// <param name="strModule">模块名</param>
        /// <param name="strName">访问数据库方法名</param>
        /// <param name="strValue">参数对象</param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        public void SendData(string strModule, string strName, string strValue)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(strModule).Append("|").Append(strName).Append("|").Append(strValue);
                byte[] bytesdata = Encoding.UTF8.GetBytes(str.ToString() + "\r\n");
                ns.BeginWrite(bytesdata, 0, bytesdata.Length, new AsyncCallback(SendCallBack), ns);
                ns.Flush();
            }
            catch (Exception e)
            {
                OnClientError(e.Message);
                return;
            }
        }
        /// <summary>
        /// 完成发送数据动作后的回调函数
        /// </summary>
        /// <param name="iar"></param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
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
