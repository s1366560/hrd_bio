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
    public partial class CalibProjectPage3 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void AssayProInfoDelegate(object sender);
        public event AssayProInfoDelegate AssayProInfoForCombEvent;

        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;

        public CalibProjectPage3()
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
                //this.ResetControlState();
                lstAssayProInfos = value;
                this.BeginInvoke(new EventHandler(delegate
                {
                    ResetControlState();
                    this.toolTip1.RemoveAll();
                    if (lstAssayProInfos.Count >= 65)
                        InitProjectButton(simpleButton1, lstAssayProInfos[64]);
                    if (lstAssayProInfos.Count >= 66)
                        InitProjectButton(simpleButton2, lstAssayProInfos[65]);
                    if (lstAssayProInfos.Count >= 67)
                        InitProjectButton(simpleButton3, lstAssayProInfos[66]);
                    if (lstAssayProInfos.Count >= 68)
                        InitProjectButton(simpleButton4, lstAssayProInfos[67]);
                    if (lstAssayProInfos.Count >= 69)
                        InitProjectButton(simpleButton5, lstAssayProInfos[68]);
                    if (lstAssayProInfos.Count >= 70)
                        InitProjectButton(simpleButton6, lstAssayProInfos[69]);
                    if (lstAssayProInfos.Count >= 71)
                        InitProjectButton(simpleButton7, lstAssayProInfos[70]);
                    if (lstAssayProInfos.Count >= 72)
                        InitProjectButton(simpleButton8, lstAssayProInfos[71]);
                    if (lstAssayProInfos.Count >= 73)
                        InitProjectButton(simpleButton9, lstAssayProInfos[72]);
                    if (lstAssayProInfos.Count >= 74)
                        InitProjectButton(simpleButton10, lstAssayProInfos[73]);
                    if (lstAssayProInfos.Count >= 75)
                        InitProjectButton(simpleButton11, lstAssayProInfos[74]);
                    if (lstAssayProInfos.Count >= 76)
                        InitProjectButton(simpleButton12, lstAssayProInfos[75]);
                    if (lstAssayProInfos.Count >= 77)
                        InitProjectButton(simpleButton13, lstAssayProInfos[76]);
                    if (lstAssayProInfos.Count >= 78)
                        InitProjectButton(simpleButton14, lstAssayProInfos[77]);
                    if (lstAssayProInfos.Count >= 79)
                        InitProjectButton(simpleButton15, lstAssayProInfos[78]);
                    if (lstAssayProInfos.Count >= 80)
                        InitProjectButton(simpleButton16, lstAssayProInfos[79]);
                    if (lstAssayProInfos.Count >= 81)
                        InitProjectButton(simpleButton17, lstAssayProInfos[80]);
                    if (lstAssayProInfos.Count >= 82)
                        InitProjectButton(simpleButton18, lstAssayProInfos[81]);
                    if (lstAssayProInfos.Count >= 83)
                        InitProjectButton(simpleButton19, lstAssayProInfos[82]);
                    if (lstAssayProInfos.Count >= 84)
                        InitProjectButton(simpleButton20, lstAssayProInfos[83]);
                    if (lstAssayProInfos.Count >= 85)
                        InitProjectButton(simpleButton21, lstAssayProInfos[84]);
                    if (lstAssayProInfos.Count >= 86)
                        InitProjectButton(simpleButton22, lstAssayProInfos[85]);
                    if (lstAssayProInfos.Count >= 87)
                        InitProjectButton(simpleButton23, lstAssayProInfos[86]);
                    if (lstAssayProInfos.Count >= 88)
                        InitProjectButton(simpleButton24, lstAssayProInfos[87]);
                    if (lstAssayProInfos.Count >= 89)
                        InitProjectButton(simpleButton25, lstAssayProInfos[88]);
                    if (lstAssayProInfos.Count >= 90)
                        InitProjectButton(simpleButton26, lstAssayProInfos[89]);
                    if (lstAssayProInfos.Count >= 91)
                        InitProjectButton(simpleButton27, lstAssayProInfos[90]);
                    if (lstAssayProInfos.Count >= 92)
                        InitProjectButton(simpleButton28, lstAssayProInfos[91]);
                    if (lstAssayProInfos.Count >= 93)
                        InitProjectButton(simpleButton29, lstAssayProInfos[92]);
                    if (lstAssayProInfos.Count >= 94)
                        InitProjectButton(simpleButton30, lstAssayProInfos[93]);
                    if (lstAssayProInfos.Count >= 95)
                        InitProjectButton(simpleButton31, lstAssayProInfos[94]);
                    if (lstAssayProInfos.Count >= 96)
                        InitProjectButton(simpleButton32, lstAssayProInfos[95]);
                }));
            }

        }
        /// <summary>
        /// 显示项目是否能用和错误信息提示
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="str"></param>
        private void InitProjectButton(Button btn, string[] str)
        {
            btn.Text = str[0];
            this.Invoke(new EventHandler(delegate
            {
                if (str[1] == "true")
                {
                    btn.Tag = "0";
                    btn.ForeColor = Color.Black;
                    btn.Enabled = true;
                }
                else if (str[2] == "该项目没有对应的校准品！")
                {

                    btn.Enabled = false;
                }
                else
                {
                    btn.Tag = "2";
                    btn.ForeColor = Color.Orange;
                    btn.Enabled = true;

                    if (str[2] != null && str[3] != null && str[4] != null)
                        this.toolTip1.SetToolTip(btn, str[2] + System.Environment.NewLine + str[3] + System.Environment.NewLine + str[4]);
                    else if (str[2] != null && str[3] != null)
                        this.toolTip1.SetToolTip(btn, str[2] + System.Environment.NewLine + str[3]);
                    else if (str[2] != null && str[4] != null)
                        this.toolTip1.SetToolTip(btn, str[2] + System.Environment.NewLine + str[4]);
                    else if (str[3] != null && str[4] != null)
                        this.toolTip1.SetToolTip(btn, str[3] + System.Environment.NewLine + str[4]);
                    else if (str[2] != null)
                        this.toolTip1.SetToolTip(btn, str[2]);
                    else if (str[3] != null)
                        this.toolTip1.SetToolTip(btn, str[3]);
                    else if (str[4] != null)
                        this.toolTip1.SetToolTip(btn, str[4]);
                    else if (str[5] != null)
                        this.toolTip1.SetToolTip(btn, str[5]);
                }
            }));
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
                    if (control.Text != null && control.Text != string.Empty && control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        foreach (string str in selectedProjects)
                        {
                            if (control.Text == str)
                            {
                                if (control.ForeColor != Color.Orange)
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
                if (control.Text != "" && control.Text != string.Empty)
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
        /// 清除控制容器中所有项目信息
        /// </summary>
        public void ResetControlState()
        {
            foreach (Control control in this.Controls)
            {
                if (control.Text != "" && control.Text != string.Empty)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        control.Tag = "0";
                        this.Invoke(new EventHandler(delegate
                        {
                            control.Text = null;
                            control.ForeColor = Color.Black;
                            control.Enabled = false;
                        }));
                    }
                }
            }
        }

        private void simpleButton_Click(object sender, EventArgs e)
        {
            Button simpleButton = sender as Button;

            if (simpleButton.Tag as string == "0")
            {
                simpleButton.Tag = "1";

                simpleButton.ForeColor = Color.Red;
            }
            else if (simpleButton.Tag as string == "1")
            {
                simpleButton.Tag = "0";

                simpleButton.ForeColor = Color.Black;
            }
        }
    }
}
