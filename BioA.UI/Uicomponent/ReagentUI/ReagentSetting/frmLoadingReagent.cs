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

namespace BioA.UI
{
    public partial class frmLoadingReagent : DevExpress.XtraEditors.XtraForm
    {

        public delegate void GetsReagent(Dictionary<string, ReagentSettingsInfo> keyValuePairs);//声明一个委托
        public event GetsReagent GetsReagentEvent;//声明一个委托事件

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> frmLoadingReagentDic = new Dictionary<string, object[]>();

        private string recieveInfo = "";
        public string RecieveInfo
        {
            set
            {
                recieveInfo = value;
                if (this.IsHandleCreated)
                {
                    BeginInvoke(new Action(() =>
                    {
                        frmLoadingReagentDic.Clear();
                        Dictionary<string, ReagentSettingsInfo> dic = new Dictionary<string, ReagentSettingsInfo>();
                        dic.Add(recieveInfo, reagentSettingsInfo);
                        if (GetsReagentEvent != null)
                        {
                            GetsReagentEvent(dic);
                            this.btnSave.Enabled = true;
                            this.btnCancel.Enabled = true;
                        }
                    }));
                }
            }
        }

        private List<AssayProjectInfo> assayProjectInfo = new List<AssayProjectInfo>();
        /// <summary>
        /// 存储所有项目
        /// </summary>
        public List<AssayProjectInfo> AssayProjectInfo
        {
            get { return assayProjectInfo; }
            set
            {
                assayProjectInfo = value;
                comBoxAdd(assayProjectInfo);
            }
        }
        /// <summary>
        /// 存储已使用的项目名
        /// </summary>
        private List<object> lstProjectName = new List<object>();
        public List<object> LstProjectName
        {
            get { return lstProjectName; }
            set { lstProjectName = value; }
        }

        private List<string> lstUsedPos = new List<string>();
        /// <summary>
        /// 存储已使用的位置
        /// </summary>
        public List<string> LstUsedPos
        {
            get { return lstUsedPos; }
            set { lstUsedPos = value; }
        }

        public frmLoadingReagent()
        {
            InitializeComponent();
            this.ControlBox = false;
            cboReagentType.Text = "血清";
            cboReagentVol.Text = "20ml";
            dtpValidDate.DateTime = DateTime.Now.AddMonths(1);
        }

        private void frmLoadingReagent_Load(object sender, EventArgs e)
        {
            //异步方法调用
            BeginInvoke(new Action(loadFrmReagent));

        }

        private void loadFrmReagent()
        {
            frmLoadingReagentDic.Clear();
            //获取所有生化项目信息
            frmLoadingReagentDic.Add("QueryAssayProAllInfo", new object[]{""});
            SendInfoToService(frmLoadingReagentDic);
            cboProjectCheck.Text = "请选择";
        }

        public void LoadingReagentData()
        {
            if (this.Text == "试剂装载R1")
            {
                List<string> lstCanUsePos = RunConfigureUtility.Reagentpos;
                lstCanUsePos.RemoveAll(i => lstUsedPos.Contains(i));
                cboReagentPos.Properties.Items.Clear();
                cboReagentPos.Properties.Items.AddRange(lstCanUsePos);
                cboReagentPos.SelectedIndex = 0;
                lstCanUsePos = null;


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
                lstCanUsePos.RemoveAll(i => lstUsedPos.Contains(i));
                cboReagentPos.Properties.Items.Clear();
                cboReagentPos.Properties.Items.AddRange(lstCanUsePos);
                cboReagentPos.SelectedIndex = 0;
                lstCanUsePos = null;
                cboReagentType.Properties.Items.Clear();
                cboReagentType.Properties.Items.AddRange(new object[] { "", "血清", "尿液", "清洗剂" });
                cboReagentType.SelectedIndex = 1;

                cboReagentVol.Properties.Items.Clear();
                cboReagentVol.Properties.Items.AddRange(new object[] { "20ml", "40ml", "70ml" });
                cboReagentVol.SelectedIndex = 0;
            }
        }

        public void comBoxAdd(List<AssayProjectInfo> lstAssayProInfos)
        {
            if (this.IsHandleCreated)
            {
                this.Invoke(new EventHandler(delegate
                {
                    this.cboProjectCheck.Properties.Items.Clear();
                    List<string> listProName = new List<string>();
                    if (cboReagentType.Text == "血清")
                    {
                        listProName.Clear();
                        cboProjectCheck.Enabled = true;
                        for (int i = 0; i < lstAssayProInfos.Count; i++)
                        {
                            if (lstAssayProInfos[i].SampleType == "血清")
                            {

                                listProName.Add(lstAssayProInfos[i].ProjectName);

                            }
                        }
                        listProName.RemoveAll(i => lstProjectName.Contains(i));
                        //this.cboProjectCheck.Properties.Items.AddRange(new object[] 
                        //{ 
                        //    lstAssayProInfos[i].ProjectName
                        //});
                        this.cboProjectCheck.Properties.Items.AddRange(listProName);
                    }
                    if (cboReagentType.Text == "尿液")
                    {
                        listProName.Clear();
                        cboProjectCheck.Enabled = true;
                        for (int i = 0; i < lstAssayProInfos.Count; i++)
                        {
                            if (lstAssayProInfos[i].SampleType == "尿液")
                            {
                                listProName.Add(lstAssayProInfos[i].ProjectName);
                            }
                        }
                        listProName.RemoveAll(i => lstProjectName.Contains(i));
                        this.cboProjectCheck.Properties.Items.AddRange(listProName);
                        //this.cboProjectCheck.Properties.Items.AddRange(new object[] { lstAssayProInfos[i].ProjectName });
                    }
                    if (cboReagentType.Text == "清洗剂")
                    {

                        cboProjectCheck.Enabled = false;

                    }
                    if (cboReagentType.Text == "")
                    {
                        listProName.Clear();
                        cboProjectCheck.Enabled = true;
                        for (int i = 0; i < lstAssayProInfos.Count; i++)
                        {
                            if (lstAssayProInfos[i].SampleType == "")
                            {
                                listProName.Add(lstAssayProInfos[i].ProjectName);
                            }
                        }
                        listProName.RemoveAll(i => lstProjectName.Contains(i));
                        this.cboProjectCheck.Properties.Items.AddRange(listProName);
                        //this.cboProjectCheck.Properties.Items.AddRange(new object[] { lstAssayProInfos[i].ProjectName });
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

            ReagentStateInfoR1R2 reagentStateInfoR1R2 = new ReagentStateInfoR1R2();
            if (cboReagentType.Text != "清洗剂" && cboProjectCheck.Text != "")
            {
                reagentStateInfoR1R2.ProjectName = cboProjectCheck.Text;
            }

            if (this.Text == "试剂装载R1")
            {
                frmLoadingReagentDic.Clear();
                //新增试剂1信息
                frmLoadingReagentDic.Add("reagentSettingAddR1", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                SendInfoToService(frmLoadingReagentDic);

            }
            else if (this.Text == "试剂装载R2")
            {
                frmLoadingReagentDic.Clear();
                //新增试剂2信息
                frmLoadingReagentDic.Add("reagentSettingAddR2", new object[] { XmlUtility.Serializer(typeof(ReagentSettingsInfo), reagentSettingsInfo) });
                SendInfoToService(frmLoadingReagentDic);
            }
            else
            {
                return;
            }
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Text == "试剂装载R1")
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
                    List<string> str = new List<string>();
                    for (int i = 0; i < 50; i++)
                    {
                        str.Add(RunConfigureUtility.Reagentpos[i]);
                    }
                    this.cboReagentPos.Properties.Items.AddRange(str);
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
                    List<string> str = new List<string>();
                    for (int i = 0; i < 50; i++)
                    {
                        str.Add(RunConfigureUtility.Reagentpos2[i]);
                    }
                    this.cboReagentPos.Properties.Items.AddRange(str);
                    cboReagentPos.SelectedIndex = 0;
                }
            }
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comBoxAdd(assayProjectInfo);
            txtReagentName.Text = "";
            cboProjectCheck.Text = "请选择";
            txtBarcode.Text = "";
            txtBatchNum.Text = "";


        }
        /// <summary>
        /// 发送信息给服务器
        /// </summary>
        /// <param name="sender"></param>
        private void SendInfoToService(Dictionary<string, object[]> sender)
        {
            var frmLoadingReagentThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.ReagentSetting, sender);
            });
            frmLoadingReagentThread.IsBackground = true;
            frmLoadingReagentThread.Start();
        }

        private void frmLoadingReagent_FormClosing(object sender, FormClosingEventArgs e)
        {
            cboReagentType.Text = "血清";
            cboReagentVol.SelectedIndex = 0;
            txtReagentName.Text = "";
            cboProjectCheck.Text = "请选择";
            cboReagentPos.SelectedIndex = 0;
            txtBarcode.Text = "";
            txtBatchNum.Text = "";
        }


    }
}