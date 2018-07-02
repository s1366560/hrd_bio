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

namespace BioA.UI.Uicomponent.CalibrationUI.CalibrationState
{
    public partial class CalibrationState : DevExpress.XtraEditors.XtraUserControl
    {
        public CalibrationState()
        {
            InitializeComponent();
        }

        private void btnCalibCurve_Click(object sender, EventArgs e)
        {
            CalibrationCurve calibrationCurve = new CalibrationCurve();
            calibrationCurve.StartPosition = FormStartPosition.CenterScreen;
            calibrationCurve.ShowDialog();
        }

        private void btnCalibTrace_Click(object sender, EventArgs e)
        {
            CalibrationTrace calibrationTrace = new CalibrationTrace();
            calibrationTrace.StartPosition = FormStartPosition.CenterScreen;
            calibrationTrace.ShowDialog();
        }

        private void btnReactionProcess_Click(object sender, EventArgs e)
        {
            ReactionProcessCB reactionProcessCB = new ReactionProcessCB();
            reactionProcessCB.StartPosition = FormStartPosition.CenterScreen;
            reactionProcessCB.ShowDialog();
        }
    }
}
