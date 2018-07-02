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

namespace BioA.UI.Uicomponent.SystemUI.LogCheck
{
    public partial class Log : DevExpress.XtraEditors.XtraUserControl
    {
        
        public Log()
        {
            InitializeComponent();
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                Alertlog alertlog = new Alertlog();
                alertlog.btnRemove();
                xtraTabPage1.Controls.Add(alertlog);

            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                Alertlog alertlog = new Alertlog();
                alertlog.btnRemove();
                xtraTabPage2.Controls.Add(alertlog);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                Alertlog alertlog = new Alertlog();
                alertlog.btnRemove();
                xtraTabPage3.Controls.Add(alertlog);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 3)
            {
                Alertlog alertlog = new Alertlog();
                xtraTabPage4.Controls.Add(alertlog);
            }
           
        }
    }
}
