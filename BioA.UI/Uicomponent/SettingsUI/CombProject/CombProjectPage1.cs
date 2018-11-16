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

namespace BioA.UI
{
    public partial class CombProjectPage1 : DevExpress.XtraEditors.XtraUserControl
    {
        public CombProjectPage1()
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
                }));
            }
                
        }

        private List<string> selectedProjects = new List<string>();
        /// <summary>
        /// 还原项目，设置被选中项目
        /// </summary>
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
                if (control.GetType() == typeof(System.Windows.Forms.Button))
                {
                    if (control.Tag.ToString() == "1")
                    {
                        control.Tag = "0";
                        this.Invoke(new EventHandler(delegate {
                            control.ForeColor = Color.Black;
                        }));
                        
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
            else
            {
                simpleButton.Tag = "1";

                simpleButton.ForeColor = Color.Red;
            }

        }
    }
}
