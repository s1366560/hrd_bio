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
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> cheProAddOrEditDic = new Dictionary<string, object[]>();

        //BioAServiceClient service = new BioAServiceClient();
        public delegate void DataHandle(Dictionary<string, object[]> sender);
        public event DataHandle DataHandleEvent;
        

        AssayProjectInfo assayProInfoOld = new AssayProjectInfo();
        public CheProjectAddOrEdit()
        {
            InitializeComponent();
            this.ControlBox = false;
            cboSampleType.Properties.Items.AddRange(RunConfigureUtility.SampleTypes);
            cboSampleType.SelectedIndex = 1;
        }
        public void FormAdd(AssayProjectInfo assayProInfo)
        {
            txtProShortName.Text = assayProInfo.ProjectName;
            txtProLongName.Text = assayProInfo.ProFullName;
            txtChannelNumber.Text = assayProInfo.ChannelNum;
            cboSampleType.Text = assayProInfo.SampleType;
            assayProInfoOld.ProjectName = assayProInfo.ProjectName;
            assayProInfoOld.SampleType = assayProInfo.SampleType;
        }

        public void BeforeClearingTheData()
        {
            txtProShortName.Text = "";
            cboSampleType.SelectedIndex = 1;
            txtProLongName.Text = "";
            txtChannelNumber.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private List<AssayProjectInfo> _LstAssayProjectInfo = new List<AssayProjectInfo>();
        /// <summary>
        /// 存储所有生化项目信息
        /// </summary>
        public List<AssayProjectInfo> LstAssayProjectInfo
        {
            get { return _LstAssayProjectInfo; }
            set { _LstAssayProjectInfo = value; }
        }

        /// <summary>
        /// 确定保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    if (!_LstAssayProjectInfo.Exists(x => x.ProjectName == assayProInfo.ProjectName && x.SampleType == assayProInfo.SampleType))
                    {
                        if (DataHandleEvent != null)
                        {
                            cheProAddOrEditDic.Clear();
                            cheProAddOrEditDic.Add("AssayProjectAdd", new object[] { XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo) });
                            DataHandleEvent(cheProAddOrEditDic);
                        }
                    }
                    else
                    {
                        MessageBox.Show("该项目已存在，请重新输入！");
                        txtProShortName.Focus();
                    }

                }
                if (this.Text == "编辑项目")
                {
                    if (DataHandleEvent != null)
                    {
                        cheProAddOrEditDic.Clear();
                        cheProAddOrEditDic.Add("AssayProjectEdit", new object[] { XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfoOld), XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo) });
                        DataHandleEvent(cheProAddOrEditDic);
                    }

                }
            }
        }
    }
}