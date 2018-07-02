using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management; 

namespace BioA.Common
{
    public static class ComputerInfo
    {
        static string cpuID = ""; //1.cpu序列号
        static string macAddress = ""; //2.mac序列号
        static string diskID = ""; //3.硬盘id
        //static string ipAddress = ""; //4.ip地址
        //static string loginUserName = ""; //5.登录用户名
        //static string computerName = ""; //6.计算机名
        //static string systemType = ""; //7.系统类型
        /// <summary>
        /// cpu序列号
        /// </summary>
        public static string CpuID
        {
            get { return cpuID = GetCpuID(); }
        }
        /// <summary>
        /// mac序列号
        /// </summary>
        public static string MacAddress
        {
            get { return macAddress = GetMacAddress(); }
        }
        /// <summary>
        /// 获取硬盘id
        /// </summary>
        public static string DiskID
        {
            get { return diskID = GetDiskID(); }
        }

        //1.获取CPU序列号代码 
        static string GetCpuID() 
        { 
            try 
            { 
                string cpuInfo = "";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor"); 
                ManagementObjectCollection moc = mc.GetInstances(); 
                foreach (ManagementObject mo in moc) 
                { 
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString(); 
                } 
                moc = null; 
                mc = null; 
                return cpuInfo; 
            } 
            catch 
            { 
                return "unknow"; 
            } 
            finally 
            {
            } 
        }
 
        //2.获取网卡硬件地址 
        static string GetMacAddress() 
        { 
            try 
            { 
                string mac = ""; 
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"); 
                ManagementObjectCollection moc = mc.GetInstances(); 
                foreach (ManagementObject mo in moc) 
                { 
                    if ((bool)mo["IPEnabled"] == true) 
                    { 
                        mac = mo["MacAddress"].ToString(); 
                        break; 
                    } 
                } 
                moc = null; 
                mc = null; 
                return mac; 
            } 
            catch 
            { 
                return "unknow"; 
            } 
            finally 
            { 
            } 

        }
 
        //3.获取硬盘ID 
        static string GetDiskID() 
        { 
            try 
            { 
                 String HDid = ""; 
                ManagementClass mc = new ManagementClass("Win32_DiskDrive"); 
                ManagementObjectCollection moc = mc.GetInstances(); 
                foreach (ManagementObject mo in moc) 
                { 
                    HDid = (string)mo.Properties["Model"].Value; 
                } 
                moc = null; 
                mc = null; 
                return HDid; 
            } 
            catch 
            { 
                return "unknow"; 
            } 
            finally 
            { 
            } 

        }
 
 
        //4.获取IP地址 

        static string GetIPAddress() 
        { 
            try 
            { 
                string st = ""; 
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration"); 
                ManagementObjectCollection moc = mc.GetInstances(); 
                foreach (ManagementObject mo in moc) 
                { 
                    if ((bool)mo["IPEnabled"] == true) 
                    { 
                        //st=mo["IpAddress"].ToString("#0.0000"); 
                        System.Array ar; 
                        ar = (System.Array)(mo.Properties["IpAddress"].Value); 
                        st = ar.GetValue(0).ToString(); 
                        break; 
                    } 
                } 
                moc = null; 
                mc = null; 
                return st; 
            } 
            catch 
            { 
                return "unknow"; 
            } 
            finally 
            { 
            } 

        } 


    
        /// 5.操作系统的登录用户名 
        static string GetUserName() 
        { 
            try 
            { 
                string un = Environment.UserName;
                return un; 
            } 
            catch 
            { 
                return "unknow"; 
            } 
            finally 
            { 
            } 

        } 



        //6.获取计算机名
        static string GetComputerName() 
        { 
            try 
            { 
                return System.Environment.MachineName;
            } 
            catch 
            { 
                return "unknow"; 
            } 
            finally 
            { 
            } 
        }
        ///7 PC类型 
        static string GetSystemType() 
        { 
            try 
            { 
                string st = ""; 
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem"); 
                ManagementObjectCollection moc = mc.GetInstances(); 
                foreach (ManagementObject mo in moc) 
                { 

                    st = mo["SystemType"].ToString(); 

                } 
                moc = null; 
                mc = null; 
                return st; 
            } 
            catch 
            { 
                return "unknow"; 
            } 
            finally 
            { 
            } 
        } 
    }
}
