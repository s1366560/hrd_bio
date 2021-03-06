﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BioA.Common;
using BioA.Common.IO;

namespace BioA.UI
{
    public partial class CalibProCombPage2 : DevExpress.XtraEditors.XtraUserControl
    {
        //声明一委托 
        public delegate bool ClickCombProNamePage2(string sender,string tag);
        public event ClickCombProNamePage2 clickCombProNamePage2Event;

        public CalibProCombPage2()
        {
            InitializeComponent();
        }

        private List<string> lstAssayProInfos = new List<string>();

        public List<string> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.ResetControlState();
                this.BeginInvoke(new EventHandler(delegate
                {
                    simpleButton1.Text = lstAssayProInfos.Count >= 17 ? lstAssayProInfos[16] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 18 ? lstAssayProInfos[17] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 19 ? lstAssayProInfos[18] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 20 ? lstAssayProInfos[19] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 21 ? lstAssayProInfos[20] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 22 ? lstAssayProInfos[21] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 23 ? lstAssayProInfos[22] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 24 ? lstAssayProInfos[23] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 25 ? lstAssayProInfos[24] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 26 ? lstAssayProInfos[25] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 27 ? lstAssayProInfos[26] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 28 ? lstAssayProInfos[27] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 29 ? lstAssayProInfos[28] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 30 ? lstAssayProInfos[29] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 31 ? lstAssayProInfos[30] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 32 ? lstAssayProInfos[31] : "";
                    //simpleButton17.Text = lstAssayProInfos.Count >= 37 ? lstAssayProInfos[36] : "";
                    //simpleButton18.Text = lstAssayProInfos.Count >= 38 ? lstAssayProInfos[37] : "";
                    //simpleButton19.Text = lstAssayProInfos.Count >= 39 ? lstAssayProInfos[38] : "";
                    //simpleButton20.Text = lstAssayProInfos.Count >= 40 ? lstAssayProInfos[39] : "";
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
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
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
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
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
                if (control.GetType() == typeof(System.Windows.Forms.Button))
                {
                    if (control.Tag.ToString() == "1")
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
                if (control.Tag != null)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            control.Text = null;
                            control.Tag = null;
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
                    if (clickCombProNamePage2Event != null)
                    {
                        bool ret = clickCombProNamePage2Event(simpleButton.Text.ToString(), "1");
                        if (ret)
                        {
                            simpleButton.Tag = "0";
                            simpleButton.ForeColor = Color.Black;
                        }
                    }
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectByCombProName", simpleButton.Text.ToString())));
                }
                else if (simpleButton.Tag as string == "0" || simpleButton.Tag == null)
                {
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibControlTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectByCombProName", simpleButton.Text.ToString())));
                    if (clickCombProNamePage2Event != null)
                    {
                        bool ret = clickCombProNamePage2Event(simpleButton.Text.ToString(),"0");
                        if(ret)
                        {
                            simpleButton.Tag = "1";
                            simpleButton.ForeColor = Color.Red;
                        }
                    }
                }
            }
        }
    }
}
