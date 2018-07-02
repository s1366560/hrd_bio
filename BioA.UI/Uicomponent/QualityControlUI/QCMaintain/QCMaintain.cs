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

namespace BioA.UI.Uicomponent.QualityControlUI.QCMaintain
{
    public partial class QCMaintain : DevExpress.XtraEditors.XtraUserControl
    {
        QualityControlAddAndEdit qualityControlAddAndEdit;
        public QCMaintain()
        {
            InitializeComponent();
            qualityControlAddAndEdit = new QualityControlAddAndEdit();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            qualityControlAddAndEdit.Text = "编辑质控品";
            qualityControlAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
            qualityControlAddAndEdit.ShowDialog();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            qualityControlAddAndEdit.Text = "新增质控品";
            qualityControlAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
            qualityControlAddAndEdit.ShowDialog();
        }
    }
}
