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
        //private BioAServiceClient serviceClient;
        //private DepartmentManageCallBack notifyCallBack;
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

            //notifyCallBack = new DepartmentManageCallBack();
            //serviceClient = new BioAServiceClient(new InstanceContext(notifyCallBack));
            // 注册客户端
            //serviceClient.RegisterClient(XmlUtility.Serializer(typeof(ModuleInfo), ModuleInfo.SystemDepartmentManage));
            //notifyCallBack.DataTransferEvent += DataTransfer_Event;

          
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

        private void comboBoxEdit2Add(List<UserInfo> QueryAuditPhysician)
        {
            this.Invoke(new EventHandler(delegate
            {
                this.comboBoxEdit2.Properties.Items.Clear();
                foreach (UserInfo str in QueryAuditPhysician)
                {
                    this.comboBoxEdit2.Properties.Items.AddRange(new object[] {
                str.UserName });
                }
            }));
        }

        private void QueryAuditPhysicianAdd(List<string> QueryAuditPhysician)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridView4.Columns.Clear();
                grdAuditDoctor.RefreshDataSource();

                int i = 1;
                DataTable dt = new DataTable();

                dt.Columns.Add("编号");
                dt.Columns.Add("医生名称");

                if (QueryAuditPhysician.Count != 0)
                {
                    foreach (string AuditPhysician in QueryAuditPhysician)
                    {
                        dt.Rows.Add(new object[] { i, AuditPhysician });

                        i++;
                    }
                }
                this.grdAuditDoctor.DataSource = dt;
                this.gridView4.Columns[0].Width = 70;

            }));
        }

        private void ApplyAuditPhysician(List<UserInfo> lstQueryAuditPhysician)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridView3.Columns.Clear();
                grdCheckoutDoctor.RefreshDataSource();

                int i = 1;
                DataTable dt = new DataTable();

                dt.Columns.Add("编号");
                dt.Columns.Add("医生名称");
              


                if (lstQueryAuditPhysician.Count != 0)
                {
                    foreach (UserInfo QueryAuditPhysician in lstQueryAuditPhysician)
                    {
                        dt.Rows.Add(new object[] { i, QueryAuditPhysician.UserName });

                        i++;
                    }
                }
                this.grdCheckoutDoctor.DataSource = dt;
                this.gridView3.Columns[0].Width = 70;

            }));
        }

        private void ApplyDoctorInfoAdd(List<ApplyDoctorInfo> lstQueryApplyDoctorInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridView2.Columns.Clear();
                grdApplyDoctor.RefreshDataSource();

                int i = 1;
                DataTable dt = new DataTable();

                dt.Columns.Add("编号");
                dt.Columns.Add("医生名称");
                dt.Columns.Add("申请科室");


                if (lstQueryApplyDoctorInfo.Count != 0)
                {
                    foreach (ApplyDoctorInfo QueryApplyDoctorInfo in lstQueryApplyDoctorInfo)
                    {
                        dt.Rows.Add(new object[] { i, QueryApplyDoctorInfo.Doctor, QueryApplyDoctorInfo.Department });

                        i++;
                    }
                }
                this.grdApplyDoctor.DataSource = dt;
                this.gridView2.Columns[0].Width = 70;

            }));
        }

        private void ComboBoxAdd(List<string> lstQueryDepartmentInfo)
        {
          
             this.Invoke(new EventHandler(delegate
            {
                this.comboBoxEdit1.Properties.Items.Clear();
                foreach (string str in lstQueryDepartmentInfo)
                {
                    this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
                str });
            }
            }));
        }

        private void DepartmentInfoAdd(List<string> lstQueryDepartmentInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridView1.Columns.Clear();
                grdApplyDepartments.RefreshDataSource();

                int i = 1;
                DataTable dt = new DataTable();

                dt.Columns.Add("编号");
                dt.Columns.Add("科室名称");

                if (lstQueryDepartmentInfo.Count != 0)
                {
                    foreach (string QueryDepartmentInfo in lstQueryDepartmentInfo)
                    {
                        dt.Rows.Add(new object[] { i, QueryDepartmentInfo });

                        i++;
                    }
                }
                this.grdApplyDepartments.DataSource = dt;
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
        /// <summary>
        /// 申请科室（添加）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSPAdd_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim() != "")
            {
                //CommunicationEntity DataConfig = new CommunicationEntity();
                //DataConfig.StrmethodName = "AddDepartmentInfo";
                //DataConfig.ObjParam = textEdit1.Text;
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

        private void DepartmentManage_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(DataLoad));
            

            //DataTable dt1 = new DataTable();

            //dt1.Columns.Add("编号");
            //dt1.Columns.Add("科室名称");
            //this.gridControl1.DataSource = dt1;
            //this.gridView1.Columns[0].Width = 70;

            //DataTable dt2 = new DataTable();

            //dt2.Columns.Add("编号");
            //dt2.Columns.Add("医生名称");
            //dt2.Columns.Add("申请科室");
            //this.gridControl2.DataSource = dt2;
            //this.gridView2.Columns[0].Width = 70;

            //DataTable dt3 = new DataTable();

            //dt3.Columns.Add("编号");
            //dt3.Columns.Add("医生名称");
            //this.gridControl3.DataSource = dt3;
            //this.gridView3.Columns[0].Width = 70;

            //DataTable dt4 = new DataTable();

            //dt4.Columns.Add("编号");
            //dt4.Columns.Add("医生名称");
            //this.gridControl4.DataSource = dt4;
            //this.gridView4.Columns[0].Width = 70;
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
            //CommunicationEntity DataConfig = new CommunicationEntity();
            //获取所有科室
            //DataConfig.StrmethodName = "QueryDepartmentInfo";
            //DataConfig.ObjParam = "";
            dePartManageDic.Clear();
            dePartManageDic.Add("QueryDepartmentInfo",null);
            DepartmentManageSend(dePartManageDic);
        }
        private void QueryApplyDoctorInfo()
        {
            //CommunicationEntity DataConfig = new CommunicationEntity();
            //获取所有医生信息
            //DataConfig.StrmethodName = "QueryApplyDoctorInfo";
            //DataConfig.ObjParam = "";
            dePartManageDic.Clear();
            dePartManageDic.Add("QueryApplyDoctorInfo",null);
            DepartmentManageSend(dePartManageDic);
        }
        private void QueryAuditPhysicianf()
        {
            //CommunicationEntity DataConfig = new CommunicationEntity();
            //获取所有审核医生信息
            //DataConfig.StrmethodName = "QueryAuditPhysician";
            //DataConfig.ObjParam = "";
            dePartManageDic.Clear();
            dePartManageDic.Add("QueryAuditPhysician", null);
            DepartmentManageSend(dePartManageDic);
        }

        private void QueryLaboratoryPhysician()
        {
            //CommunicationEntity DataConfig = new CommunicationEntity();
            //获取所有检验医生
            //DataConfig.StrmethodName = "QueryUserInfo";
            //DataConfig.ObjParam = "";
            dePartManageDic.Clear();
            dePartManageDic.Add("QueryUserInfo", null);
            DepartmentManageSend(dePartManageDic);
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
                try
                {
                    selectedHandle = this.gridView1.GetSelectedRows()[0];
                    string str1 = this.gridView1.GetRowCellValue(selectedHandle, "科室名称").ToString();
                    if (str1 != null)
                    {
                        textEdit1.Text = str1;
                        //DataConfig.StrmethodName = "DeleteDepartment";
                        //DataConfig.ObjParam = str1;
                        dePartManageDic.Clear();
                        dePartManageDic.Add("DeleteDepartment", new object[] { str1 });
                        DepartmentManageSend(dePartManageDic);
                        textEdit1.Text = "";
                    }
                }
                catch
                {

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
                //DataDepartment.StrmethodName = "UpDataDepartment";
                //DataDepartment.ObjParam = textEdit1.Text;
                dePartManageDic.Clear();
                dePartManageDic.Add("UpDataDepartment", new object[] { textEdit1.Text });
                DepartmentManageSend(dePartManageDic);
                textEdit1.Text = string.Empty;
            }
            else
            {
                MessageBoxDraw.ShowMsg("没有可修改的科室，无法进行修改！", MsgType.OK);
            }
        }
       
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

            //CommunicationEntity DataConfig = new CommunicationEntity();
            ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
            //DataConfig.StrmethodName = "AddApplyDoctorInfo";
            applyDoctorInfo.Department = comboBoxEdit1.Text;
            applyDoctorInfo.Doctor = textEdit2.Text;
            //DataConfig.ObjParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo);
            dePartManageDic.Clear();
            dePartManageDic.Add("AddApplyDoctorInfo", new object[] { XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo) });
            DepartmentManageSend(dePartManageDic);
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
                //CommunicationEntity DataConfig = new CommunicationEntity();
                ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                applyDoctorInfo.Doctor = this.gridView2.GetRowCellValue(selectedHandle, "医生名称").ToString();
                applyDoctorInfo.Department = this.gridView2.GetRowCellValue(selectedHandle, "申请科室").ToString();

                //DataConfig.StrmethodName = "DeleteApplyDoctorInfo";

                //DataConfig.ObjParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo);
                dePartManageDic.Clear();
                dePartManageDic.Add("DeleteApplyDoctorInfo", new object[] { XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo) });
                DepartmentManageSend(dePartManageDic);
            }
        }
        ApplyDoctorInfo applyDoctorInfoOld = new ApplyDoctorInfo();
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
        /// 申请医生（保存按钮）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {

                //CommunicationEntity DataConfig = new CommunicationEntity();
                ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
                //DataConfig.StrmethodName = "UpdataApplyDoctorInfo";
                applyDoctorInfo.Department = comboBoxEdit1.Text;
                applyDoctorInfo.Doctor = textEdit2.Text;
                //DataConfig.ObjParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo);
                //DataConfig.ObjLastestParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfoOld);
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
                //CommunicationEntity DataConfig = new CommunicationEntity();
                //DataConfig.StrmethodName = "AddAuditPhysician";
                //DataConfig.ObjParam = comboBoxEdit2.Text;
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
                //CommunicationEntity DataConfig = new CommunicationEntity();
                int selectedHandle;
                selectedHandle = this.gridView4.GetSelectedRows()[0];
                string str = this.gridView4.GetRowCellValue(selectedHandle, "医生名称").ToString();
                //DataConfig.StrmethodName = "DeleteAuditPhysician";
                //DataConfig.ObjParam = str;
                dePartManageDic.Clear();
                dePartManageDic.Add("DeleteAuditPhysician", new object[] { str });
                DepartmentManageSend(dePartManageDic);
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            //CommunicationEntity DataConfig = new CommunicationEntity();
            //DataConfig.StrmethodName = "UpdataAuditPhysician";
            //DataConfig.ObjParam = comboBoxEdit2.Text;
            //DataConfig.ObjLastestParam = AuditPhysicianOld;
            dePartManageDic.Clear();
            dePartManageDic.Add("UpdataAuditPhysician", new object[] { comboBoxEdit2.Text, AuditPhysicianOld });
            DepartmentManageSend(dePartManageDic);
            comboBoxEdit2.Text = string.Empty;
        }
        string AuditPhysicianOld;
        private void gridControl4_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedHandle;
                selectedHandle = this.gridView4.GetSelectedRows()[0];
                AuditPhysicianOld = this.gridView4.GetRowCellValue(selectedHandle, "医生名称").ToString();


            }
            catch
            {

            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
