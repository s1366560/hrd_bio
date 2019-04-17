using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class PatientInfoFrm : DevExpress.XtraEditors.XtraForm
    {
        PatientInfoEdit patientInfoEdit = new PatientInfoEdit();
        PatientInfoCheck patientInfoCheck = new PatientInfoCheck();

        private int intSelectedNum = 0;
        /// <summary>
        /// 项目列表点击选中的样本编号
        /// </summary>
        public int IntSelectedNum
        {
            get { return intSelectedNum; }
            set 
            { 
                intSelectedNum = value;
                patientInfoEdit.IntSelectedNum = intSelectedNum;
            }
        }

        private List<int> lstSampleNum = new List<int>();
        /// <summary>
        /// 所有生化项目样本编号
        /// </summary>
        public List<int> LstSampleNum
        {
            get { return lstSampleNum; }
            set 
            {
                lstSampleNum = value;
                patientInfoEdit.LstSampleNum = lstSampleNum;
            }
        }

        private PatientInfo patientInfoByNum = new PatientInfo();
        /// <summary>
        /// 显示病人参数信息
        /// </summary>
        public PatientInfo PatientInfoByNum
        {
            get { return patientInfoByNum; }
            set 
            { 
                patientInfoByNum = value;
                patientInfoEdit.PatientInfoByNum = patientInfoByNum;
            }
        }

        private List<PatientInfo> lstPatientInfo = new List<PatientInfo>();
        /// <summary>
        /// 所有病人参数信息
        /// </summary>
        public List<PatientInfo> LstPatientInfo
        {
            get { return lstPatientInfo; }
            set
            {
                lstPatientInfo = value;
                patientInfoEdit.LstPatientInfo = lstPatientInfo;
                patientInfoCheck.LstPatientInfo = lstPatientInfo;
            }
        }

        private List<string> lstApplyDepartment;
        /// <summary>
        /// 科室/部门信息
        /// </summary>
        public List<string> LstApplyDepartment
        {
            get { return lstApplyDepartment; }
            set 
            { 
                lstApplyDepartment = value;
                patientInfoEdit.LstApplyDepartment = lstApplyDepartment;
            }
        }
        private List<string> lstApplyDoctor;
        /// <summary>
        /// 医生信息
        /// </summary>
        public List<string> LstApplyDoctor
        {
            get { return lstApplyDoctor; }
            set 
            { 
                lstApplyDoctor = value;
                patientInfoEdit.LstApplyDoctor = lstApplyDoctor;
            }
        }
        private List<string> lstCheckDoctor;
        /// <summary>
        /// 审核医生信息
        /// </summary>
        public List<string> LstCheckDoctor
        {
            get { return lstCheckDoctor; }
            set 
            { 
                lstCheckDoctor = value;
                patientInfoEdit.LstCheckDoctor = lstCheckDoctor;
            }
        }
        private List<string> lstInspectDoctor;
        /// <summary>
        /// 检验医生信息
        /// </summary>
        public List<string> LstInspectDoctor
        {
            get { return lstInspectDoctor; }
            set 
            { 
                lstInspectDoctor = value;
                patientInfoEdit.LstInspectDoctor = lstInspectDoctor;
            }
        }

        private string strUpdateInfo;
        /// <summary>
        /// 更新病人信息
        /// </summary>
        public string StrUpdateInfo
        {
            get { return strUpdateInfo; }
            set 
            {
                strUpdateInfo = value;
                patientInfoEdit.StrUpdateInfo = strUpdateInfo;
            }
        }

        public PatientInfoFrm()
        {
            InitializeComponent();
            this.ControlBox = false;
            grpPatientInfoCheck.Controls.Add(patientInfoEdit);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 编辑病人信息点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatientInfoEdit_Click(object sender, EventArgs e)
        {
            grpPatientInfoCheck.Controls.Clear();
            if (!grpPatientInfoCheck.Contains(patientInfoEdit))
                grpPatientInfoCheck.Controls.Add(patientInfoEdit);
            patientInfoEdit.PatientInfoEdit_Load(null, null);
        }
        /// <summary>
        /// 查看所有病人信息点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatientInfoSelect_Click(object sender, EventArgs e)
        {
            grpPatientInfoCheck.Controls.Clear();
            if(!grpPatientInfoCheck.Contains(patientInfoCheck))
                grpPatientInfoCheck.Controls.Add(patientInfoCheck);
            patientInfoCheck.LstPatientInfo = lstPatientInfo;
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PatientInfo_Load(object sender, EventArgs e)
        {
            patientInfoEdit.PatientInfoEdit_Load(null, null);
            this.LoadPatientInfo();
        }

        private void LoadPatientInfo()
        {
            Dictionary<string, object[]> patientDictionary = new Dictionary<string, object[]>();
            var patientThread = new Thread(() =>
            {
                patientDictionary.Add("QueryPatientInfoBySampleNum", new object[] { intSelectedNum.ToString() });
                patientDictionary.Add("QueryPatientInfos", new object[] { "" });
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, patientDictionary);
            });
            patientThread.IsBackground = true;
            patientThread.Start();
        }
    }
}