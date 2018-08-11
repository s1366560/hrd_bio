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
            
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPatientInfoEdit_Click(object sender, EventArgs e)
        {
            grpPatientInfoCheck.Controls.Clear();
            grpPatientInfoCheck.Controls.Add(patientInfoEdit);


        }

        private void btnPatientInfoSelect_Click(object sender, EventArgs e)
        {
            grpPatientInfoCheck.Controls.Clear();
            grpPatientInfoCheck.Controls.Add(patientInfoCheck);
            patientInfoCheck.LstPatientInfo = lstPatientInfo;
        }

        private void PatientInfo_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(LoadPatientInfo));
        }

        private void LoadPatientInfo()
        {
            grpPatientInfoCheck.Controls.Clear();
            grpPatientInfoCheck.Controls.Add(patientInfoEdit);

            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, XmlUtility.Serializer(typeof(CommunicationEntity),
            //    new CommunicationEntity("QueryPatientInfoBySampleNum", intSelectedNum.ToString())));
            //CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
            //    XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryPatientInfos", null)));
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