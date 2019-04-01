using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BioA.IBLL;
using BioA.Common;
using BioA.SqlMaps;
using IBatisNet.DataMapper;
using BioA.Service;

namespace BioA.BLL
{
    public class ReagentBarcode : IReagentBarcode
    {
        /// <summary>
        /// 操作数据库
        /// </summary>
        private static MyBatis mybatis= new MyBatis();

        private int Disk = 0;
        private string Position = null;

        /// <summary>
        /// 根据试剂条码信息获取对应的项目参数信息
        /// </summary>
        /// <param name="disk"></param>
        /// <param name="pos"></param>
        /// <param name="reagentBracode"></param>
        /// <returns></returns>
        public object GetRgBracodePara(int disk, string pos, string reagentBracode)
        {
            string b = Decode(reagentBracode);
            if (b == null)
            {
                string erinfo = "条码：" + reagentBracode + "解码失败";

                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777771";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = erinfo;
                //new ReagentState().TroubleLogSave().Save(t);
                mybatis.TroubleLogSave("TroubleLogSave",t);
                return "条码：" + reagentBracode + "不识别";
            }

            this.Disk = disk;
            this.Position = pos;
            ReagentSettingsInfo newReagentSet= AnanlyeBarcode(b, reagentBracode);
            if (newReagentSet == null)
            {
                return "条码：" + reagentBracode + "不识别";
            }

            AssayProjectParamInfo a = mybatis.GetAssayProjectParamInfo(newReagentSet.ProjectName, newReagentSet.ReagentType) as AssayProjectParamInfo;
            if (a != null)
            {
                if (a.Reagent2VolSettings == 0 && newReagentSet.AssayParamType == "R2")
                {
                    string erinfo = "条码：" + reagentBracode + "不能装填。原因：参数试剂2体积为0,不能在该位置装填试剂2";

                    TroubleLog t = new TroubleLog();
                    t.TroubleCode = "7777771";
                    t.TroubleType = TROUBLETYPE.ERR;
                    t.TroubleUnit = @"试剂条码";
                    t.TroubleInfo = erinfo;
                    mybatis.TroubleLogSave("TroubleLogSave", t);

                    return "条码：" + reagentBracode + "装填失败";
                }
            }
            //检查该条码在试剂盘中是否存在
            ReagentSettingsInfo barrgtpos = mybatis.GetAssayALLReagentByBarcode(newReagentSet.Barcode);
            if (barrgtpos == null)
            {
                //newreagentPosition.AssayPara = "MR1";
            }
            else
            {
                this.RemoveOccupiedReagentInfo(barrgtpos);
                //newreagentPosition.AssayPara = barrgtpos.AssayPara;
            }
            ReagentSettingsInfo rgtpos = mybatis.GetAssayReagentByDisk(Disk,Position);
            if (rgtpos == null)
            {

            }
            else
            {
                this.RemoveOccupiedReagentInfo(rgtpos);
            }

            //List<CLItem> reagents = new RGTPOSManager().GetAssayALLReagent(newReagentSetInfo.Assay);
            //if (newReagentSetInfo.AssayPara == "R1")
            //{
            //    bool f1 = false;
            //    foreach (RGTPosition e in reagents)
            //    {
            //        if (e.AssayPara == "R1")
            //        {
            //            f1 = true;
            //            break;
            //        }
            //    }
            //    if (f1 == true)
            //    {
            //        newReagentSetInfo.AssayPara = "MR1";
            //    }
            //    else
            //    {
            //        newReagentSetInfo.AssayPara = "R1";
            //    }
            //}
            //if (newReagentSetInfo.AssayPara == "R2")
            //{
            //    bool f1 = false;
            //    foreach (RGTPosition e in reagents)
            //    {
            //        if (e.AssayPara == "R2")
            //        {
            //            f1 = true;
            //            break;
            //        }
            //    }
            //    if (f1 == true)
            //    {
            //        newReagentSetInfo.AssayPara = "MR2";
            //    }
            //    else
            //    {
            //        newReagentSetInfo.AssayPara = "R2";
            //    }
            //}

            //newReagentSetInfo.Disk = disk;
            newReagentSet.Pos = pos;

            //RGTPosition oldreagentPosition = new RGTPOSManager().Get(disk, pos);
            //if (oldreagentPosition != null)
            //{
            //    ReagentBarcode r = new ReagentBarcode();
            //    r.Barcode = oldreagentPosition.BarCode;
            //    r.ValidPercent = oldreagentPosition.ValidPercent;
            //    r.ExchangeDatetime = DateTime.Now;
            //    new ReagentBarcodeService().InsertReagentBarcode(r);
            //}

            ReagentBarcodeParam ReagentBarcode = mybatis.GetAllReagentBarParam(reagentBracode);
            if (ReagentBarcode == null)
            {
                if (disk == 1)
                {
                    newReagentSet.ValidPercent = 99;
                }
                else if (disk == 2)
                {
                    newReagentSet.ValidPercent2 = 99;
                }
            }
            else
            {
                if (disk == 1)
                {
                    newReagentSet.ValidPercent = ReagentBarcode.ValidPercent;
                }
                else if (disk == 2)
                {
                    newReagentSet.ValidPercent2 = ReagentBarcode.ValidPercent;
                }
            }

            mybatis.SaveReagentSettingInfo(disk, newReagentSet);
            new Task(new Action(() => { this.SaveOrUpReagentStateR1R2Info(newReagentSet); })).Start(); 

            TroubleLog t1 = new TroubleLog();
            t1.TroubleCode = "7777772";
            t1.TroubleType = TROUBLETYPE.WARN;
            t1.TroubleUnit = @"试剂条码";
            t1.TroubleInfo = "条码：" + reagentBracode + "加载成功"; ;
            mybatis.TroubleLogSave("TroubleLogSave", t1);

            return null;
        }

        /// <summary>
        /// 保存或更新试剂状态信息 ReagentStateInfoR1R2tb
        /// </summary>
        /// <param name="r">试剂（R1 or R2）参数信息</param>
        private void SaveOrUpReagentStateR1R2Info(ReagentSettingsInfo r)
        {
            ReagentStateInfoR1R2 r1r2 = mybatis.SelectReagentStateForR1R2("SelectReagentStateForR1R2",r);
            int microlitre = System.Convert.ToInt32(r.ReagentContainer.Substring(0, r.ReagentContainer.IndexOf("ml"))) * (Convert.ToInt32(this.Disk == 1 ? r.ValidPercent : r.ValidPercent2 ) - 3) * 1000 / 100;
            int MeasurableNumber = r.ReagentVol == 0 ? 0 : microlitre / r.ReagentVol;
            if (r1r2 != null)
            {
                mybatis.UpdateReagentR1AndR2Info(this.Disk, r, MeasurableNumber);
            }
            else
            {
                mybatis.SaveReagentR1AndR2Info(this.Disk, r, MeasurableNumber);
            }
        }

        /// <summary>
        /// 解析试剂条码
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
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
                        d1.Add(int.Parse(e + ""));
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

        /// <summary>
        /// 根据条码解析数据
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        ReagentSettingsInfo AnanlyeBarcode(string barcode, string b1)
        {
            string code = barcode.Substring(0, 3);
            ReagentItem reagentItem = mybatis.getReagentItemInfo(code);
            if (reagentItem == null)
            {
                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777772";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = "条码：" + b1 + "与封闭项目不匹配";
                mybatis.TroubleLogSave("TroubleLogSave", t);

                return null;
            }

            ReagentSettingsInfo reagentSettingsInfo = new ReagentSettingsInfo();

            reagentSettingsInfo.ProjectName = reagentItem.ItemName;
            //1:S10ml  2:M 20ml  3:L 70ml  4：XL100m
            string RGTContainerType = null;
            string containertype = barcode.Substring(3, 1);
            switch (containertype)
            {
                case "1":
                    reagentSettingsInfo.ReagentContainer = "20ml";
                    break;
                case "2":
                    reagentSettingsInfo.ReagentContainer = "40ml";
                    break;
                case "3":
                    reagentSettingsInfo.ReagentContainer = "70ml";
                    break;
                case "4":
                    reagentSettingsInfo.ReagentContainer = "100ml";
                    break;
            }
            if (string.IsNullOrEmpty(reagentSettingsInfo.ReagentContainer))
            {
                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777773";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = "条码：" + b1 + "容器类型系统不识别";
                mybatis.TroubleLogSave("TroubleLogSave", t);

                return null;
            }

            //1:R1 2:R2 3:R3 4:R4  5:稀释液 6：清洗剂 
            string reagenttype = barcode.Substring(4, 1);
            switch (reagenttype)
            {
                case "1": reagentSettingsInfo.AssayParamType = "R1"; break;
                case "2": reagentSettingsInfo.AssayParamType = "R2"; break;
                case "3": reagentSettingsInfo.AssayParamType = "R3"; break;
                case "4": reagentSettingsInfo.AssayParamType = "R4"; break;
                case "5": reagentSettingsInfo.AssayParamType = "Diluent"; break;
                //case "6": ReagentPosition.AssayPara = "R4"; break;
            }
            switch (this.Disk)
            {
                case 1:
                    if (reagentSettingsInfo.AssayParamType == "R1" || reagentSettingsInfo.AssayParamType == "Diluent")
                    {
                        reagentSettingsInfo.ReagentName = reagentItem.ItemName + "R1";
                        reagentSettingsInfo.ReagentVol = reagentItem.R1Vol;
                    }
                    else
                    {
                        TroubleLog t = new TroubleLog();
                        t.TroubleCode = "7777774";
                        t.TroubleType = TROUBLETYPE.ERR;
                        t.TroubleUnit = @"试剂条码";
                        t.TroubleInfo = "条码：" + b1 + "试剂类型R1识别失败";
                        mybatis.TroubleLogSave("TroubleLogSave", t);

                        return null;
                    }
                    break;
                case 2:
                    if (reagentSettingsInfo.AssayParamType == "R2")
                    {
                        reagentSettingsInfo.ReagentName = reagentItem.ItemName + "R2";
                        reagentSettingsInfo.ReagentVol = reagentItem.R2Vol;
                    }
                    else
                    {
                        TroubleLog t = new TroubleLog();
                        t.TroubleCode = "7777775";
                        t.TroubleType = TROUBLETYPE.ERR;
                        t.TroubleUnit = @"试剂条码";
                        t.TroubleInfo = "条码：" + b1 + "试剂类型R2识别失败";
                        mybatis.TroubleLogSave("TroubleLogSave", t);

                        return null;
                    }
                    break;

            }

            string batchnum = barcode.Substring(5, 6);
            //项目试剂类型
            string projectReagenttype = barcode.Substring(12, 1);
            switch (projectReagenttype)
            {
                case "1": reagentSettingsInfo.ReagentType = "血清"; break;
                case "2": reagentSettingsInfo.ReagentType = "尿液"; break;
                case "3": reagentSettingsInfo.ReagentType = "稀释液"; break;
                case "4": reagentSettingsInfo.ReagentType = "清洗剂"; break;
            }


            
            reagentSettingsInfo.BatchNum= batchnum;

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
                reagentSettingsInfo.ValidDate = DateTime.ParseExact(str1, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                TroubleLog t = new TroubleLog();
                t.TroubleCode = "7777776";
                t.TroubleType = TROUBLETYPE.ERR;
                t.TroubleUnit = @"试剂条码";
                t.TroubleInfo = "条码：" + b1 + "识别失败";
                mybatis.TroubleLogSave("TroubleLogSave", t);

                return null;
            }

            reagentSettingsInfo.Barcode = b1;
            
            mybatis.DeleteAssayProject(reagentItem.ItemName, reagentSettingsInfo.ReagentType);
            DoSaveAssayPorjectPara(reagentItem, reagentSettingsInfo.ReagentType);
            DoSaveAssayProjectInfo(reagentItem, reagentSettingsInfo.ReagentType);
            mybatis.SaveResultSets(reagentItem, reagentSettingsInfo.ReagentType);
            //测试序列
            mybatis.SaveProRunSequence(reagentItem.ItemName, reagentSettingsInfo.ReagentType);

            return reagentSettingsInfo;
        }
        /// <summary>
        /// 保存生化项目参数信息,范围参数信息和校准参数信息
        /// </summary>
        /// <param name="ReagentItem"></param>
        void DoSaveAssayPorjectPara(ReagentItem ReagentItem, string reagentType)
        {
            //实例化生化项目参数实体
            AssayProjectParamInfo a = new AssayProjectParamInfo();
            a.ProjectName = ReagentItem.ItemName;
            //分析方法
            switch (ReagentItem.AnalyzeMethod)
            {
                case "1Point":
                    a.AnalysisMethod = "一点终点法";
                    break;
                case "2Point":
                    a.AnalysisMethod = "二点终点法";
                    break;
                case "Arate":
                    a.AnalysisMethod = "速率A法";
                    break;
                case "Brate":
                    a.AnalysisMethod = "速率B法";
                    break;
            }
            a.SampleType = reagentType;
            //第一测试点S
            a.MeasureLightDot1 = ReagentItem.FirstPointS;
            //第一测试点E
            a.MeasureLightDot2 = ReagentItem.FirstPointE;
            //第二测试点S
            a.MeasureLightDot3 = ReagentItem.SecondPointS;
            //第二测试点E
            a.MeasureLightDot4 = ReagentItem.SecondPointE;
            //主波长
            a.MainWaveLength = ReagentItem.MainWaveLength;
            //次波长
            a.SecWaveLength = ReagentItem.SubWaveLength;
            //单位
            a.ResultUnit = ReagentItem.Unit;
            //仪器因素
            a.InstrumentFactorA = 1;
            a.InstrumentFactorB = 0;
            //试剂1体积
            a.Reagent1VolSettings = ReagentItem.R1Vol;
            //试剂2体积
            a.Reagent2VolSettings = ReagentItem.R2Vol;
            //样本反应体积
            a.IncStosteVol = ReagentItem.IncreaseVol;
            a.IncSamVol = 0;
            a.IncDilutionVol = 0;
            a.ComStosteVol= ReagentItem.NormalVol;
            a.ComSamVol = 0;
            a.ComDilutionVol= 0;
            a.DecStosteVol = ReagentItem.DecreaseVol;
            a.DecSamVol = 0;
            a.DecDilutionVol = 0;
            a.CalibStosteVol = ReagentItem.SDTVol;
            a.CalibSamVol = 0;
            a.CalibDilutionVol = 0;
            //反应方向
            switch (ReagentItem.ReacteDirect)
            {
                case 1:
                    a.ReactionDirection = "正反应";
                    break;
                case -1:
                    a.ReactionDirection = "负反应";
                    break;
            }
            //搅拌强度
            switch (ReagentItem.Stiring1Force) // 搅拌1强度
            {
                case 1:
                    a.Stirring1Intensity = "低";
                    break;
                case 2:
                    a.Stirring1Intensity = "中";
                    break;
                case 3:
                    a.Stirring1Intensity = "高";
                    break;
            }
            switch (ReagentItem.Stiring2Force) // 搅拌2强度
            {
                case 1:
                    a.Stirring2Intensity = "低";
                    break;
                case 2:
                    a.Stirring2Intensity = "中";
                    break;  
                case 3:
                    a.Stirring2Intensity = "高";
                    break;
            }
            
            //int m = new RGTPOSManager().GetReagentMode(); //等测试后是否需要加


            a.FirstSlope = ReagentItem.LineSerumLimitMin;
            a.FirstSlopeHigh = ReagentItem.LineSerumLimitMax;
            a.ReagentBlankMinimum = ReagentItem.ReagentAbsMin;
            a.ReagentBlankMaximum = ReagentItem.ReagentAbsMax;
            a.SerumCriticalMinimum = ReagentItem.SerumPanicLimitMin;
            a.SerumCriticalMaximum = ReagentItem.SerumPanicLimitMax;

            mybatis.SaveRGSpendingAssayProjectInfo(a);

            mybatis.SaveRangeParamAndCalibParam(ReagentItem.ItemName, reagentType, ReagentItem.SDTCount);
        }
        /// <summary>
        /// 保存项目信息
        /// </summary>
        /// <param name="r"></param>
        /// <param name="proType"></param>
        private void DoSaveAssayProjectInfo(ReagentItem r, string proType)
        {
            AssayProjectInfo a = new AssayProjectInfo();
            a.ProjectName = r.ItemName;
            a.SampleType = proType;
            a.ProFullName = r.LongName;
            a.ChannelNum = r.Code.ToString();
            mybatis.SaveProjectAssayInfo(a);
        }

        /// <summary>
        /// 移除被占用的是试剂信息
        /// </summary>
        private void RemoveOccupiedReagentInfo(ReagentSettingsInfo barrgtpos)
        {
            ReagentBarcodeParam r = new ReagentBarcodeParam();
            r.Barcode = barrgtpos.Barcode;
            switch (Disk)
            {
                case 1:
                    r.ValidPercent = Convert.ToInt32(barrgtpos.ValidPercent);
                    break;
                case 2:
                    r.ValidPercent = Convert.ToInt32(barrgtpos.ValidPercent2);
                    break;

            }
            r.ExchangeDatetime = DateTime.Now;
            mybatis.InsertReagentBarcode(r);
            ReagentStateInfoR1R2 r1r2 = mybatis.SelectReagentStateForR1R2("SelectReagentStateForR1R2", barrgtpos);
            this.ReagentStateInfoHandle(r1r2, barrgtpos);
            mybatis.DeleteReagentInfo(Disk, barrgtpos.Pos);
        }
        /// <summary>
        /// 处理试剂状态R1R2表信息
        /// </summary>
        /// <param name="r"></param>
        /// <param name="reagent"></param>
        private void ReagentStateInfoHandle(ReagentStateInfoR1R2 r, ReagentSettingsInfo reagent)
        {
            if (Disk == 1)
            {
                if (string.IsNullOrEmpty(r.ReagentName2) && string.IsNullOrEmpty(r.Pos2))
                {
                    mybatis.DeletereagentStateInfoR1R2("DeletereagentStateInfoR1R2", reagent);
                }
                else
                {
                    mybatis.UpdateReagentStateForR1R2CorrespondenceR1("UpdateReagentStateForR1R2CorrespondenceR1", reagent);
                }
            }
            else if (Disk == 2)
            {
                if (string.IsNullOrEmpty(r.ReagentName) && string.IsNullOrEmpty(r.Pos))
                {
                    mybatis.DeletereagentStateInfoR1R2("DeletereagentStateInfoR1R2", reagent);
                }
                else
                {
                    mybatis.UpdateReagentStateForR1R2("UpdateReagentStateForR1R2", reagent);
                }
            }
        }

        /// <summary>
        /// 条码扫码失败
        /// </summary>
        /// <param name="disk"></param>
        /// <param name="pos"></param>
        public void BarcodeScanningFailed(int disk, string pos)
        {
            mybatis.DeleteReagentInfo(disk, pos);
            ReagentStateInfoR1R2 r = mybatis.GetReagentStateInfoR1R2(disk, pos);
            this.ReagentStateInfoHandle(r, new ReagentSettingsInfo() { ProjectName = r.ProjectName});
        }
    }
}
