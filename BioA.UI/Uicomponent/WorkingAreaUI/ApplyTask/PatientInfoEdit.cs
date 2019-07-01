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
    public partial class PatientInfoEdit : DevExpress.XtraEditors.XtraUserControl
    {

        /// <summary>
        /// 存储客户端发送信息给服务器的集合
        /// </summary>
        private Dictionary<string, object[]> patientDictionary = new Dictionary<string, object[]>();

        private int intSelectedNum = 0;
        /// <summary>
        /// 样本编号
        /// </summary>
        public int IntSelectedNum
        {
            get { return intSelectedNum; }
            set { intSelectedNum = value; }
        }

        private List<int> lstSampleNum = new List<int>();
        /// <summary>
        /// 所有项目样本编号
        /// </summary>
        public List<int> LstSampleNum
        {
            get { return lstSampleNum; }
            set
            {
                lstSampleNum = value;
            }
        }

        


        private List<string> lstApplyDepartment;
        /// <summary>
        /// 部门/科室
        /// </summary>
        public List<string> LstApplyDepartment
        {
            get { return lstApplyDepartment; }
            set 
            { 
                lstApplyDepartment = value;
                this.Invoke(new EventHandler(delegate
                    {
                        this.combApplyDepartment.Properties.Items.Clear();
                        if (lstApplyDepartment != null)
                        {
                            combApplyDepartment.Properties.Items.AddRange(lstApplyDepartment);
                            combApplyDepartment.SelectedIndex = 0;
                        }
                    }));
                
            }
        }
        private List<string> lstApplyDoctor;
        /// <summary>
        /// 申请医生
        /// </summary>
        public List<string> LstApplyDoctor
        {
            get { return lstApplyDoctor; }
            set 
            { 
                lstApplyDoctor = value;
                this.Invoke(new EventHandler(delegate
                {
                    this.combApplyDoctor.Properties.Items.Clear();
                    if (lstApplyDoctor != null)
                    {
                        combApplyDoctor.Properties.Items.AddRange(lstApplyDoctor);
                        combApplyDoctor.SelectedIndex = 0;
                    }
                }));
                
            }
        }
        private List<string> lstCheckDoctor;
        /// <summary>
        /// 审核医生
        /// </summary>
        public List<string> LstCheckDoctor
        {
            get { return lstCheckDoctor; }
            set 
            { 
                lstCheckDoctor = value;
                this.Invoke(new EventHandler(delegate
                {
                    this.combCheckDoctor.Properties.Items.Clear();
                    if (lstCheckDoctor != null)
                    {
                        combCheckDoctor.Properties.Items.AddRange(lstCheckDoctor);
                        combCheckDoctor.SelectedIndex = 0;
                    }
                }));
                
            }
        }
        private List<string> lstInspectDoctor;
        /// <summary>
        /// 检验医生
        /// </summary>
        public List<string> LstInspectDoctor
        {
            get { return lstInspectDoctor; }
            set 
            { 
                lstInspectDoctor = value;
                this.Invoke(new EventHandler(delegate
                {
                    combInspectDoctor.Properties.Items.Clear();
                    if (lstInspectDoctor != null)
                    {
                        combInspectDoctor.Properties.Items.AddRange(lstInspectDoctor);
                        combInspectDoctor.SelectedIndex = 0;
                    }
                }));
                
            }
        }
        private PatientInfo patientInfoByNum = new PatientInfo();
        /// <summary>
        /// 根据样本编号获取病人信息，显示
        /// </summary>
        public PatientInfo PatientInfoByNum
        {
            get { return patientInfoByNum; }
            set 
            {
                patientInfoByNum = value;
                this.Invoke(new EventHandler(delegate
                    {
                        if (patientInfoByNum != null)
                        {
                            txtSampleNum.Text = patientInfoByNum.SampleNum.ToString();
                            txtSampleID.Text = patientInfoByNum.SampleID;
                            txtName.Text = patientInfoByNum.PatientName;
                            txtAge.Text = patientInfoByNum.Age.ToString();
                            ComBoxUnitAge.SelectedIndex = ComBoxUnitAge.Properties.Items.IndexOf(patientInfoByNum.UnitAge);
                            combSex.SelectedItem = patientInfoByNum.Sex;
                            combPatientType.SelectedItem = patientInfoByNum.PatientType;
                            dtpApplyTime.Value = patientInfoByNum.ApplyTime;
                            comMedicalRecordNum.Text = patientInfoByNum.MedicalRecordNum;
                            txtBedNum.Text = patientInfoByNum.BedNum;
                            combApplyDepartment.SelectedItem = patientInfoByNum.ApplyDepartment;
                            combApplyDoctor.SelectedItem = patientInfoByNum.ApplyDoctor;
                            dtpSamplingTime.Value = patientInfoByNum.SamplingTime;
                            combCheckDoctor.SelectedItem = patientInfoByNum.AuditDoctor;
                            combInspectDoctor.SelectedItem = patientInfoByNum.InspectDoctor;
                            dtpInspectTime.Value = patientInfoByNum.InspectTime;
                            txtClinicalDiagnosis.Text = patientInfoByNum.ClinicalDiagnosis;
                            txtRemarks.Text = patientInfoByNum.Remarks;
                        }
                        else if (intSelectedNum > 0)
                        {
                            txtSampleNum.Text = intSelectedNum.ToString();
                        }
                    }));
            }
        }
        /// <summary>
        /// 病人参数信息数据表
        /// </summary>
        DataTable dt = new DataTable();
        private List<PatientInfo> lstPatientInfo = new List<PatientInfo>();
        public List<PatientInfo> LstPatientInfo
        {
            get { return lstPatientInfo; }
            set
            {
                lstPatientInfo = value;
                if (lstPatientInfo != null)
                {
                    this.Invoke(new EventHandler(delegate
                        {
                            dt.Rows.Clear();

                            foreach (PatientInfo patientInfo in lstPatientInfo)
                            {
                                dt.Rows.Add(new object[] { patientInfo.SampleNum, patientInfo.SampleID, patientInfo.PatientName, patientInfo.ApplyDepartment });
                            }

                            lstvPatientInfo.DataSource = dt;

                        }));
                }
            }
        }

        private string strUpdateInfo;
        public string StrUpdateInfo
        {
            get { return strUpdateInfo; }
            set
            {
                strUpdateInfo = value;
                if (strUpdateInfo == "更新成功！")
                {
                    patientDictionary.Clear();
                    //获取所有病人信息
                    patientDictionary.Add("QueryPatientInfos", null);
                    SendInfoToService(patientDictionary);
                    int i = 1;
                    while (true)
                    {
                        int sampleNum = System.Convert.ToInt32(txtSampleNum.Text) + i;
                        if (lstSampleNum.Max() <= sampleNum)
                        {
                            return;
                        }
                        else
                        {
                            if (lstSampleNum.Contains(sampleNum))
                            {
                                this.Invoke(new EventHandler(delegate
                                    {
                                        foreach (PatientInfo p in lstPatientInfo)
                                        {
                                            if (p.SampleNum == sampleNum)
                                            {
                                                txtSampleNum.Text = p.SampleNum.ToString();
                                                txtSampleID.Text = p.SampleID;
                                                txtName.Text = p.PatientName;
                                                txtAge.Text = p.Age.ToString();
                                                combSex.SelectedItem = p.Sex;
                                                combPatientType.SelectedItem = p.PatientType;
                                                dtpApplyTime.Value = p.ApplyTime;
                                                comMedicalRecordNum.Text = p.MedicalRecordNum;
                                                txtBedNum.Text = p.BedNum;
                                                combApplyDepartment.SelectedItem = p.ApplyDepartment;
                                                combApplyDoctor.SelectedItem = p.ApplyDoctor;
                                                dtpSamplingTime.Value = p.SamplingTime;
                                                combCheckDoctor.SelectedItem = p.AuditDoctor;
                                                combInspectDoctor.SelectedItem = p.InspectDoctor;
                                                dtpInspectTime.Value = p.InspectTime;
                                                txtClinicalDiagnosis.Text = p.ClinicalDiagnosis;
                                                txtRemarks.Text = p.Remarks;
                                                return;
                                            }
                                        }
                                        
                                    }));
                                
                                
                            }
                            i++;
                        }
                    }
                }
                else if (strUpdateInfo == "更新失败！")
                {
                    MessageBox.Show("更新失败！");
                }
            }
        }


        public PatientInfoEdit()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;


            dt.Columns.Add("样本编号");
            dt.Columns.Add("样本ID");
            dt.Columns.Add("姓名");
            dt.Columns.Add("申请科室");
            lstvPatientInfo.DataSource = dt;
        }
        /// <summary>
        /// 发送信息给服务端
        /// </summary>
        /// <param name="sender"></param>
        private void SendInfoToService(Dictionary<string, object[]> sender)
        {
            var patientInfoThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, sender);
            });
            patientInfoThread.IsBackground = true;
            patientInfoThread.Start();
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSampleNum.Text != "")
            {
                PatientInfo patient = new PatientInfo();
                patient.SampleNum = System.Convert.ToInt32(txtSampleNum.Text);
                patient.SampleID = txtSampleID.Text;
                patient.PatientName = txtName.Text;
                if (txtAge.Text == "" || int.Parse(txtAge.Text.Trim()) > 200 || int.Parse(txtAge.Text.Trim()) < 1)
                {
                    MessageBox.Show("年龄格式输入异常！");
                    txtAge.Focus();
                    return;
                }
                patient.UnitAge = ComBoxUnitAge.Text.Trim();
                patient.Age = txtAge.Text != "" ? System.Convert.ToInt32(txtAge.Text) : 0;
                patient.Sex = combSex.SelectedItem != null ? combSex.SelectedItem.ToString() : "";
                patient.PatientType = combPatientType.SelectedItem != null ? combPatientType.SelectedItem.ToString() : "";
                patient.ApplyTime = dtpApplyTime.Value;
                patient.BedNum = txtBedNum.Text;
                patient.ApplyDepartment = combApplyDepartment.SelectedItem != null ? combApplyDepartment.SelectedItem.ToString() : "";
                patient.ApplyDoctor = combApplyDoctor.SelectedItem != null ? combApplyDoctor.SelectedItem.ToString() : "";
                patient.SamplingTime = dtpSamplingTime.Value;
                patient.AuditDoctor = combCheckDoctor.SelectedItem != null ? combCheckDoctor.SelectedItem.ToString() : "";
                patient.InspectDoctor = combInspectDoctor.SelectedItem != null ? combInspectDoctor.SelectedItem.ToString() : "";
                patient.InspectTime = dtpInspectTime.Value;
                patient.ClinicalDiagnosis = txtClinicalDiagnosis.Text;
                patient.Remarks = txtRemarks.Text;

                patientDictionary.Clear();
                //修改病人信息
                patientDictionary.Add("UpdatePatientInfo", new object[] { XmlUtility.Serializer(typeof(PatientInfo), patient) });
                SendInfoToService(patientDictionary);
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }  
        }

        private void PatientInfoEdit_Load_1(object sender, EventArgs e)
        {
            this.loadInputPatientInfo();
            
        }
        /// <summary>
        /// 录入病人基本信息
        /// </summary>
        private void loadInputPatientInfo()
        {
            patientDictionary.Clear();
            //获取部门信息
            patientDictionary.Add("QueryDepartmentInfo", null);
            //获取医生信息
            patientDictionary.Add("QueryApplyDoctor", null);
            //获取审核医生信息
            patientDictionary.Add("QueryCheckDoctor", null);
            //获取检验医生信息
            patientDictionary.Add("QueryInspectDoctor", null);
            SendInfoToService(patientDictionary);
            
            combSex.Properties.Items.AddRange(new string[] { "男", "女", "--" });
            combSex.SelectedIndex = 0;
            combPatientType.Properties.Items.AddRange(new string[] { "门诊", "住院" });
            combPatientType.SelectedIndex = 0;
        }

        private void lstvPatientInfo_Click(object sender, EventArgs e)
        {
            int selectedHandle;

            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                IntSelectedNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "样本编号").ToString());
                //this.PatientInfoEdit_Load(null,null);

                PatientInfoByNum = lstPatientInfo.SingleOrDefault(p => p.SampleNum == intSelectedNum);
            }
        }
    }
}
