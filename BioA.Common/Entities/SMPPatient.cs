using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BioA.Common.Entities
{
    public class SMPPatient:CLItem
    {
        //样本编号
        private string _SMPNO;
        public string SMPNO
        {
            get { return _SMPNO; }
            set { _SMPNO = value; }
        }
        //产生时间
        private DateTime _DrawDate = DateTime.Now;
        public DateTime DrawDate
        {
            get { return _DrawDate; }
            set { _DrawDate = value; }
        }
        //患者ID
        private string _PatID;
        public string PatID
        {
            get { return _PatID; }
            set { _PatID = value; }
        }
        //患者姓名
        private string _PatientName;
        public string PatientName
        {
            get { return _PatientName; }
            set { _PatientName = value; }
        }
        //患者性别
        private string _Gender;
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }
        //患者年龄
        private int _Age;
        public int Age
        {
            get { return _Age; }
            set { _Age = value; }
        }
        public int AgeVaule
        {
            get 
            {
                int v = 0;
                switch (this.AgeUnit)
                {
                    case "Y": v = this.Age * 365; break;
                    case "M": v = this.Age * 30; break;
                    case "D": v = this.Age; break;
                }
                return v;
            }
        }
        //年龄单位
        private string _AgeUnit;
        public string AgeUnit
        {
            get { return _AgeUnit; }
            set { _AgeUnit = value; }
        }
        //患者备注
        private string _Remarks;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        //病历号
        string _MedicalRecordNum;
        public string MedicalRecordNum
        {
            get { return _MedicalRecordNum; }
            set { _MedicalRecordNum = value; }
        }
        DateTime _Birthday = DateTime.Now;
        public DateTime Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }
        DateTime _SamplingDT = DateTime.Now;
        public DateTime SamplingDT
        {
            get { return _SamplingDT; }
            set { _SamplingDT = value; }
        }
        DateTime _DeliverDT = DateTime.Now;
        public DateTime DeliverDT
        {
            get { return _DeliverDT; }
            set { _DeliverDT = value; }
        }
        string _Department;
        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }
        string _RefDoctor;
        public string RefDoctor
        {
            get { return _RefDoctor; }
            set { _RefDoctor = value; }
        }
        string _Operator;
        public string Operator
        {
            get { return _Operator; }
            set { _Operator = value; }
        }
        //门诊 住院
        string _PatientType;
        public string PatientType
        {
            get { return _PatientType; }
            set { _PatientType = value; }
        }
        //住院号
        string _InHospitalNum;
        public string InHospitalNum
        {
            get { return _InHospitalNum; }
            set { _InHospitalNum = value; }
        }
        string _BedNum;
        public string BedNum
        {
            get { return _BedNum; }
            set { _BedNum = value; }
        }
        //血型
        string _BloodType;
        public string BloodType
        {
            get { return _BloodType; }
            set { _BloodType = value; }
        }
        //收费类型
        string _ChargeType;
        public string ChargeType
        {
            get { return _ChargeType; }
            set { _ChargeType = value; }
        }
    }
}
