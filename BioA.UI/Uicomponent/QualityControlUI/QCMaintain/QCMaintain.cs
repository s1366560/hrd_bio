﻿using System;
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
using BioA.Service;

namespace BioA.UI
{
    public partial class QCMaintain : DevExpress.XtraEditors.XtraUserControl
    {
        QualityControlAddAndEdit qualityControlAddAndEdit;

        // 为了编辑界面准备的数据
        QualityControlInfo qCInfoForEdit = new QualityControlInfo();
        /// <summary>
        /// 保存质控品对应的质控项目信息
        /// </summary>
        private List<QCRelationProjectInfo> qcRelationProForEdit = new List<QCRelationProjectInfo>();

        // 质控品信息
        private List<QualityControlInfo> lstQualityControlInfo = new List<QualityControlInfo>();
        /// <summary>
        /// 保存质控项目的平均值信息
        /// </summary>
        private DataTable dataTableProject = new DataTable();
        /// <summary>
        /// 保存质控品信息
        /// </summary>
        private DataTable dataTableQC = new DataTable();
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> qcMaintainDic = new Dictionary<string, object[]>();
        /// <summary>
        /// 存储所有的质控品项目信息
        /// </summary>
        private List<QCRelationProjectInfo> lstQCRelationProjectInfo = new List<QCRelationProjectInfo>();
        /// <summary>
        /// 所有生化项目信息
        /// </summary>
        private List<AssayProjectInfo> lstAssayProInfo = new List<AssayProjectInfo>();
        /// <summary>
        /// 数据库访问层
        /// </summary>
        QCMaintian qcMaintian = new QCMaintian();
        public QCMaintain()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;
            dataTableProject.Columns.Add("项目名称");
            dataTableProject.Columns.Add("样本类型");
            dataTableProject.Columns.Add("靶值");
            dataTableProject.Columns.Add("1SD");
            dataTableProject.Columns.Add("2SD");
            dataTableProject.Columns.Add("3SD");

            lstvQCRelativelyProject.DataSource = dataTableProject;

            dataTableQC.Columns.Add("质控品名称");
            dataTableQC.Columns.Add("位置");
            dataTableQC.Columns.Add("批号");
            dataTableQC.Columns.Add("水平浓度");
            dataTableQC.Columns.Add("失效日期");
            dataTableQC.Columns.Add("生产厂家");
            dataTableQC.Columns.Add("冻结");
            dataTableQC.Columns.Add("质控品ID");
            lstvQCInfo.DataSource = dataTableQC;

        }

        public void QCMaintain_Load(object sender, EventArgs e)
        {
            this.loadQCMaintain();
            
        }
        /// <summary>
        /// 清空本类成员属性值
        /// </summary>
        public void ClearQCMaintainParam()
        {
            this.qcRelationProForEdit.Clear();
            this.lstQualityControlInfo.Clear();
            this.qcMaintainDic.Clear();
            this.lstQCRelationProjectInfo.Clear();
            this.lstAssayProInfo.Clear();
        }
        private void loadQCMaintain()
        {
            //获取质控品信息对应的所有项目信息
            lstQCRelationProjectInfo = qcMaintian.QueryRelativelyProjectByQCInfo("QueryRelativelyProjectByQCInfo", null);

            //获取所有质控信息
            lstQualityControlInfo = new QCGraphics().QueryQCAllInfo("QueryQCAllInfo");
            InitQCInfos(lstQualityControlInfo);
            //获取所有项目信息
            lstAssayProInfo = qcMaintian.QueryAssayProAllInfo("QueryAssayProAllInfo", null);
        }
        /// <summary>
        /// 发送信息给服务器
        /// </summary>
        /// <param name="param"></param>
        private void SendToServices(Dictionary<string, object[]> param)
        {
            var qcMianThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.QCMaintain, qcMaintainDic);
            });
            qcMianThread.IsBackground = true;
            qcMianThread.Start();
        }
        /// <summary>
        /// 执行（质控品新增和修改后传递的参数方法）事件
        /// </summary>
        /// <param name="result"></param>
        /// <param name="keyValuePairs"></param>
        private void Execute_TransmitQCAndTestProjectInfoEvent(string result, Dictionary<QualityControlInfo, List<QCRelationProjectInfo>> keyValuePairs)
        {
            if(result == "新增质控品")
            {
                foreach( var keyValues in keyValuePairs)
                {
                    lstQualityControlInfo.Add(keyValues.Key);
                    foreach(var value in keyValues.Value)
                    {
                        lstQCRelationProjectInfo.Add(value);
                    }
                }

                this.Invoke(new EventHandler(delegate
                {
                    InitQCInfos(lstQualityControlInfo);
                    MessageBox.Show("质控品添加成功！");
                    qualityControlAddAndEdit.Close();
                }));
            }
            else
            {
                if(result == "编辑质控品")
                {
                    foreach (var keyValues in keyValuePairs)
                    {
                        lstQualityControlInfo.RemoveAll(r => r.QCID == keyValues.Key.QCID);
                        lstQualityControlInfo.Add(keyValues.Key);
                        foreach (var value in keyValues.Value)
                        {
                            lstQCRelationProjectInfo.RemoveAll( p => p.QCID == value.QCID && p.ProjectName == value.ProjectName);
                            lstQCRelationProjectInfo.Add(value);
                        }
                    }

                    this.Invoke(new EventHandler(delegate
                    {
                        InitQCInfos(lstQualityControlInfo);
                        MessageBox.Show("质控品修改成功！");
                        qualityControlAddAndEdit.Close();
                    }));
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            { 
                if (qCInfoForEdit.IsLocked == false)
                {
                    MessageBox.Show("该质控品在激活条件下无法编辑，请冻结此质控品！");
                    return;
                }
                if (qualityControlAddAndEdit == null)
                {
                    qualityControlAddAndEdit = new QualityControlAddAndEdit();
                    qualityControlAddAndEdit.TransmitQCAndTestProjectInfoEvent += Execute_TransmitQCAndTestProjectInfoEvent;
                    qualityControlAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
                }
                qualityControlAddAndEdit.QCOldInfo = qCInfoForEdit;
                qualityControlAddAndEdit.QCRelateProInfo = qcRelationProForEdit;
                qualityControlAddAndEdit.Text = "编辑质控品";
                qualityControlAddAndEdit.LstAssayProInfos = lstAssayProInfo;
                qualityControlAddAndEdit.ListQualityControlInfo = lstQualityControlInfo;
                qualityControlAddAndEdit.QualityControlAddAndEdit_Load(null,null);
                qualityControlAddAndEdit.ShowDialog();
            }
            
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (qualityControlAddAndEdit == null)
            {
                qualityControlAddAndEdit = new QualityControlAddAndEdit();
                qualityControlAddAndEdit.TransmitQCAndTestProjectInfoEvent += Execute_TransmitQCAndTestProjectInfoEvent;
                qualityControlAddAndEdit.StartPosition = FormStartPosition.CenterScreen;
            }
            qualityControlAddAndEdit.Text = "新增质控品";
            qualityControlAddAndEdit.LstAssayProInfos = lstAssayProInfo;
            qualityControlAddAndEdit.ListQualityControlInfo = lstQualityControlInfo;
            qualityControlAddAndEdit.QualityControlAddAndEdit_Load(null,null);
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
                    lstAssayProInfo = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    break;
                case "QueryQCPosition":
                    qualityControlAddAndEdit.LstPosition = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    break;
                case "AddQualityControl":
                    qualityControlAddAndEdit.StrReturnInfo = sender as string;
                    break;
                case "QueryQCAllInfo":
                    lstQualityControlInfo = (List<QualityControlInfo>)XmlUtility.Deserialize(typeof(List<QualityControlInfo>), sender as string);
                    this.Invoke(new EventHandler(delegate { InitQCInfos(lstQualityControlInfo); }));
                    break;
                case "QueryRelativelyProjectByQCInfo":
                    lstQCRelationProjectInfo = (List<QCRelationProjectInfo>)XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), sender as string);
                    break;
                case "EditQualityControl":
                    qualityControlAddAndEdit.StrReturnInfo = sender as string;
                    
                    break;
                case "EditQCRelateProInfo":
                    if (((int)sender) > 0)
                    {
                        this.Invoke(new EventHandler(delegate
                            {
                                qualityControlAddAndEdit.Close();
                            }));
                        qcMaintainDic.Clear();
                        qcMaintainDic.Add("QueryQCAllInfo", null);
                        SendToServices(qcMaintainDic);
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
                        qcMaintainDic.Clear();
                        qcMaintainDic.Add("QueryQCAllInfo", null);
                        SendToServices(qcMaintainDic);
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
                        qcMaintainDic.Clear();
                        qcMaintainDic.Add("QueryQCAllInfo", null);
                        SendToServices(qcMaintainDic);
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
                        qcMaintainDic.Clear();
                        //获取质控品信息对应的所有项目信息
                        qcMaintainDic.Add("QueryRelativelyProjectByQCInfo", null);
                        //获取所有质控信息
                        qcMaintainDic.Add("QueryQCAllInfo", null);
                        //获取所有项目信息
                        qcMaintainDic.Add("QueryAssayProAllInfo", new object[] { "" });
                        SendToServices(qcMaintainDic);
                        MessageBox.Show("删除成功！");
                    }
                    break;
                default:
                    break;
            }
        }

        private void InitQCRelatePros(List<QCRelationProjectInfo> lstQCRelateProsInfo)
        {
            dataTableProject.Rows.Clear();
            qcRelationProForEdit.Clear();
            if (lstQCRelateProsInfo.Count > 0 || lstQCRelationProjectInfo != null)
            {
                foreach (QCRelationProjectInfo qcInfo in lstQCRelateProsInfo)
                {
                    qcRelationProForEdit.Add(qcInfo);
                    dataTableProject.Rows.Add(new object[] { qcInfo.ProjectName, qcInfo.SampleType, qcInfo.TargetMean, qcInfo.TargetSD, qcInfo.Target2SD, qcInfo.Target3SD });
                }
            }
        }
        /// <summary>
        /// 显示所有的质控品
        /// </summary>
        /// <param name="lstQCInfos"></param>
        private void InitQCInfos(List<QualityControlInfo> lstQCInfos)
        {
            //gridControl1
            if (lstQCInfos.Count != 0)
            {
                dataTableQC.Rows.Clear();
                foreach (QualityControlInfo qcInfo in lstQCInfos)
                {
                    dataTableQC.Rows.Add(new object[] { qcInfo.QCName, qcInfo.Pos, qcInfo.LotNum, qcInfo.HorizonLevel, qcInfo.InvalidDate.ToShortDateString(), qcInfo.Manufacturer, qcInfo.IsLocked == true ? "是" : "否",qcInfo.QCID });
                }
                this.gridView1.Columns["质控品ID"].Visible = false;
            }
            this.gridView1.ClearSelection();

            lstvQCInfo_Click(null, null);
        }
        /// <summary>
        /// 质控品信息列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstvQCInfo_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];

                qCInfoForEdit.QCName = this.gridView1.GetRowCellValue(selectedHandle, "质控品名称").ToString();
                qCInfoForEdit.Pos = this.gridView1.GetRowCellValue(selectedHandle, "位置").ToString();
                qCInfoForEdit.LotNum = this.gridView1.GetRowCellValue(selectedHandle, "批号").ToString();
                qCInfoForEdit.HorizonLevel = this.gridView1.GetRowCellValue(selectedHandle, "水平浓度").ToString();
                qCInfoForEdit.InvalidDate = System.Convert.ToDateTime(this.gridView1.GetRowCellValue(selectedHandle, "失效日期").ToString());
                qCInfoForEdit.Manufacturer = this.gridView1.GetRowCellValue(selectedHandle, "生产厂家").ToString();
                qCInfoForEdit.IsLocked = this.gridView1.GetRowCellValue(selectedHandle, "冻结").ToString() == "是" ? true : false;
                qCInfoForEdit.QCID = Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "质控品ID").ToString());


                InitQCRelatePros(lstQCRelationProjectInfo.FindAll(s => s.QCID == qCInfoForEdit.QCID) == null ? null : lstQCRelationProjectInfo.FindAll(s => s.QCID == qCInfoForEdit.QCID));
            }

        }
        /// <summary>
        /// 冻结点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                qcMaintainDic.Clear();
                qcMaintainDic.Add("LockQualityControl", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo) });
                SendToServices(qcMaintainDic);
            }
        }
        /// <summary>
        /// 激活点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


                qcMaintainDic.Clear();
                qcMaintainDic.Add("UnLockQualityControl", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo) });
                SendToServices(qcMaintainDic);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                qcInfo.QCID =Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "质控品ID").ToString());
                if (qcInfo.IsLocked == false)
                {
                    MessageBoxDraw.ShowMsg("此质控品在激活情况下无法删除，请冻结后尝试删除！", MsgType.Warning);
                    return;
                }

                qcMaintainDic.Clear();
                qcMaintainDic.Add("DeleteQualityControl", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo) });
                SendToServices(qcMaintainDic);
            }
        }
        /// <summary>
        /// 删除项目对应的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SbutDeleteProject_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定要删除关联的项目吗?", "删除确认", messButton);
            if (dr != DialogResult.OK)
            {
                return;
            }
            QCRelationProjectInfo qcProjectInfo = new QCRelationProjectInfo();
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                bool b = this.gridView1.GetRowCellValue(selectedHandle, "冻结").ToString() == "是" ? true : false;
                qcProjectInfo.QCID = Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "质控品ID").ToString());
                if (b == false)
                {
                    MessageBoxDraw.ShowMsg("此质控品在激活情况下无法删除，请冻结后尝试删除！", MsgType.Warning);
                    return;
                }
                if (this.gridView2.GetSelectedRows().Count() > 0)
                {
                    int selectGridview2Handle;
                    selectGridview2Handle = this.gridView2.GetSelectedRows()[0];
                    qcProjectInfo.ProjectName = this.gridView2.GetRowCellValue(selectGridview2Handle, "项目名称").ToString();
                }
                int count = new QCMaintian().DeleteQCProjectInfo("DeleteQCProjectInfo", qcProjectInfo);
                if (count > 0)
                {
                    lstQCRelationProjectInfo.RemoveAll(x => x.QCID == qcProjectInfo.QCID && x.ProjectName == qcProjectInfo.ProjectName);
                    lstvQCInfo_Click(null,null);
                    MessageBoxDraw.ShowMsg("删除成功！", MsgType.OK);
                }
                else
                    MessageBoxDraw.ShowMsg("该项目已下任务,不能删除",MsgType.Warning);

            }
        }
    }
}
