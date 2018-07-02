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

namespace BioA.UI.Uicomponent.SystemUI.Maintenance
{
    public partial class CleaningMaintenance : DevExpress.XtraEditors.XtraUserControl
    {
        public CleaningMaintenance()
        {
            InitializeComponent();
            rtxtCleanSampleNeedle.ReadOnly = true;
            rtxtCleanSampleNeedle.AppendText(
            "步骤：\r\n\r\n    1、在样本盘的ISE2位置放入清洗液。\r\n\r\n    2、单击[开始清洗]按钮分析仪将开始执行清洗操作。");



            rtxtCleanSystem.ReadOnly = true;
            rtxtCleanSystem.AppendText(
            "步骤：\r\n\r\n    1、在试剂盘1位置29装载清洗液A。\r\n\r\n    2、在试剂盘2位置29装载清洗液A。\r\n\r\n    3、在样本位置1放入清洗液A。\r\n\r\n    4、在试剂盘1位置28装载清洗液B。\r\n\r\n    5、在试剂盘2位置28装载清洗液B。\r\n\r\n    6、在样本位置2放入清洗液B。\r\n\r\n    7、单击[开机清洗]按钮分析仪将开始执行清洗操作。");


            rtxtCleanSystemWarn.ReadOnly = true;
            rtxtCleanSystemWarn.ForeColor = Color.Red;
            rtxtCleanSystemWarn.AppendText(
            "注意：\r\n\r\n    1、不要将清洗液A和清洗液B混合。\r\n\r\n    2、不要将清洗液A和清洗液B的位置混淆。\r\n\r\n    3、不要将清洗液溅到分析仪上。");
    

        }
    }
}
