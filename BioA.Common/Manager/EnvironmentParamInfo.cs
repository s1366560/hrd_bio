using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 环境参数
    /// </summary>
    public class EnvironmentParamInfo
    {
        public EnvironmentParamInfo()
        {
            reactionDiscTemp = 0;
            reagentDiscTemp = 0;
            cuvetteBlankLow = 0;
            cuvetteBlankHigh = 0;
            reagentSurplus = 0;
            reagentLeastVol = 0;
            abluentSurplus = 0;
            abluentLeastVol = 0;
            autoTransferReagentPos = false;
            autoFreezeTask = false;

        }

        private float reactionDiscTemp;
        private float reagentDiscTemp;
        private float cuvetteBlankLow;
        private float cuvetteBlankHigh;
        private float reagentSurplus;
        private float reagentLeastVol;
        private float abluentSurplus;
        private float abluentLeastVol;
        private bool autoTransferReagentPos;
        private bool autoFreezeTask;
        private string qCSMPContainerType;
        private string sDTSMPContainerType;
        /// <summary>
        /// 反应盘温度
        /// </summary>
        public float ReactionDiscTemp
        {
            get { return reactionDiscTemp; }
            set { reactionDiscTemp = value; }
        }
        /// <summary>
        /// 试剂盘温度
        /// </summary>
        public float ReagentDiscTemp
        {
            get { return reagentDiscTemp; }
            set { reagentDiscTemp = value; }
        }
        /// <summary>
        /// 比色杯空白最低值
        /// </summary>
        public float CuvetteBlankLow
        {
            get { return cuvetteBlankLow; }
            set { cuvetteBlankLow = value; }
        }
        /// <summary>
        /// 比色杯空白最高值
        /// </summary>
        public float CuvetteBlankHigh
        {
            get { return cuvetteBlankHigh; }
            set { cuvetteBlankHigh = value; }
        }
        /// <summary>
        /// 试剂余量报警体积
        /// </summary>
        public float ReagentSurplus
        {
            get { return reagentSurplus; }
            set { reagentSurplus = value; }
        }
        /// <summary>
        /// 试剂最少报警体积
        /// </summary>
        public float ReagentLeastVol
        {
            get { return reagentLeastVol; }
            set { reagentLeastVol = value; }
        }
        /// <summary>
        /// 清洗剂余量报警体积
        /// </summary>
        public float AbluentSurplus
        {
            get { return abluentSurplus; }
            set { abluentSurplus = value; }
        }
        /// <summary>
        /// 清洗剂最少报警体积
        /// </summary>
        public float AbluentLeastVol
        {
            get { return abluentLeastVol; }
            set { abluentLeastVol = value; }
        }
        /// <summary>
        /// 试剂不足时，自动切换多试剂位
        /// </summary>
        public bool AutoTransferReagentPos
        {
            get { return autoTransferReagentPos; }
            set { autoTransferReagentPos = value; }
        }
        /// <summary>
        /// 试剂不足时自动冻结任务
        /// </summary>
        public bool AutoFreezeTask
        {
            get { return autoFreezeTask; }
            set { autoFreezeTask = value; }
        }
    }
}
