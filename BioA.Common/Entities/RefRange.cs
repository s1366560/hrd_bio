using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class RefRange
    {
        public RefRange()
        {
        }
        //年龄
        private int _Age;
        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        //年龄单位
        private string _AgeUnit;
        public string AgeUnit
        {
            get { return _AgeUnit; }
            set { _AgeUnit = value; }
        }
        //男性小值
        private float _MaleMin;
        public float MaleMin
        {
            get { return _MaleMin; }
            set { _MaleMin = value; }
        }
        //男性大值
        private float _MaleMax;
        public float MaleMax
        {
            get { return _MaleMax; }
            set { _MaleMax = value; }
        }
        //女性小值
        private float _FemaleMin;
        public float FemaleMin
        {
            get { return _FemaleMin; }
            set { _FemaleMin = value; }
        }
        //女性大值
        private float _FemaleMax;
        public float FemaleMax
        {
            get { return _FemaleMax; }
            set { _FemaleMax = value; }
        }
    }
}
