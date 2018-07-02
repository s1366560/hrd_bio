using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common
{
    public class OffSetGain:CLItem
    {
        int _WaveLength;
        public int WaveLength
        {
            get { return _WaveLength; }
            set { _WaveLength = value; }
        }

        int _Gain;
        public int Gain
        {
            get { return _Gain; }
            set { _Gain = value; }
        }
        int _OffSet;
        public int OffSet
        {
            get { return _OffSet; }
            set { _OffSet = value; }
        }
        string _InspectTime;
        public string InspectTime
        {
            get { return _InspectTime; }
            set { _InspectTime = value; }
        }
    }
}
