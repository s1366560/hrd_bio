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

namespace BioA.UI.Uicomponent.CalibrationUI.CalibrationState
{
    public partial class CalibrationTrace : DevExpress.XtraEditors.XtraForm
    {
        public CalibrationTrace()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}