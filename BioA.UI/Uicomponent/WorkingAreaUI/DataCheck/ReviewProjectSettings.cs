using BioA.BLL.ProjectParam;
using BioA.BLL.TaskInfo;
using BioA.Common;
using BioA.IBLL.IProjectParam;
using BioA.IBLL.ITaskInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioA.UI
{
    public partial class ReviewProjectSettings : Form
    {
        DataTable dt = new DataTable();

        public ReviewProjectSettings()
        {
            InitializeComponent();

            dt.Columns.Add("样本编号");
            dt.Columns.Add("检测项目");
            dt.Columns.Add("样本位置");
            dt.Columns.Add("复查原因");

            GridReviewProjectControl.DataSource = dt;
        }

        private List<string[]> reviewProjectName;
        /// <summary>
        /// 复查项目名称和超标提示集合
        /// </summary>
        public List<string[]> ReviewProjectName
        {
            get { return reviewProjectName; }
            set { reviewProjectName = value; }
        }

        private SampleInfoForResult samplePatientInfo;
        /// <summary>
        /// 样本病人信息
        /// </summary>
        public SampleInfoForResult SamplePatientInfo
        {
            get { return samplePatientInfo; }
            set { samplePatientInfo = value; }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviewProjectSettings_Load(object sender, EventArgs e)
        {
            this.DisplayReviewProjectInfo();
        }


        private void DisplayReviewProjectInfo()
        {
            dt.Rows.Clear();
            for (int i = 0; i < reviewProjectName.Count; i++)
            {
                string poitOut = reviewProjectName[i][1];
                string reviewReasons = "";
                switch (poitOut)
                {
                    case "↑":
                        reviewReasons = "浓度结果超出界限范围参数最大值";
                        break;
                    case "↓":
                        reviewReasons = "浓度结果低于界限范围参数最小值";
                        break;

                }
                dt.Rows.Add(new object[]{ samplePatientInfo.SampleNum,reviewProjectName[i][0], samplePatientInfo.SamplePos,reviewReasons});
            }
            GridReviewProjectControl.DataSource = dt;
            GridReviewProjectControl_Click(null,null);
        }

        private void GridReviewProjectControl_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedDataRow() == null) return;
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.BackColor = Color.Black;
            TxtBoxSampNumber.Text = gridView1.GetFocusedRowCellValue("样本编号").ToString();
            TxtBoxCheckProject.Text = gridView1.GetFocusedRowCellValue("检测项目").ToString();
            TxtBoxSampPos.Text = gridView1.GetFocusedRowCellValue("样本位置").ToString();
            TxtSampleType.Text = samplePatientInfo.SampleType;
            IProjectParam param = new ProjectParam();
            AssayProjectParamInfo assayParam = param.GetProjectParamInfoByNameOfType(TxtBoxCheckProject.Text, TxtSampleType.Text);
            TextNorOriginalVol.Text = assayParam.ComStosteVol.ToString();
            TextNorSamDilutionVol.Text = assayParam.ComSamVol.ToString();
            TextNorDilutionVol.Text = assayParam.ComDilutionVol.ToString();
            
            TextIncOriginalVol.Text = assayParam.IncStosteVol.ToString();
            TextIncSamDilutionVol.Text = assayParam.IncSamVol.ToString();
            TextIncDilutionVol.Text = assayParam.IncDilutionVol.ToString();

            TextDecOriginalVol.Text = assayParam.DecStosteVol.ToString();
            TextDecSamDilutionVol.Text = assayParam.DecSamVol.ToString();
            TextDecDilutionVol.Text = assayParam.DecDilutionVol.ToString();
            string reviewResult = gridView1.GetFocusedRowCellValue("复查原因").ToString();
            this.IsCheBoxDilution.Checked = false;
            if (reviewResult == "")
            {
                CheBoxNormal.CheckState = CheckState.Checked;
                this.CheBoxIncrement.CheckState = CheckState.Unchecked;
                this.CheBoxDecrement.CheckState = CheckState.Unchecked;
            }
            else if (reviewResult == "浓度结果超出界限范围参数最大值")
            {
                this.CheBoxNormal.CheckState = CheckState.Unchecked;
                this.CheBoxIncrement.CheckState = CheckState.Unchecked;
                this.CheBoxDecrement.CheckState = CheckState.Checked;
            }
            else if (reviewResult == "浓度结果低于界限范围参数最小值")
            {
                this.CheBoxNormal.CheckState = CheckState.Unchecked;
                this.CheBoxIncrement.CheckState = CheckState.Checked;
                this.CheBoxDecrement.CheckState = CheckState.Unchecked;
            }

        }

        private void ButConfirm_Click(object sender, EventArgs e)
        {
            TaskInfo taskInfo = new TaskInfo();
            taskInfo.SampleNum = int.Parse(this.TxtBoxSampNumber.Text);
            taskInfo.ProjectName = this.TxtBoxCheckProject.Text;
            taskInfo.SamplePos = int.Parse(this.TxtBoxSampPos.Text);
            taskInfo.SampleType = this.TxtSampleType.Text;
            taskInfo.CreateDate = samplePatientInfo.CreateTime;
            if (this.CheBoxNormal.Checked == true)
                taskInfo.SampleDilute = "常规体积";
            else if (this.CheBoxIncrement.Checked == true)
                taskInfo.SampleDilute = "增量体积";
            else if (this.CheBoxDecrement.Checked == true)
                taskInfo.SampleDilute = "减量体积";
            ITaskParamInfo taskParam = new TaskParamInfo();
            string result = taskParam.ReviewProject(null, taskInfo);
            if (result == "复查任务添加失败！")
            {
                MessageBox.Show(result);
            }

        }

        private void CheBoxNormal_Click(object sender, EventArgs e)
        {
            this.CheBoxNormal.CheckState = CheckState.Checked;
            this.CheBoxIncrement.CheckState = CheckState.Unchecked;
            this.CheBoxDecrement.CheckState = CheckState.Unchecked;
        }

        private void CheBoxIncrement_Click(object sender, EventArgs e)
        {
            this.CheBoxNormal.CheckState = CheckState.Unchecked;
            this.CheBoxIncrement.CheckState = CheckState.Checked;
            this.CheBoxDecrement.CheckState = CheckState.Unchecked;
        }

        private void CheBoxDecrement_Click(object sender, EventArgs e)
        {
            this.CheBoxNormal.CheckState = CheckState.Unchecked;
            this.CheBoxIncrement.CheckState = CheckState.Unchecked;
            this.CheBoxDecrement.CheckState = CheckState.Checked;
        }
    }
}
