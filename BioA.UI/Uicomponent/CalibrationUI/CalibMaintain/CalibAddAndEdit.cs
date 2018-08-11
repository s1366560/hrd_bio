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
        /// <summary>
        /// 保存更改之前的校准品名称
        /// </summary>
        public string EditCalibratorName;

        /// <summary>
        ///     显示新增校准任务失败的信息
        /// </summary>
        private string strReturnInfo = string.Empty;

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> editDictionary = new Dictionary<string, object[]>();
        public string StrReturnInfo
        {    
            set
            {
                strReturnInfo = value;
                this.Invoke(new EventHandler(delegate {
                    MessageBox.Show(strReturnInfo);
                    this.Close();
                }));
            }
        }

        public delegate void DataHandle(Dictionary<string, object[]> sender);
        public event DataHandle DataHandleEvent;
        

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

                        foreach (AssayProjectInfo a in lisassayProjectInfo)
                        {
                            if (!lstCalibrationCorrespondingProInfo.Exists(x => x.ProjectName == a.ProjectName && x.SampleType == a.SampleType))
                            {
                                lstCalibrationCorrespondingProInfo.Add(new CalibratorProjectinfo() { ProjectName = a.ProjectName, SampleType = a.SampleType });
                            }
                            
                        }
                        foreach (CalibratorProjectinfo c in lstCalibrationCorrespondingProInfo)
                        {
                            dt.Rows.Add(new object[] { c.ProjectName, c.SampleType, c.CalibConcentration == 0 ? "" : c.CalibConcentration.ToString() });
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
                //if (this.Text == "编辑校准品")
                //{
                    //string str = EditCalibratorName;
                    ////CommunicationEntity communicationEntity = new CommunicationEntity();
                    ////communicationEntity.StrmethodName = "QueryProjectItemsByCalibration";
                    ////communicationEntity.ObjParam = str;
                    //editDictionary.Clear();
                    //editDictionary.Add("QueryProjectItemsByCalibration", new List<object>() { str });
                    //CalibrationMaintainSend(editDictionary);   
                //}
                //else
                //{
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
                //}
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
        List<Calibratorinfo> listcalibratorinfo;
        public List<Calibratorinfo> Listcalibratorinfo
        {
            get { return listcalibratorinfo; }
            set
            {
                listcalibratorinfo = value;               
                this.Invoke(new EventHandler(delegate
                {
                    if (listcalibratorinfo.Count != 0)
                    {
                        cboCalibPosition.Properties.Items.Clear();
                        List<string> calibPos = new List<string>();

                        foreach (string pos in RunConfigureUtility.CalibPosition)
                        {
                            calibPos.Add(pos);
                        }
                        calibPos.RemoveAll(x => listcalibratorinfo.Exists(y => y.Pos == x)); 

                        //foreach (Calibratorinfo calibratorinfo in listcalibratorinfo)
                        //{
                        //    foreach (string pos in RunConfigureUtility.CalibPosition)
                        //    {
                        //        if (calibratorinfo.Pos == pos)
                        //        {
                        //            calibPos.Remove(pos);
                        //        }
                        //    }
                        //}
                        cboCalibPosition.Properties.Items.AddRange(calibPos);                                        
                    }
                }));
            }
        }

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
                Calibratorinfo calibratorinfo = new Calibratorinfo();
                calibratorinfo.CalibName = cboCalibName.Text;
                calibratorinfo.InvalidDate = Convert.ToDateTime(cboCalibInvalidDate.Text);
                calibratorinfo.LotNum = cboCalibBatchNumber.Text;
                calibratorinfo.Pos = cboCalibPosition.Text;
                calibratorinfo.Manufacturer = cboCalibTManufacturer.Text;
                Thread.Sleep(500);

                List<CalibratorProjectinfo> liscalibratorProjectinfo = new List<CalibratorProjectinfo>();
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
                        liscalibratorProjectinfo.Add(calibratorProjectinfo);
                    }
                }
                if(liscalibratorProjectinfo.Count == 0)             
                {
                    MessageBox.Show("必须选择一个项目样本浓度！");
                    return;
                }                   
                if (DataHandleEvent != null)
                {
                    //CommunicationEntity communicationEntity = new CommunicationEntity();
                    //communicationEntity.ObjParam = XmlUtility.Serializer(typeof(Calibratorinfo), calibratorinfo);
                    //communicationEntity.ObjLastestParam = XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), liscalibratorProjectinfo);
                    //communicationEntity.StrmethodName = "AddCalibratorinfo";
                    editDictionary.Clear();
                    editDictionary.Add("AddCalibratorinfo", new object[] { XmlUtility.Serializer(typeof(Calibratorinfo), calibratorinfo), XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), liscalibratorProjectinfo) });
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
                Calibratorinfo calibratorinfo = new Calibratorinfo();
                calibratorinfo.CalibName = cboCalibName.Text;
                calibratorinfo.InvalidDate = cboCalibInvalidDate.DateTime;
                calibratorinfo.LotNum = cboCalibBatchNumber.Text;
                calibratorinfo.Pos = cboCalibPosition.Text;
                calibratorinfo.Manufacturer = cboCalibTManufacturer.Text;
                Thread.Sleep(500);

                List<CalibratorProjectinfo> liscalibratorProjectinfo = new List<CalibratorProjectinfo>();
                int count = gridView1.RowCount;
                //List<float> str = new List<float>();
                for (int i = 0; i < count; i++)
                {
                    CalibratorProjectinfo calibratorProjectinfo = new CalibratorProjectinfo();
                    if (gridView1.GetRowCellValue(i, "浓度").ToString() != "")
                    {
                        calibratorProjectinfo.ProjectName = this.gridView1.GetRowCellValue(i, "项目名称").ToString();
                        calibratorProjectinfo.CalibConcentration = (float)Convert.ToDouble(gridView1.GetRowCellValue(i, "浓度"));
                        calibratorProjectinfo.SampleType = this.gridView1.GetRowCellValue(i, "样本类型").ToString();
                        calibratorProjectinfo.CalibName = cboCalibName.Text;
                        liscalibratorProjectinfo.Add(calibratorProjectinfo);
                    }
                }
                if (liscalibratorProjectinfo.Count == 0)
                {
                    //str.Add(calibratorProjectinfo.CalibConcentration);
                    MessageBox.Show("请选择您要修改的项目浓度！");
                    return;
                }
                if (DataHandleEvent != null)
                {
                    //CommunicationEntity communicationEntity = new CommunicationEntity();
                    //communicationEntity.ObjParam = XmlUtility.Serializer(typeof(Calibratorinfo), calibratorinfo);
                    //communicationEntity.ObjThirdParam = XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), liscalibratorProjectinfo);
                    //communicationEntity.StrmethodName = "EditCalibratorinfo";
                    //communicationEntity.ObjLastestParam = EditCalibratorName;
                    editDictionary.Clear();
                    editDictionary.Add("EditCalibratorinfo", new object[] { XmlUtility.Serializer(typeof(Calibratorinfo), calibratorinfo), EditCalibratorName, XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), liscalibratorProjectinfo) });
                    DataHandleEvent(editDictionary);
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
            //editDictionary.Clear();
            // 1.获取校准品对应项目信息
            //if(this.Text == "编辑校准品")
            //{
                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibrationMaintain, XmlUtility.Serializer(typeof(CommunicationEntity),
                //    new CommunicationEntity("QueryAssayProAllInfo")));
                //编辑，查询校准品位置（移除以使用的位置，自己本身不移除）
                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibrationMaintain,XmlUtility.Serializer(typeof(CommunicationEntity),
                //    new CommunicationEntity("QueryCalibPos")));
                //editDictionary.Add("QueryAssayProAllInfo", new List<object>() { "" });
                
            //}
            this.Listcalibratorinfo = this.lstCalibPos;
            //if (this.Text == "装载校准品")
            //{
            //    CommunicationEntity communicationEntity = new CommunicationEntity();
            //    communicationEntity.StrmethodName = "QueryCalibPos";
            //    communicationEntity.ObjParam = "";
            //    CalibrationMaintainSend(communicationEntity);      
            //}
            //editDictionary.Add("QueryCalibPos", new List<object>() { "" });
            //CalibrationMaintainSend(editDictionary);
            //CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibrationMaintain, new Dictionary<string, List<object>>() { { "QueryCalibPos", new List<object>() { "" } } });
            //2.加载位置下拉框
            //List<string> calibPos = RunConfigureUtility.CalibPosition;
            //cboCalibPosition.Properties.Items.Clear();
            //cboCalibPosition.Properties.Items.AddRange(calibPos);
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
    }
}