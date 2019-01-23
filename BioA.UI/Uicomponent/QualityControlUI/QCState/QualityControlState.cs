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
    public partial class QualityControlState : DevExpress.XtraEditors.XtraUserControl
    {
        frmEditQCResult frmEditQCRes;
        ReactionProcessQC reactionProcessQC;
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> qcStateDic = new Dictionary<string, object[]>();

        /// <summary>
        /// 保存质控任务状态信息 
        /// </summary>
        DataTable dt = new DataTable();

        /// <summary>
        /// 所有质控项目信息
        /// </summary>
        List<QCRelationProjectInfo> lstQCRelationProjects = null;
        /// <summary>
        /// 所有质控信息
        /// </summary>
        List<QualityControlInfo> lstQCInfo = null;
        public QualityControlState()
        {
            InitializeComponent();
            
        }

        private void btnReactionProcess_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                QCResultForUIInfo qcResInfoFirst = new QCResultForUIInfo();

                DataRow dr = this.gridView1.GetDataRow(this.gridView1.GetSelectedRows()[0]);
                qcResInfoFirst.QCName = (string)dr.ItemArray[0];
                qcResInfoFirst.ProjectName = (string)dr.ItemArray[1];
                qcResInfoFirst.SampleType = (string)dr.ItemArray[2];
                qcResInfoFirst.LotNum = (string)dr.ItemArray[3];
                qcResInfoFirst.Pos = (string)dr.ItemArray[4];
                qcResInfoFirst.HorizonLevel = (string)dr.ItemArray[5];
                qcResInfoFirst.TargetMean = (float)(System.Convert.ToDouble(dr.ItemArray[6]));
                qcResInfoFirst.TargetSD = (float)(System.Convert.ToDouble(dr.ItemArray[7]));
                qcResInfoFirst.ConcResult = (float)(System.Convert.ToDouble(dr.ItemArray[8]));
                string datetime = dr.ItemArray[9].ToString();
                qcResInfoFirst.Manufacturer = (string)dr.ItemArray[10];

                qcResInfoFirst.QCTimeStartTS = System.Convert.ToDateTime(dtpQCStartTime.Text);
                qcResInfoFirst.QCTimeEndTS = System.Convert.ToDateTime(dtpQCEndTime.Text).AddDays(1);
                reactionProcessQC.QCResInfo = qcResInfoFirst;
                reactionProcessQC.StrDateTime = datetime;

                reactionProcessQC.StartPosition = FormStartPosition.CenterScreen;
                reactionProcessQC.ShowDialog();
            }
        }
        /// <summary>
        /// 重复性点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRepeat_Click(object sender, EventArgs e)
        {
            List<float> lstConcResults = new List<float>();
            QCResultForUIInfo qcResInfoFirst = new QCResultForUIInfo();
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                DataRow dr = this.gridView1.GetDataRow(this.gridView1.GetSelectedRows()[0]);
                qcResInfoFirst.QCName = (string)dr.ItemArray[0];
                qcResInfoFirst.ProjectName = (string)dr.ItemArray[1];
                qcResInfoFirst.SampleType = (string)dr.ItemArray[2];
                qcResInfoFirst.LotNum = (string)dr.ItemArray[3];
                qcResInfoFirst.Pos = (string)dr.ItemArray[4];
                qcResInfoFirst.HorizonLevel = (string)dr.ItemArray[5];
                qcResInfoFirst.TargetMean = (float)(System.Convert.ToDouble(dr.ItemArray[6]));
                qcResInfoFirst.TargetSD = (float)(System.Convert.ToDouble(dr.ItemArray[7]));
                qcResInfoFirst.ConcResult = (float)(System.Convert.ToDouble(dr.ItemArray[8]));
                qcResInfoFirst.SampleCreateTime = (DateTime)(System.Convert.ToDateTime(dr.ItemArray[9]));
                qcResInfoFirst.Manufacturer = (string)dr.ItemArray[10];
            }

            foreach (int i in this.gridView1.GetSelectedRows())
            {
                DataRow dr = this.gridView1.GetDataRow(i);
                
                if (qcResInfoFirst.ProjectName == (string)dr.ItemArray[1] ||
                    qcResInfoFirst.QCName == (string)dr.ItemArray[0]      ||
                    qcResInfoFirst.SampleType == (string)dr.ItemArray[2]  ||
                    qcResInfoFirst.LotNum == (string)dr.ItemArray[3]      ||
                    qcResInfoFirst.Manufacturer == (string)dr.ItemArray[10])
                {
                    lstConcResults.Add((float)(System.Convert.ToDouble(dr.ItemArray[8])));
                }
            }




            frmRepeat frmRepeat = new frmRepeat(qcResInfoFirst, lstConcResults);

            frmRepeat.StartPosition = FormStartPosition.CenterParent;
            frmRepeat.ShowDialog();
        }

        private void QualityControlState_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadQualityControlState));
        }

        private void loadQualityControlState()
        {
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;

            dt.Columns.Add("质控品名称");
            dt.Columns.Add("项目名称");
            dt.Columns.Add("样本类型");
            dt.Columns.Add("批号");
            dt.Columns.Add("位置");
            dt.Columns.Add("水平浓度");
            dt.Columns.Add("理论平均值");
            dt.Columns.Add("理论标准差");
            dt.Columns.Add("浓度");
            dt.Columns.Add("质控时间");
            dt.Columns.Add("生产厂家");
            this.lstQCResult.DataSource = dt;
            this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[3].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[4].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[5].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[6].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[7].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[8].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[9].OptionsColumn.AllowEdit = false;
            this.gridView1.Columns[10].OptionsColumn.AllowEdit = false;
            frmEditQCRes = new frmEditQCResult();
            reactionProcessQC = new ReactionProcessQC();
            
            QCResultForUIInfo qcResultForUI = new QCResultForUIInfo();
            qcResultForUI.QCTimeStartTS = DateTime.Now.Date;
            qcResultForUI.QCTimeEndTS = DateTime.Now.Date.AddDays(1);
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCResult, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCResultInfo", XmlUtility.Serializer(typeof(QCResultForUIInfo), qcResultForUI))));
            qcStateDic.Clear();
            qcStateDic.Add("QueryQCResultInfo", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), qcResultForUI) });
            qcStateDic.Add("QueryProjectName", null);//获取项目名称
            qcStateDic.Add("GetsQCRelationProInfo", null);//获取项目、质控品关联信息
            qcStateDic.Add("QueryQCAllInfo", null);//获取所有质控品信息
            ClientSendToServices(qcStateDic);
        }

        /// <summary>
        /// 发送信息给服务器
        /// </summary>
        /// <param name="param"></param>
        private void ClientSendToServices(Dictionary<string, object[]> param)
        {
            var qcStateThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.QCResult, param);
            });
            qcStateThread.IsBackground = true;
            qcStateThread.Start();
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
                case "QueryQCResultInfo":
                    InitialQCResultInfos(XmlUtility.Deserialize(typeof(List<QCResultForUIInfo>), sender as string) as List<QCResultForUIInfo>);
                    break;
                case "QueryQCInfosForAddQCResult":
                    frmEditQCRes.QCInfos = (List<QualityControlInfo>)XmlUtility.Deserialize(typeof(List<QualityControlInfo>), sender as string);
                    break;
                case "QueryProjectName":
                    List<string> lstProjectNames = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    this.Invoke(new EventHandler(delegate { txtProjectName.Properties.Items.AddRange(lstProjectNames); }));
                    break;
                case "GetsQCRelationProInfo":
                    lstQCRelationProjects = (List<QCRelationProjectInfo>)XmlUtility.Deserialize(typeof(List<QCRelationProjectInfo>), sender as string);
                    break;
                case "QueryQCAllInfo":
                    lstQCInfo = (List<QualityControlInfo>)XmlUtility.Deserialize(typeof(List<QualityControlInfo>), sender as string);
                    break;
                case "EditQCResultForManual":
                    if ((sender as string) == "添加成功！")
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            this.frmEditQCRes.Close();
                        }));

                        //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCResult, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCResultInfo", XmlUtility.Serializer(typeof(QCResultForUIInfo), new QCResultForUIInfo() { QCTimeStartTS = dtpQCStartTime.Value, QCTimeEndTS = dtpQCEndTime.Value }))));
                        qcStateDic.Clear();
                        qcStateDic.Add("QueryQCResultInfo", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), new QCResultForUIInfo() { QCTimeStartTS = dtpQCStartTime.Value, QCTimeEndTS = dtpQCEndTime.Value })});
                        ClientSendToServices(qcStateDic);
                    }
                    break;
                case "AddQCResultForManual":
                    if ((sender as string) == "添加成功！")
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            this.frmEditQCRes.Close();
                        }));

                        //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCResult, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCResultInfo", XmlUtility.Serializer(typeof(QCResultForUIInfo), new QCResultForUIInfo() { QCTimeStartTS = dtpQCStartTime.Value, QCTimeEndTS = dtpQCEndTime.Value }))));
                        qcStateDic.Clear();
                        qcStateDic.Add("QueryQCResultInfo", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), new QCResultForUIInfo() { QCTimeStartTS = dtpQCStartTime.Value, QCTimeEndTS = dtpQCEndTime.Value })});
                        ClientSendToServices(qcStateDic);
                    }
                    else
                    {
                        this.frmEditQCRes.StrReceiveInfo = sender as string;
                    }
                    break;
                case "DeleteQCResult":
                    if ((sender as string) == "删除成功！")
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            this.frmEditQCRes.Close();
                        }));
                        //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCResult, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCResultInfo", XmlUtility.Serializer(typeof(QCResultForUIInfo), new QCResultForUIInfo()))));
                        qcStateDic.Clear();
                        qcStateDic.Add("QueryQCResultInfo", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), new QCResultForUIInfo())});
                        ClientSendToServices(qcStateDic);
                    }
                    else
                    {
                        this.frmEditQCRes.StrReceiveInfo = "删除失败！";
                    }
                    break;
                case "QueryTimeCourseByQCInfo":
                    TimeCourseInfo qcTimeCourse = (TimeCourseInfo)XmlUtility.Deserialize(typeof(TimeCourseInfo), sender as string);
                    reactionProcessQC.QCReactionInfo = qcTimeCourse;
                    
                    break;
                default:
                    break;
            }
        }

        private void InitialQCResultInfos(List<QCResultForUIInfo> lstQCResultInfos)
        {
            this.Invoke(new EventHandler(delegate {
                this.lstQCResult.RefreshDataSource();
                dt.Rows.Clear();
                foreach (QCResultForUIInfo qCResInfo in lstQCResultInfos)
                {
                    dt.Rows.Add(new object[] { qCResInfo.QCName, qCResInfo.ProjectName, qCResInfo.SampleType, qCResInfo.LotNum, qCResInfo.Pos, qCResInfo.HorizonLevel,
                                               qCResInfo.TargetMean, qCResInfo.TargetSD, qCResInfo.ConcResult, qCResInfo.SampleCreateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), qCResInfo.Manufacturer });
                }
            }));
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            QCResultForUIInfo qcResInfo = new QCResultForUIInfo();           
            if (txtQCName.Text != "")
            {
                qcResInfo.QCName = txtQCName.SelectedItem.ToString();
            }
            if (txtProjectName.Text != "")
            {
                qcResInfo.ProjectName = txtProjectName.SelectedItem.ToString();
            }
            qcResInfo.LotNum = txtLotNum.Text;
            qcResInfo.QCTimeStartTS = System.Convert.ToDateTime(dtpQCStartTime.Text);
            qcResInfo.QCTimeEndTS = System.Convert.ToDateTime(dtpQCEndTime.Text).AddDays(1);
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.QCResult, XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryQCResultInfo", XmlUtility.Serializer(typeof(QCResultForUIInfo), qcResInfo))));
            qcStateDic.Clear();
            qcStateDic.Add("QueryQCResultInfo", new object[] { XmlUtility.Serializer(typeof(QCResultForUIInfo), qcResInfo) });
            ClientSendToServices(qcStateDic);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                QCResultForUIInfo qcResInfoFirst = new QCResultForUIInfo();

                DataRow dr = this.gridView1.GetDataRow(this.gridView1.GetSelectedRows()[0]);
                qcResInfoFirst.QCName = (string)dr.ItemArray[0];
                qcResInfoFirst.ProjectName = (string)dr.ItemArray[1];
                qcResInfoFirst.SampleType = (string)dr.ItemArray[2];
                qcResInfoFirst.LotNum = (string)dr.ItemArray[3];
                qcResInfoFirst.Pos = (string)dr.ItemArray[4];
                qcResInfoFirst.HorizonLevel = (string)dr.ItemArray[5];
                qcResInfoFirst.TargetMean = (float)(System.Convert.ToDouble(dr.ItemArray[6]));
                qcResInfoFirst.TargetSD = (float)(System.Convert.ToDouble(dr.ItemArray[7]));
                qcResInfoFirst.ConcResult = (float)(System.Convert.ToDouble(dr.ItemArray[8]));
                qcResInfoFirst.SampleCreateTime = (DateTime)(System.Convert.ToDateTime(dr.ItemArray[9]));
                qcResInfoFirst.Manufacturer = (string)dr.ItemArray[10];
                
                frmEditQCRes.QCResInfo = qcResInfoFirst;

                frmEditQCRes.StartPosition = FormStartPosition.CenterParent;
                frmEditQCRes.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择至少一条记录！");
                return;
            }
        }
        /// <summary>
        /// 列表序列号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
        /// <summary>
        /// 项目下拉框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> qcId = new List<int>();
            foreach (var item in lstQCRelationProjects)
            {
                if (item.ProjectName == txtProjectName.Text)
                {
                    qcId.Add(item.QCID);
                }
            }
            if (qcId != null && qcId.Count > 0)
            {
                txtQCName.Properties.Items.Clear();
                txtQCName.SelectedIndex = -1;
                foreach (var qcInfo in lstQCInfo)
                {
                    if (qcId.Exists(x => x == qcInfo.QCID))
                    {
                        txtQCName.Properties.Items.Add(qcInfo.QCName);
                    }
                }
                txtQCName.SelectedIndex = 0;
            }
            else
            {
                txtQCName.Properties.Items.Clear();
                txtQCName.Text = "";
                txtLotNum.Text = "";
            }
        }
        /// <summary>
        /// 质控品下拉框改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var qualityControls = lstQCInfo.Find(x => x.QCName == txtQCName.Text);
            QCRelationProjectInfo qcRelationProject = lstQCRelationProjects.Find(x => x.QCID == qualityControls.QCID && x.ProjectName == txtProjectName.Text);
            if (qcRelationProject != null)
            {
                txtLotNum.Text = qualityControls.LotNum;
            }
        }
    }
}
