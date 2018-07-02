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

namespace BioA.UI.Uicomponent.CalibrationUI.CalibMaintain
{
    public partial class CalibAddAndEdit : DevExpress.XtraEditors.XtraForm
    {
        public CalibAddAndEdit()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}