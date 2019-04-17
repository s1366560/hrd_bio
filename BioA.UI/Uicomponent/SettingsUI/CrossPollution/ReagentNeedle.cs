using System;
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
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading;

namespace BioA.UI
{
    public partial class ReagentNeedle : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> reagentNeedleDic = new Dictionary<string, object[]>();
        List<ReagentNeedleAntifoulingStrategyInfo> lstReagentNeedleInfo = new List<ReagentNeedleAntifoulingStrategyInfo>();
        ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfoOld;
        List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 污染项目数据列表
        /// </summary>

        DataTable dt = new DataTable();

        public ReagentNeedle()
        {
            InitializeComponent();

            chkR1.Checked = true;

            dt.Columns.Add("序号");
            dt.Columns.Add("试剂针");
            dt.Columns.Add("污染项目名称");
            dt.Columns.Add("污染项目类型");
            dt.Columns.Add("被污染项目名称");
            dt.Columns.Add("被污染项目类型");
            dt.Columns.Add("清洗剂类型");
            dt.Columns.Add("清洗剂体积");
            dt.Columns.Add("清洗次数");
            this.lstvCrossPollution.DataSource = dt;

            cboPolSampleType.Properties.Items.AddRange(new object[] { "血清", "尿液", "" });
            cboBePolSampleType.Properties.Items.AddRange(new object[] { "血清", "尿液", "" });
        }
       
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "AddReagentNeedle":
                    this.ReagentNeedleIUDPromptMsg(sender as string);
                    break;
                case "QueryReagentNeedle":
                    lstReagentNeedleInfo = (List<ReagentNeedleAntifoulingStrategyInfo>)XmlUtility.Deserialize(typeof(List<ReagentNeedleAntifoulingStrategyInfo>), sender as string);
                    QueryReagentNeedleAdd(lstReagentNeedleInfo);
                    break;
                case "DeleteReagentNeedle":
                    this.ReagentNeedleIUDPromptMsg(sender as string);
                    break;
                case "UpdataReagentNeedle":
                    this.ReagentNeedleIUDPromptMsg(sender as string);
                    break;
                case "QueryAssayProAllInfo":
                    lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    this.Invoke(new EventHandler(delegate
                    {
                        if (cboPolSampleType.SelectedIndex == 0)
                            cboPolSampleType_SelectedIndexChanged(null, null);
                        else
                            cboPolSampleType.SelectedIndex = 0;
                        if (cboBePolSampleType.SelectedIndex == 0)
                            cboBePolSampleType_SelectedIndexChanged(null, null);
                        else
                            cboBePolSampleType.SelectedIndex = 0;
                    }));
                    break;
                case "QueryWashingLiquid":
                    List<string> lstWashLiquid = XmlUtility.Deserialize(typeof(List<string>), sender as string) as List<string>;
                    this.Invoke(new EventHandler(delegate
                        {
                            cboWashing.Properties.Items.Clear();
                            cboWashing.Properties.Items.AddRange(lstWashLiquid);
                        }));
                    break;

            }
        }
        /// <summary>
        /// 新增、删除、修改后提示是否成功
        /// </summary>
        /// <param name="msg"></param>
        private void ReagentNeedleIUDPromptMsg(string msg)
        {
            if (msg == "试剂针防污策略刹删除成功！" || msg == "试剂针防污策略修改成功！" || msg == "试剂针防污策略创建成功！")
            {
                QueryReagentNeedle();
            }
            this.Invoke(new EventHandler(delegate
            {
                MessageBox.Show(msg);
            }));
        }

        private void QueryReagentNeedle()
        {
            reagentNeedleDic.Clear();
            reagentNeedleDic.Add("QueryReagentNeedle", null);
            SendReagentNeedle(reagentNeedleDic);
        }
        private void QueryReagentNeedleAdd(List<ReagentNeedleAntifoulingStrategyInfo> lstQueryReagentNeedle)
        {
            this.Invoke(new EventHandler(delegate
            {
                int i = 1;
                this.dt.Rows.Clear();
                if (lstQueryReagentNeedle.Count != 0)
                {
                    foreach (ReagentNeedleAntifoulingStrategyInfo QueryReagentNeedle in lstQueryReagentNeedle)
                    {
                        dt.Rows.Add(new object[] { i, QueryReagentNeedle.ReagentNeedle, QueryReagentNeedle.PolluteProName, QueryReagentNeedle.PolluteProType, QueryReagentNeedle.BePollutedProName, QueryReagentNeedle.BePollutedProType, QueryReagentNeedle.CleaningLiquidName, QueryReagentNeedle.CleaningLiquidUseVol, QueryReagentNeedle.CleanTimes });

                        i++;
                    }
                }
                this.lstvCrossPollution.DataSource = dt;
                this.gridView1.Columns[0].Width = 50;
                this.gridView1.Columns[1].Width = 100;
                this.gridView1.Columns[2].Width = 200;
                this.gridView1.Columns[3].Width = 200;
                this.gridView1.Columns[4].Width = 150;
                this.gridView1.Columns[5].Width = 150;
                this.btnCancel_Click(null,null);
            }));
        }

        private void SendReagentNeedle(Dictionary<string, object[]> sender)
        {
            var reagentNeedleThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsCrossPollution, sender);
            });
            reagentNeedleThread.IsBackground = true;
            reagentNeedleThread.Start();
        }
        /// <summary>
        /// 创建污染项目保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (cboPollutionSource.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择污染项目！", MsgType.Warning);
                return;
            }

            if (cboPollutedSource.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择被污染项目！", MsgType.Warning);
                return;
            }

            if (cboPollutionSource.Text == cboPollutedSource.Text)
            {
                MessageBoxDraw.ShowMsg("污染项目与被污染项目重名，请重新输入！", MsgType.Warning);
                return;
            }

            if (cboWashing.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择清洗液！", MsgType.Exception);
                return;
            }

            if (Regex.IsMatch(txtUsingVol.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {

            }
            else
            {
                MessageBoxDraw.ShowMsg("清洗液使用量输入有误，请重新输入！", MsgType.Warning);                return;
            }

            if (isNumberic(txtWashingTimes.Text.Trim()))
            {
              
            }
            else
            {
                MessageBoxDraw.ShowMsg("清洗次数输入有误，请重新输入！", MsgType.Warning);
                return;
            }


            ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo = new ReagentNeedleAntifoulingStrategyInfo();
            if (chkR1.Checked == true)
            {
                reagentNeedleAntifoulingStrategyInfo.ReagentNeedle = "R1";
            }
            if (chkR2.Checked == true)
            {
                reagentNeedleAntifoulingStrategyInfo.ReagentNeedle = "R2";
            }
            reagentNeedleAntifoulingStrategyInfo.PolluteProName = cboPollutionSource.Text;
            reagentNeedleAntifoulingStrategyInfo.PolluteProType = cboPolSampleType.Text;
            reagentNeedleAntifoulingStrategyInfo.BePollutedProName = cboPollutedSource.Text;
            reagentNeedleAntifoulingStrategyInfo.BePollutedProType = cboBePolSampleType.Text;
            reagentNeedleAntifoulingStrategyInfo.CleaningLiquidName = cboWashing.Text;
            reagentNeedleAntifoulingStrategyInfo.CleaningLiquidUseVol = (float)Convert.ToDouble(txtUsingVol.Text.Trim());
            reagentNeedleAntifoulingStrategyInfo.CleanTimes = Convert.ToInt32(txtWashingTimes.Text.Trim());
            reagentNeedleDic.Clear();
            reagentNeedleDic.Add("AddReagentNeedle", new object[] { XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfo) });
            SendReagentNeedle(reagentNeedleDic);
        }

        private void chkR1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkR2.Checked == true)
            {
                if (chkR1.Checked == true)
                {
                    chkR2.Checked = false;
                }
            }
            else
            {
                if (chkR1.Checked == false)
                {
                    chkR1.Checked = true;
                }
            }

        }

        private void chkR2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkR1.Checked == true)
            {
                if (chkR2.Checked == true)
                {
                    chkR1.Checked = false;
                }
            }
            else
            {
                if (chkR2.Checked == false)
                {
                    chkR2.Checked = true;
                }
            }
        }

        public void ReagentNeedle_Load(object sender, EventArgs e)
        {
            this.loadReagentNeedle();
            
        }
        private void loadReagentNeedle()
        {
            this.reagentNeedleDic.Clear();
            this.lstAssayProInfos.Clear();
            this.lstReagentNeedleInfo.Clear();
            reagentNeedleAntifoulingStrategyInfoOld = new ReagentNeedleAntifoulingStrategyInfo();
            //获取所有防污试剂针
            reagentNeedleDic.Add("QueryReagentNeedle", null);
            //获取所有生化项目信息
            reagentNeedleDic.Add("QueryAssayProAllInfo", new object[] { "" });
            //获取所有清洗液信息
            reagentNeedleDic.Add("QueryWashingLiquid",null);
            SendReagentNeedle(reagentNeedleDic);
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                if (MessageBoxDraw.ShowMsg("是否确认删除该防污策略？", MsgType.Question) == DialogResult.OK)
                {
                    CommunicationEntity ReagentNeedle = new CommunicationEntity();
                    ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo = new ReagentNeedleAntifoulingStrategyInfo();
                    int selectedHandle;
                    selectedHandle = this.gridView1.GetSelectedRows()[0];
                    reagentNeedleAntifoulingStrategyInfo.ReagentNeedle = this.gridView1.GetRowCellValue(selectedHandle, "试剂针").ToString();
                    reagentNeedleAntifoulingStrategyInfo.PolluteProName = this.gridView1.GetRowCellValue(selectedHandle, "污染项目名称").ToString();
                    reagentNeedleAntifoulingStrategyInfo.PolluteProType = this.gridView1.GetRowCellValue(selectedHandle, "污染项目类型").ToString();
                    reagentNeedleAntifoulingStrategyInfo.BePollutedProName = this.gridView1.GetRowCellValue(selectedHandle, "被污染项目名称").ToString();
                    reagentNeedleAntifoulingStrategyInfo.BePollutedProType = this.gridView1.GetRowCellValue(selectedHandle, "被污染项目类型").ToString();
                    reagentNeedleAntifoulingStrategyInfo.CleaningLiquidName = this.gridView1.GetRowCellValue(selectedHandle, "清洗剂类型").ToString();
                    reagentNeedleAntifoulingStrategyInfo.CleaningLiquidUseVol = (float)Convert.ToDouble(this.gridView1.GetRowCellValue(selectedHandle, "清洗剂体积").ToString());
                    reagentNeedleAntifoulingStrategyInfo.CleanTimes = Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "清洗次数").ToString());

                    reagentNeedleDic.Clear();
                    reagentNeedleDic.Add("DeleteReagentNeedle", new object[] { XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfo) });
                    SendReagentNeedle(reagentNeedleDic);
                }
            }

        }
        /// <summary>
        /// 保存污染项目修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                if (cboPollutionSource.SelectedIndex < 0)
                {
                    MessageBoxDraw.ShowMsg("请选择污染项目！", MsgType.Exception);
                    return;
                }
                if (cboPollutedSource.SelectedIndex < 0)
                {
                    MessageBoxDraw.ShowMsg("请选择被污染项目！", MsgType.Exception);
                    return;
                }

                if (cboPollutionSource.Text == cboPollutedSource.Text)
                {
                    MessageBoxDraw.ShowMsg("污染项目与被污染项目重名，请重新输入！", MsgType.Exception);
                    return;
                }

                if (cboWashing.SelectedIndex < 0)
                {
                    MessageBoxDraw.ShowMsg("请选择清洗液！", MsgType.Exception);
                    return;
                }

                if (Regex.IsMatch(txtUsingVol.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {

                }
                else
                {
                    MessageBoxDraw.ShowMsg("清洗液使用量输入有误，请重新输入！", MsgType.Exception);
                    return;
                }

                if (isNumberic(txtWashingTimes.Text.Trim()))
                {

                }
                else
                {
                    MessageBoxDraw.ShowMsg("清洗次数输入有误，请重新输入！", MsgType.Exception);
                    return;
                }
                ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfo = new ReagentNeedleAntifoulingStrategyInfo();
                if (chkR1.Checked == true)
                {
                    reagentNeedleAntifoulingStrategyInfo.ReagentNeedle = "R1";
                }
                if (chkR2.Checked == true)
                {
                    reagentNeedleAntifoulingStrategyInfo.ReagentNeedle = "R2";
                }
                reagentNeedleAntifoulingStrategyInfo.PolluteProName = cboPollutionSource.Text;
                reagentNeedleAntifoulingStrategyInfo.PolluteProType = cboPolSampleType.Text;
                reagentNeedleAntifoulingStrategyInfo.BePollutedProName = cboPollutedSource.Text;
                reagentNeedleAntifoulingStrategyInfo.BePollutedProType = cboBePolSampleType.Text;
                reagentNeedleAntifoulingStrategyInfo.CleaningLiquidName = cboWashing.Text;
                reagentNeedleAntifoulingStrategyInfo.CleaningLiquidUseVol = (float)Convert.ToDouble(txtUsingVol.Text);
                reagentNeedleAntifoulingStrategyInfo.CleanTimes = Convert.ToInt32(txtWashingTimes.Text);
                reagentNeedleDic.Clear();
                reagentNeedleDic.Add("UpdataReagentNeedle", new object[] { XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfo), XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfoOld) });
                SendReagentNeedle(reagentNeedleDic);
            }
            else
            {
                MessageBoxDraw.ShowMsg("没有被选中的防污策略，无法保存！", MsgType.Warning);
                return;
            }
        }
        /// <summary>
        /// 污染项目参数信息列表选中点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                reagentNeedleAntifoulingStrategyInfoOld.ReagentNeedle = this.gridView1.GetRowCellValue(selectedHandle, "试剂针").ToString();
                reagentNeedleAntifoulingStrategyInfoOld.PolluteProName = this.gridView1.GetRowCellValue(selectedHandle, "污染项目名称").ToString();
                reagentNeedleAntifoulingStrategyInfoOld.PolluteProType = this.gridView1.GetRowCellValue(selectedHandle, "污染项目类型").ToString();

                reagentNeedleAntifoulingStrategyInfoOld.BePollutedProName = this.gridView1.GetRowCellValue(selectedHandle, "被污染项目名称").ToString();
                reagentNeedleAntifoulingStrategyInfoOld.BePollutedProType = this.gridView1.GetRowCellValue(selectedHandle, "被污染项目类型").ToString();

                reagentNeedleAntifoulingStrategyInfoOld.CleaningLiquidName = this.gridView1.GetRowCellValue(selectedHandle, "清洗剂类型").ToString();
                reagentNeedleAntifoulingStrategyInfoOld.CleaningLiquidUseVol = (float)Convert.ToDouble(this.gridView1.GetRowCellValue(selectedHandle, "清洗剂体积").ToString());
                reagentNeedleAntifoulingStrategyInfoOld.CleanTimes = Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "清洗次数").ToString());

                if (reagentNeedleAntifoulingStrategyInfoOld.ReagentNeedle == "R1")
                {
                    chkR1.Checked = true;
                }
                if (reagentNeedleAntifoulingStrategyInfoOld.ReagentNeedle == "R2")
                {
                    chkR2.Checked = true;
                }
                cboPollutionSource.Text = reagentNeedleAntifoulingStrategyInfoOld.PolluteProName;
                cboPolSampleType.Text = reagentNeedleAntifoulingStrategyInfoOld.PolluteProType;
                cboPollutedSource.Text = reagentNeedleAntifoulingStrategyInfoOld.BePollutedProName;
                cboBePolSampleType.Text = reagentNeedleAntifoulingStrategyInfoOld.BePollutedProType;
                cboWashing.Text = reagentNeedleAntifoulingStrategyInfoOld.CleaningLiquidName;
                txtUsingVol.Text = reagentNeedleAntifoulingStrategyInfoOld.CleaningLiquidUseVol.ToString();
                txtWashingTimes.Text = reagentNeedleAntifoulingStrategyInfoOld.CleanTimes.ToString();
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cboPollutionSource.Text = "请选择";
            cboPollutedSource.Text = "请选择";
            this.cboWashing.Text = "请选择";
            this.txtUsingVol.Text = "";
            this.txtWashingTimes.Text = "";
        }

      
        private bool isNumberic(string message)
        {
                System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
                if (rex.IsMatch(message))
                {
                    return true;
                }
                else
                    return false;
        }
        /// <summary>
        /// 污染项目类型改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboPolSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPollutionSource.Properties.Items.Clear();
            List<AssayProjectInfo> lstFilterPros = lstAssayProInfos.FindAll(x => x.SampleType == cboPolSampleType.SelectedItem.ToString());
            if (lstFilterPros.Count > 0)
            {
                foreach (AssayProjectInfo assayProject in lstFilterPros)
                {
                    cboPollutionSource.Properties.Items.Add(assayProject.ProjectName);
                }
            }            
        }

        /// <summary>
        /// 被污染项目类型改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboBePolSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPollutedSource.Properties.Items.Clear();
            List<AssayProjectInfo> lstFilterPros = lstAssayProInfos.FindAll(x => x.SampleType == cboBePolSampleType.SelectedItem.ToString());
            if (lstFilterPros.Count > 0)
            {
                foreach (AssayProjectInfo assayProject in lstFilterPros)
                {
                    cboPollutedSource.Properties.Items.Add(assayProject.ProjectName);
                }
            }
        }

        private XtraProjectSequence projectSequence;
        /// <summary>
        /// 项目测试排序按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (projectSequence == null)
            {
                projectSequence = new XtraProjectSequence();
                projectSequence.StartPosition = FormStartPosition.CenterScreen;
            }
            projectSequence.XtraProjectSequence_Load(null,null);
            projectSequence.ShowDialog();
        }        
    }
}
