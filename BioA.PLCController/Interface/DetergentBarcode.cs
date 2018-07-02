using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using CLMode.Service;
using CLMode.Entities;

namespace CLMode.Interface
{
    public class BarcodeData
    {
        public string bar;
        public int type;
    }
    public class DetergentBarcode : IDetergentBarcode
    {
        [DllImport(@"D:\NT-1000\BcDecry.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long BarCodeDecryption(char[] cpSrcStr, StringBuilder buf);

        public object Process(object barcode)
        {
            BarcodeData data = barcode as BarcodeData;
            string str = data.bar;

            LogService.Log(str, LogType.Debug, "debarcode.lg");

            char[] value = str.ToCharArray();

            StringBuilder buf = new StringBuilder(18);

            long i = 0;
            try
            {
                LogService.Log(value.Count().ToString(), LogType.Debug, "debarcode.lg");

                i = BarCodeDecryption(value, buf);

                LogService.Log("i="+i, LogType.Debug, "debarcode.lg");

                str = buf.ToString().Substring(0, 18);

                LogService.Log("str=" + i, LogType.Debug, "debarcode.lg");
            }
            catch(Exception e)
            {
                LogService.Log("DetergentBarcode:"+e.Message, LogType.Debug, "debarcode.lg");
                return 1;//条码识别失败，确认输入的条码是否正确。
            }

            string timestr = str.Substring(0, 6);
            DateTime expireDatetime = DateTime.Now;
            try
            {
                expireDatetime = DateTime.ParseExact("20" + timestr, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            }
            catch
            {
                expireDatetime = DateTime.Now;
            }
            if (expireDatetime < DateTime.Now)
            {
                return 3;//清洗剂已经过期。
            }
            

            string volstr = str.Substring(6,3);
            int vol = 0;
            try
            {
                vol = int.Parse(volstr);
            }
            catch
            {
                vol = 0;
            }
            if (vol <= 0 || vol > 999)
            {
                return 2;//容量识别失败。
            }

            

            string countstr = str.Substring(9, 2);
            int count = 0;
            try
            {
                count = int.Parse(countstr);
            }
            catch
            {
                count = 0;
            }
            if (count <= 0 || count>99)
            {
                return 4;//该清洗剂不能与该机型适配。
            }

            string style = str.Substring(11, 2);
            switch (style)
            {
                case "11":
                    if (data.type == 1)
                    {
                        if (new DetergentVolService().GetABarcode() == data.bar)
                        {
                            return 6;
                        }
                    }
                    else
                    {
                        return 5;
                    }
                    break;
                case "23":
                    if (data.type == 2)
                    {
                        if (new DetergentVolService().GetBBarcode() == data.bar)
                        {
                            return 6;
                        }
                    }
                    else
                    {
                        return 5;
                    }
                    break;
            }

            //交换操作
            DetergentBar OlddetergentBar = new DetergentBar();

            DetergentInfo detergentInfo = new DetergentInfoService().GetDetergentInfo();

            DetergentBar detergentBar = new DetergentBarService().GetLastestBarcode(data.bar);
            if (detergentBar == null)
            {
                switch (style)
                {
                    case "11": //A 50倍稀释
                        vol = vol * 50;
                        OlddetergentBar.Vol = new DetergentVolService().GetDetergentACount();
                        new DetergentVolService().UpdateDetergentACount(vol * count);
                        detergentInfo.DetergentAFullCount = vol * count;
                        new DetergentInfoService().UpdateDetergentInfo(detergentInfo);
                        OlddetergentBar.Barcode = new DetergentVolService().GetABarcode();
                        new DetergentVolService().UpdateABarcode(data.bar);
                        break;
                    case "23": //B 50倍稀释
                        vol = vol * 50;
                        OlddetergentBar.Vol = new DetergentVolService().GetDetergentBCount();
                        new DetergentVolService().UpdateDetergentBCount(vol * count);
                        detergentInfo.DetergentBFullCount = vol * count;
                        new DetergentInfoService().UpdateDetergentInfo(detergentInfo);
                        OlddetergentBar.Barcode = new DetergentVolService().GetBBarcode();
                        new DetergentVolService().UpdateBBarcode(data.bar);
                        break;
                }
            }
            else
            {
                switch (style)
                {
                    case "11": //A
                        OlddetergentBar.Vol = new DetergentVolService().GetDetergentACount();
                        new DetergentVolService().UpdateDetergentACount(detergentBar.Vol);
                        OlddetergentBar.Barcode = new DetergentVolService().GetABarcode();
                        new DetergentVolService().UpdateABarcode(detergentBar.Barcode);
                        break;
                    case "23": //B
                        OlddetergentBar.Vol = new DetergentVolService().GetDetergentBCount();
                        new DetergentVolService().UpdateDetergentBCount(detergentBar.Vol);
                        OlddetergentBar.Barcode = new DetergentVolService().GetBBarcode();
                        new DetergentVolService().UpdateBBarcode(detergentBar.Barcode);
                        break;
                }
            }

            OlddetergentBar.ExchangeDatetime = DateTime.Now;
            if (string.IsNullOrEmpty(OlddetergentBar.Barcode) || string.IsNullOrWhiteSpace(OlddetergentBar.Barcode))
            {
            }
            else
            {
                new DetergentBarService().InsertBarcode(OlddetergentBar);
            }

            return 0;//加载成功
        }
    }
}
