using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.XtraEditors;
using System.Threading;
using BioA.UI.Uicomponent.SystemUI.Configure;
using System.IO;
using BioA.Common;
using BioA.SqlMaps;

namespace BioA.UI
{
    public partial class ConfigurationScript : DevExpress.XtraEditors.XtraUserControl
    {
        public ConfigurationScript()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DoRunScriptCommand();
        }

        string SQLFile = null;
        /// <summary>
        /// 打开运行脚本文件
        /// </summary>
        void DoRunScriptCommand()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".data";
            ofd.Filter = "data file|*.data";
            if (ofd.ShowDialog() == true)
            {
                splashScreenManager1.ShowWaitForm();
                Thread.Sleep(500);
                this.SQLFile = ofd.FileName;
                new Thread(new ThreadStart(RunScriptImportSQLData)).Start();
            }
        }
        private MyBatis mybatis = null;
        /// <summary>
        /// 运行脚本导入的SQL数据
        /// </summary>
        void RunScriptImportSQLData()
        {
            mybatis = new MyBatis();
            StreamReader sr = new StreamReader(this.SQLFile);
            String line;
            int i = 0;
            sr = new StreamReader(this.SQLFile);
            while ((line = sr.ReadLine()) != null)
            {
                i++;
                line = EncryptionText.DecryptDES(line, KeyManager.DataKey);
                //new DBService().ExecSQL(line.Trim());
                mybatis.SaveReagentProjectParamInfo(line.Trim());
            }
            
            splashScreenManager1.CloseWaitForm();
        }
    }
}
