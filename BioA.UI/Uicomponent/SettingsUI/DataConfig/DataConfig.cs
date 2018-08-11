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
                    string strAdd = (string)sender;
                    if (strAdd == "项目创建成功！")
                    {
                        QueryDataConfig();
                        MessageBox.Show("结果单位：" + strAdd);
                    }
                    else
                    {
                        MessageBox.Show("结果单位：" + strAdd);
                        return;
                    }      
                    break;
                case "UpdataDataConfig":
                    int resultReturn = (int)sender;
                    if (resultReturn > 0)
                    {
                        QueryDataConfig();
                        MessageBox.Show("结果单位：修改成功！");
                    }
                    else
                    {
                        MessageBox.Show("结果单位：修改失败！");
                        return;
                    }
                    break;
                case "DeleteDataConfig":
                    int deleteReturn = (int)sender;
                    if (deleteReturn > 0)
                    {
                        QueryDataConfig();
                        MessageBox.Show("结果单位：删除成功！");
                    }
                    else
                    {
                        MessageBox.Show("结果单位：删除失败！");
                        return;
                    }               
                    break;
                   
                case  "QueryDilutionRatio":
                    List<string> lstQueryDilutionRatio = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    QueryDilutionRatioAdd(lstQueryDilutionRatio);
                    break;

                case "DilutionRatioAdd":
                    string DilutionRatioAdd = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    if (DilutionRatioAdd != "项目创建成功！")
                    {
                        QueryDilutionRatio();
                        MessageBox.Show("稀释比例：" + DilutionRatioAdd);
                    }
                    else
                    {
                        MessageBox.Show("稀释比例：" + DilutionRatioAdd);
                        return;
                    }
                    break;
                case "UpdataDilutionRatio":
                    int DiluRatioReturn = (int)sender;
                    if (DiluRatioReturn > 0)
                    {
                        QueryDataConfig();
                        MessageBox.Show("稀释比例：修改成功！");
                    }
                    else
                    {
                        MessageBox.Show("稀释比例：修改失败！");
                        return;
                    }
                    break;
                case "DeleteDilutionRatio":
                    int DeleteDiluRatioReturn = (int)sender;
                    if (DeleteDiluRatioReturn > 0)
                    {
                        QueryDataConfig();
                        MessageBox.Show("稀释比例：删除成功！");
                    }
                    else
                    {
                        MessageBox.Show("稀释比例：删除失败！");
                        return;
                    }
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
                }));
            
        }
        private void QueryDataConfig()
        {
            //CommunicationEntity DataConfig = new CommunicationEntity();
            //DataConfig.StrmethodName = "QueryDataConfig";
            //DataConfig.ObjParam = "";
            dataConfigDic.Clear();
            //获取所有数据单位信息
            dataConfigDic.Add("QueryDataConfig", null);
            DataConfigLoad(dataConfigDic);
        }
        private void QueryDilutionRatio()
        {
            //CommunicationEntity DataConfig = new CommunicationEntity();
            //DataConfig.StrmethodName = "QueryDilutionRatio";
            //DataConfig.ObjParam = "";
            dataConfigDic.Clear();
            //获取所有稀释比例信息
            dataConfigDic.Add("QueryDilutionRatio", null);
            DataConfigLoad(dataConfigDic);
        }
        private void DataConfig_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadDataConfig));
            
        }
        private void loadDataConfig()
        {
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
                //CommunicationEntity DataConfig = new CommunicationEntity();
                //DataConfig.StrmethodName = "DataConfigAdd";
                //DataConfig.ObjParam = str;
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
                
                    //UpdataDataConfig.StrmethodName = "UpdataDataConfig";
                    //UpdataDataConfig.ObjParam = str;
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
                //CommunicationEntity DataConfig = new CommunicationEntity();
                int selectedHandle;

                selectedHandle = this.gridView1.GetSelectedRows()[0];
                resultUnit = this.gridView1.GetRowCellValue(selectedHandle, "结果单位").ToString();
                if (resultUnit != null)
                {
                    textEdit1.Text = resultUnit;
                    //DataConfig.StrmethodName = "DeleteDataConfig";
                    //DataConfig.ObjParam = str1;
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
            //int selectedHandle;
            //selectedHandle = this.gridView1.GetSelectedRows()[0];
            //string str1 = this.gridView1.GetRowCellValue(selectedHandle, "结果单位").ToString();
            //textEdit1.Text = str1;
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
                //CommunicationEntity DataConfig = new CommunicationEntity();
                //DataConfig.StrmethodName = "DilutionRatioAdd";
                //DataConfig.ObjParam = str;
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
                    dataConfigDic.Add("UpdataDilutionRatio", new object[] { dilutionRatio });
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
                //CommunicationEntity DataConfig = new CommunicationEntity();
                int selectedHandle;

                selectedHandle = this.gridView2.GetSelectedRows()[0];
                dilutionRatio = this.gridView2.GetRowCellValue(selectedHandle, "稀释比例").ToString();
                if (dilutionRatio != null)
                {
                    textEdit2.Text = dilutionRatio;
                    //DataConfig.StrmethodName = "DeleteDilutionRatio";
                    //DataConfig.ObjParam = str1;
                    //DataConfigLoad(DataConfig);
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
    }
}
