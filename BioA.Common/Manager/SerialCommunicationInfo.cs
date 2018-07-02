using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// LIS串口通讯
    /// </summary>
    public class SerialCommunicationInfo
    {
        public SerialCommunicationInfo()
        {
            serialName = string.Empty;
            baudRate = 0;
            dataBits = 0;
            stopBits = 0;
            parity = 0;
            communicateionType = string.Empty;
            communicateionOvertime = 0;
            reConnectionTime = 0;
            startingUp = false;
            reconnection = false;
            realTimeSendResult = false;
        }

        private string serialName;
        private int baudRate;
        private int dataBits;
        private int stopBits;
        private int parity;
        private string communicateionType;
        private float communicateionOvertime;
        private float reConnectionTime;
        private bool startingUp;
        private bool reconnection;
        private bool realTimeSendResult;
        /// <summary>
        /// 串口名
        /// </summary>
        public string SerialName
        {
            get { return serialName; }
            set { serialName = value; }
        }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }
        /// <summary>
        /// 停止位
        /// </summary>
        public int StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }
        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public int Parity
        {
            get { return parity; }
            set { parity = value; }
        }
        /// <summary>
        /// 通讯类型
        /// </summary>
        public string CommunicateionType
        {
            get { return communicateionType; }
            set { communicateionType = value; }
        }
        /// <summary>
        /// 通讯超时
        /// </summary>
        public float CommunicateionOvertime
        {
            get { return communicateionOvertime; }
            set { communicateionOvertime = value; }
        }
        /// <summary>
        /// 重连时间
        /// </summary>
        public float ReConnectionTime
        {
            get { return reConnectionTime; }
            set { reConnectionTime = value; }
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
