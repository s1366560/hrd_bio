using System;
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

namespace BioA.UI
{
    public partial class CombProjectPage3 : DevExpress.XtraEditors.XtraUserControl
    {
        public CombProjectPage3()
        {
            InitializeComponent();

            //projectPageInfo = projectPageEnum;

            
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
                    //ResetControlState();
                    simpleButton1.Text = lstAssayProInfos.Count >= 65 ? lstAssayProInfos[64] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 66 ? lstAssayProInfos[65] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 67 ? lstAssayProInfos[66] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 68 ? lstAssayProInfos[67] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 69 ? lstAssayProInfos[68] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 70 ? lstAssayProInfos[69] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 71 ? lstAssayProInfos[70] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 72 ? lstAssayProInfos[71] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 73 ? lstAssayProInfos[72] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 74 ? lstAssayProInfos[73] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 75 ? lstAssayProInfos[74] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 76 ? lstAssayProInfos[75] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 77 ? lstAssayProInfos[76] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 78 ? lstAssayProInfos[77] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 79 ? lstAssayProInfos[78] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 80 ? lstAssayProInfos[89] : "";
                    simpleButton17.Text = lstAssayProInfos.Count >= 81 ? lstAssayProInfos[80] : "";
                    simpleButton18.Text = lstAssayProInfos.Count >= 82 ? lstAssayProInfos[81] : "";
                    simpleButton19.Text = lstAssayProInfos.Count >= 83 ? lstAssayProInfos[82] : "";
                    simpleButton20.Text = lstAssayProInfos.Count >= 84 ? lstAssayProInfos[83] : "";
                    simpleButton21.Text = lstAssayProInfos.Count >= 85 ? lstAssayProInfos[84] : "";
                    simpleButton22.Text = lstAssayProInfos.Count >= 86 ? lstAssayProInfos[85] : "";
                    simpleButton23.Text = lstAssayProInfos.Count >= 87 ? lstAssayProInfos[86] : "";
                    simpleButton24.Text = lstAssayProInfos.Count >= 88 ? lstAssayProInfos[87] : "";
                    simpleButton25.Text = lstAssayProInfos.Count >= 89 ? lstAssayProInfos[88] : "";
                    simpleButton26.Text = lstAssayProInfos.Count >= 90 ? lstAssayProInfos[99] : "";
                    simpleButton27.Text = lstAssayProInfos.Count >= 91 ? lstAssayProInfos[90] : "";
                    simpleButton28.Text = lstAssayProInfos.Count >= 92 ? lstAssayProInfos[91] : "";
                    simpleButton29.Text = lstAssayProInfos.Count >= 93 ? lstAssayProInfos[92] : "";
                    simpleButton30.Text = lstAssayProInfos.Count >= 94 ? lstAssayProInfos[93] : "";
                    simpleButton31.Text = lstAssayProInfos.Count >= 95 ? lstAssayProInfos[94] : "";
                    simpleButton32.Text = lstAssayProInfos.Count >= 96 ? lstAssayProInfos[95] : "";
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
                if (control.Tag != null)
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
                            control.ForeColor = Color.Black;
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

            if (simpleButton.Tag as string == "1")
            {
                simpleButton.Tag = "0";

                simpleButton.ForeColor = Color.Black;
            }
            else if(simpleButton.Tag as string == "0")
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
