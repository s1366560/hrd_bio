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
using BioA.UI.ServiceReference1;
using System.ServiceModel;
using BioA.Common;
using BioA.Common.IO;

namespace BioA.UI.Uicomponent.SettingsUI.ChemicalParameter
{
    public partial class ChemicalParameter : DevExpress.XtraEditors.XtraUserControl
    {
        ProjectParameter projectParameter;
        RangeParameter rangeParameter;
        CalibrationParameter calibrationParameter;

        // windows service对象
        private BioAServiceClient serviceClient;
        
        // 传输接口类
        private ChemicalParamNotifyCallBack notifyCallBack;
        public ChemicalParameter()
        {
            InitializeComponent();
            // 连接服务器
            notifyCallBack = new ChemicalParamNotifyCallBack();
            serviceClient = new BioAServiceClient(new InstanceContext(notifyCallBack));
            // 注册客户端
            serviceClient.RegisterClient(XmlUtility.Serializer(typeof(ModuleInfo), ModuleInfo.SettingsChemicalParameter));

            projectParameter = new ProjectParameter();
            projectParameter.AssayProInfoEvent += AssayProInfo_Event;
            notifyCallBack.DataTransferEvent += DataTransfer_Event;
            
            rangeParameter = new RangeParameter();
            calibrationParameter = new CalibrationParameter();
            xtraTabPage1.Controls.Add(projectParameter);
        }

        /// <summary>
        /// 生化项目信息事件响应方法
        /// </summary>
        /// <param name="strAccessSqlMethod"></param>
        /// <param name="sender"></param>
        private void AssayProInfo_Event(object sender)
        {
            serviceClient.GetDataUsingDataContract(new CompositeType());
            if (sender.GetType() == typeof(CommunicationEntity1))
            {
                CommunicationEntity1 a = new CommunicationEntity1();
                a = sender as CommunicationEntity1;
                serviceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, a);
                return;
            }
            else if (sender.GetType() == typeof(CommunicationEntityThreeParam1))
            {
                CommunicationEntityThreeParam1 a = new CommunicationEntityThreeParam1();
                a.StrmethodName = ((CommunicationEntityThreeParam1)sender).StrmethodName;
                a.ObjParam = ((CommunicationEntityThreeParam1)sender).ObjParam;
                a.ObjLastestParam = ((CommunicationEntityThreeParam1)sender).ObjLastestParam;
                serviceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, a );
                return;
            }
            serviceClient.ClientSendMsgToService(ModuleInfo.SettingsChemicalParameter, new CommunicationEntity1());
        }

        /// <summary>
        /// 接收数据传到窗体
        /// </summary>
        /// <param name="strMethod"></param>
        /// <param name="sender"></param>
        private void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryAssayProAllInfo":
                    List<AssayProjectInfo> lstAssayProInfos = (List<AssayProjectInfo>)XmlUtility.Deserialize(typeof(List<AssayProjectInfo>), sender as string);
                    projectParameter.LstAssayProInfos = lstAssayProInfos;
                    break;
                default:
                    break;
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0 && xtraTabPage1.Controls.Contains(projectParameter)==false)
            {
                xtraTabPage1.Controls.Clear();
                xtraTabPage1.Controls.Add(projectParameter);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1 && xtraTabPage1.Controls.Contains(calibrationParameter) == false)
            {
                xtraTabPage2.Controls.Clear();
                xtraTabPage2.Controls.Add(calibrationParameter);
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2 && xtraTabPage1.Controls.Contains(rangeParameter) == false)
            {
                xtraTabPage3.Controls.Clear();
                xtraTabPage3.Controls.Add(rangeParameter);
            }
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
