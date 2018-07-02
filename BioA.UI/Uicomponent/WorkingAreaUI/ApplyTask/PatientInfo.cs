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

namespace BioA.UI.Uicomponent.WorkingAreaUI.ApplyTask
{
    public partial class PatientInfo : DevExpress.XtraEditors.XtraForm
    {
        PatientInfoEdit patientInfoEdit;
        PatientInfoCheck patientInfoCheck;
        public PatientInfo()
        {
            InitializeComponent();
            this.ControlBox = false;
            patientInfoEdit = new PatientInfoEdit();
            grpPatientInfoCheck.Controls.Add(patientInfoEdit);
            patientInfoCheck = new PatientInfoCheck();
           


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPatientInfoEdit_Click(object sender, EventArgs e)
        {
            grpPatientInfoCheck.Controls.Clear();
            grpPatientInfoCheck.Controls.Add(patientInfoEdit);
        }

        private void btnPatientInfoSelect_Click(object sender, EventArgs e)
        {
            grpPatientInfoCheck.Controls.Clear();
            grpPatientInfoCheck.Controls.Add(patientInfoCheck);
        }
    }
}