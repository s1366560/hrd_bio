using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public enum Module
    {
        FramUI = 1,
        WorkingArea = 2,
        Reagent = 3,
        Calibration = 4,
        QualityControl = 5,
        Setting = 6, 
        System = 7,
        WindowsService = 8,
        DAO = 9,
        Common = 10,
        PLCData =11
    }

    public static class LogInfo
    {
        public static object lockObj = new object();

        public static void WriteErrorLog(string strLogInfo, Module module)
        {
            lock (lockObj)
            {
                string directory = @"d:\ErrorLog\" + module.ToString();

                if (Directory.Exists(directory))
                {

                }
                else
                {
                    Directory.CreateDirectory(directory);
                }
                string strPath = directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                if (File.Exists(strPath))
                {
                    FileStream fs = new FileStream(directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd"), FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(DateTime.Now.ToLongTimeString() + "：" + strLogInfo);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
                else
                {
                    FileStream fs = new FileStream(directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd"), FileMode.Create);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(DateTime.Now.ToLongTimeString() + "：" + strLogInfo);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
            }
        }

        public static void WriteProcessLog(string strLogInfo, Module module)
        {
            lock (lockObj)
            {
                string directory = @"D:\ProcessLog\" + module.ToString();

                if (Directory.Exists(directory))
                {

                }
                else
                {
                    Directory.CreateDirectory(directory);
                }
                string strPath = directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                if (File.Exists(strPath))
                {
                    FileStream fs = new FileStream(directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd"), FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(DateTime.Now.ToLongTimeString() + "：" + strLogInfo);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
                else
                {
                    FileStream fs = new FileStream(directory + "\\" + DateTime.Now.ToString("yyyy-MM-dd"), FileMode.Create);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(DateTime.Now.ToLongTimeString() + "：" + strLogInfo);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
            }
        }

        public enum ComLogType
        {
            SEND,
            RECEIVE
        }
        public static void Log(string info, ComLogType t, string logFile = @"COM.lg")
        {
            try
            {
                string dayStr = DateTime.Now.ToString("yyyyMMdd");
                string fileName = @"D:\aaa" + @"\" + logFile;
                StreamWriter sw = new StreamWriter(fileName, true, Encoding.Unicode);
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                sw.Write(timestamp);
                sw.Write('\t');
                sw.Write(t.ToString());
                sw.Write('\t');
                sw.Write(info);
                sw.Write("\r\n");
                sw.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
