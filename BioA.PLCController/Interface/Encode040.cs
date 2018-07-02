using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Machine;
using CLMode.Protocol;
using System.IO;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Encode040
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Encode040
 * 创建时间：       4/26/2012 8:41:57 AM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace CLMode.Interface
{
    public class Encode040 : IEncode
    {
        public byte[] Encode(object o)
        {
            ScheduleTASK T = o as ScheduleTASK;
            if (T == null)
            {
                return NullScheduleEncode();
            }
            else
            {
                return TaskEncode(T.T,T.WN);
            }
        }
        byte[] TaskEncode(TASK t, int wn)
        {
            List<byte> Listbyte = new List<byte>();
            if (t.V == 0)
            {
                Listbyte.Add(0x02);
                Listbyte.Add(0x07);
                Listbyte.Add(0x30);
                Listbyte.Add(0x30);
                Listbyte.Add(0x31);
                int[] bytes = MachineControlProtocol.HDecConverToHex(wn);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + MachineInfo.GetWaveLengthIndex(t.PW)));
                Listbyte.Add((byte)('0' + MachineInfo.GetWaveLengthIndex(t.SW)));

                //急诊和常规都在样本位取样，急诊意思就是队列优先
                if (t.PT == 1)
                {
                    t.PT = 0;
                }
                Listbyte.Add((byte)('0' + t.PT));
                Listbyte.Add(0x31);
                bytes = MachineControlProtocol.DecConverToHex(int.Parse(t.SMPPOS.TrimStart('B', 'S', 'C', 'E')));
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                bytes = MachineControlProtocol.HDecConverToHex(t.PV);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + t.CT));

                bytes = MachineControlProtocol.DecConverToHex(t.R1POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                bytes = MachineControlProtocol.HDecConverToHex(t.R1VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                bytes = MachineControlProtocol.DecConverToHex(t.R2POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                bytes = MachineControlProtocol.HDecConverToHex(t.R2VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add(0x03);
            }
            else
            {
                Listbyte.Add(0x02);
                Listbyte.Add(0x07);
                Listbyte.Add(0x30);
                Listbyte.Add(0x30);
                Listbyte.Add(0x32);

                int[] bytes = MachineControlProtocol.HDecConverToHex(wn);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + MachineInfo.GetWaveLengthIndex(t.PW)));
                Listbyte.Add((byte)('0' + MachineInfo.GetWaveLengthIndex(t.SW)));

                //急诊和常规都在样本位取样，急诊意思就是队列优先
                if (t.PT == 1)
                {
                    t.PT = 0;
                }
                Listbyte.Add((byte)('0' + t.PT));
                Listbyte.Add(0x31);
                bytes = MachineControlProtocol.DecConverToHex(int.Parse(t.SMPPOS.TrimStart('B', 'S', 'C', 'E')));
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                bytes = MachineControlProtocol.HDecConverToHex(t.PV);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + t.CT));

                bytes = MachineControlProtocol.DecConverToHex(t.DPOS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                bytes = MachineControlProtocol.HDecConverToHex(t.DV);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                bytes = MachineControlProtocol.DecConverToHex(t.R2POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(t.R2VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                //---------------------------------------------------------------
                bytes = MachineControlProtocol.HDecConverToHex(wn);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + MachineInfo.GetWaveLengthIndex(t.PW)));
                Listbyte.Add((byte)('0' + MachineInfo.GetWaveLengthIndex(t.SW)));

                Listbyte.Add(0x0A);
                Listbyte.Add(0x31);
                bytes = MachineControlProtocol.DecConverToHex(int.Parse(t.SMPPOS.TrimStart('B', 'S', 'C', 'E')));
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(t.V);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                Listbyte.Add(0x01);
                bytes = MachineControlProtocol.DecConverToHex(t.R1POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(t.R1VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                bytes = MachineControlProtocol.DecConverToHex(t.R2POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(t.R2VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                Listbyte.Add(0x03);
            }
            long sum = 0;
            byte[] Tbytes = new byte[Listbyte.Count + 2];
            for (int i = 0; i < Listbyte.Count; i++)
            {
                Tbytes[i] = Listbyte.ElementAt(i);
                sum += (int)Listbyte.ElementAt(i);
            }
            byte[] Tchecksum = MachineControlProtocol.CheckSum(sum);

            Tbytes[Tbytes.Length - 2] = Tchecksum[0];
            Tbytes[Tbytes.Length - 1] = Tchecksum[1];

            return Tbytes;
        }
        byte[] NullScheduleEncode()
        {
            byte[] bytes = new byte[31];
            bytes[0] = 0x02;
            bytes[1] = 0x07;
            bytes[2] = 0x30;
            bytes[3] = 0x30;
            bytes[4] = 0x30;
            bytes[5] = 0x30;
            bytes[6] = 0x30;
            bytes[7] = 0x30;
            bytes[8] = 0x30;
            bytes[9] = 0x30;
            bytes[10] = 0x30;
            bytes[11] = 0x30;
            bytes[12] = 0x30;
            bytes[13] = 0x30;
            bytes[14] = 0x30;
            bytes[15] = 0x30;
            bytes[16] = 0x30;
            bytes[17] = 0x30;
            bytes[18] = 0x30;
            bytes[19] = 0x30;
            bytes[20] = 0x30;
            bytes[21] = 0x30;
            bytes[22] = 0x30;
            bytes[23] = 0x30;
            bytes[24] = 0x30;
            bytes[25] = 0x30;
            bytes[26] = 0x30;
            bytes[27] = 0x30;
            bytes[28] = 0x03;
            bytes[29] = 0x45;
            bytes[30] = 0x43;

            return bytes;
        }
    }
}
