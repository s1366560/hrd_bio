using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace BioA.Common.Communication
{
    class DataReadWrite
    {
        public TcpClient client;
        public NetworkStream ns;
        public byte[] read;
        public byte[] write;
        public DataReadWrite(TcpClient client)
        {
            this.client = client;
            ns = client.GetStream();
            read = new byte[client.ReceiveBufferSize];
            write = new byte[client.SendBufferSize];
        }
        public void InitReadArray()
        {
            read = new byte[client.ReceiveBufferSize];
        }
        public void InitWriteArray()
        {
            write = new byte[client.SendBufferSize];
        }
    }
    class DataRead
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
