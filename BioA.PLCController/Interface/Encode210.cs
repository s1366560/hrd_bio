using BioA.PLCController.Interface;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BioA.Common;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Encode210
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Encode210
 * 创建时间：       4/26/2012 12:35:09 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace BioA.PLCController.Interface
{
    public class Encode210 : IEncode
    {
        MyBatis myBatis = new MyBatis();
        public byte[] Encode(object o)
        {
            int i = (int)o;
            if (i == 0x23)
                return new byte[] { 0x2, 0x23, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 
                    0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x3, 0x41, 0x38 };

            return AvoidPolluteTable((byte)i);
        }

        public byte[] AvoidPolluteTable(byte c)
        {
            List<byte> lbytes = new List<byte>();

            lbytes.Add(0x02);
            lbytes.Add(c);

            List<ReagentNeedleAntifoulingStrategyInfo> CLItems = GetR1CrossContamination();
            switch (c)
            {
                case 0x21: CLItems = GetR1CrossContamination(); break;
                case 0x22: CLItems = GetR2CrossContamination(); break;
            }
            foreach (ReagentNeedleAntifoulingStrategyInfo e in CLItems)
            {

                int px = myBatis.QueryRunSequenceByProject(e.PolluteProName, e.PolluteProType);
                int[] p = MachineControlProtocol.DecConverToHex(px);
                lbytes.Add((byte)p[0]);
                lbytes.Add((byte)p[1]);

                int nx = myBatis.QueryRunSequenceByProject(e.BePollutedProName, e.BePollutedProType);
                int[] n = MachineControlProtocol.DecConverToHex(nx);
                lbytes.Add((byte)n[0]);
                lbytes.Add((byte)n[1]);
            }
            if (lbytes.Count < 82)
            {
                for (int i = 0; i < 82 - lbytes.Count; i++)
                {
                    lbytes.Add(0x30);
                }
            }
            lbytes.Add(0x03);
            lbytes.Add(0x00);
            lbytes.Add(0x00);

            byte[] ds = new byte[lbytes.Count];//85
            for (int i = 0; i < lbytes.Count; i++)
            {
                ds[i] = lbytes[i];
            }
            byte[] checksum = MachineControlProtocol.CheckSum(ds);
            ds[lbytes.Count - 2] = checksum[0];
            ds[lbytes.Count - 1] = checksum[1];

            return ds;
        }

        public List<ReagentNeedleAntifoulingStrategyInfo> GetR1CrossContamination()
        {
            List<ReagentNeedleAntifoulingStrategyInfo> all = new List<ReagentNeedleAntifoulingStrategyInfo>();

            
            all = myBatis.QueryReagentNeedleByR1R2("QueryReagentNeedleByR1R2", "R1");

            return all;
        }

        public List<ReagentNeedleAntifoulingStrategyInfo> GetR2CrossContamination()
        {
            List<ReagentNeedleAntifoulingStrategyInfo> all = new List<ReagentNeedleAntifoulingStrategyInfo>();

            all = myBatis.QueryReagentNeedleByR1R2("QueryReagentNeedleByR1R2", "R2");

            return all;
        }
    }
}
