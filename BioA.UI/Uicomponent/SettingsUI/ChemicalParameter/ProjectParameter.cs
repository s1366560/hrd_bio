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
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class ProjectParameter : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 数据增、删、查
        /// </summary>
        /// <param name="strAccessSqlMethod">访问数据库方法名</param>
        /// <param name="sender">参数对象</param>
        public delegate void AssayProInfoDelegate(Dictionary<string, object[]> sender);
        public event AssayProInfoDelegate AssayProInfoEvent;

        /// <summary>
        /// 存储所有生化项目信息
        /// </summary>
        private DataTable dt = new DataTable();

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> proParamDic = new Dictionary<string, object[]>();

        //实例化数据库对象
        private SettingsChemicalParameter settingParameter = new SettingsChemicalParameter();
        ///获取所有生化项目范围参数信息
        List<AssayProjectRangeParamInfo> lstRangeParamInfo = new List<AssayProjectRangeParamInfo>();
        /// <summary>
        /// 生化项目范围参数数据表格
        /// </summary>
        DataTable dtRange = new DataTable();
        /// <summary>
        /// 
        /// </summary>
        

        private string strReceiveInfo = "";
        /// <summary>
        /// 显示项目参数保存成功或者失败
        /// </summary>
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
                {
                    
                }
                else
                {
                    lstAssayProParamInfoAll.RemoveAll(x => x.ProjectName == saveProParamInfo.ProjectName && x.SampleType == saveProParamInfo.SampleType);
                    lstAssayProParamInfoAll.Add(saveProParamInfo);
                    QueryResultSetTb queryRsult = new QueryResultSetTb(true);
                    List<ResultSetInfo> lstqueryResult = QueryResultSetTb.QueryResultSetInfo;
                }
                MessageBoxDraw.ShowMsg(strReceiveInfo, MsgType.OK);
            }
        }
        /// <summary>
        /// 处理删除和修改项目信息后关闭子窗体
        /// </summary>
        private int _EnditOrDeleteProInfoHandle;
        public int EnditOrDeleteProInfoHandle
        {
            get { return _EnditOrDeleteProInfoHandle; }
            set
            {
                _EnditOrDeleteProInfoHandle = value;
                this.Invoke(new EventHandler(delegate { cheProjectAddOrEdit.Close(); }));
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
            this.CBSuperiorLimit.Enabled = false;
            this.CBLowerLimit.Enabled = false;

            dt.Columns.Add("序号");
            dt.Columns.Add("项目名称");
            dt.Columns.Add("类型");
            dt.Columns.Add("项目全称");
            dt.Columns.Add("通道号");
            this.lstvProject.DataSource = dt;
            dtRange.Columns.Add("样本类型");
            dtRange.Columns.Add("年龄范围");
            dtRange.Columns.Add("男（浓度范围）");
            dtRange.Columns.Add("女（浓度范围）");
            this.grpRangeParam.DataSource = dtRange;
            // 分析方法
            cboAnalizeMethod.Properties.Items.AddRange(RunConfigureUtility.AnalizeMethodList);
            //测光点范围
            txtCheckLightDot1.Properties.Items.Add("0");
            txtCheckLightDot1.Properties.Items.AddRange(RunConfigureUtility.ReactionPoints);
            txtCheckLightDot2.Properties.Items.Add("0");
            txtCheckLightDot2.Properties.Items.AddRange(RunConfigureUtility.ReactionPoints);
            txtCheckLightDot3.Properties.Items.AddRange(RunConfigureUtility.ReactionPoints);
            txtCheckLightDot4.Properties.Items.AddRange(RunConfigureUtility.ReactionPoints);
            // 小数位数
            cboDecimal.Properties.Items.AddRange(RunConfigureUtility.ResultDecimalList);
            // 波长
            cboSecWaveLength.Properties.Items.Add("请选择");
            cboWaveLength.Properties.Items.AddRange(RunConfigureUtility.WaveLengthList);
            cboSecWaveLength.Properties.Items.AddRange(RunConfigureUtility.WaveLengthList);
            // 反应方向
            cboReactionDirection.Properties.Items.AddRange(RunConfigureUtility.ReactionDirectionList);
            cboReactionDirection.SelectedIndex = 0;
            // 搅拌1强度
            cboStirring1Intensity.Properties.Items.AddRange(RunConfigureUtility.StirStrengthList);
            cboStirring1Intensity.SelectedIndex = 1;
            // 搅拌2强度
            cboStirring2Intensity.Properties.Items.AddRange(RunConfigureUtility.StirStrengthList);
            cboStirring2Intensity.SelectedIndex = 1;
            //cheProjectAddOrEdit.StartPosition = FormStartPosition.CenterScreen;

            this.txtSerumManConsLow.LostFocus += SerumManConsLowMaxAndMinValue;
            this.txtSerumManConsHigh.LostFocus += SerumManConsHighMaxAndMinValue;
            this.txtSerumWomanConsLow.LostFocus += SerumWomanConsLowMaxAndMinValue;
            this.txtSerumWomanConsHigh.LostFocus += SerumWomanConsHighMaxAndMinValue;
        }


        ////生化项目信息
        //private AssayProjectInfo _AssayProjectInfo = new AssayProjectInfo();
        private void cheProjectAddOrEdit_DataHandleEvent(Dictionary<string, object[]> sender)
        {

            if (AssayProInfoEvent != null && sender != null)
            {
                AssayProInfoEvent(sender);
                //从字典中获取的值没有被其他地方使用，所有先注释掉
                //foreach (KeyValuePair<string, object[]> item in sender)
                //{
                //    if(item.Key == "AssayProjectAdd")
                //    {
                //        if (_AssayProjectInfo == null)
                //            _AssayProjectInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), item.Value[0].ToString()) as AssayProjectInfo;
                //        else
                //        {
                //            _AssayProjectInfo = null;
                //            _AssayProjectInfo = XmlUtility.Deserialize(typeof(AssayProjectInfo), item.Value[0].ToString()) as AssayProjectInfo;
                //        }
                //    }
                //}
            }
        }
        /// <summary>
        /// 清空缓存的成员属性值
        /// </summary>
        public void ClearProjectParamMemberPropertier()
        {
            showLstRangeParam.Clear();
            lstRangeParamInfo.Clear();
            proParamDic.Clear();
        }

        public void ProjectParameter_Load(object sender, EventArgs e)
        {
            this.InitialControl();
        }

        private void InitialControl()
        {
            
            //获取所有生化项目
            List<AssayProjectInfo> lstProjectInfos = settingParameter.QueryAssayProAllInfo("QueryAssayProAllInfo", null);
            this.LstAssayProInfos = lstProjectInfos;
            // 获取结果单位
            LstUnits = settingParameter.QueryProjectResultUnits("QueryProjectResultUnits");
            //获取生化项目范围参数
            lstRangeParamInfo = settingParameter.QueryRangeParam("QueryRangeParam");
            //获取所有生化项目对应的参数信息
            LstAssayProParamInfoAll = settingParameter.QueryAssayProjectParamInfoAll("QueryAssayProjectParamInfoAll", QueryResultSetTb.QueryResultSetInfo);
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
                    cboResultUnit.Properties.Items.Clear();
                    cboResultUnit.Properties.Items.Add("");
                    foreach (string unit in _lstUnits)
                    {
                        cboResultUnit.Properties.Items.Add(unit);
                    }
                }
            }
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetele_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                AssayProjectInfo assayProInfo = new AssayProjectInfo();
                int selectedHandle;
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                assayProInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
                assayProInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();
                foreach (AssayProjectParamInfo assayProParam in lstAssayProParamInfoAll)
                {
                    if (assayProParam.ProjectName == assayProInfo.ProjectName && assayProParam.SampleType == assayProInfo.SampleType)
                    {
                        this.AssProParamInfoList = assayProParam;
                    }
                }
            }
        }

        /// <summary>
        /// 新增和编辑界面
        /// </summary>
        CheProjectAddOrEdit cheProjectAddOrEdit;
        private void btnAddProject_Click(object sender, EventArgs e)
        {
        
            if (cheProjectAddOrEdit == null)
            {
                cheProjectAddOrEdit = new CheProjectAddOrEdit();
                cheProjectAddOrEdit.DataHandleEvent += cheProjectAddOrEdit_DataHandleEvent;
                cheProjectAddOrEdit.StartPosition = FormStartPosition.CenterScreen;
            }
            cheProjectAddOrEdit.Text = "新建项目";
            cheProjectAddOrEdit.LstAssayProjectInfo = lstAssayProInfos;
            cheProjectAddOrEdit.ShowDialog();
        }

        private void btnEditProject_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                if (cheProjectAddOrEdit == null)
                {
                    cheProjectAddOrEdit = new CheProjectAddOrEdit();
                    cheProjectAddOrEdit.DataHandleEvent += cheProjectAddOrEdit_DataHandleEvent;
                    cheProjectAddOrEdit.StartPosition = FormStartPosition.CenterScreen;
                }
                cheProjectAddOrEdit.BeforeClearingTheData();
                cheProjectAddOrEdit.Text = "编辑项目";
                AssayProjectInfo assayProInfo = new AssayProjectInfo();
                int selectedHandle;
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                assayProInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
                assayProInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();
                assayProInfo.ProFullName = this.gridView2.GetRowCellValue(selectedHandle, "项目全称").ToString();
                assayProInfo.ChannelNum = this.gridView2.GetRowCellValue(selectedHandle, "通道号").ToString();
                cheProjectAddOrEdit.FormAdd(assayProInfo);
                cheProjectAddOrEdit.ShowDialog();
            }
        }

        private List<AssayProjectParamInfo> lstAssayProParamInfoAll = new List<AssayProjectParamInfo>();
        /// <summary>
        /// 存储所有生化项目参数信息
        /// </summary>
        public List<AssayProjectParamInfo> LstAssayProParamInfoAll
        {
            get { return lstAssayProParamInfoAll; }
            set { lstAssayProParamInfoAll = value; lstvProject_Click(null, null); }
        }
        //显示项目范围参数信息
       List<AssayProjectRangeParamInfo> showLstRangeParam = new List<AssayProjectRangeParamInfo>();

        private AssayProjectParamInfo proParamInfo = new AssayProjectParamInfo();
        /// <summary>
        /// 显示生化项目对应的生化项目参数信息
        /// </summary>
        public AssayProjectParamInfo AssProParamInfoList
        {
            get { return proParamInfo; }
            set
            {
                proParamInfo = value;
                //检测方法
                if (proParamInfo.AnalysisMethod != string.Empty && proParamInfo.AnalysisMethod != null)
                    cboAnalizeMethod.SelectedIndex = cboAnalizeMethod.Properties.Items.IndexOf(proParamInfo.AnalysisMethod);
                else
                    cboAnalizeMethod.Text = "请选择";
                //测光点1
                if (proParamInfo.MeasureLightDot1 == 0)
                    txtCheckLightDot1.SelectedIndex = 0;
                else
                    txtCheckLightDot1.Text = proParamInfo.MeasureLightDot1.ToString();
                //测光点2
                if (proParamInfo.MeasureLightDot2 == 0)
                    txtCheckLightDot2.SelectedIndex = 0;
                else
                    txtCheckLightDot2.Text = proParamInfo.MeasureLightDot2.ToString();
                //测光点3
                if (proParamInfo.MeasureLightDot3 == 0)
                    txtCheckLightDot3.SelectedIndex = 42;
                else
                    txtCheckLightDot3.Text = proParamInfo.MeasureLightDot3.ToString();
                //测光点4
                if (proParamInfo.MeasureLightDot4 == 0)
                    txtCheckLightDot4.SelectedIndex = 42;
                else
                    txtCheckLightDot4.Text = proParamInfo.MeasureLightDot4.ToString();
                //小数点
                if (proParamInfo.ResultDecimal != 100000000)
                    cboDecimal.SelectedIndex = cboDecimal.Properties.Items.IndexOf(proParamInfo.ResultDecimal.ToString());
                else
                    cboDecimal.SelectedIndex = 4;
                //结果单位
                if (proParamInfo.ResultUnit != string.Empty)
                    cboResultUnit.SelectedIndex = cboResultUnit.Properties.Items.IndexOf(proParamInfo.ResultUnit);
                else
                    cboResultUnit.Text = "请选择";
                //主波长
                if (proParamInfo.MainWaveLength != 0)
                    cboWaveLength.SelectedIndex = cboWaveLength.Properties.Items.IndexOf(proParamInfo.MainWaveLength.ToString());
                else
                    cboWaveLength.Text = "请选择";
                //次波长
                if (proParamInfo.SecWaveLength != 0)
                    cboSecWaveLength.SelectedIndex = cboSecWaveLength.Properties.Items.IndexOf(proParamInfo.SecWaveLength.ToString());
                else
                    cboSecWaveLength.Text = "请选择";
                //仪器因素法A
                if (proParamInfo.InstrumentFactorA == 100000000)
                    txtInstrumentFactorA.Text = "";
                else if (proParamInfo.InstrumentFactorA == 0)
                    txtInstrumentFactorA.Text = "1";
                else
                    proParamInfo.InstrumentFactorA.ToString();
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

                txtFirstSlope.Text = proParamInfo.FirstSlope == 100000000 ? "0" : proParamInfo.FirstSlope.ToString();
                txtFirstSlopeHigh.Text = proParamInfo.FirstSlopeHigh == 100000000 ? "0" : proParamInfo.FirstSlopeHigh.ToString();
                txtProLowestBound.Text = proParamInfo.ProLowestBound == 100000000 ? "0" : proParamInfo.ProLowestBound.ToString();
                txtAbsLimitValue.Text = proParamInfo.LimitValue == 100000000 ? "0" : proParamInfo.LimitValue.ToString();
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
                if (this.showLstRangeParam.Count <= 0)
                {

                }
                else
                {
                    LoandProjectRangeParam(this.showLstRangeParam);
                }

                txtReagent1VolSettings.Text = proParamInfo.Reagent1VolSettings == 100000000 ? "0" : proParamInfo.Reagent1VolSettings.ToString();
                txtReagent2VolSettings.Text = proParamInfo.Reagent2VolSettings == 100000000 ? "0" : proParamInfo.Reagent2VolSettings.ToString();
                txtSerumMinValue.Text = proParamInfo.SerumCriticalMinimum == 100000000 ? "0" : proParamInfo.SerumCriticalMinimum.ToString();
                txtSerumMaxValue.Text = proParamInfo.SerumCriticalMaximum == 100000000 ? "0" : proParamInfo.SerumCriticalMaximum.ToString();
                txtReagentMinValue.Text = proParamInfo.ReagentBlankMinimum == 100000000 ? "0" : proParamInfo.ReagentBlankMinimum.ToString();
                txtReagentMaxValue.Text = proParamInfo.ReagentBlankMaximum == 100000000 ? "0" : proParamInfo.ReagentBlankMaximum.ToString();
            }
        }
        /// <summary>
        /// 显示生化项目范围参数
        /// </summary>
        private void LoandProjectRangeParam(List<AssayProjectRangeParamInfo> lstrangeParam)
        {
            CloseRangeParameInfo();
            dtRange.Rows.Clear();
            foreach (AssayProjectRangeParamInfo r in lstrangeParam)
            {
                chkAutoResurvey.Checked = r.AutoRerun;
                if (r.AgeLow1 != -100000000 && r.AgeHigh1 != 100000000)
                {
                    dtRange.Rows.Add(r.SampleType, string.Format(r.AgeLow1 + " - " + r.AgeHigh1), string.Format(r.ManConsLow1.ToString() + " - " +  r.ManConsHigh1.ToString()), string.Format(r.WomanConsLow1.ToString() + " - " + r.WomanConsHigh1.ToString()));
                }
                if (r.AgeLow2 != -100000000 && r.AgeHigh2 != 100000000)
                {
                    dtRange.Rows.Add(r.SampleType, string.Format(r.AgeLow2 + " - " + r.AgeHigh2), string.Format(r.ManConsLow2.ToString() + " - " + r.ManConsHigh2.ToString()), string.Format(r.WomanConsLow2.ToString() + " - " + r.WomanConsHigh2.ToString()));
                } 
                if (r.AgeLow3 != -100000000 && r.AgeHigh3 != 100000000)
                {
                    dtRange.Rows.Add(r.SampleType, string.Format(r.AgeLow3 + " - " + r.AgeHigh3), string.Format(r.ManConsLow3.ToString() + " - " + r.ManConsHigh3.ToString()), string.Format(r.WomanConsLow3.ToString() + " - " + r.WomanConsHigh3.ToString()));
                }
                if (r.AgeLow4 != -100000000 && r.AgeHigh4 != 100000000)
                {
                    dtRange.Rows.Add(r.SampleType, string.Format(r.AgeLow4 + " - " + r.AgeHigh4), string.Format(r.ManConsLow4.ToString() + " - " + r.ManConsHigh4.ToString()), string.Format(r.WomanConsLow4.ToString() + " - " + r.WomanConsHigh4.ToString()));
                }
                
            }
            this.grpRangeParam.DataSource = dtRange;
        }

        //生化项目参数信息
        private AssayProjectParamInfo _AssayProjectParamInfo= new AssayProjectParamInfo();
        /// <summary>
        /// 处理生化项目保存是否成功
        /// </summary>
        public AssayProjectParamInfo AssayProjectParamInfos
        {
            get { return _AssayProjectParamInfo; }
            set
            {
                _AssayProjectParamInfo = value;
                this.Invoke(new EventHandler(delegate
                {
                    if (_AssayProjectParamInfo != null)
                    {
                        this.lstAssayProParamInfoAll.Add(_AssayProjectParamInfo);
                        List<AssayProjectInfo> lstProjectInfos = new SettingsChemicalParameter().QueryAssayProAllInfo("QueryAssayProAllInfo", null);
                        this.LstAssayProInfos = lstProjectInfos;
                        MessageBox.Show("项目保存成功！");
                    }
                    else
                    {
                        MessageBox.Show("项目保存失败！");
                    }

                    cheProjectAddOrEdit.BeforeClearingTheData();
                    cheProjectAddOrEdit.Close();
                }));
            }
        }

        private List<AssayProjectInfo> listAssayProjectInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 存储所有项目信息
        /// </summary>
        public List<AssayProjectInfo> ListAssayprojectInfos
        {
            get { return listAssayProjectInfos; }
            set { listAssayProjectInfos = value; }
        }

        private List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 显示所有生化项目信息
        /// </summary>
        public List<AssayProjectInfo> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                int i = 1;
                dt.Rows.Clear();
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
                    //lstvProject_Click(null, null);
                }
            }
        }

        private void btnDeleteProject_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                if (MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo) == DialogResult.Yes)
                {
                    AssayProjectInfo assayProInfo = new AssayProjectInfo();
                    int selectedHandle;
                    selectedHandle = this.gridView2.GetSelectedRows()[0];


                    assayProInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
                    assayProInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();

                    if (AssayProInfoEvent != null)
                    {
                        proParamDic.Clear();
                        proParamDic.Add("AssayProjectDelete", new object[] { XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo) });
                        AssayProInfoEvent(proParamDic);
                    }
                }
            }
        }
        /// <summary>
        /// 生化项目信息列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvProject_Click(object sender, EventArgs e)
        {
            AssayProjectInfo assayProInfo = new AssayProjectInfo();
            int selectedHandle;

            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                assayProInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
                assayProInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();
                foreach (AssayProjectParamInfo assayProParam in lstAssayProParamInfoAll)
                {
                    if (assayProParam.ProjectName == assayProInfo.ProjectName && assayProParam.SampleType == assayProInfo.SampleType)
                    {
                        List<AssayProjectRangeParamInfo> range = lstRangeParamInfo.FindAll(r => r.ProjectName == assayProInfo.ProjectName);
                        if (range != null)
                            this.showLstRangeParam = range;
                        else
                            this.showLstRangeParam.Clear();
                        this.AssProParamInfoList = assayProParam;
                    }
                }
            }
        }
        /// <summary>
        /// 存储（保存生化项目参数信息）
        /// </summary>
        private AssayProjectParamInfo saveProParamInfo;

        private void btnParamSave_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() <= 0)
                return;

            saveProParamInfo = new AssayProjectParamInfo();

            int selectedHandle;
            selectedHandle = this.gridView2.GetSelectedRows()[0];
            saveProParamInfo.ProjectName = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
            saveProParamInfo.SampleType = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();
            
            if (cboAnalizeMethod.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择分析方法！", MsgType.Warning);
                return;
            }
            else
                saveProParamInfo.AnalysisMethod = cboAnalizeMethod.SelectedIndex >= 0 ? cboAnalizeMethod.SelectedItem.ToString() : "";

            if (txtCheckLightDot1.Enabled == true)
            {
                if (Regex.IsMatch(txtCheckLightDot1.Text.Trim(), "^([0-9]{1,})$") && System.Convert.ToInt32(txtCheckLightDot1.Text.Trim()) > 0 && System.Convert.ToInt32(txtCheckLightDot1.Text.Trim()) <= RunConfigureUtility.LastPoint)
                {
                    saveProParamInfo.MeasureLightDot1 = System.Convert.ToInt32(txtCheckLightDot1.Text);
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
                    saveProParamInfo.MeasureLightDot2 = System.Convert.ToInt32(txtCheckLightDot2.Text);
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
                    saveProParamInfo.MeasureLightDot3 = System.Convert.ToInt32(txtCheckLightDot3.Text);
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
                    saveProParamInfo.MeasureLightDot4 = System.Convert.ToInt32(txtCheckLightDot4.Text);
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
                saveProParamInfo.ResultDecimal = System.Convert.ToInt32(cboDecimal.SelectedItem.ToString());
            }

            saveProParamInfo.ResultUnit = cboResultUnit.SelectedIndex >= 0 ? cboResultUnit.SelectedItem.ToString() : "";
            if (cboWaveLength.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择主波长！", MsgType.Warning);
                return;
            }
            else
                saveProParamInfo.MainWaveLength = cboWaveLength.SelectedIndex >= 0 ? System.Convert.ToInt32(cboWaveLength.SelectedItem.ToString()) : 100000000;
            // 次波长
            if (cboSecWaveLength.Text == "请选择")
            {

            }
            else
                saveProParamInfo.SecWaveLength = cboSecWaveLength.SelectedIndex >= 0 ? System.Convert.ToInt32(cboSecWaveLength.SelectedItem.ToString()) : 100000000; 

            if (Regex.IsMatch(txtInstrumentFactorA.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                saveProParamInfo.InstrumentFactorA = (float)System.Convert.ToDouble(txtInstrumentFactorA.Text);
            }
            else
            {
                MessageBox.Show("仪器因素a值格式有误，请重新输入！");
                return;
            }

            if (Regex.IsMatch(txtInstrumentFactorB.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                saveProParamInfo.InstrumentFactorB = (float)System.Convert.ToDouble(txtInstrumentFactorB.Text);
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
                    saveProParamInfo.ComStosteVol = (float)System.Convert.ToDouble(txtComStosteVol.Text);
                    saveProParamInfo.ComSamVol = (float)System.Convert.ToDouble(txtComSamVol.Text);
                    saveProParamInfo.ComDilutionVol = (float)System.Convert.ToDouble(txtComDilutionVol.Text);
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
                    saveProParamInfo.DecStosteVol = (float)System.Convert.ToDouble(txtDecStosteVol.Text);
                    saveProParamInfo.DecSamVol = (float)System.Convert.ToDouble(txtDecSamVol.Text);
                    saveProParamInfo.DecDilutionVol = (float)System.Convert.ToDouble(txtDecDilutionVol.Text);
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
                if (((float)System.Convert.ToDouble(txtIncStosteVol.Text) >= 1.5 && (float)System.Convert.ToDouble(txtIncStosteVol.Text) <= 30 &&
                    (float)System.Convert.ToDouble(txtIncSamVol.Text) == 0 && (float)System.Convert.ToDouble(txtIncDilutionVol.Text) == 0 ) ||
                    ((float)System.Convert.ToDouble(txtIncSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtIncDilutionVol.Text) != 0 &&
                    (float)System.Convert.ToDouble(txtIncSamVol.Text) >= 2 && (float)System.Convert.ToDouble(txtIncSamVol.Text) <= 20 &&
                    (float)System.Convert.ToDouble(txtIncDilutionVol.Text) >= 60 && (float)System.Convert.ToDouble(txtIncDilutionVol.Text) <= 300))
                {
                    saveProParamInfo.IncStosteVol = (float)System.Convert.ToDouble(txtIncStosteVol.Text);
                    saveProParamInfo.IncSamVol = (float)System.Convert.ToDouble(txtIncSamVol.Text);
                    saveProParamInfo.IncDilutionVol = (float)System.Convert.ToDouble(txtIncDilutionVol.Text);
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
                if (((float)System.Convert.ToDouble(txtCalibStosteVol .Text) >= 1.5 && (float)System.Convert.ToDouble(txtCalibStosteVol.Text) <= 30 &&
                    (float)System.Convert.ToDouble(txtCalibSamVol.Text) == 0 && (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) == 0 ) ||
                    ((float)System.Convert.ToDouble(txtCalibSamVol.Text) != 0 && (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) != 0 &&
                    (float)System.Convert.ToDouble(txtCalibSamVol.Text) >= 2 && (float)System.Convert.ToDouble(txtCalibSamVol.Text) <= 20 &&
                    (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) >= 60 && (float)System.Convert.ToDouble(txtCalibDilutionVol.Text) <= 300))
                {
                    saveProParamInfo.CalibStosteVol = (float)(System.Convert.ToDouble(txtCalibStosteVol.Text));
                    saveProParamInfo.CalibSamVol = (float)System.Convert.ToDouble(txtCalibSamVol.Text);
                    saveProParamInfo.CalibDilutionVol = (float)System.Convert.ToDouble(txtCalibDilutionVol.Text);
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
            saveProParamInfo.ComStosteVol = float.Parse(txtComStosteVol.Text);
            saveProParamInfo.ComSamVol = float.Parse(txtComSamVol.Text);
            saveProParamInfo.ComDilutionVol = float.Parse(txtComDilutionVol.Text);
            saveProParamInfo.DecStosteVol = float.Parse(txtDecStosteVol.Text);
            saveProParamInfo.DecSamVol = float.Parse(txtDecSamVol.Text);
            saveProParamInfo.DecDilutionVol = float.Parse(txtDecDilutionVol.Text);
            saveProParamInfo.IncStosteVol = float.Parse(txtIncStosteVol.Text);
            saveProParamInfo.IncSamVol = float.Parse(txtIncSamVol.Text);
            saveProParamInfo.IncDilutionVol = float.Parse(txtIncDilutionVol.Text);

            if (txtFirstSlope.Text.Trim() != "" && txtFirstSlopeHigh.Text.Trim() != "" )
            {
                if (
                    Regex.IsMatch(txtFirstSlope.Text.Trim(), @"^(-?\d+)(\.\d+)?$") &&
                    Regex.IsMatch(txtFirstSlopeHigh.Text.Trim(), @"^(-?\d+)(\.\d+)?$")
                    )
                {
                    saveProParamInfo.FirstSlope = float.Parse(txtFirstSlope.Text);
                    saveProParamInfo.FirstSlopeHigh = float.Parse(txtFirstSlopeHigh.Text);
                }
                else
                {
                    MessageBox.Show("线性界限输入格式有误，请重新输入！");
                    return;
                }
            }

            if (txtProLowestBound.Text.Trim() != "")
            {
                if (Regex.IsMatch(txtProLowestBound.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    saveProParamInfo.ProLowestBound = float.Parse(txtProLowestBound.Text);
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
                    saveProParamInfo.LimitValue = float.Parse(txtAbsLimitValue.Text);
                }
                else
                {
                    MessageBox.Show("吸光度界限值格式有误，请重新输入！");
                    return;
                }
            }

            saveProParamInfo.ReactionDirection = cboReactionDirection.SelectedIndex >= 0 ? cboReactionDirection.SelectedItem.ToString() : "";
            saveProParamInfo.Stirring1Intensity = cboStirring1Intensity.SelectedIndex >= 0 ? cboStirring1Intensity.SelectedItem.ToString() : "";
            saveProParamInfo.Stirring2Intensity = cboStirring2Intensity.SelectedIndex >= 0 ? cboStirring2Intensity.SelectedItem.ToString() : "";

            if (Regex.IsMatch(txtReagent1VolSettings.Text.Trim(), @"^\d+(\.\d+)?$") &&
                Regex.IsMatch(txtReagent2VolSettings.Text.Trim(), @"^\d+(\.\d+)?$"))
            {
                if (int.Parse(txtReagent1VolSettings.Text.Trim().ToString()) < 10 || int.Parse(txtReagent1VolSettings.Text.Trim().ToString()) > 300)
                {
                    MessageBox.Show("试剂体积设定限制在10~300，请重新输入！");
                    this.txtReagent1VolSettings.Focus();
                    return;
                }
                else if (int.Parse(txtReagent2VolSettings.Text.Trim().ToString()) < 10 || int.Parse(txtReagent2VolSettings.Text.Trim().ToString()) > 180)
                {
                    MessageBox.Show("试剂体积设定限制在10~180，请重新输入！");
                    this.txtReagent2VolSettings.Focus();
                    return;
                }
                else
                {
                    saveProParamInfo.Reagent1VolSettings = System.Convert.ToInt32(txtReagent1VolSettings.Text);
                    saveProParamInfo.Reagent2VolSettings = System.Convert.ToInt32(txtReagent2VolSettings.Text);
                }
            }
            else
            {
                MessageBox.Show("试剂体积设定格式有误，请重新输入！");
                return;
            }

            if (txtSerumMinValue.Text.Trim() != "" && txtSerumMaxValue.Text.Trim() != "" || Regex.IsMatch(txtSerumMinValue.Text.Trim(), @"^\d+(\.\d+)?$") && Regex.IsMatch(txtSerumMaxValue.Text.Trim(), @"^\d+(\.\d+)?$"))
            {
                saveProParamInfo.SerumCriticalMinimum = float.Parse(txtSerumMinValue.Text);
                saveProParamInfo.SerumCriticalMaximum = float.Parse(txtSerumMaxValue.Text);
            }
            else
            {
                MessageBoxDraw.ShowMsg("血清临界值格式有误,请重新输入", MsgType.OK);
                return;
            }
            if (txtReagentMinValue.Text.Trim() != "" && txtReagentMaxValue.Text.Trim() != "" || Regex.IsMatch(txtReagentMinValue.Text.Trim(), @"^\d+(\.\d+)?$") && Regex.IsMatch(txtReagentMaxValue.Text.Trim(), @"^\d+(\.\d+)?$"))
            {
                saveProParamInfo.ReagentBlankMinimum = float.Parse(txtReagentMinValue.Text);
                saveProParamInfo.ReagentBlankMaximum = float.Parse(txtReagentMaxValue.Text);
            }
            else
            {
                MessageBoxDraw.ShowMsg("试剂空白值格式有误,请重新输入", MsgType.OK);
                return;
            }

            AssayProjectRangeParamInfo rangParamInfo = this.btnSaveRangeParameterInfo();
            if (rangParamInfo == null)
            {
                return;
            }
            int result = new SettingsChemicalParameter().UpdateAssayProjectParamInfo("UpdateAssayProjectParamInfo", saveProParamInfo);
            new SettingsChemicalParameter().UpdateRangeParamByProNameAndType("UpdateRangeParamByProNameAndType", rangParamInfo);
            if (result > 0)
            {
                MessageBoxDraw.ShowMsg("保存成功！",MsgType.OK);
                AssayProjectParamInfo assay = lstAssayProParamInfoAll.SingleOrDefault(s => s.ProjectName == saveProParamInfo.ProjectName);
                saveProParamInfo.Reagent1Name = assay.Reagent1Name;
                saveProParamInfo.Reagent1Pos = assay.Reagent1Pos;
                saveProParamInfo.Reagent1Vol = assay.Reagent1Vol;
                saveProParamInfo.Reagent1ValidDate = assay.Reagent1ValidDate;
                saveProParamInfo.Reagent2Name = assay.Reagent2Name;
                saveProParamInfo.Reagent2Pos = assay.Reagent2Pos;
                saveProParamInfo.Reagent2Vol = assay.Reagent2Vol;
                saveProParamInfo.Reagent2ValidDate = assay.Reagent2ValidDate;
                lstAssayProParamInfoAll.RemoveAll(a => a.ProjectName == saveProParamInfo.ProjectName);
                lstRangeParamInfo.RemoveAll(r => r.ProjectName == rangParamInfo.ProjectName);
                lstAssayProParamInfoAll.Add(saveProParamInfo);
                lstRangeParamInfo.Add(rangParamInfo);
                lstvProject_Click(null,null);
            }
        }

        /// <summary>
        /// 保存范围参数信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private AssayProjectRangeParamInfo btnSaveRangeParameterInfo()
        {
            AssayProjectRangeParamInfo parameterInfo = new AssayProjectRangeParamInfo();
            parameterInfo.ProjectName = this.gridView2.GetRowCellValue(this.gridView2.GetSelectedRows()[0], "项目名称").ToString();
            parameterInfo.SampleType = this.gridView2.GetRowCellValue(this.gridView2.GetSelectedRows()[0], "类型").ToString();

            parameterInfo.AutoRerun = chkAutoResurvey.Checked;

            if (this.gridView3.RowCount < 0)
            {

            }
            else
            {
                for (int i = 0; i < this.gridView3.RowCount; i++)
                {
                    string[] age = this.gridView3.GetRowCellValue(i, "年龄范围").ToString().Replace(" ", "").Split('-');
                    string[] man = this.gridView3.GetRowCellValue(i, "男（浓度范围）").ToString().Replace(" ", "").Split('-');
                    string[] woMan = this.gridView3.GetRowCellValue(i, "女（浓度范围）").ToString().Replace(" ", "").Split('-');
                    if(i == 0)
                    {
                        parameterInfo.AgeLow1 = Convert.ToInt32(age[0]);
                        parameterInfo.AgeHigh1 = Convert.ToInt32(age[1]);
                        parameterInfo.ManConsLow1 = float.Parse(man[0]);
                        parameterInfo.ManConsHigh1 = float.Parse(man[1]);
                        parameterInfo.WomanConsLow1 = float.Parse(woMan[0]);
                        parameterInfo.WomanConsHigh1 = float.Parse(woMan[1]);
                    }
                    else if (i == 1)
                    {
                        parameterInfo.AgeLow2 = Convert.ToInt32(age[0]);
                        parameterInfo.AgeHigh2 = Convert.ToInt32(age[1]);
                        parameterInfo.ManConsLow2 = float.Parse(man[0]);
                        parameterInfo.ManConsHigh2 = float.Parse(man[1]);
                        parameterInfo.WomanConsLow2 = float.Parse(woMan[0]);
                        parameterInfo.WomanConsHigh2 = float.Parse(woMan[1]);
                    }
                    else if (i == 2)
                    {
                        parameterInfo.AgeLow3 = Convert.ToInt32(age[0]);
                        parameterInfo.AgeHigh3 = Convert.ToInt32(age[1]);
                        parameterInfo.ManConsLow3 = float.Parse(man[0]);
                        parameterInfo.ManConsHigh3 = float.Parse(man[1]);
                        parameterInfo.WomanConsLow3 = float.Parse(woMan[0]);
                        parameterInfo.WomanConsHigh3 = float.Parse(woMan[1]);
                    }
                    else if (i == 3)
                    {
                        parameterInfo.AgeLow4 = Convert.ToInt32(age[0]);
                        parameterInfo.AgeHigh4 = Convert.ToInt32(age[1]);
                        parameterInfo.ManConsLow4 = float.Parse(man[0]);
                        parameterInfo.ManConsHigh4 = float.Parse(man[1]);
                        parameterInfo.WomanConsLow4 = float.Parse(woMan[0]);
                        parameterInfo.WomanConsHigh4 = float.Parse(woMan[1]);
                    }
                }
            }
            
            return parameterInfo;
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
                    txtCheckLightDot1.SelectedIndex = 0;
                    txtCheckLightDot2.SelectedIndex = 0;
                    txtCheckLightDot3.SelectedIndex = 42;
                    txtCheckLightDot4.SelectedIndex = 42;
                    break;
                case "二点终点法":
                    txtCheckLightDot1.Enabled = true;
                    txtCheckLightDot2.Enabled = true;
                    txtCheckLightDot3.Enabled = true;
                    txtCheckLightDot4.Enabled = true;
                    txtCheckLightDot1.SelectedIndex = 0;
                    txtCheckLightDot2.SelectedIndex = 0;
                    txtCheckLightDot3.SelectedIndex = 42;
                    txtCheckLightDot4.SelectedIndex = 42;
                    break;
                case "速率A法":
                    txtCheckLightDot1.Enabled = false;
                    txtCheckLightDot2.Enabled = false;
                    txtCheckLightDot3.Enabled = true;
                    txtCheckLightDot4.Enabled = true;
                    txtCheckLightDot1.SelectedIndex = 0;
                    txtCheckLightDot2.SelectedIndex = 0;
                    txtCheckLightDot3.SelectedIndex = 42;
                    txtCheckLightDot4.SelectedIndex = 42;
                    break;
                case "速率B法":
                    txtCheckLightDot1.Enabled = true;
                    txtCheckLightDot2.Enabled = true;
                    txtCheckLightDot3.Enabled = true;
                    txtCheckLightDot4.Enabled = true;
                    txtCheckLightDot1.SelectedIndex = 0;
                    txtCheckLightDot2.SelectedIndex = 0;
                    txtCheckLightDot3.SelectedIndex = 42;
                    txtCheckLightDot4.SelectedIndex = 42;
                    break;
                default:
                    txtCheckLightDot1.Enabled = false;
                    txtCheckLightDot2.Enabled = false;
                    txtCheckLightDot3.Enabled = false;
                    txtCheckLightDot4.Enabled = false;
                    txtCheckLightDot1.SelectedIndex = 0;
                    txtCheckLightDot2.SelectedIndex = 0;
                    txtCheckLightDot3.SelectedIndex = 42;
                    txtCheckLightDot4.SelectedIndex = 42;
                    break;
            }
        }
        /// <summary>
        /// 范围参数删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView3.GetSelectedRows().Count() > 0)
            {
                int row = this.gridView3.GetSelectedRows()[0];
                dtRange.Rows.RemoveAt(row);
            }
            else
                MessageBoxDraw.ShowMsg("请选择要删除的范围参数！",MsgType.OK);
        }
        /// <summary>
        /// 范围参数保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButSave_Click(object sender, EventArgs e)
        {
            List<AssayProjectRangeParamInfo> lstRange = new List<AssayProjectRangeParamInfo>();
            AssayProjectRangeParamInfo parameter = new AssayProjectRangeParamInfo();
            string sampleType = null;
            if (this.gridView3.RowCount >= 4)
            {
                MessageBoxDraw.ShowMsg("范围参数设置不能超过4条！请选择列表中任意一行数据删除，然后再添加！", MsgType.OK);
                return;
            }
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                sampleType = this.gridView2.GetRowCellValue(this.gridView2.GetSelectedRows()[0], "类型").ToString();
            }
            // 最小和最大年龄范围只能输入数字
            if (txtSerumAgeHigh1.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeHigh1.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeLow1.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeLow1.Text.Trim(), "^([0-9]{1,})$")||
                Convert.ToInt32(txtSerumAgeHigh1.Text.Trim()) > 200 || Convert.ToInt32(txtSerumAgeLow1.Text.Trim()) >= 200 ||
                Convert.ToInt32(txtSerumAgeLow1.Text.Trim()) > Convert.ToInt32(txtSerumAgeHigh1.Text.Trim()))
            {
                MessageBox.Show("期望值中的年龄输入格式有误，请重新输入！");
                return;
            }

            if (float.Parse(this.txtSerumManConsHigh.Text.ToString()) < float.Parse(this.txtSerumManConsLow.Text.ToString()))
            {
                MessageBox.Show(string.Format("男：高浓度值不能小于低浓度的值，如（低浓度等于：{0}；高浓度必须大于等于{1}）！", this.txtSerumManConsLow.Text, this.txtSerumManConsLow.Text));
                return;
            }
            if (float.Parse(this.txtSerumWomanConsHigh.Text.ToString()) < float.Parse(this.txtSerumWomanConsLow.Text.ToString()))
            {
                MessageBox.Show(string.Format("女：高浓度值不能小于低浓度的值，如（低浓度等于：{0}；高浓度必须大于等于{1}）！", this.txtSerumWomanConsLow.Text, this.txtSerumWomanConsLow.Text));
                return;
            }
            if (this.gridView3.RowCount > 0)
            {

                int rows = this.gridView3.RowCount;
                for (int i = 0; i < rows; i++)
                {
                    string s = this.gridView3.GetRowCellValue(i, "年龄范围").ToString();
                    string[] age = s.Replace(" ","").Split('-');
                    if (Convert.ToInt32(txtSerumAgeHigh1.Text.Trim()) <= Convert.ToInt32(age[1]) || Convert.ToInt32(txtSerumAgeLow1.Text.Trim()) <= Convert.ToInt32(age[1]))
                    {
                        MessageBoxDraw.ShowMsg("列表中已包含您输入的年龄！", MsgType.OK);
                        return;
                    }
                    else
                    {
                        string man = this.gridView3.GetRowCellValue(i, "男（浓度范围）").ToString();
                        string woMan = this.gridView3.GetRowCellValue(i, "女（浓度范围）").ToString();
                        string[] mans = man.Replace(" ", "").Split('-');
                        string[] woMans = woMan.Replace(" ", "").Split('-');
                        AssayProjectRangeParamInfo rangeParam = new AssayProjectRangeParamInfo();
                        rangeParam.SampleType = sampleType;
                        rangeParam.AgeLow1 = Convert.ToInt32(age[0].Trim());
                        rangeParam.AgeHigh1 = Convert.ToInt32(age[1].Trim());
                        rangeParam.ManConsLow1 = float.Parse(mans[0].Trim());
                        rangeParam.ManConsHigh1 = float.Parse(mans[1].Trim());
                        rangeParam.WomanConsLow1 = float.Parse(woMans[0].Trim());
                        rangeParam.WomanConsHigh1 = float.Parse(woMans[1].Trim());
                        lstRange.Add(rangeParam);
                    }
                }
            }
            parameter.SampleType = sampleType;
            parameter.AgeLow1 =System.Convert.ToInt32(txtSerumAgeLow1.Text.Trim());
            parameter.AgeHigh1 = System.Convert.ToInt32(txtSerumAgeHigh1.Text.Trim());
            parameter.ManConsLow1 = float.Parse(txtSerumManConsLow.Text.Trim());
            parameter.ManConsHigh1 = float.Parse(txtSerumManConsHigh.Text.Trim());
            parameter.WomanConsLow1 = float.Parse(txtSerumWomanConsLow.Text.Trim());
            parameter.WomanConsHigh1 = float.Parse(txtSerumWomanConsHigh.Text.Trim());
            lstRange.Add(parameter);
            LoandProjectRangeParam(lstRange);
            CloseRangeParameInfo();
        }
        /// <summary>
        /// 反应方向下标改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboReactionDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboReactionDirection.SelectedIndex == 0)
            {
                this.CBSuperiorLimit.CheckState = CheckState.Checked;
                this.CBLowerLimit.CheckState = CheckState.Unchecked;
            }
            else
            {
                this.CBLowerLimit.CheckState = CheckState.Checked;
                this.CBSuperiorLimit.CheckState = CheckState.Unchecked;
            }
        }
        /// <summary>
        /// 清除上次范围参数录入的信息
        /// </summary>
        private void CloseRangeParameInfo()
        {
            this.txtSerumAgeLow1.Text = "0";
            this.txtSerumAgeHigh1.Text = "200";
            this.txtSerumManConsLow.Text = "0";
            this.txtSerumManConsHigh.Text = "0";
            this.txtSerumWomanConsLow.Text = "0";
            this.txtSerumWomanConsHigh.Text = "0";
        }
        /// <summary>
        /// 范围参数浓度输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSerumManConsLow_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
                {
                    if (e.KeyChar == '.')
                    {
                        if (((TextEdit)sender).Text.Trim().IndexOf('.') > -1)
                            e.Handled = true;
                    }
                    else
                        e.Handled = true;
                }
                else
                {
                    if (e.KeyChar <= 31)
                    {
                        e.Handled = false;
                    }
                    else if (((TextEdit)sender).Text.Trim().IndexOf('.') > -1)
                    {
                        if (((TextEdit)sender).Text.Trim().Substring(((TextEdit)sender).Text.Trim().IndexOf('.') + 1).Length >= 4)
                            e.Handled = true;
                    }
                    else if (((TextEdit)sender).Text.Trim().Length == 1)
                    {
                        if (e.KeyChar != '.' && ((TextEdit)sender).Text.Trim() == "0")
                            e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteErrorLog("txtSerumManConsLow_KeyPress(object sender, KeyPressEventArgs e) ==" + ex.Message, Module.Setting);
            }
        }
        /// <summary>
        /// 男：低浓度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerumManConsLowMaxAndMinValue(object sender, EventArgs e)
        {
            if (this.txtSerumManConsLow.Text.ToString() == "")
            {
                this.txtSerumManConsLow.Text = "0";
            }
            else
            {
                if (float.Parse(this.txtSerumManConsLow.Text.ToString()) == 0)
                {
                    this.txtSerumManConsLow.Text = "0";
                }
                else if (float.Parse(this.txtSerumManConsLow.Text.ToString()) > 100000f)
                {
                    MessageBox.Show("男：低浓度值不能超过100000");
                    this.txtSerumManConsLow.Text = "0";
                    return;
                }
            }
        }
        /// <summary>
        /// 男：高浓度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerumManConsHighMaxAndMinValue(object sender, EventArgs e)
        {
            if (this.txtSerumManConsHigh.Text.ToString() == "")
            {
                this.txtSerumManConsHigh.Text = "0";
            }
            else
            {
                if (float.Parse(this.txtSerumManConsHigh.Text.ToString()) == 0)
                {
                    this.txtSerumManConsHigh.Text = "0";
                }
                else if (float.Parse(this.txtSerumManConsHigh.Text.ToString()) > 100000f)
                {
                    MessageBox.Show("男：高浓度值不能超过100000");
                    this.txtSerumManConsHigh.Text = "0";
                    return;
                }            }
        }
        /// <summary>
        /// 女：低浓度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerumWomanConsLowMaxAndMinValue(object sender, EventArgs e)
        {
            if (this.txtSerumWomanConsLow.Text.ToString() == "")
            {
                this.txtSerumWomanConsLow.Text = "0";
            }
            else
            {
                if (float.Parse(this.txtSerumWomanConsLow.Text.ToString()) == 0)
                {
                    this.txtSerumWomanConsLow.Text = "0";
                }
                else if (float.Parse(this.txtSerumWomanConsLow.Text.ToString()) > 100000f)
                {
                    MessageBox.Show("女：低浓度值不能超过100000");
                    this.txtSerumWomanConsLow.Text = "0";
                    return;
                }
            }
        }
        /// <summary>
        /// 女：高浓度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerumWomanConsHighMaxAndMinValue(object sender, EventArgs e)
        {
            if (this.txtSerumWomanConsHigh.Text.ToString() == "")
            {
                this.txtSerumWomanConsHigh.Text = "0";
            }
            else
            {
                if (float.Parse(this.txtSerumWomanConsHigh.Text.ToString()) == 0)
                {
                    this.txtSerumWomanConsHigh.Text = "0";
                }
                else if (float.Parse(this.txtSerumWomanConsHigh.Text.ToString()) > 100000f)
                {
                    MessageBox.Show("女：高浓度值不能超过100000");
                    this.txtSerumWomanConsHigh.Text = "0";
                    return;
                }
            }
        }

    }
}
