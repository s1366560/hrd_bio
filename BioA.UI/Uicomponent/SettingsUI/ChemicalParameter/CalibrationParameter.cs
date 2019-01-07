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
using System.Text.RegularExpressions;
using System.Threading;
using BioA.Service;

namespace BioA.UI
{
    public partial class CalibrationParameter : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 数据增、删、查
        /// </summary>
        /// <param name="strAccessSqlMethod">访问数据库方法名</param>
        /// <param name="sender">参数对象</param>
        public delegate void AssayProInfoDelegate(Dictionary<string, object[]> sender);
        public event AssayProInfoDelegate AssayProInfoForCalibParamEvent;        
        public CalibrationParameter()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            cboCalibMethod.Text = "请选择";
            label4.Visible = false;
            textEdit1.Visible = false;
            cboCalibMethod.Properties.Items.AddRange(RunConfigureUtility.CalibrationMethods);

            cboCalibTimes.Properties.Items.AddRange(RunConfigureUtility.CalibrationTimes);
            
        }

        /// <summary>
        /// 保存客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> calibParamDic = new Dictionary<string, object[]>();

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
        /// 显示所有项目信息
        /// </summary>
        public List<AssayProjectInfo> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.Invoke(new EventHandler(delegate
                {
                    lstvAssayProject.RefreshDataSource();
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
                    this.lstvAssayProject.DataSource = dt;
                    if (this.gridView1.RowCount > 0)
                    {
                        this.gridView1.SelectRow(0);//FocusedRowHandle = 0;
                        //lstvAssayProject_Click(null, null);
                    }
                }));
            }
        }
        private List<AssayProjectCalibrationParamInfo> lstCalibParamInfo = new List<AssayProjectCalibrationParamInfo>();
        /// <summary>
        /// 存储所有校准项目参数信息
        /// </summary>
        public List<AssayProjectCalibrationParamInfo> LstCalibParamInfo
        {
            get { return lstCalibParamInfo; }
            set { lstCalibParamInfo = value; lstvAssayProject_Click(null, null); }
        }

        AssayProjectCalibrationParamInfo calibParamInfo = new AssayProjectCalibrationParamInfo();
        /// <summary>
        /// 显示生化项目对应的校准参数信息
        /// </summary>
        public AssayProjectCalibrationParamInfo CalibParamInfo
        {
            get { return calibParamInfo; }
            set
            {
                calibParamInfo = value;
                BeginInvoke(new Action(() =>
                {
                    if (calibParamInfo.CalibrationMethod == "")
                    {
                        cboCalibMethod.Text = "请选择";
                        this.nullinfo();
                        cboCalibTimes.SelectedIndex = 0;
                        this.BeginInvoke(new Action(AddCalibratorProjectinfo));
                    }
                    else
                    {
                        ClearCalibName();
                        this.BeginInvoke(new Action(AddCalibratorProjectinfo));
                        cboCalibMethod.Text = calibParamInfo.CalibrationMethod;
                        this.nullinfo();
                        txtCalibPoint.Text = calibParamInfo.Point.ToString();
                        txtAbsLimit.Text = calibParamInfo.AbsLimit.ToString();
                        txtSpan.Text = calibParamInfo.Span.ToString();
                        txtSensitivityLow.Text = calibParamInfo.SensitivityLow.ToString();
                        txtSensitivityHigh.Text = calibParamInfo.SensitivityHigh.ToString();
                        txtDuplicatePercent.Text = calibParamInfo.DuplicatePercent.ToString();
                        txtDuplicateAbs.Text = calibParamInfo.DuplicateAbs.ToString();
                        cboCalibTimes.Text = calibParamInfo.CalibrationTimes.ToString();
                        txtBlankAbsLow.Text = calibParamInfo.BlankAbsLow.ToString();
                        txtBlankAbsHigh.Text = calibParamInfo.BlankAbsHigh.ToString();
                        chkAutoCalibration.Checked = calibParamInfo.AutoCalibration;
                        chkAutoCalibMask.Checked = calibParamInfo.AutoCalibMask;
                        cboCalib1.Text = calibParamInfo.CalibName0;
                        cboCalib2.Text = calibParamInfo.CalibName1;
                        cboCalib3.Text = calibParamInfo.CalibName2;
                        cboCalib4.Text = calibParamInfo.CalibName3;
                        cboCalib5.Text = calibParamInfo.CalibName4;
                        cboCalib6.Text = calibParamInfo.CalibName5;
                        cboCalib7.Text = calibParamInfo.CalibName6;
                        txtPos1.Text = calibParamInfo.CalibPos0;
                        txtPos2.Text = calibParamInfo.CalibPos1;
                        txtPos3.Text = calibParamInfo.CalibPos2;
                        txtPos4.Text = calibParamInfo.CalibPos3;
                        txtPos5.Text = calibParamInfo.CalibPos4;
                        txtPos6.Text = calibParamInfo.CalibPos5;
                        txtPos7.Text = calibParamInfo.CalibPos6;
                        txtCalibConc1.Text = calibParamInfo.CalibConcentration0.ToString();
                        txtCalibConc2.Text = calibParamInfo.CalibConcentration1.ToString();
                        txtCalibConc3.Text = calibParamInfo.CalibConcentration2.ToString();
                        txtCalibConc4.Text = calibParamInfo.CalibConcentration3.ToString();
                        txtCalibConc5.Text = calibParamInfo.CalibConcentration4.ToString();
                        txtCalibConc6.Text = calibParamInfo.CalibConcentration5.ToString();
                        txtCalibConc7.Text = calibParamInfo.CalibConcentration6.ToString();
                        textEdit1.Text = calibParamInfo.Factor.ToString();

                        if (calibParamInfo.CalibLotCheck)
                            cboCalibLotCheck.Text = "是";
                        else
                            cboCalibLotCheck.Text = "否";
                        if (calibParamInfo.CalibValidDateCheck)
                            cboCalibValidDateCheck.Text = "是";
                        else
                            cboCalibValidDateCheck.Text = "否";

                        if (calibParamInfo.ReagentLotCheck)
                            cboReagentLotCheck.Text = "是";
                        else
                            cboReagentLotCheck.Text = "否";
                        if (calibParamInfo.ReagentValidDateCheck)
                            cboReagentValidDateCheck.Text = "是";
                        else
                            cboReagentValidDateCheck.Text = "否";

                        if (calibParamInfo.QCFailed)
                            cboQCFailed.Text = "是";
                        else
                            cboQCFailed.Text = "否";
                    }
                }));

            }

        }

        private void nullinfo()
        {
            txtCalibConc1.Text = "";
            txtCalibConc2.Text = "";
            txtCalibConc3.Text = "";
            txtCalibConc4.Text = "";
            txtCalibConc5.Text = "";
            txtCalibConc6.Text = "";
            txtCalibConc7.Text = "";
            cboCalib1.Text = "";
            cboCalib2.Text = "";
            cboCalib3.Text = "";
            cboCalib4.Text = "";
            cboCalib5.Text = "";
            cboCalib6.Text = "";
            cboCalib7.Text = "";
            txtPos1.Text = "";
            txtPos2.Text = "";
            txtPos3.Text = "";
            txtPos4.Text = "";
            txtPos5.Text = "";
            txtPos6.Text = "";
            txtPos7.Text = "";
            textEdit1.Text = "";
            txtCalibPoint.Text = "0";
            txtAbsLimit.Text = "0";
            txtSpan.Text = "0";
            txtSensitivityLow.Text = "0";
            txtSensitivityHigh.Text = "0";
            txtDuplicatePercent.Text = "0";
            txtDuplicateAbs.Text = "0";
            cboCalibTimes.Text = "0";
            txtBlankAbsLow.Text = "0";
            txtBlankAbsHigh.Text = "0";
        }
        
        private void AddCalibrationCurveInfo ()
        {
            List<CalibrationCurveInfo> lisCalibrationCurveInfo = new List<CalibrationCurveInfo>();
            if (cboCalibMethod.Text == "2点线性法" )
            {
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                CalibrationCurveInfo calibrationCurveInfo1 = new CalibrationCurveInfo();
                calibrationCurveInfo1.CalibConcentration = (float)Convert.ToDouble(txtCalibConc1.Text);
                calibrationCurveInfo1.CalibName = cboCalib1.Text;
                calibrationCurveInfo1.Pos = txtPos1.Text;
                calibrationCurveInfo1.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo1.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo1.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo1.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo1);
                CalibrationCurveInfo calibrationCurveInfo2 = new CalibrationCurveInfo();
                calibrationCurveInfo2.CalibConcentration = (float)Convert.ToDouble(txtCalibConc2.Text);
                calibrationCurveInfo2.CalibName = cboCalib2.Text;
                calibrationCurveInfo2.Pos = txtPos2.Text;
                calibrationCurveInfo2.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo2.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo2.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo2.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo2);
            }
            if (cboCalibMethod.Text == "K系数法")
            {
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                CalibrationCurveInfo calibrationCurveInfo1 = new CalibrationCurveInfo();
                calibrationCurveInfo1.CalibConcentration = (float)Convert.ToDouble(txtCalibConc1.Text);
                calibrationCurveInfo1.CalibName = cboCalib1.Text;
                calibrationCurveInfo1.Pos = txtPos1.Text;
                calibrationCurveInfo1.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo1.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo1.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo1.Factor =(float)Convert.ToDouble(textEdit1.Text);
                calibrationCurveInfo1.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo1);             
            }
            //"K系数法,2点线性法,多点线性法,折线法,对数法,指数法,多项式法,样条法,一次多项式法,二次多项式法,三次多项式法"
            if (cboCalibMethod.Text == "多点线性法" || cboCalibMethod.Text == "折线法" ||
                cboCalibMethod.Text == "对数法" || cboCalibMethod.Text == "指数法" ||
                cboCalibMethod.Text == "多项式法" || cboCalibMethod.Text == "样条法" ||
                cboCalibMethod.Text == "一次多项式法" || cboCalibMethod.Text == "二次多项式法" ||
                cboCalibMethod.Text == "三次多项式法" )
            {
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                CalibrationCurveInfo calibrationCurveInfo1 = new CalibrationCurveInfo();
                calibrationCurveInfo1.CalibConcentration = (float)Convert.ToDouble(txtCalibConc1.Text);
                calibrationCurveInfo1.CalibName = cboCalib1.Text;
                calibrationCurveInfo1.Pos = txtPos1.Text;
                calibrationCurveInfo1.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo1.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo1.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo1.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo1);
                CalibrationCurveInfo calibrationCurveInfo2 = new CalibrationCurveInfo();
                calibrationCurveInfo2.CalibConcentration = (float)Convert.ToDouble(txtCalibConc2.Text);
                calibrationCurveInfo2.CalibName = cboCalib2.Text;
                calibrationCurveInfo2.Pos = txtPos2.Text;
                calibrationCurveInfo2.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo2.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo2.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo2.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo2);
                CalibrationCurveInfo calibrationCurveInfo3 = new CalibrationCurveInfo();
                calibrationCurveInfo3.CalibConcentration = (float)Convert.ToDouble(txtCalibConc3.Text);
                calibrationCurveInfo3.CalibName = cboCalib3.Text;
                calibrationCurveInfo3.Pos = txtPos3.Text;
                calibrationCurveInfo3.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo3.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo3.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo3.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo3);
                CalibrationCurveInfo calibrationCurveInfo4 = new CalibrationCurveInfo();
                calibrationCurveInfo4.CalibConcentration = (float)Convert.ToDouble(txtCalibConc4.Text);
                calibrationCurveInfo4.CalibName = cboCalib4.Text;
                calibrationCurveInfo4.Pos = txtPos4.Text;
                calibrationCurveInfo4.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo4.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo4.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo4.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo4);
                CalibrationCurveInfo calibrationCurveInfo5 = new CalibrationCurveInfo();
                calibrationCurveInfo5.CalibConcentration = (float)Convert.ToDouble(txtCalibConc5.Text);
                calibrationCurveInfo5.CalibName = cboCalib5.Text;
                calibrationCurveInfo5.Pos = txtPos5.Text;
                calibrationCurveInfo5.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo5.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo5.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo5.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo5);
                CalibrationCurveInfo calibrationCurveInfo6 = new CalibrationCurveInfo();
                calibrationCurveInfo6.CalibConcentration = (float)Convert.ToDouble(txtCalibConc6.Text);
                calibrationCurveInfo6.CalibName = cboCalib6.Text;
                calibrationCurveInfo6.Pos = txtPos6.Text;
                calibrationCurveInfo6.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo6.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo6.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo6.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo6);
                CalibrationCurveInfo calibrationCurveInfo7 = new CalibrationCurveInfo();
                calibrationCurveInfo7.CalibConcentration = (float)Convert.ToDouble(txtCalibConc7.Text);
                calibrationCurveInfo7.CalibName = cboCalib7.Text;
                calibrationCurveInfo7.Pos = txtPos7.Text;
                calibrationCurveInfo7.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                calibrationCurveInfo7.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                calibrationCurveInfo7.CalibType = cboCalibMethod.Text;
                calibrationCurveInfo7.CalibTime = Convert.ToInt32(cboCalibTimes.Text);
                lisCalibrationCurveInfo.Add(calibrationCurveInfo7);

            }
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity),
            //    new CommunicationEntity("AddCalibrationCurveInfo", XmlUtility.Serializer(typeof(List<CalibrationCurveInfo>), lisCalibrationCurveInfo))));
            calibParamDic.Add("AddCalibrationCurveInfo", new object[] { XmlUtility.Serializer(typeof(List<CalibrationCurveInfo>), lisCalibrationCurveInfo) });
            ClientSendMsgToServices(calibParamDic);
         
        }
        /// <summary>
        /// 存储保存校准项目参数信息对象
        /// </summary>
        private AssayProjectCalibrationParamInfo parameter;

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboCalibMethod.Text == "请选择" || cboCalibMethod.Text == null)
            {
                MessageBoxDraw.ShowMsg("请选择校准方法！", MsgType.Warning);
                return;
            }
            if (txtCalibConc1.Text != "" && txtCalibConc2.Text != "" && txtCalibConc3.Text != "" && txtCalibConc4.Text != "" && txtCalibConc5.Text != ""
                && txtCalibConc6.Text != "" && txtCalibConc7.Text !="")
            {
                if (Convert.ToDouble(txtCalibConc1.Text) > Convert.ToDouble(txtCalibConc2.Text) && txtCalibConc1.Visible == true && txtCalibConc2.Visible == true ||
                    Convert.ToDouble(txtCalibConc2.Text) > Convert.ToDouble(txtCalibConc3.Text) && txtCalibConc2.Visible == true && txtCalibConc3.Visible == true ||
                    Convert.ToDouble(txtCalibConc3.Text) > Convert.ToDouble(txtCalibConc4.Text) && txtCalibConc3.Visible == true && txtCalibConc4.Visible == true ||
                    Convert.ToDouble(txtCalibConc4.Text) > Convert.ToDouble(txtCalibConc5.Text) && txtCalibConc4.Visible == true && txtCalibConc5.Visible == true ||
                    Convert.ToDouble(txtCalibConc5.Text) > Convert.ToDouble(txtCalibConc6.Text) && txtCalibConc5.Visible == true && txtCalibConc6.Visible == true ||
                    Convert.ToDouble(txtCalibConc6.Text) > Convert.ToDouble(txtCalibConc7.Text) && txtCalibConc6.Visible == true && txtCalibConc7.Visible == true
                    )
                {
                    MessageBoxDraw.ShowMsg("请按浓度大小排列！", MsgType.Warning);
                    return;
                }
            }
            else if (txtCalibConc1.Text != "" && txtCalibConc2.Text != "" )
            {
                if (Convert.ToDouble(txtCalibConc1.Text) > Convert.ToDouble(txtCalibConc2.Text) && txtCalibConc1.Visible == true && txtCalibConc2.Visible == true 
                   
                    )
                {
                    MessageBoxDraw.ShowMsg("请按浓度大小排列！", MsgType.Warning);
                    return;
                }
            }
            if (cboCalib1.Visible == true && cboCalib1.Text == "")
            {
                MessageBoxDraw.ShowMsg("请选择浓度0的校准品！",MsgType.Warning);
                return;
            }
            else
            {

            }
            if (textEdit1.Visible == true && textEdit1.Text == "")
            {
                MessageBoxDraw.ShowMsg("请输入因子！", MsgType.Warning);
                return;
            }
            else
            {

            }
            if (cboCalib2.Visible == true && cboCalib2.Text == "")
            {
                MessageBoxDraw.ShowMsg("请选择浓度1的校准品！", MsgType.Warning);
                return;
            }
            else
            {

            }
            if (cboCalib3.Visible == true && cboCalib3.Text == "")
            {
                MessageBoxDraw.ShowMsg("请选择浓度2的校准品！", MsgType.Warning);
                return;
            }
            else
            {

            }
            if (cboCalib4.Visible == true && cboCalib4.Text == "")
            {
                MessageBoxDraw.ShowMsg("请选择浓度3的校准品！", MsgType.Warning);
                return;
            }
            else
            {

            }
            if (cboCalib5.Visible == true && cboCalib5.Text == "")
            {
                MessageBoxDraw.ShowMsg("请选择浓度4的校准品！", MsgType.Warning);
                return;
            }
            else
            {

            }
            if (cboCalib6.Visible == true && cboCalib6.Text == "")
            {
                MessageBoxDraw.ShowMsg("请选择浓度5的校准品！", MsgType.Warning);
                return;
            }
            else
            {

            }
            if (cboCalib7.Visible == true && cboCalib7.Text == "")
            {
                MessageBoxDraw.ShowMsg("请选择浓度6的校准品！", MsgType.Warning);
                return;
            }
            else
            {

            }
            if (txtPos1.Visible == true && txtPos1.Text.Trim() == ""||txtPos2.Visible == true && txtPos2.Text.Trim() == ""||
                txtPos3.Visible == true && txtPos3.Text.Trim() == ""||txtPos4.Visible == true && txtPos4.Text.Trim() == ""||
                txtPos5.Visible == true && txtPos5.Text.Trim() == ""||txtPos6.Visible == true && txtPos6.Text.Trim() == ""||
                txtPos7.Visible == true && txtPos7.Text.Trim() == "")
            {
                MessageBoxDraw.ShowMsg("校准品位置不能为空值！", MsgType.Warning);
                return;
            }
            if (txtCalibConc1.Visible == true && txtCalibConc1.Text.Trim() == "" || txtCalibConc2.Visible == true && txtCalibConc2.Text.Trim() == "" ||
               txtCalibConc3.Visible == true && txtCalibConc3.Text.Trim() == "" || txtCalibConc4.Visible == true && txtCalibConc4.Text.Trim() == "" ||
               txtCalibConc5.Visible == true && txtCalibConc5.Text.Trim() == "" || txtCalibConc6.Visible == true && txtCalibConc6.Text.Trim() == "" ||
               txtCalibConc7.Visible == true && txtCalibConc7.Text.Trim() == "" )
            {
                MessageBoxDraw.ShowMsg("校准品浓度不能为空值！", MsgType.Warning);
                return;
            }

          
           
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                parameter = new AssayProjectCalibrationParamInfo();
                parameter.ProjectName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString();
                parameter.SampleType = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "类型").ToString();

                parameter.CalibrationMethod = cboCalibMethod.Text;

                if (Regex.IsMatch(txtCalibPoint.Text.Trim(), "^([0-9]{1,})$"))
                {
                    parameter.Point = System.Convert.ToInt32(txtCalibPoint.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("校准点输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtAbsLimit.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    parameter.AbsLimit = (float)System.Convert.ToDouble(txtAbsLimit.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("吸光度限制输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtSpan.Text.Trim(), "^([0-9]{1,})$"))
                {
                    parameter.Span = System.Convert.ToInt32(txtSpan.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("量程点输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtSensitivityLow.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    parameter.SensitivityLow = (float)System.Convert.ToDouble(txtSensitivityLow.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("灵敏度输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtSensitivityHigh.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    parameter.SensitivityHigh = (float)System.Convert.ToDouble(txtSensitivityHigh.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("灵敏度输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtDuplicatePercent.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    parameter.DuplicatePercent = (float)System.Convert.ToDouble(txtDuplicatePercent.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("重复性限制输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtDuplicateAbs.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    parameter.DuplicateAbs = (float)System.Convert.ToDouble(txtDuplicateAbs.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("重复性限制输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(cboCalibTimes.Text.Trim(), "^([0-9]{1,})$"))
                {
                    parameter.CalibrationTimes = System.Convert.ToInt32(cboCalibTimes.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("校准次数输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtBlankAbsLow.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    parameter.BlankAbsLow = (float)System.Convert.ToDouble(txtBlankAbsLow.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("空白吸光度输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }

                if (Regex.IsMatch(txtBlankAbsHigh.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    parameter.BlankAbsHigh = (float)System.Convert.ToDouble(txtBlankAbsHigh.Text);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("空白吸光度输入格式有误，请重新输入！", MsgType.Warning);
                    return;
                }
                
          
                parameter.AutoCalibration = chkAutoCalibration.Checked;
                parameter.AutoCalibMask = chkAutoCalibMask.Checked;
                parameter.CalibLotCheck = cboCalibLotCheck.Text == "是" ? true : false;
                parameter.CalibValidDateCheck = cboCalibValidDateCheck.Text == "是" ? true : false;
                parameter.ReagentLotCheck = cboReagentLotCheck.Text == "是" ? true : false;
                parameter.ReagentValidDateCheck = cboReagentValidDateCheck.Text == "是" ? true : false;
                parameter.QCFailed = cboQCFailed.Text == "是" ? true : false;
                if (textEdit1.Text != "" && textEdit1.Visible == true)
                {
                    parameter.Factor = (float)Convert.ToDouble(textEdit1.Text);
                }
                else
                {
                    parameter.Factor = 0;
                }
                if (txtCalibConc1.Text != "" && txtCalibConc1.Visible == true)
                {
                    parameter.CalibConcentration0 = (float)Convert.ToDouble(txtCalibConc1.Text);
                }
                else
                {
                    parameter.CalibConcentration0 = 0;
                }
                if (txtCalibConc2.Text != "" && txtCalibConc2.Visible == true)
                {
                    parameter.CalibConcentration1 = (float)Convert.ToDouble(txtCalibConc2.Text);
                }
                else
                {
                    parameter.CalibConcentration1 = 0;
                }
                if (txtCalibConc3.Text != "" && txtCalibConc3.Visible == true)
                {
                    parameter.CalibConcentration2 = (float)Convert.ToDouble(txtCalibConc3.Text);
                }
                else
                {
                    parameter.CalibConcentration2 = 0;
                }
                if (txtCalibConc4.Text != "" && txtCalibConc4.Visible == true)
                {
                    parameter.CalibConcentration3 = (float)Convert.ToDouble(txtCalibConc4.Text);
                }
                else
                {
                    parameter.CalibConcentration3 = 0;
                }
                if (txtCalibConc5.Text != "" && txtCalibConc5.Visible == true)
                {
                    parameter.CalibConcentration4 = (float)Convert.ToDouble(txtCalibConc5.Text);
                }
                else
                {
                    parameter.CalibConcentration4 = 0;
                }
                if (txtCalibConc6.Text != "" && txtCalibConc6.Visible == true)
                {
                    parameter.CalibConcentration5 = (float)Convert.ToDouble(txtCalibConc6.Text);
                }
                else
                {
                    parameter.CalibConcentration5 = 0;
                }
                if (txtCalibConc7.Text != "" && txtCalibConc7.Visible==true)
                {
                    parameter.CalibConcentration6 = (float)Convert.ToDouble(txtCalibConc7.Text);
                }
                else
                {
                    parameter.CalibConcentration6 = 0;
                }
                if (cboCalib1.Visible==true)
                {
                    parameter.CalibName0 = cboCalib1.Text;
                }
                if (cboCalib2.Visible == true)
                {
                    parameter.CalibName1 = cboCalib2.Text;
                }
                if (cboCalib3.Visible == true)
                {
                    parameter.CalibName2 = cboCalib3.Text;
                }
                if (cboCalib4.Visible == true)
                {
                    parameter.CalibName3 = cboCalib4.Text;
                }
                if (cboCalib5.Visible == true)
                {
                    parameter.CalibName4 = cboCalib5.Text;
                }
                if (cboCalib6.Visible == true)
                {
                    parameter.CalibName5 = cboCalib6.Text;
                }
                if (cboCalib7.Visible == true)
                {
                    parameter.CalibName6 = cboCalib7.Text;
                }
                if (txtPos1.Visible == true)
                {
                    parameter.CalibPos0 = txtPos1.Text;
                }
                if (txtPos2.Visible == true)
                {
                    parameter.CalibPos1 = txtPos2.Text;
                }
                if (txtPos3.Visible == true)
                {
                    parameter.CalibPos2 = txtPos3.Text;
                }
                if (txtPos4.Visible == true)
                {
                    parameter.CalibPos3 = txtPos4.Text;
                }
                if (txtPos5.Visible == true)
                {
                    parameter.CalibPos4 = txtPos5.Text;
                }
                if (txtPos6.Visible == true)
                {
                    parameter.CalibPos5 = txtPos6.Text;
                }
                if (txtPos7.Visible == true)
                {
                    parameter.CalibPos6 = txtPos7.Text;
                }
                calibParamDic.Clear();
                calibParamDic.Add("UpdateCalibParamByProNameAndType", new object[] { XmlUtility.Serializer(typeof(AssayProjectCalibrationParamInfo), parameter) });
                //保存或更新校准方法对应的校准品信息
                BeginInvoke(new Action(AddCalibrationCurveInfo));
        
            }
        }

        private void CalibrationParameter_Load(object sender, EventArgs e)
        {
            //启动异步线程调用方法
            BeginInvoke(new Action(InitialControl));
        }
        public void InitialControl()
        {
            cboCalibLotCheck.Properties.Items.Add("是");
            cboCalibLotCheck.Properties.Items.Add("否");
            cboCalibLotCheck.SelectedIndex = 0;

            cboCalibValidDateCheck.Properties.Items.Add("是");
            cboCalibValidDateCheck.Properties.Items.Add("否");
            cboCalibValidDateCheck.SelectedIndex = 0;

            cboQCFailed.Properties.Items.Add("是");
            cboQCFailed.Properties.Items.Add("否");
            cboQCFailed.SelectedIndex = 0;

            cboReagentLotCheck.Properties.Items.Add("是");
            cboReagentLotCheck.Properties.Items.Add("否");
            cboReagentLotCheck.SelectedIndex = 0;

            cboReagentValidDateCheck.Properties.Items.Add("是");
            cboReagentValidDateCheck.Properties.Items.Add("否");
            cboReagentValidDateCheck.SelectedIndex = 0;

            //CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsChemicalParameter, new Dictionary<string, List<object>>() { { "QueryCalibParamInfoAll", null } });
            calibParamDic.Clear();
            //获取所有生化项目
            List<AssayProjectInfo> lstProjectInfos = new SettingsChemicalParameter().QueryAssayProAllInfo("QueryAssayProAllInfo", null);
            this.LstAssayProInfos = lstProjectInfos;
            
            calibParamDic.Add("QueryCalibParamInfoAll", null);
            ClientSendMsgToServices(calibParamDic);

        }

        private void ClientSendMsgToServices(Dictionary<string, object[]> sender)
        {
            var calibParamThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsChemicalParameter, sender);
            });
            calibParamThread.IsBackground = true;
            calibParamThread.Start();
        }
        /// <summary>
        /// 校准项目参数保存成功或失败处理信息
        /// </summary>
        public void ProcessSuccessOrFailureInfo(int sender)
        {
            if (sender > 0)
            {
                this.lstCalibParamInfo.RemoveAll(x => x.ProjectName == parameter.ProjectName && x.SampleType == parameter.SampleType);
                this.lstCalibParamInfo.Add(this.parameter);
                this.LstCalibParamInfo = lstCalibParamInfo;
                this.Invoke(new EventHandler(delegate { MessageBox.Show("校准项目参数保存成功！"); }));
            }
            else if(sender == -1)
            {
                MessageBox.Show("保存失败，此项目已下校准任务");
            }
            else
            {
                MessageBox.Show("校准项目参数保存失败！");
            }
        }


        /// <summary>
        /// 生化项目信息列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvAssayProject_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                AssayProjectInfo assayProInfo = new AssayProjectInfo();
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                assayProInfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                assayProInfo.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
                AssayProjectCalibrationParamInfo assayProParamInfo = lstCalibParamInfo.Find(x => x.ProjectName == assayProInfo.ProjectName && x.SampleType == assayProInfo.SampleType);
                if(assayProParamInfo != null) 
                    this.CalibParamInfo = assayProParamInfo;
                //foreach (AssayProjectCalibrationParamInfo assayProParamInfo in lstCalibParamInfo)
                //{
                //    if (assayProParamInfo.ProjectName == assayProInfo.ProjectName && assayProParamInfo.SampleType == assayProInfo.SampleType)
                //    {
                //        this.CalibParamInfo = assayProParamInfo;
                //    }
                //}
            }
        }
        /// <summary>
        /// 清除所有集合的校准品名称
        /// </summary>
        private void ClearCalibName()
        {
            cboCalib1.Properties.Items.Clear();
            cboCalib2.Properties.Items.Clear();
            cboCalib3.Properties.Items.Clear();
            cboCalib4.Properties.Items.Clear();
            cboCalib5.Properties.Items.Clear();
            cboCalib6.Properties.Items.Clear();
            cboCalib7.Properties.Items.Clear();
            calibratorPro.Clear();
        }

        List<CalibratorProjectinfo> calibratorPro = new List<CalibratorProjectinfo>();
        public void AddCalibrator(List<CalibratorProjectinfo> calibratorProjectinfo)
        {
             this.Invoke(new EventHandler(delegate
                {
                    ClearCalibName();
                    if (calibratorProjectinfo.Count>0)
                    {

                        foreach(CalibratorProjectinfo cal in calibratorProjectinfo)
                        {
                            calibratorPro.Add(cal);
                            cboCalib1.Properties.Items.Add(cal.CalibName);
                            cboCalib2.Properties.Items.Add(cal.CalibName);
                            cboCalib3.Properties.Items.Add(cal.CalibName);
                            cboCalib4.Properties.Items.Add(cal.CalibName);
                            cboCalib5.Properties.Items.Add(cal.CalibName);
                            cboCalib6.Properties.Items.Add(cal.CalibName);
                            cboCalib7.Properties.Items.Add(cal.CalibName);
                        }
                    }
              }));
        }
        private void AddCalibratorProjectinfo()
        {
            int selectedHandle;
            if (this.gridView1.SelectedRowsCount > 0)
            {
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                string ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
                string SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();

                calibParamDic.Clear();
                calibParamDic.Add("QueryCalibratorProinfo", new object[] { ProjectName, SampleType });
                ClientSendMsgToServices(calibParamDic);
            }
            
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            AssayProjectInfo assayProInfo = new AssayProjectInfo();
            //CommunicationEntity communicationEntity = new CommunicationEntity();
            int selectedHandle;
            selectedHandle = this.gridView1.GetSelectedRows()[0];
            assayProInfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
            assayProInfo.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();
            foreach (AssayProjectCalibrationParamInfo assayProParamInfo in lstCalibParamInfo)
            {
                if (assayProParamInfo.ProjectName == assayProInfo.ProjectName && assayProParamInfo.SampleType == assayProInfo.SampleType)
                {
                    this.CalibParamInfo = assayProParamInfo;
                }
            }
            //    communicationEntity.StrmethodName = "QueryCalibParamByProNameAndType";
            //    communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo);

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
        }

        private void cboCalibMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //"K系数法,2点线性法,多点线性法,折线法,对数法,指数法,多项式法,样条法,一次多项式法,二次多项式法,三次多项式法"
          //  nullinfo();
            if (cboCalibMethod.SelectedItem.ToString() == "2点线性法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = false;
                lblConc4.Visible = false;
                lblConc5.Visible = false;
                lblConc6.Visible = false;
                lblConc7.Visible = false;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = false;
                cboCalib4.Visible = false;
                cboCalib5.Visible = false;
                cboCalib6.Visible = false;
                cboCalib7.Visible = false;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = false;
                txtPos4.Visible = false;
                txtPos5.Visible = false;
                txtPos6.Visible = false;
                txtPos7.Visible = false;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = false;
                txtCalibConc4.Visible = false;
                txtCalibConc5.Visible = false;
                txtCalibConc6.Visible = false;
                txtCalibConc7.Visible = false;
                label4.Visible = false;
                textEdit1.Visible = false;
            }
            else if (cboCalibMethod.Text== "请选择")
            {
                lblConc1.Visible = false;
                lblConc2.Visible = false;
                lblConc3.Visible = false;
                lblConc4.Visible = false;
                lblConc5.Visible = false;
                lblConc6.Visible = false;
                lblConc7.Visible = false;
                cboCalib1.Visible = false;
                cboCalib2.Visible = false;
                cboCalib3.Visible = false;
                cboCalib4.Visible = false;
                cboCalib5.Visible = false;
                cboCalib6.Visible = false;
                cboCalib7.Visible = false;
                txtPos1.Visible = false;
                txtPos2.Visible = false;
                txtPos3.Visible = false;
                txtPos4.Visible = false;
                txtPos5.Visible = false;
                txtPos6.Visible = false;
                txtPos7.Visible = false;
                txtCalibConc1.Visible = false;
                txtCalibConc2.Visible = false;
                txtCalibConc3.Visible = false;
                txtCalibConc4.Visible = false;
                txtCalibConc5.Visible = false;
                txtCalibConc6.Visible = false;
                txtCalibConc7.Visible = false;
                label4.Visible = false;
                textEdit1.Visible = false;
            }
            else if (cboCalibMethod.SelectedItem.ToString() == "K系数法")
            {
                lblConc1.Visible = true;
                label4.Visible = true;
                textEdit1.Visible = true;
                lblConc2.Visible = false;
                lblConc3.Visible = false;
                lblConc4.Visible = false;
                lblConc5.Visible = false;
                lblConc6.Visible = false;
                lblConc7.Visible = false;
                cboCalib1.Visible = true;
                cboCalib2.Visible = false;
                cboCalib3.Visible = false;
                cboCalib4.Visible = false;
                cboCalib5.Visible = false;
                cboCalib6.Visible = false;
                cboCalib7.Visible = false;
                txtPos1.Visible = true;
                txtPos2.Visible = false;
                txtPos3.Visible = false;
                txtPos4.Visible = false;
                txtPos5.Visible = false;
                txtPos6.Visible = false;
                txtPos7.Visible = false;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = false;
                txtCalibConc3.Visible = false;
                txtCalibConc4.Visible = false;
                txtCalibConc5.Visible = false;
                txtCalibConc6.Visible = false;
                txtCalibConc7.Visible = false;
            }
            else if (cboCalibMethod.SelectedItem.ToString() == "多项式法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            }
            else if (cboCalibMethod.SelectedItem.ToString() == "一次多项式法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            }
            else if (cboCalibMethod.SelectedItem.ToString() == "二次多项式法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            }
            else if (cboCalibMethod.SelectedItem.ToString() == "三次多项式法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            } 
            else if (cboCalibMethod.SelectedItem.ToString() == "样条法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            } 
            else if (cboCalibMethod.SelectedItem.ToString() == "指数法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            } 
            else if (cboCalibMethod.SelectedItem.ToString() == "对数法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            } 
            else if (cboCalibMethod.SelectedItem.ToString() == "多点线性法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            }
            else if (cboCalibMethod.SelectedItem.ToString() == "折线法")
            {
                lblConc1.Visible = true;
                lblConc2.Visible = true;
                lblConc3.Visible = true;
                lblConc4.Visible = true;
                lblConc5.Visible = true;
                lblConc6.Visible = true;
                lblConc7.Visible = true;
                cboCalib1.Visible = true;
                cboCalib2.Visible = true;
                cboCalib3.Visible = true;
                cboCalib4.Visible = true;
                cboCalib5.Visible = true;
                cboCalib6.Visible = true;
                cboCalib7.Visible = true;
                txtPos1.Visible = true;
                txtPos2.Visible = true;
                txtPos3.Visible = true;
                txtPos4.Visible = true;
                txtPos5.Visible = true;
                txtPos6.Visible = true;
                txtPos7.Visible = true;
                txtCalibConc1.Visible = true;
                txtCalibConc2.Visible = true;
                txtCalibConc3.Visible = true;
                txtCalibConc4.Visible = true;
                txtCalibConc5.Visible = true;
                txtCalibConc6.Visible = true;
                txtCalibConc7.Visible = true;
                label4.Visible = false;
                textEdit1.Visible = false;
            }
        }

        private void cboCalib1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCalib1.Text == cboCalib2.Text && cboCalib2.Visible == true && cboCalib1.Visible == true && cboCalib1.Text.Trim() != "" ||
                cboCalib1.Text == cboCalib3.Text && cboCalib3.Visible == true && cboCalib1.Visible == true && cboCalib1.Text.Trim() != ""
                || cboCalib1.Text == cboCalib4.Text && cboCalib4.Visible == true && cboCalib1.Visible == true && cboCalib1.Text.Trim() != "" ||
                cboCalib1.Text == cboCalib5.Text && cboCalib5.Visible == true && cboCalib1.Visible == true && cboCalib1.Text.Trim() != "" ||
                cboCalib1.Text == cboCalib6.Text && cboCalib6.Visible == true && cboCalib1.Visible == true && cboCalib1.Text.Trim() != "" ||
                cboCalib1.Text == cboCalib7.Text && cboCalib7.Visible == true && cboCalib1.Visible == true && cboCalib1.Text.Trim() != "")
            {

                cboCalib1.Text = "";
                MessageBoxDraw.ShowMsg("您输入的校准品名称重复，请重新输入！", MsgType.Warning);
                return;
            }
            //CommunicationEntity communicationEntity = new CommunicationEntity();
   
            //    communicationEntity.StrmethodName = "QueryCalib";
            //    communicationEntity.ObjParam = cboCalib1.Text;

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            for (int i = 0; i < calibratorPro.Count; i++)
            {
                if (cboCalib1.Text == calibratorPro[i].CalibName)
                {
                    txtPos1.Text = calibratorPro[i].Pos;
                    txtCalibConc1.Text = calibratorPro[i].CalibConcentration.ToString();
                }
            }


        }
        /// <summary>
        /// 显示校准品对应的位置
        /// </summary>
        /// <param name="lisCalibratorinfo"></param>
        public void lisCalibratorinfo(List<Calibratorinfo> lisCalibratorinfo)
        {
            BeginInvoke(new Action(() =>
            {
                for (int i = 0; i < lisCalibratorinfo.Count; i++)
                {
                    if (lisCalibratorinfo[i].CalibName == cboCalib1.Text)
                    {
                        txtPos1.Text = lisCalibratorinfo[i].Pos;
                    }
                    if (lisCalibratorinfo[i].CalibName == cboCalib2.Text)
                    {
                        txtPos2.Text = lisCalibratorinfo[i].Pos;
                    }
                    if (lisCalibratorinfo[i].CalibName == cboCalib3.Text)
                    {
                        txtPos3.Text = lisCalibratorinfo[i].Pos;
                    }
                    if (lisCalibratorinfo[i].CalibName == cboCalib4.Text)
                    {
                        txtPos4.Text = lisCalibratorinfo[i].Pos;
                    }
                    if (lisCalibratorinfo[i].CalibName == cboCalib5.Text)
                    {
                        txtPos5.Text = lisCalibratorinfo[i].Pos;
                    }
                    if (lisCalibratorinfo[i].CalibName == cboCalib6.Text)
                    {
                        txtPos6.Text = lisCalibratorinfo[i].Pos;
                    }
                    if (lisCalibratorinfo[i].CalibName == cboCalib7.Text)
                    {
                        txtPos7.Text = lisCalibratorinfo[i].Pos;
                    }
                }
            }));
        }

        private void cboCalib2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCalib2.Text == cboCalib1.Text && cboCalib1.Visible == true && cboCalib2.Visible == true && cboCalib2.Text.Trim() != "" ||
                cboCalib2.Text == cboCalib3.Text && cboCalib3.Visible == true && cboCalib2.Visible == true && cboCalib2.Text.Trim() != "" ||
                cboCalib2.Text == cboCalib4.Text && cboCalib4.Visible == true && cboCalib2.Visible == true && cboCalib2.Text.Trim() != "" ||
                cboCalib2.Text == cboCalib5.Text && cboCalib5.Visible == true && cboCalib2.Visible == true && cboCalib2.Text.Trim() != "" ||
                cboCalib2.Text == cboCalib6.Text && cboCalib6.Visible == true && cboCalib2.Visible == true && cboCalib2.Text.Trim() != "" ||
                cboCalib2.Text == cboCalib7.Text && cboCalib7.Visible == true && cboCalib2.Visible == true && cboCalib2.Text.Trim() != "")
            {
                cboCalib2.Text = "";
                MessageBoxDraw.ShowMsg("您输入的校准品名称重复，请重新输入！", MsgType.Warning);
                //MessageBox.Show("您输入的校准品名称重复，请重新输入！");
                return;
            }
            //CommunicationEntity communicationEntity = new CommunicationEntity();

            //    communicationEntity.StrmethodName = "QueryCalib";
            //    communicationEntity.ObjParam = cboCalib2.Text;

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            for (int i = 0; i < calibratorPro.Count; i++)
            {
                if (cboCalib2.Text == calibratorPro[i].CalibName)
                {
                    txtPos2.Text = calibratorPro[i].Pos;
                    txtCalibConc2.Text = calibratorPro[i].CalibConcentration.ToString();
                }
            }
        }

        private void cboCalib3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCalib3.Text == cboCalib2.Text && cboCalib2.Visible == true && cboCalib3.Visible == true && cboCalib3.Text.Trim() != "" ||
                cboCalib3.Text == cboCalib1.Text && cboCalib1.Visible == true && cboCalib3.Visible == true && cboCalib3.Text.Trim() != "" ||
                cboCalib3.Text == cboCalib4.Text && cboCalib4.Visible == true && cboCalib3.Visible == true && cboCalib3.Text.Trim() != "" ||
                cboCalib3.Text == cboCalib5.Text && cboCalib5.Visible == true && cboCalib3.Visible == true && cboCalib3.Text.Trim() != "" ||
                cboCalib3.Text == cboCalib6.Text && cboCalib6.Visible == true && cboCalib3.Visible == true && cboCalib3.Text.Trim() != "" ||
                cboCalib3.Text == cboCalib7.Text && cboCalib7.Visible == true && cboCalib3.Visible == true && cboCalib3.Text.Trim() != "")
            {
                cboCalib3.Text = "";
                MessageBoxDraw.ShowMsg("您输入的校准品名称重复，请重新输入！", MsgType.Warning);
                return;
            }
            //CommunicationEntity communicationEntity = new CommunicationEntity();
  
            //    communicationEntity.StrmethodName = "QueryCalib";
            //    communicationEntity.ObjParam = cboCalib3.Text;
    
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            for (int i = 0; i < calibratorPro.Count; i++)
            {
                if (cboCalib3.Text == calibratorPro[i].CalibName)
                {
                    txtPos3.Text = calibratorPro[i].Pos;
                    txtCalibConc3.Text = calibratorPro[i].CalibConcentration.ToString();
                }
            }
        }

        private void cboCalib4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCalib4.Text == cboCalib2.Text && cboCalib2.Visible == true && cboCalib4.Visible == true && cboCalib4.Text.Trim() != "" ||
                cboCalib4.Text == cboCalib3.Text && cboCalib3.Visible == true && cboCalib4.Visible == true && cboCalib4.Text.Trim() != "" ||
                cboCalib4.Text == cboCalib1.Text && cboCalib1.Visible == true && cboCalib4.Visible == true && cboCalib4.Text.Trim() != "" ||
                cboCalib4.Text == cboCalib5.Text && cboCalib5.Visible == true && cboCalib4.Visible == true && cboCalib4.Text.Trim() != "" ||
                cboCalib4.Text == cboCalib6.Text && cboCalib6.Visible == true && cboCalib4.Visible == true && cboCalib4.Text.Trim() != "" ||
                cboCalib4.Text == cboCalib7.Text && cboCalib7.Visible == true && cboCalib4.Visible == true && cboCalib4.Text.Trim() != "")
            {
                cboCalib4.Text = "";
                MessageBox.Show("您输入的校准品名称重复，请重新输入！");
                return;
            }
            //CommunicationEntity communicationEntity = new CommunicationEntity();

            //    communicationEntity.StrmethodName = "QueryCalib";
            //    communicationEntity.ObjParam = cboCalib4.Text;

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            for (int i = 0; i < calibratorPro.Count; i++)
            {
                if (cboCalib4.Text == calibratorPro[i].CalibName)
                {
                    txtPos4.Text = calibratorPro[i].Pos;
                    txtCalibConc4.Text = calibratorPro[i].CalibConcentration.ToString();
                }
            }
        }

        private void cboCalib5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCalib5.Text == cboCalib2.Text && cboCalib2.Visible == true && cboCalib5.Visible == true && cboCalib5.Text.Trim() != "" ||
                cboCalib5.Text == cboCalib3.Text && cboCalib3.Visible == true && cboCalib5.Visible == true && cboCalib5.Text.Trim() != "" ||
                cboCalib5.Text == cboCalib4.Text && cboCalib4.Visible == true && cboCalib5.Visible == true && cboCalib5.Text.Trim() != "" ||
                cboCalib5.Text == cboCalib1.Text && cboCalib1.Visible == true && cboCalib5.Visible == true && cboCalib5.Text.Trim() != "" ||
                cboCalib5.Text == cboCalib6.Text && cboCalib6.Visible == true && cboCalib5.Visible == true && cboCalib5.Text.Trim() != "" ||
                cboCalib5.Text == cboCalib7.Text && cboCalib7.Visible == true && cboCalib5.Visible == true && cboCalib5.Text.Trim() != "")
            {
                cboCalib5.Text = "";
                MessageBoxDraw.ShowMsg("您输入的校准品名称重复，请重新输入！", MsgType.Warning);
                return;
            }
            //CommunicationEntity communicationEntity = new CommunicationEntity();

            //    communicationEntity.StrmethodName = "QueryCalib";
            //    communicationEntity.ObjParam = cboCalib5.Text;

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            for (int i = 0; i < calibratorPro.Count; i++)
            {
                if (cboCalib5.Text == calibratorPro[i].CalibName)
                {
                    txtPos5.Text = calibratorPro[i].Pos;
                    txtCalibConc5.Text = calibratorPro[i].CalibConcentration.ToString();
                }
            }
        }

        private void cboCalib6_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCalib6.Text == cboCalib2.Text && cboCalib2.Visible == true && cboCalib6.Visible == true && cboCalib6.Text.Trim() != "" ||
                cboCalib6.Text == cboCalib3.Text && cboCalib3.Visible == true && cboCalib6.Visible == true && cboCalib6.Text.Trim() != "" ||
                cboCalib6.Text == cboCalib4.Text && cboCalib4.Visible == true && cboCalib6.Visible == true && cboCalib6.Text.Trim() != "" ||
                cboCalib6.Text == cboCalib5.Text && cboCalib5.Visible == true && cboCalib6.Visible == true && cboCalib6.Text.Trim() != "" ||
                cboCalib6.Text == cboCalib1.Text && cboCalib1.Visible == true && cboCalib6.Visible == true && cboCalib6.Text.Trim() != "" ||
                cboCalib6.Text == cboCalib7.Text && cboCalib7.Visible == true && cboCalib6.Visible == true && cboCalib6.Text.Trim() != "")
            {
                cboCalib6.Text = "";
                MessageBoxDraw.ShowMsg("您输入的校准品名称重复，请重新输入！", MsgType.Warning);
                return;
            }
            //CommunicationEntity communicationEntity = new CommunicationEntity();
        
            //    communicationEntity.StrmethodName = "QueryCalib";
            //    communicationEntity.ObjParam = cboCalib6.Text;
       
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            for (int i = 0; i < calibratorPro.Count; i++)
            {
                if (cboCalib6.Text == calibratorPro[i].CalibName)
                {
                    txtPos6.Text = calibratorPro[i].Pos;
                    txtCalibConc6.Text = calibratorPro[i].CalibConcentration.ToString();
                }
            }
        }

        private void cboCalib7_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboCalib7.Text == cboCalib2.Text && cboCalib2.Visible == true && cboCalib7.Visible == true && cboCalib7.Text.Trim() !=""||
                cboCalib7.Text == cboCalib3.Text && cboCalib3.Visible == true && cboCalib7.Visible == true && cboCalib7.Text.Trim() != "" ||
                cboCalib7.Text == cboCalib4.Text && cboCalib4.Visible == true && cboCalib7.Visible == true && cboCalib7.Text.Trim() != "" ||
                cboCalib7.Text == cboCalib5.Text && cboCalib5.Visible == true && cboCalib7.Visible == true && cboCalib7.Text.Trim() != "" ||
                cboCalib7.Text == cboCalib6.Text && cboCalib6.Visible == true && cboCalib7.Visible == true && cboCalib7.Text.Trim() != "" ||
                cboCalib7.Text == cboCalib1.Text && cboCalib1.Visible == true && cboCalib7.Visible == true && cboCalib7.Text.Trim() != ""
               )
            {
                cboCalib7.Text = "";
                MessageBoxDraw.ShowMsg("您输入的校准品名称重复，请重新输入！", MsgType.Warning);
                return;
            }
            //CommunicationEntity communicationEntity = new CommunicationEntity();

            //    communicationEntity.StrmethodName = "QueryCalib";
            //    communicationEntity.ObjParam = cboCalib7.Text;
   
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            for (int i = 0; i < calibratorPro.Count; i++)
            {
                if (cboCalib7.Text == calibratorPro[i].CalibName)
                {
                    txtPos7.Text = calibratorPro[i].Pos;
                    txtCalibConc7.Text = calibratorPro[i].CalibConcentration.ToString();
                }
            }
        }

        public void AddcalibrationCurveInfo(List<CalibrationCurveInfo> calibrationCurveInfo)
        {
            calibrationCurveInfo.Sort(delegate(CalibrationCurveInfo x, CalibrationCurveInfo y)
            {
                return x.CalibConcentration.CompareTo(y.CalibConcentration);
            });
            BeginInvoke(new Action(() =>
            {

                txtCalibConc1.Text = "";
                txtCalibConc2.Text = "";
                txtCalibConc3.Text = "";
                txtCalibConc4.Text = "";
                txtCalibConc5.Text = "";
                txtCalibConc6.Text = "";
                txtCalibConc7.Text = "";
                cboCalib1.Text = "";
                cboCalib2.Text = "";
                cboCalib3.Text = "";
                cboCalib4.Text = "";
                cboCalib5.Text = "";
                cboCalib6.Text = "";
                cboCalib7.Text = "";
                txtPos1.Text = "";
                txtPos2.Text = "";
                txtPos3.Text = "";
                txtPos4.Text = "";
                txtPos5.Text = "";
                txtPos6.Text = "";
                txtPos7.Text = "";
                textEdit1.Text = "";
                if (calibrationCurveInfo.Count == 7)
                {
                    txtCalibConc1.Text = calibrationCurveInfo[0].CalibConcentration.ToString();
                    txtCalibConc2.Text = calibrationCurveInfo[1].CalibConcentration.ToString();
                    txtCalibConc3.Text = calibrationCurveInfo[2].CalibConcentration.ToString();
                    txtCalibConc4.Text = calibrationCurveInfo[3].CalibConcentration.ToString();
                    txtCalibConc5.Text = calibrationCurveInfo[4].CalibConcentration.ToString();
                    txtCalibConc6.Text = calibrationCurveInfo[5].CalibConcentration.ToString();
                    txtCalibConc7.Text = calibrationCurveInfo[6].CalibConcentration.ToString();
                    cboCalib1.Text = calibrationCurveInfo[0].CalibName;
                    cboCalib2.Text = calibrationCurveInfo[1].CalibName;
                    cboCalib3.Text = calibrationCurveInfo[2].CalibName;
                    cboCalib4.Text = calibrationCurveInfo[3].CalibName;
                    cboCalib5.Text = calibrationCurveInfo[4].CalibName;
                    cboCalib6.Text = calibrationCurveInfo[5].CalibName;
                    cboCalib7.Text = calibrationCurveInfo[6].CalibName;
                    txtPos1.Text = calibrationCurveInfo[0].Pos;
                    txtPos2.Text = calibrationCurveInfo[1].Pos;
                    txtPos3.Text = calibrationCurveInfo[2].Pos;
                    txtPos4.Text = calibrationCurveInfo[3].Pos;
                    txtPos5.Text = calibrationCurveInfo[4].Pos;
                    txtPos6.Text = calibrationCurveInfo[5].Pos;
                    txtPos7.Text = calibrationCurveInfo[6].Pos;
                }
                else if (calibrationCurveInfo.Count == 2)
                {
                    txtCalibConc1.Text = calibrationCurveInfo[0].CalibConcentration.ToString();
                    txtCalibConc2.Text = calibrationCurveInfo[1].CalibConcentration.ToString();
                    txtCalibConc3.Text = "";
                    txtCalibConc4.Text = "";
                    txtCalibConc5.Text = "";
                    txtCalibConc6.Text = "";
                    txtCalibConc7.Text = "";
                    cboCalib1.Text = calibrationCurveInfo[0].CalibName;
                    cboCalib2.Text = calibrationCurveInfo[1].CalibName;
                    cboCalib3.Text = "";
                    cboCalib4.Text = "";
                    cboCalib5.Text = "";
                    cboCalib6.Text = "";
                    cboCalib7.Text = "";
                    txtPos1.Text = calibrationCurveInfo[0].Pos;
                    txtPos2.Text = calibrationCurveInfo[1].Pos;
                    txtPos3.Text = "";
                    txtPos4.Text = "";
                    txtPos5.Text = "";
                    txtPos6.Text = "";
                    txtPos7.Text = "";
                }
                else if (calibrationCurveInfo.Count == 1)
                {
                    txtCalibConc1.Text = calibrationCurveInfo[0].CalibConcentration.ToString();
                    txtCalibConc2.Text = "";
                    txtCalibConc3.Text = "";
                    txtCalibConc4.Text = "";
                    txtCalibConc5.Text = "";
                    txtCalibConc6.Text = "";
                    txtCalibConc7.Text = "";
                    cboCalib1.Text = calibrationCurveInfo[0].CalibName;
                    cboCalib2.Text = "";
                    cboCalib3.Text = "";
                    cboCalib4.Text = "";
                    cboCalib5.Text = "";
                    cboCalib6.Text = "";
                    cboCalib7.Text = "";
                    txtPos1.Text = calibrationCurveInfo[0].Pos;
                    txtPos2.Text = "";
                    txtPos3.Text = "";
                    txtPos4.Text = "";
                    txtPos5.Text = "";
                    txtPos6.Text = "";
                    txtPos7.Text = "";
                    textEdit1.Text = calibrationCurveInfo[0].Factor.ToString();
                }
                else if (calibrationCurveInfo.Count == 0)
                {
                    cboCalibMethod.Text = "请选择";
                }

            }));
        }
    }
}