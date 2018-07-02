using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class LisConnector
    {
        string _IP;
        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }
        string _Port;
        public string Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        string _Com;
        public string Com
        {
            get { return _Com; }
            set { _Com = value; }
        }
        int _BaudRate;
        public int BaudRate
        {
            get { return _BaudRate; }
            set { _BaudRate = value; }
        }
        int _DataBits;
        public int DataBits
        {
            get { return _DataBits; }
            set { _DataBits = value; }
        }
        string _Parity;
        public string Parity
        {
            get { return _Parity; }
            set { _Parity = value; }
        }
        string _StopBits;
        public string StopBits
        {
            get { return _StopBits; }
            set { _StopBits = value; }
        }

        string _ConnectMode;
        public string ConnectMode
        {
            get { return _ConnectMode; }
            set { _ConnectMode = value; }
        }
        string _ConnectProtocol;
        public string ConnectProtocol
        {
            get { return _ConnectProtocol; }
            set { _ConnectProtocol = value; }
        }
        string _CommuMode;
        public string CommuMode
        {
            get { return _CommuMode; }
            set { _CommuMode = value; }
        }
        int _TimeOut;
        public int TimeOut
        {
            get { return _TimeOut; }
            set { _TimeOut = value; }
        }
        bool _IsRealTimeSending;
        public bool IsRealTimeSending
        {
            get { return _IsRealTimeSending; }
            set { _IsRealTimeSending = value; }
        }
        bool _IsAutoStart;
        public bool IsAutoStart
        {
            get { return _IsAutoStart; }
            set { _IsAutoStart = value; }
        }
        string _State = "STOP";
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
    }
}
