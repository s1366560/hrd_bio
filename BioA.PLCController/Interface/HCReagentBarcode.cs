using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Service;
using CLMode.Entities;

namespace CLMode.Interface
{
    public class RegentConstInfo
    {
        public string Name { get; set; }
        public int R1Count { get; set; }
        public int R2Count { get; set; }
    }
    public class BarDrawInfo
    {
        public string Bar { get; set; }
        public DateTime Datetime { get; set; }
    }
    public class RegentConstRecordInfo
    {
        public string Name { get; set; }
        public List<BarDrawInfo> R1BarDrawInfos = new List<BarDrawInfo>();
        public List<BarDrawInfo> R2BarDrawInfos = new List<BarDrawInfo>();
    }
    public class HCReagentBarcode : IReagentBarcode
    {
        int    Disk = 0;
        string Position = null;
        public object Process(string barcode, int disk, string position)
        {
            string b = Decode(barcode);
            if (b == null)
            {
                string erinfo = "条码：" + barcode + "解码失败";

                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777771";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = erinfo;
                new TroubleLogService().Save(t);

                return "条码：" + barcode + "不识别";
            }

            this.Disk = disk;
            this.Position = position;
            RGTPosition newreagentPosition = AnanlyeBarcode(b, barcode);
            if (newreagentPosition == null)
            {
                return "条码：" + barcode + "不识别";
            }

            AssayRunPara a = new AssayRunParaService().Get(newreagentPosition.Assay) as AssayRunPara;
            if (a != null)
            {
                if (a.R2Vol == 0 && newreagentPosition.AssayPara == "R2")
                {
                    string erinfo = "条码：" + barcode + "不能装填。原因：参数试剂2体积为0,不能在该位置装填试剂2";

                    TroubleLog t = new TroubleLog();
                    t.TroubleCode = "7777771";
                    t.TroubleType = TROUBLETYPE.ERR;
                    t.TroubleUnit = @"试剂条码";
                    t.TroubleInfo = erinfo;
                    new TroubleLogService().Save(t);

                    return "条码：" + barcode + "装填失败" ;
                }
            }
            //检查该条码在试剂盘中是否存在
            RGTPosition barrgtpos = new RGTPOSManager().GetAssayALLReagentByBarcode(newreagentPosition.BarCode);
            if (barrgtpos == null)
            {
                //newreagentPosition.AssayPara = "MR1";
            }
            else
            {
                ReagentBarcode r = new ReagentBarcode();
                r.Barcode = barrgtpos.BarCode;
                r.ValidPercent = barrgtpos.ValidPercent;
                r.ExchangeDatetime = DateTime.Now;
                new ReagentBarcodeService().InsertReagentBarcode(r);
                new RGTPOSManager().Delete(barrgtpos);

                //newreagentPosition.AssayPara = barrgtpos.AssayPara;
            }
            RGTPosition rgtpos = new RGTPOSManager().Get(disk, position);
            if (rgtpos == null)
            {

            }
            else
            {
                ReagentBarcode r = new ReagentBarcode();
                r.Barcode = rgtpos.BarCode;
                r.ValidPercent = rgtpos.ValidPercent;
                r.ExchangeDatetime = DateTime.Now;
                new ReagentBarcodeService().InsertReagentBarcode(r);
                new RGTPOSManager().Delete(rgtpos);
            }

            List<CLItem> reagents = new RGTPOSManager().GetAssayALLReagent(newreagentPosition.Assay);
            if (newreagentPosition.AssayPara == "R1")
            {
                bool f1 = false;
                foreach (RGTPosition e in reagents)
                {
                    if (e.AssayPara == "R1")
                    {
                        f1 = true;
                        break;
                    }
                }
                if (f1 == true)
                {
                    newreagentPosition.AssayPara = "MR1";
                }
                else
                {
                    newreagentPosition.AssayPara = "R1";
                }
            }
            if (newreagentPosition.AssayPara == "R2")
            {
                bool f1 = false;
                foreach (RGTPosition e in reagents)
                {
                    if (e.AssayPara == "R2")
                    {
                        f1 = true;
                        break;
                    }
                }
                if (f1 == true)
                {
                    newreagentPosition.AssayPara = "MR2";
                }
                else
                {
                    newreagentPosition.AssayPara = "R2";
                }
            }

            newreagentPosition.Disk = disk;
            newreagentPosition.Position = position;

            RGTPosition oldreagentPosition = new RGTPOSManager().Get(disk, position);
            if (oldreagentPosition != null)
            {
                ReagentBarcode r = new ReagentBarcode();
                r.Barcode = oldreagentPosition.BarCode;
                r.ValidPercent = oldreagentPosition.ValidPercent;
                r.ExchangeDatetime = DateTime.Now;
                new ReagentBarcodeService().InsertReagentBarcode(r);
            }

            ReagentBarcode ReagentBarcode = new ReagentBarcodeService().GetLastestReagentBarcode(barcode);
            if (ReagentBarcode == null)
            {
                newreagentPosition.ValidPercent = 99;
            }
            else
            {
                newreagentPosition.ValidPercent = ReagentBarcode.ValidPercent;
            }

            new RGTPOSManager().Delete(newreagentPosition);
            new RGTPOSManager().Save(newreagentPosition);

            TroubleLog t1 = new TroubleLog();
            t1.TroubleCode = "7777772";
            t1.TroubleType = TROUBLETYPE.WARN;
            t1.TroubleUnit = @"试剂条码";
            t1.TroubleInfo = "条码：" + barcode + "加载成功"; ;
            new TroubleLogService().Save(t1);

            return null;
        }
        string Decode(string strSource)
        {
            if (strSource.Length != 18)
            {
                return null;
            }

            List<int> d1 = new List<int>();
            foreach (char e in strSource)
            {
                switch (e)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        d1.Add(int.Parse(e+""));
                        break;
                    case 'A':
                        d1.Add(10);
                        break;
                    case 'B':
                        d1.Add(11);
                        break;
                    case 'C':
                        d1.Add(12);
                        break;
                    case 'D':
                        d1.Add(13);
                        break;
                    case 'E':
                        d1.Add(14);
                        break;
                    case 'F':
                        d1.Add(15);
                        break;
                    case 'G':
                        d1.Add(16);
                        break;
                    case 'H':
                        d1.Add(17);
                        break;
                    case 'I':
                        d1.Add(18);
                        break;
                    default:
                        return null;
                }
            }

            int t = 0;
            //（1,8）
            t = d1[0];
            d1[0] = d1[7];
            d1[7] = t;
            //（2,10）
            t = d1[1];
            d1[1] = d1[9];
            d1[9] = t;
            //（3,15）
            t = d1[2];
            d1[2] = d1[14];
            d1[14] = t;
            //（4,17）
            t = d1[3];
            d1[3] = d1[16];
            d1[16] = t;
            //（5,12）
            t = d1[4];
            d1[4] = d1[11];
            d1[11] = t;
            //（6,18）
            t = d1[5];
            d1[5] = d1[17];
            d1[17] = t;
            //（7,14）
            t = d1[6];
            d1[6] = d1[13];
            d1[13] = t;

            //1位+18位
            t = d1[0] - d1[17];
            d1[0] = t;
            //2位+17位
            t = d1[1] - d1[16];
            d1[1] = t;
            //3位+16位
            t = d1[2] - d1[15];
            d1[2] = t;

            string strv = "";
            foreach (int e in d1)
            {
                switch (e)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        strv += e;
                        break;
                    case 10:
                        strv += "A";
                        break;
                    case 11:
                        strv += "B";
                        break;
                    case 12:
                        strv += "C";
                        break;
                    case 13:
                        strv += "D";
                        break;
                    case 14:
                        strv += "E";
                        break;
                    case 15:
                        strv += "F";
                        break;
                    case 16:
                        strv += "G";
                        break;
                    case 17:
                        strv += "H";
                        break;
                    case 18:
                        strv += "I";
                        break;
                }
            }

            return strv;
        }
        void DoSaveAssayRunPara(ReagentItem ReagentItem)
        {
            //加载测试参数
            AssayRunPara a = new AssayRunPara();
            a.Name = ReagentItem.ItemName;
            a.FullName = ReagentItem.LongName;
            //分析方法
            a.AnalyzeMethod = ReagentItem.AnalyzeMethod;
            //第一测试点S
            a.FirstPointS = ReagentItem.FirstPointS;
            //第一测试点E
            a.FirstPointE = ReagentItem.FirstPointE;
            //第二测试点S
            a.SecondPointS = ReagentItem.SecondPointS;
            //第二测试点E
            a.SecondPointE = ReagentItem.SecondPointE;
            //主波长
            a.MainWaveLength = ReagentItem.MainWaveLength;
            //次波长
            a.SubWaveLength = ReagentItem.SubWaveLength;
            //测试次数
            a.DoCount = ReagentItem.DoCount;
            //质控间隔
            a.QCSpace = ReagentItem.QCSpace;
            //定标次数
            a.SDTCount = ReagentItem.SDTCount;
            //试剂1体积
            a.R1Vol = ReagentItem.R1Vol;
            //试剂2体积
            a.R2Vol = ReagentItem.R2Vol;
            //样本反应体积
            a.SerumIncreaseVol.VolPre = ReagentItem.IncreaseVol;
            a.SerumIncreaseVol.VolAft = 0;
            a.SerumIncreaseVol.VolDil = 0;
            a.SerumNormalVol.VolPre = ReagentItem.NormalVol;
            a.SerumNormalVol.VolAft = 0;
            a.SerumNormalVol.VolDil = 0;
            a.SerumDecreaseVol.VolPre = ReagentItem.DecreaseVol;
            a.SerumDecreaseVol.VolAft = 0;
            a.SerumDecreaseVol.VolDil = 0;
            a.UrineIncreaseVol.VolPre = ReagentItem.IncreaseVol;
            a.UrineIncreaseVol.VolAft = 0;
            a.UrineIncreaseVol.VolDil = 0;
            a.UrineNormalVol.VolPre = ReagentItem.NormalVol;
            a.UrineNormalVol.VolAft = 0;
            a.UrineNormalVol.VolDil = 0;
            a.UrineDecreaseVol.VolPre = ReagentItem.DecreaseVol;
            a.UrineDecreaseVol.VolAft = 0;
            a.UrineDecreaseVol.VolDil = 0;
            a.SDTVol.VolPre = ReagentItem.SDTVol;
            a.SDTVol.VolAft = 0;
            a.SDTVol.VolDil = 0;
            //反应方向
            a.ReacteDirect = ReagentItem.ReacteDirect;
            //搅拌强度
            a.Stiring1Force = ReagentItem.Stiring1Force;
            a.Stiring2Force = ReagentItem.Stiring2Force ;
            //显示顺序
            int m = new RGTPOSManager().GetReagentMode();
            
            AssayRunPara a1 = new AssayRunParaService().Get(ReagentItem.ItemName) as AssayRunPara;
            if (a1 != null && a1.DisplaySQ <= 0)
            {
                new AssayRunParaService().Delete(a1.DisplaySQ);
            }
            a1 = new AssayRunParaService().Get(ReagentItem.ItemName) as AssayRunPara;
            if (a1 == null)
            {
                for (int i = 1; i <= 120; i++)
                {
                    AssayRunPara e = new AssayRunParaService().Get(i) as AssayRunPara;
                    if (e == null)
                    {
                        if (m == 3)
                        {
                            List<int> itemnumbers = new RGTPOSManager().GetReagentNumbers();
                            bool isflag = false;
                            foreach (int ie in itemnumbers)
                            {
                                if (ie == i)
                                {
                                    isflag = true;
                                }
                            }

                            if (isflag == true)
                            {
                                continue;
                            }
                            else
                            {
                                a.DisplaySQ = i;
                                break;
                            }
                        }


                        if (m == 2)
                        {
                            a.DisplaySQ = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                a.DisplaySQ = a1.DisplaySQ;
            }

            new AssayRunParaService().Delete(ReagentItem.ItemName);
            new AssayRunParaService().Delete(a.DisplaySQ);
            new AssayRunParaService().Save(a);
        }
        void DoSaveAssayValuePara(ReagentItem ReagentItem)
        {
            AssayValuePara a = new AssayValuePara();
            a.Name = ReagentItem.ItemName;

            a.EquipAdjustRfA = 1;
            a.EquipAdjustRfB = 0;

            a.ReagentAbsMin = ReagentItem.ReagentAbsMin;
            a.ReagentAbsMax = ReagentItem.ReagentAbsMax;

            a.LineSerumLimitMax = ReagentItem.LineSerumLimitMax;
            a.LineSerumLimitMin = ReagentItem.LineSerumLimitMin;
            a.LineUrineLimitMax = ReagentItem.LineSerumLimitMax;
            a.LineUrineLimitMin = ReagentItem.LineSerumLimitMin;
            a.SerumPanicLimitMax = ReagentItem.SerumPanicLimitMax;
            a.SerumPanicLimitMin = ReagentItem.SerumPanicLimitMin;
            a.UrineAbs = 0;
            a.SerumAbs = 0;
            a.OtherAbs = 0;

            a.PreDirection = ReagentItem.ReacteDirect;
            a.PreviousLimit = 0;

            a.IsAutoRedo = false;

            new AssayValueParaService().Delete(ReagentItem.ItemName);
            new AssayValueParaService().Save(a);
        }
        void DoSaveResultSets(ReagentItem ReagentItem)
        {
            new ResultSetService().Delete(ReagentItem.ItemName);
            foreach (SMPType e in new SMPTypeService().GetALL())
            {
                ResultSet d = new ResultSet();
                d.SampleType = e.Name;
                d.Unit = ReagentItem.Unit;
                d.Name = ReagentItem.ItemName;
                d.RadixPointNum = ReagentItem.RadixPointNum;

                new ResultSetService().Save(d);
            }
        }
        void DoSaveRunSQ(ReagentItem ReagentItem)
        {
            RunAssaySQ r = null;

            RunAssaySQ runAssaySQ = new RunAssaySQService().Get(ReagentItem.ItemName) as RunAssaySQ;
            if (runAssaySQ != null)
            {
                r = new RunAssaySQ();
                r.AssayName = ReagentItem.ItemName;
                r.RunSQ = runAssaySQ.RunSQ;
            }
            else
            {
                r = new RunAssaySQ();
                r.AssayName = ReagentItem.ItemName;
                r.RunSQ = new RunAssaySQService().GetALL().Count;
            }

            new RunAssaySQService().Delete(ReagentItem.ItemName);
            new RunAssaySQService().Save(r);
        }

        RGTPosition AnanlyeBarcode(string barcode,string b1)
        {
            string code = barcode.Substring(0, 3);
            ReagentItem reagentItem = new ReagentItemService().GetReagentItem(code);
            if (reagentItem == null)
            {
                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777772";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = "条码：" + b1 + "与封闭项目不匹配";
                new TroubleLogService().Save(t);

                return null;
            }

            RGTPosition reagentPosition = new RGTPosition();

            reagentPosition.Assay = reagentItem.ItemName;
            //1:S10ml  2:M 20ml  3:L 70ml  4：XL100m
            RGTContainerType RGTContainerType = null;
            string containertype = barcode.Substring(3, 1);
            switch (containertype)
            {
                case "1":
                    RGTContainerType = this.GetRGTContainerType(20);
                    break;
                case "2":
                    RGTContainerType = this.GetRGTContainerType(40);
                    break;
                case "3":
                    RGTContainerType = this.GetRGTContainerType(70);
                    break;
                case "4":
                    RGTContainerType = this.GetRGTContainerType(100);
                    break;
            }
            if (RGTContainerType == null)
            {
                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777773";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = "条码：" + b1 + "容器类型系统不识别";
                new TroubleLogService().Save(t);

                return null;
            }
            else
            {
                reagentPosition.CType.Name = RGTContainerType.Name;
            }

            //1:R1 2:R2 3:R3 4:R4  5:稀释液 6：清洗剂 
            string reagenttype = barcode.Substring(4, 1);
            switch (reagenttype)
            {
                case "1": reagentPosition.AssayPara = "R1"; break;
                case "2": reagentPosition.AssayPara = "R2"; break;
                case "3": reagentPosition.AssayPara = "R3"; break;
                case "4": reagentPosition.AssayPara = "R4"; break;
                case "5": reagentPosition.AssayPara = "Diluent"; break;
                //case "6": ReagentPosition.AssayPara = "R4"; break;
            }
            switch (this.Disk)
            {
                case 1:
                    if (reagentPosition.AssayPara == "R1" || reagentPosition.AssayPara == "Diluent")
                    {

                    }
                    else
                    {
                        TroubleLog t = new TroubleLog();
                        t.TroubleCode = "7777774";
                        t.TroubleType = TROUBLETYPE.ERR;
                        t.TroubleUnit = @"试剂条码";
                        t.TroubleInfo = "条码：" + b1 + "试剂类型R1识别失败";
                        new TroubleLogService().Save(t);

                        return null;
                    }
                    break;
                case 2:
                    if (reagentPosition.AssayPara == "R2")
                    {

                    }
                    else
                    {
                        TroubleLog t = new TroubleLog();
                        t.TroubleCode = "7777775";
                        t.TroubleType = TROUBLETYPE.ERR;
                        t.TroubleUnit = @"试剂条码";
                        t.TroubleInfo = "条码：" + b1 + "试剂类型R2识别失败";
                        new TroubleLogService().Save(t);

                        return null;
                    }
                    break;

            }

            string batchnum = barcode.Substring(5, 6);


            reagentPosition.RGTProductor.FactoryName = "上海华臣生物试剂有限公司";
            reagentPosition.RGTProductor.BatchNO = batchnum;

            string str1 = batchnum.Substring(0, 2);
            int str1int = 0;
            try
            {
                str1int = int.Parse(str1);
            }
            catch
            {
                return null;
            }
            str1 = "20" + (str1int + 1).ToString() + batchnum.Substring(2);

            try
            {
                reagentPosition.RGTProductor.ExpireDay = DateTime.ParseExact(str1, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777776";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = "条码：" + b1 + "识别失败";
                new TroubleLogService().Save(t);

                return null;
            }

            reagentPosition.BarCode = b1;

            DoSaveAssayRunPara(reagentItem);
            DoSaveAssayValuePara(reagentItem);
            DoSaveResultSets(reagentItem);
            //测试序列
            DoSaveRunSQ(reagentItem);

            return reagentPosition;
        }
        RGTContainerType GetRGTContainerType(int vol)
        {
            foreach (RGTContainerType e in new RGTContainerTypeService().GetALL())
            {
                if (e.Volume == vol)
                {
                    return e;
                }
            }

            return null;
        }


        public RegentConstInfo GetR1R2ConstCount(ReagentItem item, DateTime begin, DateTime end)
        {
            RegentConstInfo inf = new RegentConstInfo();
            inf.Name = item.ItemName;
            inf.R1Count = 0;
            inf.R2Count = 0;
            List<string> bars = new ReagentBarcodeService().GetAllBarString(begin,end);
            foreach (string e in bars)
            {
                string b = Decode(e);
                if (b == null)
                {
                    continue;
                }

                string code = b.Substring(0, 3);
                if (code != item.Code.ToString())
                {
                    continue;
                }

                string reagenttype = b.Substring(4, 1);
                switch (reagenttype)
                {
                    case "1": inf.R1Count++; break;
                    case "2": inf.R2Count++; break;
                    case "3": break;
                    case "4": break;
                    case "5":break;
                    //case "6": ReagentPosition.AssayPara = "R4"; break;
                }

            }

            return inf;
        }

        public RegentConstRecordInfo GetRegentConstRecordInfo(ReagentItem item, DateTime begin, DateTime end)
        {
            RegentConstRecordInfo inf = new RegentConstRecordInfo();
            inf.Name = item.ItemName;
           
            List<ReagentBarcode> bars = new ReagentBarcodeService().GetReagentBarcodes(begin, end);
            foreach (ReagentBarcode e in bars)
            {
                /*
                if (e.ValidPercent != 99)
                {
                    continue;
                }
                */
                string b = Decode(e.Barcode);
                if (b == null)
                {
                    continue;
                }

                string code = b.Substring(0, 3);
                if (code != item.Code.ToString())
                {
                    continue;
                }

                BarDrawInfo barDrawInfo = new BarDrawInfo();
                barDrawInfo.Bar = e.Barcode;
                barDrawInfo.Datetime = e.ExchangeDatetime;
                string reagenttype = b.Substring(4, 1);
                switch (reagenttype)
                {
                    case "1":
                        inf.R1BarDrawInfos.Add(barDrawInfo);
                        break;
                    case "2":
                        inf.R2BarDrawInfos.Add(barDrawInfo);
                        break;
                    case "3": break;
                    case "4": break;
                    case "5":break;
                    //case "6": ReagentPosition.AssayPara = "R4"; break;
                }

            }

            return inf;
        }
       

    }
}
