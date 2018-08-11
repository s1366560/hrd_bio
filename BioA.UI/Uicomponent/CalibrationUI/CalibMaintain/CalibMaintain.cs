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
        /// 保存已用的所有校准品位置
        /// </summary>
        private List<Calibratorinfo> calibratorPos;
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
            //CommunicationEntity CalibrationMaintain = new CommunicationEntity();
            //CalibrationMaintain.StrmethodName = "QueryCalibrationMaintain";
            //CalibrationMaintain.ObjParam = "";
            calibMainDictionary.Clear();
            //获取所有校准品对应的所有项目信息
            calibMainDictionary.Add("QueryCalibratorProjectinfo", new object[] { "" });
            //获取所有校准品信息
            calibMainDictionary.Add("QueryCalibrationMaintain", new object[] { "" });
            //获取所有生化项目信息
            calibMainDictionary.Add("QueryAssayProAllInfo", new object[] { "" });
            //获取所有校准品位置
            calibMainDictionary.Add("QueryCalibPos", new object[] { "" });
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
            //CommunicationEntity CalibrationMaintain = new CommunicationEntity();
            //CalibrationMaintain.StrmethodName = "QueryAssayProAllInfo";
            //CalibrationMaintain.ObjParam = "";
            calibAddAndEdit.LisassayProjectInfo = lisassayProjectInfo;
            calibAddAndEdit.Text = "装载校准品";
            calibAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
            calibAddAndEdit.ShowDialog();                    
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
                CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                int selectedHandle = this.gridView1.GetSelectedRows()[0];
                calibratorinfo.CalibName = this.gridView1.GetRowCellValue(selectedHandle, "校准品名称").ToString();
                calibratorinfo.InvalidDate = Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedHandle, "失效日期"));
                calibratorinfo.LotNum = this.gridView1.GetRowCellValue(selectedHandle, "批号").ToString();
                calibratorinfo.Manufacturer = this.gridView1.GetRowCellValue(selectedHandle, "生产厂家").ToString();
                calibratorinfo.Pos = this.gridView1.GetRowCellValue(selectedHandle, "装载位置").ToString();
                calibAddAndEdit.EditCalibratorName = calibratorinfo.CalibName;
                //显示所有项目信息
                calibAddAndEdit.LisassayProjectInfo = lisassayProjectInfo;
                //显示校准品包含的项目信息
                calibAddAndEdit.LstCalibrationCorrespondingProInfo = lstCalibrationCorrespondingProInfo;
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
                    List<Calibratorinfo> calibratorinfo = (List<Calibratorinfo>)XmlUtility.Deserialize(typeof(List<Calibratorinfo>), sender as string);
                    BeginInvoke(new Action(() =>
                    {
                        lstvCalibInfo.RefreshDataSource();
                        dtCalib.Rows.Clear();
                        foreach (Calibratorinfo calibInfo in calibratorinfo)
                        {
                            dtCalib.Rows.Add(new object[] { calibInfo.CalibName,calibInfo.Pos,calibInfo.LotNum,
                        calibInfo.InvalidDate.ToString("yyyy-MM-dd"),calibInfo.Manufacturer});
                        }
                        if (dtCalib.Rows.Count > 0)
                        {
                            gridView1.SelectRow(0);
                            gridControl1_Click(null, null);
                        }
                    }));
                    break;
                case "QueryCalibratorProjectinfo":
                    calibratorProjectinfo = (List<CalibratorProjectinfo>)XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), sender as string);
                    //BeginInvoke(new Action(() =>
                    //{
                    //    lstvDetectionProject.RefreshDataSource();
                    //    dtProject.Rows.Clear();
                    //    foreach (CalibratorProjectinfo calibProjectInfo in calibratorProjectinfo)
                    //    {
                    //        dtProject.Rows.Add(new object[] {calibProjectInfo.ProjectName,calibProjectInfo.SampleType,calibProjectInfo.CalibConcentration});
                    //    }

                    //}));
                    break;              
                case  "QueryAssayProAllInfo":
                    lisassayProjectInfo = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    //calibAddAndEdit.LisassayProjectInfo = lisassayProjectInfo;
                    break;
                case "AddCalibratorinfo":
                    string strAddCalib = sender as string;
                    if (strAddCalib == "你添加的校准品名称已存在！" || strAddCalib == "添加校准任务失败！")
                    {
                        calibAddAndEdit.StrReturnInfo = strAddCalib;
                    }
                    else
                    {
                        calibAddAndEdit.StrReturnInfo = "新增校准品成功！";
                        //calibAddAndEdit.CalibClose = "";
                        CalibrationMaintainLoad();
                    }              
                    break;
                case "AddCalibratorProjectinfo":
                    CalibrationMaintainLoad();                   
                    break;
                case "DeleteCalibrationMaintain":
                    CalibrationMaintainLoad();
                    break;
                //case "DeleteCalibratorProjectinfo":
                //    CalibrationMaintainLoad();
                //    break;
                case "EditCalibratorinfo":
                    string strUpdateCalib = sender as string;
                    if (strUpdateCalib == "修改校准任务失败！" || strUpdateCalib == "您修改的校准品名称已经存在！")
                    {
                        calibAddAndEdit.StrReturnInfo = strUpdateCalib;
                    }
                    else
                    {
                        calibAddAndEdit.StrReturnInfo = "成功修改校准品！";
                        //calibAddAndEdit.CalibClose = "";   
                        CalibrationMaintainLoad();
                    }
                    break;
                //case "QueryProjectItemsByCalibration":
                //    List<CalibratorProjectinfo> lisCalibratorProjectinfo1 = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), sender as string) as List<CalibratorProjectinfo>;
                //    calibAddAndEdit.LisCalibratorProjectinfo1 = lisCalibratorProjectinfo1;
                //    break;
                case "QueryCalibPos":
                    calibratorPos = (List<Calibratorinfo>)XmlUtility.Deserialize(typeof(List<Calibratorinfo>), sender as string);
                    calibAddAndEdit.LstCalibPos = calibratorPos;
                    //calibAddAndEdit.Listcalibratorinfo = calibratorPos;
                    break;
            }
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
        /// 删除校准品点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                DialogResult result = MessageBoxDraw.ShowMsg("请确认是否删除！", MsgType.YesNo);
                if (result == DialogResult.Yes)
                {
                }
                else
                {
                    return;
                }
                string str = this.gridView1.GetRowCellValue(selectedHandle, "校准品名称").ToString();
                //CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                //CalibrationMaintain.StrmethodName = "DeleteCalibrationMaintain";
                //CalibrationMaintain.ObjParam = str;
                calibMainDictionary.Clear();
                calibMainDictionary.Add("DeleteCalibrationMaintain", new object[] { str });
                CalibrationMaintainSend(calibMainDictionary);
                //CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                //CalibrationMaintain.StrmethodName = "DeleteCalibratorProjectinfo";
                //CalibrationMaintain.ObjParam = str;
                //CalibrationMaintainSend(CalibrationMaintain);
            }           
        }   
    }
}
