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
using BioA.Common.IO;
using BioA.Common;

namespace BioA.UI
{
    public partial class TestEquipment : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;

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
            reactionDiskDebug.SendNetworkEvent += SendNetwork_Event;
            xtraTabPage1.Controls.Add(reactionDiskDebug);
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {

            switch (strMethod)
            {
                case "QueryManuOffsetGain":
                    lightSystem.ManuOffsetGainInfo = XmlUtility.Deserialize(typeof(ManuOffsetGain), sender as string) as ManuOffsetGain;
                    break;
                case "InitialPhotometerManualCheck":
                    lightSystem.IInitialRes = (int)sender;
                    break;
                case "GetLatestOffSetGain":

                    break;
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                xtraTabPage1.Controls.Clear();

                if (reactionDiskDebug != null)
                    reactionDiskDebug.SendNetworkEvent -= SendNetwork_Event;
                reactionDiskDebug = new ReactionDiskDebug();
                reactionDiskDebug.SendNetworkEvent += SendNetwork_Event;
                xtraTabPage1.Controls.Add(reactionDiskDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                xtraTabPage2.Controls.Clear();

                if (reagentPanelDebug != null)
                    reagentPanelDebug.SendNetworkEvent -= SendNetwork_Event;
                reagentPanelDebug = new ReagentPanelDebug();
                reagentPanelDebug.SendNetworkEvent += SendNetwork_Event;
                xtraTabPage2.Controls.Add(reagentPanelDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                xtraTabPage3.Controls.Clear();

                if (samplePanelDebug != null)
                    samplePanelDebug.SendNetworkEvent -= SendNetwork_Event;
                samplePanelDebug = new SamplePanelDebug();
                samplePanelDebug.SendNetworkEvent += SendNetwork_Event;
                xtraTabPage3.Controls.Add(samplePanelDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                xtraTabPage4.Controls.Clear();

                if (absorberDebug != null)
                    absorberDebug.SendNetworkEvent -= SendNetwork_Event;
                absorberDebug = new AbsorberDebug();
                absorberDebug.SendNetworkEvent += SendNetwork_Event;
                xtraTabPage4.Controls.Add(absorberDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 4)
            {
                xtraTabPage5.Controls.Clear();

                if (agitatorAdjustment != null)
                    agitatorAdjustment.SendNetworkEvent -= SendNetwork_Event;
                agitatorAdjustment = new AgitatorAdjustment();
                agitatorAdjustment.SendNetworkEvent += SendNetwork_Event;
                xtraTabPage5.Controls.Add(agitatorAdjustment);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 5)
            {
                xtraTabPage6.Controls.Clear();

                if (liquidRoadDebug != null)
                    liquidRoadDebug.SendNetworkEvent -= SendNetwork_Event;
                liquidRoadDebug = new LiquidRoadDebug();
                liquidRoadDebug.SendNetworkEvent += SendNetwork_Event;
                xtraTabPage6.Controls.Add(liquidRoadDebug);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 6)
            {
                xtraTabPage7.Controls.Clear();

                if (lightSystem != null)
                    lightSystem.SendNetworkEvent -= SendNetwork_Event;
                lightSystem = new LightSystem();
                lightSystem.SendNetworkEvent += SendNetwork_Event;
                xtraTabPage7.Controls.Add(lightSystem);
            }
        }

        private void SendNetwork_Event(string sender)
        {
            if (SendNetworkEvent != null)
            {
                SendNetworkEvent(sender);
            }
        }
    }
}
