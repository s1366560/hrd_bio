using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class CalibratorinfoTask
    {
        public CalibratorinfoTask()
        {

            calibName = string.Empty;
            pos = string.Empty;
            sampleType = string.Empty;
            inspectTimes = 0;
            sendTimes = 0;
            finishTimes = 0;
            taskState = 0;
        }

        private int iD;
        private string sampleNum;
        private string calibName;
        private string pos;
        private string projectName;
        private DateTime createDate;
        private string sampleType;
        private int inspectTimes;
        private int sendTimes;
        private int finishTimes;
        private int taskState;
        // 自增id
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <summary>
        /// 顺序号
        /// </summary>
        public string SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }
      
        /// <summary>
        /// 校准品名
        /// </summary>
        public string CalibName
        {
            get { return calibName; }
            set { calibName = value; }
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
        /// 位置
        /// </summary>
        public string Pos
        {
            get { return pos; }
            set { pos = value; }
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
        /// 检验总次数
        /// </summary>
        public int InspectTimes
        {
            get { return inspectTimes; }
            set { inspectTimes = value; }
        }
        /// <summary>
        /// 发送任务次数
        /// </summary>
        public int SendTimes
        {
            get { return sendTimes; }
            set { sendTimes = value; }
        }
        /// <summary>
        /// 完成次数
        /// </summary>
        public int FinishTimes
        {
            get { return finishTimes; }
            set { finishTimes = value; }
        }
        /// <summary>
        /// 任务状态，0表示未发送给下位机，1表示执行中，2表示已完成，3表示被终止
        /// </summary>
        public int TaskState
        {
            get { return taskState; }
            set { taskState = value; }
        }
    }

    
}
