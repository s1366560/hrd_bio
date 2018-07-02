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
    public partial class frmHisResultChart : DevExpress.XtraEditors.XtraForm
    {
        public frmHisResultChart()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void btnCloes_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}