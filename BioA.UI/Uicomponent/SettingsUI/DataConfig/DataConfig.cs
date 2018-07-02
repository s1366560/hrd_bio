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
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using BioA.Common.IO;

namespace BioA.UI
{
    public partial class DataConfig : DevExpress.XtraEditors.XtraUserControl
    {
        public DataConfig()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;
        }

        protected bool isNumberic(string message, out int result)
        {
            System.Text.RegularExpressions.Regex rex =
            new System.Text.RegularExpressions.Regex(@"^\d+$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = int.Parse(message);
                return true;
            }
            else
                return false;
        }
        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryDataConfig":
                    List<string> lstQueryDataConfig = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    QueryDataConfigAdd(lstQueryDataConfig);
                    break;
                case "DataConfigAdd":
                    string strAdd = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    if (strAdd != "")
                    {
                        QueryDataConfig();
                    }
                    break;
                case "UpdataDataConfig":
                   
                        QueryDataConfig();
                    
                    break;
                case "DeleteDataConfig":
                                       
                        QueryDataConfig();                    
                    break;
                   
                case  "QueryDilutionRatio":
                    List<string> lstQueryDilutionRatio = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    QueryDilutionRatioAdd(lstQueryDilutionRatio);
                    break;

                case "DilutionRatioAdd":
                    string DilutionRatioAdd = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    if (DilutionRatioAdd != "")
                    {
                        QueryDilutionRatio();
                    }
                    break;
                case "UpdataDilutionRatio":

                    QueryDilutionRatio();

                    break;
                case "DeleteDilutionRatio":

                    QueryDilutionRatio();
                    break;
            }
        }

        private void DataConfigAdd(string DataConfig)
        {
            throw new NotImplementedException();
        }
        private void QueryDilutionRatioAdd(List<string> lstQueryDataConfig)
        {

            this.BeginInvoke(new EventHandler(delegate
            {
                gridView2.Columns.Clear();
                gridControl2.RefreshDataSource();

                int i = 1;
                DataTable dt = new DataTable();

                dt.Columns.Add("编号");
                dt.Columns.Add("稀释比例");

                if (lstQueryDataConfig.Count != 0)
                {
                    foreach (string QueryDataConfig in lstQueryDataConfig)
                    {
                        dt.Rows.Add(new object[] { i, QueryDataConfig });

                        i++;
                    }
                }
                this.gridControl2.DataSource = dt;

            }));

        }
        private void QueryDataConfigAdd(List<string> lstQueryDataConfig)
        {
           
                this.BeginInvoke(new EventHandler(delegate
                    {
                    gridView1.Columns.Clear();
                    gridControl1.RefreshDataSource();
                    
                    int i = 1;
                    DataTable dt = new DataTable();

                    dt.Columns.Add("编号");
                    dt.Columns.Add("结果单位");

                    if (lstQueryDataConfig.Count != 0)
                    {
                        foreach (string QueryDataConfig in lstQueryDataConfig)
                        {
                            dt.Rows.Add(new object[] { i, QueryDataConfig });

                            i++;
                        }
                    }
                    this.gridControl1.DataSource = dt;
                    gridControl1_Click(null, null);
                }));
            
        }
        private void QueryDataConfig()
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "QueryDataConfig";
            DataConfig.ObjParam = "";
            DataConfigLoad(DataConfig);
        }
        private void QueryDilutionRatio()
        {
            CommunicationEntity DataConfig = new CommunicationEntity();
            DataConfig.StrmethodName = "QueryDilutionRatio";
            DataConfig.ObjParam = "";
            DataConfigLoad(DataConfig);
        }
        private void DataConfig_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadDataConfig));
            
        }
        private void loadDataConfig()
        {
            QueryDataConfig();
            QueryDilutionRatio();
        }

        private void DataConfigLoad(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsDataConfig, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string str = textEdit1.Text.Trim();
           
           
            if (str !="")
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "DataConfigAdd";
                DataConfig.ObjParam = str;
                DataConfigLoad(DataConfig);
            }
            else
            {
                MessageBoxDraw.ShowMsg("请填写结果单位！", MsgType.Warning);
                return;
            }
            textEdit1.Text = "";
        }
        CommunicationEntity UpdataDataConfig = new CommunicationEntity();
        CommunicationEntity UpdataDilutionRatio = new CommunicationEntity();
        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = textEdit1.Text;
            if (str != "")
            {
                
                UpdataDataConfig.StrmethodName = "UpdataDataConfig";
                UpdataDataConfig.ObjParam = str;
                DataConfigLoad(UpdataDataConfig);
            }
            else
            {
                MessageBoxDraw.ShowMsg("您的输入有误，请重新输入！", MsgType.Exception);
                return;
            }
            textEdit1.Text = "";

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                string str1 = this.gridView1.GetRowCellValue(selectedHandle, "结果单位").ToString();
                textEdit1.Text = str1;
                UpdataDataConfig.ObjLastestParam = str1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                CommunicationEntity DataConfig = new CommunicationEntity();
                int selectedHandle;

                selectedHandle = this.gridView1.GetSelectedRows()[0];
                string str1 = this.gridView1.GetRowCellValue(selectedHandle, "结果单位").ToString();
                if (str1 != null)
                {
                    textEdit1.Text = str1;
                    DataConfig.StrmethodName = "DeleteDataConfig";
                    DataConfig.ObjParam = str1;
                    DataConfigLoad(DataConfig);
                    textEdit1.Text = "";
                }
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //int selectedHandle;
            //selectedHandle = this.gridView1.GetSelectedRows()[0];
            //string str1 = this.gridView1.GetRowCellValue(selectedHandle, "结果单位").ToString();
            //textEdit1.Text = str1;
            textEdit1.Text = "";
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            string str = textEdit2.Text.Trim();
            int result;
            if (isNumberic(str, out result))
            {

            }
            else
            {
                MessageBoxDraw.ShowMsg("稀释比例输入格式有误！", MsgType.Exception);
                return;
            }


            if (str != "")
            {
                CommunicationEntity DataConfig = new CommunicationEntity();
                DataConfig.StrmethodName = "DilutionRatioAdd";
                DataConfig.ObjParam = str;
                DataConfigLoad(DataConfig);
            }
            else
            {
                MessageBoxDraw.ShowMsg("请输入稀释比例！", MsgType.Exception);
                return;
            }
            textEdit2.Text = "";
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            string str = textEdit2.Text;
            int result;
            if (isNumberic(str, out result))
            {

            }
            else
            {
                MessageBoxDraw.ShowMsg("稀释比例输入格式有误！", MsgType.Exception);
                return;
            }
            if (str != "")
            {

                UpdataDilutionRatio.StrmethodName = "UpdataDilutionRatio";
                UpdataDilutionRatio.ObjParam = str;
                DataConfigLoad(UpdataDilutionRatio);
            }
            else
            {
                MessageBoxDraw.ShowMsg("请输入稀释比例！", MsgType.Exception);
                return;
            }
            textEdit2.Text = "";
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                string str2 = this.gridView2.GetRowCellValue(selectedHandle, "稀释比例").ToString();
                textEdit2.Text = str2;
                UpdataDilutionRatio.ObjLastestParam = str2;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                CommunicationEntity DataConfig = new CommunicationEntity();
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                string str1 = this.gridView2.GetRowCellValue(selectedHandle, "稀释比例").ToString();
                if (str1 != null)
                {
                    textEdit2.Text = str1;
                    DataConfig.StrmethodName = "DeleteDilutionRatio";
                    DataConfig.ObjParam = str1;
                    DataConfigLoad(DataConfig);
                    textEdit2.Text = "";
                }
            }
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            textEdit2.Text = "";
        }
    }
}
