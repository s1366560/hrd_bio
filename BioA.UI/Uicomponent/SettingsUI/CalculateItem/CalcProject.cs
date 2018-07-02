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
using BioA.UI;
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using BioA.Common.IO;
using System.Threading;
using BioA.Common;

namespace BioA.UI
{
    public partial class CalcProject : DevExpress.XtraEditors.XtraUserControl
    {
        AddOrEditCalcItem addOrEditCalcItem;
        public CalcProject()
        {
            InitializeComponent();
            
        }

        private void DataSend_Event(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.SettingsCalculateItem, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }
        
        public void CalcProjectDataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryCalcProjectAllInfo":
                    List<CalcProjectInfo> lstCalcProInfos = (List<CalcProjectInfo>)XmlUtility.Deserialize(typeof(List<CalcProjectInfo>), sender as string);
                    InitialCalcProList(lstCalcProInfos);
                    break;
                case "QueryProjectResultUnits":
                    List<string> lstUnits = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    addOrEditCalcItem.Units = lstUnits;
                    break;
                case "AddCalcProject":
                    string AddResult = sender as string;
                    if (AddResult == "该项目名称已存在！")
                    {
                        MessageBox.Show("此项目（计算项目或普通项目）已存在，请重新录入！");
                        return;
                    }
                    else
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            addOrEditCalcItem.Close();
                        }));

                        DataSend_Event(new CommunicationEntity("QueryCalcProjectAllInfo", null));
                    }
                    break;
                case "UpdateCalcProject":
                    int i = (int)sender;
                    if (i > 0)
                    {
                        this.Invoke(new EventHandler(delegate
                        {
                            addOrEditCalcItem.Close();
                        }));
                        DataSend_Event(new CommunicationEntity("QueryCalcProjectAllInfo", null));
                    }
                    else
                    {
                        MessageBox.Show("更新失败！");
                        return;
                    }
                    break;
                case "DeleteCalcProject":
                    int j = (int)sender;
                    if (j > 0)
                    {
                        DataSend_Event(new CommunicationEntity("QueryCalcProjectAllInfo", null));
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                        return;
                    }
                    break;
                case "ProjectPageinfoForCalc":
                    List<string> lstProjectNames = (List<string>)XmlUtility.Deserialize(typeof(List<string>), sender as string);
                    addOrEditCalcItem.ProjectNames = lstProjectNames;
                    break;
                default:
                    break;
            }
        }

        private void InitialCalcProList(List<CalcProjectInfo> lstCalcProInfos)
        {
            this.Invoke(new EventHandler(delegate {
                this.lstCalcProjectInfo.RefreshDataSource();
                int i = 1;

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[]{
                    new DataColumn(){ColumnName = "编号", MaxLength = 50},
                    new DataColumn(){ColumnName = "项目名称", MaxLength = 100},
                    new DataColumn(){ColumnName = "报告名称", MaxLength = 200},
                    new DataColumn(){ColumnName = "单位", MaxLength = 70},
                    new DataColumn(){ColumnName = "样本类型", MaxLength = 70},
                    new DataColumn(){ColumnName = "计算公式", MaxLength = 300},
                    new DataColumn(){ColumnName = "参考范围", MaxLength = 100}
                });

                foreach (CalcProjectInfo calcProInfo in lstCalcProInfos)
                {
                    string range = "";
                    if (calcProInfo.ReferenceRangeLow != 100000000 && calcProInfo.ReferenceRangeHigh != 100000000)
                        range = (calcProInfo.ReferenceRangeLow == 100000000 ? "" : calcProInfo.ReferenceRangeLow.ToString()) + " - " + (calcProInfo.ReferenceRangeHigh == 100000000 ? "" : calcProInfo.ReferenceRangeHigh.ToString());
                    dt.Rows.Add(new object[] { i, calcProInfo.CalcProjectName, calcProInfo.CalcProjectFullName, calcProInfo.Unit, calcProInfo.SampleType, calcProInfo.CalcFormula, range });
                }

                this.lstCalcProjectInfo.DataSource = dt;
                this.gridView1.Columns[0].Width = 50;
                this.gridView1.Columns[1].Width = 200;
                this.gridView1.Columns[2].Width = 300;
                this.gridView1.Columns[3].Width = 100;
                this.gridView1.Columns[4].Width = 150;
                this.gridView1.Columns[5].Width = 500;
                this.gridView1.Columns[6].Width = 300;
            }));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addOrEditCalcItem = new AddOrEditCalcItem();
            addOrEditCalcItem.Text = "添加计算项目";
            addOrEditCalcItem.StartPosition = FormStartPosition.CenterScreen;
            addOrEditCalcItem.LoadData();
            addOrEditCalcItem.ShowDialog();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.gridView1.SelectedRowsCount > 0)
            {
                CalcProjectInfo calcProInfo = new CalcProjectInfo();
                calcProInfo.CalcProjectName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString();
                calcProInfo.CalcProjectFullName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "报告名称").ToString();
                calcProInfo.Unit = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "单位").ToString();
                calcProInfo.SampleType = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "样本类型").ToString();
                calcProInfo.CalcFormula = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "计算公式").ToString();
                string range = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "参考范围").ToString();
                if (range != string.Empty)
                {
                    calcProInfo.ReferenceRangeLow = (float)System.Convert.ToDouble(range.Substring(0, range.IndexOf("-") - 1));
                    calcProInfo.ReferenceRangeHigh = (float)System.Convert.ToDouble(range.Substring(range.IndexOf("-") + 2));
                }



                addOrEditCalcItem = new AddOrEditCalcItem();
                addOrEditCalcItem.Text = "编辑计算项目";
                addOrEditCalcItem.StartPosition = FormStartPosition.CenterScreen;
                addOrEditCalcItem.CalcProInfoForEdit = calcProInfo;
                addOrEditCalcItem.LoadData();
                addOrEditCalcItem.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择需编辑的项目！");
                return;
            }
            
        }

        private void CalcProject_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadCalcProject));
        }
        private void loadCalcProject()
        {
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
            Thread.Sleep(1000);

            //Thread.Sleep(1000);
            // new Thread(new ParameterizedThreadStart(DataSend_Event)).Start(new CommunicationEntity("QueryCalcProjectAllInfo", null));
            DataSend_Event(new CommunicationEntity("QueryCalcProjectAllInfo", null));
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {
            //DeleteCalcProject
            if (this.gridView1.SelectedRowsCount > 0)
            {
                if (MessageBoxDraw.ShowMsg("是否确认删除该计算项目？", MsgType.Question) == DialogResult.OK)
                {
                    CalcProjectInfo calcProInfo = new CalcProjectInfo();
                    calcProInfo.CalcProjectName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString();
                    calcProInfo.SampleType = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "样本类型").ToString();
                    List<CalcProjectInfo> lstCalcProInfos = new List<CalcProjectInfo>();
                    lstCalcProInfos.Add(calcProInfo);
                    DataSend_Event(new CommunicationEntity("DeleteCalcProject", XmlUtility.Serializer(typeof(List<CalcProjectInfo>), lstCalcProInfos)));
                }
            }
        }
    }
}
