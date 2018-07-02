using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class TABLESTATE
    {
        public const string EMPTY = "NULL";
        public const string NEW = "WAITING";
        public const string PREP = "PREPARING";
        public const string CALI = "CALIBRATING";

        public const string COMP = "COMPLETE";
        public const string SUCC = "SUCCESSFUL";
        public const string FAIL = "FAILED";
    }
    public class SDTTableItem :CLItem
    {
        public SDTTableItem()
        {
        }
        public SDTTableItem(string name)
            :base(name)
        {
        }

        //ID识别
        int _SDTCurveID;
        public int SDTCurveID
        {
            get { return _SDTCurveID; }
            set { _SDTCurveID = value; }
        }
        //定标曲线
        string _SDTCurve;
        public string SDTCurve
        {
            get { return _SDTCurve; }
            set { _SDTCurve = value; }
        }
        //有效天数
        int _ValidDay;
        public int ValidDay
        {
            get { return _ValidDay; }
            set { _ValidDay = value; }
        }
        //应用于系统标识
        bool   _IsUsed = false;
        public bool  IsUsed 
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }
        //完成定标时间
        DateTime _CalibDate = DateTime.Now;
        public DateTime CalibDate 
        {
            get { return _CalibDate; }
            set { _CalibDate = value; }
        }
        //定标申请时间
        DateTime _DrawDate = DateTime.Now;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }

        float _BlkAbs = 0;
        public float BlkAbs 
        {
            get { return _BlkAbs; }
            set { _BlkAbs = value; }
        }
        float _SDT1Abs = 0;
        public float SDT1Abs 
        {
            get { return _SDT1Abs; }
            set { _SDT1Abs = value; }
        }
        float _SDT2Abs = 0;
        public float SDT2Abs 
        {
            get { return _SDT2Abs; }
            set { _SDT2Abs = value; }
        }
        float _SDT3Abs = 0;
        public float SDT3Abs 
        {
            get { return _SDT3Abs; }
            set { _SDT3Abs = value; }
        }
        float _SDT4Abs = 0;
        public float SDT4Abs 
        {
            get { return _SDT4Abs; }
            set { _SDT4Abs = value; }
        }
        float _SDT5Abs = 0;
        public float SDT5Abs 
        {
            get { return _SDT5Abs; }
            set { _SDT5Abs = value; }
        }

        float _ExpireBlkAbs;
        public float ExpireBlkAbs
        {
            get { return _ExpireBlkAbs; }
            set { _ExpireBlkAbs = value; }
        }
        float _ExpireSDT1Abs;
        public float ExpireSDT1Abs
        {
            get { return _ExpireSDT1Abs; }
            set { _ExpireSDT1Abs = value; }
        }
        float _ExpireSDT2Abs;
        public float ExpireSDT2Abs
        {
            get { return _ExpireSDT2Abs; }
            set { _ExpireSDT2Abs = value; }
        }
        float _ExpireSDT3Abs;
        public float ExpireSDT3Abs
        {
            get { return _ExpireSDT3Abs; }
            set { _ExpireSDT3Abs = value; }
        }
        float _ExpireSDT4Abs;
        public float ExpireSDT4Abs
        {
            get { return _ExpireSDT4Abs; }
            set { _ExpireSDT4Abs = value; }
        }
        float _ExpireSDT5Abs;
        public float ExpireSDT5Abs
        {
            get { return _ExpireSDT5Abs; }
            set { _ExpireSDT5Abs = value; }
        }
        
        float _BlkConc = 0;
        public float BlkConc 
        {
            get { return _BlkConc; }
            set { _BlkConc = value; }
        }
        float _SDT1Conc = 0;
        public float SDT1Conc 
        {
            get { return _SDT1Conc; }
            set { _SDT1Conc = value; }
        }
        float _SDT2Conc = 0;
        public float SDT2Conc 
        {
            get { return _SDT2Conc; }
            set { _SDT2Conc = value; }
        }
        float _SDT3Conc = 0;
        public float SDT3Conc 
        {
            get { return _SDT3Conc; }
            set { _SDT3Conc = value; }
        }
        float _SDT4Conc = 0;
        public float SDT4Conc 
        {
            get { return _SDT4Conc; }
            set { _SDT4Conc = value; }
        }
        float _SDT5Conc = 0;
        public float SDT5Conc 
        {
            get { return _SDT5Conc; }
            set { _SDT5Conc = value; }
        }

        //K系数法直线斜率
        float _AbsoluteFactor = 0;
        public float AbsoluteFactor
        {
            get { return _AbsoluteFactor; }
            set { _AbsoluteFactor = value; }
        }

        string _BLKItem;
        public string BLKItem
        {
            get { return _BLKItem; }
            set { _BLKItem = value; }
        }
        string _SDT1Item;
        public string SDT1Item
        {
            get { return _SDT1Item; }
            set { _SDT1Item = value; }
        }
        string _SDT2Item;
        public string SDT2Item
        {
            get { return _SDT2Item; }
            set { _SDT2Item = value; }
        }
        string _SDT3Item;
        public string SDT3Item
        {
            get { return _SDT3Item; }
            set { _SDT3Item = value; }
        }
        string _SDT4Item;
        public string SDT4Item
        {
            get { return _SDT4Item; }
            set { _SDT4Item = value; }
        }
        string _SDT5Item;
        public string SDT5Item
        {
            get { return _SDT5Item; }
            set { _SDT5Item = value; }
        }
        string _SDTTableState = TABLESTATE.EMPTY;
        public string SDTTableState
        {
            get { return _SDTTableState; }
            set { _SDTTableState = value; }
        }
    }
}
