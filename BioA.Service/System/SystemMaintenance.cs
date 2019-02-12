using BioA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Service
{
    public class SystemMaintenance : DataTransmit
    {
        public List<CuvetteBlankInfo> QueryWaterBlankValueByWave(string strMethodName, string waveLength)
        {
            return myBatis.QueryWaterBlankValueByWave(strMethodName,waveLength);
        }

        public List<List<OffSetGain>> QueryNewPhotemetricValue(string strMethodName)
        {
            return myBatis.QueryNewPhotemetricValue(strMethodName);
        }
        public string getMaxMinforCuvette()
        {
            return myBatis.getMaxMinforCuvette();
        }
        //public List<OffSetGain> QueryOldPhotemetricValue(string strMethodName)
        //{
        //    return myBatis.QueryOldPhotemetricValue(strMethodName);
        //}
    }
}
