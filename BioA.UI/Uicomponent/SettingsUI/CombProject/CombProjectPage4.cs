using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using DevExpress.XtraEditors;

namespace BioA.UI
{
    public partial class CombProjectPage4 : DevExpress.XtraEditors.XtraUserControl
    {
        public CombProjectPage4()
        {
            InitializeComponent();

            //projectPageInfo = projectPageEnum;

            
        }

        private List<string> lstProNamesForComb;
        /// <summary>
        /// 处理组合项目点击
        /// </summary>
        public List<string> LstProNamesForComb
        {
            get { return lstProNamesForComb; }
            set
            {
                lstProNamesForComb = value;

                foreach (string proName in lstProNamesForComb)
                {
                    foreach (Control c in this.Controls)
                    {
                        if (c.GetType() == typeof(System.Windows.Forms.Button) && c.Text == proName)
                        {
                            c.Tag = "1";

                            this.Invoke(new EventHandler(delegate { c.ForeColor = Color.Red; }));
                        }
                    }
                }
            }
        }

        private List<string> lstAssayProInfos = new List<string>();

        public List<string> LstAssayProInfos
        {
            get { return lstAssayProInfos; }
            set
            {
                lstAssayProInfos = value;
                this.Invoke(new EventHandler(delegate{
                    ResetControlState();
                    simpleButton1.Text = lstAssayProInfos.Count >= 97 ? lstAssayProInfos[96] : "";
                    simpleButton2.Text = lstAssayProInfos.Count >= 98 ? lstAssayProInfos[97] : "";
                    simpleButton3.Text = lstAssayProInfos.Count >= 99 ? lstAssayProInfos[98] : "";
                    simpleButton4.Text = lstAssayProInfos.Count >= 100 ? lstAssayProInfos[99] : "";
                    simpleButton5.Text = lstAssayProInfos.Count >= 101 ? lstAssayProInfos[100] : "";
                    simpleButton6.Text = lstAssayProInfos.Count >= 102 ? lstAssayProInfos[101] : "";
                    simpleButton7.Text = lstAssayProInfos.Count >= 103 ? lstAssayProInfos[102] : "";
                    simpleButton8.Text = lstAssayProInfos.Count >= 104 ? lstAssayProInfos[103] : "";
                    simpleButton9.Text = lstAssayProInfos.Count >= 105 ? lstAssayProInfos[104] : "";
                    simpleButton10.Text = lstAssayProInfos.Count >= 106 ? lstAssayProInfos[105] : "";
                    simpleButton11.Text = lstAssayProInfos.Count >= 107 ? lstAssayProInfos[106] : "";
                    simpleButton12.Text = lstAssayProInfos.Count >= 108 ? lstAssayProInfos[107] : "";
                    simpleButton13.Text = lstAssayProInfos.Count >= 109 ? lstAssayProInfos[108] : "";
                    simpleButton14.Text = lstAssayProInfos.Count >= 110 ? lstAssayProInfos[109] : "";
                    simpleButton15.Text = lstAssayProInfos.Count >= 111 ? lstAssayProInfos[110] : "";
                    simpleButton16.Text = lstAssayProInfos.Count >= 112 ? lstAssayProInfos[111] : "";
                    simpleButton17.Text = lstAssayProInfos.Count >= 113 ? lstAssayProInfos[112] : "";
                    simpleButton18.Text = lstAssayProInfos.Count >= 114 ? lstAssayProInfos[113] : "";
                    simpleButton19.Text = lstAssayProInfos.Count >= 115 ? lstAssayProInfos[114] : "";
                    simpleButton20.Text = lstAssayProInfos.Count >= 116 ? lstAssayProInfos[115] : "";
                    simpleButton21.Text = lstAssayProInfos.Count >= 117 ? lstAssayProInfos[116] : "";
                    simpleButton22.Text = lstAssayProInfos.Count >= 118 ? lstAssayProInfos[117] : "";
                    simpleButton23.Text = lstAssayProInfos.Count >= 119 ? lstAssayProInfos[118] : "";
                    simpleButton24.Text = lstAssayProInfos.Count >= 110 ? lstAssayProInfos[119] : "";
                    simpleButton25.Text = lstAssayProInfos.Count >= 121 ? lstAssayProInfos[110] : "";
                    simpleButton26.Text = lstAssayProInfos.Count >= 122 ? lstAssayProInfos[121] : "";
                    simpleButton27.Text = lstAssayProInfos.Count >= 123 ? lstAssayProInfos[122] : "";
                    simpleButton28.Text = lstAssayProInfos.Count >= 124 ? lstAssayProInfos[123] : "";
                    simpleButton29.Text = lstAssayProInfos.Count >= 125 ? lstAssayProInfos[124] : "";
                    simpleButton30.Text = lstAssayProInfos.Count >= 126 ? lstAssayProInfos[125] : "";
                    simpleButton31.Text = lstAssayProInfos.Count >= 127 ? lstAssayProInfos[126] : "";
                    simpleButton32.Text = lstAssayProInfos.Count >= 128 ? lstAssayProInfos[127] : "";
                }));
            }
                
        }

        private List<string> selectedProjects = new List<string>();

        public List<string> SelectedProjects
        {
            get { return selectedProjects; }
            set
            {
                selectedProjects = value;
                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(System.Windows.Forms.Button))
                    {
                        foreach (string str in selectedProjects)
                        {
                            if (control.Text == str)
                            {
                                control.Tag = "1";

                                this.Invoke(new EventHandler(delegate
                                {
                                    control.ForeColor = Color.Red;
                                }));

                            }
                        }


                    }
                }
            }
        }

        public List<string> GetSelectedProjects()
        {
            List<string> lstProInfos = new List<string>();

            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.Button))
                {
                    if (control.Tag.ToString() == "1")
                    {
                        if (control.Text != string.Empty)
                        {
                            lstProInfos.Add(control.Text);
                        }
                    }
                }
            }


            return lstProInfos;
        }

        public void ResetControlState()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.Button))
                {
                    if (control.Tag.ToString() == "1")
                    {
                        control.Tag = "0";
                        this.Invoke(new EventHandler(delegate
                        {
                            control.ForeColor = Color.Black;
                        }));

                    }
                }
            }
        }

        private void ProjectPage_Load(object sender, EventArgs e)
        {

        }


        public void InitialProjectPage(List<AssayProjectInfo> lstAssayProInfos)
        {
            //simpleButton12
        }

        private void simpleButton_Click(object sender, EventArgs e)
        {
            Button simpleButton = sender as Button;

            if (simpleButton.Tag as string == "1")
            {
                simpleButton.Tag = "0";

                simpleButton.ForeColor = Color.Black;
            }
            else if(simpleButton.Tag as string == "0")
            {
                simpleButton.Tag = "1";

                simpleButton.ForeColor = Color.Red;
            }
            else
            {
                simpleButton.Tag = "1";

                simpleButton.ForeColor = Color.Red;
            }
        }
    }
}
