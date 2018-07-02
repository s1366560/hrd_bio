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

namespace BioA.UI.Uicomponent.ReagentUI.ReagentSetting
{
    public partial class frmLoadingReagent : DevExpress.XtraEditors.XtraForm
    {
        public frmLoadingReagent()
        {
            InitializeComponent();
            this.ControlBox = false; 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}