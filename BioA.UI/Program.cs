using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using BioA.Common;

namespace BioA.UI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Coffee");
            try
            {
                //显示登录窗体
                //LoginInterface login = new LoginInterface();
                ////login.LoginEvent += login_LoginEvent;
                //CommunicationUI.notifyCallBack.LoginDataTransferEvent += login.DataTransfer_Event;
                //DialogResult result = login.ShowDialog();

                ////同过窗体返回值，确定是否显示主窗体
                //if (result == DialogResult.OK)
                //{

                    Application.Run(new Form1());
                //}
                //else
                //    Application.Exit();
            }
            catch(Exception ex)
            {
                LogInfo.WriteErrorLog("程序启动异常! error:" +ex.ToString(), Module.FramUI);
            }
        }

        public static UserInfo userInfo = null;
    }
}
