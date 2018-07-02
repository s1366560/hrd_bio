using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    public class Parse0E0 : IParse
    {
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> data)
        {
            myBatis.UpdateLatestWaterState("UpdateLatestWaterState", new byte[]{ data[2], data[3]});
            ProcessWaterStateA(data[2], data[3]);

            int t = MachineControlProtocol.HexConverToDec(data[4], data[5], data[6]);
            float tf = t * 0.1f;
            myBatis.UpdateLatestCUVPanelTemperature("UpdateLatestCUVPanelTemperature", tf);

            string temstr = tf.ToString("#0.0");
            if (temstr == "0.0")
            {
                temstr = "21.0";
            }

            return temstr;
        }

        public void ProcessWaterStateA(int state1, int state2)
        {
            //高位状态码
            int s1 = state1 - 0x30;

            //纯水槽低位浮球
            if ((s1 & 0x01) == 0x01)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = MachineReturnState.TroubleUnit1;
                trouble.TroubleCode = @"10001";
                trouble.TroubleInfo = MachineReturnState.RunService1;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //反应槽液位
            if ((s1 & 0x02) == 0x02)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = MachineReturnState.TroubleUnit1;
                trouble.TroubleCode = @"10002";
                trouble.TroubleInfo = MachineReturnState.RunService2;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //溢流罐液位报警
            if ((s1 & 0x04) == 0x04)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = MachineReturnState.TroubleUnit1;
                trouble.TroubleCode = @"10003";
                trouble.TroubleInfo = MachineReturnState.RunService3;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //真空罐液位报警
            if ((s1 & 0x08) == 0x08)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = MachineReturnState.TroubleUnit1;
                trouble.TroubleCode = @"10004";
                trouble.TroubleInfo = MachineReturnState.RunService4;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //低位状态码
            int s2 = state2 - 0x30;
            //恒温值有问题 OK
            if ((s2 & 0x01) == 0x01)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.WARN;
                trouble.TroubleUnit = MachineReturnState.TroubleUnit1;
                trouble.TroubleCode = @"10005";
                trouble.TroubleInfo = MachineReturnState.RunService5;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //恒温槽浮球错误
            if ((s2 & 0x04) == 0x04)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = MachineReturnState.TroubleUnit1;
                trouble.TroubleCode = @"10007";
                trouble.TroubleInfo = MachineReturnState.RunService6;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }

            //纯水槽高位浮球
            if ((s2 & 0x08) == 0x08)
            {
                TroubleLog trouble = new TroubleLog();
                trouble.TroubleType = TROUBLETYPE.ERR;
                trouble.TroubleUnit = MachineReturnState.TroubleUnit1;
                trouble.TroubleCode = @"10008";
                trouble.TroubleInfo = MachineReturnState.RunService7;
                myBatis.TroubleLogSave("TroubleLogSave", trouble);
            }
        }
    }
}
