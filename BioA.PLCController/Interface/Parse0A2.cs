using BioA.Common;
using BioA.Common.Machine;
using BioA.SqlMaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.PLCController.Interface
{
    //NT-1000比色杯空白数据解析器
    public class Parse0A2 : IParse
    {
        static bool IsFisrt = false;
        MyBatis myBatis = new MyBatis();
        public string Parse(List<byte> Data)
        {
            int i = 2;
            int cuvno = MachineControlProtocol.HexConverToDec(Data[i], Data[i + 1], Data[i + 2]);

            if (cuvno == 1)
            {
                IsFisrt = true;
            }
            else
            {
                IsFisrt = false;
            }

            if (IsFisrt == true)
            {
                myBatis.BackupLastestToHistory();
                myBatis.ClearupCuvNewBlk();
            }

            for (int j = i + 3, index = 0; j < i + 72; j = j + 6, index++)
            {
                float blk = MachineControlProtocol.HexConverToFloat(Data[j], Data[j + 1], Data[j + 2], Data[j + 3], Data[j + 4], Data[j + 5]);
                if (blk > -0.000001 && blk < 0.000001)
                {
                    blk = 0.0000f;
                }
                else
                {
                    blk = (float)Math.Log10(10 / blk) * RunConfigureUtility.LightSpan;
                }
                myBatis.SaveLatestCuvBlkOfWaveAndCuvNO(System.Convert.ToInt32(RunConfigureUtility.WaveLengthList[index]), cuvno, blk);
            }

            return cuvno.ToString();
        }
    }
}
