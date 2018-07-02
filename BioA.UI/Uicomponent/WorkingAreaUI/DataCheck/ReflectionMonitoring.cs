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

namespace BioA.UI.Uicomponent.WorkingAreaUI.DataCheck
{
    public partial class ReflectionMonitoring : DevExpress.XtraEditors.XtraForm
    {
        public ReflectionMonitoring()
        {
            InitializeComponent();
            this.ControlBox = false; 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}