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
    public partial class QCMaintain : DevExpress.XtraEditors.XtraUserControl
    {
        QualityControlAddAndEdit qualityControlAddAndEdit;

        // 为了编辑界面准备的数据
        QualityControlInfo qCInfoForEdit = new QualityControlInfo();
        List<QCRelationProjectInfo> qcRelationProForEdit = new List<QCRelationProjectInfo>();

        // 质控品信息
        List<QualityControlInfo> lstQualityControlInfo = new List<QualityControlInfo>();

        DataTable dataTableProject = new DataTable();

        DataTable dataTableQC = new DataTable();

        public QCMaintain()
        {
            InitializeComponent();
            
        }

        private void QCMaintain_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadQCMaintain));
            
        }
        private void loadQCMaintain()
        {
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;
            qualityControlAddAndEdit = new QualityControlAddAndEdit();

            dataTableProject.Columns.Add("项目名称");
            dataTableProject.Columns.Add("样本类型");
            dataTableProject.Columns.Add("靶值");
            dataTableProject.Columns.Add("1SD");
            dataTableProject.Columns.Add("2SD");
            dataTableProject.Columns.Add("3SD");

            lstvQCRelativelyProject.DataSource = dataTableProject;

            this.gridView2.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[2].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[3].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[4].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[5].OptionsColumn.AllowEdit = false;
            this.gridView2.Columns[0].Width = 200;
            this.gridView2.Columns[1].Width = 200;
            this.gridView2.Columns[2].Width = 109;
            this.gridView2.Columns[3].Width = 109;
            this.gridView2.Columns[4].Width = 109;
            this.gridView2.Columns[5].Width = 109;

            dataTableQC.Columns.Add("质控品名称");
            dataTableQC.Columns.Add("位置");
            dataTableQC.Columns.Add("批号");
            dataTableQC.Columns.Add("水平浓度");
            dataTableQC.Columns.Add("失效日期");
            dataTableQC.Columns.Add("生产厂家");
            dataTableQC.Columns.Add("冻结");
            lstvQCInfo.DataSource = dataTableQC;
            this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[3].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[4].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[5].OptionsColumn.AllowEdit = false;

            this.gridView1.Columns[0].Width = 130;
            this.gridView1.Columns[1].Width = 100;
            this.gridView1.Columns[2].Width = 100;
            this.gridView1.Columns[3].Width = 100;
            this.gridView1.Columns[4].Width = 150;
            this.gridView1.Columns[5].Width = 200;

            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {                //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCPosition", null)));

                if (qCInfoForEdit.IsLocked == false)
                {
                    MessageBox.Show("该质控品在激活条件下无法编辑，请冻结此质控品！");
                    return;
                }                
                CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryAssayProAllInfo", null)));
                qualityControlAddAndEdit.QCOldInfo = qCInfoForEdit;
                qualityControlAddAndEdit.QCRelateProInfo = qcRelationProForEdit;
                qualityControlAddAndEdit.Text = "编辑质控品";
                qualityControlAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
                qualityControlAddAndEdit.ShowDialog();
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            qualityControlAddAndEdit.Text = "新增质控品";
            qualityControlAddAndEdit.StartPosition = FormStartPosition.CenterScreen;

            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryAssayProAllInfo", null)));
           // CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCPosition", null)));
            qualityControlAddAndEdit.ShowDialog();
        }
        /// <summary>
        /// 接收数据库数据传输
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>
        public void DataTransfer_Event(string strMethod, object sender)
        { 
            switch (strMethod)
            {
                case "QueryAssayProAllInfo":
                    qualityControlAddAndEdit.LstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    break;
                case "QueryQCPosition":
                    qualityControlAddAndEdit.LstPosition = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "AddQualityControl":
                    qualityControlAddAndEdit.StrReturnInfo = sender as string;
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
                    break;
                case "QueryQCAllInfo":
                    lstQualityControlInfo = (List<QualityControlInfo>)XmlUtility.Deserialize(typeof(List<QualityControlInfo>), sender as string);
                    InitQCInfos(lstQualityControlInfo);
                    break;
                case "QueryRelativelyProjectByQCInfo":
                    InitQCRelatePros((List<QCRelationProjectInfo>)XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), sender as string));
                    break;
                case "EditQualityControl":
                    qualityControlAddAndEdit.StrReturnInfo = sender as string;
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
                    break;
                case "EditQCRelateProInfo":
                    if (((int)sender) > 0)
                    {
                        this.Invoke(new EventHandler(delegate
                            {
                                qualityControlAddAndEdit.Close();
                            }));
                        
                        CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
                    }
                    else
                    {
                        MessageBox.Show("质控品关联项目信息更新失败！");
                        return;
                    }
                    break;
                case "LockQualityControl":
                    int iLockRes = (int)sender;
                    if (iLockRes > 0)
                    {
                        CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
                    }
                    else
                    {
                        MessageBox.Show("冻结失败！");
                    }
                    break;
                case "UnLockQualityControl":
                    int iUnLockRes = (int)sender;
                    if (iUnLockRes > 0)
                    {
                        CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
                    }
                    else
                    {
                        MessageBox.Show("解锁失败！");
                    }
                    break;
                case "DeleteQualityControl":
                    if ((sender as string) == "该质控品已被使用，无法删除！")
                    {
                        MessageBox.Show("该质控品已被使用，无法删除！");
                    }
                    else if ((sender as string) == "删除失败！")
                    {
                        MessageBox.Show("删除失败！");
                    }
                    else
                    {
                        CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCAllInfo", null)));
                    }
                    break;
                default:
                    break;
            }
        }

        private void InitQCRelatePros(List<QCRelationProjectInfo> lstQCRelateProsInfo)
        {
            qcRelationProForEdit = lstQCRelateProsInfo;

            this.Invoke(new EventHandler(delegate
                {
                    lstvQCRelativelyProject.RefreshDataSource();
                    dataTableProject.Clear();
                    int i = 1;

                    if (lstQCRelateProsInfo.Count != 0)
                    {
                        foreach (QCRelationProjectInfo qcRelatePro in lstQCRelateProsInfo)
                        {
                            dataTableProject.Rows.Add(new object[] { qcRelatePro.ProjectName, qcRelatePro.SampleType, qcRelatePro.TargetMean, qcRelatePro.TargetSD, qcRelatePro.Target2SD, qcRelatePro.Target3SD });
                        }
                    }


                }));
        }

        private void InitQCInfos(List<QualityControlInfo> lstQCInfos)
        {
            this.Invoke(new EventHandler(delegate
                {
                    lstvQCInfo.RefreshDataSource();
                    dataTableQC.Clear();

                    //gridControl1
                    if (lstQCInfos.Count != 0)
                    {
                        foreach (QualityControlInfo qcInfo in lstQCInfos)
                        {
                            dataTableQC.Rows.Add(new object[] { qcInfo.QCName, qcInfo.Pos, qcInfo.LotNum, qcInfo.HorizonLevel, qcInfo.InvalidDate.ToShortDateString(), qcInfo.Manufacturer, qcInfo.IsLocked == true ? "是" : "否" });
                        }
                    }
                    this.gridView1.ClearSelection();                    

                    lstvQCInfo_Click(null, null);
                }));
        }

        private void lstvQCInfo_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                this.lstvQCRelativelyProject.RefreshDataSource();

                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];

                qCInfoForEdit.QCName = this.gridView1.GetRowCellValue(selectedHandle, "质控品名称").ToString();
                qCInfoForEdit.Pos = this.gridView1.GetRowCellValue(selectedHandle, "位置").ToString();
                qCInfoForEdit.LotNum = this.gridView1.GetRowCellValue(selectedHandle, "批号").ToString();
                qCInfoForEdit.HorizonLevel = this.gridView1.GetRowCellValue(selectedHandle, "水平浓度").ToString();
                qCInfoForEdit.InvalidDate = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedHandle, "失效日期").ToString());
                qCInfoForEdit.Manufacturer = this.gridView1.GetRowCellValue(selectedHandle, "生产厂家").ToString();
                qCInfoForEdit.IsLocked = this.gridView1.GetRowCellValue(selectedHandle, "冻结").ToString() == "是" ? true : false;

                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "QueryRelativelyProjectByQCInfo";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(QualityControlInfo), qCInfoForEdit);
                CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            }

        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                QualityControlInfo qcInfo = new QualityControlInfo();
                qcInfo.QCName = this.gridView1.GetRowCellValue(selectedHandle, "质控品名称").ToString();
                qcInfo.Pos = this.gridView1.GetRowCellValue(selectedHandle, "位置").ToString();
                qcInfo.LotNum = this.gridView1.GetRowCellValue(selectedHandle, "批号").ToString();
                qcInfo.HorizonLevel = this.gridView1.GetRowCellValue(selectedHandle, "水平浓度").ToString();
                qcInfo.InvalidDate = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedHandle, "失效日期").ToString());
                qcInfo.Manufacturer = this.gridView1.GetRowCellValue(selectedHandle, "生产厂家").ToString();
                qcInfo.IsLocked = this.gridView1.GetRowCellValue(selectedHandle, "冻结").ToString() == "是" ? true : false;

                if (qcInfo.IsLocked == true)
                {
                    return;
                }
                else
                {
                    qcInfo.IsLocked = true;
                }


                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "LockQualityControl";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo);
                CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            }
        }

        private void btnUnLock_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                QualityControlInfo qcInfo = new QualityControlInfo();
                qcInfo.QCName = this.gridView1.GetRowCellValue(selectedHandle, "质控品名称").ToString();
                qcInfo.Pos = this.gridView1.GetRowCellValue(selectedHandle, "位置").ToString();
                qcInfo.LotNum = this.gridView1.GetRowCellValue(selectedHandle, "批号").ToString();
                qcInfo.HorizonLevel = this.gridView1.GetRowCellValue(selectedHandle, "水平浓度").ToString();
                qcInfo.InvalidDate = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedHandle, "失效日期").ToString());
                qcInfo.Manufacturer = this.gridView1.GetRowCellValue(selectedHandle, "生产厂家").ToString();
                qcInfo.IsLocked = this.gridView1.GetRowCellValue(selectedHandle, "冻结").ToString() == "是" ? true : false;

                if (qcInfo.IsLocked == false)
                {
                    return;
                }
                else
                {
                    qcInfo.IsLocked = false;
                }

                //
                foreach (QualityControlInfo qc in lstQualityControlInfo)
                {
                    if (qc.Pos == qcInfo.Pos)
                    {
                        if (qc.IsLocked == false)
                        {
                            MessageBoxDraw.ShowMsg(string.Format("在{0}位置已存在被激活的质控品，无法激活！", qc.Pos), MsgType.Warning);
                            return;
                        }
                    }
                }


                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "UnLockQualityControl";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo);
                CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                QualityControlInfo qcInfo = new QualityControlInfo();
                qcInfo.QCName = this.gridView1.GetRowCellValue(selectedHandle, "质控品名称").ToString();
                qcInfo.Pos = this.gridView1.GetRowCellValue(selectedHandle, "位置").ToString();
                qcInfo.LotNum = this.gridView1.GetRowCellValue(selectedHandle, "批号").ToString();
                qcInfo.HorizonLevel = this.gridView1.GetRowCellValue(selectedHandle, "水平浓度").ToString();
                qcInfo.InvalidDate = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedHandle, "失效日期").ToString());
                qcInfo.Manufacturer = this.gridView1.GetRowCellValue(selectedHandle, "生产厂家").ToString();
                qcInfo.IsLocked = this.gridView1.GetRowCellValue(selectedHandle, "冻结").ToString() == "是" ? true : false;

                if (qcInfo.IsLocked == false)
                {
                    MessageBoxDraw.ShowMsg("此质控品在激活情况下无法删除，请冻结后尝试删除！", MsgType.Warning);
                    return;
                }

                CommunicationEntity communicationEntity = new CommunicationEntity();
                communicationEntity.StrmethodName = "DeleteQualityControl";
                communicationEntity.ObjParam = XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo);
                CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCMaintain, XmlUtility.Serializer(typeof(CommunicationEntity), communicationEntity));
            }
        }
    }
}
