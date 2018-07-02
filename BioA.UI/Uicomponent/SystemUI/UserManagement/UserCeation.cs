using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BioA.UI.Uicomponent.SystemUI.UserManagement
{
    public partial class UserCeation : DevExpress.XtraEditors.XtraForm
    {
        public UserCeation()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void lblNotesLoad1()
        {
            lblNotes.Text = "包含英文字母或数字";
        }
        public void lblNotesLoad2()
        {
            lblNotes.Text = "不可修改";
        }
       
    }
}