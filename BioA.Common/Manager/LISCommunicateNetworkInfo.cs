using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
   public class LISCommunicateNetworkInfo
    {
        public LISCommunicateNetworkInfo()
        {
            ipaddress ="";
            networkPort ="";
            startingUp = false ;
            reconnection = false;
            realTimeSendResult = false;
        }

        string ipaddress;
       /// <summary>
       /// IP地址
       /// </summary>
        public string IPAddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }

       
                string networkPort;
        /// <summary>
        /// 网口
        /// </summary>
        public string NetworkPort
        {
            get { return networkPort; }
            set { networkPort = value; }
        }



        bool startingUp;
        /// <summary>
        /// 是否开机启动
        /// </summary>
        public bool StartingUp
        {
            get { return startingUp; }
            set { startingUp = value; }
        }


        bool reconnection;
        /// <summary>
        /// 是否自动重连
        /// </summary>
        public bool Reconnection
        {
            get { return reconnection; }
            set { reconnection = value; }
        }


        bool realTimeSendResult;
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
