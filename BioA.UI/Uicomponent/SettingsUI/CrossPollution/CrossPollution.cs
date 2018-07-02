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

namespace BioA.UI.Uicomponent.SettingsUI.CrossPollution
{
    public partial class CrossPollution : DevExpress.XtraEditors.XtraUserControl
    {
        CuvetteAntifouling cuvetteAntifouling;
        ReagentNeedle reagentNeedle;
        NeddleSamples neddleSamples;
        public CrossPollution()
        {
            InitializeComponent();
            cuvetteAntifouling = new CuvetteAntifouling();
            reagentNeedle = new ReagentNeedle();
            neddleSamples=new NeddleSamples ();
            xtraTabPage1.Controls.Add(reagentNeedle);
        }

        private void CrossPollution_Click(object sender, EventArgs e)
        {
            
           
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                xtraTabPage1.Controls.Add(reagentNeedle);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage2.Controls.Add(cuvetteAntifouling);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabPage3.Controls.Add(neddleSamples);
            }
        }
    }
}
