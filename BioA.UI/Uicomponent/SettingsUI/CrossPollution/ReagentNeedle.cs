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

namespace BioA.UI
{
    public partial class ReagentNeedle : DevExpress.XtraEditors.XtraUserControl
    {
        List<ReagentNeedleAntifoulingStrategyInfo> lstReagentNeedleInfo = new List<ReagentNeedleAntifoulingStrategyInfo>();
        ReagentNeedleAntifoulingStrategyInfo reagentNeedleAntifoulingStrategyInfoOld = new ReagentNeedleAntifoulingStrategyInfo();
        List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        public ReagentNeedle()
        {
            InitializeComponent();

            chkR1.Checked = true;
        }
        
        private void add(List<AssayProjectInfo> lstAssayProInfos)
        {
             this.Invoke(new EventHandler(delegate
                {
                    this.cboPollutionSource.Properties.Items.Clear();
                    this.cboPollutedSource.Properties.Items.Clear();
                    foreach (AssayProjectInfo assayProjectInfo in lstAssayProInfos)
                    {

                        this.cboPollutionSource.Properties.Items.AddRange(new object[] { assayProjectInfo.ProjectName });
                        this.cboPollutedSource.Properties.Items.AddRange(new object[] { assayProjectInfo.ProjectName });
                    }
                }));
        }
       
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "AddReagentNeedle":
                    if ((string)sender == "试剂针防污策略创建失败，请联系管理员！")
                    {
                        MessageBoxDraw.ShowMsg((string)sender, MsgType.Exception);
                        return;
                    }
                    else if ((string)sender == "该试剂针防污策略存在，请重新录入。")
                    {
                        MessageBoxDraw.ShowMsg((string)sender, MsgType.Warning);
                        return;
                    }
                    else
                        QueryReagentNeedle();
                    break;
                case "QueryReagentNeedle":
                    lstReagentNeedleInfo = (List<ReagentNeedleAntifoulingStrategyInfo>)XmlUtility.Deserialize(typeof(List<ReagentNeedleAntifoulingStrategyInfo>), sender as string);
                    QueryReagentNeedleAdd(lstReagentNeedleInfo);
                    this.Invoke(new EventHandler(delegate
                    {
                        if (lstReagentNeedleInfo.Count > 0)
                        {
                            cboPollutionSource.Text = lstReagentNeedleInfo[0].PolluteProName;
                            cboPolSampleType.Text = lstReagentNeedleInfo[0].PolluteProType;
                            cboPollutedSource.Text = lstReagentNeedleInfo[0].BePollutedProName;
                            cboBePolSampleType.Text = lstReagentNeedleInfo[0].BePollutedProType;
                            cboWashing.Text = lstReagentNeedleInfo[0].CleaningLiquidName;
                        }
                    }));
                    break;
                case "DeleteReagentNeedle":
                    if ((int)sender == 0)
                    {
                        MessageBoxDraw.ShowMsg("删除失败！", MsgType.Warning);
                        return;
                    }
                    else
                        QueryReagentNeedle();
                    break;
                case "UpdataReagentNeedle":
                    if (sender as string == "保存成功！")
                        QueryReagentNeedle();
                    else
                        MessageBoxDraw.ShowMsg(sender as string, MsgType.Warning);
                    break;
                case "QueryAssayProAllInfo":
                    lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    add(lstAssayProInfos);
                    break;
                case "QueryWashingLiquid":
                    List<string> lstWashLiquid = XmlUtility.Deserialize(typeof(List<string>), sender as string) as List<string>;
                    this.Invoke(new EventHandler(delegate
                        {
                            cboWashing.Properties.Items.AddRange(lstWashLiquid);
                        }));
                    break;

            }
        }
        private void QueryReagentNeedle()
        {
            CommunicationEntity communicationEntity = new CommunicationEntity();
            communicationEntity.StrmethodName = "QueryReagentNeedle";
            communicationEntity.ObjParam = "";
            SendReagentNeedle(communicationEntity);
        }
        private void QueryReagentNeedleAdd(List<ReagentNeedleAntifoulingStrategyInfo> lstQueryReagentNeedle)
        {
            this.Invoke(new EventHandler(delegate
            {
                lstvCrossPollution.RefreshDataSource();
                int i = 1;
                DataTable dt = new DataTable();

                dt.Columns.Add("序号");
                dt.Columns.Add("试剂针");
                dt.Columns.Add("污染项目名称");
                dt.Columns.Add("污染项目类型");
                dt.Columns.Add("被污染项目名称");
                dt.Columns.Add("被污染项目类型");
                dt.Columns.Add("清洗剂类型");
                dt.Columns.Add("清洗剂体积");
                dt.Columns.Add("清洗次数");

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
                gridControl1_Click(null, null);
            }));
        }

        private void SendReagentNeedle(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsCrossPollution, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }
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
            CommunicationEntity communicationEntity = new CommunicationEntity();
            communicationEntity.StrmethodName = "AddReagentNeedle";
            communicationEntity.ObjParam = XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfo);
            SendReagentNeedle(communicationEntity);



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

        private void ReagentNeedle_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadReagentNeedle));
            
        }
        private void loadReagentNeedle()
        {
            QueryReagentNeedle();
            CommunicationEntity communicationEntity = new CommunicationEntity();
            communicationEntity.StrmethodName = "QueryAssayProAllInfo";
            communicationEntity.ObjParam = "";
            SendReagentNeedle(communicationEntity);
            communicationEntity.StrmethodName = "QueryWashingLiquid";
            SendReagentNeedle(communicationEntity);

            cboPolSampleType.Properties.Items.AddRange(new object[] { "血清", "尿液", "" });
            cboBePolSampleType.Properties.Items.AddRange(new object[] { "血清", "尿液", "" });
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


                    ReagentNeedle.StrmethodName = "DeleteReagentNeedle";
                    ReagentNeedle.ObjParam = XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfo);
                    SendReagentNeedle(ReagentNeedle);
                }
            }

        }

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
                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "UpdataReagentNeedle";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfo);
                communicationEntity.ObjLastestParam = XmlUtility.Serializer(typeof(ReagentNeedleAntifoulingStrategyInfo), reagentNeedleAntifoulingStrategyInfoOld);

                SendReagentNeedle(communicationEntity);
            }
            else
            {
                MessageBoxDraw.ShowMsg("没有被选中的防污策略，无法保存！", MsgType.Warning);
                return;
            }
        }

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

        private void btnCancel_Click(object sender, EventArgs e)
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
            List<AssayProjectInfo> lstFilterPros = lstAssayProInfos.FindAll(x => x.SampleType == cboPolSampleType.SelectedItem.ToString());
            if (lstFilterPros.Count > 0)
            {
                foreach (AssayProjectInfo assayProject in lstFilterPros)
                {
                    cboPollutionSource.Properties.Items.Add(assayProject.ProjectName);
                }
            }

            cboPolSampleType.Text = reagentNeedleAntifoulingStrategyInfoOld.PolluteProType;
            cboPollutedSource.Text = reagentNeedleAntifoulingStrategyInfoOld.BePollutedProName;
            List<AssayProjectInfo> lstFilterBePros = lstAssayProInfos.FindAll(x => x.SampleType == cboBePolSampleType.SelectedItem.ToString());
            if (lstFilterBePros.Count > 0)
            {
                foreach (AssayProjectInfo assayProject in lstFilterBePros)
                {
                    cboPollutedSource.Properties.Items.Add(assayProject.ProjectName);
                }
            }

            cboBePolSampleType.Text = reagentNeedleAntifoulingStrategyInfoOld.BePollutedProType;
            cboWashing.Text = reagentNeedleAntifoulingStrategyInfoOld.CleaningLiquidName;
            txtUsingVol.Text = reagentNeedleAntifoulingStrategyInfoOld.CleaningLiquidUseVol.ToString();
            txtWashingTimes.Text = reagentNeedleAntifoulingStrategyInfoOld.CleanTimes.ToString();
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

        private void cboPolSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboPollutionSource
            cboPollutionSource.Properties.ReadOnly = false;
            cboPollutionSource.Properties.Items.Clear();
            cboPollutionSource.Text = "请选择";
            List<AssayProjectInfo> lstFilterPros = lstAssayProInfos.FindAll(x => x.SampleType == cboPolSampleType.SelectedItem.ToString());
            if (lstFilterPros.Count > 0)
            {
                foreach (AssayProjectInfo assayProject in lstFilterPros)
                {
                    cboPollutionSource.Properties.Items.Add(assayProject.ProjectName);
                }
            }            
        }


        private void cboBePolSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPollutedSource.Properties.ReadOnly = false;
            cboPollutedSource.Properties.Items.Clear();
            cboPollutedSource.Text = "请选择";
            List<AssayProjectInfo> lstFilterPros = lstAssayProInfos.FindAll(x => x.SampleType == cboBePolSampleType.SelectedItem.ToString());
            if (lstFilterPros.Count > 0)
            {
                foreach (AssayProjectInfo assayProject in lstFilterPros)
                {
                    cboPollutedSource.Properties.Items.Add(assayProject.ProjectName);
                }
            }
        }        
    }
}
