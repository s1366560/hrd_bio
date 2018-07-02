using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 样本容器信息
    /// </summary>
    public class SMPContainerTypeInfo
    {
        public SMPContainerTypeInfo()
        {
            sMPContainerType = "";
            nO = 0;
            volume = 0;
            code = 0;
        }

        private string sMPContainerType;
        private int nO;
        private int volume;
        private int code;
        /// <summary>
        /// 样本容器类型
        /// </summary>
        public string SMPContainerType
        {
            get { return sMPContainerType; }
            set { sMPContainerType = value; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public int NO
        {
            get { return nO; }
            set { nO = value; }
        }
        /// <summary>
        /// 体积
        /// </summary>
        public int Volume
        {
            get { return volume; }
            set { volume = value; }
        }
        /// <summary>
        /// 编码，发给下位机
        /// </summary>
        public int Code
        {
            get { return code; }
            set { code = value; }
        }
    }
}
