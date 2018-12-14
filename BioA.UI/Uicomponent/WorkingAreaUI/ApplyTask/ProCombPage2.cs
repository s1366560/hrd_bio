using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioA.UI
{
    public partial class ProCombPage2 : DevExpress.XtraEditors.XtraUserControl
    {
        //声明一个委托
        public delegate void ClickProCombNamePage2(string sender);
        public event ClickProCombNamePage2 clickProCombNamePage2Event;
        public ProCombPage2()
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
                this.Invoke(new EventHandler(delegate{
                    
                    simpleButton1.Text = lstAssayProInfos.Count >= 21 ? lstAssayProInfos[20] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 22 ? lstAssayProInfos[21] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 23 ? lstAssayProInfos[22] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 24 ? lstAssayProInfos[23] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 25 ? lstAssayProInfos[24] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 26 ? lstAssayProInfos[25] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 27 ? lstAssayProInfos[26] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 28 ? lstAssayProInfos[27] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 29 ? lstAssayProInfos[28] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 30 ? lstAssayProInfos[29] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 31 ? lstAssayProInfos[30] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 32 ? lstAssayProInfos[31] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 33 ? lstAssayProInfos[32] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 34 ? lstAssayProInfos[33] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 35 ? lstAssayProInfos[34] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 36 ? lstAssayProInfos[35] : "";
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
                if (control.GetType() == typeof(DevExpress.XtraEditors.SimpleButton))
                {
                    if (control.Tag.ToString() == "1")
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
                else if (simpleButton.Tag as string == "0" || simpleButton.Tag == null)
                {
                    simpleButton.Tag = "1";
                    simpleButton.ForeColor = Color.Red;
                    //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryProjectByCombProName", simpleButton.Text)));
                    if (clickProCombNamePage2Event != null)
                    {
                        clickProCombNamePage2Event(simpleButton.Text.ToString());
                    }
                }
            }
        }
    }
}
