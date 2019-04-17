using BioA.Common;
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
    public partial class frmRepeat : Form
    {
        public frmRepeat()
        {
            InitializeComponent();
        }

        public void ClearFrmRepeatParam()
        {
            lstConcResults.Clear();
            qcResultInfo = null;
        }

        public void frmRepeat_Load(QCResultForUIInfo qcResInfo, List<float> lstConcResult)
        {
            lstConcResults = lstConcResult;
            qcResultInfo = qcResInfo;
            this.loadFrmRepeat();
        }

        private List<float> lstConcResults = new List<float>();
        private QCResultForUIInfo qcResultInfo = new QCResultForUIInfo();
        private void loadFrmRepeat()
        {
            float fSumTotal = 0;
            float fAverage = 0;// 平均值
            float fVariance = 0; // 方差
            float fStandardDeviation = 0; // 标准差
            float fCV = 0; // CV值
            double a = Math.Sqrt(1.5);
            double b = Math.Pow(-1.5, 2.0);
            foreach (float f in lstConcResults)
            {
                fSumTotal += f;
            }

            fAverage = fSumTotal / lstConcResults.Count;
            foreach (float f in lstConcResults)
            {
                fVariance += (float)Math.Pow((double)(f - fAverage), 2.0);
            }
            fVariance = fVariance / lstConcResults.Count;

            fStandardDeviation = (float)Math.Sqrt(fVariance);

            fCV = fStandardDeviation / fAverage;

            txtStatistic.Text = lstConcResults.Count.ToString();
            txtProjectName.Text = qcResultInfo.ProjectName;
            txtQCName.Text = qcResultInfo.QCName;
            txtLotNum.Text = qcResultInfo.LotNum;
            txtManufacturer.Text = qcResultInfo.Manufacturer;
            txtHorizonLevel.Text = qcResultInfo.HorizonLevel;
            txtMean.Text = fAverage.ToString();
            txtSD.Text = fStandardDeviation.ToString();
            txtCV.Text = fCV.ToString();
            txtTargetMean.Text = qcResultInfo.TargetMean.ToString();
            txtTargetSD.Text = qcResultInfo.TargetSD.ToString();
        }
    }
}
