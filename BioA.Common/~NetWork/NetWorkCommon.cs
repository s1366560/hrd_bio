using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common.NetWork
{
    public static class NetWorkCommon
    {
        public static string LocalIPAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);
            IPAddress localaddr = localhost.AddressList[0];

            return localaddr.ToString();
        }
    }
}
