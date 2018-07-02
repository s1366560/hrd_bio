using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class QCSchedule : CLItem
    {
        //质控名称
        private string _QCName;
        public string QCName
        {
            get { return _QCName; }
            set { _QCName = value; }
        }
        //项目名称
        string _ItemName;
        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }
        //产生时间
        private DateTime _DrawDate = DateTime.Now;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }
        //项目类型 计算 组合 
        string _ItemType;
        public string ItemType
        {
            get { return _ItemType; }
            set { _ItemType = value; }
        }
        //项目状态
        string _ItemState;
        public string ItemState
        {
            get { return _ItemState; }
            set { _ItemState = value; }
        }
        //反应体积类型 
        private string _VolType;
        public string VolType
        {
            get { return _VolType; }
            set { _VolType = value; }
        }
        //项目重复次数
        private int _DoCount = 1;
        public int DoCount
        {
            get { return _DoCount; }
            set { _DoCount = value; }
        }
        //项目已经完成次数
        private int _FinishCount = 0;
        public int FinishCount
        {
            get { return _FinishCount; }
            set { _FinishCount = value; }
        }
    }
}
