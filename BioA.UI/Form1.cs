using BioA.UI.Uicomponent;
using BioA.UI.Uicomponent.CalibrationUI.CalibMaintain;
using BioA.UI.Uicomponent.CalibrationUI.CalibrationState;
using BioA.UI.Uicomponent.QualityControlUI.QCMaintain;
using BioA.UI.Uicomponent.QualityControlUI.QCState;
using BioA.UI.Uicomponent.ReagentUI.ReagentSetting;
using BioA.UI.Uicomponent.ReagentUI.ReagentState;
using BioA.UI.Uicomponent.SettingsUI.CalculatelItem;
using BioA.UI.Uicomponent.SettingsUI.ChemicalParameter;
using BioA.UI.Uicomponent.SettingsUI.CrossPollution;
using BioA.UI.Uicomponent.SettingsUI.DataConfig;
using BioA.UI.Uicomponent.SettingsUI.Environment;
using BioA.UI.Uicomponent.SettingsUI.LISCommunicate;
using BioA.UI.Uicomponent.SettingsUI.Portfolio;
using BioA.UI.Uicomponent.SystemUI.Configure;
using BioA.UI.Uicomponent.SystemUI.DepartmentManage;
using BioA.UI.Uicomponent.SystemUI.EquipmentManage;
using BioA.UI.Uicomponent.SystemUI.LogCheck;
using BioA.UI.Uicomponent.SystemUI.Maintenance;
using BioA.UI.Uicomponent.SystemUI.UserManagement;
using BioA.UI.Uicomponent.SystemUI.VersionInformation;
using BioA.UI.Uicomponent.WorkingAreaUI.ApplyTask;
using BioA.UI.Uicomponent.WorkingAreaUI.CalibDataCheck;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BioA.UI
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        DataCheck dadtCheck;
        CalibDataCheck calibDataCheck;
        ApplyTask applyTask;
        ReagentState reagentState;
        ReagentSetting reagentSetting;
        CalibMaintain calibMaintain;
        CalibrationState calibrationState;
        QCMaintain qCMaintain;
        QualityControlState qualityControlState;
        ChemicalParameter chemicalParameter;
        CombinationItem portfolio;
        ComputationItem computationlItem;
        EnvironmentData environments;
        CrossPollution crossPollution;
        LISCommunicate lISCommunicate;
        DataConfig dataConfig;
        RMThirdMenu rMThirdMenu;
        TestEquipment testEquipment;
        UserManagement userManagement;
        DepartmentManage departmentManage;
        Configure configure;
        VersionInformation versionInformation;
        Log log;
       
        
        public Form1()
        {
            InitializeComponent();
            dadtCheck = new DataCheck();
            calibDataCheck=new CalibDataCheck ();
            applyTask = new ApplyTask();
            reagentState = new ReagentState();
            reagentSetting = new ReagentSetting();
            calibMaintain = new CalibMaintain();
            calibrationState = new CalibrationState();
            qCMaintain = new QCMaintain();
            qualityControlState=new QualityControlState() ;
            chemicalParameter = new ChemicalParameter();
            portfolio = new CombinationItem();
            computationlItem = new ComputationItem();
            environments = new EnvironmentData();
            crossPollution = new CrossPollution();
            lISCommunicate = new LISCommunicate();
            dataConfig = new DataConfig();
            rMThirdMenu = new RMThirdMenu();
            testEquipment = new TestEquipment();
            userManagement = new UserManagement();
            departmentManage = new DepartmentManage();
            configure = new Configure();
            log = new Log();
            versionInformation = new VersionInformation();

            init();
            //Rectangle rt = this.progressBar1.ClientRectangle;
            //MessageBox.Show(rt.Height.ToString());
        }
        private void init()
        {
            this.WindowState = FormWindowState.Maximized;  //窗口最大化
            this.FormBorderStyle = FormBorderStyle.None; //状态栏没有
            this.CloseBox = false;                      //关闭按钮
            this.MaximizeBox = false;                   //最大化按钮
            this.MinimizeBox = false;                   //最小化按钮

            //DataCheck apply = new DataCheck();
            //this.panelControl1.Controls.Add(apply);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement2,
            this.accordionControlElement3,
            
            });
           // Rectangle rt = this.panelControl1.ClientRectangle;
           
         //  apply.ClientRectangle.Size = rt.Size;
           // apply.Location = new Point(100, 120);
          
           
            
        }

       /* private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement2,
            this.accordionControlElement3
            });
        }*/

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement4,
            this.accordionControlElement5
           
            });
            if (panelControl1.Controls.Equals(reagentState) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(reagentState);

            }

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement6,
            this.accordionControlElement7
           
            });
            if (panelControl1.Controls.Equals(calibMaintain) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(calibMaintain);

            }
            
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1,
            this.accordionControlElement2,
            this.accordionControlElement3
            });
            if (panelControl1.Controls.Equals(applyTask) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(applyTask);

            }
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {        
            if (panelControl1.Controls.Equals(dadtCheck)==false)
            {
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(dadtCheck);
          
            }          
        }
        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(applyTask) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(applyTask);

            }
        }
        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(calibDataCheck) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(calibDataCheck);

            }   
        }
        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(reagentState) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(reagentState);

            }
        }
        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(reagentSetting) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(reagentSetting);

            }
        }
        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(calibMaintain) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(calibMaintain);

            }
        }
        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(calibrationState) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(calibrationState);

            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement8,
            this.accordionControlElement9
           
            });
            if (panelControl1.Controls.Equals(qualityControlState) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(qualityControlState);

            }
        }
        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(qualityControlState) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(qualityControlState);

            }
        }
        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(qCMaintain) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(qCMaintain);

            }
        }
        private void accordionControlElement10_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(chemicalParameter) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(chemicalParameter);

            }
        }
        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(portfolio) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(portfolio);

            }
        }
        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(computationlItem) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(computationlItem);

            }
        }
        private void accordionControlElement13_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(environments) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(environments);

            }
        }
        private void accordionControlElement14_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(qCMaintain) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(crossPollution);

            }
        }
        private void accordionControlElement15_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(dataConfig) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(dataConfig);

            }
        }
        private void accordionControlElement16_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(lISCommunicate) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(lISCommunicate);

            }
        }
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement10,
            this.accordionControlElement11,
            this.accordionControlElement12,
            this.accordionControlElement13,
            this.accordionControlElement14,
            this.accordionControlElement15,
            this.accordionControlElement16,
           
            });
            if (panelControl1.Controls.Equals(chemicalParameter) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(chemicalParameter);

            }

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.accordionControl1.Elements.Clear();
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement17,
            this.accordionControlElement18,
            this.accordionControlElement19,
            this.accordionControlElement20,
            this.accordionControlElement21,
            this.accordionControlElement22,
            this.accordionControlElement23,
           
            });
        }

        private void accordionControlElement17_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(rMThirdMenu) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(rMThirdMenu);

            }
        }

        private void accordionControlElement18_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(testEquipment) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(testEquipment);

            }
        }

        private void accordionControlElement19_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(userManagement) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(userManagement);

            }
        }

        private void accordionControlElement20_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(departmentManage) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(departmentManage);

            }
        }

        private void accordionControlElement21_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(configure) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(configure);

            }
        }

        private void accordionControlElement22_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(log) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(log);

            }
        }

        private void accordionControlElement23_Click(object sender, EventArgs e)
        {
            if (panelControl1.Controls.Equals(versionInformation) == false)
            {
                panelControl1.Controls.Clear();
                panelControl1.Controls.Add(versionInformation);

            }
        }
       // private void ribbonControl1_Click(object sender, EventArgs e)
       // {

//}

        //private void ribbonPageGroup1_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
        //{
           // MessageBox.Show("hahahaha");
//        }

    //    private void ribbonPageGroup1_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e)
    //   {
    //        MessageBox.Show("test");
    //  }
      /*  protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x112)
            {
                switch ((int)m.WParam)
                {
                    //禁止双击标题栏关闭窗体
                    case 0xF063:
                    case 0xF093:
                        m.WParam = IntPtr.Zero;
                        break;

                    //禁止拖拽标题栏还原窗体
                    case 0xF012:
                    case 0xF010:
                        m.WParam = IntPtr.Zero;
                        break;

                    //禁止双击标题栏
                    case 0xf122:
                        m.WParam = IntPtr.Zero;
                        break;

                    //禁止关闭按钮
                    case 0xF060:
                        m.WParam = IntPtr.Zero;
                        break;

                    //禁止最大化按钮
                    case 0xf020:
                        m.WParam = IntPtr.Zero;
                        break;

                    //禁止最小化按钮
                    case 0xf030:
                        m.WParam = IntPtr.Zero;
                        break;

                    //禁止还原按钮
                    case 0xf120:
                        m.WParam = IntPtr.Zero;
                        break;
                }
            }
            base.WndProc(ref m);
        }*/
    }
}
