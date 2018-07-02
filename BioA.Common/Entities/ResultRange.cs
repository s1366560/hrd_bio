using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class ResultRange : CLItem
    {
        public ResultRange()
        {
        }
        public ResultRange(string n)
            :base(n)
        {
        }
        //样本类型
        string _SMPType;
        public string SMPType
        {
            get { return _SMPType; }
            set { _SMPType = value; }
        }
        //性别
        string _Gender = "F";
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        //年龄范围
        int _AgeMin = 0;
        public int AgeMin
        {
            get { return _AgeMin; }
            set { _AgeMin = value; }
        }
        int _AgeMax = 100;
        public int AgeMax
        {
            get { return _AgeMax; }
            set { _AgeMax = value; }
        }
        string _AgeUnit = "Y";
        public string AgeUnit
        {
            get { return _AgeUnit; }
            set { _AgeUnit = value; }
        }
        public int AgeRangeLow
        {
            get
            {
                int v = 0;
                switch (this.AgeUnit)
                {
                    case "Y": v = this.AgeMin * 365; break;
                    case "M": v = this.AgeMin * 30; break;
                    case "D": v = this.AgeMin; break;
                }
                return v;
            }
        }
        public int AgeRangeUp
        {
            get
            {
                int v = 0;
                switch (this.AgeUnit)
                {
                    case "Y": v = this.AgeMax * 365; break;
                    case "M": v = this.AgeMax * 30; break;
                    case "D": v = this.AgeMax; break;
                }
                return v;
            }
        }
        //值范围
        float _ValueMin = 0;
        public float ValueMin
        {
            get { return _ValueMin; }
            set { _ValueMin = value; }
        }
        float _ValueMax = 0;
        public float ValueMax
        {
            get { return _ValueMax; }
            set { _ValueMax = value; }
        }
    }
}
