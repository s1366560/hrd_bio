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

namespace BioA.UI.Uicomponent.WorkingAreaUI.ApplyTask
{
    public partial class PatientInfoEdit : DevExpress.XtraEditors.XtraUserControl
    {
        public PatientInfoEdit()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
