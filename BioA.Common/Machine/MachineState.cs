using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Machine
{
    public class MachineState
    {
        public delegate void MachineStateHandler(object sender);
        public event MachineStateHandler MachineStateChangedEvent;
        void OnMachineStateChangedEvent(object sender)
        {
            if (this.MachineStateChangedEvent != null)
            {
                this.MachineStateChangedEvent(sender);
            }
        }

        string _State;
        public string State 
        {   
            get{return _State;}
            set
            {
                _State = value;
                OnMachineStateChangedEvent(value);
            }
        }

        
        public string StateValue { get; set; }
        public Command Command { get; set; }
        public byte[] SentData { get; set; }
        public string Temp { get; set; }
        
        /// <summary>
        /// 机器反馈状态
        /// </summary>
        public string Fired { get; set; }
    }
}
