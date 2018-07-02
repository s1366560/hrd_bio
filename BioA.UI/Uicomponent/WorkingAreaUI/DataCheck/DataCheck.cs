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
using BioA.UI.Uicomponent.WorkingAreaUI.DataCheck;

namespace BioA.UI.Uicomponent
{
    public partial class DataCheck : DevExpress.XtraEditors.XtraUserControl
    {
        public DataCheck()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            ScreeningSample screeningSample = new ScreeningSample();
            screeningSample.StartPosition = FormStartPosition.CenterScreen;
            screeningSample.ShowDialog();
        }

        private void chkFilterOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilterOpen.Checked)
            {
                chkFilterClose.Checked = false;
            }
            else if (chkFilterOpen.Checked == false && chkFilterClose.Checked == false)
            {
                chkFilterOpen.Checked = true;
            }
        }

        private void chkFilterClose_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilterClose.Checked)
            {
                chkFilterOpen.Checked = false;
            }
            else if (chkFilterClose.Checked == false && chkFilterOpen.Checked == false)
            {
                chkFilterClose.Checked = true;
            }
        }

        private void btnExamine_Click(object sender, EventArgs e)
        {
            TestAudit testAudit = new TestAudit();
            testAudit.StartPosition = FormStartPosition.CenterScreen;
            testAudit.ShowDialog();

        }

        private void btnReactionMonitoring_Click(object sender, EventArgs e)
        {
            ReflectionMonitoring reflectionMonitoring = new ReflectionMonitoring();
            reflectionMonitoring.StartPosition = FormStartPosition.CenterScreen;
            reflectionMonitoring.ShowDialog();
        }
    }
}
