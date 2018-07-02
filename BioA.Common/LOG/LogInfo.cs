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
        Common = 10
    }

    public static class LogInfo
    {
        public static void WriteErrorLog(string strLogInfo, Module module)
        {
            string directory = @"D:\ErrorLog\" + module.ToString();

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

        public static void WriteProcessLog(string strLogInfo, Module module)
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
}
