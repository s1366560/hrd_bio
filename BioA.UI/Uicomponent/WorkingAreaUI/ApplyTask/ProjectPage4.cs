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
    public partial class ProjectPage4 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void AssayProInfoDelegate(object sender);
        public event AssayProInfoDelegate AssayProInfoForCombEvent;

        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;

        public ProjectPage4()
        {
            

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
                        if (c.GetType() == typeof(DevExpress.XtraEditors.SimpleButton) && c.Text == proName)
                        {
                            c.Tag = "1";

                            this.Invoke(new EventHandler(delegate { c.ForeColor = Color.Red; }));
                        }
                    }
                }
            }
        }

        private ProjectPageEnum projectPageInfo;

        public ProjectPageEnum ProjectPageInfo
        {
            get { return projectPageInfo; }
            set
            {
                projectPageInfo = value;
            }
        }

        private List<string[]> lstAssayProInfos = new List<string[]>();

        public List<string[]> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                BeginInvoke(new Action(AllArrayProInfo));
            }
                
        }

        private void AllArrayProInfo()
        {
            InitializeComponent();
            ResetControlState();
            this.toolTip1.RemoveAll();
            if (lstAssayProInfos.Count >= 97)
                InitProjectButton(simpleButton1, lstAssayProInfos[96]);
            if (lstAssayProInfos.Count >= 98)
                InitProjectButton(simpleButton2, lstAssayProInfos[97]);
            if (lstAssayProInfos.Count >= 99)
                InitProjectButton(simpleButton3, lstAssayProInfos[98]);
            if (lstAssayProInfos.Count >= 100)
                InitProjectButton(simpleButton4, lstAssayProInfos[99]);
            if (lstAssayProInfos.Count >= 101)
                InitProjectButton(simpleButton5, lstAssayProInfos[100]);
            if (lstAssayProInfos.Count >= 102)
                InitProjectButton(simpleButton6, lstAssayProInfos[101]);
            if (lstAssayProInfos.Count >= 103)
                InitProjectButton(simpleButton7, lstAssayProInfos[102]);
            if (lstAssayProInfos.Count >= 104)
                InitProjectButton(simpleButton8, lstAssayProInfos[103]);
            if (lstAssayProInfos.Count >= 105)
                InitProjectButton(simpleButton9, lstAssayProInfos[104]);
            if (lstAssayProInfos.Count >= 106)
                InitProjectButton(simpleButton10, lstAssayProInfos[105]);
            if (lstAssayProInfos.Count >= 107)
                InitProjectButton(simpleButton11, lstAssayProInfos[106]);
            if (lstAssayProInfos.Count >= 108)
                InitProjectButton(simpleButton12, lstAssayProInfos[107]);
            if (lstAssayProInfos.Count >= 109)
                InitProjectButton(simpleButton13, lstAssayProInfos[108]);
            if (lstAssayProInfos.Count >= 110)
                InitProjectButton(simpleButton14, lstAssayProInfos[109]);
            if (lstAssayProInfos.Count >= 111)
                InitProjectButton(simpleButton15, lstAssayProInfos[110]);
            if (lstAssayProInfos.Count >= 112)
                InitProjectButton(simpleButton16, lstAssayProInfos[111]);
            if (lstAssayProInfos.Count >= 113)
                InitProjectButton(simpleButton17, lstAssayProInfos[112]);
            if (lstAssayProInfos.Count >= 114)
                InitProjectButton(simpleButton18, lstAssayProInfos[113]);
            if (lstAssayProInfos.Count >= 115)
                InitProjectButton(simpleButton19, lstAssayProInfos[114]);
            if (lstAssayProInfos.Count >= 116)
                InitProjectButton(simpleButton20, lstAssayProInfos[115]);
            if (lstAssayProInfos.Count >= 117)
                InitProjectButton(simpleButton21, lstAssayProInfos[116]);
            if (lstAssayProInfos.Count >= 118)
                InitProjectButton(simpleButton22, lstAssayProInfos[117]);
            if (lstAssayProInfos.Count >= 119)
                InitProjectButton(simpleButton23, lstAssayProInfos[118]);
            if (lstAssayProInfos.Count >= 120)
                InitProjectButton(simpleButton24, lstAssayProInfos[119]);
            if (lstAssayProInfos.Count >= 121)
                InitProjectButton(simpleButton25, lstAssayProInfos[120]);
            if (lstAssayProInfos.Count >= 122)
                InitProjectButton(simpleButton26, lstAssayProInfos[121]);
            if (lstAssayProInfos.Count >= 123)
                InitProjectButton(simpleButton27, lstAssayProInfos[122]);
            if (lstAssayProInfos.Count >= 124)
                InitProjectButton(simpleButton28, lstAssayProInfos[123]);
            if (lstAssayProInfos.Count >= 125)
                InitProjectButton(simpleButton29, lstAssayProInfos[124]);
            if (lstAssayProInfos.Count >= 126)
                InitProjectButton(simpleButton30, lstAssayProInfos[125]);
            if (lstAssayProInfos.Count >= 127)
                InitProjectButton(simpleButton31, lstAssayProInfos[126]);
            if (lstAssayProInfos.Count >= 128)
                InitProjectButton(simpleButton32, lstAssayProInfos[127]);
        }

        private void InitProjectButton(Button btn, string[] projectInfo)
        {
            btn.Text = projectInfo[0];
            if (projectInfo[1] == "false")
            {
                btn.Tag = "2";
                btn.ForeColor = Color.Orange;
                btn.Enabled = true;
                if (projectInfo[2] != null && projectInfo[3] != null && projectInfo[4] != null)
                    this.toolTip1.SetToolTip(btn, projectInfo[2] + System.Environment.NewLine + projectInfo[3] + System.Environment.NewLine + projectInfo[4]);
                else if (projectInfo[2] != null && projectInfo[3] != null)
                    this.toolTip1.SetToolTip(btn, projectInfo[2] + System.Environment.NewLine + projectInfo[3]);
                else if (projectInfo[2] != null && projectInfo[4] != null)
                    this.toolTip1.SetToolTip(btn, projectInfo[2] + System.Environment.NewLine + projectInfo[4]);
                else if (projectInfo[3] != null && projectInfo[4] != null)
                    this.toolTip1.SetToolTip(btn, projectInfo[3] + System.Environment.NewLine + projectInfo[4]);
                else if (projectInfo[2] != null)
                    this.toolTip1.SetToolTip(btn, projectInfo[2]);
                else if (projectInfo[3] != null)
                    this.toolTip1.SetToolTip(btn, projectInfo[3]);
                else if (projectInfo[4] != null)
                    this.toolTip1.SetToolTip(btn, projectInfo[4]);
            }
            else
            {
                btn.Tag = "0";
                btn.ForeColor = Color.Black;
                btn.Enabled = true;
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
                    control.Text = null;
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
        }
    }
}
