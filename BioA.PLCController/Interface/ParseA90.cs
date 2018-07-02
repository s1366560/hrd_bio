using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Protocol;
using CLMode.Entities;
using CLMode.Service;
using CLMode.Land;

namespace CLMode.Interface
{
    //ISE校准数据解析
    public class ParseA90 : IParse
    {
        public string Parse(List<byte> data)
        {
            ISEItemSDTTable ISEItemSDTTable = new ISEItemSDTTableService().GetNewISEItemSDTTable();
            if (ISEItemSDTTable == null)
            {
                return null;
            }

            int index = 5;

            ISEItemSDTTable.NaHighSampleValue = MachineControlProtocol.HexConverToFloat(GetData(data,index,9));
            index += 9;
            ISEItemSDTTable.NaHighSampleBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.NaLowSampleValue = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.NaLowSampleBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.NaSlope = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.NaDilRate = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.KHighSampleValue = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.KHighSampleBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.KLowSampleValue = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.KLowSampleBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.KSlope = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.KDilRate = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.ClHighSampleValue = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.ClHighSampleBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.ClLowSampleValue = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.ClLowSampleBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.ClSlope = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.ClDilRate = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.HighThValue = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.HighThBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.LowThValue = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));
            index += 9;
            ISEItemSDTTable.LowThBase = MachineControlProtocol.HexConverToFloat(GetData(data, index, 9));

            int s = MachineControlProtocol.HexConverToDec(data[2], data[3]);
            switch (s)
            {
                case 01: ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA901").ToString(); break;
                case 97: ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA902").ToString(); break;
                case 94: ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA903").ToString(); break;
                case 79: ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA904").ToString(); break;
                case 78: ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA905").ToString(); break;
                case 77: ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA905").ToString(); break;
                case 76: ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA905").ToString(); break;
            }
            new ISEItemSDTTableService().DeleteNewISEItemSDTTable();
            if (s == 1)
            {
                ISEItemSDTTable.State = "success";
                new ISEItemSDTTableService().Save(ISEItemSDTTable);
                new ISEItemSDTTableService().SetISEItemSDTTableUsing(ISEItemSDTTable);
            }

            TroubleLog isestatetrouble = new TroubleLog();
            isestatetrouble.TroubleCode = @"ISE00" + s.ToString("#00");
            isestatetrouble.TroubleUnit = @"ISE";
            if (s <= 50)
            {
                isestatetrouble.TroubleType = TROUBLETYPE.WARN;
            }
            else
            {
                isestatetrouble.TroubleType = TROUBLETYPE.ERR;
            }
            new TroubleLogService().Save(isestatetrouble);


            if (Math.Abs(ISEItemSDTTable.ClSlope) < 38 || Math.Abs(ISEItemSDTTable.ClSlope) > 65)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleCode = @"ISE0000";
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = @"ISE";
                trouble.TroubleInfo = MyResources.Instance.FindResource("ParseA906").ToString();
                new TroubleLogService().Save(trouble);
            }
            
            return null;
        }
        byte[] GetData(List<byte> data,int s,int l)
        {
            byte[] v = new byte[l];

            for (int i = 0,p=s; i < l; i++,p++)
            {
                v[i] = data[p];
            }
            return v;
        }
    }
}
