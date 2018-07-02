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

namespace BioA.UI.Uicomponent.CalibrationUI.CalibMaintain
{
    public partial class CalibMaintain : DevExpress.XtraEditors.XtraUserControl
    {
        CalibAddAndEdit calibAddAndEdit;
        public CalibMaintain()
        {
            InitializeComponent();
            calibAddAndEdit = new CalibAddAndEdit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            calibAddAndEdit.Text = "装载校准品";
            calibAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
            calibAddAndEdit.ShowDialog();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            calibAddAndEdit.Text = "编辑校准品";
            calibAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
            calibAddAndEdit.ShowDialog();
        }
    }
}
