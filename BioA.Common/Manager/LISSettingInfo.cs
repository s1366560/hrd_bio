using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// LIS设置实体
    /// </summary>
    public class LISSettingInfo
    {
        public LISSettingInfo()
        {

        }
        private string _CommunicationMode;
        private string _CommunicationDirection;
        private int _CommunicationOverTime;
        private bool _RealTiimeSampleResults;
        /// <summary>
        /// 通讯方式
        /// </summary>
        public string CommunicationMode
        {
            get { return _CommunicationMode; }
            set { _CommunicationMode = value; }
        }
        /// <summary>
        /// 通讯模式（单、双向）
        /// </summary>
        public string CommunicationDirection
        {
            get { return _CommunicationDirection; }
            set { _CommunicationDirection = value; }
        }
        /// <summary>
        /// 通讯超时
        /// </summary>
        public int CommunicationOverTime
        {
            get { return _CommunicationOverTime; }
            set { _CommunicationOverTime = value; }
        }
        /// <summary>
        /// 实时发送样本测试结果
        /// </summary>
        public bool RealTiimeSampleResults
        {
            get { return _RealTiimeSampleResults; }
            set { _RealTiimeSampleResults = value; }
        }
    }


}
