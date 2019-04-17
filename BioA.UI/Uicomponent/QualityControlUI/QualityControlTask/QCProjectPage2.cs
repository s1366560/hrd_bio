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
    public partial class QCProjectPage2 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;
        public QCProjectPage2()
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
                this.Invoke(new EventHandler(delegate
                {
                    simpleButton1.Text = lstAssayProInfos.Count >= 49 ? lstAssayProInfos[48] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 50 ? lstAssayProInfos[49] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 51 ? lstAssayProInfos[50] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 52 ? lstAssayProInfos[51] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 53 ? lstAssayProInfos[52] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 54 ? lstAssayProInfos[53] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 55 ? lstAssayProInfos[54] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 56 ? lstAssayProInfos[55] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 57 ? lstAssayProInfos[56] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 58 ? lstAssayProInfos[57] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 59 ? lstAssayProInfos[58] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 60 ? lstAssayProInfos[59] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 61 ? lstAssayProInfos[60] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 62 ? lstAssayProInfos[61] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 63 ? lstAssayProInfos[62] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 64 ? lstAssayProInfos[63] : "";
                    simpleButton17.Text = lstAssayProInfos.Count >= 65 ? lstAssayProInfos[64] : "";
                    simpleButton18.Text = lstAssayProInfos.Count >= 66 ? lstAssayProInfos[65] : "";
                    simpleButton19.Text = lstAssayProInfos.Count >= 67 ? lstAssayProInfos[66] : "";
                    simpleButton20.Text = lstAssayProInfos.Count >= 68 ? lstAssayProInfos[67] : "";
                    simpleButton21.Text = lstAssayProInfos.Count >= 69 ? lstAssayProInfos[68] : "";
                    simpleButton22.Text = lstAssayProInfos.Count >= 70 ? lstAssayProInfos[69] : "";
                    simpleButton23.Text = lstAssayProInfos.Count >= 71 ? lstAssayProInfos[70] : "";
                    simpleButton24.Text = lstAssayProInfos.Count >= 72 ? lstAssayProInfos[71] : "";
                    simpleButton25.Text = lstAssayProInfos.Count >= 73 ? lstAssayProInfos[72] : "";
                    simpleButton26.Text = lstAssayProInfos.Count >= 74 ? lstAssayProInfos[73] : "";
                    simpleButton27.Text = lstAssayProInfos.Count >= 75 ? lstAssayProInfos[74] : "";
                    simpleButton28.Text = lstAssayProInfos.Count >= 76 ? lstAssayProInfos[75] : "";
                    simpleButton29.Text = lstAssayProInfos.Count >= 77 ? lstAssayProInfos[76] : "";
                    simpleButton30.Text = lstAssayProInfos.Count >= 78 ? lstAssayProInfos[77] : "";
                    simpleButton31.Text = lstAssayProInfos.Count >= 79 ? lstAssayProInfos[78] : "";
                    simpleButton32.Text = lstAssayProInfos.Count >= 80 ? lstAssayProInfos[79] : "";
                    simpleButton33.Text = lstAssayProInfos.Count >= 81 ? lstAssayProInfos[80] : "";
                    simpleButton34.Text = lstAssayProInfos.Count >= 82 ? lstAssayProInfos[81] : "";
                    simpleButton35.Text = lstAssayProInfos.Count >= 83 ? lstAssayProInfos[82] : "";
                    simpleButton36.Text = lstAssayProInfos.Count >= 84 ? lstAssayProInfos[83] : "";
                    simpleButton37.Text = lstAssayProInfos.Count >= 85 ? lstAssayProInfos[84] : "";
                    simpleButton38.Text = lstAssayProInfos.Count >= 86 ? lstAssayProInfos[85] : "";
                    simpleButton39.Text = lstAssayProInfos.Count >= 87 ? lstAssayProInfos[86] : "";
                    simpleButton40.Text = lstAssayProInfos.Count >= 88 ? lstAssayProInfos[87] : "";
                    simpleButton41.Text = lstAssayProInfos.Count >= 89 ? lstAssayProInfos[88] : "";
                    simpleButton42.Text = lstAssayProInfos.Count >= 90 ? lstAssayProInfos[89] : "";
                    simpleButton43.Text = lstAssayProInfos.Count >= 91 ? lstAssayProInfos[90] : "";
                    simpleButton44.Text = lstAssayProInfos.Count >= 92 ? lstAssayProInfos[91] : "";
                    simpleButton45.Text = lstAssayProInfos.Count >= 93 ? lstAssayProInfos[92] : "";
                    simpleButton46.Text = lstAssayProInfos.Count >= 94 ? lstAssayProInfos[93] : "";
                    simpleButton47.Text = lstAssayProInfos.Count >= 95 ? lstAssayProInfos[94] : "";
                    simpleButton48.Text = lstAssayProInfos.Count >= 96 ? lstAssayProInfos[95] : "";
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
                this.ClearLastSelectedInfo();
                foreach (Control control in this.Controls)
                {
                    if (!string.IsNullOrEmpty(control.Text.Trim()))
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
        /// <summary>
        /// 清除上一次缓存下的选种项目信息
        /// </summary>
        public void ClearLastSelectedInfo()
        {
            this.Invoke(new EventHandler(delegate
            {
                foreach (Control control in this.Controls)
                {
                    //if (control.Tag != null)
                    //    if (control.Text != "" && control.Tag != null)
                    if (control.Tag != null && !string.IsNullOrEmpty(control.Text.Trim()))
                    {
                        control.Tag = "0";
                        control.ForeColor = Color.Black;
                        control.Enabled = false;
                    }
                }
            }));
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
        /// <summary>
        /// 项目按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
