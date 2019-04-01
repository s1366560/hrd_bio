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
using BioA.Common;
using BioA.Common.IO;
using System.Threading;
using BioA.BLL;
using BioA.IBLL;

namespace BioA.UI
{
    public partial class LoadingReagentBlocking : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 默认为0：没有装载试剂，1：代表装载试剂1， 2：代表装载试剂2
        /// </summary>
        public int ReagentDisk { get; set; }
        //试剂状态设置信息
        private ReagentStateInfo rs; 
        //条码配置信息
        private ReagentConfigInfo rc;

        public LoadingReagentBlocking()
        {
            InitializeComponent();
            this.ControlBox = false;
            dtpValidDate.DateTime = DateTime.Now.AddMonths(1);
        }

        private void LoadingReagentBlocking_Load(object sender, EventArgs e)
        {
            //异步方法调用
            BeginInvoke(new Action(loadFrmReagent));
            rs= ReagentConfigInfoConstrunction.ReagentStateInfo;
            rc = ReagentConfigInfoConstrunction.ReagentConfig;

        }
        //存储已经分好类型<key> 对应项目名称<value>
        private Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

        private void loadFrmReagent()
        {
            if (rs.ReagentNumberList.Count > 0)
            {
                IReagentStateSetting iReagentSS = new ReagentStateSetting();
                dictionary = iReagentSS.Get(rs.ReagentNumberList);
            }
            this.LoadingReagentData();
            this.txtBarcode.Focus();
        }

        public void LoadingReagentData()
        {
            if (this.Text == "试剂条码装载R1")
            {
                List<string> lstCanUsePos = RunConfigureUtility.Reagentpos;
                cboReagentPos.Properties.Items.Clear();
                cboReagentPos.Properties.Items.AddRange(lstCanUsePos);
                cboReagentPos.SelectedIndex = 0;

                cboReagentType.Properties.Items.Clear();
                cboReagentType.Properties.Items.AddRange(new object[] { "", "血清", "尿液", "清洗剂" });
                cboReagentType.SelectedIndex = 1;

                cboReagentVol.Properties.Items.Clear();
                cboReagentVol.Properties.Items.AddRange(new object[] { "20ml", "40ml", "70ml" });
                cboReagentVol.SelectedIndex = 0;
            }
            else
            {
                List<string> lstCanUsePos = RunConfigureUtility.Reagentpos2;
                cboReagentPos.Properties.Items.Clear();
                cboReagentPos.Properties.Items.AddRange(lstCanUsePos);
                cboReagentPos.SelectedIndex = 0;
                cboReagentType.Properties.Items.Clear();
                cboReagentType.Properties.Items.AddRange(new object[] { "", "血清", "尿液", "清洗剂" });
                cboReagentType.SelectedIndex = 1;

                cboReagentVol.Properties.Items.Clear();
                cboReagentVol.Properties.Items.AddRange(new object[] { "20ml", "40ml", "70ml" });
                cboReagentVol.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 显示试剂对应的项目名称
        /// </summary>
        /// <param name="dic"></param>
        public void DisplayReagentProjectName(Dictionary<string, List<string>> dic)
        {
            if (dic.Count() > 0)
            {
                this.Invoke(new EventHandler(delegate
                {
                    this.cboProjectCheck.Properties.Items.Clear();
                    foreach (var item in dic)
	                {
                        if (item.Key == cboReagentType.Text)
                        {
                            cboProjectCheck.Properties.Items.AddRange(item.Value.FindAll(i => !LstProjectName.Contains(i)));
                        }
	                }
                }));
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            cboReagentType.Text = "血清";
            txtReagentName.Text = "";
            cboProjectCheck.Text = "请选择";
            txtBarcode.Text = "";
            txtBatchNum.Text = "";

        }

        /// <summary>
        /// 存储已使用的项目名
        /// </summary>
        public List<object> LstProjectName
        {
            get;
            set;
        }

        /// <summary>
        /// 存储已使用的位置
        /// </summary>
        public List<string> LstPos
        {
            get;
            set;
        }
        /// <summary>
        /// 私有变量，存储试剂保存信息
        /// </summary>
        private ReagentSettingsInfo reagentSettingsInfo;

        private void btnSave_Click(object sender, EventArgs e)
        {
            reagentSettingsInfo = new ReagentSettingsInfo();
            reagentSettingsInfo.Barcode = txtBarcode.Text;
            reagentSettingsInfo.BatchNum = txtBatchNum.Text;
            
            if (cboReagentPos.Text != "")
            {
                if (LstPos.Contains(cboReagentPos.Text.Trim()) == true)
                {
                    MessageBox.Show("试剂位置"+cboReagentPos.Text + "已被其他项目占用！");
                    return;
                }
                reagentSettingsInfo.Pos = cboReagentPos.Text;
            }
            else
            {
                //MessageBoxDraw.ShowMsg("请填写试剂位置！", MsgType.Warning);
                MessageBox.Show("请填写试剂位置！");
                this.cboReagentPos.Focus();
                return;
            }

            if (cboProjectCheck.Text != "" && cboProjectCheck.Text != "请选择")
            {
                reagentSettingsInfo.ProjectName = cboProjectCheck.Text;
            }
            else if (cboReagentType.Text == "清洗剂")
            {
            }
            else
            {
                MessageBox.Show("请选择项目名称！");
                this.cboProjectCheck.Focus();
                return;
            }

            if (txtReagentName.Text.Trim() != "")
            {
                reagentSettingsInfo.ReagentName = txtReagentName.Text;
            }
            else
            {
                MessageBox.Show("请填写试剂名称！");
                this.txtReagentName.Focus();
                return;
            }
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            reagentSettingsInfo.ValidDate = dtpValidDate.DateTime;
            reagentSettingsInfo.ReagentContainer = cboReagentVol.Text;
            reagentSettingsInfo.ReagentType = cboReagentType.Text;
            string s = new BioA.Service.ReagentSetting().AddreagentSettingInfo(ReagentDisk, reagentSettingsInfo);
            if (s != "试剂装载成功！")
            {
                MessageBox.Show("试剂条码R"+ ReagentDisk +"装载失败！");
            }
            else
            {
                if (RefreshReagentInfoEvent != null)
                {
                    RefreshReagentInfoEvent(ReagentDisk, reagentSettingsInfo);
                }
            }
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.Close();
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Text == "试剂条码装载R1")
            {
                if (this.cboReagentVol.Text == "20ml" || this.cboReagentVol.Text == "40ml")
                {
                    this.cboReagentPos.Properties.Items.Clear();
                    this.cboReagentPos.Properties.Items.AddRange(RunConfigureUtility.Reagentpos);
                    cboReagentPos.SelectedIndex = 0;
                }
                if (this.cboReagentVol.Text == "70ml")
                {
                    this.cboReagentPos.Properties.Items.Clear();
                    this.cboReagentPos.Properties.Items.AddRange(RunConfigureUtility.Reagentpos);
                    cboReagentPos.SelectedIndex = 0;
                }
            }
            else
            {
                if (this.cboReagentVol.Text == "20ml" || this.cboReagentVol.Text == "40ml")
                {
                    this.cboReagentPos.Properties.Items.Clear();
                    this.cboReagentPos.Properties.Items.AddRange(RunConfigureUtility.Reagentpos2);
                    cboReagentPos.SelectedIndex = 0;
                }
                if (this.cboReagentVol.Text == "70ml")
                {
                    this.cboReagentPos.Properties.Items.Clear();
                    this.cboReagentPos.Properties.Items.AddRange(RunConfigureUtility.Reagentpos2);
                    cboReagentPos.SelectedIndex = 0;
                }
            }
        }
        /// <summary>
        /// 试剂类型下拉框改变下标事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayReagentProjectName(dictionary);
            txtReagentName.Text = "";
            cboProjectCheck.Text = "请选择";
            txtBarcode.Text = "";
            txtBatchNum.Text = "";

        }

        private void LoadingReagentBlocking_FormClosing(object sender, FormClosingEventArgs e)
        {
            cboReagentType.Text = "血清";
            cboReagentVol.SelectedIndex = 0;
            txtReagentName.Text = "";
            cboProjectCheck.Text = "请选择";
            cboReagentPos.SelectedIndex = 0;
            txtBarcode.Text = "";
            txtBatchNum.Text = "";
        }
        /// <summary>
        /// 扫描试剂单个试剂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butReagentScann_Click(object sender, EventArgs e)
        {
            if (int.Parse(this.cboReagentPos.Text.Trim()) <= 50)
            {
                if (this.ScannSingleReagentEvent != null)
                {
                    ScannSingleReagentEvent(int.Parse(this.cboReagentPos.Text));
                }
            }
            else
            {
                MessageBox.Show(string.Format("当前试剂位置{0}已超出试剂仓扫描范围！",int.Parse(this.cboReagentPos.Text.Trim())));
            }
        }
        public delegate void RefreshReagentInfo(int d, ReagentSettingsInfo rs);//声明一个委托
        public event RefreshReagentInfo RefreshReagentInfoEvent;//声明一个委托事件

        public delegate void LoadingReagentBlockingHandler(object sender);
        public event LoadingReagentBlockingHandler ScannSingleReagentEvent;
        public event LoadingReagentBlockingHandler InputReagentBarcodeEvent;
        /// <summary>
        /// 扫码所有试剂
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSacnAllBarcode_Click(object sender, EventArgs e)
        {
            if (this.ScannSingleReagentEvent != null)
            {
                ScannSingleReagentEvent(0);
            }
        }
        /// <summary>
        /// 试剂条码输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 文本框值发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBarcode_EditValueChanged(object sender, EventArgs e)
        {
            if (txtBarcode.Text.Trim().Length == 18)
            {
                BeginInvoke(new Action(() => { this.ProcessReagentBarcodeScan(); }));
            }
        }

        private void ProcessReagentBarcodeScan()
        {
            string s = new ReagentBarcode().GetRgBracodePara(ReagentDisk, cboReagentPos.Text.Trim(), txtBarcode.Text.Trim()) as string;
            if (s == null)
            {
                if (this.InputReagentBarcodeEvent != null)
                {
                    this.InputReagentBarcodeEvent(ReagentDisk);
                }
                MessageBox.Show(txtBarcode.Text.Trim() + "条码装载试剂成功！");
            }
            else
            {
                MessageBox.Show(s);
            }
            //Thread.Sleep(500);
            this.Close();
        }

    }
}