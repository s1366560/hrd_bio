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
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class AddOrEditCalcItem : DevExpress.XtraEditors.XtraForm
    {
        CalcProjectPage1 calcProjectPage1 = new CalcProjectPage1();
        CalcProjectPage2 calcProjectPage2 = new CalcProjectPage2();
        CalcProjectPage3 calcProjectPage3 = new CalcProjectPage3();
        CalcProjectPage4 calcProjectPage4 = new CalcProjectPage4();

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> addOrEditItemDic = new Dictionary<string, object[]>();

        CalcProjectInfo calcProInfoForEdit = new CalcProjectInfo();
        /// <summary>
        /// 存储编辑计算项目的信息
        /// </summary>
        public CalcProjectInfo CalcProInfoForEdit
        {
            get { return calcProInfoForEdit; }
            set
            {
                calcProInfoForEdit = value;
                txtProjectName.Text = calcProInfoForEdit.CalcProjectName;
                txtProjectFullName.Text = calcProInfoForEdit.CalcProjectFullName;
                cboUnit.SelectedIndex = cboUnit.Properties.Items.IndexOf(calcProInfoForEdit.Unit);
                cboSampleType.SelectedIndex = cboSampleType.Properties.Items.IndexOf(calcProInfoForEdit.SampleType);
                if (calcProInfoForEdit.ReferenceRangeLow != -1)
                    txtReferenceRangeLow.Text = calcProInfoForEdit.ReferenceRangeLow.ToString();
                else
                    txtReferenceRangeLow.Text = "";
                if (calcProInfoForEdit.ReferenceRangeHigh != -1)
                    txtReferenceRangeHigh.Text = calcProInfoForEdit.ReferenceRangeHigh.ToString();
                else
                    txtReferenceRangeHigh.Text = "";
                txtCalcFormula.Text = calcProInfoForEdit.CalcFormula;
            }
        }

        public AddOrEditCalcItem()
        {
            InitializeComponent();

            this.ControlBox = false;

            calcProjectPage1.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;
            calcProjectPage2.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;
            calcProjectPage3.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;
            calcProjectPage4.ProjectCallbackInfoEvent += ProjectCallbackInfo_Event;

            cboSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);

            
        }

        private void ProjectCallbackInfo_Event(string strProInfo)
        {
            txtCalcFormula.Text += "[" + strProInfo + "]";
        }

        private List<string> projectNames = new List<string>();

        public List<string> ProjectNames
        {
            get { return projectNames; }
            set
            {
                projectNames = value;
            }
        }

        public void AddOrEditCalcItem_Load(object sender, EventArgs e)
        {
            if (this.Text == "添加计算项目")
                cboSampleType.SelectedIndex = 1;
            else
                cboSampleType_SelectedIndexChanged(null, null);
            this.loadAddOrEditCalcItem();
            
        }
        private void loadAddOrEditCalcItem()
        {
            cboUnit.Properties.Items.Clear();
            cboUnit.Properties.Items.AddRange(new SettingsChemicalParameter().QueryProjectResultUnits("QueryProjectResultUnits"));
            
            if (xtraTabControl1.SelectedTabPageIndex == 0)
                xtraTabControl1_SelectedPageChanged(null, null);
            else
                xtraTabControl1.SelectedTabPageIndex = 0;
        }

        public void ClearCalcProjectParameter()
        {
            txtProjectName.Text = "";
            txtProjectFullName.Text = "";
            cboUnit.SelectedIndex = -1;
            txtReferenceRangeLow.Text = "";
            txtReferenceRangeHigh.Text = "";
            txtCalcFormula.Text = "";
        }

        private void SendService(Dictionary<string,object[]> sender)
        {
            var addOrCalcThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsCalculateItem, sender);
            });
            addOrCalcThread.IsBackground = true;
            addOrCalcThread.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                addOrEditItemDic.Clear();
                addOrEditItemDic.Add("AddCalcProject", new object[] { XmlUtility.Serializer(typeof(CalcProjectInfo), calcProInfo) });
                SendService(addOrEditItemDic);
            }
            else
            {
                addOrEditItemDic.Clear();
                addOrEditItemDic.Add("UpdateCalcProject",new object[]{XmlUtility.Serializer(typeof(CalcProjectInfo), calcProInfoForEdit),XmlUtility.Serializer(typeof(CalcProjectInfo), calcProInfo)});
                SendService(addOrEditItemDic);
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

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
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
            //addOrEditItemDic.Clear();
            //addOrEditItemDic.Add("ProjectPageinfoForCalc", new object[] { cboSampleType.SelectedItem.ToString() });
            //SendService(addOrEditItemDic);
            this.ProjectNames = new CalcProjectParameter().ProjectPageinfoForCalc("ProjectPageinfoForCalc", cboSampleType.SelectedItem.ToString());
            txtCalcFormula.Text = "";
        }

        
    }
}