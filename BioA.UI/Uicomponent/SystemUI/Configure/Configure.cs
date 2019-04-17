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
using BioA.Service;
using BioA.IBLL;
using BioA.BLL;

namespace BioA.UI
{
    public partial class FunctionConfig : DevExpress.XtraEditors.XtraUserControl
    {
        public FunctionConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FunctionConfig_Load(object sender, EventArgs e)
        {
            this.DisplayReagentConfig(ReagentConfigInfoConstrunction.ReagentStateInfo, ReagentConfigInfoConstrunction.ReagentConfig);
        }
        /// <summary>
        /// 显示条码配置和试剂状态设置数据
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="rc"></param>
        private void DisplayReagentConfig(ReagentStateInfo rs, ReagentConfigInfo rc)
        {
            if (rc != null)
            {
                this.chkSDBROpen.Checked = rc.IsSampBarcodeOpen;
                this.chkRDBROpen.Checked = rc.IsReagentBarcodeOpen;
                this.chkHBROpen.Checked = rc.IsHandheldBarcodeOpen;
                this.textBox1.Text = rc.SampleBracodeLength.ToString();
                this.textBox2.Text = rc.ReagentBarcodeLength.ToString();
            }
            if (rs != null)
            {
                switch (rs.ReagentStatusModule)
                {
                    case 1:
                        this.checkReagentOpen.Checked = true;
                        break;
                    case 2:
                        this.checkReagentSemiBlocking.Checked = true;
                        this.grpChannleNumber.Enabled = true;
                        break;
                }
                if (rs.ReagentNumberList.Count > 0)
                {
                    for (int i = 0; i < rs.ReagentNumberList.Count; i++)
                    {
                        if (rs.ReagentNumberList[i] == 1)
                        {
                            this.checkEdit1.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 2)
                        {
                            this.checkEdit2.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 3)
                        {
                            this.checkEdit3.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 4)
                        {
                            this.checkEdit4.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 5)
                        {
                            this.checkEdit5.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 6)
                        {
                            this.checkEdit6.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 7)
                        {
                            this.checkEdit7.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 8)
                        {
                            this.checkEdit8.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 9)
                        {
                            this.checkEdit9.Checked = true;
                            continue;
                        }
                        else if (rs.ReagentNumberList[i] == 10)
                        {
                            this.checkEdit10.Checked = true;
                            continue;
                        }
                    }
                }
                
            }
        }

        /// <summary>
        /// 文本框1限制只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar!='\b')//这是允许输入退格键  
            {  
                if((e.KeyChar<'0')||(e.KeyChar>'9') || textBox1.Text.Length > 1)//这是允许输入0-9数字  
                {  
                    e.Handled = true;  
                }
            }  
        }
        /// <summary>
        /// 文本框2限制只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9') || textBox2.Text.Length > 1)//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }
        /// <summary>
        /// 保存条码配制功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chekSaveConfig_CheckedChanged(object sender, EventArgs e)
        {
            ReagentConfigInfo rc = new ReagentConfigInfo();
            rc.IsSampBarcodeOpen = this.chkSDBROpen.Checked;
            rc.IsReagentBarcodeOpen = this.chkRDBROpen.Checked;
            rc.IsHandheldBarcodeOpen = this.chkHBROpen.Checked;
            if (textBox1.Text.Trim() == "" || textBox1.Text.Trim() == "0")
            {
                MessageBox.Show("样本条码长度不能为空，而且必须大于0！");
                return;
            }
            if (textBox2.Text.Trim() == "" || textBox2.Text.Trim() == "0")
            {
                MessageBox.Show("试剂条码长度不能为空，而且必须大于0！");
                return;
            }
            rc.SampleBracodeLength = Convert.ToInt32(textBox1.Text.Trim());
            rc.ReagentBarcodeLength = Convert.ToInt32(textBox2.Text.Trim());

            new BioA.Service.ReagentState().ISaveReagentConfigInfo(rc);
            ReagentConfigInfoConstrunction.ReagentConfig = null;
        }
        /// <summary>
        /// 保存试剂状态设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chekSaveSetting_CheckedChanged(object sender, EventArgs e)
        {
            ReagentStateInfo rs = new ReagentStateInfo();
            int checkValue1 = 0;
            int checkValue2 = 0;
            if (this.checkReagentOpen.Checked == true)
            {
                rs.ReagentStatusModule = 1;
            }
            if (this.checkReagentSemiBlocking.Checked == true)
            {
                rs.ReagentStatusModule = 2;
                checkValue1 = 31;
                if (this.checkEdit1.Checked == true)
                {
                    checkValue1 = checkValue1 & 31;
                }
                else
                {
                    checkValue1 = checkValue1 & 30;
                }

                if (this.checkEdit2.Checked == true)
                {
                    checkValue1 = checkValue1 & 31;
                }
                else
                {
                    checkValue1 = checkValue1 & 29;
                }

                if (this.checkEdit3.Checked == true)
                {
                    checkValue1 = checkValue1 & 31;
                }
                else
                {
                    checkValue1 = checkValue1 & 27;
                }

                if (this.checkEdit4.Checked == true)
                {
                    checkValue1 = checkValue1 & 31;
                }
                else
                {
                    checkValue1 = checkValue1 & 23;
                }

                if (this.checkEdit5.Checked == true)
                {
                    checkValue1 = checkValue1 & 31;
                }
                else
                {
                    checkValue1 = checkValue1 & 15;
                }
                //---checkValue2-------
                checkValue2 = 31;
                if (this.checkEdit6.Checked == true)
                {
                    checkValue2 = checkValue2 & 31;
                }
                else
                {
                    checkValue2 = checkValue2 & 30;
                }

                if (this.checkEdit7.Checked == true)
                {
                    checkValue2 = checkValue2 & 31;
                }
                else
                {
                    checkValue2 = checkValue2 & 29;
                }

                if (this.checkEdit8.Checked == true)
                {
                    checkValue2 = checkValue2 & 31;
                }
                else
                {
                    checkValue2 = checkValue2 & 27;
                }

                if (this.checkEdit9.Checked == true)
                {
                    checkValue2 = checkValue2 & 31;
                }
                else
                {
                    checkValue2 = checkValue2 & 23;
                }

                if (this.checkEdit10.Checked == true)
                {
                    checkValue2 = checkValue2 & 31;
                }
                else
                {
                    checkValue2 = checkValue2 & 15;
                }

            }
            rs.ReagentChannelNum1 = checkValue1;
            rs.ReagentChannelNum2 = checkValue2;
            new BioA.Service.ReagentState().IUpdateReagentStateInfo(rs);
            ReagentConfigInfoConstrunction.ReagentStateInfo = null;
        }
        
        /// <summary>
        /// 选择试剂半封闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkReagentSemiBlocking_Click(object sender, EventArgs e)
        {
            this.grpChannleNumber.Enabled = true;
        }
        /// <summary>
        /// 选择试剂开放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkReagentOpen_Click(object sender, EventArgs e)
        {
            this.grpChannleNumber.Enabled = false;
            this.CloseChanleNUmber();
        }

        private void CloseChanleNUmber()
        {
            this.checkEdit1.Checked = false;
            this.checkEdit2.Checked = false;
            this.checkEdit3.Checked = false;
            this.checkEdit4.Checked = false;
            this.checkEdit5.Checked = false;
            this.checkEdit6.Checked = false;
            this.checkEdit7.Checked = false;
            this.checkEdit8.Checked = false;
            this.checkEdit9.Checked = false;
            this.checkEdit10.Checked = false;
        }
    }

    public static class ReagentConfigInfoConstrunction
    {
        static ReagentConfigInfo reagentConfig;
        /// <summary>
        /// 单例模式：只会获取一次，如果条码配置信息有改动，也会重新获取一次
        /// </summary>
        public static ReagentConfigInfo ReagentConfig
        {
            get
            {
                if (reagentConfig == null)
                {
                    reagentConfig = new ReagentConfigInfo();
                    reagentConfig = new BioA.Service.ReagentState().IGetReagentConfigInfo();
                }
                return reagentConfig;
            }
            set
            {
                reagentConfig = value;
            }
        }

        static ReagentStateInfo reagentStateInfo;
        /// <summary>
        /// 单例模式：只会获取一次，如果试剂状态设置信息有改动，也会重新获取一次
        /// </summary>
        public static ReagentStateInfo ReagentStateInfo
        {
            get
            {
                if (reagentStateInfo == null)
                {
                    reagentStateInfo = new ReagentStateInfo();
                    reagentStateInfo = new ReagentStateSetting().ReagentNumbersList();
                }
                return reagentStateInfo;
            }
            set
            {
                reagentStateInfo = value;
            }
        }
    }
}
