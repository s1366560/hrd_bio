using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service.MainTains
{
    public class MainTain : DataTransmit
    {
        public int GetAllTasksCount(string StrmethodName)
        {
            return myBatis.GetAllTasksCount(StrmethodName);
        }
    }
}
