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
                    string strAdd = (string)XmlUtility.Deserialize(typeof(string), sender as string);
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
                    string str = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    int count = Convert.ToInt32(str);
                    if (count>0)
                    {
                        MessageBoxDraw.ShowMsg("删除失败！请先删除申请医师中的科室！", MsgType.Exception);
                    }
                    QueryDepartmentInfo();
                    break;
                case "QueryApplyDoctorInfo":
                    List<ApplyDoctorInfo> lstQueryApplyDoctorInfo = (List<ApplyDoctorInfo>)XmlUtility.Deserialize(typeof(List<ApplyDoctorInfo>), sender as string);
                    ApplyDoctorInfoAdd(lstQueryApplyDoctorInfo);
                   
                    break;
                case "AddApplyDoctorInfo":
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
                gridControl4.RefreshDataSource();

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
                this.gridControl4.DataSource = dt;
                this.gridView4.Columns[0].Width = 70;

            }));
        }

        private void ApplyAuditPhysician(List<UserInfo> lstQueryAuditPhysician)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridView3.Columns.Clear();
                gridControl3.RefreshDataSource();

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
                this.gridControl3.DataSource = dt;
                this.gridView3.Columns[0].Width = 70;

            }));
        }

        private void ApplyDoctorInfoAdd(List<ApplyDoctorInfo> lstQueryApplyDoctorInfo)
        {
            this.Invoke(new EventHandler(delegate
            {
                gridView2.Columns.Clear();
                gridControl2.RefreshDataSource();

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
                this.gridControl2.DataSource = dt;
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
                gridControl1.RefreshDataSource();

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
                this.gridControl1.DataSource = dt;
                this.gridView1.Columns[0].Width = 70;

            }));
        }
        

        private void QueryDepartmentInfo()
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "QueryDepartmentInfo";
            DataConfig.ObjParam = "";
            DepartmentManageSend(DataConfig);
        }
        private void QueryApplyDoctorInfo()
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "QueryApplyDoctorInfo";
            DataConfig.ObjParam = "";
            DepartmentManageSend(DataConfig);
        }
        private void DepartmentManageSend(object sender)
        {

            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SystemDepartmentManage, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }
        private void btnSPAdd_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text.Trim() != "")
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "AddDepartmentInfo";
                DataConfig.ObjParam = textEdit1.Text;
                DepartmentManageSend(DataConfig);
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
            QueryDepartmentInfo();
            QueryApplyDoctorInfo();
            QueryLaboratoryPhysician();
            QueryAuditPhysicianf();
        }

        private void QueryAuditPhysicianf()
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "QueryAuditPhysician";
            DataConfig.ObjParam = "";
            DepartmentManageSend(DataConfig);
        }

        private void QueryLaboratoryPhysician()
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "QueryUserInfo";
            DataConfig.ObjParam = "";
            DepartmentManageSend(DataConfig);
        }

        private void btnSPDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                CommunicationEntity DataConfig = new CommunicationEntity();
                int selectedHandle;
                try
                {
                    selectedHandle = this.gridView1.GetSelectedRows()[0];
                    string str1 = this.gridView1.GetRowCellValue(selectedHandle, "科室名称").ToString();
                    if (str1 != null)
                    {
                        textEdit1.Text = str1;
                        DataConfig.StrmethodName = "DeleteDepartment";
                        DataConfig.ObjParam = str1;
                        DepartmentManageSend(DataConfig);
                        textEdit1.Text = "";
                    }
                }
                catch
                {

                }
            }
           
        }
        CommunicationEntity DataDepartment = new CommunicationEntity();
        private void btnSPSave_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                DataDepartment.StrmethodName = "UpDataDepartment";
                DataDepartment.ObjParam = textEdit1.Text;
                DepartmentManageSend(DataDepartment);
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



        private void btnSPCancel_Click(object sender, EventArgs e)
        {
            textEdit1.Text = string.Empty;
        }

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

            CommunicationEntity DataConfig = new CommunicationEntity();
            ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
            DataConfig.StrmethodName = "AddApplyDoctorInfo";
            applyDoctorInfo.Department = comboBoxEdit1.Text;
            applyDoctorInfo.Doctor = textEdit2.Text;
            DataConfig.ObjParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo);
            DepartmentManageSend(DataConfig);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gridView2.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                CommunicationEntity DataConfig = new CommunicationEntity();
                ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                applyDoctorInfo.Doctor = this.gridView2.GetRowCellValue(selectedHandle, "医生名称").ToString();
                applyDoctorInfo.Department = this.gridView2.GetRowCellValue(selectedHandle, "申请科室").ToString();

                DataConfig.StrmethodName = "DeleteApplyDoctorInfo";

                DataConfig.ObjParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo);
                DepartmentManageSend(DataConfig);
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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {

                CommunicationEntity DataConfig = new CommunicationEntity();
                ApplyDoctorInfo applyDoctorInfo = new ApplyDoctorInfo();
                DataConfig.StrmethodName = "UpdataApplyDoctorInfo";
                applyDoctorInfo.Department = comboBoxEdit1.Text;
                applyDoctorInfo.Doctor = textEdit2.Text;
                DataConfig.ObjParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfo);
                DataConfig.ObjLastestParam = XmlUtility.Serializer(typeof(ApplyDoctorInfo), applyDoctorInfoOld);
                DepartmentManageSend(DataConfig);
                textEdit2.Text = "";
            }
            else
            {
                MessageBoxDraw.ShowMsg("没有可修改的申请医师，无法进行修改！", MsgType.OK);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            textEdit2.Text = "";
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit2.SelectedIndex > 0)
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "AddAuditPhysician";
                DataConfig.ObjParam = comboBoxEdit2.Text;
                DepartmentManageSend(DataConfig);
                comboBoxEdit2.Text = string.Empty;
            }
            else
            {
                MessageBoxDraw.ShowMsg("请选择待添加的审核医师！", MsgType.OK);
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (this.gridView4.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                CommunicationEntity DataConfig = new CommunicationEntity();

                int selectedHandle;

                selectedHandle = this.gridView4.GetSelectedRows()[0];
                string str = this.gridView4.GetRowCellValue(selectedHandle, "医生名称").ToString();


                DataConfig.StrmethodName = "DeleteAuditPhysician";

                DataConfig.ObjParam = str;
                DepartmentManageSend(DataConfig);
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "UpdataAuditPhysician";
            DataConfig.ObjParam = comboBoxEdit2.Text;
            DataConfig.ObjLastestParam = AuditPhysicianOld;
            DepartmentManageSend(DataConfig);
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
