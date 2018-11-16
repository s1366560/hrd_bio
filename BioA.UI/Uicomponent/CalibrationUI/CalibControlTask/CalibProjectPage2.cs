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
    public partial class CalibProjectPage2 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void AssayProInfoDelegate(object sender);
        public event AssayProInfoDelegate AssayProInfoForCombEvent;

        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;

        public CalibProjectPage2()
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
                    if (lstAssayProInfos.Count >= 33)
                        InitProjectButton(simpleButton1, lstAssayProInfos[32]);
                    if (lstAssayProInfos.Count >= 34)
                        InitProjectButton(simpleButton2, lstAssayProInfos[33]);
                    if (lstAssayProInfos.Count >= 35)
                        InitProjectButton(simpleButton3, lstAssayProInfos[34]);
                    if (lstAssayProInfos.Count >= 36)
                        InitProjectButton(simpleButton4, lstAssayProInfos[35]);
                    if (lstAssayProInfos.Count >= 37)
                        InitProjectButton(simpleButton5, lstAssayProInfos[36]);
                    if (lstAssayProInfos.Count >= 38)
                        InitProjectButton(simpleButton6, lstAssayProInfos[37]);
                    if (lstAssayProInfos.Count >= 39)
                        InitProjectButton(simpleButton7, lstAssayProInfos[38]);
                    if (lstAssayProInfos.Count >= 40)
                        InitProjectButton(simpleButton8, lstAssayProInfos[39]);
                    if (lstAssayProInfos.Count >= 41)
                        InitProjectButton(simpleButton9, lstAssayProInfos[40]);
                    if (lstAssayProInfos.Count >= 42)
                        InitProjectButton(simpleButton10, lstAssayProInfos[41]);
                    if (lstAssayProInfos.Count >= 43)
                        InitProjectButton(simpleButton11, lstAssayProInfos[42]);
                    if (lstAssayProInfos.Count >= 44)
                        InitProjectButton(simpleButton12, lstAssayProInfos[43]);
                    if (lstAssayProInfos.Count >= 45)
                        InitProjectButton(simpleButton13, lstAssayProInfos[44]);
                    if (lstAssayProInfos.Count >= 46)
                        InitProjectButton(simpleButton14, lstAssayProInfos[45]);
                    if (lstAssayProInfos.Count >= 47)
                        InitProjectButton(simpleButton15, lstAssayProInfos[46]);
                    if (lstAssayProInfos.Count >= 48)
                        InitProjectButton(simpleButton16, lstAssayProInfos[47]);
                    if (lstAssayProInfos.Count >= 49)
                        InitProjectButton(simpleButton17, lstAssayProInfos[48]);
                    if (lstAssayProInfos.Count >= 50)
                        InitProjectButton(simpleButton18, lstAssayProInfos[49]);
                    if (lstAssayProInfos.Count >= 51)
                        InitProjectButton(simpleButton19, lstAssayProInfos[50]);
                    if (lstAssayProInfos.Count >= 52)
                        InitProjectButton(simpleButton20, lstAssayProInfos[51]);
                    if (lstAssayProInfos.Count >= 53)
                        InitProjectButton(simpleButton21, lstAssayProInfos[52]);
                    if (lstAssayProInfos.Count >= 54)
                        InitProjectButton(simpleButton22, lstAssayProInfos[53]);
                    if (lstAssayProInfos.Count >= 55)
                        InitProjectButton(simpleButton23, lstAssayProInfos[54]);
                    if (lstAssayProInfos.Count >= 56)
                        InitProjectButton(simpleButton24, lstAssayProInfos[55]);
                    if (lstAssayProInfos.Count >= 57)
                        InitProjectButton(simpleButton25, lstAssayProInfos[56]);
                    if (lstAssayProInfos.Count >= 58)
                        InitProjectButton(simpleButton26, lstAssayProInfos[57]);
                    if (lstAssayProInfos.Count >= 59)
                        InitProjectButton(simpleButton27, lstAssayProInfos[58]);
                    if (lstAssayProInfos.Count >= 60)
                        InitProjectButton(simpleButton28, lstAssayProInfos[59]);
                    if (lstAssayProInfos.Count >= 61)
                        InitProjectButton(simpleButton29, lstAssayProInfos[60]);
                    if (lstAssayProInfos.Count >= 62)
                        InitProjectButton(simpleButton30, lstAssayProInfos[61]);
                    if (lstAssayProInfos.Count >= 63)
                        InitProjectButton(simpleButton31, lstAssayProInfos[62]);
                    if (lstAssayProInfos.Count >= 64)
                        InitProjectButton(simpleButton32, lstAssayProInfos[63]);
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
                            if (control.Text == str )
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
