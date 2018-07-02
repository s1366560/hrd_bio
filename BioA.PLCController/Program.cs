using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using BioA.Common.Communication;
using BioA.Common.IO;
using BioA.Common.Machine;

namespace BioA.PLCController
{
    class Program
    {
        //网络控制器
        static SERVER TcpServer = null;
        static Machine Analyzer = null;

        static void Main()
        {
            //SetProcessAffinityMask(GetCurrentProcess(), 0x0001);
            //new DetergentVolService().UpdateDetergentUsingFinishingTime(DateTime.Now);

            TcpServer = new SERVER();
            TcpServer.AnalyeEvent += new SERVER.SERVERHandler(OnServerAnalyeEvent);
            TcpServer.AnalyeConnectedEvent += new SERVER.SERVERHandler(OnTcpServerAnalyeConnectedEvent);
            TcpServer.SetupService();

            Analyzer = new Machine();
            Analyzer.MachineState.MachineStateChangedEvent += new MachineState.MachineStateHandler(OnMachineStateChangedEvent);
        }

        static void OnTcpServerAnalyeConnectedEvent(object sender)
        {
            string str = XmlUtility.Serializer(typeof(MachineState), Analyzer.MachineState);
            Console.WriteLine("客户端连接成功!" + str);
            TcpServer.SendCMD(str);
        }

        static void OnMachineStateChangedEvent(object sender)
        {
            string str = XmlUtility.Serializer(typeof(MachineState), Analyzer.MachineState);
            TcpServer.SendCMD(str);
        }

        static void OnServerAnalyeEvent(object sender)
        {
            Console.WriteLine(sender as string);
            try
            {
                Command c = CommandService.GetCommand(sender as string);
                if (c != null)
                {
                    Analyzer.ReceiveCommand(c);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
