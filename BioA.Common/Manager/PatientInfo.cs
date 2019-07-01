using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    /// <summary>
    /// 病人信息
    /// </summary>
    public class PatientInfo
    {
        public PatientInfo()
        {
            iD = 0;
            sampleNum = 0;
            inputTime = DateTime.Now;
            sampleID = string.Empty;
            patientName = string.Empty;
            birthDate = DateTime.Now;
            age = 0;
            sex = string.Empty;
            patientType = string.Empty;
            inspectTime = DateTime.Now;
            medicalRecordNum = string.Empty;
            bedNum = string.Empty;
            applyDepartment = string.Empty;
            applyDoctor = string.Empty;
            auditDoctor = string.Empty;
            inspectDoctor = string.Empty;
            samplingTime = DateTime.Now;
            applyTime = DateTime.Now;
            clinicalDiagnosis = string.Empty;
            remarks = string.Empty;
        }

        public int ID
        {
            get { return iD; }
            set 
            { 
                iD = value; 
            }
        }
        /// <summary>
        /// 样本/患者编号
        /// </summary>
        public int SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }
        /// <summary>
        /// 录入患者信息创建时间
        /// </summary>
        public DateTime InputTime
        {
            get { return inputTime; }
            set { inputTime = value; }
        }
        /// <summary>
        /// 患者ID
        /// </summary>
        public string SampleID
        {
            get { return sampleID; }
            set { sampleID = value; }
        }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }
        /// <summary>
        /// 患者出生日期
        /// </summary>
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        /// <summary>
        /// 年龄单位
        /// </summary>
        public string UnitAge
        {
            get { return unitAge; }
            set { unitAge = value; }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        /// <summary>
        /// 姓别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        /// <summary>
        /// 患者类型
        /// </summary>
        public string PatientType
        {
            get { return patientType; }
            set { patientType = value; }
        }
        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime InspectTime
        {
            get { return inspectTime; }
            set { inspectTime = value; }
        }
        /// <summary>
        /// 病历号
        /// </summary>
        public string MedicalRecordNum
        {
            get { return medicalRecordNum; }
            set { medicalRecordNum = value; }
        }
        /// <summary>
        /// 床位号
        /// </summary>
        public string BedNum
        {
            get { return bedNum; }
            set { bedNum = value; }
        }
        /// <summary>
        /// 申请部门
        /// </summary>
        public string ApplyDepartment
        {
            get { return applyDepartment; }
            set { applyDepartment = value; }
        }
        /// <summary>
        /// 申请医生
        /// </summary>
        public string ApplyDoctor
        {
            get { return applyDoctor; }
            set { applyDoctor = value; }
        }
        /// <summary>
        /// 审核医生
        /// </summary>
        public string AuditDoctor
        {
            get { return auditDoctor; }
            set { auditDoctor = value; }
        }
        /// <summary>
        /// 送检验医生
        /// </summary>
        public string InspectDoctor
        {
            get { return inspectDoctor; }
            set { inspectDoctor = value; }
        }
        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime SamplingTime
        {
            get { return samplingTime; }
            set { samplingTime = value; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }
        /// <summary>
        /// 临床诊断
        /// </summary>
        public string ClinicalDiagnosis
        {
            get { return clinicalDiagnosis; }
            set { clinicalDiagnosis = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }

        private int iD;
        private int sampleNum;
        private DateTime inputTime;
        private string sampleID;
        private string patientName;
        private DateTime birthDate;
        private string unitAge;
        private int age;
        private string sex;
        private string patientType;
        private DateTime inspectTime;
        private string medicalRecordNum;
        private string bedNum;
        private string applyDepartment;
        private string applyDoctor;
        private string auditDoctor;
        private string inspectDoctor;
        private DateTime samplingTime;
        private DateTime applyTime;
        private string clinicalDiagnosis;
        private string remarks;
    }
}