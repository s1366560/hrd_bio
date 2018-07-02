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

namespace BioA.UI.Uicomponent.QualityControlUI.QCState
{
    public partial class QualityControlState : DevExpress.XtraEditors.XtraUserControl
    {
        public QualityControlState()
        {
            InitializeComponent();
        }

        private void btnHisResult_Click(object sender, EventArgs e)
        {
            frmHisResultChart ResultChart = new frmHisResultChart();
            ResultChart.StartPosition = FormStartPosition.CenterScreen;
            ResultChart.ShowDialog();
        }

        private void btnReactionProcess_Click(object sender, EventArgs e)
        {
            ReactionProcessQC reactionProcessQC = new ReactionProcessQC();
            reactionProcessQC.StartPosition = FormStartPosition.CenterScreen;
            reactionProcessQC.ShowDialog();
        }
    }
}
