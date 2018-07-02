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

namespace BioA.UI
{
    public partial class CalibMaintain : DevExpress.XtraEditors.XtraUserControl
    {
        CalibAddAndEdit calibAddAndEdit;
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

        private void calibAddAndEdit_DataHandleEvent(object sender)
        {
            CalibrationMaintainSend(sender);
        }

        private void CalibrationMaintainSend(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.CalibrationMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }
        private void CalibrationMaintainLoad()
        {
            CommunicationEntity CalibrationMaintain = new CommunicationEntity();
            CalibrationMaintain.StrmethodName = "QueryCalibrationMaintain";
            CalibrationMaintain.ObjParam = "";
            CalibrationMaintainSend(CalibrationMaintain);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            calibAddAndEdit.clear();
            CommunicationEntity CalibrationMaintain = new CommunicationEntity();
            CalibrationMaintain.StrmethodName = "QueryAssayProAllInfo";
            CalibrationMaintain.ObjParam = "";
            CalibrationMaintainSend(CalibrationMaintain);
            calibAddAndEdit.Text = "装载校准品";
            calibAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
            calibAddAndEdit.ShowDialog();                    
        }


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
                calibAddAndEdit.EditCalibratorinfo = calibratorinfo.CalibName;
                calibAddAndEdit.Calibratorinfo_Load(calibratorinfo);
                calibAddAndEdit.Text = "编辑校准品";
                calibAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
                calibAddAndEdit.ShowDialog();
            }
            else
            {
                
            }
        }

        private void CalibMaintain_Load(object sender, EventArgs e)
        {
            CalibrationMaintainLoad(); 
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
                    InitialCalibratorinfo(calibratorinfo);
                    string str="";
                    if (this.gridView1.SelectedRowsCount>0)
                    {
                        str = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "校准品名称").ToString();
                    }        
                    CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                    CalibrationMaintain.StrmethodName = "QueryCalibratorProjectinfo";
                    CalibrationMaintain.ObjParam = str;
                    CalibrationMaintainSend(CalibrationMaintain);
                    break;

                case "QueryCalibratorProjectinfo":
                    List<CalibratorProjectinfo> calibratorProjectinfo = (List<CalibratorProjectinfo>)XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), sender as string);
                    InitialcalibratorProjectinfo(calibratorProjectinfo);
                    break;              
                case  "QueryAssayProAllInfo":
                    List<AssayProjectInfo> lisassayProjectInfo = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    calibAddAndEdit.LisassayProjectInfo = lisassayProjectInfo;
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
                case "QueryProjectItemsByCalibration":
                    List<CalibratorProjectinfo> lisCalibratorProjectinfo1 = XmlUtility.Deserialize(typeof(List<CalibratorProjectinfo>), sender as string) as List<CalibratorProjectinfo>;
                    calibAddAndEdit.LisCalibratorProjectinfo1 = lisCalibratorProjectinfo1;
                    break;
                case "QueryCalibPos":
                    List<Calibratorinfo> calibratorPos = (List<Calibratorinfo>)XmlUtility.Deserialize(typeof(List<Calibratorinfo>), sender as string);
                    calibAddAndEdit.Listcalibratorinfo = calibratorPos;
                    break;
            }
        }


        private void InitialcalibratorProjectinfo(List<CalibratorProjectinfo> listCalibratorProjectinfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                lstvDetectionProject.RefreshDataSource();
                DataTable dt = new DataTable();
                dt.Columns.Add("项目名称");
                dt.Columns.Add("样本类型");
                dt.Columns.Add("校准品浓度");
                foreach (CalibratorProjectinfo calibratorProjectinfo in listCalibratorProjectinfo)
                {
                    dt.Rows.Add(new object[] {calibratorProjectinfo.ProjectName,calibratorProjectinfo.SampleType,calibratorProjectinfo.CalibConcentration
                    });
                }
                lstvDetectionProject.DataSource = dt;
                this.gridView2.Columns[0].OptionsColumn.AllowEdit = false;
                this.gridView2.Columns[1].OptionsColumn.AllowEdit = false;
                this.gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            }));
        }


        private void InitialCalibratorinfo(List<Calibratorinfo> listcalibratorinfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                lstvCalibInfo.RefreshDataSource();
                DataTable dt = new DataTable();
                dt.Columns.Add("校准品名称");
                dt.Columns.Add("装载位置");
                dt.Columns.Add("批号");
                dt.Columns.Add("失效日期");
                dt.Columns.Add("生产厂家");
                foreach (Calibratorinfo calibratorinfo in listcalibratorinfo)
                {
                    dt.Rows.Add(new object[] { calibratorinfo.CalibName,calibratorinfo.Pos,calibratorinfo.LotNum,
                        calibratorinfo.InvalidDate.ToString("yyyy-MM-dd"),calibratorinfo.Manufacturer
                    });
                }
                lstvCalibInfo.DataSource = dt;
                this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[3].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[4].OptionsColumn.AllowEdit = false;
            }));
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.SelectedRowsCount > 0)
            {
                string str = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "校准品名称").ToString();     
            
                CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                CalibrationMaintain.StrmethodName = "QueryCalibratorProjectinfo";
                CalibrationMaintain.ObjParam = str;
                CalibrationMaintainSend(CalibrationMaintain);

            }
        }
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
                CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                CalibrationMaintain.StrmethodName = "DeleteCalibrationMaintain";
                CalibrationMaintain.ObjParam = str;
                CalibrationMaintainSend(CalibrationMaintain);
                //CommunicationEntity CalibrationMaintain = new CommunicationEntity();
                //CalibrationMaintain.StrmethodName = "DeleteCalibratorProjectinfo";
                //CalibrationMaintain.ObjParam = str;
                //CalibrationMaintainSend(CalibrationMaintain);
            }           
        }   
    }
}
