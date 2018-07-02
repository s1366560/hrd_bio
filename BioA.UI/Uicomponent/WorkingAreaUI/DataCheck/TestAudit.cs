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

namespace BioA.UI.Uicomponent.WorkingAreaUI.DataCheck
{
    public partial class TestAudit : DevExpress.XtraEditors.XtraForm
    {
        public TestAudit()
        {
            InitializeComponent();
            this.ControlBox = false; 
        }

        private void TestAudit_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReactionCurve_Click(object sender, EventArgs e)
        {
            ReflectionMonitoring reflectionMonitoring = new ReflectionMonitoring();
            reflectionMonitoring.StartPosition = FormStartPosition.CenterScreen;
            reflectionMonitoring.ShowDialog();
        }
    }
}