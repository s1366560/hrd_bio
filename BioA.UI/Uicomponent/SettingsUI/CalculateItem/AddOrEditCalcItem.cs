using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BioA.UI;
using System.Text.RegularExpressions;
using BioA.Common.IO;
using BioA.Common;

namespace BioA.UI
{
    public partial class AddOrEditCalcItem : DevExpress.XtraEditors.XtraForm
    {
        CalcProjectPage1 calcProjectPage1;
        CalcProjectPage2 calcProjectPage2;
        CalcProjectPage3 calcProjectPage3;
        CalcProjectPage4 calcProjectPage4;


        CalcProjectInfo calcProInfoForEdit = new CalcProjectInfo();
        public CalcProjectInfo CalcProInfoForEdit
        {
            get { return calcProInfoForEdit; }
            set
            {
                calcProInfoForEdit = value;
                
            }
        }

        public AddOrEditCalcItem()
        {
            InitializeComponent();

            this.ControlBox = false;

            
        }

        private void ProjectCallbackInfo_Event(string strProInfo)
        {
            txtCalcFormula.Text += "[" + strProInfo + "]";
        }

        public void LoadData()
        {
            SendService(new CommunicationEntity("QueryProjectResultUnits", null));

        }

        private List<string> units = new List<string>();

        public List<string> Units
        {
            get { return units; }
            set
            {
                units = value;

                this.Invoke(new EventHandler(delegate {
                    cboUnit.Properties.Items.AddRange(units);
                }));
            }
        }


        private List<string> projectNames = new List<string>();

        public List<string> ProjectNames
        {
            get { return projectNames; }
            set
            {
                projectNames = value;

                InitialCombProInfos(projectNames);
            }
        }

        private void InitialCombProInfos(List<string> lstAssayProInfos)
        {
            calcProjectPage1.LstAssayProInfos = lstAssayProInfos;
            this.Invoke(new EventHandler(delegate
                {
                    xtraTabControl1.SelectedTabPageIndex = 0;
                }));            
        }

        private void AddOrEditCalcItem_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadAddOrEditCalcItem));
            
        }
        private void loadAddOrEditCalcItem()
        {
            calcProjectPage1 = new CalcProjectPage1();
            calcProjectPage2 = new CalcProjectPage2();
            calcProjectPage3 = new CalcProjectPage3();
            calcProjectPage4 = new CalcProjectPage4();
            calcProjectPage1.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;
            calcProjectPage2.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;
            calcProjectPage3.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;
            calcProjectPage4.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;


            foreach (string sampleType in RunConfigureUtility.SampleTypes)
            {
                cboSampleType.Properties.Items.Add(sampleType);
            }

            txtProjectName.Text = calcProInfoForEdit.CalcProjectName;
            txtProjectFullName.Text = calcProInfoForEdit.CalcProjectFullName;
            cboUnit.SelectedItem = calcProInfoForEdit.Unit;
            cboSampleType.SelectedItem = calcProInfoForEdit.SampleType;
            txtReferenceRangeLow.Text = calcProInfoForEdit.ReferenceRangeLow == 100000000 ? "" : calcProInfoForEdit.ReferenceRangeLow.ToString();
            txtReferenceRangeHigh.Text = calcProInfoForEdit.ReferenceRangeHigh == 100000000 ? "" : calcProInfoForEdit.ReferenceRangeHigh.ToString();
            txtCalcFormula.Text = calcProInfoForEdit.CalcFormula;
            if (this.Text == "添加计算项目")
                cboSampleType.SelectedIndex = 1;
            SendService(new CommunicationEntity("ProjectPageinfoForCalc", cboSampleType.SelectedItem.ToString()));

            xtraTabControl1.SelectedTabPageIndex = 0;
            xtraTabPage1.Controls.Add(calcProjectPage1);
        }

        private void SendService(CommunicationEntity sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsCalculateItem, XmlUtility.Serializer(typeof(CommunicationEntity), sender));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CalcProjectInfo calcProInfo = new CalcProjectInfo();

            if (txtProjectName.Text.Trim() == null || txtProjectName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("报告名称不能为空，请输入报告名称");
                return;
            }

            if (txtCalcFormula.Text.Trim() ==  null ||txtCalcFormula.Text.Trim() == string.Empty)
            {
                MessageBox.Show("计算公式不能为空，请输入计算公式");
                return;
            }

            if (cboSampleType.SelectedItem == null)
            {
                MessageBox.Show("样本类型不能为空，请选择样本类型！");
                return;
            }
            if (txtReferenceRangeLow.Text.Trim() != string.Empty)
            {
                if (!Regex.IsMatch(txtReferenceRangeLow.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("范围参数输入有误，请重新输入！");
                    return;
                }
            }
            if (txtReferenceRangeHigh.Text.Trim() != string.Empty)
            {
                if (!Regex.IsMatch(txtReferenceRangeHigh.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("范围参数输入有误，请重新输入！");
                    return;
                }
            }

            if (txtCalcFormula.Text != string.Empty)
            {
                if (!CheckFormulation(txtCalcFormula.Text))
                {
                    MessageBox.Show("计算公式格式输入有误，请重新输入");
                    return;
                }

            }

            calcProInfo.CalcProjectName = txtProjectName.Text.Trim();
            calcProInfo.CalcProjectFullName = txtProjectFullName.Text.Trim();
            if (cboUnit.SelectedItem != null && cboUnit.SelectedItem.ToString() != string.Empty)
                calcProInfo.Unit = cboUnit.SelectedItem.ToString();
            calcProInfo.SampleType = cboSampleType.SelectedItem.ToString();

            if (txtReferenceRangeLow.Text.Trim() !=  null && txtReferenceRangeLow.Text.Trim() != string.Empty)
                calcProInfo.ReferenceRangeLow = (float)System.Convert.ToDouble(txtReferenceRangeLow.Text.Trim());
            if (txtReferenceRangeHigh.Text.Trim() != null && txtReferenceRangeHigh.Text.Trim() != string.Empty)
                calcProInfo.ReferenceRangeHigh = (float)System.Convert.ToDouble(txtReferenceRangeHigh.Text.Trim());

            calcProInfo.CalcFormula = txtCalcFormula.Text.Trim();

            if (this.Text == "添加计算项目")
            {
                SendService(new CommunicationEntity("AddCalcProject", XmlUtility.Serializer(typeof(CalcProjectInfo), calcProInfo)));
            }
            else
            {
                SendService(new CommunicationEntity("UpdateCalcProject", XmlUtility.Serializer(typeof(CalcProjectInfo), calcProInfoForEdit), XmlUtility.Serializer(typeof(CalcProjectInfo), calcProInfo)));
            }

        }
        /// <summary>
        /// 检查计算公式是否符合规则
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        bool CheckFormulation(string f)
        {
            if (f == null)
            {
                return true;
            }

            for (int i = 0; i < f.Length - 1; i++)
            {
                char c = f[i];
                char a = f[i + 1];
                if (c == '+' || c == '-' || c == '/' || c == '*' || c == '.')
                {
                    if (a == '+' || a == '-' || a == '/' || a == '*' || a == '.')
                    {
                        return false;
                    }
                }
            }
            //GW
            for (int i = 0; i < f.Length - 1; i++)
            {

                char a = f[i];
                char a1 = f[i + 1];

                if (a == ']' && a1 == '[')
                {
                    return false;

                }

                if (a == ']' && a1 == '.')
                {
                    return false;
                }

                if (a == '.' && a1 == '[')
                {
                    return false;
                }

                if (a == '.' && a1 == '.')
                {
                    return false;
                }

                if (a == '.' && a1 == '.')
                {
                    return false;
                }

                if (a == '(' && a1 == ')')
                {
                    return false;
                }

                if (a == '(' && a1 == ')')
                {
                    return false;

                }

                if (a == ')' && a1 == '(')
                {
                    return false;
                }

                if (a == ')')
                {
                    if (a1 != '+' && a1 != '-' && a1 != '*' && a1 != '/')
                    {
                        return false;
                    }
                }

                if (i == 0 && (a == '+' || a == '*' || a == '/' || a == '.'))
                {
                    return false;
                }

                if (i != 0 && a1 == '(')
                {
                    if (a != '+' && a != '-' && a != '*' && a != '/')
                    {
                        return false;
                    }
                }

                if (a == ']' && a1 == '(')
                {
                    return false;
                }

                if (a == ')' && a1 == '[')
                {
                    return false;
                }
            }

            if (f[0] == '/' || f[0] == '*' || f[0] == '+')
            {
                return false;
            }

            if (f[f.Length - 1] == '/' || f[f.Length - 1] == '*' || f[f.Length - 1] == '-' || f[f.Length - 1] == '+')
            {
                return false;
            }

            for (int m = 0; m < f.Length; m++)
            {

                if (f[m] == '/')
                {
                    if ((m + 2) == f.Length)
                    {
                        if (f[m + 1] == '0')
                        {

                            return false;//末尾判断

                        }
                    }

                    if ((m + 2) < f.Length)
                    {
                        if (f[m + 1] == '0' && f[m + 2] != '.')
                        {

                            return false;//判断/0.

                        }
                    }

                    if ((m + 3) == f.Length)
                    {
                        if (f[m + 1] == '0' && f[m + 2] == '.')
                        {

                            return false;//判断/0.

                        }

                    }

                    if ((m + 3) < f.Length)
                    {
                        if (f[m + 1] == '0' && f[m + 2] == '.' && (f[m + 3] > '9' || f[m + 3] < '0'))
                        {

                            return false;//判断/0.后面不是数字

                        }

                        for (int n = m + 3; n < f.Length; n++)
                        {

                            if (n == (f.Length - 1))
                            {
                                if ((f[n] > '9' || f[n] < '0') && f[n] != ']' && f[n] != ')')
                                {
                                    return false;

                                }
                            }

                            if (f[n] > '0' && f[n] <= '9')
                                break;//只要/0.后面有数字且不是0就算合规 

                            if (f[n] == '0' && n == (f.Length - 1))
                            {
                                return false;//判断/0.后面是不是0000000000

                            }
                        }
                    }
                }
            }


            bool b = false;
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == '(')
                {
                    b = false;
                    for (int j = i + 1; j < f.Length; j++)
                    {
                        if (f[j] == ')')
                        {
                            b = true;
                            break;
                        }
                        else
                        {
                            b = false;
                        }
                    }
                    if (b == false)
                    {
                        return false;
                    }
                }
            }

            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == ')')
                {
                    b = false;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (f[j] == '(')
                        {
                            b = true;
                            break;
                        }
                        else
                        {
                            b = false;
                        }
                    }
                    if (b == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            SimpleButton simpleBtn = sender as SimpleButton;

            txtCalcFormula.Text += simpleBtn.Text;
        }

        private void btnEmpty_Click(object sender, EventArgs e)
        {
            txtCalcFormula.Text = string.Empty;
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                if (!xtraTabPage1.Controls.Contains(calcProjectPage1))
                    xtraTabPage1.Controls.Add(calcProjectPage1);
                calcProjectPage1.LstAssayProInfos = projectNames;
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                if (!xtraTabPage2.Controls.Contains(calcProjectPage2))
                    xtraTabPage2.Controls.Add(calcProjectPage2);
                calcProjectPage2.LstAssayProInfos = projectNames;
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                if (!xtraTabPage3.Controls.Contains(calcProjectPage3))
                    xtraTabPage3.Controls.Add(calcProjectPage3);
                calcProjectPage3.LstAssayProInfos = projectNames;
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                if (!xtraTabPage4.Controls.Contains(calcProjectPage4))
                    xtraTabPage4.Controls.Add(calcProjectPage4);
                calcProjectPage4.LstAssayProInfos = projectNames;
            }
        }

        private void cboSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SendService(new CommunicationEntity("ProjectPageinfoForCalc", cboSampleType.SelectedItem.ToString()));
            txtCalcFormula.Text = "";
        }
    }
}