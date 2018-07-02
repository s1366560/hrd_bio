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
using DevExpress.XtraGrid.Columns;
using BioA.UI.ServiceReference1;

namespace BioA.UI.Uicomponent.SettingsUI.ChemicalParameter
{
    public partial class ProjectParameter : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 数据增、删、查
        /// </summary>
        /// <param name="strAccessSqlMethod">访问数据库方法名</param>
        /// <param name="sender">参数对象</param>
        public delegate void AssayProInfoDelegate(object sender);
        public event AssayProInfoDelegate AssayProInfoEvent;

        CheProjectAddOrEdit cheProjectAddOrEdit;
        public ProjectParameter()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            //cheProjectAddOrEdit = new CheProjectAddOrEdit();
            //cheProjectAddOrEdit.DataHandleEvent+=cheProjectAddOrEdit_DataHandleEvent;

            

        }

        private void cheProjectAddOrEdit_DataHandleEvent(object sender)
        {
           
            if (AssayProInfoEvent != null)
                AssayProInfoEvent(sender);
        }

        private void ProjectParameter_Load(object sender, EventArgs e)
        {
            //"QueryAssayProAllInfo", null
           // if (AssayProInfoEvent != null)
            CommunicationEntity1 a = new CommunicationEntity1();
            a.StrmethodName = "QueryAssayProAllInfo";
            a.ObjParam = null;
            AssayProInfoEvent(a);
        }

        private void btnDetele_Click(object sender, EventArgs e)
        {

        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            CheProjectAddOrEdit cheProjectAddOrEdit = new CheProjectAddOrEdit();
            cheProjectAddOrEdit.DataHandleEvent += cheProjectAddOrEdit_DataHandleEvent;
            cheProjectAddOrEdit.Text = "新建项目";
            cheProjectAddOrEdit.StartPosition = FormStartPosition.CenterScreen;
            cheProjectAddOrEdit.ShowDialog();
        }

        private void btnEditProject_Click(object sender, EventArgs e)
        {
            
            CheProjectAddOrEdit cheProjectAddOrEdit = new CheProjectAddOrEdit();
            cheProjectAddOrEdit.DataHandleEvent += cheProjectAddOrEdit_DataHandleEvent;
            cheProjectAddOrEdit.Text = "编辑项目";
            cheProjectAddOrEdit.StartPosition = FormStartPosition.CenterScreen;
         
            int  selectedHandle;
            selectedHandle = this.gridView2.GetSelectedRows()[0];
            string str1 = this.gridView2.GetRowCellValue(selectedHandle, "项目名称").ToString();
            string str2 = this.gridView2.GetRowCellValue(selectedHandle, "类型").ToString();
            string str3 = this.gridView2.GetRowCellValue(selectedHandle, "项目全称").ToString();
            string str4 = this.gridView2.GetRowCellValue(selectedHandle, "通道号").ToString();
            cheProjectAddOrEdit.FormAdd(str1, str2, str3, str4);
            cheProjectAddOrEdit.ShowDialog();
        }

        private List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();

        public List<AssayProjectInfo> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.Invoke(new EventHandler(delegate 
                {
                    lstvProject.RefreshDataSource();
                    int i = 1;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("序号");
                    dt.Columns.Add("项目名称");
                    dt.Columns.Add("类型");
                    dt.Columns.Add("项目全称");
                    dt.Columns.Add("通道号");
                    if (lstAssayProInfos.Count != 0)
                    {
                        foreach (AssayProjectInfo assayProInfo in lstAssayProInfos)
                        {
                            dt.Rows.Add(new object[] { i, assayProInfo.ProjectName, assayProInfo.SampleType, assayProInfo.ProFullName, assayProInfo.ChannelNum });

                            i++;
                        }
                    }
                    this.lstvProject.DataSource = dt;
                    
                }));
            }
        }

        private void btnDeleteProject_Click(object sender, EventArgs e)
        {

        }
    }
}
