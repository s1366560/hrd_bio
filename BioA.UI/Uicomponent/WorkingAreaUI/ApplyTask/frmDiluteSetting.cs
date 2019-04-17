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

namespace BioA.UI
{
    public partial class frmDiluteSetting : Form
    {
        //private List<string> lstSelectedProName = new List<string>();

        public delegate void DataTransferDelegate(List<string[]> lstDiluteInfo);

        public event DataTransferDelegate DataTransferEvent;

        /// <summary>
        /// 被选中的项目名称
        /// </summary>
        //public List<string> LstSelectedProName
        //{
        //    get { return lstSelectedProName; }
        //    set { lstSelectedProName = value; }
        //}

        public List<float> lstDiluteRatio = new List<float>();
        public List<float> LstDiluteRatio
        {
            get { return lstDiluteRatio; }
            set { lstDiluteRatio = value; }
        }


        private List<string[]> lstDiluteInfos = new List<string[]>();

        public List<string[]> LstDiluteInfos
        {
            get { return lstDiluteInfos; }
            set { lstDiluteInfos = value; }
        }


        public frmDiluteSetting()
        {
            InitializeComponent();
        }

        private void frmDiluteSetting_Load(object sender, EventArgs e)
        {

            BeginInvoke(new Action(loadDiluteSetting));
        }
        /// <summary>
        /// 稀释设置
        /// </summary>
        private void loadDiluteSetting()
        {
            for (int i = 0; i < lstDiluteInfos.Count; i++)
            {
                Label label = new Label();
                label.Left = 20;
                label.Top = 30 * (i + 1) + 3;
                label.Width = 65;
                label.Text = "项目名称：";

                this.Controls.Add(label);

                TextEdit textEdit = new TextEdit();
                textEdit.Left = 90;
                textEdit.Top = 30 * (i + 1);
                textEdit.Width = 100;
                textEdit.Text = lstDiluteInfos[i][0];
                textEdit.Name = "text" + lstDiluteInfos[i][0];
                textEdit.Enabled = false;
                this.Controls.Add(textEdit);

                Label label1 = new Label();
                label1.Left = 205;
                label1.Top = 30 * (i + 1) + 3;
                label1.Width = 75;
                label1.Text = "样本/稀释：";
                this.Controls.Add(label1);

                ComboBoxEdit combBoxEdit = new ComboBoxEdit();
                combBoxEdit.Left = 280;
                combBoxEdit.Top = 30 * (i + 1);
                combBoxEdit.Width = 100;
                combBoxEdit.Name = "combDiluteType" + lstDiluteInfos[i][0];
                combBoxEdit.Properties.Items.AddRange(RunConfigureUtility.SampleDilute);
                combBoxEdit.SelectedItem = lstDiluteInfos[i][1];
                combBoxEdit.SelectedIndexChanged += combBoxEdit_SelectedIndexChanged;
                this.Controls.Add(combBoxEdit);

                Label label2 = new Label();
                label2.Left = 400;
                label2.Top = 30 * (i + 1) + 3;
                label2.Text = "稀释比例值：";
                label2.Name = "label" + lstDiluteInfos[i][0];
                label2.Width = 80;
                this.Controls.Add(label2);
                label2.Enabled = false;

                ComboBoxEdit combBoxEdit1 = new ComboBoxEdit();
                combBoxEdit1.Left = 485;
                combBoxEdit1.Top = 30 * (i + 1);
                combBoxEdit1.Width = 100;
                combBoxEdit1.Name = "combDiluteRatio" + lstDiluteInfos[i][0];
                combBoxEdit1.Properties.Items.AddRange(lstDiluteRatio);
                if (lstDiluteInfos[i][2] == "")
                {
                    combBoxEdit1.SelectedIndex = 0;
                    combBoxEdit1.Enabled = false;
                }
                else
                {
                    combBoxEdit1.SelectedItem = lstDiluteInfos[i][2];
                    if (lstDiluteInfos[i][1] != "自定义")
                    {
                        combBoxEdit1.Enabled = false;
                    }
                    else
                    {
                        combBoxEdit1.Enabled = true;
                    }
                }
                this.Controls.Add(combBoxEdit1);
            }


            SimpleButton btnSave = new SimpleButton();
            btnSave.Text = "保存";
            btnSave.Top = 30 * (lstDiluteInfos.Count + 1);
            btnSave.Height = 40;
            btnSave.Left = 350;
            btnSave.Width = 80;
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            SimpleButton btnCancel = new SimpleButton();
            btnCancel.Text = "取消";
            btnCancel.Top = 30 * (lstDiluteInfos.Count + 1);
            btnCancel.Height = 40;
            btnCancel.Left = 450;
            btnCancel.Width = 80;
            btnCancel.Click += btnCancel_Click;
            this.Controls.Add(btnCancel);

            this.AutoScroll = true;
        }

        private void combBoxEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((ComboBoxEdit)sender).SelectedItem.ToString())
            {
                case "常规体积":
                    (this.Controls.Find("label" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = false;
                    (this.Controls.Find("combDiluteRatio" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = false;
                    break;
                case "增量体积":
                    (this.Controls.Find("label" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = false;
                    (this.Controls.Find("combDiluteRatio" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = false;
                    break;
                case "减量体积":
                    (this.Controls.Find("label" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = false;
                    (this.Controls.Find("combDiluteRatio" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = false;
                    break;
                case "自定义":
                    (this.Controls.Find("label" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = true;
                    (this.Controls.Find("combDiluteRatio" + ((ComboBoxEdit)sender).Name.Substring(14), true))[0].Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string[]> lstDiluteInfo = new List<string[]>();
            foreach (string[] strProName in lstDiluteInfos)
            {
                string[] strDatas = new string[3];
                strDatas[0] = (this.Controls.Find("text" + strProName[0], true))[0].Text;
                strDatas[1] = ((this.Controls.Find("combDiluteType" + strProName[0], true))[0] as ComboBoxEdit).SelectedItem.ToString();
                strDatas[2] = ((this.Controls.Find("combDiluteRatio" + strProName[0], true))[0] as ComboBoxEdit).SelectedItem.ToString();
                lstDiluteInfo.Add(strDatas);
            }

            DataTransferEvent(lstDiluteInfo);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //foreach (string[] strProName in lstDiluteInfos)
            //{
            //    ((this.Controls.Find("combDiluteType" + strProName[0], true))[0] as ComboBoxEdit).SelectedIndex = 0;
            //    ((this.Controls.Find("label" + strProName[0], true))[0] as Label).Enabled = false;
            //    ((this.Controls.Find("combDiluteRatio" + strProName[0], true))[0] as ComboBoxEdit).Enabled = false;
            //    ((this.Controls.Find("combDiluteRatio" + strProName[0], true))[0] as ComboBoxEdit).SelectedIndex = 0;
            //}

            //List<string[]> lstDiluteInfo = new List<string[]>();
            //foreach (string[] strProName in lstDiluteInfos)
            //{
            //    string[] strDatas = new string[3];
            //    strDatas[0] = (this.Controls.Find("text" + strProName[0], true))[0].Text;
            //    strDatas[1] = ((this.Controls.Find("combDiluteType" + strProName[0], true))[0] as ComboBoxEdit).SelectedItem.ToString();
                

            //    strDatas[2] = ((this.Controls.Find("combDiluteRatio" + strProName[0], true))[0] as ComboBoxEdit).SelectedItem.ToString();
                
            //    lstDiluteInfo.Add(strDatas);
            //}

            //DataTransferEvent(lstDiluteInfo);

            this.Close();
        }
    }
}