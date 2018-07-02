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

namespace BioA.UI.Uicomponent.SystemUI.Maintenance
{
    public partial class RMThirdMenu : DevExpress.XtraEditors.XtraUserControl
    {
        UltravioletRays ultravioletRays;
        WaterBlankCheck waterBlankCheck;
        CleaningMaintenance cleaningMaintenance;
        public RMThirdMenu()
        {
            InitializeComponent();

            ultravioletRays = new UltravioletRays();

            waterBlankCheck = new WaterBlankCheck();
            cleaningMaintenance = new CleaningMaintenance();

        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                xtraTabPage1.Controls.Add(waterBlankCheck);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage2.Controls.Add(ultravioletRays);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabPage3.Controls.Add(cleaningMaintenance);
            }
           
        }
    }
}
