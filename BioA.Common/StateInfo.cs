using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class CalibRemarks
    {
        public const string EMPTY = "NULL";
        public const string NEW = "WAITING";
        public const string CALI = "CALIBRATING";
        public const string SUCC = "SUCCESSFUL";
        public const string FAIL = "FAILED";
    }

    public class TaskState
    {
        public const int NEW = 0;
        public const int START = 1;
        public const int SUCC = 2;
        public const int PAUSE = 3;
        public const int FAIL = 4;
    }
    /// <summary>
    /// 体积类型
    /// </summary>
    public class VOLTYPE
    {
        public const string NA = "NULL";
        public const string NV = "NORMALVOL";
        public const string DV = "DECREAVOL";
        public const string IV = "INCREAVOL";
        public const string SV = "SDTVOL";
    }
}
