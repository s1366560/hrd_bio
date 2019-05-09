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
using System.ServiceModel;
using BioA.Common.IO;
using System.Threading;

namespace BioA.UI
{
    public partial class DataConfig : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> dataConfigDic = new Dictionary<string, object[]>();
        /// <summary>
        /// 稀释比例参数
        /// </summary>
        string dilutionRatio;
        /// <summary>
        /// 结果单位
        /// </summary>
        string resultUnit;
        /// <summary>
        /// 保存修改之前的结果单位参数
        /// </summary>
        string resultUnitParam;
        /// <summary>
        /// 保存修改之前的稀释比例参数
        /// </summary>
        string dilutionRationParam;

        /// <summary>
        /// 稀释比例数据表
        /// </summary>
        DataTable dilution  = new DataTable();
        /// <summary>
        /// 结果单位数据表
        /// </summary>
        DataTable Unit = new DataTable();

        public DataConfig()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            gridView2.Appearance.HeaderPanel.Font = font;
            gridView2.Appearance.Row.Font = font;

            Unit.Columns.Add("编号");
            Unit.Columns.Add("结果单位");

            dilution.Columns.Add("编号");
            dilution.Columns.Add("稀释比例");
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
                    this.DisplayIUDPromptMsg(sender as string, 1);
                    break;
                case "UpdataDataConfig":
                    this.DisplayIUDPromptMsg(sender as string, 1);
                    break;
                case "DeleteDataConfig":
                    this.DisplayIUDPromptMsg(sender as string, 1);
                    break;
                   
                case  "QueryDilutionRatio":
                    List<float> lstQueryDilutionRatio = (List<float>)XmlUtility.Deserialize(typeof(List<float>), sender as string);
                    QueryDilutionRatioAdd(lstQueryDilutionRatio);
                    break;

                case "DilutionRatioAdd":
                    this.DisplayIUDPromptMsg(sender as string,0);
                    break;
                case "UpdataDilutionRatio":
                    this.DisplayIUDPromptMsg(sender as string, 0);
                    break;
                case "DeleteDilutionRatio":
                    this.DisplayIUDPromptMsg(sender as string, 0);
                    break;
            }
        }

        private void DisplayIUDPromptMsg(string msg, int state)
        {
            if (state == 0)
            {
                if (msg == "稀释比例创建成功！" || msg == "稀释比例修改成功！" || msg == "稀释比例删除成功！")
                {
                    QueryDilutionRatio();
                }
            }
            else
            {
                if (msg == "新增结果单位成功！" || msg == "修改结果单位成功！" || msg == "删除结果单位成功！")
                {
                    QueryDataConfig();
                }
            }
            this.Invoke(new EventHandler(delegate { MessageBox.Show(msg); }));
        }

        private void QueryDilutionRatioAdd(List<float> lstQueryDataConfig)
        {

            this.BeginInvoke(new EventHandler(delegate
            {
                int i = 1;
                dilution.Rows.Clear();
                if (lstQueryDataConfig.Count != 0)
                {
                    foreach (float QueryDataConfig in lstQueryDataConfig)
                    {
                        dilution.Rows.Add(new object[] { i, QueryDataConfig });

                        i++;
                    }
                }
                this.gridControl2.DataSource = dilution;

            }));

        }
        private void QueryDataConfigAdd(List<string> lstQueryDataConfig)
        {

            this.BeginInvoke(new EventHandler(delegate
            {

                int i = 1;
                Unit.Rows.Clear();
                if (lstQueryDataConfig.Count != 0)
                {
                    foreach (string QueryDataConfig in lstQueryDataConfig)
                    {
                        Unit.Rows.Add(new object[] { i, QueryDataConfig });

                        i++;
                    }
                }
                this.gridControl1.DataSource = Unit;
            }));
            
        }
        private void QueryDataConfig()
        {
            dataConfigDic.Clear();
            //获取所有数据单位信息
            dataConfigDic.Add("QueryDataConfig", null);
            DataConfigLoad(dataConfigDic);
        }
        private void QueryDilutionRatio()
        {
            dataConfigDic.Clear();
            //获取所有稀释比例信息
            dataConfigDic.Add("QueryDilutionRatio", null);
            DataConfigLoad(dataConfigDic);
        }
        public void DataConfig_Load(object sender, EventArgs e)
        {
             this.loadDataConfig();
            
        }
        private void loadDataConfig()
        {
            comboPaperType.Properties.Items.Clear();
            comboPaperType.Properties.Items.AddRange(RunConfigureUtility.PrintPaper);
            string setting = new BioA.SqlMaps.MyBatis().QueryPrintSetting();
            string[]  ret = setting.Split('|');
            this.comboPaperType.Text  = ret[0].Trim();
            if (ret[1].Trim() == "1")
            {
                chkChecker.Checked = true;
            }
            else
            {
                chkChecker.Checked = false;
            }
            if (ret[2].Trim() == "1")
            {
                chkAuditor.Checked = true;
            }
            else
            {
                chkAuditor.Checked = false;
            }

            dataConfigDic.Clear();
            //获取所有数据单位信息
            dataConfigDic.Add("QueryDataConfig",null);
            //获取所有稀释比例信息
            dataConfigDic.Add("QueryDilutionRatio",null);
            DataConfigLoad(dataConfigDic);
        }

        private void DataConfigLoad(Dictionary<string, object[]> sender)
        {
            var dataConfigThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.SettingsDataConfig, sender);
            });
            dataConfigThread.IsBackground = true;
            dataConfigThread.Start();
        }
        /// <summary>
        /// 新增（结果单位）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            resultUnit = null;
            resultUnit = textEdit1.Text.Trim();


            if (dilutionRatio != "")
            {
                dataConfigDic.Clear();
                //添加数据结果单位
                dataConfigDic.Add("DataConfigAdd", new object[] { resultUnit });
                DataConfigLoad(dataConfigDic);
            }
            else
            {
                MessageBoxDraw.ShowMsg("请填写结果单位！", MsgType.Warning);
                return;
            }
            textEdit1.Text = "";
        }
        /// <summary>
        /// 修改（结果单位）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateResultUnit_Clik(object sender, EventArgs e)
        {
            resultUnit = null;
            resultUnit = textEdit1.Text;
            if (resultUnitParam != null)
            {
                if (resultUnit != "")
                {
                    dataConfigDic.Clear();
                    //修改数据结果单位
                    dataConfigDic.Add("UpdataDataConfig", new object[] { resultUnit, resultUnitParam });
                    DataConfigLoad(dataConfigDic);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("您的输入有误，请重新输入！", MsgType.Exception);
                    return;
                }
            }
            else
            {
                MessageBoxDraw.ShowMsg("请选择您要修改的结果单位中的某一列！", MsgType.Exception);
                return;
            }
                
            textEdit1.Text = "";

        }
        /// <summary>
        /// 选择结果单位列表中的某一列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl1_Click(object sender, EventArgs e)
        {
            resultUnit = null;
            resultUnitParam = null;
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView1.GetSelectedRows()[0];
                resultUnit = this.gridView1.GetRowCellValue(selectedHandle, "结果单位").ToString();
                textEdit1.Text = resultUnit;
                resultUnitParam = resultUnit;
            }
        }
        /// <summary>
        /// 删除（结果单位）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            resultUnit = null;
            if (this.gridView1.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                int selectedHandle;

                selectedHandle = this.gridView1.GetSelectedRows()[0];
                resultUnit = this.gridView1.GetRowCellValue(selectedHandle, "结果单位").ToString();
                if (resultUnit != null)
                {
                    textEdit1.Text = resultUnit;
                    dataConfigDic.Clear();
                    //删除结果单位
                    dataConfigDic.Add("DeleteDataConfig", new object[] { resultUnit });
                    DataConfigLoad(dataConfigDic);
                    textEdit1.Text = "";
                }
            }
           
        }

        private void btnResultUnitCancel_Click(object sender, EventArgs e)
        {
            textEdit1.Text = "";
            resultUnitParam = null;
        }
        /// <summary>
        /// 新增（稀释比例）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDilutionRatio_Click(object sender, EventArgs e)
        {
            dilutionRatio = null;
            dilutionRatio = textEdit2.Text.Trim();
            int result;
            if (isNumberic(dilutionRatio, out result))
            {

            }
            else
            {
                MessageBoxDraw.ShowMsg("稀释比例输入格式有误！", MsgType.Exception);
                return;
            }


            if (dilutionRatio != "")
            {
                dataConfigDic.Clear();
                //添加稀释比例
                dataConfigDic.Add("DilutionRatioAdd", new object[] { dilutionRatio });
                DataConfigLoad(dataConfigDic);
            }
            else
            {
                MessageBoxDraw.ShowMsg("请输入稀释比例！", MsgType.Exception);
                return;
            }
            textEdit2.Text = "";
        }
        /// <summary>
        /// 修改（稀释比例）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateDilutionRatio_Click(object sender, EventArgs e)
        {
            dilutionRatio = null;
            dilutionRatio = textEdit2.Text;
            int result;
            if (isNumberic(dilutionRatio, out result))
            {

            }
            else
            {
                MessageBoxDraw.ShowMsg("稀释比例输入格式有误！", MsgType.Exception);
                return;
            }
            if (dilutionRationParam != null)
            {
                if (dilutionRatio != "")
                {
                    dataConfigDic.Clear();
                    //修改稀释比例
                    dataConfigDic.Add("UpdataDilutionRatio", new object[] { dilutionRatio, dilutionRationParam });
                    DataConfigLoad(dataConfigDic);
                }
                else
                {
                    MessageBoxDraw.ShowMsg("请输入稀释比例！", MsgType.Exception);
                    return;
                }
            }
            else
            {
                MessageBoxDraw.ShowMsg("请选择您要修改的稀释比例中的某一列！", MsgType.Exception);
                return;
            }
            textEdit2.Text = "";
        }
        /// <summary>
        /// 选择稀释比例列表中的某一列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridControl2_Click(object sender, EventArgs e)
        {
            dilutionRatio = null;
            dilutionRationParam = null;
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                int selectedHandle;
                selectedHandle = this.gridView2.GetSelectedRows()[0];
                dilutionRatio = this.gridView2.GetRowCellValue(selectedHandle, "稀释比例").ToString();
                textEdit2.Text = dilutionRatio;
                dilutionRationParam = dilutionRatio;
            }
        }
        /// <summary>
        /// 删除（稀释比例）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteDilutionRatio_Click(object sender, EventArgs e)
        {
            dilutionRatio = null;
            if (this.gridView2.GetSelectedRows().Count() > 0)
            {
                DialogResult yesorno = MessageBoxDraw.ShowMsg("是否确认删除", MsgType.YesNo);
                if (yesorno == DialogResult.No)
                {
                    return;
                }
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                dilutionRatio = this.gridView2.GetRowCellValue(selectedHandle, "稀释比例").ToString();
                if (dilutionRatio != null)
                {
                    textEdit2.Text = dilutionRatio;
                    dataConfigDic.Clear();
                    dataConfigDic.Add("DeleteDilutionRatio", new object[] { dilutionRatio });
                    DataConfigLoad(dataConfigDic);
                    textEdit2.Text = "";
                }
            }
           
        }

        private void btnDilutionRatioCancel_Click(object sender, EventArgs e)
        {
            textEdit2.Text = "";
            dilutionRatio = null;
        }
        /// <summary>
        /// 保存打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {
            string parperType = comboPaperType.SelectedItem.ToString();
            int cheker = 0;
            int aduitor = 0;
            if (chkChecker.Checked == true)
            {
                cheker = 1;
            }
            if (chkAuditor.Checked == true)
            {
                aduitor = 1;
            }
            int row = new BioA.SqlMaps.MyBatis().SavePritSetting(parperType, cheker, aduitor);
            if (row > 0)
            {
                MessageBoxDraw.Show("保存成功！");
            }
        }
    }
}
