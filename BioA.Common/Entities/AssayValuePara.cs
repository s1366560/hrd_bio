using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class AssayValuePara : CLItem
    {
        public AssayValuePara()
        {
        }
        public AssayValuePara(string name)
            :base(name)
        {
        }
        //设备校准系数
        private float _EquipAdjustRfA;
        public float EquipAdjustRfA
        {
            get { return _EquipAdjustRfA; }
            set { _EquipAdjustRfA = value; }
        }
        private float _EquipAdjustRfB;
        public float EquipAdjustRfB
        {
            get { return _EquipAdjustRfB; }
            set { _EquipAdjustRfB = value; }
        }
        //试剂空白范围
        private float _ReagentAbsMin;
        public float ReagentAbsMin
        {
            get { return _ReagentAbsMin; }
            set { _ReagentAbsMin = value; }
        }
        private float _ReagentAbsMax;
        public float ReagentAbsMax
        {
            get { return _ReagentAbsMax; }
            set { _ReagentAbsMax = value; }
        }
        //血清线性范围
        private float _LineSerumLimitMax;
        public float LineSerumLimitMax
        {
            get { return _LineSerumLimitMax; }
            set { _LineSerumLimitMax = value; }
        }
        private float _LineSerumLimitMin;
        public float LineSerumLimitMin
        {
            get { return _LineSerumLimitMin; }
            set { _LineSerumLimitMin = value; }
        }
        //尿液线性范围
        private float _LineUrineLimitMax;
        public float LineUrineLimitMax
        {
            get { return _LineUrineLimitMax; }
            set { _LineUrineLimitMax = value; }
        }
        private float _LineUrineLimitMin;
        public float LineUrineLimitMin
        {
            get { return _LineUrineLimitMin; }
            set { _LineUrineLimitMin = value; }
        }
        //血清临界值
        private float _SerumPanicLimitMax;
        public float SerumPanicLimitMax
        {
            get { return _SerumPanicLimitMax; }
            set { _SerumPanicLimitMax = value; }
        }
        private float _SerumPanicLimitMin;
        public float SerumPanicLimitMin
        {
            get { return _SerumPanicLimitMin; }
            set { _SerumPanicLimitMin = value; }
        }
        //尿液底物耗尽
        private float _UrineAbs;
        public float UrineAbs
        {
            get { return _UrineAbs; }
            set { _UrineAbs = value; }
        }
        //血清底物耗尽
        private float _SerumAbs;
        public float SerumAbs
        {
            get { return _SerumAbs; }
            set { _SerumAbs = value; }
        }
        //其他底物耗尽
        private float _OtherAbs;
        public float OtherAbs
        {
            get { return _OtherAbs; }
            set { _OtherAbs = value; }
        }
        //前驱界限方向
        private int _PreDirection;
        public int PreDirection
        {
            get { return _PreDirection; }
            set { _PreDirection = value; }
        }
        //前驱界限范围
        private float _PreviousLimit;
        public float PreviousLimit
        {
            get { return _PreviousLimit; }
            set { _PreviousLimit = value; }
        }
        
        //自动重测标志
        bool _IsAutoRedo = false;
        public bool IsAutoRedo
        {
            get { return _IsAutoRedo; }
            set { _IsAutoRedo = value; }
        }
    }
}
