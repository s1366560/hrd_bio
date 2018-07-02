// ================================================================================================
//
// 文件名（File Name）：              DataReadWrite.cs
//
// 功能描述（Description）：          数据读取、写入类
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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common.NetWork
{
    /// <summary>
    /// 服务器端读写数据类
    /// </summary>
    /// <date>2017-07-17</date>
    /// <author>冯旗</author>
    class DataReadWrite
    {
        public TcpClient client;
        public NetworkStream ns;
        public byte[] read;
        public byte[] write;
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="client">客户端</param>
        /// <date>2017-07-17</date>
        /// <author>冯旗</author>
        public DataReadWrite(TcpClient client)
        {
            this.client = client;
            ns = client.GetStream();
            read = new byte[client.ReceiveBufferSize];  // 客户端接收字符长度
            write = new byte[client.SendBufferSize];    // 客户端发送字符长度
        }
        /// <summary>
        /// 初始化接收数据的数组
        /// </summary>
        public void InitReadArray()
        {
            read = new byte[client.SendBufferSize];
        }
        /// <summary>
        /// 初始化写入数据的数组
        /// </summary>
        public void InitWriteArray()
        {
            write = new byte[client.SendBufferSize];
        }
    }
    /// <summary>
    /// 数据读取类
    /// </summary>
    public class DataRead
    {
        public NetworkStream ns;
        public byte[] msg;

        public DataRead(NetworkStream ns, int buffersize)
        {
            this.ns = ns;
            msg = new byte[buffersize];
        }
    }
}
