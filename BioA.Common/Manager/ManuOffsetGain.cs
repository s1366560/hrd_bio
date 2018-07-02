using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*************************************************************************************
 * CLR版本：        4.0.30319.269
 * 类 名 称：       ManuOffsetGain
 * 机器名称：       WENSION
 * 命名空间：       BioA.Common.Entities
 * 文 件 名：       ManuOffsetGain
 * 创建时间：       5/23/2012 2:21:42 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.Common.Entities
{
    public class ManuOffsetGain : CLItem
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
        float _Voltage;
        public float Voltage
        {
            get { return _Voltage; }
            set { _Voltage = value; }
        }
    }
}
