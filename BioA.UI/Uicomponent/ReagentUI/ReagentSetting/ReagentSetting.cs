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

namespace BioA.UI.Uicomponent.ReagentUI.ReagentSetting
{
    public partial class ReagentSetting : DevExpress.XtraEditors.XtraUserControl
    {
        public ReagentSetting()
        {
            InitializeComponent();
        }

        private void btnUnloadReagent_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadingReagent_Click(object sender, EventArgs e)
        {
            frmLoadingReagent frmloadingReagent = new frmLoadingReagent();
            frmloadingReagent.StartPosition = FormStartPosition.CenterScreen;
            frmloadingReagent.ShowDialog();
        }
    }
}
