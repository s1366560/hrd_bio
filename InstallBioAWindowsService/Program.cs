using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallBioAWindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = System.Environment.CurrentDirectory;

            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = "Install.bat";
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }
    }
}
