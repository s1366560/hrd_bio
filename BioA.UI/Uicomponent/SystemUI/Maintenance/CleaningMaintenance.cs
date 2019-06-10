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
using BioA.Common.Machine;

namespace BioA.UI
{
    public partial class CleaningMaintenance : DevExpress.XtraEditors.XtraUserControl
    {
        public event SendNetworkDelegate SendNetworkEvent;

        public event SendMaintenanceNameDelegate SendMaintenanceNameEvent;

        public CleaningMaintenance()
        {
            InitializeComponent();
            
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {


            string strSender = "";
            Subsystem ConfigureInfo = MachineInfo.SubsystemList.Find(str => str.Name == "Common");
            MessageBoxButtons but = MessageBoxButtons.OKCancel;
            DialogResult result = DialogResult.No;
            switch (((SimpleButton)sender).Name)
            {
                case "btnCleanSN":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnCleanSN.Text).Name;
                    result = MessageBox.Show("确定进行清洗样本针吗?", "清洗样本针", but);
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                    break;
                case "btnCleanSystem":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnCleanSystem.Text).Name;
                    result = MessageBox.Show("确定进行系统清洗吗?", "清洗系统", but);
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                    break;
                case "btnWaterExchange":
                    strSender = ConfigureInfo.ComponetList.Find(componet => componet.Name == "Maintance").CommandList.Find(command => command.FullName == btnWaterExchange.Text).Name;
                    result = MessageBox.Show("确定进行孵育槽水交换吗?", "孵育槽水交换确认", but);
                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                    break;
                default:
                    break;
            }
            if (SendNetworkEvent != null && strSender != "")
            {
                SendNetworkEvent(strSender);
            }
            if(SendMaintenanceNameEvent != null)
            {
                SendMaintenanceNameEvent(((SimpleButton)sender).Text);
            }
        }

        public void CleaningMaintenance_Load(object sender, EventArgs e)
        {
            rtxtCleanSampleNeedle.Text = "";
            rtxtCleanSystem.Text = "";
            rtxtCleanSystemWarn.Text = "";
            rtxtWaterExchange.Text = "";
            this.loadCleaningMaintenance();
        }
        private void loadCleaningMaintenance()
        {
            rtxtCleanSampleNeedle.ReadOnly = true;
            rtxtCleanSampleNeedle.AppendText(
            "步骤：\r\n\r\n    1、在样本盘的S28位置放入清洗液。\r\n\r\n    2、单击[开始清洗]按钮分析仪将开始执行清洗操作。");



            rtxtCleanSystem.ReadOnly = true;
            rtxtCleanSystem.AppendText(
            "步骤：\r\n\r\n    1、在试剂盘1位置29装载清洗液A。\r\n\r\n    2、在试剂盘2位置29装载清洗液A。\r\n\r\n    3、在样本位置1放入清洗液A。\r\n\r\n    4、在试剂盘1位置28装载清洗液B。\r\n\r\n    5、在试剂盘2位置28装载清洗液B。\r\n\r\n    6、在样本位置2放入清洗液B。\r\n\r\n    7、单击[开机清洗]按钮分析仪将开始执行清洗操作。");


            rtxtCleanSystemWarn.ReadOnly = true;
            rtxtCleanSystemWarn.ForeColor = Color.Red;
            rtxtCleanSystemWarn.AppendText(
            "注意：\r\n\r\n    1、不要将清洗液A和清洗液B混合。\r\n\r\n    2、不要将清洗液A和清洗液B的位置混淆。\r\n\r\n    3、不要将清洗液溅到分析仪上。");

            rtxtWaterExchange.ReadOnly = true;
            rtxtWaterExchange.AppendText(
            "步骤：\r\n\r\n    1、在试剂盘2的30号位置装载抑菌剂。\r\n\r\n    2、水交换时间有点长，请勿随便关闭机器。\r\n\r\n    2、单击[开始水交换]按钮分析仪将开始执行水交换操作。");

            List<Subsystem> lstConfigureInfo = MachineInfo.SubsystemList;
            foreach (Subsystem sub in lstConfigureInfo)
            {
                if (sub.Name == "Common")
                {
                    btnCleanSN.Text = sub.ComponetList[1].CommandList[1].FullName;
                    btnCleanSystem.Text = sub.ComponetList[1].CommandList[4].FullName;
                    btnWaterExchange.Text = sub.ComponetList[1].CommandList[20].FullName;
                }
            }
        }
    }
}
