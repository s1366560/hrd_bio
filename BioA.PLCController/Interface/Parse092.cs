using BioA.Common.Machine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //SN解码器
    public class Parse092 : IParse
    {
        public string Parse(List<byte> Data)
        {
            string snsrt = "";
            for (int i = 3; i < 3 + 8; i++)
            {
                snsrt += (char)Data[i];
            }
            string mstr = "";
            mstr += (char)Data[11];

            int n1  = MachineControlProtocol.HexConverToDec(Data[12], Data[13]);
            int n2 = MachineControlProtocol.HexConverToDec(Data[14], Data[15]);

            string str2 = "";
            str2 += (char)Data[16];

            return snsrt + "|" + mstr + "|" + n1 + "|" + n2 + "|" + str2;
        }
    }
}
