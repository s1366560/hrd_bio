using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Entities;
using CLMode.Protocol;
using CLMode.Service;
using CL.Common.IO;

namespace CLMode.Interface
{
    public class Encode091 : IEncode
    {
        public byte[] Encode(object o)
        {
            string str = o as string;

            WashSettingData d = (WashSettingData)XmlUtilit.Deserialize(typeof(WashSettingData), str);

            if (d == null)
            {
                return null;
            }

            List<byte> Listbyte = new List<byte>();

            Listbyte.Add(0x02);
            Listbyte.Add(0x90);

            Listbyte.Add((byte)('0' + d.ACount));
            Listbyte.Add((byte)('0' + d.BCount));

            Listbyte.Add((byte)('0' + 0));//CC
            int[] bytes = MachineControlProtocol.DecConverToHex(d.ASMPPosition);//DD EE
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            bytes = MachineControlProtocol.HDecConverToHex((int)d.ASMPVolume * 10);//FF GG II 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            Listbyte.Add((byte)bytes[2]);
            int ct = (new SMPContainerTypeService().Get(d.SampleContainerType) as SMPContainerType).Code;//JJ
            Listbyte.Add((byte)('0' + ct));
            bytes = MachineControlProtocol.DecConverToHex(d.ARGTPosition1);//KK LL 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            bytes = MachineControlProtocol.HDecConverToHex(d.ARGTVolume1);//MM NN OO 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            Listbyte.Add((byte)bytes[2]);
            bytes = MachineControlProtocol.DecConverToHex(d.ARGTPosition2);//PP QQ 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            bytes = MachineControlProtocol.HDecConverToHex(d.ARGTVolume2);//RR SS TT UU
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            Listbyte.Add((byte)bytes[2]);

            Listbyte.Add((byte)('0' + 0));//CC
            bytes = MachineControlProtocol.DecConverToHex(d.BSMPPosition);//DD EE
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            bytes = MachineControlProtocol.HDecConverToHex((int)d.BSMPVolume * 10);//FF GG II 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            Listbyte.Add((byte)bytes[2]);
            ct = (new SMPContainerTypeService().Get(d.SampleContainerType) as SMPContainerType).Code;//JJ
            Listbyte.Add((byte)('0' + ct));
            bytes = MachineControlProtocol.DecConverToHex(d.BRGTPosition1);//KK LL 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            bytes = MachineControlProtocol.HDecConverToHex(d.BRGTVolume1);//MM NN OO 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            Listbyte.Add((byte)bytes[2]);
            bytes = MachineControlProtocol.DecConverToHex(d.BRGTPosition2);//PP QQ 
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            bytes = MachineControlProtocol.HDecConverToHex(d.BRGTVolume2);//RR SS TT UU
            Listbyte.Add((byte)bytes[0]);
            Listbyte.Add((byte)bytes[1]);
            Listbyte.Add((byte)bytes[2]);

            Listbyte.Add(0x03);

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
    }
}
