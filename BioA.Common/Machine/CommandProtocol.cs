using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Machine
{
    public enum CommandState
    {
        RUNNING =1,
        SUCCESS =2,
        FAILURE =3,
        TIMEOUT =4
    }

    public class Command
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        //命令参数
        public object Para { get; set; }
        //命令状态   RUNNING =1,SUCCESS =2,FAILURE =3,TIMEOUT =4

        int _State;
        public int State 
        {
            get { return _State; }
            set
            {
                _State = value;
                if (value == 2)
                {
                    Console.WriteLine(Name+"成功执行");
                }
            }
        }

        public int AdjustID { get; set; }//1:zuo;2you;3shang 4xia
    }
}
