using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Entities;
using CLMode.Service;
using CLMode.Protocol;

namespace CLMode.Interface
{
    //ISE模块校准参数设置编码器
    public class EncodeA90 : IEncode
    {
        const byte BLKVALUE = (byte)(' ');
        public byte[] Encode(object o)
        {
            //定标品类型 血清 尿液
            string SMPType = o as string;

            List<byte> byteList = new List<byte>();
            byteList.Add(0x02);
            byteList.Add(0xA9);
            switch (SMPType)
            {
                case "S":
                    byteList.Add(0x31);
                    break;
                case "U":
                    byteList.Add(0x32);
                    break;
            }
            //22.0
            //byteList.Add(0x32);
            //byteList.Add(0x32);
            //byteList.Add(0x2E);
            //byteList.Add(0x30);

            //byte[] v;
            //ISEItemSDTTable ISEItemSDTTable = null;
            //switch (SMPType)
            //{
            //    case "S":
            //        ISEItemSDTTable = new ISEItemSDTTableService().GetUsingISEItemSDTTable("S");
            //        break;
            //    case "U":
            //        ISEItemSDTTable = new ISEItemSDTTableService().GetUsingISEItemSDTTable("U");
            //        break;
            //}
            ////Na----------------------------------------------------------
            //if (ISEItemSDTTable != null)
            //{
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.NaHighSampleValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.NaHighSampleBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }

            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.NaLowSampleValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.NaLowSampleBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }

            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.NaSlope);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.NaDilRate);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    //K----------------------------------------------------------------
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.KHighSampleValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.KHighSampleBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }

            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.KLowSampleValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.KLowSampleBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }

            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.KSlope);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.KDilRate);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    //Cl----------------------------------------------------------------
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.ClHighSampleValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.ClHighSampleBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }

            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.ClLowSampleValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.ClLowSampleBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }

            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.ClSlope);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.ClDilRate);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    //------------------------------------------------
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.HighThValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.HighThBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.LowThValue);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //    v = ConvertFloatTo9Bits(ISEItemSDTTable.LowThBase);
            //    for (int i = 0; i < v.Count(); i++)
            //    {
            //        byteList.Add(v[i]);
            //    }
            //}
            //else
            //{
            //    for (int c = 1; c <= 24; c++)
            //    {
            //        v = ConvertStringTo9Bits("0.000");
            //        for (int i = 0; i < v.Count(); i++)
            //        {
            //            byteList.Add(v[i]);
            //        }
            //    }
            //}

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

        //9.000
        byte[] ConvertStringTo9Bits(string vstr)
        {
            byte[] datas = new byte[9];

            int strcount = vstr.Length;
            int offcount = 9 - strcount;
            if (offcount > 0)
            {
                for (int i = 0; i < offcount; i++)
                {
                    datas[i] = BLKVALUE;
                }
                for (int i = offcount, j = 0; i < 9; i++, j++)
                {
                    datas[i] = (byte)vstr[j];
                }
            }

            return datas;
        }
        byte[] ConvertFloatTo9Bits(float v)
        {
            return ConvertStringTo9Bits(v.ToString("#0.000"));
        }
    }
}
