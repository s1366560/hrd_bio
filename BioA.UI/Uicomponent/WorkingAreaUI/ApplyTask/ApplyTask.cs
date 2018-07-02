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

namespace BioA.UI.Uicomponent.WorkingAreaUI.ApplyTask
{
    public partial class ApplyTask : DevExpress.XtraEditors.XtraUserControl
    {
        ProjectPage projectPage;
        ProCombPage proCombPage;
        public ApplyTask()
        {
            InitializeComponent();
            projectPage = new ProjectPage();
            xtraTabPage1.Controls.Add(projectPage);
            proCombPage = new ProCombPage();
            xtraTabPage5.Controls.Add(proCombPage);
        }


        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
               
                xtraTabPage1.Controls.Add(projectPage);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage2.Controls.Add(projectPage);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabPage3.Controls.Add(projectPage);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                xtraTabPage4.Controls.Add(projectPage);
            }
        }

        private void xtraTabControl2_Click(object sender, EventArgs e)
        {
            if (xtraTabControl2.SelectedTabPageIndex == 0)
            {
                xtraTabPage5.Controls.Add(proCombPage);
            }
            else if (xtraTabControl2.SelectedTabPageIndex == 1)
            {
                xtraTabPage6.Controls.Add(proCombPage);
            }
            else if (xtraTabControl2.SelectedTabPageIndex == 2)
            {
                xtraTabPage7.Controls.Add(proCombPage);
            }
            else if (xtraTabControl2.SelectedTabPageIndex == 3)
            {
                xtraTabPage8.Controls.Add(proCombPage);
            }
        }

        private void btnPatientInfo_Click(object sender, EventArgs e)
        {
            PatientInfo patientInfo = new PatientInfo();
            patientInfo.StartPosition = FormStartPosition.CenterScreen;
            patientInfo.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
