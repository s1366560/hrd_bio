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
    public partial class QCProjectPage3 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;
        public QCProjectPage3()
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
                    simpleButton1.Text = lstAssayProInfos.Count >= 97 ? lstAssayProInfos[96] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 98 ? lstAssayProInfos[97] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 99 ? lstAssayProInfos[98] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 100 ? lstAssayProInfos[99] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 101 ? lstAssayProInfos[100] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 102 ? lstAssayProInfos[101] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 103 ? lstAssayProInfos[102] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 104 ? lstAssayProInfos[103] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 105 ? lstAssayProInfos[104] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 106 ? lstAssayProInfos[105] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 107 ? lstAssayProInfos[106] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 108 ? lstAssayProInfos[107] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 109 ? lstAssayProInfos[108] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 110 ? lstAssayProInfos[109] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 111 ? lstAssayProInfos[110] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 112 ? lstAssayProInfos[111] : "";
                    simpleButton17.Text = lstAssayProInfos.Count >= 113 ? lstAssayProInfos[112] : "";
                    simpleButton18.Text = lstAssayProInfos.Count >= 114 ? lstAssayProInfos[113] : "";
                    simpleButton19.Text = lstAssayProInfos.Count >= 115 ? lstAssayProInfos[114] : "";
                    simpleButton20.Text = lstAssayProInfos.Count >= 116 ? lstAssayProInfos[115] : "";
                    simpleButton21.Text = lstAssayProInfos.Count >= 117 ? lstAssayProInfos[116] : "";
                    simpleButton22.Text = lstAssayProInfos.Count >= 118 ? lstAssayProInfos[117] : "";
                    simpleButton23.Text = lstAssayProInfos.Count >= 119 ? lstAssayProInfos[118] : "";
                    simpleButton24.Text = lstAssayProInfos.Count >= 120 ? lstAssayProInfos[119] : "";
                    simpleButton25.Text = lstAssayProInfos.Count >= 121 ? lstAssayProInfos[120] : "";
                    simpleButton26.Text = lstAssayProInfos.Count >= 122 ? lstAssayProInfos[121] : "";
                    simpleButton27.Text = lstAssayProInfos.Count >= 123 ? lstAssayProInfos[122] : "";
                    simpleButton28.Text = lstAssayProInfos.Count >= 124 ? lstAssayProInfos[123] : "";
                    simpleButton29.Text = lstAssayProInfos.Count >= 125 ? lstAssayProInfos[124] : "";
                    simpleButton30.Text = lstAssayProInfos.Count >= 126 ? lstAssayProInfos[125] : "";
                    simpleButton31.Text = lstAssayProInfos.Count >= 127 ? lstAssayProInfos[126] : "";
                    simpleButton32.Text = lstAssayProInfos.Count >= 128 ? lstAssayProInfos[127] : "";
                    simpleButton33.Text = lstAssayProInfos.Count >= 129 ? lstAssayProInfos[128] : "";
                    simpleButton34.Text = lstAssayProInfos.Count >= 130 ? lstAssayProInfos[129] : "";
                    simpleButton35.Text = lstAssayProInfos.Count >= 131 ? lstAssayProInfos[130] : "";
                    simpleButton36.Text = lstAssayProInfos.Count >= 132 ? lstAssayProInfos[131] : "";
                    simpleButton37.Text = lstAssayProInfos.Count >= 133 ? lstAssayProInfos[132] : "";
                    simpleButton38.Text = lstAssayProInfos.Count >= 134 ? lstAssayProInfos[133] : "";
                    simpleButton39.Text = lstAssayProInfos.Count >= 135 ? lstAssayProInfos[134] : "";
                    simpleButton40.Text = lstAssayProInfos.Count >= 136 ? lstAssayProInfos[135] : "";
                    simpleButton41.Text = lstAssayProInfos.Count >= 137 ? lstAssayProInfos[136] : "";
                    simpleButton42.Text = lstAssayProInfos.Count >= 138 ? lstAssayProInfos[137] : "";
                    simpleButton43.Text = lstAssayProInfos.Count >= 139 ? lstAssayProInfos[138] : "";
                    simpleButton44.Text = lstAssayProInfos.Count >= 140 ? lstAssayProInfos[139] : "";
                    simpleButton45.Text = lstAssayProInfos.Count >= 141 ? lstAssayProInfos[140] : "";
                    simpleButton46.Text = lstAssayProInfos.Count >= 142 ? lstAssayProInfos[141] : "";
                    simpleButton47.Text = lstAssayProInfos.Count >= 143 ? lstAssayProInfos[142] : "";
                    simpleButton48.Text = lstAssayProInfos.Count >= 144 ? lstAssayProInfos[143] : "";
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
