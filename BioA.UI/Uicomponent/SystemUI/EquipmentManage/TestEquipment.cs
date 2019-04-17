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
                if (!xtrReactionTrayDebug.Contains(reactionDiskDebug))
                    xtrReactionTrayDebug.Controls.Add(reactionDiskDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 1)
            {
                if (reagentPanelDebug == null)
                {
                    reagentPanelDebug = new ReagentPanelDebug();
                    reagentPanelDebug.SendNetworkEvent += SendNetwork_Event;
                    xtrReagentTrayDebug.Controls.Add(reagentPanelDebug);
                }
                if(!xtrReagentTrayDebug.Contains(reagentPanelDebug))
                    xtrReagentTrayDebug.Controls.Add(reagentPanelDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 2)
            {
                if (samplePanelDebug == null)
                {
                    samplePanelDebug = new SamplePanelDebug();
                    samplePanelDebug.SendNetworkEvent += SendNetwork_Event;
                    xtrSampTrayDebug.Controls.Add(samplePanelDebug);
                }
                if(!xtrSampTrayDebug.Contains(samplePanelDebug))
                    xtrSampTrayDebug.Controls.Add(samplePanelDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 3)
            {

                if (absorberDebug == null)
                {
                    absorberDebug = new AbsorberDebug();
                    absorberDebug.SendNetworkEvent += SendNetwork_Event;
                    xtrAbsorberDebug.Controls.Add(absorberDebug);
                }
                if (!xtrAbsorberDebug.Contains(absorberDebug))
                    xtrAbsorberDebug.Controls.Add(absorberDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 4)
            {
                if (agitatorAdjustment == null)
                {
                    agitatorAdjustment = new AgitatorAdjustment();
                    agitatorAdjustment.SendNetworkEvent += SendNetwork_Event;
                    xtrTreaterDebug.Controls.Add(agitatorAdjustment);
                }
                if (!xtrTreaterDebug.Contains(agitatorAdjustment))
                    xtrTreaterDebug.Controls.Add(agitatorAdjustment);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 5)
            {
                if (liquidRoadDebug == null)
                {
                    liquidRoadDebug = new LiquidRoadDebug();
                    liquidRoadDebug.SendNetworkEvent += SendNetwork_Event;
                    xtrLiquidCircuitDebug.Controls.Add(liquidRoadDebug);
                }
                if (!xtrLiquidCircuitDebug.Contains(liquidRoadDebug))
                    xtrLiquidCircuitDebug.Controls.Add(liquidRoadDebug);
            }
            else if (xtrEquipmentDebug.SelectedTabPageIndex == 6)
            {
                if (lightSystem == null)
                {
                    lightSystem = new LightSystem();
                    lightSystem.SendNetworkEvent += SendNetwork_Event;
                    xtrOpticalPathDebug.Controls.Add(lightSystem);
                }
                if (!xtrOpticalPathDebug.Contains(lightSystem))
                    xtrOpticalPathDebug.Controls.Add(lightSystem);
                lightSystem.LightSystem_Load(null,null);
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
