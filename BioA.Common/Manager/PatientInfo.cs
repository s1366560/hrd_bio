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

        public int SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; }
        }

        public DateTime InputTime
        {
            get { return inputTime; }
            set { inputTime = value; }
        }

        public string SampleID
        {
            get { return sampleID; }
            set { sampleID = value; }
        }

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public string PatientType
        {
            get { return patientType; }
            set { patientType = value; }
        }

        public DateTime InspectTime
        {
            get { return inspectTime; }
            set { inspectTime = value; }
        }

        public string BedNum
        {
            get { return bedNum; }
            set { bedNum = value; }
        }

        public string ApplyDepartment
        {
            get { return applyDepartment; }
            set { applyDepartment = value; }
        }

        public string ApplyDoctor
        {
            get { return applyDoctor; }
            set { applyDoctor = value; }
        }

        public string AuditDoctor
        {
            get { return auditDoctor; }
            set { auditDoctor = value; }
        }

        public string InspectDoctor
        {
            get { return inspectDoctor; }
            set { inspectDoctor = value; }
        }

        public DateTime SamplingTime
        {
            get { return samplingTime; }
            set { samplingTime = value; }
        }

        public DateTime ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }

        public string ClinicalDiagnosis
        {
            get { return clinicalDiagnosis; }
            set { clinicalDiagnosis = value; }
        }

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
        private int age;
        private string sex;
        private string patientType;
        private DateTime inspectTime;
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