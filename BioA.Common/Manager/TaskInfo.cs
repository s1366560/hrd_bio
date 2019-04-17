using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 普通任务表——按项目
    /// </summary>
    public class TaskInfo
    {
        public TaskInfo()
        {
            iD = 0;
            sampleNum = -1;
            createDate = DateTime.Now;
            projectName = string.Empty;
            sampleType = string.Empty;
            sampleDilute = string.Empty;
            dilutedRatio = 0;
            inspectTimes = 0;
            sendTimes = 0;
            finishTimes = 0;
            taskState = 0; 
        }

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        /// <summary>
        /// 样本编号
        /// </summary>
        public int SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }
        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }
        /// <summary>
        /// 样本类型
        /// </summary>
        public string SampleType
        {
            get { return sampleType; }
            set { sampleType = value; }
        }
        /// <summary>
        /// 样本稀释
        /// </summary>
        public string SampleDilute
        {
            get { return sampleDilute; }
            set { sampleDilute = value; }
        }
        /// <summary>
        /// 稀释比例
        /// </summary>
        public float DilutedRatio
        {
            get { return dilutedRatio; }
            set { dilutedRatio = value; }
        }
        /// <summary>
        /// 任务总次数
        /// </summary>
        public int InspectTimes
        {
            get { return inspectTimes; }
            set { inspectTimes = value; }
        }
        /// <summary>
        /// 任务发送次数
        /// </summary>
        public int SendTimes
        {
            get { return sendTimes; }
            set { sendTimes = value; }
        }
        /// <summary>
        /// 任务完成次数
        /// </summary>
        public int FinishTimes
        {
            get { return finishTimes; }
            set { finishTimes = value; }
        }
        /// <summary>
        /// 任务状态; 0:表示待测，1:表示检测中，2:表示已完成
        /// </summary>
        public int TaskState
        {
            get { return taskState; }
            set { taskState = value; }
        }

        private string _Barcode;
        /// <summary>
        /// 条码
        /// </summary>
        public string Barcode
        {
            get { return _Barcode; }
            set { _Barcode = value; }
        }
        /// <summary>
        /// 是否重测
        /// </summary>
        public bool IsReRun
        {
            get { return isReRun; }
            set { isReRun = value; }
        }
        private int samplePos;
        /// <summary>
        /// 样本位置
        /// </summary>
        public int SamplePos {
            get { return samplePos; }
            set { samplePos = value; }
        }

        private int iD;
        private int sampleNum;
        private DateTime createDate;
        private string projectName;
        private string sampleType;
        private string sampleDilute;
        private float dilutedRatio;
        private int inspectTimes;
        private int sendTimes;
        private int finishTimes;
        private int taskState;
        private bool isReRun;
    }
}
