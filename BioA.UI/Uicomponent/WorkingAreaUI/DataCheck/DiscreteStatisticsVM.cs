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
    public partial class DiscreteStatisticsVM : DevExpress.XtraEditors.XtraForm
    {
        public DiscreteStatisticsVM(DiscreteStatisticalInfo discreteStatisticalInfo)
        {
            InitializeComponent();
            this.sampleNumValue.Text = discreteStatisticalInfo.SampleNum;
            this.CountValue.Text = discreteStatisticalInfo.Count;
            this.MeanValue.Text = discreteStatisticalInfo.Average;
            this.SDValue.Text = discreteStatisticalInfo.StandardDeviation;
            if(discreteStatisticalInfo.CVValue == "非数字%")
                this.CVValue.Text = "0.00%";
            else
                this.CVValue.Text = discreteStatisticalInfo.CVValue;
            this.RangeValue.Text = discreteStatisticalInfo.Range;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}