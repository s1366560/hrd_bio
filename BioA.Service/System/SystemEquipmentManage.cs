using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.Common;

namespace BioA.Service
{
    public class SystemEquipmentManage : DataTransmit
    {
        public ManuOffsetGain QueryManuOffsetGain(string strMethodName)
        {
            return myBatis.QueryManuOffsetGain(strMethodName);
        }

        public int InitialPhotometerManualCheck(string strMethodName, ManuOffsetGain manuOffsetGain)
        {
            return myBatis.InitialPhotometerManualCheck(strMethodName, manuOffsetGain);
        }

        public OffSetGain GetLatestOffSetGain(int waveLength)
        {
            return myBatis.GetLatestOffSetGain(waveLength);
        }
    }
}
