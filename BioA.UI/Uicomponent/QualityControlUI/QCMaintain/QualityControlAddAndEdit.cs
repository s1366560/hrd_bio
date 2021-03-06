﻿using System;
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
using DevExpress.XtraEditors.Repository;
using System.Text.RegularExpressions;
using BioA.Common.IO;
using System.Threading;

namespace BioA.UI
{
    public partial class QualityControlAddAndEdit : DevExpress.XtraEditors.XtraForm
    {
        //声明一个委托
        public delegate void TransmitQCInfoAndTestProjectInfo(string result, Dictionary<QualityControlInfo, List<QCRelationProjectInfo>> keyValuePairs);
        //声明一个事件
        public event TransmitQCInfoAndTestProjectInfo TransmitQCAndTestProjectInfoEvent;


        /// <summary>
        /// 客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> qcAddDic = new Dictionary<string, object[]>();

        private List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 新增界面项目信息
        /// </summary>
        public List<AssayProjectInfo> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                try
                {
                    //this.Invoke(new EventHandler(delegate
                    //    {
                    lstvQCMaintainInfos.RefreshDataSource();
                    dataTable.Rows.Clear();
                    List<int> lstSelected = new List<int>();

                    int i = 1;
                    //gridControl1
                    if (lstAssayProInfos.Count != 0)
                    {
                        foreach (AssayProjectInfo assayProInfo in lstAssayProInfos)
                        {
                            object[] obj = new object[5] { i, assayProInfo.ProjectName, assayProInfo.SampleType, null, null };

                            if (this.Text == "编辑质控品")
                                foreach (QCRelationProjectInfo qcProInfo in qCRelateProInfo)
                                {
                                    if (assayProInfo.ProjectName == qcProInfo.ProjectName && assayProInfo.SampleType == qcProInfo.SampleType)
                                    {
                                        obj[3] = qcProInfo.TargetMean;
                                        obj[4] = qcProInfo.TargetSD;
                                        lstSelected.Add(i);
                                    }
                                }

                            dataTable.Rows.Add(obj);
                            i++;
                        }
                    }

                    this.gridView1.ClearSelection();

                    foreach (int selected in lstSelected)
                    {
                        this.gridView1.SelectRow(selected - 1);
                    }
                    //}));
                }
                catch (Exception e)
                {

                }
            }
        }

        private QualityControlInfo qCOldInfo = new QualityControlInfo();
        /// <summary>
        /// 老的质控品信息
        /// </summary>
        public QualityControlInfo QCOldInfo
        {
            get { return qCOldInfo; }
            set
            {
                qCOldInfo = value;
                txtQCName.Text = qCOldInfo.QCName;
                combLevelConc.SelectedItem = qCOldInfo.HorizonLevel;
                txtLotNum.Text = qCOldInfo.LotNum;
                cboPosition.Text = qCOldInfo.Pos;//ly
                //cboPosition.SelectedItem = qCOldInfo.Pos;
                txtManufacturer.Text = qCOldInfo.Manufacturer;
                dtpInvalidDate.Text = qCOldInfo.InvalidDate.ToShortDateString();
            }
        }

        private List<QualityControlInfo> _ListQualityControlInfo = null;
        /// <summary>
        /// 所有的质控品信息
        /// </summary>
        public List<QualityControlInfo> ListQualityControlInfo
        {
            get { return _ListQualityControlInfo; }
            set
            {
                _ListQualityControlInfo = value;
                cboPosition.Properties.Items.Clear();
                foreach (var item in RunConfigureUtility.QCPosition)
                {
                    if (!_ListQualityControlInfo.Exists(x => x.Pos == item))
                    {
                        cboPosition.Properties.Items.Add(item);
                    }
                }
            }
        }


        private List<QCRelationProjectInfo> qCRelateProInfo = new List<QCRelationProjectInfo>();
        /// <summary>
        /// 质控品关联项目信息
        /// </summary>
        public List<QCRelationProjectInfo> QCRelateProInfo
        {
            get { return qCRelateProInfo; }
            set { qCRelateProInfo = value; }
        }

        private string strReturnInfo = string.Empty;
        /// <summary>
        /// 添加或编辑返回信息
        /// </summary>
        public string StrReturnInfo
        {
            set
            {
                strReturnInfo = value;
                if (strReturnInfo == "该质控品或项目已在任务列表中，不能对其进行修改！")
                {
                    MessageBox.Show("该质控品或项目已在任务列表中，不能对其进行修改！");
                    return;
                }
                else if (strReturnInfo == "质控品和项目信息修改失败！")
                {
                    MessageBox.Show("质控品和项目信息修改失败！");
                    return;
                }
                else if (strReturnInfo == "质控品名称已存在！")
                {
                    MessageBox.Show("质控品名称已存在！");
                    return;
                }
                else if (strReturnInfo == "该质控品已存在，无法添加！")
                {
                    MessageBox.Show("该质控品已存在，无法添加！");
                    return;
                }
                else if (strReturnInfo == "质控品添加失败，请联系管理员！")
                {
                    MessageBox.Show("质控品添加失败，请联系管理员！");
                    return;
                }
                else
                {
                    if (this.Text == "新增质控品")
                    {
                        if (strReturnInfo.Contains("已成功添加质控品信息！"))
                        {
                            string qid = strReturnInfo.Substring(11);
                            qcInfo.QCID = int.Parse(qid);
                            List<QCRelationProjectInfo> qcpro = new List<QCRelationProjectInfo>();
                            foreach (QCRelationProjectInfo r in lstQCRelationProInfo)
                            {
                                r.QCID = qcInfo.QCID;
                                qcpro.Add(r);
                            }
                            Dictionary<QualityControlInfo, List<QCRelationProjectInfo>> keyValuePairs = new Dictionary<QualityControlInfo, List<QCRelationProjectInfo>>();
                            keyValuePairs.Add(qcInfo, qcpro);
                            strReturnInfo = this.Text;
                            if (TransmitQCAndTestProjectInfoEvent != null)
                            {
                                TransmitQCAndTestProjectInfoEvent(strReturnInfo, keyValuePairs);
                            }
                        }
                    }
                    else
                    {
                        if (strReturnInfo == "质控品和项目信息修改成功！")
                        {
                            Dictionary<QualityControlInfo, List<QCRelationProjectInfo>> keyValuePairs = new Dictionary<QualityControlInfo, List<QCRelationProjectInfo>>();
                            keyValuePairs.Add(qcInfo, lstQCRelationProInfo);
                            strReturnInfo = this.Text;
                            if (TransmitQCAndTestProjectInfoEvent != null)
                            {
                                TransmitQCAndTestProjectInfoEvent(strReturnInfo, keyValuePairs);
                            }
                        }

                    }
                }
            }
        }

        private List<string> lstPosition = new List<string>();
        /// <summary>
        /// 已经使用了的质控位置
        /// </summary>
        public List<string> LstPosition
        {
            get { return lstPosition; }
            set
            {
                lstPosition = value;

                List<string> lstAllPosition = RunConfigureUtility.QCPosition;
                foreach (string str in lstPosition)
                {
                    lstAllPosition.Remove(str);
                }
                this.Invoke(new EventHandler(delegate
                {
                    cboPosition.Properties.Items.AddRange(lstAllPosition);
                    cboPosition.SelectedIndex = 0;
                }));
            }
        }

        DataTable dataTable = new DataTable();
        public QualityControlAddAndEdit()
        {
            InitializeComponent();
            this.ControlBox = false;
            dataTable.Columns.Add("序号");
            dataTable.Columns.Add("项目名称");
            dataTable.Columns.Add("样本类型");
            dataTable.Columns.Add("靶值");
            dataTable.Columns.Add("SD");

            lstvQCMaintainInfos.DataSource = dataTable;

            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[0].Width = 50;
            this.gridView1.Columns[1].Width = 200;
            this.gridView1.Columns[2].Width = 200;
            this.gridView1.Columns[3].Width = 100;
            this.gridView1.Columns[4].Width = 100;
            //获取配置文件中质控品水平浓度
            combLevelConc.Properties.Items.AddRange(RunConfigureUtility.QCLevelConc);

            //获取质控品位置
            //cboPosition.Properties.Items.AddRange(RunConfigureUtility.QCPosition);


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 存储质控品信息(新)
        /// </summary>
        QualityControlInfo qcInfo = new QualityControlInfo();
        /// <summary>
        /// 存储一个质控品对应的一个或多个检测项目信息(新)
        /// </summary>
        List<QCRelationProjectInfo> lstQCRelationProInfo = new List<QCRelationProjectInfo>();
        /// <summary>
        /// 质控品信息保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            lstQCRelationProInfo.Clear();
            if (txtQCName.Text.Trim() == null || txtQCName.Text.Trim() == string.Empty)
            {
                MessageBox.Show("质控品名称不能为空，请填写质控品名称！");
                return;
            }

            if (txtLotNum.Text.Trim() == null || txtLotNum.Text.Trim() == string.Empty)
            {
                MessageBox.Show("质控品批号不能为空，请填写质控品批号！");
                return;
            }

            if (txtManufacturer.Text.Trim() == null || txtManufacturer.Text.Trim() == string.Empty)
            {
                MessageBox.Show("质控品生产厂家不能为空，请填写生产厂家！");
                return;
            }
            if (string.IsNullOrEmpty(cboPosition.Text))//ly
            {
                MessageBox.Show("质控品位置不能为空！");
                return;
            }
            if (this.gridView1.SelectedRowsCount != 0)
            {
                foreach (int i in this.gridView1.GetSelectedRows())
                {
                    DataRow dr = this.gridView1.GetDataRow(i);

                    try
                    {
                        if (!Regex.IsMatch(((string)dr.ItemArray[3]).Trim(), @"^(-?\d+)(\.\d+)?$") ||
                            !Regex.IsMatch(((string)dr.ItemArray[4]).Trim(), @"^(-?\d+)(\.\d+)?$"))
                        {
                            MessageBox.Show("质控品对应生化项目录入数据有误，请检查并修改！");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("质控品对应生化项目录入数据有误，请检查并修改！");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择对应的生化项目！");
                return;
            }

            qcInfo.QCID = qCOldInfo.QCID;
            qcInfo.QCName = txtQCName.Text.Trim();
            qcInfo.Pos = cboPosition.SelectedItem.ToString();
            qcInfo.LotNum = txtLotNum.Text.Trim();
            qcInfo.InvalidDate = Convert.ToDateTime(dtpInvalidDate.Text);
            qcInfo.HorizonLevel = combLevelConc.SelectedItem as string;
            qcInfo.Manufacturer = txtManufacturer.Text.Trim();
            lstQCRelationProInfo.Clear();
            int count = gridView1.RowCount;
            for (int i = 0; i < count; i++)
            {
                QCRelationProjectInfo QcRelationProjectinfo = new QCRelationProjectInfo();
                if (gridView1.GetRowCellValue(i, "靶值").ToString() != "" && gridView1.GetRowCellValue(i, "SD").ToString() != "")
                {

                    QcRelationProjectinfo.ProjectName = this.gridView1.GetRowCellValue(i, "项目名称").ToString();
                    QcRelationProjectinfo.SampleType = this.gridView1.GetRowCellValue(i, "样本类型").ToString();
                    QcRelationProjectinfo.TargetMean = float.Parse(this.gridView1.GetRowCellValue(i, "靶值").ToString());
                    QcRelationProjectinfo.TargetSD = float.Parse(this.gridView1.GetRowCellValue(i, "SD").ToString());
                    QcRelationProjectinfo.Target2SD = QcRelationProjectinfo.TargetSD * 2;
                    QcRelationProjectinfo.Target3SD = QcRelationProjectinfo.TargetSD * 3;
                    QcRelationProjectinfo.QCID = qCOldInfo.QCID;
                    lstQCRelationProInfo.Add(QcRelationProjectinfo);
                }
            }
            if (strReturnInfo == "编辑中")
            {
                return;
            }
            strReturnInfo = "编辑中";
            if (this.Text == "新增质控品")
            {
                qcAddDic.Clear();
                qcAddDic.Add("AddQualityControl", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), XmlUtility.Serializer(typeof(List<QCRelationProjectInfo>), lstQCRelationProInfo) });
                SendToServices(qcAddDic);
            }
            else
            {
                if (lstQCRelationProInfo.Count < QCRelateProInfo.Count)
                {
                    MessageBox.Show("不能将原靶值或SD设置为空！");
                    return;
                }
                qcAddDic.Clear();
                qcAddDic.Add("EditQualityControl", new object[] { XmlUtility.Serializer(typeof(QualityControlInfo), qCOldInfo),
                    XmlUtility.Serializer(typeof(QualityControlInfo), qcInfo), XmlUtility.Serializer(typeof(List<QCRelationProjectInfo>), lstQCRelationProInfo),
                XmlUtility.Serializer(typeof(List<QCRelationProjectInfo>), QCRelateProInfo)});

                SendToServices(qcAddDic);
            }


        }
        /// <summary>
        /// 发送信息给服务器
        /// </summary>
        /// <param name="param"></param>
        private void SendToServices(Dictionary<string, object[]> param)
        {
            var qcAddThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.QCMaintain, param);
            });
            qcAddThread.IsBackground = true;
            qcAddThread.Start();
        }

        public void QualityControlAddAndEdit_Load(object sender, EventArgs e)
        {
            this.qcAddDic.Clear();
            this.loadQualityControlAddAndEdit();

        }

        private void loadQualityControlAddAndEdit()
        {
            if (this.Text == "新增质控品")
            {
                txtQCName.Text = "";
                combLevelConc.SelectedIndex = 1;
                txtLotNum.Text = "";
                cboPosition.SelectedIndex = 0;
                txtManufacturer.Text = "";
                dtpInvalidDate.Value = DateTime.Now;
            }
        }
    }
}