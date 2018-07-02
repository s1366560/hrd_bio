using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLMode.Service;
using CLMode.Protocol;
using CLMode.Machine;
/*************************************************************************************
 * CLR版本：        4.0.30319.261
 * 类 名 称：       Parse0A
 * 机器名称：       WENSION
 * 命名空间：       CLMode.Interface
 * 文 件 名：       Parse0A
 * 创建时间：       4/25/2012 12:40:26 PM
 * 作    者：       李文山
 * 
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

namespace CLMode.Interface
{
    //NSA-400比色杯空白数据解析器
    public class Parse0A0 : IParse
    {
        public string Parse(List<byte> Data)
        {
            CUVBLKService CUVBLKSer = new CUVBLKService();

            CUVBLKSer.BackupLastestToHistory();

            int i = 2;
            while (i < Data.Count - 3)
            {
                int cuvno = MachineControlProtocol.HexConverToDec(Data[i], Data[i + 1]);
                for (int j = i + 2, index = 0; j < i + 72; j = j + 6, index++)
                {
                    float blk = MachineControlProtocol.HexConverToFloat(Data[j], Data[j + 1], Data[j + 2], Data[j + 3], Data[j + 4], Data[j + 5]);
                    if (blk > -0.000001 && blk < 0.000001)
                    {
                        blk = 0.0000f;
                    }
                    else
                    {
                        blk = (float)Math.Log10(10 / blk) * MachineInfo.LightSpan;
                    }
                    CUVBLKSer.SaveLatestCuvBlkOfWaveAndCuvNO(MachineInfo.WaveLengthArray[index], cuvno, blk);
                }
                i += 74;
            }
            return "FinishUpdateCuvBlk";
        }
    }
}
