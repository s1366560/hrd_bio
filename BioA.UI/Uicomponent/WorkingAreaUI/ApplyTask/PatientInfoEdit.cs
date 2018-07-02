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

namespace BioA.UI
{
    public partial class PatientInfoEdit : DevExpress.XtraEditors.XtraUserControl
    {
        private int intSelectedNum = 0;
        public int IntSelectedNum
        {
            get { return intSelectedNum; }
            set { intSelectedNum = value; }
        }

        private List<int> lstSampleNum = new List<int>();
        public List<int> LstSampleNum
        {
            get { return lstSampleNum; }
            set
            {
                lstSampleNum = value;
            }
        }

        


        private List<string> lstApplyDepartment;
        public List<string> LstApplyDepartment
        {
            get { return lstApplyDepartment; }
            set 
            { 
                lstApplyDepartment = value;
                this.Invoke(new EventHandler(delegate
                    {
                        if (lstApplyDepartment != null)
                        {
                            combApplyDepartment.Properties.Items.AddRange(lstApplyDepartment);
                            combApplyDepartment.SelectedIndex = 0;
                        }
                    }));
                
            }
        }
        private List<string> lstApplyDoctor;
        public List<string> LstApplyDoctor
        {
            get { return lstApplyDoctor; }
            set 
            { 
                lstApplyDoctor = value;
                this.Invoke(new EventHandler(delegate
                {
                    if (lstApplyDoctor != null)
                    {
                        combApplyDoctor.Properties.Items.AddRange(lstApplyDoctor);
                        combApplyDoctor.SelectedIndex = 0;
                    }
                }));
                
            }
        }
        private List<string> lstCheckDoctor;
        public List<string> LstCheckDoctor
        {
            get { return lstCheckDoctor; }
            set 
            { 
                lstCheckDoctor = value;
                this.Invoke(new EventHandler(delegate
                {
                    if (lstCheckDoctor != null)
                    {
                        combCheckDoctor.Properties.Items.AddRange(lstCheckDoctor);
                        combCheckDoctor.SelectedIndex = 0;
                    }
                }));
                
            }
        }
        private List<string> lstInspectDoctor;
        public List<string> LstInspectDoctor
        {
            get { return lstInspectDoctor; }
            set 
            { 
                lstInspectDoctor = value;
                this.Invoke(new EventHandler(delegate
                {
                    if (lstInspectDoctor != null)
                    {
                        combInspectDoctor.Properties.Items.AddRange(lstInspectDoctor);
                        combInspectDoctor.SelectedIndex = 0;
                    }
                }));
                
            }
        }
        private PatientInfo patientInfoByNum = new PatientInfo();
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
                            dtpDateBirth.Value = patientInfoByNum.BirthDate;
                            txtAge.Text = patientInfoByNum.Age.ToString();
                            combSex.SelectedItem = patientInfoByNum.Sex;
                            combPatientType.SelectedItem = patientInfoByNum.PatientType;
                            dtpApplyTime.Value = patientInfoByNum.ApplyTime;
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

                            DataTable dt = new DataTable();
                            dt.Columns.Add("样本编号");
                            dt.Columns.Add("样本ID");
                            dt.Columns.Add("姓名");
                            dt.Columns.Add("申请科室");

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
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryPatientInfos", null)));
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
                                                dtpDateBirth.Value = p.BirthDate;
                                                txtAge.Text = p.Age.ToString();
                                                combSex.SelectedItem = p.Sex;
                                                combPatientType.SelectedItem = p.PatientType;
                                                dtpApplyTime.Value = p.ApplyTime;
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
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSampleNum.Text != "")
            {
                PatientInfo patient = new PatientInfo();
                patient.SampleNum = System.Convert.ToInt32(txtSampleNum.Text);
                patient.SampleID = txtSampleID.Text;
                patient.PatientName = txtName.Text;
                patient.BirthDate = dtpDateBirth.Value;
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

                CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                    new CommunicationEntity("UpdatePatientInfo", XmlUtility.Serializer(typeof(PatientInfo), patient))));
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

        private void PatientInfoEdit_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadInputPatientInfo));
            
        }
        /// <summary>
        /// 录入病人基本信息
        /// </summary>
        private void loadInputPatientInfo()
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryApplyApartment", null)));
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryApplyDoctor", null)));
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryCheckDoctor", null)));
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryInspectDoctor", null)));
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
            //    XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryPatientInfos", null)));

            combSex.Properties.Items.AddRange(new string[] { "男", "女", "" });
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
                intSelectedNum = System.Convert.ToInt32(this.gridView1.GetRowCellValue(selectedHandle, "样本编号").ToString());
                CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity),
                new CommunicationEntity("QueryPatientInfoBySampleNum", intSelectedNum.ToString())));
            }
        }
    }
}