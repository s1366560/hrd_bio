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
using BioA.Common;
using BioA.Common.IO;
using System.Threading;

namespace BioA.UI
{
    public partial class ProCombPage1 : DevExpress.XtraEditors.XtraUserControl
    {
        public ProCombPage1()
        {
            
        }

        private List<string> lstProjectGroups = new List<string>();

        public List<string> LstProjectGroups
        {
            get { return lstProjectGroups; }
            set
            {
                lstProjectGroups = value;
                this.BeginInvoke(new EventHandler(delegate { 
                    InitializeComponent();
                    simpleButton1.Text = lstProjectGroups.Count >= 1 ? lstProjectGroups[0] : "";
                    simpleButton2.Text = lstProjectGroups.Count >= 2 ? lstProjectGroups[1] : "";
                    simpleButton3.Text = lstProjectGroups.Count >= 3 ? lstProjectGroups[2] : "";
                    simpleButton4.Text = lstProjectGroups.Count >= 4 ? lstProjectGroups[3] : "";
                    simpleButton5.Text = lstProjectGroups.Count >= 5 ? lstProjectGroups[4] : "";
                    simpleButton6.Text = lstProjectGroups.Count >= 6 ? lstProjectGroups[5] : "";
                    simpleButton7.Text = lstProjectGroups.Count >= 7 ? lstProjectGroups[6] : "";
                    simpleButton8.Text = lstProjectGroups.Count >= 8 ? lstProjectGroups[7] : "";
                    simpleButton9.Text = lstProjectGroups.Count >= 9 ? lstProjectGroups[8] : "";
                    simpleButton10.Text = lstProjectGroups.Count >= 10 ? lstProjectGroups[9] : "";
                    simpleButton11.Text = lstProjectGroups.Count >= 11 ? lstProjectGroups[10] : "";
                    simpleButton12.Text = lstProjectGroups.Count >= 12 ? lstProjectGroups[11] : "";
                    simpleButton13.Text = lstProjectGroups.Count >= 13 ? lstProjectGroups[12] : "";
                    simpleButton14.Text = lstProjectGroups.Count >= 14 ? lstProjectGroups[13] : "";
                    simpleButton15.Text = lstProjectGroups.Count >= 15 ? lstProjectGroups[14] : "";
                    simpleButton16.Text = lstProjectGroups.Count >= 16 ? lstProjectGroups[15] : "";
                    //simpleButton17.Text = lstProjectGroups.Count >= 17 ? lstProjectGroups[16] : "";
                    //simpleButton18.Text = lstProjectGroups.Count >= 18 ? lstProjectGroups[17] : "";
                    //simpleButton19.Text = lstProjectGroups.Count >= 19 ? lstProjectGroups[18] : "";
                    //simpleButton20.Text = lstProjectGroups.Count >= 20 ? lstProjectGroups[19] : "";
                }));
            }

        }

        private List<string> selectedProjects = new List<string>();

        public List<string> SelectedProjects
        {
            get { return selectedProjects; }
            set
            {
                selectedProjects = value;

                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            control.Tag = "0";
                            control.ForeColor = Color.Black;
                        }));
                    }
                }

                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
                    {
                        foreach (string str in selectedProjects)
                        {
                            if (control.Text == str)
                            {
                                control.Tag = "1";

                                this.Invoke(new EventHandler(delegate
                                {
                                    control.ForeColor = Color.Red;
                                }));

                            }
                        }


                    }
                }
            }
        }

        public List<string> GetSelectedProjects()
        {
            List<string> lstProInfos = new List<string>();

            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
                {
                    if (control.Tag == "1")
                    {
                        if (control.Text != string.Empty)
                        {
                            lstProInfos.Add(control.Text);
                        }
                    }
                }
            }


            return lstProInfos;
        }

        public void ResetControlState()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
                {
                    if (control.Tag == "1")
                    {
                        control.Tag = "0";
                        this.Invoke(new EventHandler(delegate
                        {
                            control.ForeColor = Color.Black;
                        }));

                    }
                }
            }
        }

        private void simpleButton_Click(object sender, EventArgs e)
        {
            Button simpleButton = sender as Button;

            if (simpleButton.Text != "")
            {
                if (simpleButton.Tag as string == "1")
                {
                    simpleButton.Tag = "0";

                    simpleButton.ForeColor = Color.Black;
                }
                else if (simpleButton.Tag as string == "0")
                {
                    simpleButton.Tag = "1";
                    simpleButton.ForeColor = Color.Red;
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectByCombProName", simpleButton.Text)));
                }
                else
                {
                    simpleButton.Tag = "1";
                    simpleButton.ForeColor = Color.Red;
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectByCombProName", simpleButton.Text)));
                }
            }
        }
    }
}
