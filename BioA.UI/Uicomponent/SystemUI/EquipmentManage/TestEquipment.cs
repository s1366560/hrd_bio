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

namespace BioA.UI.Uicomponent.SystemUI.EquipmentManage
{
    public partial class TestEquipment : DevExpress.XtraEditors.XtraUserControl
    {
        ReactionDiskDebug reactionDiskDebug;
        ReagentPanelDebug reagentPanelDebug;
        SamplePanelDebug samplePanelDebug;
        AbsorberDebug absorberDebug;
        AgitatorAdjustment agitatorAdjustment;
        LiquidRoadDebug liquidRoadDebug;
        LightSystem lightSystem;


        public TestEquipment()
        {
            InitializeComponent();
            reactionDiskDebug = new ReactionDiskDebug();
            reagentPanelDebug = new ReagentPanelDebug();
            samplePanelDebug = new SamplePanelDebug();
            absorberDebug = new AbsorberDebug();
            agitatorAdjustment = new AgitatorAdjustment();
            liquidRoadDebug = new LiquidRoadDebug();
            lightSystem = new LightSystem();

        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                xtraTabPage1.Controls.Add(reactionDiskDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage2.Controls.Add(reagentPanelDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabPage3.Controls.Add(samplePanelDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                xtraTabPage4.Controls.Add(absorberDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 4)
            {
                xtraTabPage5.Controls.Add(agitatorAdjustment);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 5)
            {
                xtraTabPage6.Controls.Add(liquidRoadDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 6)
            {
                xtraTabPage7.Controls.Add(lightSystem);
            }
        }
    }
}
