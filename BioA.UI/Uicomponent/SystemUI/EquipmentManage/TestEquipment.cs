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
            xtrReactionTrayDebug.Controls.Add(reactionDiskDebug);
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
            if (xtrEquipmentDebug.SelectedTabPageIndex == 0)
            {
                xtrReactionTrayDebug.Controls.Clear();

                if (reactionDiskDebug != null)
                    reactionDiskDebug.SendNetworkEvent -= SendNetwork_Event;
                reactionDiskDebug = new ReactionDiskDebug();
                reactionDiskDebug.SendNetworkEvent += SendNetwork_Event;
                xtrReactionTrayDebug.Controls.Add(reactionDiskDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 1)
            {
                xtrReagentTrayDebug.Controls.Clear();

                if (reagentPanelDebug != null)
                    reagentPanelDebug.SendNetworkEvent -= SendNetwork_Event;
                reagentPanelDebug = new ReagentPanelDebug();
                reagentPanelDebug.SendNetworkEvent += SendNetwork_Event;
                xtrReagentTrayDebug.Controls.Add(reagentPanelDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 2)
            {
                xtrSampTrayDebug.Controls.Clear();

                if (samplePanelDebug != null)
                    samplePanelDebug.SendNetworkEvent -= SendNetwork_Event;
                samplePanelDebug = new SamplePanelDebug();
                samplePanelDebug.SendNetworkEvent += SendNetwork_Event;
                xtrSampTrayDebug.Controls.Add(samplePanelDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 3)
            {
                xtrAbsorberDebug.Controls.Clear();

                if (absorberDebug != null)
                    absorberDebug.SendNetworkEvent -= SendNetwork_Event;
                absorberDebug = new AbsorberDebug();
                absorberDebug.SendNetworkEvent += SendNetwork_Event;
                xtrAbsorberDebug.Controls.Add(absorberDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 4)
            {
                xtrTreaterDebug.Controls.Clear();

                if (agitatorAdjustment != null)
                    agitatorAdjustment.SendNetworkEvent -= SendNetwork_Event;
                agitatorAdjustment = new AgitatorAdjustment();
                agitatorAdjustment.SendNetworkEvent += SendNetwork_Event;
                xtrTreaterDebug.Controls.Add(agitatorAdjustment);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 5)
            {
                xtrLiquidCircuitDebug.Controls.Clear();

                if (liquidRoadDebug != null)
                    liquidRoadDebug.SendNetworkEvent -= SendNetwork_Event;
                liquidRoadDebug = new LiquidRoadDebug();
                liquidRoadDebug.SendNetworkEvent += SendNetwork_Event;
                xtrLiquidCircuitDebug.Controls.Add(liquidRoadDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 6)
            {
                xtrOpticalPathDebug.Controls.Clear();

                if (lightSystem != null)
                    lightSystem.SendNetworkEvent -= SendNetwork_Event;
                lightSystem = new LightSystem();
                lightSystem.SendNetworkEvent += SendNetwork_Event;
                xtrOpticalPathDebug.Controls.Add(lightSystem);
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
