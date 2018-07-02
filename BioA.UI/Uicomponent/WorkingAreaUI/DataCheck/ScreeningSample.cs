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
    public partial class ScreeningSample : DevExpress.XtraEditors.XtraForm
    {
        public ScreeningSample()
        {
            InitializeComponent();
            this.ControlBox = false; 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSever_Click(object sender, EventArgs e)
        {
            string Completed = chkCompletedSampleTest.Checked.ToString();
            string Starting = chkOngoingSampleTes.Checked.ToString();
            string NoStart = chkInitialSampleTest.Checked.ToString();

            Dictionary<string, string> dctValue = new Dictionary<string,string>();
            dctValue.Add("Completed", Completed);
            dctValue.Add("Starting", Starting);
            dctValue.Add("NoStart", NoStart);

            RunConfigureUtility.UpdateConfigureInfo("CheckSampleTaskState", dctValue);

            this.Close();
        }

        private void ScreeningSample_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(loadScreeningSample));
        }
        private void loadScreeningSample()
        {
            Dictionary<string, bool> dicSampleTaskState = RunConfigureUtility.ChkSampleTaskState;
            chkCompletedSampleTest.Checked = dicSampleTaskState["Completed"];
            chkOngoingSampleTes.Checked = dicSampleTaskState["Starting"];
            chkInitialSampleTest.Checked = dicSampleTaskState["NoStart"];
        }
    }
}