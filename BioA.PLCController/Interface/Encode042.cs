using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BioA.Common.Machine;
using BioA.PLCController;
using BioA.Common;

namespace BioA.PLCController.Interface
{
    //NT-1000任务编码器
    public class Encode042 : IEncode
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
                return TaskEncode(T.T, T.WN);
            }
        }

        byte[] TaskEncode(TASK t, int wn)
        {
            List<byte> Listbyte = new List<byte>();
            if (t.V == 0)
            {
                Listbyte.Add(0x02);
                Listbyte.Add(0x07);
                //普通测试包
                //if (t.ASSAYTYPE == "ISE")
                //{
                //    if (t.SMPTYPE == "U")
                //    {
                //        Listbyte.Add(0x31);
                //        Listbyte.Add(0x32);
                //        Listbyte.Add(0x31);
                //    }
                //    else
                //    {
                //        Listbyte.Add(0x31);
                //        Listbyte.Add(0x31);
                //        Listbyte.Add(0x31);
                //    }
                //}
                //else
                //{
                Listbyte.Add(0x30);
                Listbyte.Add(0x30);
                Listbyte.Add(0x31);
                //}


                int[] bytes = MachineControlProtocol.HDecConverToHex(wn);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + GetWaveLengthIndex(t.PW)));
                Listbyte.Add((byte)('0' + GetWaveLengthIndex(t.SW)));

                bytes = MachineControlProtocol.DecConverToHex(t.PPNO);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
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

                //int rgttype = new RunService().GetRgtPanelType();
                //if (rgttype == 2)
                //{
                //    t.R1POS += 45;
                //}
                bytes = MachineControlProtocol.DecConverToHex(t.R1POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                bytes = MachineControlProtocol.HDecConverToHex(t.R1VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                Listbyte.Add((byte)('0' + t.SF1));

                bytes = MachineControlProtocol.DecConverToHex(t.R2POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                if (t.R2POS == 0)
                {
                    bytes = MachineControlProtocol.HDecConverToHex(0);
                    Listbyte.Add((byte)bytes[0]);
                    Listbyte.Add((byte)bytes[1]);
                    Listbyte.Add((byte)bytes[2]);
                    Listbyte.Add((byte)('0' + 0));
                }
                else
                {
                    bytes = MachineControlProtocol.HDecConverToHex(t.R2VOL);
                    Listbyte.Add((byte)bytes[0]);
                    Listbyte.Add((byte)bytes[1]);
                    Listbyte.Add((byte)bytes[2]);
                    Listbyte.Add((byte)('0' + t.SF2));
                }
                Listbyte.Add(0x03);
            }
            else
            {
                Listbyte.Add(0x02);
                Listbyte.Add(0x07);
                //稀释测试包
                Listbyte.Add(0x30);
                Listbyte.Add(0x30);
                Listbyte.Add(0x32);

                int[] bytes = MachineControlProtocol.HDecConverToHex(wn);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + GetWaveLengthIndex(t.PW)));
                Listbyte.Add((byte)('0' + GetWaveLengthIndex(t.SW)));

                bytes = MachineControlProtocol.DecConverToHex(t.PPNO);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
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
                Listbyte.Add((byte)('0' + t.SF1));
                bytes = MachineControlProtocol.DecConverToHex(0);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(0);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                Listbyte.Add((byte)('0' + t.SF2));
                //---------------------------------------------------------------
                bytes = MachineControlProtocol.HDecConverToHex(wn);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);

                Listbyte.Add((byte)('0' + GetWaveLengthIndex(t.PW)));
                Listbyte.Add((byte)('0' + GetWaveLengthIndex(t.SW)));

                bytes = MachineControlProtocol.DecConverToHex(t.PPNO);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);

                Listbyte.Add(0x3A);
                Listbyte.Add(0x31);
                bytes = MachineControlProtocol.DecConverToHex(int.Parse(t.SMPPOS.TrimStart('B', 'S', 'C', 'E')));
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(t.V);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                Listbyte.Add(0x31);

                //int rgttype = new RunService().GetRgtPanelType();
                //if (rgttype == 2)
                //{
                //    t.R1POS += 45;
                //}
                bytes = MachineControlProtocol.DecConverToHex(t.R1POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(t.R1VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                Listbyte.Add((byte)('0' + t.SF1));
                bytes = MachineControlProtocol.DecConverToHex(t.R2POS);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                bytes = MachineControlProtocol.HDecConverToHex(t.R2VOL);
                Listbyte.Add((byte)bytes[0]);
                Listbyte.Add((byte)bytes[1]);
                Listbyte.Add((byte)bytes[2]);
                Listbyte.Add((byte)('0' + t.SF2));
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
        int GetWaveLengthIndex(int w)
        {
            for (int i = 0; i < RunConfigureUtility.WaveLengthList.Count(); i++)
            {
                if (w == System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[i]))
                {
                    return i;
                }
            }
            return 0;
        }
        byte[] NullScheduleEncode()
        {
            List<byte> BList = new List<byte>();

            BList.Add(0x02);
            BList.Add(0x07);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x30);
            BList.Add(0x03);
            BList.Add(0x00);
            BList.Add(0x00);

            byte[] bytes = new byte[BList.Count];
            for (int i = 0; i < BList.Count; i++)
            {
                bytes[i] = BList[i];
            }
            byte[] checksum = MachineControlProtocol.CheckSum(bytes);
            bytes[BList.Count - 2] = checksum[0];
            bytes[BList.Count - 1] = checksum[1];
            return bytes;
        }
    }
}
