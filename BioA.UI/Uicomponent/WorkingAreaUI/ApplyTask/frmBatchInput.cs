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

namespace BioA.UI
{
    public partial class frmBatchInput : DevExpress.XtraEditors.XtraForm
    {
        public delegate void DataTransfer(int startSanMun, int SamCount);
        public event DataTransfer DataTransferEvent;

        public delegate void EndBatch();
        public event EndBatch EndBatchEvent;

        private List<int> lstSampleNum = new List<int>();
        public List<int> LstSampleNum
        {
            get { return lstSampleNum; }
            set { lstSampleNum = value; }
        }

        private List<string> lstReceiveInfo = null;
        /// <summary>
        /// 批量录入完成后返回的结果
        /// </summary>
        public List<string> LstReceiveInfo
        {
            get { return lstReceiveInfo; }
            set
            {

                timer1.Stop();
                lstReceiveInfo = value;
                //int MaxNum = System.Convert.ToInt32(receiveInfo.Substring(0, receiveInfo.IndexOf(",")));
                //string str = receiveInfo.Substring(receiveInfo.IndexOf(",") + 1);
                foreach (string strResult in lstReceiveInfo)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        rtxtInfo.Text += Environment.NewLine + string.Format(strResult);
                        //rtxtInfo.Text += Environment.NewLine + string.Format("{0}号样本{1}", MaxNum, str);
                        //string st = string.Format("{0}号样本{1}", MaxNum, str);
                        //rtx.Add(st);
                    }));
                }
                MessageBox.Show("批量录入执行完成！");
                this.Close();
            }
        }
        /// <summary>
        /// 清空上次的缓存数据
        /// </summary>
        public void clera()
        {
            txtStartSamNum.Text = "";
            txtSampleAmount.Text = "";
            rtxtInfo.Text = "";
            btnSave.Enabled = true;
        }

        private string sampleNum = "";
        /// <summary>
        /// 批量录入的起始样本编号
        /// </summary>
        public string SampleNum
        {
            get { return sampleNum; }
            set { sampleNum = value; txtStartSamNum.Text = sampleNum;}
        }

        public frmBatchInput()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 批量录入：申请任务的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtStartSamNum.Text == null || txtStartSamNum.Text == "")
            {
                rtxtInfo.Text = "请录入起始样本号！";
                rtxtInfo.ForeColor = Color.Red;
                return;
            }

            if (lstSampleNum.Contains(System.Convert.ToInt32(txtStartSamNum.Text)))
            {
                rtxtInfo.Text = "此起始样本号已被申请任务，不能作为起始样本号！";
                rtxtInfo.ForeColor = Color.Red;
                return;
            }

            if (txtSampleAmount.Text == null || txtSampleAmount.Text == "")
            {
                rtxtInfo.Text = "请录入样本数量！";
                rtxtInfo.ForeColor = Color.Red;
                return;
            }

            if (DataTransferEvent != null)
            {
                //委托这个事件执行对应的方法
                DataTransferEvent(System.Convert.ToInt32(txtStartSamNum.Text), System.Convert.ToInt32(txtSampleAmount.Text));
                timer1.Interval = 500;
                timer1.Start();
                btnSave.Enabled = false;                
            }
        }
        string str = "任务申请中";
        private void timer1_Tick(object sender, EventArgs e)
        {
            rtxtInfo.Text = rtxtInfo.Text.TrimEnd(str.ToCharArray());
            if (str == "任务申请中••••••" || str.Length > 11)
            {
                str = "任务申请中";
            }
            else
            {
                str = str + "•";
            }

            rtxtInfo.AppendText(str);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSampleAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }  
        }

        private void txtStartSamNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }  
        }

        private void txtStartSamNum_Leave(object sender, EventArgs e)
        {
            //if (txtStartSamNum.Text.Trim() != null)
            //{
            //    if (lstSampleNum.Contains(System.Convert.ToInt32(txtStartSamNum.Text)))
            //    {
            //        rtxtInfo.Text = "此起始样本号已被申请任务，不能作为起始样本号！";
            //        rtxtInfo.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        rtxtInfo.Text = "";
            //    }
            //}
        }
        
        private void txtSampleAmount_Leave(object sender, EventArgs e)
        {
            //List<int> lstAppliedNum = new List<int>();
            //if (txtSampleAmount.Text != null && txtSampleAmount.Text.Trim() != "")
            //{
            //    for (int j = System.Convert.ToInt32(txtStartSamNum.Text); j <= lstSampleNum.Count; j++)
            //    {
            //        if (lstSampleNum.Contains(j))
            //        {
            //            lstAppliedNum.Add(j);
            //        }
            //    }
            //        //for (int i = 0; i <= (System.Convert.ToInt32(txtSampleAmount.Text) - 1); i++)
            //        //{
            //        //    if (lstSampleNum.Contains(System.Convert.ToInt32(txtStartSamNum.Text) + i))
            //        //    {
            //        //        lstAppliedNum.Add(System.Convert.ToInt32(txtStartSamNum.Text) + i);
            //        //    }
            //        //}
            //    string str = "";
            //    for (int i = 0; i < lstAppliedNum.Count; i++)
            //    {
            //        str += lstAppliedNum[i].ToString() + "，";
            //    }

            //    if (lstAppliedNum.Count > 0)
            //    {
            //        rtxtInfo.Text = "区间内样本号" + str.TrimEnd('，') + "已被申请任务，本次批量输入已跳过已被申请任务的样本号！" + Environment.NewLine;
            //        rtxtInfo.ForeColor = Color.Black;
            //        txtStartSamNum.Text = (System.Convert.ToInt32(lstSampleNum[lstSampleNum.Count - 1]) + 1).ToString();
            //    }
            //}
        }

        private void frmBatchInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (EndBatchEvent != null)
            {
                EndBatchEvent();
            }
        }
     

    }
}