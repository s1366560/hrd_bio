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
using BioA.UI.Uicomponent.WorkingAreaUI.ApplyTask;

namespace BioA.UI.Uicomponent.SettingsUI.Portfolio
{
    public partial class CombinationItem : DevExpress.XtraEditors.XtraUserControl
    {
        ProjectPage projectPage;
        public CombinationItem()
        {
            InitializeComponent();
            projectPage = new ProjectPage();
            xtraTabPage1.Controls.Add(projectPage);
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
    }
}
