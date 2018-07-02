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
using BioA.UI.ServiceReference1;
using BioA.Common.IO;

namespace BioA.UI
{
    public partial class CheProjectAddOrEdit : DevExpress.XtraEditors.XtraForm
    {
        //BioAServiceClient service = new BioAServiceClient();
        public delegate void DataHandle(object sender);
        public event DataHandle DataHandleEvent;

        AssayProjectInfo assayProInfoOld = new AssayProjectInfo();
        public CheProjectAddOrEdit()
        {
            InitializeComponent();
            this.ControlBox = false;

            foreach (string str in RunConfigureUtility.SampleTypes)
            {
                cboSampleType.Properties.Items.Add(str);
            }

            cboSampleType.SelectedIndex = 1;
        }
        public void FormAdd(string strProShortName, string strSampleType, string strProLongName, string strChannelNumber)
        {
            txtProShortName.Text = strProShortName;
            txtProLongName.Text = strProLongName;
            txtChannelNumber.Text = strChannelNumber;
            cboSampleType.Text = strSampleType;
            assayProInfoOld.ProjectName = strProShortName;
            assayProInfoOld.SampleType = strSampleType;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            if (txtProShortName.Text.Trim() == "")
            {
                MessageBoxDraw.ShowMsg("项目名称为必填项，请填写项目名称！", MsgType.Warning);
                return;
            }
            else if (cboSampleType.SelectedIndex < 0)
            {
                MessageBoxDraw.ShowMsg("请选择项目样本类型！", MsgType.Warning);
                return;
            }
            else
            {
                AssayProjectInfo assayProInfo = new AssayProjectInfo();
                assayProInfo.ProjectName = txtProShortName.Text.Trim();
                assayProInfo.SampleType = cboSampleType.SelectedItem.ToString();
                if (txtProLongName.Text.Trim() != null)
                    assayProInfo.ProFullName = txtProLongName.Text.Trim();
                if (txtChannelNumber.Text.Trim() != null)
                    assayProInfo.ChannelNum = txtChannelNumber.Text.Trim();

                if (this.Text == "新建项目")
                {
                    if (DataHandleEvent != null)
                    {
                        CommunicationEntity communicationEntity = new CommunicationEntity();
                        communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo);
                        communicationEntity.StrmethodName = "AssayProjectAdd";
                        DataHandleEvent(communicationEntity);
                        this.Close();
                    }

                }
                if (this.Text == "编辑项目")
                {
                    if (DataHandleEvent != null)
                    {
                        CommunicationEntity communicationEntity = new CommunicationEntity();
                        communicationEntity.ObjLastestParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo);
                        communicationEntity.StrmethodName = "AssayProjectEdit";
                        communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfoOld);
                        DataHandleEvent(communicationEntity);
                        this.Close();
                    }

                }
                txtProShortName.Text = "";
                cboSampleType.SelectedIndex = 1;
                txtProLongName.Text = "";
                txtChannelNumber.Text = "";

            }
        }
    }
}