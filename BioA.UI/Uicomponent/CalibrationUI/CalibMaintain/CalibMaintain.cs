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
using System.Threading;

namespace BioA.UI
{
    public partial class CalibMaintain : DevExpress.XtraEditors.XtraUserControl
    {
        CalibAddAndEdit calibAddAndEdit;

        /// <summary>
        /// 存储客户端发送信息给服务器参数的集合
        /// </summary>
        private Dictionary<string, object[]> calibMainDictionary = new Dictionary<string, object[]>();
        /// <summary>
        /// 存储所有校准品对应的所有项目
        /// </summary>
        private List<CalibratorProjectinfo> calibratorProjectinfo;
        /// <summary>
        /// 存储所有项目信息
        /// </summary>
        private List<AssayProjectInfo> lisassayProjectInfo;
        /// <summary>
        /// 保存校编辑校准品对应的项目信息
        /// </summary>
        private List<CalibratorProjectinfo> lstCalibrationCorrespondingProInfo = new List<CalibratorProjectinfo>();
        /// <summary>
        /// 保存校准品界面已用校准品位置
        /// </summary>
        private List<string> _LstCalibratorPos = new List<string>();
        public CalibMaintain()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;            
            gridView2.Appearance.FocusedRow.Font = font;
            calibAddAndEdit = new CalibAddAndEdit();
            calibAddAndEdit.DataHandleEvent+=calibAddAndEdit_DataHandleEvent;
            calibAddAndEdit.CalibrationSaveOrEnditSuccessEvent += calibAddAndEdit_SaveOrEnditSuccessEvent;
        }
        /// <summary>
        /// 校准品保存成功后被触发的事件
        /// </summary>
        /// <param name="calibInfo"></param>
        /// <param name="lstCalibProInfo"></param>
        private void calibAddAndEdit_SaveOrEnditSuccessEvent(string CalibAddOrUpdateState, Calibratorinfo calibInfo, List<CalibratorProjectinfo> lstCalibProInfo)
        {
            if (CalibAddOrUpdateState == "Add")
            {
                calibratorinfo.Add(calibInfo);
                foreach (CalibratorProjectinfo calibProIfo in lstCalibProInfo)
                {
                    calibratorProjectinfo.Add(calibProIfo);
                }
                DisplayCalibrationInfo(calibratorinfo);
            }
            else
            {
                string calibName = CalibAddOrUpdateState.Substring(CalibAddOrUpdateState.IndexOf(".")+1);
                calibratorinfo.RemoveAll(x => x.CalibName == calibName);
                calibratorinfo.Add(calibInfo);
                foreach (CalibratorProjectinfo calibProIfo in lstCalibProInfo)
                {
                    calibratorProjectinfo.RemoveAll(x => x.ProjectName == calibProIfo.ProjectName && x.CalibName == calibProIfo.CalibName);
                    calibratorProjectinfo.Add(calibProIfo);
                }
                DisplayCalibrationInfo(calibratorinfo);
            }

        }

        private void calibAddAndEdit_DataHandleEvent(Dictionary<string, object[]> sender)
        {
            CalibrationMaintainSend(sender);
        }
        /// <summary>
        /// 发送给服务器处理的参数
        /// </summary>
        /// <param name="sender"></param>
        private void CalibrationMaintainSend(Dictionary<string, object[]> sender)
        {
            var calibMainThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.CalibrationMaintain, sender);
            });
            calibMainThread.IsBackground = true;
            calibMainThread.Start();
            
        }
        private void CalibrationMaintainLoad()
        {
            calibMainDictionary.Clear();
            //获取所有校准品对应的所有项目信息
            calibMainDictionary.Add("QueryCalibratorProjectinfo", new object[] { "" });
            //获取所有校准品信息
            calibMainDictionary.Add("QueryCalibrationMaintain", new object[] { "" });
            //获取所有生化项目信息
            calibMainDictionary.Add("QueryAssayProAllInfo", null);
            //获取所有校准品位置
            //calibMainDictionary.Add("QueryCalibPos", new object[] { "" });
            CalibrationMaintainSend(calibMainDictionary);
        }
        /// <summary>
        /// 新增校准品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            calibAddAndEdit.clear();
            if (lisassayProjectInfo != null)
            {
                calibAddAndEdit.LisassayProjectInfo = lisassayProjectInfo;
                calibAddAndEdit.Text = "装载校准品";
                this.BeginInvoke(new Action(AddOrEditCalibPos));
                calibAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
                calibAddAndEdit.ShowDialog();
            }
        }
        /// <summary>
        /// 添加和编辑获取校准品列表中的所有校准位置
        /// </summary>
        private void AddOrEditCalibPos()
        {
            this._LstCalibratorPos.Clear();
            if (this.gridView1.RowCount > 0)
            {
                for (int i =0; i < this.gridView1.RowCount; i++)
                {
                    string CalibPos = (string)this.gridView1.GetRowCellValue(i, "装载位置");
                    this._LstCalibratorPos.Add(CalibPos);
                }
            }
            calibAddAndEdit.Listcalibratorinfo = this._LstCalibratorPos;
        }

        /// <summary>
        /// 编辑校准品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                Calibratorinfo calibratorinfo = new Calibratorinfo();
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                calibratorinfo.CalibName = this.gridView1.GetRowCellValue(selectedHandle, "校准品名称").ToString();
                calibratorinfo.InvalidDate = Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedHandle, "失效日期"));
                calibratorinfo.LotNum = this.gridView1.GetRowCellValue(selectedHandle, "批号").ToString();
                calibratorinfo.Manufacturer = this.gridView1.GetRowCellValue(selectedHandle, "生产厂家").ToString();
                calibratorinfo.Pos = this.gridView1.GetRowCellValue(selectedHandle, "装载位置").ToString();
                calibAddAndEdit.EditCalibratorName = calibratorinfo.CalibName;
                ////显示所有项目信息
                calibAddAndEdit.LisassayProjectInfo = lisassayProjectInfo;
                //显示校准品包含的项目信息
                calibAddAndEdit.LstCalibrationCorrespondingProInfo = lstCalibrationCorrespondingProInfo;
                this.BeginInvoke(new Action(AddOrEditCalibPos));
                calibAddAndEdit.Calibratorinfo_Load(calibratorinfo);
                calibAddAndEdit.Text = "编辑校准品";
                calibAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
                calibAddAndEdit.ShowDialog();
            }
            else
            {
                
            }
        }
        /// <summary>
        /// 保存校准品信息
        /// </summary>
        DataTable dtCalib = new DataTable();
        /// <summary>
        /// 保存校准品对应的项目信息
        /// </summary>
        DataTable dtProject = new DataTable();
        /// <summary>
        /// 加载页面数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalibMaintain_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(CalibrationMaintainLoad));

            dtCalib.Columns.Add("校准品名称");
            dtCalib.Columns.Add("装载位置");
            dtCalib.Columns.Add("批号");
            dtCalib.Columns.Add("失效日期");
            dtCalib.Columns.Add("生产厂家");
            lstvCalibInfo.DataSource = dtCalib;
            this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[3].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[4].OptionsColumn.AllowEdit = false;

            dtProject.Columns.Add("项目名称");
            dtProject.Columns.Add("样本类型");
            dtProject.Columns.Add("校准品浓度");
            lstvDetectionProject.DataSource = dtProject;
            this.gridView2.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[2].OptionsColumn.AllowEdit = false;
        }

        /// <summary>
        /// 存储所有校准品信息
        /// </summary>
        List<Calibratorinfo> calibratorinfo;
        /// <summary>
        ///     校准品维护界面：
        ///         后端数据传到前端UI事件
        /// </summary>
        /// <param name="strMethod">返回数据的方法名</param>
        /// <param name="sender">返回的数据参数</param>
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryCalibrationMaintain":
                    calibratorinfo = (List<Calibratorinfo>)XmlUtility.Deserialize(typeof(List<Calibratorinfo>), sender as string);
                    BeginInvoke(new Action(() =>
                    {
                        DisplayCalibrationInfo(calibratorinfo);
                    }));
                    break;
                case "QueryCalibratorProjectinfo":
                    calibratorProjectinfo = (List<CalibratorProjectinfo>)XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), sender as string);
                    break;              

                case  "QueryAssayProAllInfo":
                    lisassayProjectInfo = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    break;

                case "AddCalibratorinfo":
                    string strAddCalib = sender as string;
                    calibAddAndEdit.StrReturnInfo = strAddCalib;
                    break;

                case "DeleteCalibrationMaintain":
                    string strResult = sender as string;
                    if (strResult == "删除成功！")
                    {
                        for (int i = 0; i < lstCalibProjectInfo.Count; i++)
                        {
                            if (i == 0)
                            {
                                calibratorinfo.RemoveAll(y => y.CalibName == lstCalibProjectInfo[i].CalibName);
                            }
                            calibratorProjectinfo.RemoveAll(x => x.ProjectName == lstCalibProjectInfo[i].ProjectName && x.SampleType == lstCalibProjectInfo[i].SampleType);
                        }
                        BeginInvoke(new Action(() => { DisplayCalibrationInfo(calibratorinfo); }));
                        MessageBox.Show(strResult);
                    }
                    else
                        MessageBox.Show(strResult);
                    break;

                case "EditCalibratorinfo":
                    string strUpdateCalib = sender as string;
                    calibAddAndEdit.StrReturnInfo = strUpdateCalib;
                    break;
                    //服务层没有注释掉
                //case "QueryCalibPos":
                //    calibratorPos = (List<Calibratorinfo>)XmlUtility.Deserialize(typeof(List<Calibratorinfo>), sender as string);
                //    calibAddAndEdit.LstCalibPos = calibratorPos;
                //    break;
            }
        }
        /// <summary>
        /// 界面显示校准品信息
        /// </summary>
        /// <param name="lstcalibInfo"></param>
        private void DisplayCalibrationInfo(List<Calibratorinfo> lstcalibInfo)
        {
            this.Invoke(new EventHandler(delegate 
            { 
                lstvCalibInfo.RefreshDataSource();
                dtCalib.Rows.Clear();
                foreach (Calibratorinfo calibInfo in lstcalibInfo)
                {
                    dtCalib.Rows.Add(new object[] { calibInfo.CalibName,calibInfo.Pos,calibInfo.LotNum,calibInfo.InvalidDate.ToString("yyyy-MM-dd"),calibInfo.Manufacturer});
                }
                if (dtCalib.Rows.Count > 0)
                {
                    gridView1.SelectRow(0);
                    gridControl1_Click(null, null);
                }
            }));
        }

        /// <summary>
        /// 校准品列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_Click(object sender, EventArgs e)
        {
            calibMainDictionary.Clear();
            if (this.gridView1.SelectedRowsCount > 0)
            {
                string str = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "校准品名称").ToString();
                lstvDetectionProject.RefreshDataSource();
                dtProject.Rows.Clear();
                lstCalibrationCorrespondingProInfo.Clear();
                foreach (CalibratorProjectinfo calibProjectInfo in calibratorProjectinfo)
                {
                    if (calibProjectInfo.CalibName == str)
                    {
                        dtProject.Rows.Add(new object[] { calibProjectInfo.ProjectName, calibProjectInfo.SampleType, calibProjectInfo.CalibConcentration });
                        lstCalibrationCorrespondingProInfo.Add(calibProjectInfo);
                    }
                }
                //CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                //CalibrationMaintain.StrmethodName = "QueryCalibratorProjectinfo";
                //CalibrationMaintain.ObjParam = str;
                //calibMainDictionary.Add("QueryCalibratorProjectinfo", new object[] { str });
                //CalibrationMaintainSend(calibMainDictionary);

            }
        }
        /// <summary>
        /// 存储删除校准品信息和对应的项目信息
        /// </summary>
        private List<CalibratorProjectinfo> lstCalibProjectInfo;
        /// <summary>
        /// 删除校准品点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            lstCalibProjectInfo = new List<CalibratorProjectinfo>();
            if (gridView1.SelectedRowsCount > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                DialogResult result = MessageBoxDraw.ShowMsg("请确认是否删除！", MsgType.YesNo);
                if (result == DialogResult.Yes)
                {
                    string calibName = this.gridView1.GetRowCellValue(selectedHandle, "校准品名称").ToString();
                    for(int i =0; i< gridView2.RowCount; i++)
                    {
                        CalibratorProjectinfo calibProjectInfo = new CalibratorProjectinfo();
                        calibProjectInfo.ProjectName = this.gridView2.GetRowCellValue(i, "项目名称").ToString();
                        calibProjectInfo.SampleType = this.gridView2.GetRowCellValue(i, "样本类型").ToString();
                        calibProjectInfo.CalibName = calibName;
                        lstCalibProjectInfo.Add(calibProjectInfo);
                        
                    }
                    calibMainDictionary.Clear();
                    calibMainDictionary.Add("DeleteCalibrationMaintain", new object[] { XmlUtility.Serializer(typeof(List<CalibratorProjectinfo>), lstCalibProjectInfo) });
                    CalibrationMaintainSend(calibMainDictionary);
                }
                else
                {
                    return;
                }
            }           
        }   
    }
}
