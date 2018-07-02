using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Machine
{
    public class MachineControlProtocol
    {
        //包头
        public const byte BEGIN = 0x02;
        //包尾
        public const byte END = 0x03;

        //计算校验和
        public static byte[] CheckSum(long sum)
        {
            long check_sum = 0;
            check_sum = sum % 256;

            long first_data = '0' + check_sum / 16;
            if (check_sum / 16 > 9)
            {
                first_data = first_data + 7;
            }

            long secnd_data = '0' + check_sum % 16;
            if (check_sum % 16 > 9)
            {
                secnd_data = secnd_data + 7;
            }

            byte[] checksum = new byte[2];
            checksum[0] = (byte)first_data;
            checksum[1] = (byte)secnd_data;

            return checksum;
        }
        public static byte[] CheckSum(byte[] bytes)
        {
            long bytessum = 0;
            for (int i = 0; i < bytes.Count()-2; i++)
            {
                bytessum += (int)bytes[i];
            }
            return MachineControlProtocol.CheckSum(bytessum);
        }
        public static int HexConverToDec(int h1, int h2, int h3, int h4, int h5)
        {
            int r;

            int d1 = h1 - '0';
            int d2 = h2 - '0';
            int d3 = h3 - '0';
            int d4 = h4 - '0';
            int d5 = h5 - '0';

            r = d1 * 10000 + d2 * 1000 + d3 * 100 + d4 * 10 + d5;
            return r;
        }
        public static int HexConverToDec(int hxdata, int lxdata)
        {
            int r;
            int h = hxdata - '0';
            int l = lxdata - '0';
            r = h * 10 + l;
            return r;
        }
        public static int HexConverToDec(int hhxdata, int hxdata, int lxdata)
        {
            int r;
            int hh = hhxdata - '0';
            int h = hxdata - '0';
            int l = lxdata - '0';
            r = hh * 100 + h * 10 + l;
            return r;
        }
        public static float HexConverToFloat(int h1, int h2, int h3, int h4, int h5, int h6)
        {
            if (h2 == 0x2E)
            {
                int d1 = h1 - '0';
                int d2 = h3 - '0';
                int d3 = h4 - '0';
                int d4 = h5 - '0';
                int d5 = h6 - '0';

                return (float)d1 + (float)(d2 * 0.1) + (float)(d3 * 0.01) + (float)(d4 * 0.001) + (float)(d5 * 0.0001);
            }
            if(h3 == 0x2E)
            {
                int d1 = h1 - '0';
                int d2 = h2 - '0';
                int d3 = h4 - '0';
                int d4 = h5 - '0';
                int d5 = h6 - '0';

                return (float)d1*10 + (float)d2 + (float)(d3 * 0.1) + (float)(d4 * 0.01) + (float)(d5 * 0.001);
            }

            return 0.0f;
                 
        }
        public static string ConvertArrayToString(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            string vstr = "";
            foreach (char e in data)
            {
                string v = string.Format("{0}", e);
                vstr += v;
            }
            return vstr;
        }
        //0011.11
        public static float HexConverToFloat(byte[] data)
        {
            string v = ConvertArrayToString(data);
            return float.Parse(v);
        }
        public static float HexConverToFloat(int h1, int h2, int h3, int h4)
        {
            if (h2 == 0x2E)
            {
                int d1 = h1 - '0';
                int d2 = h3 - '0';
                int d3 = h4 - '0';

                return (float)d1 + (float)(d2 * 0.1) + (float)(d3 * 0.01);
            }
            else
            {
                return 0;
            }
        }
        //99
        public static int[] DecConverToHex(int sourcedata)
        {
            int data;
            int hxdata, lxdata;
            data = sourcedata / 10;
            hxdata = data + '0';

            data = sourcedata % 10;
            lxdata = data + '0';

            int[] hex = new int[2];
            hex[0] = hxdata;
            hex[1] = lxdata;

            return hex;
        }
        //100
        public static int[] HDecConverToHex(int sourcedata)
        {
            int data;
            int hhxdata, hxdata, lxdata;

            data = sourcedata / 100;
            hhxdata = data + '0';

            data = (sourcedata % 100) / 10;
            hxdata = data + '0';

            data = sourcedata % 10;
            lxdata = data + '0';

            int[] hex = new int[3];
            hex[0] = hhxdata;
            hex[1] = hxdata;
            hex[2] = lxdata;

            return hex;
        }
        //99999
        public static int[] HDecConverToHex99999(int sourcedata)
        {
            int data;
            int h1, h2, h3, h4, h5;

            data = sourcedata / 10000;
            h1 = data + '0';

            data = (sourcedata % 10000) / 1000;
            h2 = data + '0';

            data = (sourcedata % 1000) / 100;
            h3 = data + '0';

            data = (sourcedata % 100) / 10;
            h4 = data + '0';

            data = sourcedata % 10;
            h5 = data + '0';

            int[] hex = new int[5];
            hex[0] = h1;
            hex[1] = h2;
            hex[2] = h3;
            hex[3] = h4;
            hex[4] = h5;

            return hex;
        }
        // 十六进制字符串转换字节数组
        public static byte[] HexStringToByteArray(string txt)
        {
            string[] key = txt.Split(' ');
            byte[] buffer = new byte[key.Count()];
            for (int i = 0; i < key.Count(); i++)
            {
                buffer[i] = Convert.ToByte(key[i], 16);
            }

            return buffer;
        }
        public static byte[] HexStringToByteArray(string txt,char s)
        {
            string[] key = txt.Split(s);
            byte[] buffer = new byte[key.Count()];
            for (int i = 0; i < key.Count(); i++)
            {
                buffer[i] = Convert.ToByte(key[i].Trim(), 16);
            }

            return buffer;
        }
        //字节数组转换十六进制
        public static string ByteArrayToHexString(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            string HexStr = "";
            for (int i = 0; i < data.Count(); i++)
            {
                string str1 = string.Format(@"{0,2:X}", data[i]);
                //str1 = str1.PadLeft(2, '0');
                string key = "0x" + str1.ToUpper() + " ";
                
                HexStr += key;
            }

            return HexStr;
        }
        public static string BytelistToHexString(List<byte> data)
        {
            string HexStr = "";
            for (int i = 0; i < data.Count(); i++)
            {
                string str1 = string.Format(@"{0,2:X}", data[i]);
                //str1 = str1.PadLeft(2,'0');
                string key = "0x" + str1.ToUpper() + " ";
                HexStr += key;
            }

            return HexStr;
        }
        //字节List转换字节数组
        public static byte[] BytelistToByteArray(List<byte> bytelist)
        {
            if (bytelist.Count <= 0)
            {
                return null;
            }
            byte[] buffer = new byte[bytelist.Count];
            for (int i = 0; i < bytelist.Count; i++)
            {
                buffer[i] = bytelist.ElementAt(i);
            }
            return buffer;
        }
        //老型号任务包
        
        
    }
}
