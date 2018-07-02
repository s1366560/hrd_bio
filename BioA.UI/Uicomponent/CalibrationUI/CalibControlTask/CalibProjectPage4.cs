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
    public partial class CalibProjectPage4 : DevExpress.XtraEditors.XtraUserControl
    {
        public delegate void AssayProInfoDelegate(object sender);
        public event AssayProInfoDelegate AssayProInfoForCombEvent;

        public delegate void ProjectCallbackInfoDelegate(string strProName);
        public event ProjectCallbackInfoDelegate ProjectCallbackInfoEvent;

        public CalibProjectPage4()
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

        //private List<string> lstAssayProInfos = new List<string>();
        //public List<string> LstAssayProInfos
        //{
        //    get { return lstAssayProInfos; }
        //    set
        //    {
        //        lstAssayProInfos = value;
        //        this.Invoke(new EventHandler(delegate{
        //            ResetControlState();
        //            simpleButton1.Text = lstAssayProInfos.Count >= 97 ? lstAssayProInfos[96] : "";
        //            simpleButton2.Text = lstAssayProInfos.Count >= 98 ? lstAssayProInfos[97] : "";
        //            simpleButton3.Text = lstAssayProInfos.Count >= 99 ? lstAssayProInfos[98] : "";
        //            simpleButton4.Text = lstAssayProInfos.Count >= 100 ? lstAssayProInfos[99] : "";
        //            simpleButton5.Text = lstAssayProInfos.Count >= 101 ? lstAssayProInfos[100] : "";
        //            simpleButton6.Text = lstAssayProInfos.Count >= 102 ? lstAssayProInfos[101] : "";
        //            simpleButton7.Text = lstAssayProInfos.Count >= 103 ? lstAssayProInfos[102] : "";
        //            simpleButton8.Text = lstAssayProInfos.Count >= 104 ? lstAssayProInfos[103] : "";
        //            simpleButton9.Text = lstAssayProInfos.Count >= 105 ? lstAssayProInfos[104] : "";
        //            simpleButton10.Text = lstAssayProInfos.Count >= 106 ? lstAssayProInfos[105] : "";
        //            simpleButton11.Text = lstAssayProInfos.Count >= 107 ? lstAssayProInfos[106] : "";
        //            simpleButton12.Text = lstAssayProInfos.Count >= 108 ? lstAssayProInfos[107] : "";
        //            simpleButton13.Text = lstAssayProInfos.Count >= 109 ? lstAssayProInfos[108] : "";
        //            simpleButton14.Text = lstAssayProInfos.Count >= 110 ? lstAssayProInfos[109] : "";
        //            simpleButton15.Text = lstAssayProInfos.Count >= 111 ? lstAssayProInfos[110] : "";
        //            simpleButton16.Text = lstAssayProInfos.Count >= 112 ? lstAssayProInfos[111] : "";
        //            simpleButton17.Text = lstAssayProInfos.Count >= 113 ? lstAssayProInfos[112] : "";
        //            simpleButton18.Text = lstAssayProInfos.Count >= 114 ? lstAssayProInfos[113] : "";
        //            simpleButton19.Text = lstAssayProInfos.Count >= 115 ? lstAssayProInfos[114] : "";
        //            simpleButton20.Text = lstAssayProInfos.Count >= 116 ? lstAssayProInfos[115] : "";
        //            simpleButton21.Text = lstAssayProInfos.Count >= 117 ? lstAssayProInfos[116] : "";
        //            simpleButton22.Text = lstAssayProInfos.Count >= 118 ? lstAssayProInfos[117] : "";
        //            simpleButton23.Text = lstAssayProInfos.Count >= 119 ? lstAssayProInfos[118] : "";
        //            simpleButton24.Text = lstAssayProInfos.Count >= 110 ? lstAssayProInfos[119] : "";
        //            simpleButton25.Text = lstAssayProInfos.Count >= 121 ? lstAssayProInfos[110] : "";
        //            simpleButton26.Text = lstAssayProInfos.Count >= 122 ? lstAssayProInfos[121] : "";
        //            simpleButton27.Text = lstAssayProInfos.Count >= 123 ? lstAssayProInfos[122] : "";
        //            simpleButton28.Text = lstAssayProInfos.Count >= 124 ? lstAssayProInfos[123] : "";
        //            simpleButton29.Text = lstAssayProInfos.Count >= 125 ? lstAssayProInfos[124] : "";
        //            simpleButton30.Text = lstAssayProInfos.Count >= 126 ? lstAssayProInfos[125] : "";
        //            simpleButton31.Text = lstAssayProInfos.Count >= 127 ? lstAssayProInfos[126] : "";
        //            simpleButton32.Text = lstAssayProInfos.Count >= 128 ? lstAssayProInfos[127] : "";
        //        }));
        //    }
                
        //}

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

        private List<string[]> selectedProjects1 = new List<string[]>();
        public List<string[]> SelectedProjects1
        {
            get { return selectedProjects1; }
            set
            {
                selectedProjects1 = value;

                this.ResetControlState();
                this.Invoke(new EventHandler(delegate { this.toolTip1.RemoveAll(); }));

                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        foreach (string[] str in selectedProjects1)
                        {
                            if (control.Text == str[0])
                            {
                                this.Invoke(new EventHandler(delegate
                                {
                                    if (str[1] == "true")
                                    {
                                        control.Tag = "0";
                                        control.ForeColor = Color.Black;
                                        control.Enabled = true;
                                    }
                                    else if (str[2] == "该项目没有对应的校准品！")
                                    {
                                        control.Enabled = false;
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
