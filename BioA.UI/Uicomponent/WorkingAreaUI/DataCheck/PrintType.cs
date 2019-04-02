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
    public partial class PrintType : Form
    {
        public delegate void PrintDelegate(string print);
        public event PrintDelegate PrintDelegateEvent;

        public PrintType()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 开始打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButPrintState_Click(object sender, EventArgs e)
        {
            this.ButPrintState.Enabled = false;
            if(this.CECurrentSample.Checked == true)
            {
                if (this.PrintDelegateEvent != null)
                    this.PrintDelegateEvent("1");
            }
            else if (this.CESelectedSample.Checked == true)
            {
                if (this.PrintDelegateEvent != null)
                    this.PrintDelegateEvent("2");
            }
            else if(this.CEAllSample.Checked == true)
            {
                if (this.PrintDelegateEvent != null)
                    this.PrintDelegateEvent("3");
            }
        }
        /// <summary>
        /// 当选择已选样本时，其它选中的都为不选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CESelectedSample_Click(object sender, EventArgs e)
        {
            if (this.CECurrentSample.CheckState == CheckState.Checked)
            {
                this.CECurrentSample.CheckState = CheckState.Unchecked;
            }
            else if (this.CEAllSample.CheckState == CheckState.Checked)
            {
                this.CEAllSample.CheckState = CheckState.Unchecked;
            }
        }
        /// <summary>
        /// 当选择所有样本时，其它选中的都为不选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CEAllSample_Click(object sender, EventArgs e)
        {
            if (this.CECurrentSample.CheckState == CheckState.Checked)
            {
                this.CECurrentSample.CheckState = CheckState.Unchecked;
            }
            else if (this.CESelectedSample.CheckState == CheckState.Checked)
            {
                this.CESelectedSample.CheckState = CheckState.Unchecked;
            }
        }
        /// <summary>
        /// 当选择当前样本时，其它选中的都为不选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CECurrentSample_Click(object sender, EventArgs e)
        {
            if (this.CESelectedSample.CheckState == CheckState.Checked)
            {
                this.CESelectedSample.CheckState = CheckState.Unchecked;
            }
            else if (this.CEAllSample.CheckState == CheckState.Checked)
            {
                this.CEAllSample.CheckState = CheckState.Unchecked;
            }
        }

        private void ButReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
