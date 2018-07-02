using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using BioA.Common.IO;

namespace BioA.UI.Uicomponent.Analog
{
    public partial class SetScheduleWork : Form
    {
        List<string> lstPanel = new List<string>();
        public SetScheduleWork(object v)
        {
            string[] str = v.ToString().Split('|');
            for (int i = 0; i < str.Length; i++)
            {
                if (!string.IsNullOrEmpty(str[i]))
                {
                    lstPanel.Add(str[i]);
                }
            }
            InitializeComponent();
        }

        private void SetScheduleWork_Load(object sender, EventArgs e)
        {
            this.Invoke(new EventHandler(delegate
                {
                    comboPanelNum.Items.AddRange(lstPanel.ToArray());
                }));
                    
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strPanel = comboPanelNum.Text;
            if (!string.IsNullOrEmpty(strPanel)) 
            {
                if (MessageBoxDraw.ShowMsg("确认修改工作盘号？", MsgType.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("UpdateRunningTaskWorDisk", strPanel)));
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("请选择您要修改的工作盘号！");
                return;
            }
        }
    }
}
