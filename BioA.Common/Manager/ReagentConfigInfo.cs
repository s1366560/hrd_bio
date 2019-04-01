using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class ReagentConfigInfo
    {
        private bool isSampBarcodeOpen;
        private bool isReagentBarcodeOpen;
        private bool isHandheldBarcodeOpen;
        private int sampleBracodeLength;
        private int reagentBarcodeLength;
        /// <summary>
        /// 样本条码是否开放
        /// </summary>
        public bool IsSampBarcodeOpen
        {
            get { return isSampBarcodeOpen; }
            set { isSampBarcodeOpen = value; }
        }
        /// <summary>
        /// 试剂条码是否开放
        /// </summary>
        public bool IsReagentBarcodeOpen
        {
            get { return isReagentBarcodeOpen; }
            set { isReagentBarcodeOpen = value; }
        }
        /// <summary>
        /// 手持条码
        /// </summary>
        public bool IsHandheldBarcodeOpen
        {
            get { return isHandheldBarcodeOpen; }
            set { isHandheldBarcodeOpen = value; }
        }
        /// <summary>
        /// 样本条码长度
        /// </summary>
        public int SampleBracodeLength
        {
            get { return sampleBracodeLength; }
            set { sampleBracodeLength = value; }
        }
        /// <summary>
        /// 试剂条码长度
        /// </summary>
        public int ReagentBarcodeLength
        {
            get { return reagentBarcodeLength; }
            set { reagentBarcodeLength = value; }
        }


    }
}
