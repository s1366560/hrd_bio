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

namespace BioA.UI.Uicomponent.SystemUI.LogCheck
{
    public partial class Alertlog : DevExpress.XtraEditors.XtraUserControl
    {
        public Alertlog()
        {
            InitializeComponent();
        }

        private void btnSelectall_Click(object sender, EventArgs e)
        {

        }

        public void btnRemove ()
        {
            this.Controls.Remove(btnSearch);
        }
    }
}
