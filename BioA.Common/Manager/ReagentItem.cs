using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common
{
    public class ReagentItem
    {
        public string ItemName { get; set; }
        public string LongName { get; set; }
        //厂商
        public string FactoryName { get; set; }
        //通道号
        public int Code { get; set; }

        //分析方法
        public string AnalyzeMethod{ get;set;}
        //第一测试点S
        public int FirstPointS{ get;set;}
        //第一测试点E
        public int FirstPointE{ get;set;}
        //第二测试点S
        public int SecondPointS{ get;set;}
        //第二测试点E
        public int SecondPointE{get;set;}
        //主波长
        public int MainWaveLength{ get;set;}
        //次波长
        public int SubWaveLength{ get;set;}
        //测试次数
        public int DoCount{ get;set;}
        //质控间隔
        public int QCSpace{ get;set;}
        //定标次数
        public int SDTCount{get;set;}

        //试剂1体积
        public int R1Vol{ get;set;}
        //试剂2体积
        public int R2Vol{ get;set;}
        //样本反应体积
        public float IncreaseVol { get; set; }
        public float NormalVol { get; set; }
        public float DecreaseVol { get; set; }
        public float SDTVol { get; set; }
        //反应方向
        public int ReacteDirect{ get;set;}
        //单位
        public string Unit{ get;set;}
        //数据精度
        public int RadixPointNum{ get;set;}
        //试剂空白范围
        public float ReagentAbsMin{ get;set;}
        public float ReagentAbsMax{ get;set;}
        //血清线性范围
        public float LineSerumLimitMax{ get;set;}
        public float LineSerumLimitMin{ get;set;}
        //血清临界值
        public float SerumPanicLimitMax{ get;set;}
        public float SerumPanicLimitMin{ get;set;}
        //试剂搅拌强度
        public int Stiring1Force { get; set; }
        public int Stiring2Force { get; set; }
    }
}
