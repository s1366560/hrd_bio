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
    public partial class SMPResultSend : Form
    {
        public delegate void SMPResultSendDelegate(string sender);
        public event SMPResultSendDelegate SMPResultSendDelegateEvent;

        public SMPResultSend()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 发送结果数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButPrintState_Click(object sender, EventArgs e)
        {
            if(this.CESendCurrentSample.Checked == true)
            {
                if (this.SMPResultSendDelegateEvent != null)
                    this.SMPResultSendDelegateEvent("1");
            }
            else if (this.CESendSelectedSample.Checked == true)
            {
                if (this.SMPResultSendDelegateEvent != null)
                    this.SMPResultSendDelegateEvent("2");
            }
            else if(this.CESendAllSample.Checked == true)
            {
                if (this.SMPResultSendDelegateEvent != null)
                    this.SMPResultSendDelegateEvent("3");
            }
        }
        /// <summary>
        /// 当选择已选样本时，其它选中的都为不选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CESelectedSample_Click(object sender, EventArgs e)
        {
            if (this.CESendCurrentSample.CheckState == CheckState.Checked)
            {
                this.CESendCurrentSample.CheckState = CheckState.Unchecked;
            }
            else if (this.CESendAllSample.CheckState == CheckState.Checked)
            {
                this.CESendAllSample.CheckState = CheckState.Unchecked;
            }
        }
        /// <summary>
        /// 当选择所有样本时，其它选中的都为不选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CEAllSample_Click(object sender, EventArgs e)
        {
            if (this.CESendCurrentSample.CheckState == CheckState.Checked)
            {
                this.CESendCurrentSample.CheckState = CheckState.Unchecked;
            }
            else if (this.CESendSelectedSample.CheckState == CheckState.Checked)
            {
                this.CESendSelectedSample.CheckState = CheckState.Unchecked;
            }
        }
        /// <summary>
        /// 当选择当前样本时，其它选中的都为不选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CECurrentSample_Click(object sender, EventArgs e)
        {
            if (this.CESendSelectedSample.CheckState == CheckState.Checked)
            {
                this.CESendSelectedSample.CheckState = CheckState.Unchecked;
            }
            else if (this.CESendAllSample.CheckState == CheckState.Checked)
            {
                this.CESendAllSample.CheckState = CheckState.Unchecked;
            }
        }

        private void ButReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
