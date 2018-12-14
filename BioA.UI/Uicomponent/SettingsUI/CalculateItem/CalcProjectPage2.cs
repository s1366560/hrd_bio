using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using DevExpress.XtraEditors;

namespace BioA.UI
{
    public partial class CalcProjectPage2 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;

        public CalcProjectPage2()
        {
            InitializeComponent();
        }

        private List<string> lstProNamesForComb;
        /// <summary>
        /// 处理组合项目点击
        /// </summary>
        public List<string> LstProNamesForComb
        {
            get { return lstProNamesForComb; }
            set
            {
                lstProNamesForComb = value;

                foreach (string proName in lstProNamesForComb)
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType() == typeof(System.Windows.Forms.Button) && c.Text == proName)
                        {
                            c.Tag = "1";

                            this.Invoke(new EventHandler(delegate { c.ForeColor = Color.Red; }));
                        }
                    }
                }
            }
        }

        private List<string> lstAssayProInfos = new List<string>();

        public List<string> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.Invoke(new EventHandler(delegate{
                    ResetControlState();
                    simpleButton1.Text = lstAssayProInfos.Count >= 33 ? lstAssayProInfos[32] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 34 ? lstAssayProInfos[33] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 35 ? lstAssayProInfos[34] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 36 ? lstAssayProInfos[35] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 37 ? lstAssayProInfos[36] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 38 ? lstAssayProInfos[37] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 39 ? lstAssayProInfos[38] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 40 ? lstAssayProInfos[39] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 41 ? lstAssayProInfos[40] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 42 ? lstAssayProInfos[41] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 43 ? lstAssayProInfos[42] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 44 ? lstAssayProInfos[43] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 45 ? lstAssayProInfos[44] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 46 ? lstAssayProInfos[45] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 47 ? lstAssayProInfos[46] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 48 ? lstAssayProInfos[47] : "";
                    simpleButton17.Text = lstAssayProInfos.Count >= 49 ? lstAssayProInfos[48] : "";
                    simpleButton18.Text = lstAssayProInfos.Count >= 50 ? lstAssayProInfos[49] : "";
                    simpleButton19.Text = lstAssayProInfos.Count >= 51 ? lstAssayProInfos[50] : "";
                    simpleButton20.Text = lstAssayProInfos.Count >= 52 ? lstAssayProInfos[51] : "";
                    simpleButton21.Text = lstAssayProInfos.Count >= 53 ? lstAssayProInfos[52] : "";
                    simpleButton22.Text = lstAssayProInfos.Count >= 54 ? lstAssayProInfos[53] : "";
                    simpleButton23.Text = lstAssayProInfos.Count >= 55 ? lstAssayProInfos[54] : "";
                    simpleButton24.Text = lstAssayProInfos.Count >= 56 ? lstAssayProInfos[55] : "";
                    simpleButton25.Text = lstAssayProInfos.Count >= 57 ? lstAssayProInfos[56] : "";
                    simpleButton26.Text = lstAssayProInfos.Count >= 58 ? lstAssayProInfos[57] : "";
                    simpleButton27.Text = lstAssayProInfos.Count >= 59 ? lstAssayProInfos[58] : "";
                    simpleButton28.Text = lstAssayProInfos.Count >= 60 ? lstAssayProInfos[59] : "";
                    simpleButton29.Text = lstAssayProInfos.Count >= 61 ? lstAssayProInfos[60] : "";
                    simpleButton30.Text = lstAssayProInfos.Count >= 62 ? lstAssayProInfos[61] : "";
                    simpleButton31.Text = lstAssayProInfos.Count >= 63 ? lstAssayProInfos[62] : "";
                    simpleButton32.Text = lstAssayProInfos.Count >= 64 ? lstAssayProInfos[63] : "";
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

        private List<string> selectedProjectsForComb = new List<string>();
        /// <summary>
        /// 设置被选中项目for组合项目
        /// </summary>
        public List<string> SelectedProjectsForComb
        {
            get { return selectedProjectsForComb; }
            set
            {
                selectedProjectsForComb = value;

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
                if (control.Tag != null)//ly 2018年12月13日
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
        }

        private void ProjectPage_Load(object sender, EventArgs e)
        {

        }


        public void InitialProjectPage(List<AssayProjectInfo> lstAssayProInfos)
        {
            //simpleButton12
        }

        private void simpleButton_Click(object sender, EventArgs e)
        {
            Button simpleButton = sender as Button;
            if (ProjectCallbackInfoEvent != null && simpleButton.Text != string.Empty)
            {
                ProjectCallbackInfoEvent(simpleButton.Text);
            }
        }
    }
}
