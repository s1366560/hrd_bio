using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Entities;
using CLMode.Protocol;

namespace CLMode.Interface
{
    //ISE模块分析参数设置编码器
    public class EncodeAB1 : IEncode
    {
        const byte BLKVALUE = (byte)(' ');
        public byte[] Encode(object o)
        {
            ISECalParaSet ISECalParaSet = o as ISECalParaSet;

            List<byte> byteList = new List<byte>();

            byteList.Add(0x02);
            byteList.Add(0xAB);
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    byteList.Add(0x31);
                    byteList.Add(0x31);
                    break;
                case "U": 
                    byteList.Add(0x32);
                    byteList.Add(0x32);
                    break;
            }
            byteList.Add(0x34);

            //Na-----------------------------------------------------------------

            //G 
            byte[] v = ConvertStringTo7Bits("1.4");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //H
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    v = ConvertStringTo7Bits("160.0");
                    break;
                case "U":
                    v = ConvertStringTo7Bits("200.0");
                    break;
            }
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //I
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    v = ConvertStringTo7Bits("130.0");
                    break;
                case "U":
                    v = ConvertStringTo7Bits("50.0");
                    break;
            }
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //J
            v = ConvertFloatTo7Bits(ISECalParaSet.NaHSTDMaxValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //K
            v = ConvertFloatTo7Bits(ISECalParaSet.NaLSTDMaxValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //L
            v = ConvertFloatTo7Bits(ISECalParaSet.NaHSTDMinValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //M
            v = ConvertFloatTo7Bits(ISECalParaSet.NaLSTDMinValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //N
            v = ConvertStringTo7Bits("10.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //O
            v = ConvertStringTo7Bits("38.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //P
            v = ConvertStringTo7Bits("65.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //Q
            v = ConvertStringTo7Bits("1.00");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //R
            v = ConvertFloatTo7Bits(ISECalParaSet.NaHSTD);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //T
            v = ConvertFloatTo7Bits(ISECalParaSet.NaLSTD);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //预留位
            ////U
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////V
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////W
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////X
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////Y
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////Z
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}

            //K-------------------------------------------------------

            //G 
            v = ConvertStringTo7Bits("1.3");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //H
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    v = ConvertStringTo7Bits("6.0");
                    break;
                case "U":
                    v = ConvertStringTo7Bits("100.0");
                    break;
            }
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //I
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    v = ConvertStringTo7Bits("3.5");
                    break;
                case "U":
                    v = ConvertStringTo7Bits("10.0");
                    break;
            }
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //J
            v = ConvertFloatTo7Bits(ISECalParaSet.KHSTDMaxValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //K
            v = ConvertFloatTo7Bits(ISECalParaSet.KLSTDMaxValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //L
            v = ConvertFloatTo7Bits(ISECalParaSet.KHSTDMinValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //M
            v = ConvertFloatTo7Bits(ISECalParaSet.KLSTDMinValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //N
            v = ConvertStringTo7Bits("10.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //O
            v = ConvertStringTo7Bits("38.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //P
            v = ConvertStringTo7Bits("65.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //Q
            v = ConvertStringTo7Bits("0.05");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //R
            v = ConvertFloatTo7Bits(ISECalParaSet.KHSTD);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //T
            v = ConvertFloatTo7Bits(ISECalParaSet.KLSTD);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //预留位
            ////U
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////V
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////W
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////X
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////Y
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////Z
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}

            //CL------------------------------------------------------

            //G 
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    v = ConvertStringTo7Bits("0.0");
                    break;
                case "U":
                    v = ConvertStringTo7Bits("-0.1");
                    break;
            }
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //H
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    v = ConvertStringTo7Bits("120.0");
                    break;
                case "U":
                    v = ConvertStringTo7Bits("180.0");
                    break;
            }
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //I
            switch (ISECalParaSet.SMPType)
            {
                case "S":
                    v = ConvertStringTo7Bits("85.0");
                    break;
                case "U":
                    v = ConvertStringTo7Bits("50.0");
                    break;
            }
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //J
            v = ConvertFloatTo7Bits(ISECalParaSet.ClHSTDMaxValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //K
            v = ConvertFloatTo7Bits(ISECalParaSet.ClLSTDMaxValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //L
            v = ConvertFloatTo7Bits(ISECalParaSet.ClHSTDMinValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //M
            v = ConvertFloatTo7Bits(ISECalParaSet.ClLSTDMinValue);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //N
            v = ConvertStringTo7Bits("5.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //O
            v = ConvertStringTo7Bits("-65.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //P
            v = ConvertStringTo7Bits("-38.0");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //Q
            v = ConvertStringTo7Bits("1.00");
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //R
            v = ConvertFloatTo7Bits(ISECalParaSet.ClHSTD);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //T
            v = ConvertFloatTo7Bits(ISECalParaSet.ClLSTD);
            for (int i = 0; i < v.Count(); i++)
            {
                byteList.Add(v[i]);
            }
            //预留位
            ////U
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////V
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////W
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////X
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////Y
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            ////Z
            //v = ConvertStringTo7Bits("0.0");
            //for (int i = 0; i < v.Count(); i++)
            //{
            //    byteList.Add(v[i]);
            //}
            //----------------------------------------------------------------------------------------------
            byteList.Add(0x03);

            long sum = 0;
            byte[] Tbytes = new byte[byteList.Count + 2];
            for (int i = 0; i < byteList.Count; i++)
            {
                Tbytes[i] = byteList.ElementAt(i);
                sum += (int)byteList.ElementAt(i);
            }
            byte[] Tchecksum = MachineControlProtocol.CheckSum(sum);

            Tbytes[Tbytes.Length - 2] = Tchecksum[0];
            Tbytes[Tbytes.Length - 1] = Tchecksum[1];

            return Tbytes;

        }
        List<byte> SerumValue(ISECalParaSet ISECalParaSet)
        {
            List<byte> byteList = new List<byte>();
            return byteList;
        }
        //浮点字符串数据 eg 12.44 1.66 170.55
        byte[] ConvertStringTo7Bits(string vstr)
        {
            byte[] datas = new byte[7];

            int strcount = vstr.Length;
            int offcount = 7 - strcount;
            if (offcount > 0)
            {
                for (int i = 0; i < offcount; i++)
                {
                    datas[i] = BLKVALUE;
                }
                for (int i = offcount,j=0; i < 7; i++,j++)
                {
                    datas[i] = (byte)vstr[j];
                }
            }

            return datas;
        }

        byte[] ConvertFloatTo7Bits(float v)
        {
            return ConvertStringTo7Bits(v.ToString("#0.0"));
        }
    }
}
