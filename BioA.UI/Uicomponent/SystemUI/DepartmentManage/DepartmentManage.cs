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
using BioA.UI;
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using System.Threading;

namespace BioA.UI
{
    public partial class DepartmentManage : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> dePartManageDic = new Dictionary<string, object[]>();

        /// <summary>
        /// 申请医生数据表
        /// </summary>
        DataTable applyForADoctor = new DataTable();

        /// <summary>
        /// 审核医生数据表
        /// </summary>
        DataTable auditDoctor = new DataTable();

        /// <summary>
        /// 检验医生数据表
        /// </summary>
        DataTable checkoutDoctor = new DataTable();

        /// <summary>
        /// 申请科室数据表
        /// </summary>
        DataTable applicationSection = new DataTable();

        public DepartmentManage()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;
            gridView3.Appearance.HeaderPanel.Font = font;
            gridView3.Appearance.Row.Font = font;
            gridView4.Appearance.HeaderPanel.Font = font;
            gridView4.Appearance.Row.Font = font;

            auditDoctor.Columns.Add("编号");
            auditDoctor.Columns.Add("医生名称");
            grdAuditDoctor.DataSource = auditDoctor;

            applyForADoctor.Columns.Add("编号");
            applyForADoctor.Columns.Add("医生名称");
            applyForADoctor.Columns.Add("申请科室");
            grdApplyDoctor.DataSource = applicationSection;

            checkoutDoctor.Columns.Add("编号");
            checkoutDoctor.Columns.Add("医生名称");
            grdCheckoutDoctor.DataSource = checkoutDoctor;

            applicationSection.Columns.Add("编号");
            applicationSection.Columns.Add("科室名称");
            grdApplyDepartments.DataSource = applicationSection;
        }

        public void DataTransfer_Event(string strMethod, object sender)
        {
            
            switch (strMethod)
            {
                case "QueryDepartmentInfo":
                    List<string> lstQueryDepartmentInfo = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    DepartmentInfoAdd(lstQueryDepartmentInfo);
                    ComboBoxAdd(lstQueryDepartmentInfo);
                    break;
                case "AddDepartmentInfo":
                    string strAdd = Convert.ToString(sender);
                    if (strAdd == "科室创建成功！")
                    {
                        QueryDepartmentInfo();
                    }
                    if (strAdd == "科室创建失败，请联系管理员！")
                    {
                        MessageBoxDraw.ShowMsg("科室创建失败，请联系管理员！", MsgType.Warning);
                    }
                    if (strAdd == "该科室已存在，请重新录入。")
                    {
                        MessageBoxDraw.ShowMsg("该科室已存在，请重新录入。", MsgType.OK);
                    }
                    break;
                case "UpDataDepartment":

                    QueryDepartmentInfo();

                    break;
                case "DeleteDepartment":
                    string str = Convert.ToString(sender);
                    int count = Convert.ToInt32(str);
                    if (count>0)
                    {
                        MessageBoxDraw.ShowMsg("删除失败！请先删除该科室中的医师！", MsgType.Exception);
                    }
                    QueryDepartmentInfo();
                    break;
                case "QueryApplyDoctorInfo":
                    List<ApplyDoctorInfo> lstQueryApplyDoctorInfo = (List<ApplyDoctorInfo>)XmlUtility.Deserialize(typeof(List<ApplyDoctorInfo>), sender as string);
                    ApplyDoctorInfoAdd(lstQueryApplyDoctorInfo);
                   
                    break;
                case "AddApplyDoctorInfo":
                    MessageBoxDraw.ShowMsg(Convert.ToString(sender), MsgType.Exception);
                    QueryApplyDoctorInfo();
                    break;
                case "DeleteApplyDoctorInfo":

                    QueryApplyDoctorInfo();
                    break;
                case "UpdataApplyDoctorInfo":

                    QueryApplyDoctorInfo();

                    break;

                case "QueryUserInfo":
                    List<UserInfo> lstQueryAuditPhysician = (List<UserInfo>)XmlUtility.Deserialize(typeof(List<UserInfo>), sender as string);
                    ApplyAuditPhysician(lstQueryAuditPhysician);
                    comboBoxEdit2Add(lstQueryAuditPhysician);
                   
                    break;
                case "QueryAuditPhysician":
                    List<string> QueryAuditPhysician = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    QueryAuditPhysicianAdd(QueryAuditPhysician);
                   
                   
                    break;
                case "AddAuditPhysician":

                    QueryAuditPhysicianf();
                    break;
                case "UpdataAuditPhysician":

                    QueryAuditPhysicianf();
                    break;
                case "DeleteAuditPhysician":

                    QueryAuditPhysicianf();
                    break;
                
             
            }
        }
        /// <summary>
        /// 审核下拉列表
        /// </summary>
        /// <param name="QueryAuditPhysician"></param>
        private void comboBoxEdit2Add(List<UserInfo> QueryAuditPhysician)
        {
            this.Invoke(new EventHandler(delegate
            {
                this.comboBoxEdit2.Text = "请选择";
                this.comboBoxEdit2.Properties.Items.Clear();
                foreach (UserInfo str in QueryAuditPhysician)
                {
                    this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
                str.UserName });
                }
            }));
        }
        /// <summary>
        /// 审核医生
        /// </summary>
        /// <param name="QueryAuditPhysician"></param>
        private void QueryAuditPhysicianAdd(List<string> QueryAuditPhysician)
        {
            this.Invoke(new EventHandler(delegate
            {
                auditDoctor.Rows.Clear();
                
                int i = 1;
                if (QueryAuditPhysician.Count != 0)
                {
                    foreach (string AuditPhysician in QueryAuditPhysician)
                    {
                        auditDoctor.Rows.Add(new object[] { i, AuditPhysician });
                        i++;
                    }
                }
                this.grdAuditDoctor.DataSource = auditDoctor;
                this.gridView4.Columns[0].Width = 70;

            }));
        }
        /// <summary>
        /// 检验医生
        /// </summary>
        /// <param name="lstQueryAuditPhysician"></param>
        private void ApplyAuditPhysician(List<UserInfo> lstQueryAuditPhysician)
        {
            this.Invoke(new EventHandler(delegate
            {
                checkoutDoctor.Rows.Clear();
                
                int i = 1;

                if (lstQueryAuditPhysician.Count != 0)
                {
                    foreach (UserInfo QueryAuditPhysician in lstQueryAuditPhysician)
                    {
                        checkoutDoctor.Rows.Add(new object[] { i, QueryAuditPhysician.UserName });

                        i++;
                    }
                }
                this.grdCheckoutDoctor.DataSource = checkoutDoctor;
                this.gridView3.Columns[0].Width = 70;

            }));
        }
        /// <summary>
        /// 申请医生
        /// </summary>
        /// <param name="lstQueryApplyDoctorInfo"></param>
        private void ApplyDoctorInfoAdd(List<ApplyDoctorInfo> lstQueryApplyDoctorInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                applyForADoctor.Rows.Clear();

                int i = 1;
                if (lstQueryApplyDoctorInfo.Count != 0)
                {
                    foreach (ApplyDoctorInfo QueryApplyDoctorInfo in lstQueryApplyDoctorInfo)
                    {
                        applyForADoctor.Rows.Add(new object[] { i, QueryApplyDoctorInfo.Doctor, QueryApplyDoctorInfo.Department });

                        i++;
                    }
                }
                this.grdApplyDoctor.DataSource = applyForADoctor;
                this.gridView2.Columns[0].Width = 70;

            }));
        }
        /// <summary>
        /// 科室下拉列表
        /// </summary>
        /// <param name="lstQueryDepartmentInfo"></param>
        private void ComboBoxAdd(List<string> lstQueryDepartmentInfo)
        {
          
             this.Invoke(new EventHandler(delegate
            {
                this.comboBoxEdit1.Text = "请选择";
                this.comboBoxEdit1.Properties.Items.Clear();
                foreach (string str in lstQueryDepartmentInfo)
                {
                    this.comboBoxEdit1.Properties.Items.AddRange(new object[] {str });
            }
            }));
        }
        /// <summary>
        /// 申请科室
        /// </summary>
        /// <param name="lstQueryDepartmentInfo"></param>
        private void DepartmentInfoAdd(List<string> lstQueryDepartmentInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                applicationSection.Rows.Clear();
                int i = 1;
                if (lstQueryDepartmentInfo.Count != 0)
                {
                    foreach (string QueryDepartmentInfo in lstQueryDepartmentInfo)
                    {
                        applicationSection.Rows.Add(new object[] { i, QueryDepartmentInfo });

                        i++;
                    }
                }
                this.grdApplyDepartments.DataSource = applicationSection;
                this.gridView1.Columns[0].Width = 70;

            }));
        }
        

        
        private void DepartmentManageSend(Dictionary<string, object[]> sender)
        {
            var depManageThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SystemDepartmentManage, sender);
            });
            depManageThread.IsBackground = true;
            depManageThread.Start();
        }
        

        public void DepartmentManage_Load(object sender, EventArgs e)
        {
            this.DataLoad();
        }

        private void DataLoad()
        {
            dePartManageDic.Clear();
            //获取所有科室
            dePartManageDic.Add("QueryDepartmentInfo",null);
            //获取所有医生信息
            dePartManageDic.Add("QueryApplyDoctorInfo",null);
            //获取所有检验医生
            dePartManageDic.Add("QueryUserInfo", null);
            //获取所有审核医生信息
            dePartManageDic.Add("QueryAuditPhysician", null);
            DepartmentManageSend(dePartManageDic);
        }
        private void QueryDepartmentInfo()
        {
            dePartManageDic.Clear();
            dePartManageDic.Add("QueryDepartmentInfo",null);
            DepartmentManageSend(dePartManageDic);
        }
        private void QueryApplyDoctorInfo()
        {
            dePartManageDic.Clear();
            dePartManageDic.Add("QueryApplyDoctorInfo",null);
            DepartmentManageSend(dePartManageDic);
        }
        private void QueryAuditPhysicianf()
        {
            dePartManageDic.Clear();
            dePartManageDic.Add("QueryAuditPhysician", null);
            DepartmentManageSend(dePartManageDic);
        }

        /// <summary>
        /// 申请科室（添加）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSPAdd_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim() != "")
            {
                dePartManageDic.Clear();
                dePartManageDic.Add("AddDepartmentInfo", new object[] { textEdit1.Text });
                DepartmentManageSend(dePartManageDic);
                textEdit1.Text = string.Empty;
            }
            else
            {
                MessageBoxDraw.ShowMsg("请输入申请科室名称！", MsgType.OK);
            }
        }

        /// <summary>
        /// 申请科室（删除按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSPDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                string str1 = this.gridView1.GetRowCellValue(selectedHandle, "科室名称").ToString();
                if (str1 != null)
                {
                    textEdit1.Text = str1;
                    dePartManageDic.Clear();
                    dePartManageDic.Add("DeleteDepartment", new object[] { str1 });
                    DepartmentManageSend(dePartManageDic);
                    textEdit1.Text = "";
                }
            }
           
        }
        CommunicationEntity DataDepartment = new CommunicationEntity();
        /// <summary>
        /// 申请科室（修改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSPSave_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                dePartManageDic.Clear();
                dePartManageDic.Add("UpDataDepartment", new object[] { textEdit1.Text,DataDepartment.ObjLastestParam });
                DepartmentManageSend(dePartManageDic);
                textEdit1.Text = string.Empty;
            }
            else
            {
                MessageBoxDraw.ShowMsg("没有可修改的科室，无法进行修改！", MsgType.OK);
            }
        }
        /// <summary>
        /// 科室列表点击选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                DataDepartment.ObjLastestParam = this.gridView1.GetRowCellValue(selectedHandle, "科室名称").ToString();
            }
        }


        /// <summary>
        /// 申请科室（取消）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSPCancel_Click(object sender, EventArgs e)
        {
            textEdit1.Text = string.Empty;
        }
        
        /// <summary>
        /// 审核医生（删除按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                applyDoctorInfo.Doctor = this.gridView2.GetRowCellValue(selectedHandle, "医生名称").ToString();
                applyDoctorInfo.Department = this.gridView2.GetRowCellValue(selectedHandle, "申请科室").ToString();

                dePartManageDic.Clear();
                dePartManageDic.Add("DeleteApplyDoctorInfo", new object[] { XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo) });
                DepartmentManageSend(dePartManageDic);
            }
        }

        /// <summary>
        /// 申请医生（添加按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (textEdit2.Text.Trim() == "")
            {
                MessageBoxDraw.ShowMsg("请输入申请医师名称！", MsgType.OK);
                return;
            }
            if (comboBoxEdit1.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择申请科室！", MsgType.OK);
                return;
            }

            ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
            applyDoctorInfo.Department = comboBoxEdit1.Text;
            applyDoctorInfo.Doctor = textEdit2.Text;
            dePartManageDic.Clear();
            dePartManageDic.Add("AddApplyDoctorInfo", new object[] { XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo) });
            DepartmentManageSend(dePartManageDic);
        }
        ApplyDoctorInfo applyDoctorInfoOld = new ApplyDoctorInfo();
        /// <summary>
        /// 申请医生列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                applyDoctorInfoOld.Doctor = this.gridView2.GetRowCellValue(selectedHandle, "医生名称").ToString();
                applyDoctorInfoOld.Department = this.gridView2.GetRowCellValue(selectedHandle, "申请科室").ToString();
            }
        }
        /// <summary>
        /// 修改申请医生（保存按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
                applyDoctorInfo.Department = comboBoxEdit1.Text;
                applyDoctorInfo.Doctor = textEdit2.Text;
                dePartManageDic.Clear();
                dePartManageDic.Add("UpdataApplyDoctorInfo", new object[] { XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo), XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfoOld) });
                DepartmentManageSend(dePartManageDic);
                textEdit2.Text = "";
            }
            else
            {
                MessageBoxDraw.ShowMsg("没有可修改的申请医师，无法进行修改！", MsgType.OK);
            }
        }
        /// <summary>
        /// 申请医生（取消按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            textEdit2.Text = "";
        }
        /// <summary>
        /// 审核医生（添加按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit2.SelectedIndex >= 0)
            {
                dePartManageDic.Clear();
                dePartManageDic.Add("AddAuditPhysician", new object[] { comboBoxEdit2.Text });
                DepartmentManageSend(dePartManageDic);
                comboBoxEdit2.SelectedIndex = -1;
                comboBoxEdit2.Text = "请选择";
            }
            else
            {
                MessageBoxDraw.ShowMsg("请选择待添加的审核医师！", MsgType.OK);
            }
        }
        /// <summary>
        /// 删除审核医生（删除按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (this.gridView4.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                int selectedHandle;
                selectedHandle = this.gridView4.GetSelectedRows()[0];
                string str = this.gridView4.GetRowCellValue(selectedHandle, "医生名称").ToString();
                dePartManageDic.Clear();
                dePartManageDic.Add("DeleteAuditPhysician", new object[] { str });
                DepartmentManageSend(dePartManageDic);
            }
        }
    }
}
