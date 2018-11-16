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
using BioA.Common.IO;
using System.Text.RegularExpressions;

namespace BioA.UI
{
    public partial class RangeParameter : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 数据增、删、查
        /// </summary>
        /// <param name="strAccessSqlMethod">访问数据库方法名</param>
        /// <param name="sender">参数对象</param>
        public delegate void AssayProInfoDelegate(Dictionary<string, object[]> sender);
        public event AssayProInfoDelegate AssayProInfoForRangeParamEvent;
        public RangeParameter()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
        }

        /// <summary>
        /// 存储客户端发送信息给服务器的参数集合
        /// </summary>
        private Dictionary<string, object[]> rangeParamDic = new Dictionary<string, object[]>();

        private List<AssayProjectInfo> listAssayProjectInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 存储所有项目信息
        /// </summary>
        public List<AssayProjectInfo> ListAssayprojectInfos
        {
            get { return listAssayProjectInfos; }
            set { listAssayProjectInfos = value; }
        }

        private List<AssayProjectInfo> lstAssayProInfos = new List<AssayProjectInfo>();
        /// <summary>
        /// 显示所有生化项目信息
        /// </summary>
        public List<AssayProjectInfo> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.Invoke(new EventHandler(delegate
                {
                    lstvAssayProject.RefreshDataSource();
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
                    this.lstvAssayProject.DataSource = dt;
                    if (this.gridView1.RowCount > 0)
                    {
                        this.gridView1.SelectRow(0);//FocusedRowHandle = 0;
                        lstvAssayProject_Click(null, null);
                    }
                }));
            }
        }
        AssayProjectRangeParamInfo rangeParamInfo = new AssayProjectRangeParamInfo();
        /// <summary>
        /// 项目范围参数信息显示
        /// </summary>
        public AssayProjectRangeParamInfo RangeParamInfo
        {
            get { return rangeParamInfo; }
            set
            {
                rangeParamInfo = value;
                this.Invoke(new EventHandler(delegate
                {
                    chkAutoResurvey.Checked = rangeParamInfo.AutoRerun;
                    txtRepeatLimitLow.Text = rangeParamInfo.RepeatLimitLow.ToString();
                    txtRepeatLimitHigh.Text = rangeParamInfo.RepeatLimitHigh.ToString();

                    txtSerumAgeHigh1.Text = rangeParamInfo.AgeHigh1 == 100000000 ? "" : rangeParamInfo.AgeHigh1.ToString();
                    txtSerumAgeHigh2.Text = rangeParamInfo.AgeHigh2 == 100000000 ? "" : rangeParamInfo.AgeHigh2.ToString();
                    txtSerumAgeHigh3.Text = rangeParamInfo.AgeHigh3 == 100000000 ? "" : rangeParamInfo.AgeHigh3.ToString();
                    txtSerumAgeHigh4.Text = rangeParamInfo.AgeHigh4 == 100000000 ? "" : rangeParamInfo.AgeHigh4.ToString();
                    txtSerumAgeLow1.Text = rangeParamInfo.AgeLow1 == -100000000 ? "" : rangeParamInfo.AgeLow1.ToString();
                    txtSerumAgeLow2.Text = rangeParamInfo.AgeLow2 == -100000000 ? "" : rangeParamInfo.AgeLow2.ToString();
                    txtSerumAgeLow3.Text = rangeParamInfo.AgeLow3 == -100000000 ? "" : rangeParamInfo.AgeLow3.ToString();
                    txtSerumAgeLow4.Text = rangeParamInfo.AgeLow4 == -100000000 ? "" : rangeParamInfo.AgeLow4.ToString();
                    txtSerumManConsHigh1.Text = rangeParamInfo.ManConsHigh1 == 100000000 ? "" : rangeParamInfo.ManConsHigh1.ToString("#0.0000");
                    txtSerumManConsHigh2.Text = rangeParamInfo.ManConsHigh2 == 100000000 ? "" : rangeParamInfo.ManConsHigh2.ToString("#0.0000");
                    txtSerumManConsHigh3.Text = rangeParamInfo.ManConsHigh3 == 100000000 ? "" : rangeParamInfo.ManConsHigh3.ToString("#0.0000");
                    txtSerumManConsHigh4.Text = rangeParamInfo.ManConsHigh4 == 100000000 ? "" : rangeParamInfo.ManConsHigh4.ToString("#0.0000");
                    txtSerumManConsLow1.Text = rangeParamInfo.ManConsLow1 == -100000000 ? "" : rangeParamInfo.ManConsLow1.ToString("#0.0000");
                    txtSerumManConsLow2.Text = rangeParamInfo.ManConsLow2 == -100000000 ? "" : rangeParamInfo.ManConsLow2.ToString("#0.0000");
                    txtSerumManConsLow3.Text = rangeParamInfo.ManConsLow3 == -100000000 ? "" : rangeParamInfo.ManConsLow3.ToString("#0.0000");
                    txtSerumManConsLow4.Text = rangeParamInfo.ManConsLow4 == -100000000 ? "" : rangeParamInfo.ManConsLow4.ToString("#0.0000");
                    txtSerumWomanConsHigh1.Text = rangeParamInfo.WomanConsHigh1 == 100000000 ? "" : rangeParamInfo.WomanConsHigh1.ToString("#0.0000");
                    txtSerumWomanConsHigh2.Text = rangeParamInfo.WomanConsHigh2 == 100000000 ? "" : rangeParamInfo.WomanConsHigh2.ToString("#0.0000");
                    txtSerumWomanConsHigh3.Text = rangeParamInfo.WomanConsHigh3 == 100000000 ? "" : rangeParamInfo.WomanConsHigh3.ToString("#0.0000");
                    txtSerumWomanConsHigh4.Text = rangeParamInfo.WomanConsHigh4 == 100000000 ? "" : rangeParamInfo.WomanConsHigh4.ToString("#0.0000");
                    txtSerumWomanConsLow1.Text = rangeParamInfo.WomanConsLow1 == -100000000 ? "" : rangeParamInfo.WomanConsLow1.ToString("#0.0000");
                    txtSerumWomanConsLow2.Text = rangeParamInfo.WomanConsLow2 == -100000000 ? "" : rangeParamInfo.WomanConsLow2.ToString("#0.0000");
                    txtSerumWomanConsLow3.Text = rangeParamInfo.WomanConsLow3 == -100000000 ? "" : rangeParamInfo.WomanConsLow3.ToString("#0.0000");
                    txtSerumWomanConsLow4.Text = rangeParamInfo.WomanConsLow4 == -100000000 ? "" : rangeParamInfo.WomanConsLow4.ToString("#0.0000");

                }));

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AssayProjectRangeParamInfo parameter = new AssayProjectRangeParamInfo();

            parameter.ProjectName = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "项目名称").ToString();
            parameter.SampleType = this.gridView1.GetRowCellValue(this.gridView1.GetSelectedRows()[0], "类型").ToString();

            parameter.AutoRerun = chkAutoResurvey.Checked;


            if (!Regex.IsMatch(txtRepeatLimitLow.Text.Trim(), @"^(-?\d+)(\.\d+)?$") || !Regex.IsMatch(txtRepeatLimitHigh.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                MessageBox.Show("重测检查参数输入有误，请重新输入！");
                return;
            }

            if (System.Convert.ToInt32(txtRepeatLimitLow.Text) > System.Convert.ToInt32(txtRepeatLimitHigh.Text))
            {
                MessageBox.Show("重测检查参数输入有误，请重新输入！");
                return;
            }
            // 仅仅输入数字
            if (txtSerumAgeHigh1.Text.Trim() != "")


            if (txtSerumAgeHigh1.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeHigh1.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeLow1.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeLow1.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeHigh2.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeHigh2.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeLow2.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeLow2.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeHigh3.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeHigh3.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeLow3.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeLow3.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeHigh4.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeHigh4.Text.Trim(), "^([0-9]{1,})$") ||
                txtSerumAgeLow4.Text.Trim() != "" && !Regex.IsMatch(txtSerumAgeLow4.Text.Trim(), "^([0-9]{1,})$")
                )
            {
                MessageBox.Show("血清期望值中的年龄输入格式有误，请重新输入！");
                return;
            }
            // 血清同一区间最高值和最低值需全部填写
            if (txtSerumAgeLow1.Text.Trim() != "" && txtSerumAgeHigh1.Text.Trim() == "" ||
                txtSerumAgeLow1.Text.Trim() == "" && txtSerumAgeHigh1.Text.Trim() != "" ||
                txtSerumAgeLow2.Text.Trim() != "" && txtSerumAgeHigh2.Text.Trim() == "" ||
                txtSerumAgeLow2.Text.Trim() == "" && txtSerumAgeHigh2.Text.Trim() != "" ||
                txtSerumAgeLow3.Text.Trim() != "" && txtSerumAgeHigh3.Text.Trim() == "" ||
                txtSerumAgeLow3.Text.Trim() == "" && txtSerumAgeHigh3.Text.Trim() != "" ||
                txtSerumAgeLow4.Text.Trim() != "" && txtSerumAgeHigh4.Text.Trim() == "" ||
                txtSerumAgeLow4.Text.Trim() == "" && txtSerumAgeHigh4.Text.Trim() != "")
            {
                MessageBox.Show("血清期望值中的年龄输入格式有误，同一区间的最高值和最低值需全部填写，请重新输入！");
                return;
            }
            // 如果上一年龄为空，则下面不能填写年龄
            if (txtSerumAgeLow4.Text.Trim() != "")
            { 
                if (txtSerumAgeLow1.Text.Trim() == "" || txtSerumAgeLow2.Text.Trim() == "" || txtSerumAgeLow3.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的年龄输入格式有误，同一区间的最高值和最低值需全部填写，请重新输入！");
                    return;
                }
            }
            if (txtSerumAgeLow3.Text.Trim() != "")
            {
                if (txtSerumAgeLow1.Text.Trim() == "" || txtSerumAgeLow2.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的年龄输入格式有误，同一区间的最高值和最低值需全部填写，请重新输入！");
                    return;
                }
            }
            if (txtSerumAgeLow2.Text.Trim() != "")
            {
                if (txtSerumAgeLow1.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的年龄输入格式有误，同一区间的最高值和最低值需全部填写，请重新输入！");
                    return;
                }
            }
            // 年龄最小值不能大于最大值
            if (txtSerumAgeLow1.Text.Trim() != "" && System.Convert.ToInt32(txtSerumAgeHigh1.Text) < System.Convert.ToInt32(txtSerumAgeLow1.Text) ||
                txtSerumAgeLow2.Text.Trim() != "" && System.Convert.ToInt32(txtSerumAgeHigh2.Text) < System.Convert.ToInt32(txtSerumAgeLow2.Text) ||
                txtSerumAgeLow3.Text.Trim() != "" && System.Convert.ToInt32(txtSerumAgeHigh3.Text) < System.Convert.ToInt32(txtSerumAgeLow3.Text) ||
                txtSerumAgeLow4.Text.Trim() != "" && System.Convert.ToInt32(txtSerumAgeHigh4.Text) < System.Convert.ToInt32(txtSerumAgeLow4.Text))
            {
                MessageBox.Show("血清期望值中的年龄最大值不能小于最小值，请重新输入！");
                return;
            }

            // 区间不能重合
            List<int> lstValues1 = new List<int>();
            List<int> lstValues2 = new List<int>();
            List<int> lstValues3 = new List<int>();
            List<int> lstValues4 = new List<int>();

            if (txtSerumAgeLow1.Text.Trim() != "")
                for (int i = System.Convert.ToInt32(txtSerumAgeLow1.Text); i <= System.Convert.ToInt32(txtSerumAgeHigh1.Text); i++)
                {
                    lstValues1.Add(i);
                }

            if (txtSerumAgeLow2.Text.Trim() != "")
                for (int i = System.Convert.ToInt32(txtSerumAgeLow2.Text); i <= System.Convert.ToInt32(txtSerumAgeHigh2.Text); i++)
                {
                    lstValues2.Add(i);
                }

            if (txtSerumAgeLow3.Text.Trim() != "")
                for (int i = System.Convert.ToInt32(txtSerumAgeLow3.Text); i <= System.Convert.ToInt32(txtSerumAgeHigh3.Text); i++)
                {
                    lstValues3.Add(i);
                }

            if (txtSerumAgeLow4.Text.Trim() != "")
                for (int i = System.Convert.ToInt32(txtSerumAgeLow4.Text); i <= System.Convert.ToInt32(txtSerumAgeHigh4.Text); i++)
                {
                    lstValues4.Add(i);
                }

            if (lstValues1.Intersect(lstValues2).Count<int>() > 0 ||
                lstValues1.Intersect(lstValues3).Count<int>() > 0 ||
                lstValues1.Intersect(lstValues4).Count<int>() > 0 )
            {
                MessageBox.Show("血清期望值中的年龄区间不能重叠，请重新输入！");
                return;
            }
            else if (lstValues2.Intersect(lstValues3).Count<int>() > 0 ||
                     lstValues2.Intersect(lstValues4).Count<int>() > 0 )
            {
                MessageBox.Show("血清期望值中的年龄区间不能重叠，请重新输入！");
                return;
            }
            else if (lstValues3.Intersect(lstValues4).Count<int>() > 0)
            {
                MessageBox.Show("血清期望值中的年龄区间不能重叠，请重新输入！");
                return;
            }
            else
            {

            }
            // 当输入了对应年龄，则男、女值不为空时，值不能为非数字，男、女范围如果填写，则最大最小值需全填写，如果年龄未输入，则不允许输入男、女值（第一行除外）
            if (txtSerumManConsLow2.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsLow2.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                txtSerumManConsHigh2.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsHigh2.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                return;
            }
            else if (txtSerumManConsLow2.Text.Trim() != "" && txtSerumManConsHigh2.Text.Trim() == "" ||
                txtSerumManConsLow2.Text.Trim() != "" && txtSerumManConsHigh2.Text.Trim() == "")
            {
                MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                return;
            }
            else if (txtSerumManConsLow2.Text.Trim() != "" && txtSerumManConsHigh2.Text.Trim() == "" && System.Convert.ToDouble(txtSerumManConsLow2.Text) > System.Convert.ToDouble(txtSerumManConsHigh2.Text))
            {
                MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                return;
            }

            if (txtSerumWomanConsLow2.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsLow2.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                txtSerumWomanConsHigh2.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsHigh2.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
            {
                MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                return;
            }
            else if (txtSerumWomanConsLow2.Text.Trim() != "" && txtSerumWomanConsHigh2.Text.Trim() == "" ||
                txtSerumWomanConsLow2.Text.Trim() != "" && txtSerumWomanConsHigh2.Text.Trim() == "")
            {
                MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                return;
            }
            else if (txtSerumWomanConsLow2.Text.Trim() != "" && txtSerumWomanConsHigh2.Text.Trim() == "" && System.Convert.ToDouble(txtSerumWomanConsLow2.Text) > System.Convert.ToDouble(txtSerumWomanConsHigh2.Text))
            {
                MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                return;
            }

            if (txtSerumAgeLow2.Text.Trim() != "")
            {
                if (txtSerumManConsLow2.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsLow2.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                    txtSerumManConsHigh2.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsHigh2.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumManConsLow2.Text.Trim() != "" && txtSerumManConsHigh2.Text.Trim() == "" ||
                    txtSerumManConsLow2.Text.Trim() != "" && txtSerumManConsHigh2.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumManConsLow2.Text.Trim() != "" && txtSerumManConsHigh2.Text.Trim() == "" && System.Convert.ToDouble(txtSerumManConsLow2.Text) > System.Convert.ToDouble(txtSerumManConsHigh2.Text))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }

                if (txtSerumWomanConsLow2.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsLow2.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                    txtSerumWomanConsHigh2.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsHigh2.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumWomanConsLow2.Text.Trim() != "" && txtSerumWomanConsHigh2.Text.Trim() == "" ||
                    txtSerumWomanConsLow2.Text.Trim() != "" && txtSerumWomanConsHigh2.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumWomanConsLow2.Text.Trim() != "" && txtSerumWomanConsHigh2.Text.Trim() == "" && System.Convert.ToDouble(txtSerumWomanConsLow2.Text) > System.Convert.ToDouble(txtSerumWomanConsHigh2.Text))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
            }
            else
            {
                if (txtSerumManConsLow2.Text.Trim() != "" || txtSerumManConsHigh2.Text.Trim() != "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }

                if (txtSerumWomanConsLow2.Text.Trim() != "" || txtSerumWomanConsHigh2.Text.Trim() != "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
            }

            if (txtSerumAgeLow3.Text.Trim() != "")
            {
                if (txtSerumManConsLow3.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsLow3.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                    txtSerumManConsHigh3.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsHigh3.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumManConsLow3.Text.Trim() != "" && txtSerumManConsHigh3.Text.Trim() == "" ||
                    txtSerumManConsLow3.Text.Trim() != "" && txtSerumManConsHigh3.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumManConsLow3.Text.Trim() != "" && txtSerumManConsHigh3.Text.Trim() == "" && System.Convert.ToDouble(txtSerumManConsLow3.Text) > System.Convert.ToDouble(txtSerumManConsHigh3.Text))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }

                if (txtSerumWomanConsLow3.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsLow3.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                    txtSerumWomanConsHigh3.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsHigh3.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumWomanConsLow3.Text.Trim() != "" && txtSerumWomanConsHigh3.Text.Trim() == "" ||
                    txtSerumWomanConsLow3.Text.Trim() != "" && txtSerumWomanConsHigh3.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumWomanConsLow3.Text.Trim() != "" && txtSerumWomanConsHigh3.Text.Trim() == "" && System.Convert.ToDouble(txtSerumWomanConsLow3.Text) > System.Convert.ToDouble(txtSerumWomanConsHigh3.Text))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
            }
            else
            {
                if (txtSerumManConsLow3.Text.Trim() != "" || txtSerumManConsHigh3.Text.Trim() != "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }

                if (txtSerumWomanConsLow3.Text.Trim() != "" || txtSerumWomanConsHigh3.Text.Trim() != "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
            }

            if (txtSerumAgeLow4.Text.Trim() != "")
            {
                if (txtSerumManConsLow4.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsLow4.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                    txtSerumManConsHigh4.Text.Trim() != "" && !Regex.IsMatch(txtSerumManConsHigh4.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumManConsLow4.Text.Trim() != "" && txtSerumManConsHigh4.Text.Trim() == "" ||
                    txtSerumManConsLow4.Text.Trim() != "" && txtSerumManConsHigh4.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumManConsLow4.Text.Trim() != "" && txtSerumManConsHigh4.Text.Trim() == "" && System.Convert.ToDouble(txtSerumManConsLow4.Text) > System.Convert.ToDouble(txtSerumManConsHigh4.Text))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }

                if (txtSerumWomanConsLow4.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsLow4.Text.Trim(), @"^(-?\d+)(\.\d+)?$") ||
                    txtSerumWomanConsHigh4.Text.Trim() != "" && !Regex.IsMatch(txtSerumWomanConsHigh4.Text.Trim(), @"^(-?\d+)(\.\d+)?$"))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumWomanConsLow4.Text.Trim() != "" && txtSerumWomanConsHigh4.Text.Trim() == "" ||
                    txtSerumWomanConsLow4.Text.Trim() != "" && txtSerumWomanConsHigh4.Text.Trim() == "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
                else if (txtSerumWomanConsLow4.Text.Trim() != "" && txtSerumWomanConsHigh4.Text.Trim() == "" && System.Convert.ToDouble(txtSerumWomanConsLow4.Text) > System.Convert.ToDouble(txtSerumWomanConsHigh4.Text))
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
            }
            else
            {
                if (txtSerumManConsLow4.Text.Trim() != "" || txtSerumManConsHigh4.Text.Trim() != "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }

                if (txtSerumWomanConsLow4.Text.Trim() != "" || txtSerumWomanConsHigh4.Text.Trim() != "")
                {
                    MessageBox.Show("血清期望值中的范围值输入有误，请重新输入！");
                    return;
                }
            }

            parameter.RepeatLimitLow = txtRepeatLimitLow.Text.Trim() != "" ? System.Convert.ToInt32(txtRepeatLimitLow.Text) : 0;
            parameter.RepeatLimitHigh = txtRepeatLimitHigh.Text.Trim() != "" ? System.Convert.ToInt32(txtRepeatLimitHigh.Text) : 0;

            parameter.AgeLow1 = txtSerumAgeLow1.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeLow1.Text.Trim()) : -100000000;
            parameter.AgeLow2 = txtSerumAgeLow2.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeLow2.Text.Trim()) : -100000000;
            parameter.AgeLow3 = txtSerumAgeLow3.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeLow3.Text.Trim()) : -100000000;
            parameter.AgeLow4 = txtSerumAgeLow4.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeLow4.Text.Trim()) : -100000000;
            parameter.AgeHigh1 = txtSerumAgeHigh1.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeHigh1.Text.Trim()) : 100000000;
            parameter.AgeHigh2 = txtSerumAgeHigh2.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeHigh2.Text.Trim()) : 100000000;
            parameter.AgeHigh3 = txtSerumAgeHigh3.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeHigh3.Text.Trim()) : 100000000;
            parameter.AgeHigh4 = txtSerumAgeHigh4.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumAgeHigh4.Text.Trim()) : 100000000;
            parameter.ManConsLow1 = txtSerumManConsLow1.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsLow1.Text.Trim()) : -100000000;
            parameter.ManConsLow2 = txtSerumManConsLow2.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsLow2.Text.Trim()) : -100000000;
            parameter.ManConsLow3 = txtSerumManConsLow3.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsLow3.Text.Trim()) : -100000000;
            parameter.ManConsLow4 = txtSerumManConsLow4.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsLow4.Text.Trim()) : -100000000;
            parameter.ManConsHigh1 = txtSerumManConsHigh1.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsHigh1.Text.Trim()) : 100000000;
            parameter.ManConsHigh2 = txtSerumManConsHigh2.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsHigh2.Text.Trim()) : 100000000;
            parameter.ManConsHigh3 = txtSerumManConsHigh3.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsHigh3.Text.Trim()) : 100000000;
            parameter.ManConsHigh4 = txtSerumManConsHigh4.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumManConsHigh4.Text.Trim()) : 100000000;
            parameter.WomanConsLow1 = txtSerumWomanConsLow1.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsLow1.Text.Trim()) : -100000000;
            parameter.WomanConsLow2 = txtSerumWomanConsLow2.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsLow2.Text.Trim()) : -100000000;
            parameter.WomanConsLow3 = txtSerumWomanConsLow3.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsLow3.Text.Trim()) : -100000000;
            parameter.WomanConsLow4 = txtSerumWomanConsLow4.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsLow4.Text.Trim()) : -100000000;
            parameter.WomanConsHigh1 = txtSerumWomanConsHigh1.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsHigh1.Text.Trim()) : 100000000;
            parameter.WomanConsHigh2 = txtSerumWomanConsHigh2.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsHigh2.Text.Trim()) : 100000000;
            parameter.WomanConsHigh3 = txtSerumWomanConsHigh3.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsHigh3.Text.Trim()) : 100000000;
            parameter.WomanConsHigh4 = txtSerumWomanConsHigh4.Text.Trim() != "" ? System.Convert.ToInt32(txtSerumWomanConsHigh4.Text.Trim()) : 100000000;

            if (AssayProInfoForRangeParamEvent != null)
            {
                //AssayProInfoForRangeParamEvent(new CommunicationEntity("UpdateRangeParamByProNameAndType", XmlUtility.Serializer(typeof(AssayProjectRangeParamInfo), parameter)));
                rangeParamDic.Clear();
                rangeParamDic.Add("UpdateRangeParamByProNameAndType", new object[] { XmlUtility.Serializer(typeof(AssayProjectRangeParamInfo), parameter) });
                AssayProInfoForRangeParamEvent(rangeParamDic);

            }
        }

        

        private void lstvAssayProject_Click(object sender, EventArgs e)
        {
            AssayProjectInfo assayProInfo = new AssayProjectInfo();
            CommunicationEntity communicationEntity = new CommunicationEntity();
            int selectedHandle;
            selectedHandle = this.gridView1.GetSelectedRows()[0];
            assayProInfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
            assayProInfo.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();

            if (AssayProInfoForRangeParamEvent != null)
            {
                //communicationEntity.StrmethodName = "QueryRangeParamByProNameAndType";
                //communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo);
                rangeParamDic.Clear();
                rangeParamDic.Add("QueryRangeParamByProNameAndType", new object[] { XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo) });
                AssayProInfoForRangeParamEvent(rangeParamDic);
            }

        }

        private void RangeParameter_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => 
            {
                if (ListAssayprojectInfos.Count == 0)
                {
                    //获取所有生化项目
                    rangeParamDic.Add("QueryAssayProAllInfo", new object[] { ""});
                }
                else
                    this.LstAssayProInfos = this.listAssayProjectInfos;
            }));
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            AssayProjectInfo assayProInfo = new AssayProjectInfo();
            CommunicationEntity communicationEntity = new CommunicationEntity();
            int selectedHandle;
            selectedHandle = this.gridView1.GetSelectedRows()[0];
            assayProInfo.ProjectName = this.gridView1.GetRowCellValue(selectedHandle, "项目名称").ToString();
            assayProInfo.SampleType = this.gridView1.GetRowCellValue(selectedHandle, "类型").ToString();

            if (AssayProInfoForRangeParamEvent != null)
            {
                //communicationEntity.StrmethodName = "QueryRangeParamByProNameAndType";
                //communicationEntity.ObjParam = XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo);
                rangeParamDic.Clear();
                rangeParamDic.Add("QueryRangeParamByProNameAndType", new object[] { XmlUtility.Serializer(typeof(AssayProjectInfo), assayProInfo) });
                AssayProInfoForRangeParamEvent(rangeParamDic);
            }
        }

        private void chkAutoResurvey_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoResurvey.Checked == true)
            {
                txtRepeatLimitLow.Properties.ReadOnly = false;
                txtRepeatLimitHigh.Properties.ReadOnly = false;
            }
            else
            {
                txtRepeatLimitLow.Properties.ReadOnly = true;
                txtRepeatLimitHigh.Properties.ReadOnly = true;
            }
        }
    }
}
