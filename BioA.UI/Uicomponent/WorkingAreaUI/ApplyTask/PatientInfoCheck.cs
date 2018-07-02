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

namespace BioA.UI
{
    public partial class PatientInfoCheck : DevExpress.XtraEditors.XtraUserControl
    {
        private List<PatientInfo> lstPatientInfo = new List<PatientInfo>();
        public List<PatientInfo> LstPatientInfo
        {
            get { return lstPatientInfo; }
            set
            {
                lstPatientInfo = value;
                if (lstPatientInfo.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("样本编号");
                    dt.Columns.Add("样本ID");
                    dt.Columns.Add("病人类型");
                    dt.Columns.Add("姓名");
                    dt.Columns.Add("性别");
                    dt.Columns.Add("年龄");
                    dt.Columns.Add("申请科室");
                    dt.Columns.Add("申请医生");
                    dt.Columns.Add("床号");
                    dt.Columns.Add("采样时间");
                    dt.Columns.Add("备注");
                    foreach (PatientInfo p in lstPatientInfo)
                    {
                        dt.Rows.Add(new object[]{p.SampleNum, p.SampleID, p.PatientType, p.PatientName, p.Sex, p.Age, p.ApplyDepartment, p.ApplyDoctor, p.BedNum, p.SamplingTime, p.Remarks});
                    }
                    gridControl1.DataSource = dt;
                }
            }
        }
        public PatientInfoCheck()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }
    }
}
