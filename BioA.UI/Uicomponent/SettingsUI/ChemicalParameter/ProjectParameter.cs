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
using DevExpress.XtraGrid.Columns;
using BioA.Common.IO;
using System.Text.RegularExpressions;

namespace BioA.UI
{
    public partial class ProjectParameter : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 数据增、删、查
        /// </summary>
        /// <param name="strAccessSqlMethod">访问数据库方法名</param>
        /// <param name="sender">参数对象</param>
        public delegate void AssayProInfoDelegate(object sender);
        public event AssayProInfoDelegate AssayProInfoEvent;

        private string strReceiveInfo = "";

        public string StrReceiveInfo
        {
            get 
            { 
                return strReceiveInfo;
            }
            set 
            {
                strReceiveInfo = value;
                if (strReceiveInfo == "保存失败！")
                    MessageBoxDraw.ShowMsg(strReceiveInfo, MsgType.Warning);
                else
                    MessageBoxDraw.ShowMsg(strReceiveInfo, MsgType.OK);
            }
        }

       // CheProjectAddOrEdit cheProjectAddOrEdit;
        public ProjectParameter()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;
            this.Dock = DockStyle.Fill;

            cheProjectAddOrEdit.DataHandleEvent += cheProjectAddOrEdit_DataHandleEvent;
            //cheProjectAddOrEdit.StartPosition = FormStartPosition.CenterScreen;
        }

        private void cheProjectAddOrEdit_DataHandleEvent(object sender)
        {
           
            if (AssayProInfoEvent != null)
                AssayProInfoEvent(sender);
        }

        private void ProjectParameter_Load(object sender, EventArgs e)
        {
            //"QueryAssayProAllInfo", null
            BeginInvoke(new Action(InitialControl)); 

            
        }

        private void InitialControl()
        {
            // 分析方法
            foreach (string analizeMethod in RunConfigureUtility.AnalizeMethodList)
            {
                cboAnalizeMethod.Properties.Items.Add(analizeMethod);
            }
            //cboAnalizeMethod.SelectedIndex = 0;
            // 小数位数
            foreach (string resultDecimal in RunConfigureUtility.ResultDecimalList)
            {
                cboDecimal.Properties.Items.Add(resultDecimal);
            }
            //cboDecimal.SelectedIndex = 0;
            // 波长
            cboSecWaveLength.Properties.Items.Add("请选择");
            foreach (string wave in RunConfigureUtility.WaveLengthList)
            {
                cboWaveLength.Properties.Items.Add(wave);
                cboSecWaveLength.Properties.Items.Add(wave);
            }
            //cboWaveLength.SelectedIndex = 0;
            //cboSecWaveLength.SelectedIndex = 0;
            // 范围方向
            foreach (string boundDirection in RunConfigureUtility.BoundDirectionList)
            {
                cboBoundDirection.Properties.Items.Add(boundDirection);
            }
            cboBoundDirection.SelectedIndex = 0;
            // 反应方向
            foreach (string reactionDirection in RunConfigureUtility.ReactionDirectionList)
            {
                cboReactionDirection.Properties.Items.Add(reactionDirection);
            }
            cboReactionDirection.SelectedIndex = 0;
            // 搅拌1强度
            foreach (string stirStrength in RunConfigureUtility.StirStrengthList)
            {
                cboStirring1Intensity.Properties.Items.Add(stirStrength);
            }
            cboStirring1Intensity.SelectedIndex = 1;
            // 搅拌2强度
            foreach (string stirStrength in RunConfigureUtility.StirStrengthList)
            {
                cboStirring2Intensity.Properties.Items.Add(stirStrength);
            }
            cboStirring2Intensity.SelectedIndex = 1;

            // 获取结果单位
            if (AssayProInfoEvent != null)
                AssayProInfoEvent(new CommunicationEntity("QueryProjectResultUnits", null));

            if (AssayProInfoEvent != null)
                AssayProInfoEvent(new CommunicationEntity("QueryAssayProAllInfo", null));
        }

        private List<string> _lstUnits = new List<string>();

        public List<string> LstUnits
        {
            get { return _lstUnits; }
            set
            {
                _lstUnits = value;
                if (_lstUnits != null)
                {
                    this.Invoke(new EventHandler(delegate
                        {
                            cboResultUnit.Properties.Items.Clear();
                            cboResultUnit.Properties.Items.Add("");
                            foreach (string unit in _lstUnits)
                            {
                                cboResultUnit.Properties.Items.Add(unit);
                            }
                        }));                    
                }
            }
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            AssayProjectInfo assayProInfo = new AssayProjectInfo();
            CommunicationEntity communicationEntity = new CommunicationEntity();
            int selectedHandle;
            selectedHandle = this.gridView2.GetSelectedRows()[0];
            assayProInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
            assayProInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();

            if (AssayProInfoEvent != null)
            {
                communicationEntity.StrmethodName = "GetAssayProjectParamInfoByNameAndType";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo);
                AssayProInfoEvent(communicationEntity);
            }
        }


        CheProjectAddOrEdit cheProjectAddOrEdit = new CheProjectAddOrEdit();
            

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            cheProjectAddOrEdit.Text = "新建项目";
            cheProjectAddOrEdit.StartPosition = FormStartPosition.CenterScreen;
            cheProjectAddOrEdit.ShowDialog();
        }

        private void btnEditProject_Click(object sender, EventArgs e)
        {
            cheProjectAddOrEdit.Text = "编辑项目";
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                string str1 = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
                string str2 = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();
                string str3 = this.gridView2.GetRowCellValue(selectedHandle, "项目全称").ToString();
                string str4 = this.gridView2.GetRowCellValue(selectedHandle, "通道号").ToString();
                cheProjectAddOrEdit.FormAdd(str1, str2, str3, str4);
                cheProjectAddOrEdit.ShowDialog();
            }
        }
        AssayProjectParamInfo proParamInfo = new AssayProjectParamInfo();
        public AssayProjectParamInfo AssProParamInfoList
        {
            get { return proParamInfo; }
            set
            {
                proParamInfo = value;
                this.Invoke(new EventHandler(delegate
                {
                    if (proParamInfo.AnalysisMethod != string.Empty)
                        cboAnalizeMethod.SelectedIndex = cboAnalizeMethod.Properties.Items.IndexOf(proParamInfo.AnalysisMethod);
                    else
                    {
                        cboAnalizeMethod.Text = "请选择";
                    }

                    txtCheckLightDot1.Text = proParamInfo.MeasureLightDot1 == 0 ? "" : proParamInfo.MeasureLightDot1.ToString();
                    txtCheckLightDot2.Text = proParamInfo.MeasureLightDot2 == 0 ? "" : proParamInfo.MeasureLightDot2.ToString();
                    txtCheckLightDot3.Text = proParamInfo.MeasureLightDot3 == 0 ? "" : proParamInfo.MeasureLightDot3.ToString();
                    txtCheckLightDot4.Text = proParamInfo.MeasureLightDot4 == 0 ? "" : proParamInfo.MeasureLightDot4.ToString();

                    if (proParamInfo.ResultDecimal != 100000000)
                        cboDecimal.SelectedIndex = cboDecimal.Properties.Items.IndexOf(proParamInfo.ResultDecimal.ToString());
                    else
                        cboDecimal.Text = "请选择";

                    if (proParamInfo.ResultUnit != string.Empty)
                        cboResultUnit.SelectedIndex = cboResultUnit.Properties.Items.IndexOf(proParamInfo.ResultUnit);
                    else
                        cboResultUnit.Text = "请选择";

                    if (proParamInfo.MainWaveLength != 0)
                        cboWaveLength.SelectedIndex = cboWaveLength.Properties.Items.IndexOf(proParamInfo.MainWaveLength.ToString());
                    else
                        cboWaveLength.Text = "请选择";

                    if (proParamInfo.SecWaveLength != 0)
                        cboSecWaveLength.SelectedIndex = cboSecWaveLength.Properties.Items.IndexOf(proParamInfo.SecWaveLength.ToString());
                    else 
                        cboSecWaveLength.Text = "请选择";

                    txtInstrumentFactorA.Text = proParamInfo.InstrumentFactorA == 100000000 ? "" : proParamInfo.InstrumentFactorA.ToString();
                    txtInstrumentFactorB.Text = proParamInfo.InstrumentFactorB == 100000000 ? "" : proParamInfo.InstrumentFactorB.ToString();
                    txtComStosteVol.Text = proParamInfo.ComStosteVol == 100000000 ? "" : proParamInfo.ComStosteVol.ToString();
                    txtComSamVol.Text = proParamInfo.ComSamVol == 100000000 ? "" : proParamInfo.ComSamVol.ToString();
                    txtComDilutionVol.Text = proParamInfo.ComDilutionVol == 100000000 ? "" : proParamInfo.ComDilutionVol.ToString();
                    txtDecStosteVol.Text = proParamInfo.DecStosteVol == 100000000 ? "" : proParamInfo.DecStosteVol.ToString();
                    txtDecSamVol.Text = proParamInfo.DecSamVol == 100000000 ? "" : proParamInfo.DecSamVol.ToString();
                    txtDecDilutionVol.Text = proParamInfo.DecDilutionVol == 100000000 ? "" : proParamInfo.DecDilutionVol.ToString();
                    txtIncStosteVol.Text = proParamInfo.IncStosteVol == 100000000 ? "" : proParamInfo.IncStosteVol.ToString();
                    txtIncSamVol.Text = proParamInfo.IncSamVol == 100000000 ? "" : proParamInfo.IncSamVol.ToString();
                    txtIncDilutionVol.Text = proParamInfo.IncDilutionVol == 100000000 ? "" : proParamInfo.IncDilutionVol.ToString();
                    txtCalibStosteVol.Text = proParamInfo.CalibStosteVol == 100000000 ? "" : proParamInfo.CalibStosteVol.ToString();
                    txtCalibSamVol.Text = proParamInfo.CalibSamVol == 100000000 ? "" : proParamInfo.CalibSamVol.ToString();
                    txtCalibDilutionVol.Text = proParamInfo.CalibDilutionVol == 100000000 ? "" : proParamInfo.CalibDilutionVol.ToString();

                    txtReagent1Name.Text = proParamInfo.Reagent1Name;
                    txtReagent1Pos.Text = proParamInfo.Reagent1Pos;
                    txtReagent1Vol.Text = proParamInfo.Reagent1Vol == 100000000 ? "" : proParamInfo.Reagent1Vol.ToString();
                    if (txtReagent1Name.Text == string.Empty)
                    {
                        txtRea1ValidDate.Text = string.Empty;
                    }
                    else
                    {
                        txtRea1ValidDate.Text = proParamInfo.Reagent1ValidDate.ToShortDateString();
                    }
                    
                    txtReagent2Name.Text = proParamInfo.Reagent2Name;
                    txtReagent2Pos.Text = proParamInfo.Reagent2Pos;
                    txtReagent2Vol.Text = proParamInfo.Reagent2Vol == 100000000 ? "" : proParamInfo.Reagent2Vol.ToString();
                    if (txtReagent2Name.Text == string.Empty)
                    {
                        txtRea2ValidDate.Text = string.Empty;
                    }
                    else
                    {
                        txtRea2ValidDate.Text = proParamInfo.Reagent2ValidDate.ToShortDateString();
                    }

                    txtFirstSlope.Text = proParamInfo.FirstSlope == 100000000 ? "" : proParamInfo.FirstSlope.ToString();
                    txtSecondSlope.Text = proParamInfo.SecondSlope == 100000000 ? "" : proParamInfo.SecondSlope.ToString();
                    txtFirstSlopeHigh.Text = proParamInfo.FirstSlopeHigh == 100000000 ? "" : proParamInfo.FirstSlopeHigh.ToString();
                    txtSecondSlopeHigh.Text = proParamInfo.SecondSlopeHigh == 100000000 ? "" : proParamInfo.SecondSlopeHigh.ToString();
                    txtProLowestBound.Text = proParamInfo.ProLowestBound == 100000000 ? "" : proParamInfo.ProLowestBound.ToString();
                    txtProHighestBound.Text = proParamInfo.ProHighestBound == 100000000 ? "" : proParamInfo.ProHighestBound.ToString();
                    txtPmp1.Text = proParamInfo.Pmp1 == 100000000 ? "" : proParamInfo.Pmp1.ToString();
                    txtPmp2.Text = proParamInfo.Pmp2 == 100000000 ? "" : proParamInfo.Pmp2.ToString();
                    txtPmp3.Text = proParamInfo.Pmp3 == 100000000 ? "" : proParamInfo.Pmp3.ToString();
                    txtPmp4.Text = proParamInfo.Pmp4 == 100000000 ? "" : proParamInfo.Pmp4.ToString();
                    if (proParamInfo.BoundDirection != string.Empty)
                        cboBoundDirection.SelectedIndex = cboBoundDirection.Properties.Items.IndexOf(proParamInfo.BoundDirection);
                    else
                    {
                        cboBoundDirection.SelectedIndex = 0;
                    }
                    txtLimit1.Text = proParamInfo.Limit1 == 100000000 ? "" : proParamInfo.Limit1.ToString();
                    txtLimit2.Text = proParamInfo.Limit2 == 100000000 ? "" : proParamInfo.Limit2.ToString();
                    txtAbsLimitValue.Text = proParamInfo.AbsLimitValue == 100000000 ? "" : proParamInfo.AbsLimitValue.ToString();
                    if (proParamInfo.ReactionDirection != string.Empty)
                        cboReactionDirection.SelectedIndex = cboReactionDirection.Properties.Items.IndexOf(proParamInfo.ReactionDirection);
                    else
                    {
                        cboReactionDirection.SelectedIndex = 0;
                    }
                    if (proParamInfo.Stirring1Intensity != string.Empty)
                        cboStirring1Intensity.SelectedIndex = cboStirring1Intensity.Properties.Items.IndexOf(proParamInfo.Stirring1Intensity);
                    else
                    {
                        cboStirring1Intensity.SelectedIndex = 0;
                    }
                    if (proParamInfo.Stirring2Intensity != string.Empty)
                        cboStirring2Intensity.SelectedIndex = cboStirring2Intensity.Properties.Items.IndexOf(proParamInfo.Stirring2Intensity);
                    else
                    {
                        cboStirring2Intensity.SelectedIndex = 0;
                    }



                    txtReagent1VolSettings.Text = proParamInfo.Reagent1VolSettings == 100000000 ? "" : proParamInfo.Reagent1VolSettings.ToString();
                    txtReagent2VolSettings.Text = proParamInfo.Reagent2VolSettings == 100000000 ? "" : proParamInfo.Reagent2VolSettings.ToString();
                }));
            }
        }

        private List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();

        public List<AssayProjectInfo> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.Invoke(new EventHandler(delegate 
                {
                    lstvProject.RefreshDataSource();
                    int i = 1;
                    DataTable dt = new DataTable();

                    dt.Columns.Add("序号");
                    dt.Columns.Add("项目名称");
                    dt.Columns.Add("类型");
                    dt.Columns.Add("项目全称");
                    dt.Columns.Add("通道号");
                    if (lstAssayProInfos.Count != 0)
                    {
                        foreach (AssayProjectInfo assayProInfo in lstAssayProInfos)
                        {
                            dt.Rows.Add(new object[] { i, assayProInfo.ProjectName, assayProInfo.SampleType, assayProInfo.ProFullName, assayProInfo.ChannelNum });

                            i++;
                        }
                    }

                    this.lstvProject.DataSource = dt;
                    if (this.gridView2.RowCount > 0)
                    {
                        this.gridView1.SelectRow(0);//FocusedRowHandle = 0;
                        lstvProject_Click(null, null);
                    }
                }));
            }
        }

        private void btnDeleteProject_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                if (MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo) == DialogResult.Yes)
                {
                    AssayProjectInfo assayProInfo = new AssayProjectInfo();
                    List<AssayProjectInfo> lstAssayProInfo = new List<AssayProjectInfo>();
                    CommunicationEntity communicationEntity = new CommunicationEntity();
                    int selectedHandle;
                    selectedHandle = this.gridView2.GetSelectedRows()[0];


                    assayProInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
                    assayProInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();
                    assayProInfo.ProFullName = this.gridView2.GetRowCellValue(selectedHandle, "项目全称").ToString();
                    assayProInfo.ChannelNum = this.gridView2.GetRowCellValue(selectedHandle, "通道号").ToString();
                    lstAssayProInfo.Add(assayProInfo);

                    if (AssayProInfoEvent != null)
                    {

                        communicationEntity.StrmethodName = "AssayProjectDelete";
                        communicationEntity.ObjParam = XmlUtility.Serializer(typeof(List<AssayProjectInfo>), lstAssayProInfo);
                        AssayProInfoEvent(communicationEntity);
                    }
                }
            }
        }

       
        private void lstvProject_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(SelectAllProjectPara));
        }

        private void SelectAllProjectPara()
        {
            AssayProjectInfo assayProInfo = new AssayProjectInfo();
            CommunicationEntity communicationEntity = new CommunicationEntity();
            int selectedHandle;

            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                assayProInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
                assayProInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();

                if (AssayProInfoEvent != null)
                {
                    communicationEntity.StrmethodName = "GetAssayProjectParamInfoByNameAndType";
                    communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo);
                    AssayProInfoEvent(communicationEntity);
                }
            }
        }

        private void btnParamSave_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() <= 0)
                return;

            AssayProjectParamInfo proParamInfo = new AssayProjectParamInfo();
            CommunicationEntity communicationEntity = new CommunicationEntity();

            int selectedHandle;
            selectedHandle = this.gridView2.GetSelectedRows()[0];
            proParamInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
            proParamInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();

            if (cboAnalizeMethod.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择分析方法！", MsgType.Warning);
                return;
            }
            else
                proParamInfo.AnalysisMethod = cboAnalizeMethod.SelectedIndex >= 0 ? cboAnalizeMethod.SelectedItem.ToString() : "";

            if (txtCheckLightDot1.Enabled == true)
            {
                if (Regex.IsMatch(txtCheckLightDot1.Text.Trim(), "^([0-9]{1,})$") && System.Convert.ToInt32(txtCheckLightDot1.Text.Trim()) > 0 && System.Convert.ToInt32(txtCheckLightDot1.Text.Trim()) <= RunConfigureUtility.LastPoint)
                {
                    proParamInfo.MeasureLightDot1 = System.Convert.ToInt32(txtCheckLightDot1.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("测光点1输入有误，请重新输入！", MsgType.Warning);
                    return;
                }
            }

            if (txtCheckLightDot2.Enabled == true)
            {
                if (Regex.IsMatch(txtCheckLightDot2.Text.Trim(), "^([0-9]{1,})$") && System.Convert.ToInt32(txtCheckLightDot2.Text.Trim()) > 0 && System.Convert.ToInt32(txtCheckLightDot2.Text.Trim()) <= RunConfigureUtility.LastPoint)
                {
                    proParamInfo.MeasureLightDot2 = System.Convert.ToInt32(txtCheckLightDot2.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("测光点2输入有误，请重新输入！", MsgType.Warning);
                    return;
                }
            }

            if (txtCheckLightDot3.Enabled == true)
            {
                if (Regex.IsMatch(txtCheckLightDot3.Text.Trim(), "^([0-9]{1,})$") && System.Convert.ToInt32(txtCheckLightDot3.Text.Trim()) > 0 && System.Convert.ToInt32(txtCheckLightDot3.Text.Trim()) <= RunConfigureUtility.LastPoint)
                {
                    proParamInfo.MeasureLightDot3 = System.Convert.ToInt32(txtCheckLightDot3.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("测光点3输入有误，请重新输入！", MsgType.Warning);
                    return;
                }
            }

            if (txtCheckLightDot4.Enabled == true)
            {
                if (Regex.IsMatch(txtCheckLightDot4.Text.Trim(), "^([0-9]{1,})$") && System.Convert.ToInt32(txtCheckLightDot4.Text.Trim()) > 0 && System.Convert.ToInt32(txtCheckLightDot4.Text.Trim()) <= RunConfigureUtility.LastPoint)
                {
                    proParamInfo.MeasureLightDot4 = System.Convert.ToInt32(txtCheckLightDot4.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("测光点4输入有误，请重新输入！", MsgType.Warning);
                    return;
                }
            }

            if (txtCheckLightDot1.Text.Trim() != "" && txtCheckLightDot2.Text.Trim() != "")
            {
                if (System.Convert.ToInt32(txtCheckLightDot1.Text.Trim()) > System.Convert.ToInt32(txtCheckLightDot2.Text.Trim()))
                {
                    MessageBoxDraw.ShowMsg("测光点1不能大于测光点2，请重新输入！", MsgType.Warning);
                    return;
                }
            }

            if (txtCheckLightDot3.Text.Trim() != "" && txtCheckLightDot4.Text.Trim() != "")
            {
                if (System.Convert.ToInt32(txtCheckLightDot3.Text.Trim()) > System.Convert.ToInt32(txtCheckLightDot4.Text.Trim()))
                {
                    MessageBoxDraw.ShowMsg("测光点3不能大于测光点4，请重新输入！", MsgType.Warning);
                    return;
                }
            }

            if (cboDecimal.SelectedIndex >= 0)
            {
                proParamInfo.ResultDecimal = System.Convert.ToInt32(cboDecimal.SelectedItem.ToString());
            }

            proParamInfo.ResultUnit = cboResultUnit.SelectedIndex >= 0 ? cboResultUnit.SelectedItem.ToString() : "";
            if (cboWaveLength.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择主波长！", MsgType.Warning);
                return;
            }
            else
                proParamInfo.MainWaveLength = cboWaveLength.SelectedIndex >= 0 ? System.Convert.ToInt32(cboWaveLength.SelectedItem.ToString()) : 100000000;
            // 次波长
            if (cboSecWaveLength.Text == "请选择")
            {

            }
            else
                proParamInfo.SecWaveLength = cboSecWaveLength.SelectedIndex >= 0 ? System.Convert.ToInt32(cboSecWaveLength.SelectedItem.ToString()) : 100000000; 

            if (Regex.IsMatch(txtInstrumentFactorA.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                proParamInfo.InstrumentFactorA = (float)System.Convert.ToDouble(txtInstrumentFactorA.Text);
            }
            else
            {
                MessageBox.Show("仪器因素a值格式有误，请重新输入！");
                return;
            }

            if (Regex.IsMatch(txtInstrumentFactorB.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                proParamInfo.InstrumentFactorB = (float)System.Convert.ToDouble(txtInstrumentFactorB.Text);
            }
            else
            {
                MessageBox.Show("仪器因素b值格式有误，请重新输入！");
                return;
            }


            if (Regex.IsMatch(txtComStosteVol.Text.Trim(), @"^\d+(\.\d+)?$")  &&
                Regex.IsMatch(txtComSamVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtComDilutionVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtDecStosteVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtDecSamVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtDecDilutionVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtIncStosteVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtIncSamVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtIncDilutionVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtCalibStosteVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtCalibSamVol.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtCalibDilutionVol.Text.Trim(), @"^\d+(\.\d+)?$")
                )
            {
                //样本血清常规体积
                if (((float)System.Convert.ToDouble(txtComStosteVol.Text) >= 1.5 && (float)System.Convert.ToDouble(txtComStosteVol.Text) <= 30 &&
                    (float)System.Convert.ToDouble(txtComSamVol.Text) == 0 && (float)System.Convert.ToDouble(txtComDilutionVol.Text) == 0 )||
                    ((float)System.Convert.ToDouble(txtComSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtComDilutionVol.Text) != 0 &&
                    (float)System.Convert.ToDouble(txtComSamVol.Text) >= 2 && (float)System.Convert.ToDouble(txtComSamVol.Text) <= 20 &&
                    (float)System.Convert.ToDouble(txtComDilutionVol.Text) >= 60 && (float)System.Convert.ToDouble(txtComDilutionVol.Text) <= 300))
                {
                    proParamInfo.ComStosteVol = (float)System.Convert.ToDouble(txtComStosteVol.Text);
                    proParamInfo.ComSamVol = (float)System.Convert.ToDouble(txtComSamVol.Text);
                    proParamInfo.ComDilutionVol = (float)System.Convert.ToDouble(txtComDilutionVol.Text);
                }
                else if ((float)System.Convert.ToDouble(txtComStosteVol.Text) < 1.5)
                {
                    MessageBox.Show("血清常规体积：原液体积不能小于1.5 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtComStosteVol.Text) > 30)
                {
                    MessageBox.Show("血清常规体积：原液体积不能大于30 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtComSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtComDilutionVol.Text) == 0)
                {
                    MessageBox.Show("血清常规体积：稀释样本体积是稀释后测试体积，稀释液体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtComDilutionVol.Text) != 0 && (float)System.Convert.ToDouble(txtComSamVol.Text) == 0) 
                {
                    MessageBox.Show("血清常规体积：稀释液体积是稀释样本设置，稀释后加稀释样体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtComSamVol.Text) < 2)
                {
                    MessageBox.Show("血清常规体积：稀释样本体积不能小于2 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtComSamVol.Text) > 20)
                {
                    MessageBox.Show("血清常规体积：稀释样本体积不能大于20 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtComDilutionVol.Text) < 60)
                {
                    MessageBox.Show("血清常规体积：稀释液体积不能小于60 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtComDilutionVol.Text) > 300)
                {
                    MessageBox.Show("血清常规体积：稀释液体积不能大于300 ul");
                    return;
                }
                //样本血清减量体积
                if (((float)System.Convert.ToDouble(txtDecStosteVol.Text) >= 1.5 && (float)System.Convert.ToDouble(txtDecStosteVol.Text) <= 30 &&
                    (float)System.Convert.ToDouble(txtDecSamVol.Text) == 0 && (float)System.Convert.ToDouble(txtDecDilutionVol.Text) == 0 ) ||
                    ((float)System.Convert.ToDouble(txtDecSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtDecDilutionVol.Text) != 0 &&
                    (float)System.Convert.ToDouble(txtDecSamVol.Text) >= 2 && (float)System.Convert.ToDouble(txtDecSamVol.Text) <= 20 &&
                    (float)System.Convert.ToDouble(txtDecDilutionVol.Text) >= 60 && (float)System.Convert.ToDouble(txtDecDilutionVol.Text) <= 300))
                {
                    proParamInfo.DecStosteVol = (float)System.Convert.ToDouble(txtDecStosteVol.Text);
                    proParamInfo.DecSamVol = (float)System.Convert.ToDouble(txtDecSamVol.Text);
                    proParamInfo.DecDilutionVol = (float)System.Convert.ToDouble(txtDecDilutionVol.Text);
                }
                else if ((float)System.Convert.ToDouble(txtDecStosteVol.Text) < 1.5)
                {
                    MessageBox.Show("血清减量体积：原液体积不能小于1.5 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtDecStosteVol.Text) > 30)
                {
                    MessageBox.Show("血清减量体积：原液体积不能大于30 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtDecSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtDecDilutionVol.Text) == 0)
                {
                    MessageBox.Show("血清减量体积：稀释样本体积是稀释后测试体积，稀释液体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtDecDilutionVol.Text) != 0 && (float)System.Convert.ToDouble(txtDecSamVol.Text) == 0)
                {
                    MessageBox.Show("血清减量体积：稀释液体积是稀释样本设置，稀释后加稀释样体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtDecSamVol.Text) < 2)
                {
                    MessageBox.Show("血清减量体积：稀释样本体积不能小于2 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtDecSamVol.Text) > 20)
                {
                    MessageBox.Show("血清减量体积：稀释样本体积不能大于20 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtDecDilutionVol.Text) < 60)
                {
                    MessageBox.Show("血清减量体积：稀释液体积不能小于60 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtDecDilutionVol.Text) > 300)
                {
                    MessageBox.Show("血清减量体积：稀释液体积不能大于300 ul");
                    return;
                }
                //样本血清增量体积
                if (((float)System.Convert.ToDouble(txtDecStosteVol.Text) >= 1.5 && (float)System.Convert.ToDouble(txtDecStosteVol.Text) <= 30 &&
                    (float)System.Convert.ToDouble(txtIncSamVol.Text) == 0 && (float)System.Convert.ToDouble(txtIncDilutionVol.Text) == 0 ) ||
                    ((float)System.Convert.ToDouble(txtIncSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtIncDilutionVol.Text) != 0 &&
                    (float)System.Convert.ToDouble(txtIncSamVol.Text) >= 2 && (float)System.Convert.ToDouble(txtIncSamVol.Text) <= 20 &&
                    (float)System.Convert.ToDouble(txtIncDilutionVol.Text) >= 60 && (float)System.Convert.ToDouble(txtIncDilutionVol.Text) <= 300))
                {
                    proParamInfo.IncStosteVol = (float)System.Convert.ToDouble(txtIncStosteVol.Text);
                    proParamInfo.IncSamVol = (float)System.Convert.ToDouble(txtIncSamVol.Text);
                    proParamInfo.IncDilutionVol = (float)System.Convert.ToDouble(txtIncDilutionVol.Text);
                }
                else if ((float)System.Convert.ToDouble(txtIncStosteVol.Text) < 1.5)
                {
                    MessageBox.Show("血清增量体积：原液体积不能小于1.5 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtIncStosteVol.Text) > 30)
                {
                    MessageBox.Show("血清增量体积：原液体积不能大于30 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtIncSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtIncDilutionVol.Text) == 0)
                {
                    MessageBox.Show("血清增量体积：稀释样本体积是稀释后测试体积，稀释液体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtIncDilutionVol.Text) != 0 && (float)System.Convert.ToDouble(txtIncSamVol.Text) == 0)
                {
                    MessageBox.Show("血清增量体积：稀释液体积是稀释样本设置，稀释后加稀释样体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtIncSamVol.Text) < 2)
                {
                    MessageBox.Show("血清增量体积：稀释样本体积不能小于2 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtIncSamVol.Text) > 20)
                {
                    MessageBox.Show("血清增量体积：稀释样本体积不能大于20 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtIncDilutionVol.Text) < 60)
                {
                    MessageBox.Show("血清增量体积：稀释液体积不能小于60 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtIncDilutionVol.Text) > 300)
                {
                    MessageBox.Show("血清增量体积：稀释液体积不能大于300 ul");
                    return;
                }
                //样本血清定标体积
                if (((float)System.Convert.ToDouble(txtDecStosteVol.Text) >= 1.5 && (float)System.Convert.ToDouble(txtDecStosteVol.Text) <= 30 &&
                    (float)System.Convert.ToDouble(txtCalibSamVol.Text) == 0 && (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) == 0 ) ||
                    ((float)System.Convert.ToDouble(txtCalibSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) != 0 &&
                    (float)System.Convert.ToDouble(txtCalibSamVol.Text) >= 2 && (float)System.Convert.ToDouble(txtCalibSamVol.Text) <= 20 &&
                    (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) >= 60 && (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) <= 300))
                {
                    proParamInfo.CalibStosteVol = (float)(System.Convert.ToDouble(txtCalibStosteVol.Text));
                    proParamInfo.CalibSamVol = (float)System.Convert.ToDouble(txtCalibSamVol.Text);
                    proParamInfo.CalibDilutionVol = (float)System.Convert.ToDouble(txtCalibDilutionVol.Text);
                }
                else if ((float)System.Convert.ToDouble(txtCalibStosteVol.Text) < 1.5)
                {
                    MessageBox.Show("血清定标体积：原液体积不能小于1.5 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtCalibStosteVol.Text) > 30)
                {
                    MessageBox.Show("血清定标体积：原液体积不能大于30 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtCalibSamVol.Text) != 0)
                {
                    MessageBox.Show("血清定标体积：稀释样本体积是稀释后测试体积，稀释液体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtCalibDilutionVol.Text) != 0)
                {
                    MessageBox.Show("血清定标体积：稀释液体积是稀释样本设置，稀释后加稀释样体积不能为0！");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtCalibSamVol.Text) < 2)
                {
                    MessageBox.Show("血清定标体积：稀释样本体积不能小于2 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtCalibSamVol.Text) > 20)
                {
                    MessageBox.Show("血清定标体积：稀释样本体积不能大于20 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtCalibDilutionVol.Text) < 60)
                {
                    MessageBox.Show("血清定标体积：稀释体积不能小于60 ul");
                    return;
                }
                else if ((float)System.Convert.ToDouble(txtCalibDilutionVol.Text) > 300)
                {
                    MessageBox.Show("血清定标体积：稀释体积不能大于300 ul");
                    return;
                }
            }
            else
            {
                MessageBox.Show("样本体积设定数据输入格式有误，请重新输入！");
                return;
            }
            //if ((float)System.Convert.ToDouble(txtComStosteVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtComSamVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtComDilutionVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtDecStosteVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtDecSamVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtDecDilutionVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtIncStosteVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtIncSamVol.Text) > 30 ||
            //    (float)System.Convert.ToDouble(txtIncDilutionVol.Text) > 30)
            //    (float)System.Convert.ToDouble(txtCalibStosteVol.Text)
            //    (float)System.Convert.ToDouble(txtCalibSamVol.Text) 
            //    (float)System.Convert.ToDouble(txtCalibDilutionVol.Text)
            //{
            //    MessageBox.Show("样本体积设定不能大于30vl，请重新输入！");
            //    return;
            //}

            proParamInfo.ComStosteVol = (float)System.Convert.ToDouble(txtComStosteVol.Text);
            proParamInfo.ComSamVol = (float)System.Convert.ToDouble(txtComSamVol.Text);
            proParamInfo.ComDilutionVol = (float)System.Convert.ToDouble(txtComDilutionVol.Text);
            proParamInfo.DecStosteVol = (float)System.Convert.ToDouble(txtDecStosteVol.Text);
            proParamInfo.DecSamVol = (float)System.Convert.ToDouble(txtDecSamVol.Text);
            proParamInfo.DecDilutionVol = (float)System.Convert.ToDouble(txtDecDilutionVol.Text);
            proParamInfo.IncStosteVol = (float)System.Convert.ToDouble(txtIncStosteVol.Text);
            proParamInfo.IncSamVol = (float)System.Convert.ToDouble(txtIncSamVol.Text);
            proParamInfo.IncDilutionVol = (float)System.Convert.ToDouble(txtIncDilutionVol.Text);

            if (txtFirstSlope.Text.Trim() != "" && txtSecondSlope.Text.Trim() != "" &&
                txtFirstSlopeHigh.Text.Trim() != "" && txtSecondSlopeHigh.Text.Trim() != "")
            {
                if (
                    Regex.IsMatch(txtFirstSlope.Text.Trim(), @"^(-?\d+)(\.\d+)?$") &&
                    Regex.IsMatch(txtSecondSlope.Text.Trim(), @"^(-?\d+)(\.\d+)?$") &&
                    Regex.IsMatch(txtFirstSlopeHigh.Text.Trim(), @"^(-?\d+)(\.\d+)?$") &&
                    Regex.IsMatch(txtSecondSlopeHigh.Text.Trim(), @"^(-?\d+)(\.\d+)?$")
                    )
                {
                    proParamInfo.FirstSlope = (float)System.Convert.ToDouble(txtFirstSlope.Text);
                    proParamInfo.SecondSlope = (float)System.Convert.ToDouble(txtSecondSlope.Text);
                    proParamInfo.FirstSlopeHigh = (float)System.Convert.ToDouble(txtFirstSlopeHigh.Text);
                    proParamInfo.SecondSlopeHigh = (float)System.Convert.ToDouble(txtSecondSlopeHigh.Text);
                }
                else
                {
                    MessageBox.Show("线性界限输入格式有误，请重新输入！");
                    return;
                }
            }

            if (txtProLowestBound.Text.Trim() != "" && txtProHighestBound.Text.Trim() != "" &&
                txtPmp1.Text.Trim() != "" && txtPmp2.Text.Trim() != "" && txtPmp3.Text.Trim() != "" && txtPmp4.Text.Trim() != "" &&
                txtLimit1.Text.Trim() != "" && txtLimit2.Text.Trim() != "")
            {
                if (
                    Regex.IsMatch(txtProLowestBound.Text.Trim(), @"^(-?\d+)(\.\d+)?$") &&
                    Regex.IsMatch(txtProHighestBound.Text.Trim(), @"^(-?\d+)(\.\d+)?$") &&
                    Regex.IsMatch(txtPmp1.Text.Trim(), @"^([0-9]{1,})$") &&
                    Regex.IsMatch(txtPmp2.Text.Trim(), @"^([0-9]{1,})$") &&
                    Regex.IsMatch(txtPmp3.Text.Trim(), @"^([0-9]{1,})$") &&
                    Regex.IsMatch(txtPmp4.Text.Trim(), @"^([0-9]{1,})$") &&
                    Regex.IsMatch(txtLimit1.Text.Trim(), @"^(-?\d+)(\.\d+)?$") &&
                    Regex.IsMatch(txtLimit2.Text.Trim(), @"^(-?\d+)(\.\d+)?$")
                    )
                {
                    proParamInfo.ProLowestBound = (float)System.Convert.ToDouble(txtProLowestBound.Text);
                    proParamInfo.ProHighestBound = (float)System.Convert.ToDouble(txtProHighestBound.Text);
                    proParamInfo.Pmp1 = System.Convert.ToInt32(txtPmp1.Text);
                    proParamInfo.Pmp2 = System.Convert.ToInt32(txtPmp2.Text);
                    proParamInfo.Pmp3 = System.Convert.ToInt32(txtPmp3.Text);
                    proParamInfo.Pmp4 = System.Convert.ToInt32(txtPmp4.Text);
                    proParamInfo.BoundDirection = cboBoundDirection.SelectedIndex >= 0 ? cboBoundDirection.SelectedItem.ToString() : "";
                    proParamInfo.Limit1 = (float)System.Convert.ToDouble(txtLimit1.Text);
                    proParamInfo.Limit2 = (float)System.Convert.ToDouble(txtLimit2.Text);
                }
                else
                {
                    MessageBox.Show("前区界限值格式有误，请重新输入！");
                    return;
                }
            }


            if (txtAbsLimitValue.Text.Trim() != "")
            {
                if (Regex.IsMatch(txtAbsLimitValue.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    proParamInfo.AbsLimitValue = (float)System.Convert.ToDouble(txtAbsLimitValue.Text);
                }
                else
                {
                    MessageBox.Show("吸光度界限值格式有误，请重新输入！");
                    return;
                }
            }

            proParamInfo.ReactionDirection = cboReactionDirection.SelectedIndex >= 0 ? cboReactionDirection.SelectedItem.ToString() : "";
            proParamInfo.Stirring1Intensity = cboStirring1Intensity.SelectedIndex >= 0 ? cboStirring1Intensity.SelectedItem.ToString() : "";
            proParamInfo.Stirring2Intensity = cboStirring2Intensity.SelectedIndex >= 0 ? cboStirring2Intensity.SelectedItem.ToString() : "";

            if (Regex.IsMatch(txtReagent1VolSettings.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtReagent2VolSettings.Text.Trim(), @"^\d+(\.\d+)?$"))
            {
                proParamInfo.Reagent1VolSettings = System.Convert.ToInt32(txtReagent1VolSettings.Text);
                proParamInfo.Reagent2VolSettings = System.Convert.ToInt32(txtReagent2VolSettings.Text);
            }
            else
            {
                MessageBox.Show("试剂体积设定格式有误，请重新输入！");
                return;
            }


            if (AssayProInfoEvent != null)
            {
                communicationEntity.StrmethodName = "UpdateAssayProjectParamInfo";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectParamInfo), proParamInfo);
                AssayProInfoEvent(communicationEntity);
            }
                
        }

        private void cboAnalizeMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboAnalizeMethod.SelectedItem.ToString())
            {
                case "一点终点法":
                    txtCheckLightDot1.Enabled = false;
                    txtCheckLightDot2.Enabled = false;
                    txtCheckLightDot3.Enabled = true;
                    txtCheckLightDot4.Enabled = true;
                    txtCheckLightDot1.Text = "";
                    txtCheckLightDot2.Text = "";
                    txtCheckLightDot3.Text = "";
                    txtCheckLightDot4.Text = "";
                    break;
                case "二点终点法":
                    txtCheckLightDot1.Enabled = true;
                    txtCheckLightDot2.Enabled = true;
                    txtCheckLightDot3.Enabled = true;
                    txtCheckLightDot4.Enabled = true;
                    txtCheckLightDot1.Text = "";
                    txtCheckLightDot2.Text = "";
                    txtCheckLightDot3.Text = "";
                    txtCheckLightDot4.Text = "";
                    break;
                case "速率A法":
                    txtCheckLightDot1.Enabled = false;
                    txtCheckLightDot2.Enabled = false;
                    txtCheckLightDot3.Enabled = true;
                    txtCheckLightDot4.Enabled = true;
                    txtCheckLightDot1.Text = "";
                    txtCheckLightDot2.Text = "";
                    txtCheckLightDot3.Text = "";
                    txtCheckLightDot4.Text = "";
                    break;
                case "速率B法":
                    txtCheckLightDot1.Enabled = true;
                    txtCheckLightDot2.Enabled = true;
                    txtCheckLightDot3.Enabled = true;
                    txtCheckLightDot4.Enabled = true;
                    txtCheckLightDot1.Text = "";
                    txtCheckLightDot2.Text = "";
                    txtCheckLightDot3.Text = "";
                    txtCheckLightDot4.Text = "";
                    break;
                default:
                    txtCheckLightDot1.Enabled = false;
                    txtCheckLightDot2.Enabled = false;
                    txtCheckLightDot3.Enabled = false;
                    txtCheckLightDot4.Enabled = false;
                    txtCheckLightDot1.Text = "";
                    txtCheckLightDot2.Text = "";
                    txtCheckLightDot3.Text = "";
                    txtCheckLightDot4.Text = "";
                    break;
            }
        }
    }
}
