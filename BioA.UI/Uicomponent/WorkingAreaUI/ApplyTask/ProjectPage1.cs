﻿using System;
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
    public enum ProjectPageEnum
    {
        WorkingArea = 0,
        CombProject = 1,
        CalcProject = 2
    }

    public partial class ProjectPage1 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void AssayProInfoDelegate(object sender);
        public event AssayProInfoDelegate AssayProInfoForCombEvent;

        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;

        public ProjectPage1()
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


        private ProjectPageEnum projectPageInfo;
        /// <summary>
        /// 确定是哪个二级菜单调用
        /// </summary>
        public ProjectPageEnum ProjectPageInfo
        {
            get { return projectPageInfo; }
            set
            {
                projectPageInfo = value;

                switch (projectPageInfo)
                {
                    case ProjectPageEnum.WorkingArea:
                        break;
                    case ProjectPageEnum.CombProject:
                        if (AssayProInfoForCombEvent != null)
                            AssayProInfoForCombEvent(new CommunicationEntity("ProjectPageinfo", null));
                        break;
                    case ProjectPageEnum.CalcProject:
                        if (AssayProInfoForCombEvent != null)
                            AssayProInfoForCombEvent(new CommunicationEntity("ProjectPageinfoForCalc", null));
                        break;
                    default:
                        break;
                }
            }
        }



        private List<string[]> lstAssayProInfos = new List<string[]>();

        public List<string[]> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                BeginInvoke(new Action(AllAssatProInfo));
            }
                    
        }

        private void AllAssatProInfo()
        {
            
            ResetControlState();
            this.toolTip1.RemoveAll();
            if (lstAssayProInfos.Count >= 1)
                InitProjectButton(simpleButton1, lstAssayProInfos[0]);
            if (lstAssayProInfos.Count >= 2)
                InitProjectButton(simpleButton2, lstAssayProInfos[1]);
            if (lstAssayProInfos.Count >= 3)
                InitProjectButton(simpleButton3, lstAssayProInfos[2]);
            if (lstAssayProInfos.Count >= 4)
                InitProjectButton(simpleButton4, lstAssayProInfos[3]);
            if (lstAssayProInfos.Count >= 5)
                InitProjectButton(simpleButton5, lstAssayProInfos[4]);
            if (lstAssayProInfos.Count >= 6)
                InitProjectButton(simpleButton6, lstAssayProInfos[5]);
            if (lstAssayProInfos.Count >= 7)
                InitProjectButton(simpleButton7, lstAssayProInfos[6]);
            if (lstAssayProInfos.Count >= 8)
                InitProjectButton(simpleButton8, lstAssayProInfos[7]);
            if (lstAssayProInfos.Count >= 9)
                InitProjectButton(simpleButton9, lstAssayProInfos[8]);
            if (lstAssayProInfos.Count >= 10)
                InitProjectButton(simpleButton10, lstAssayProInfos[9]);
            if (lstAssayProInfos.Count >= 11)
                InitProjectButton(simpleButton11, lstAssayProInfos[10]);
            if (lstAssayProInfos.Count >= 12)
                InitProjectButton(simpleButton12, lstAssayProInfos[11]);
            if (lstAssayProInfos.Count >= 13)
                InitProjectButton(simpleButton13, lstAssayProInfos[12]);
            if (lstAssayProInfos.Count >= 14)
                InitProjectButton(simpleButton14, lstAssayProInfos[13]);
            if (lstAssayProInfos.Count >= 15)
                InitProjectButton(simpleButton15, lstAssayProInfos[14]);
            if (lstAssayProInfos.Count >= 16)
                InitProjectButton(simpleButton16, lstAssayProInfos[15]);
            if (lstAssayProInfos.Count >= 17)
                InitProjectButton(simpleButton17, lstAssayProInfos[16]);
            if (lstAssayProInfos.Count >= 18)
                InitProjectButton(simpleButton18, lstAssayProInfos[17]);
            if (lstAssayProInfos.Count >= 19)
                InitProjectButton(simpleButton19, lstAssayProInfos[18]);
            if (lstAssayProInfos.Count >= 20)
                InitProjectButton(simpleButton20, lstAssayProInfos[19]);
            if (lstAssayProInfos.Count >= 21)
                InitProjectButton(simpleButton21, lstAssayProInfos[20]);
            if (lstAssayProInfos.Count >= 22)
                InitProjectButton(simpleButton22, lstAssayProInfos[21]);
            if (lstAssayProInfos.Count >= 23)
                InitProjectButton(simpleButton23, lstAssayProInfos[22]);
            if (lstAssayProInfos.Count >= 24)
                InitProjectButton(simpleButton24, lstAssayProInfos[23]);
            if (lstAssayProInfos.Count >= 25)
                InitProjectButton(simpleButton25, lstAssayProInfos[24]);
            if (lstAssayProInfos.Count >= 26)
                InitProjectButton(simpleButton26, lstAssayProInfos[25]);
            if (lstAssayProInfos.Count >= 27)
                InitProjectButton(simpleButton27, lstAssayProInfos[26]);
            if (lstAssayProInfos.Count >= 28)
                InitProjectButton(simpleButton28, lstAssayProInfos[27]);
            if (lstAssayProInfos.Count >= 29)
                InitProjectButton(simpleButton29, lstAssayProInfos[28]);
            if (lstAssayProInfos.Count >= 30)
                InitProjectButton(simpleButton30, lstAssayProInfos[29]);
            if (lstAssayProInfos.Count >= 31)
                InitProjectButton(simpleButton31, lstAssayProInfos[30]);
            if (lstAssayProInfos.Count >= 32)
                InitProjectButton(simpleButton32, lstAssayProInfos[31]);
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
                                if (control.ForeColor == Color.Black)
                                {
                                    control.Tag = "1";

                                    this.Invoke(new EventHandler(delegate
                                    {
                                        control.ForeColor = Color.Red;
                                    }));
                                }
                                else if(control.ForeColor == Color.Red)
                                {
                                    control.Tag = "0";

                                    this.Invoke(new EventHandler(delegate
                                    {
                                        control.ForeColor = Color.Black;
                                    }));
                                }
                                else if(control.ForeColor == Color.Orange)
                                {
                                    MessageBox.Show("项目:" + control.Text + "参数存在问题，不可下任务！");
                                    return;
                                }
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
                        if(control.Tag.ToString() == "1")
                        {
                            lstProInfos.Add(control.Text);
                        }
                    }
                }
            }
            return lstProInfos;
        }

        /// <summary>
        /// 清空控制容器中的所有项目信息
        /// </summary>
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

        private void ProjectPage_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton_Click(object sender, EventArgs e)
        {
            Button simpleButton = sender as Button;

            if (simpleButton.Tag as string == "1")
            {
                simpleButton.Tag = "0";

                simpleButton.ForeColor = Color.Black;
            }
            else if (simpleButton.Tag as string == "0" || simpleButton.Tag == null)
            {
                simpleButton.Tag = "1";

                simpleButton.ForeColor = Color.Red;
            }


        }
    }
}
