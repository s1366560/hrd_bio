using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BioA.UI.Uicomponent.SystemUI.UserManagement
{
    public partial class UserManagement : DevExpress.XtraEditors.XtraUserControl
    {
        UserCeation userCeation ;
        public UserManagement()
        {
            InitializeComponent();
            userCeation = new UserCeation();
        }

        private void btnUsercreation_Click(object sender, EventArgs e)
        {
            
            userCeation.Text = "用户创建";
            userCeation.lblNotesLoad1();
            userCeation.StartPosition = FormStartPosition.CenterScreen;
            userCeation.ShowDialog();
        }

        private void btnUsereditor_Click(object sender, EventArgs e)
        {
           
            userCeation.Text = "用户编辑";
            userCeation.lblNotesLoad2();
            userCeation.StartPosition = FormStartPosition.CenterScreen;
            userCeation.ShowDialog();
        }
    }
}
