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

namespace BioA.UI.Uicomponent.SettingsUI.CalculatelItem
{
    public partial class ComputationItem : DevExpress.XtraEditors.XtraUserControl
    {
        AddOrEditCalcItem addOrEditCalcItem;
        public ComputationItem()
        {
            InitializeComponent();
            addOrEditCalcItem = new AddOrEditCalcItem();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addOrEditCalcItem.Text = "添加计算项目";
            addOrEditCalcItem.StartPosition = FormStartPosition.CenterScreen;
            addOrEditCalcItem.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            addOrEditCalcItem.Text = "编辑计算项目";
            addOrEditCalcItem.StartPosition = FormStartPosition.CenterScreen;
            addOrEditCalcItem.ShowDialog();
        }
    }
}
