﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 样本结果实体类
    /// </summary>
    public class SampleResultInfo : ResultInfo
    {
        public SampleResultInfo()
        {
            iD = 0;
            sampleNum = 0;
            unitAndRange = string.Empty;
            //startTime = DateTime.Now;
            isResurvey = false;
            confirm = false;
            
        }

        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        /// <summary>
        /// 样本号
        /// </summary>
        public int SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }

        /// <summary>
        /// 单位
        /// </summary>
        public string UnitAndRange
        {
            get { return unitAndRange; }
            set { unitAndRange = value; }
        }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string ChineseName
        {
            get { return chineseName; }
            set { chineseName = value; }
        }

        /// <summary>
        /// 年龄 范围参数
        /// </summary>
        public string RangeParameter
        {
            get { return rangeParameter; }
            set { rangeParameter = value; }
        }

        /// <summary>
        /// 样本状态
        /// </summary>
        public int SampleCompletionStatus
        {
            get { return sampleCompletionStatus; }
            set { sampleCompletionStatus = value; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        //public DateTime StartTime
        //{
        //    get { return startTime; }
        //    set { startTime = value; }
        //}
        
        /// <summary>
        /// 是否从重测
        /// </summary>
        public bool IsResurvey
        {
            get { return isResurvey; }
            set { isResurvey = value; }
        }
        /// <summary>
        /// 是否发送
        /// </summary>
        public bool IsSend
        {
            get { return isSend; }
            set { isSend = value; }
        }
        /// <summary>
        /// 是否确认
        /// </summary>
        public bool Confirm
        {
            get { return confirm; }
            set { confirm = value; }
        }
        /// <summary>
        /// 结果体积类型
        /// </summary>
        public string ResultVolType
        {
            get { return resultVolType; }
            set { resultVolType = value; }
        }


        private int iD;
        private int sampleNum;
        private int sampleCompletionStatus;
        private string chineseName;
        private string unitAndRange;
        private string rangeParameter;
        //private DateTime startTime;
        private bool isResurvey;
        private bool isSend;
        private bool confirm;
        private string resultVolType;
    }
}
