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

namespace BioA.UI
{
    public partial class CrossPollution : DevExpress.XtraEditors.XtraUserControl
    {
        CuvetteAntifouling cuvetteAntifouling = new CuvetteAntifouling();
        ReagentNeedle reagentNeedle = new ReagentNeedle();
        NeddleSamples neddleSamples = new NeddleSamples();
        public CrossPollution()
        {
            InitializeComponent();
        }


        private void CrossPollution_Load(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
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
