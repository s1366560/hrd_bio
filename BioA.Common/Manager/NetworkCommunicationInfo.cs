using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common.Manager
{
    /// <summary>
    /// LIS网口通讯
    /// </summary>
    public class NetworkCommunicationInfo
    {
        public NetworkCommunicationInfo()
        {
            iPAddress = string.Empty;
            networkPort = string.Empty;
            startingUp = false;
            reconnection = false;
            realTimeSendResult = false;
        }

        private string iPAddress;
        private string networkPort;
        private bool startingUp;
        private bool reconnection;
        private bool realTimeSendResult;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IPAddress
        {
            get { return iPAddress; }
            set { iPAddress = value; }
        }
        /// <summary>
        /// 网口
        /// </summary>
        public string NetworkPort
        {
            get { return networkPort; }
            set { networkPort = value; }
        }
        /// <summary>
        /// 是否开机启动
        /// </summary>
        public bool StartingUp
        {
            get { return startingUp; }
            set { startingUp = value; }
        }
        /// <summary>
        /// 是否自动重连
        /// </summary>
        public bool Reconnection
        {
            get { return reconnection; }
            set { reconnection = value; }
        }
        /// <summary>
        /// 是否实时发送结果
        /// </summary>
        public bool RealTimeSendResult
        {
            get { return realTimeSendResult; }
            set { realTimeSendResult = value; }
        }
    }
}
