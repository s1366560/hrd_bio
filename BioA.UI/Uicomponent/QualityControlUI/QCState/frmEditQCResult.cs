﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using BioA.Common.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace BioA.UI
{
    public partial class frmEditQCResult : Form
    {
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> frmEditQCResultDic = new Dictionary<string, object[]>();
        private enum EditModel
        {
            Edit = 0,
            Add = 1,
            Delete = 2
        }

        private EditModel editModel;

        public frmEditQCResult()
        {
            InitializeComponent();

            editModel = EditModel.Edit;
            cboQCName.Properties.ReadOnly = true;
            cboSampleType.Properties.ReadOnly = true;
            cboProjectName.Properties.ReadOnly = true;
            cboLotNum.Properties.ReadOnly = true;
            cboPosition.Properties.ReadOnly = true;
            cboHorizonLevel.Properties.ReadOnly = true;
            txtTargetMean.Properties.ReadOnly = true;
            txtTargetSD.Properties.ReadOnly = true;
            txtConcResult.Properties.ReadOnly = false;
            dtpQCStartTime.Enabled = false;
            cboManufacturer.Properties.ReadOnly = true;
        }

        private QCResultForUIInfo qCResInfo = new QCResultForUIInfo();

        public QCResultForUIInfo QCResInfo
        {
            get { return qCResInfo; }
            set 
            {
                qCResInfo = value;
            }
        }

        private List<QualityControlInfo> qCInfos = new List<QualityControlInfo>();

        public List<QualityControlInfo> QCInfos
        {
            get { return qCInfos; }
            set
            {
                qCInfos = value;

                //cboQCName.Properties.Items.ad
                this.Invoke(new EventHandler(delegate 
                    {
                        cboQCName.Properties.Items.Clear();
                        cboLotNum.Properties.Items.Clear();
                        cboManufacturer.Properties.Items.Clear();
                        foreach (QualityControlInfo qcInfo in qCInfos)
                        {
                            if (!cboQCName.Properties.Items.Contains(qcInfo.QCName))
                                cboQCName.Properties.Items.Add(qcInfo.QCName);
                            if (!cboLotNum.Properties.Items.Contains(qcInfo.LotNum))
                                cboLotNum.Properties.Items.Add(qcInfo.LotNum);
                            if (!cboManufacturer.Properties.Items.Contains(qcInfo.Manufacturer))
                                cboManufacturer.Properties.Items.Add(qcInfo.Manufacturer);
                        }
                    }));
            }
        }

        private List<string> lstProjectName = new List<string>();

        public List<string> LstProjectName
        {
            get { return lstProjectName; }
            set
            {
                lstProjectName = value;
                this.Invoke(new EventHandler(delegate
                    {
                        cboProjectName.Properties.Items.AddRange(lstProjectName.ToArray());
                    }));
                
            }
        }

        private string strReceiveInfo = string.Empty;

        public string StrReceiveInfo
        {
            get { return strReceiveInfo; }
            set 
            { 
                strReceiveInfo = value; 
                this.Invoke(new EventHandler(delegate
                    {
                        MessageBox.Show(strReceiveInfo);    
                    }));
                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            editModel = EditModel.Add;
            frmEditQCResultDic.Clear();
            //获取质控品信息和质控结果信息
            frmEditQCResultDic.Add("QueryQCInfosForAddQCResult", null);
            //获取所有项目名称
            frmEditQCResultDic.Add("QueryProjectName", null);
            SendInfoToService(frmEditQCResultDic);

            cboQCName.Properties.ReadOnly = false;
            cboSampleType.Properties.ReadOnly = false;
            cboProjectName.Properties.ReadOnly = false;
            cboLotNum.Properties.ReadOnly = false;
            cboPosition.Properties.ReadOnly = false;
            cboHorizonLevel.Properties.ReadOnly = false;
            txtTargetMean.Properties.ReadOnly = true;
            txtTargetSD.Properties.ReadOnly = true;
            txtConcResult.Properties.ReadOnly = false;
            dtpQCStartTime.Enabled = true;
            cboManufacturer.Properties.ReadOnly = false;


            cboQCName.Text = "请选择";
            cboSampleType.Text = "请选择";
            cboProjectName.Text = "请选择";
            cboLotNum.Text = "请选择";
            cboPosition.Text = "请选择";
            cboHorizonLevel.Text = "请选择";
            txtTargetMean.Text = "";
            txtTargetSD.Text = "";
            txtConcResult.Text = "";
            dtpQCStartTime.Text = "";
            cboManufacturer.Text = "请选择";

            cboSampleType.Properties.Items.Clear();
            cboHorizonLevel.Properties.Items.Clear();
            cboSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);
            cboPosition.Properties.Items.AddRange(RunConfigureUtility.QCPosition);
            cboHorizonLevel.Properties.Items.AddRange(RunConfigureUtility.QCLevelConc);
        }

        /// <summary>
        /// 发送信息给服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendInfoToService(Dictionary<string, object[]> sender)
        {
            var frmEditQCThread = new Thread (() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.QCResult, sender);
            });
            frmEditQCThread.IsBackground = true;
            frmEditQCThread.Start();
        }

        public void frmEditQCResult_Load(object sender, EventArgs e)
        {
            this.loadFrmEditQCResult();
        }

        private void loadFrmEditQCResult()
        {
            cboQCName.Text = qCResInfo.QCName;
            cboSampleType.Text = qCResInfo.SampleType;
            cboProjectName.Text = qCResInfo.ProjectName;
            cboLotNum.Text = qCResInfo.LotNum;
            cboPosition.Text = qCResInfo.Pos;
            cboHorizonLevel.Text = qCResInfo.HorizonLevel;
            txtTargetMean.Text = qCResInfo.TargetMean.ToString();
            txtTargetSD.Text = qCResInfo.TargetSD.ToString();
            txtConcResult.Text = qCResInfo.ConcResult.ToString();
            dtpQCStartTime.Value = qCResInfo.SampleCreateTime;
            cboManufacturer.Text = qCResInfo.Manufacturer;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboQCName.SelectedItem.ToString() == "请选择" || 
                cboSampleType.SelectedItem.ToString() == "请选择" ||
                cboProjectName.SelectedItem.ToString() == "请选择" || 
                cboPosition.SelectedItem.ToString() == "请选择" || 
                cboHorizonLevel.SelectedItem.ToString() == "请选择" || 
                cboManufacturer.SelectedItem.ToString() == "请选择" ||
                !Regex.IsMatch(txtConcResult.Text.Trim(), @"^\d+(\.\d+)?$"))
            {
                MessageBox.Show("请正确填写信息！");
                return;
            }
            
            if (!Regex.IsMatch(txtConcResult.Text.Trim(), @"^\d+(\.\d+)?$"))
            {
                MessageBox.Show("请正确填写浓度值！");
                return;
            }

            if (dtpQCStartTime.Value > DateTime.Now)
            {
                MessageBox.Show("请选择小于当前时间的质控时间！");
                return;
            }
            
            QCResultForUIInfo qcResEditOrAdd = new QCResultForUIInfo();
            qcResEditOrAdd.QCName = cboQCName.SelectedItem.ToString();
            qcResEditOrAdd.SampleType = cboSampleType.SelectedItem.ToString();
            qcResEditOrAdd.ProjectName = cboProjectName.SelectedItem.ToString();
            qcResEditOrAdd.LotNum = cboLotNum.SelectedItem.ToString();
            qcResEditOrAdd.Pos = cboPosition.SelectedItem.ToString();
            qcResEditOrAdd.HorizonLevel = cboHorizonLevel.SelectedItem.ToString();
            qcResEditOrAdd.ConcResult = (float)System.Convert.ToDouble(txtConcResult.Text);
            qcResEditOrAdd.SampleCreateTime = dtpQCStartTime.Value;
            qcResEditOrAdd.Manufacturer = cboManufacturer.SelectedItem.ToString();

            switch (editModel)
            {
                case EditModel.Edit:
                    frmEditQCResultDic.Clear();
                    //修改质控结果信息
                    frmEditQCResultDic.Add("EditQCResultForManual", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), qCResInfo), XmlUtility.Serializer(typeof(QCResultForUIInfo), qcResEditOrAdd) });
                    SendInfoToService(frmEditQCResultDic);
                    break;
                case EditModel.Add:
                    frmEditQCResultDic.Clear();
                    //新增质控结果信息
                    frmEditQCResultDic.Add("AddQCResultForManual", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), qcResEditOrAdd) });
                    SendInfoToService(frmEditQCResultDic);
                    break;
                case EditModel.Delete:
                    break;
                default:
                    break;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (editModel == EditModel.Edit)
            {
                frmEditQCResultDic.Clear();
                //删除质控结果信息
                frmEditQCResultDic.Add("DeleteQCResult", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), qCResInfo) });
                SendInfoToService(frmEditQCResultDic);
            }
        }
    }
}
