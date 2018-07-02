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

namespace BioA.UI.Uicomponent.QualityControlUI.QCMaintain
{
    public partial class QualityControlAddAndEdit : DevExpress.XtraEditors.XtraForm
    {
        public QualityControlAddAndEdit()
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