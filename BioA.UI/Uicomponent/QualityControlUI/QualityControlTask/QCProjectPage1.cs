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
    public partial class QCProjectPage1 : DevExpress.XtraEditors.XtraUserControl
    {
        public QCProjectPage1()
        {
            InitializeComponent();
        }
        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;

        private List<string> lstAssayProInfos = new List<string>();

        public List<string> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.Invoke(new EventHandler(delegate{
                    simpleButton1.Text = lstAssayProInfos.Count >= 1 ? lstAssayProInfos[0] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 2 ? lstAssayProInfos[1] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 3 ? lstAssayProInfos[2] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 4 ? lstAssayProInfos[3] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 5 ? lstAssayProInfos[4] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 6 ? lstAssayProInfos[5] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 7 ? lstAssayProInfos[6] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 8 ? lstAssayProInfos[7] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 9 ? lstAssayProInfos[8] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 10 ? lstAssayProInfos[9] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 11 ? lstAssayProInfos[10] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 12 ? lstAssayProInfos[11] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 13 ? lstAssayProInfos[12] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 14 ? lstAssayProInfos[13] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 15 ? lstAssayProInfos[14] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 16 ? lstAssayProInfos[15] : "";
                    simpleButton17.Text = lstAssayProInfos.Count >= 17 ? lstAssayProInfos[16] : "";
                    simpleButton18.Text = lstAssayProInfos.Count >= 18 ? lstAssayProInfos[17] : "";
                    simpleButton19.Text = lstAssayProInfos.Count >= 19 ? lstAssayProInfos[18] : "";
                    simpleButton20.Text = lstAssayProInfos.Count >= 20 ? lstAssayProInfos[19] : "";
                    simpleButton21.Text = lstAssayProInfos.Count >= 21 ? lstAssayProInfos[20] : "";
                    simpleButton22.Text = lstAssayProInfos.Count >= 22 ? lstAssayProInfos[21] : "";
                    simpleButton23.Text = lstAssayProInfos.Count >= 23 ? lstAssayProInfos[22] : "";
                    simpleButton24.Text = lstAssayProInfos.Count >= 24 ? lstAssayProInfos[23] : "";
                    simpleButton25.Text = lstAssayProInfos.Count >= 25 ? lstAssayProInfos[24] : "";
                    simpleButton26.Text = lstAssayProInfos.Count >= 26 ? lstAssayProInfos[25] : "";
                    simpleButton27.Text = lstAssayProInfos.Count >= 27 ? lstAssayProInfos[26] : "";
                    simpleButton28.Text = lstAssayProInfos.Count >= 28 ? lstAssayProInfos[27] : "";
                    simpleButton29.Text = lstAssayProInfos.Count >= 29 ? lstAssayProInfos[28] : "";
                    simpleButton30.Text = lstAssayProInfos.Count >= 30 ? lstAssayProInfos[29] : "";
                    simpleButton31.Text = lstAssayProInfos.Count >= 31 ? lstAssayProInfos[30] : "";
                    simpleButton32.Text = lstAssayProInfos.Count >= 32 ? lstAssayProInfos[31] : "";
                    simpleButton33.Text = lstAssayProInfos.Count >= 33 ? lstAssayProInfos[32] : "";
                    simpleButton34.Text = lstAssayProInfos.Count >= 34 ? lstAssayProInfos[33] : "";
                    simpleButton35.Text = lstAssayProInfos.Count >= 35 ? lstAssayProInfos[34] : "";
                    simpleButton36.Text = lstAssayProInfos.Count >= 36 ? lstAssayProInfos[35] : "";
                    simpleButton37.Text = lstAssayProInfos.Count >= 37 ? lstAssayProInfos[36] : "";
                    simpleButton38.Text = lstAssayProInfos.Count >= 38 ? lstAssayProInfos[37] : "";
                    simpleButton39.Text = lstAssayProInfos.Count >= 39 ? lstAssayProInfos[38] : "";
                    simpleButton40.Text = lstAssayProInfos.Count >= 40 ? lstAssayProInfos[39] : "";
                    simpleButton41.Text = lstAssayProInfos.Count >= 41 ? lstAssayProInfos[40] : "";
                    simpleButton42.Text = lstAssayProInfos.Count >= 42 ? lstAssayProInfos[41] : "";
                    simpleButton43.Text = lstAssayProInfos.Count >= 43 ? lstAssayProInfos[42] : "";
                    simpleButton44.Text = lstAssayProInfos.Count >= 44 ? lstAssayProInfos[43] : "";
                    simpleButton45.Text = lstAssayProInfos.Count >= 45 ? lstAssayProInfos[44] : "";
                    simpleButton46.Text = lstAssayProInfos.Count >= 46 ? lstAssayProInfos[45] : "";
                    simpleButton47.Text = lstAssayProInfos.Count >= 47 ? lstAssayProInfos[46] : "";
                    simpleButton48.Text = lstAssayProInfos.Count >= 48 ? lstAssayProInfos[47] : "";
                }));
            }
                
        }

        private List<string[]> selectedProjects = new List<string[]>();

        public List<string[]> SelectedProjects
        {
            get { return selectedProjects; }
            set
            {
                selectedProjects = value;

                ResetControlState();

                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        foreach (string[] str in selectedProjects)
                        {
                            if (control.Text == str[0])
                            {
                                this.Invoke(new EventHandler(delegate
                                {
                                    if (str[1] == "true")
                                    {
                                        control.Tag = "1";
                                        control.ForeColor = Color.Red;
                                        control.Enabled = true;
                                    }
                                    else
                                    {
                                        control.Tag = "2";
                                        control.ForeColor = Color.Orange;
                                        control.Enabled = true;
                                       
                                        if (str[2] != null && str[3] != null && str[4] != null)
                                            this.toolTip1.SetToolTip(control, str[2] + System.Environment.NewLine + str[3] + System.Environment.NewLine + str[4]);
                                        else if (str[2] != null && str[3] != null)
                                            this.toolTip1.SetToolTip(control, str[2] + System.Environment.NewLine + str[3]);
                                        else if (str[2] != null && str[4] != null)
                                            this.toolTip1.SetToolTip(control, str[2] + System.Environment.NewLine + str[4]);
                                        else if (str[3] != null && str[4] != null)
                                            this.toolTip1.SetToolTip(control, str[3] + System.Environment.NewLine + str[4]);
                                        else if (str[2] != null)
                                            this.toolTip1.SetToolTip(control, str[2]);
                                        else if (str[3] != null)
                                            this.toolTip1.SetToolTip(control, str[3]);
                                        else if (str[4] != null)
                                            this.toolTip1.SetToolTip(control, str[4]);
                                        else if (str[5] != null)
                                            this.toolTip1.SetToolTip(control, str[5]);

                                    }
                                }));

                            }
                        }
                    }
                }
            }
        }

        private List<string> taskProjects = new List<string>();

        public List<string> TaskProjects
        {
            get { return taskProjects; }
            set
            {
                taskProjects = value;

                ResetControlState();

                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        foreach (string str in taskProjects)
                        {
                            if (control.Text == str)
                            {
                                this.Invoke(new EventHandler(delegate
                                {
                                    control.Tag = "1";
                                    control.ForeColor = Color.Red;
                                    control.Enabled = true;
                                }));

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取被选中的项目名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetSelectedProjects()
        {
            List<string> lstProInfos = new List<string>();

            foreach (Control control in this.Controls)
            {
                if (control.Tag != null)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        if (control.Tag.ToString() == "1")
                        {
                            lstProInfos.Add(control.Text);
                        }
                    }
                }
            }
            return lstProInfos;
        }

        /// <summary>
        /// 清除控制容器中项目信息
        /// </summary>
        public void ResetControlState()
        {
            foreach (Control control in this.Controls)
            {
                //if (control.Tag != null)
                //    if (control.Text != "" && control.Tag != null)
                if (control.Tag != null)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        if (control.Tag.ToString() == "1" || control.Tag.ToString() == "2")
                        {
                            control.Tag = "0";
                            this.Invoke(new EventHandler(delegate
                            {
                                control.ForeColor = Color.Black;
                                control.Enabled = false;
                            }));

                        }
                    }
                }
            }
        }

        private void simpleButton_Click(object sender, EventArgs e)
        {
            Button simpleButton = sender as Button;

            if (simpleButton.Tag as string == "1")
            {
                simpleButton.Tag = "0";

                simpleButton.ForeColor = Color.Black;
            }
            else if (simpleButton.Tag as string == "0")
            {
                simpleButton.Tag = "1";

                simpleButton.ForeColor = Color.Red;
            }
        }
    }
}
