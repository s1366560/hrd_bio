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
using BioA.UI;

namespace BioA.UI
{
    public partial class CalibAddAndEdit : DevExpress.XtraEditors.XtraForm
    {
        //（添加/编辑校准品和校准品项目信息成功的委托事件）
        public delegate void CalibrationSaveOrEnditSuccess(string AddOrUpdateState, Calibratorinfo calibInfo, List<CalibratorProjectinfo> lstCalibProInfo);
        public event CalibrationSaveOrEnditSuccess CalibrationSaveOrEnditSuccessEvent;
        //（处理添加/编辑校准品和校准品项目信息的委托事件）
        public delegate void DataHandle(Dictionary<string, object[]> sender);
        public event DataHandle DataHandleEvent;
        /// <summary>
        /// 保存更改之前的校准品名称
        /// </summary>
        public string EditCalibratorName;

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> editDictionary = new Dictionary<string, object[]>();

        /// <summary>
        ///     显示新增校准任务失败的信息
        /// </summary>
        private string strReturnInfo = string.Empty;

        public string StrReturnInfo
        {
            set
            {
                strReturnInfo = value;
                if (strReturnInfo == "添加校准品任务成功！")
                {
                    if (CalibrationSaveOrEnditSuccessEvent != null)
                    {
                        CalibrationSaveOrEnditSuccessEvent("Add", _NewCalibratorinfo, liscalibratorProjectinfo);
                    }
                    MessageBoxDraw.ShowMsg(strReturnInfo,MsgType.OK);
                    this.Invoke(new EventHandler(delegate { this.Close(); }));
                }
                else if (strReturnInfo == "校准品和项目信息修改成功！")
                {
                    if (CalibrationSaveOrEnditSuccessEvent != null)
                    {
                        CalibrationSaveOrEnditSuccessEvent("Update." + EditCalibratorName, _NewCalibratorinfo, liscalibratorProjectinfo);
                    }
                    MessageBoxDraw.ShowMsg(strReturnInfo, MsgType.OK);
                    this.Invoke(new EventHandler(delegate { this.Close(); }));
                }
                else
                {
                    MessageBoxDraw.ShowMsg(strReturnInfo, MsgType.OK);
                    this.Invoke(new EventHandler(delegate { this.Close(); }));
                }
            }
        }
        /// <summary>
        ///     校准品维护：
        ///         编辑界面
        ///             （根据校准品名称查找对应的项目信息和没有关联的项目信息）
        /// </summary>
        List<CalibratorProjectinfo> lstCalibrationCorrespondingProInfo;

        public List<CalibratorProjectinfo> LstCalibrationCorrespondingProInfo
        {
            get { return lstCalibrationCorrespondingProInfo; }
            set
            {
                lstCalibrationCorrespondingProInfo = value;
                if (lisassayProjectInfo != null && lstCalibrationCorrespondingProInfo != null)
                {
                    //this.Invoke(new EventHandler(delegate
                    //{
                    DataTable dt = new DataTable();
                    dt.Columns.Add("项目名称");
                    dt.Columns.Add("样本类型");
                    dt.Columns.Add("浓度");

                    foreach (CalibratorProjectinfo c in lstCalibrationCorrespondingProInfo)
                    {
                        dt.Rows.Add(new object[] { c.ProjectName, c.SampleType, c.CalibConcentration });
                    }

                    foreach (AssayProjectInfo a in lisassayProjectInfo)
                    {
                        if (!lstCalibrationCorrespondingProInfo.Exists(x => x.ProjectName == a.ProjectName && x.SampleType == a.SampleType))
                        {
                            dt.Rows.Add(new object[] { a.ProjectName, a.SampleType, "" });
                        }
                    }
                    lstvProjectInfo.DataSource = null;
                    lstvProjectInfo.DataSource = dt;
                    gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                    gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                    //}));
                }

            }
        }

        /// <summary>
        ///     校准品维护：
        ///         编辑校准品/新增校准品
        ///             （显示校准品信息（LisassayProjectInfo）和项目信息（LisCalibratorProjectinfo1））
        /// </summary>
        List<AssayProjectInfo> lisassayProjectInfo;
        public List<AssayProjectInfo> LisassayProjectInfo
        {
            get { return lisassayProjectInfo; }
            set
            {
                lisassayProjectInfo = value;
                DataTable dt = new DataTable();
                dt.Columns.Add("项目名称");
                dt.Columns.Add("样本类型");
                dt.Columns.Add("浓度");
                foreach (AssayProjectInfo a in lisassayProjectInfo)
                {
                    dt.Rows.Add(new object[] {a.ProjectName,a.SampleType,""
                        });
                }
                lstvProjectInfo.DataSource = null;
                lstvProjectInfo.DataSource = dt;
                this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            }
        }
        public CalibAddAndEdit()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        public void clear()
        {
            cboCalibName.Text = "";
            cboCalibBatchNumber.Text = "";
            cboCalibTManufacturer.Text = "";
            cboCalibInvalidDate.DateTime = DateTime.Now.AddMonths(1);
            cboCalibPosition.Text = "请选择";

        }
        public void Calibratorinfo_Load(Calibratorinfo calibratorinfo)
        {
            this._OldCalibratorinfo = calibratorinfo;
            cboCalibName.Text = calibratorinfo.CalibName;
            cboCalibInvalidDate.DateTime = calibratorinfo.InvalidDate;
            cboCalibBatchNumber.Text = calibratorinfo.LotNum;
            cboCalibPosition.Text = calibratorinfo.Pos;
            cboCalibTManufacturer.Text = calibratorinfo.Manufacturer;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
            lstvProjectInfo.DataSource = null;
        }

        List<Calibratorinfo> lstCalibPos = new List<Calibratorinfo>();
        public List<Calibratorinfo> LstCalibPos
        {
            get { return lstCalibPos; }
            set { lstCalibPos = value; }
        }
        /// <summary>
        ///     校准品维护：
        ///         显示校准品位置
        ///             （移除已选）
        /// </summary>
        List<string> listcalibratorinfo;
        public List<string> Listcalibratorinfo
        {
            get { return listcalibratorinfo; }
            set
            {
                listcalibratorinfo = value;
                this.Invoke(new EventHandler(delegate
                {
                    cboCalibPosition.Properties.Items.Clear();
                    if (listcalibratorinfo.Count != 0)
                    {
                        List<string> calibPos = new List<string>();

                        calibPos.AddRange(RunConfigureUtility.CalibPosition);

                        calibPos.RemoveAll(x => listcalibratorinfo.Exists(y => y == x));
                        cboCalibPosition.Properties.Items.AddRange(calibPos);
                    }
                    else
                        cboCalibPosition.Properties.Items.AddRange(RunConfigureUtility.CalibPosition);
                }));
            }
        }
        /// <summary>
        /// 修改之前的校准品信息
        /// </summary>
        private Calibratorinfo _OldCalibratorinfo = new Calibratorinfo();
        /// <summary>
        /// 添加或编辑（存储新的校准品信息）
        /// </summary>
        private Calibratorinfo _NewCalibratorinfo;
        /// <summary>
        /// 添加或编辑（存储校准品对应的项目信息）
        /// </summary>
        private List<CalibratorProjectinfo> liscalibratorProjectinfo;
        /// <summary>
        /// 校准品维护：
        ///     保存（新增/修改（校准品和项目信息））
        ///     
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Text == "装载校准品")
            {

                if (cboCalibName.Text == null || cboCalibName.Text == "")
                {
                    MessageBox.Show("校准品名称不能为空，请填写校准品名称！");
                    this.cboCalibName.Focus();
                    return;
                }
                if (cboCalibPosition.Text == "请选择")
                {
                    MessageBox.Show("校准品位置不能为空，请选择位置！");
                    this.cboCalibPosition.Focus();
                    return;
                }

                Invoke(new Action(() => { AddOrEnditCalibrationInfo(this.Text); }));

                if (liscalibratorProjectinfo.Count == 0)
                {
                    MessageBox.Show("必须选择一个项目样本浓度！");
                    return;
                }
                if (DataHandleEvent != null)
                {
                    editDictionary.Clear();
                    editDictionary.Add("AddCalibratorinfo",
                        new object[] {
                            XmlUtility.Serializer(typeof(Calibratorinfo), _NewCalibratorinfo),
                            XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), liscalibratorProjectinfo)
                        });
                    DataHandleEvent(editDictionary);

                }
            }
            if (this.Text == "编辑校准品")
            {

                if (cboCalibName.Text == null || cboCalibName.Text == "")
                {
                    MessageBox.Show("校准品名称不能为空，请填写校准品名称！");
                    return;
                }
                if (cboCalibPosition.Text == "请选择")
                {
                    MessageBox.Show("校准品位置不能为空，请选择位置！");
                    return;
                }
                if (strReturnInfo == "校准品编辑中")
                {
                    return;
                }
                strReturnInfo = "校准品编辑中";
                this.Invoke(new Action(() => { AddOrEnditCalibrationInfo(this.Text); }));
                if (liscalibratorProjectinfo.Count == 0)
                {
                    //str.Add(calibratorProjectinfo.CalibConcentration);
                    MessageBox.Show("请选择您要修改的项目浓度！");
                    return;
                }
                if (liscalibratorProjectinfo.Count < lstCalibrationCorrespondingProInfo.Count)
                {
                    MessageBox.Show("请不要将浓度设置为空！");
                    return;
                }
                if (DataHandleEvent != null)
                {
                    editDictionary.Clear();
                    editDictionary.Add("EditCalibratorinfo",
                        new object[] {
                            XmlUtility.Serializer(typeof(Calibratorinfo), _NewCalibratorinfo),
                            XmlUtility.Serializer(typeof(Calibratorinfo), _OldCalibratorinfo),
                            XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), liscalibratorProjectinfo),
                            XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>),lstCalibrationCorrespondingProInfo)
                        });
                    DataHandleEvent(editDictionary);
                }

            }
        }
        /// <summary>
        /// 获取添加或编辑校准品信息和对应的项目信息
        /// </summary>
        private void AddOrEnditCalibrationInfo(string text)
        {
            _NewCalibratorinfo = new Calibratorinfo();
            _NewCalibratorinfo.CalibName = cboCalibName.Text;
            _NewCalibratorinfo.InvalidDate = Convert.ToDateTime(cboCalibInvalidDate.Text);
            _NewCalibratorinfo.LotNum = cboCalibBatchNumber.Text;
            _NewCalibratorinfo.Pos = cboCalibPosition.Text;
            _NewCalibratorinfo.Manufacturer = cboCalibTManufacturer.Text;
            Thread.Sleep(500);

            liscalibratorProjectinfo = new List<CalibratorProjectinfo>();
            int count = gridView1.RowCount;
            //List<float> str = new List<float>();
            for (int i = 0; i < count; i++)
            {
                CalibratorProjectinfo calibratorProjectinfo = new CalibratorProjectinfo();
                if (gridView1.GetRowCellValue(i, "浓度").ToString() != "")
                {
                    calibratorProjectinfo.ProjectName = this.gridView1.GetRowCellValue(i, "项目名称").ToString();
                    calibratorProjectinfo.SampleType = this.gridView1.GetRowCellValue(i, "样本类型").ToString();
                    calibratorProjectinfo.CalibConcentration = (float)Convert.ToDouble(gridView1.GetRowCellValue(i, "浓度"));
                    calibratorProjectinfo.CalibName = cboCalibName.Text;
                    if (text != "装载校准品")
                    {
                        //if (!lstCalibrationCorrespondingProInfo.Exists(x => x.ProjectName == calibratorProjectinfo.ProjectName))
                        liscalibratorProjectinfo.Add(calibratorProjectinfo);
                    }
                    else
                        liscalibratorProjectinfo.Add(calibratorProjectinfo);
                }

            }
        }



        private void CalibrationMaintainSend(Dictionary<string, object[]> sender)
        {
            var calibEditThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibrationMaintain, sender);
            });
            calibEditThread.IsBackground = true;
            calibEditThread.Start();
        }

        /// <summary>
        ///     新增和编辑（页面加载）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibAddAndEdit_Load(object sender, EventArgs e)
        {
            //this.Listcalibratorinfo = this.lstCalibPos;
        }

        /// <summary>
        ///     校准品维护界面：
        ///         表单关闭事件
        ///             清除（上次新增、编辑界面的数据）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibAddAndEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            lstvProjectInfo.DataSource = new DataTable();
        }
        /// <summary>
        /// 项目浓度值点击事件：
        ///     有值就不能编辑，没有值就能编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            //DataRow row = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            //if (row != null)
            //{
            //    if ((string)row["浓度"] != "")
            //    {
            //        e.Cancel = true;
            //    }
            //}
        }
    }
}