using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Entities;
using CLMode.Protocol;
using CLMode.Service;
using CLMode.Land;

namespace CLMode.Interface
{
    public class ParseAD0 : IParse
    {
        byte[] GetData(List<byte> data, int s, int l)
        {
            byte[] v = new byte[l];
            for (int i = 0, p = s; i < l; i++, p++)
            {
                v[i] = data[p];
            }
            return v;
        }
        
        public string Parse(List<byte> Data)
        {
            ISECalibratedResult ISECalibratedResult = new ISECalibratedResult();

            int sate = MachineControlProtocol.HexConverToDec(Data[2], Data[3]);

            TroubleLog isestatetrouble = new TroubleLog();
            isestatetrouble.TroubleCode = @"ISE00" + sate.ToString("#00");
            isestatetrouble.TroubleUnit = @"ISE";
            if (sate <= 50)
            {
                isestatetrouble.TroubleType = TROUBLETYPE.WARN;
            }
            else
            {
                isestatetrouble.TroubleType = TROUBLETYPE.ERR;
            }
            new TroubleLogService().Save(isestatetrouble);

            if (sate == 73)
            {
                ISEItemSDTTable ISEItemSDTTable = new ISEItemSDTTableService().GetNewISEItemSDTTable();
                if (ISEItemSDTTable != null)
                {
                    ISEItemSDTTable.RunLog = MyResources.Instance.FindResource("ParseA905").ToString();
                    ISEItemSDTTable.State = "failed";
                    ISEItemSDTTable.IsUsed = false;
                    new ISEItemSDTTableService().DeleteNewISEItemSDTTable();
                    new ISEItemSDTTableService().Save(ISEItemSDTTable);
                }
            }

            ISECalibratedResult.ResultState = sate.ToString();

            int isecode = MachineControlProtocol.HexConverToDec(Data[4], Data[5], Data[6]);
            ISECalibratedResult.CalibratePosition = isecode.ToString();

            int index = 7;
            ISECalibratedResult.NaE = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.NaF = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.NaG = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));

            index += 9;
            ISECalibratedResult.KE = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.KF = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.KG = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));

            index += 9;
            ISECalibratedResult.ClE = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.ClF = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.ClG = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));

            index += 9;
            ISECalibratedResult.Th1H = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.Th1I = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));

            index += 9;
            ISECalibratedResult.Th2H = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));
            index += 9;
            ISECalibratedResult.Th2I = MachineControlProtocol.ConvertArrayToString(GetData(Data, index, 9));

            ISECalibratedResult.CalibrateCode = new RunService().GetISECalibrateCode()-1;

            new ISECalibratedResultService().Save(ISECalibratedResult);

            return null;
        }
    }
}
