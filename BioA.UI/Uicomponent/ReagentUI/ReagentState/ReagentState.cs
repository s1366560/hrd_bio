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
using BioA.Common;
using BioA.Common.IO;
using BioA.Common.Machine;

namespace BioA.UI
{
    public partial class ReagentState : DevExpress.XtraEditors.XtraUserControl
    {
        List<ReagentStateInfoR1R2> lstReagentStateInfo = new List<ReagentStateInfoR1R2>();


        public event SendNetworkCommandDelegate SendNetworkCommandEvent;

        public ReagentState()
        {
            InitializeComponent();
            Font font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            gridView1.Appearance.HeaderPanel.Font = font;
            gridView1.Appearance.Row.Font = font;
        }

     
        private void ReagentStateLoad()
        {
            CommunicationEntity Reagent = new CommunicationEntity();
            Reagent.StrmethodName = "QueryReagentState";
            Reagent.ObjParam = "";
            ReagentStateSend(Reagent);

            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
          
        }

        private void ReagentStateSend(object sender)
        {
            CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.ReagentState, XmlUtility.Serializer(typeof(CommunicationEntity), sender as CommunicationEntity));
        }
        private void ReagentState_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(ReagentStateLoad));
            
        }

     

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryReagentState":
                    lstReagentStateInfo = (List<ReagentStateInfoR1R2>)XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), sender as string);
                    InitialReagentStateInfo(lstReagentStateInfo);
                    break;
                case "LockReagentState":
                    string Count = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    ReagentStateLoad();                   
                    break;
                case "UnlockReagentState":
                    string Counts = (string)XmlUtility.Deserialize(typeof(string), sender as string);
                    ReagentStateLoad();
                    break;             
            }
        }

        private void InitialReagentStateInfo(List<ReagentStateInfoR1R2> ReagentStateInfo)
        {
            List<ReagentStateInfoR1R2> lisReagentStateInfo1 = new List<ReagentStateInfoR1R2>();
            List<ReagentStateInfoR1R2> lisReagentStateInfo2 = new List<ReagentStateInfoR1R2>();
            List<ReagentStateInfoR1R2> lisReagentStateInfo3 = new List<ReagentStateInfoR1R2>();

            for (int i = 0; i < ReagentStateInfo.Count;i++ )
            {
                if(ReagentStateInfo[i].ReagentType=="清洗剂")
                {
                    lisReagentStateInfo1.Add(ReagentStateInfo[i]);
                }
                else if (ReagentStateInfo[i].ReagentType2 == "清洗剂")
                {
                    lisReagentStateInfo2.Add(ReagentStateInfo[i]);
                }
                else
                {
                    lisReagentStateInfo3.Add(ReagentStateInfo[i]);
                }

            }
            this.Invoke(new EventHandler(delegate
            {
                gridControl1.RefreshDataSource();
                DataTable dt = new DataTable();
                dt.Columns.Add("项目名称");
                dt.Columns.Add("试剂1名称");
                dt.Columns.Add("试剂1可测数量");
                dt.Columns.Add("试剂1位置");
                dt.Columns.Add("试剂1类型");
                dt.Columns.Add("试剂1剩余容量ml");
                dt.Columns.Add("试剂1剩余容量%");
                
                dt.Columns.Add("试剂2名称");
                dt.Columns.Add("试剂2可测数量");
                dt.Columns.Add("试剂2位置");
                dt.Columns.Add("试剂2类型");
                dt.Columns.Add("试剂2剩余容量ml");
                dt.Columns.Add("试剂2剩余容量%");
                               dt.Columns.Add("是否锁定");

                foreach (ReagentStateInfoR1R2 reagentStateInfo in lisReagentStateInfo3)
                {
                    if (reagentStateInfo.ReagentName == "")
                    {
                        dt.Rows.Add(new object[] { reagentStateInfo.ProjectName, "", "", "", "", "","",
                            reagentStateInfo.ReagentName2,reagentStateInfo.ResidualQuantity2,reagentStateInfo.Pos2,reagentStateInfo.ReagentType2,
                            //reagentStateInfo.BatchNum2,
                            reagentStateInfo.ReagentResidualVol2,
                            reagentStateInfo.ValidPercent2 + "%",
                            reagentStateInfo.Locked == true ? "锁定" : "未锁定"                        });
                    }
                    else if (reagentStateInfo.ReagentName2 == "")
                    {
                        dt.Rows.Add(new object[] { reagentStateInfo.ProjectName,reagentStateInfo.ReagentName,reagentStateInfo.ResidualQuantity,reagentStateInfo.Pos,
                            reagentStateInfo.ReagentType,
                            //reagentStateInfo.BatchNum,
                            reagentStateInfo.ReagentResidualVol,reagentStateInfo.ValidPercent + "%",
                            "", "", "", "", "", "", reagentStateInfo.Locked == true ? "锁定" : "未锁定"
                        }); 
                    }
                    else
                    {
                        dt.Rows.Add(new object[] { reagentStateInfo.ProjectName,reagentStateInfo.ReagentName,reagentStateInfo.ResidualQuantity,reagentStateInfo.Pos,
                            reagentStateInfo.ReagentType,
                            //reagentStateInfo.BatchNum,
                            reagentStateInfo.ReagentResidualVol,reagentStateInfo.ValidPercent + "%",                            reagentStateInfo.ReagentName2,reagentStateInfo.ResidualQuantity2,reagentStateInfo.Pos2,reagentStateInfo.ReagentType2,
                            //reagentStateInfo.BatchNum2,
                            reagentStateInfo.ReagentResidualVol2,reagentStateInfo.ValidPercent2 + "%",
                            reagentStateInfo.Locked == true ? "锁定" : "未锁定"
                        });   
                    }
                                  
                }
                foreach (ReagentStateInfoR1R2 reagentStateInfo in lisReagentStateInfo1)
                {
                    dt.Rows.Add(new object[] { reagentStateInfo.ProjectName,reagentStateInfo.ReagentName,reagentStateInfo.ResidualQuantity,reagentStateInfo.Pos,
                        reagentStateInfo.ReagentType,
                        //reagentStateInfo.BatchNum,
                        reagentStateInfo.ReagentResidualVol,reagentStateInfo.ValidPercent + "%",
                        "","","","","",
                        "",reagentStateInfo.Locked == true ? "锁定" : "未锁定"                    });
                }
                foreach (ReagentStateInfoR1R2 reagentStateInfo in lisReagentStateInfo2)
                {
                    dt.Rows.Add(new object[] { "","","","",
                        "","","",
                        reagentStateInfo.ReagentName2,reagentStateInfo.ResidualQuantity2,reagentStateInfo.Pos2,reagentStateInfo.ReagentType2,
                        //reagentStateInfo.BatchNum2,
                        reagentStateInfo.ReagentResidualVol2,reagentStateInfo.ValidPercent2 + "%",
                        reagentStateInfo.Locked == true ? "锁定" : "未锁定"
                    });
                }


                gridControl1.DataSource = dt;
                this.gridView1.Columns[0].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[1].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[2].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[3].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[4].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[5].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[6].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[7].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[8].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[9].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[10].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[11].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[12].OptionsColumn.AllowEdit = false;
                this.gridView1.Columns[13].OptionsColumn.AllowEdit = false;               
            }));
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            gridView1.SelectAll();
        }
        
        private void btnLocking_Click(object sender, EventArgs e)
        {
            List<ReagentStateInfoR1R2> ReagentStateInfo = new List<ReagentStateInfoR1R2>();
            
            int [] aaa = this.gridView1.GetSelectedRows();
            for (int i=0;i<aaa.Length;i++)
            {
                ReagentStateInfoR1R2 reagentStateInfoR1R2 = new ReagentStateInfoR1R2();
                reagentStateInfoR1R2.ProjectName = this.gridView1.GetRowCellValue(aaa[i], "项目名称").ToString();
                reagentStateInfoR1R2.ReagentName = this.gridView1.GetRowCellValue(aaa[i], "试剂1名称").ToString();
                reagentStateInfoR1R2.Pos = this.gridView1.GetRowCellValue(aaa[i], "试剂1位置").ToString();
                reagentStateInfoR1R2.ReagentType = this.gridView1.GetRowCellValue(aaa[i], "试剂1类型").ToString();
                reagentStateInfoR1R2.ReagentName2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2名称").ToString();
                reagentStateInfoR1R2.Pos2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2位置").ToString();
                reagentStateInfoR1R2.ReagentType2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2类型").ToString();
                reagentStateInfoR1R2.Locked = true;
                ReagentStateInfo.Add(reagentStateInfoR1R2);
            }
            CommunicationEntity Reagent = new CommunicationEntity();
            Reagent.StrmethodName = "LockReagentState";
            Reagent.ObjParam = XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), ReagentStateInfo); 
            ReagentStateSend(Reagent);           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            List<ReagentStateInfoR1R2> ReagentStateInfo = new List<ReagentStateInfoR1R2>();

            int[] aaa = this.gridView1.GetSelectedRows();
            for (int i = 0; i < aaa.Length; i++)
            {
                ReagentStateInfoR1R2 reagentStateInfoR1R2 = new ReagentStateInfoR1R2();
                reagentStateInfoR1R2.ProjectName = this.gridView1.GetRowCellValue(aaa[i], "项目名称").ToString();
                reagentStateInfoR1R2.ReagentName = this.gridView1.GetRowCellValue(aaa[i], "试剂1名称").ToString();
                reagentStateInfoR1R2.Pos = this.gridView1.GetRowCellValue(aaa[i], "试剂1位置").ToString();
                reagentStateInfoR1R2.ReagentType = this.gridView1.GetRowCellValue(aaa[i], "试剂1类型").ToString();
                reagentStateInfoR1R2.ReagentName2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2名称").ToString();
                reagentStateInfoR1R2.Pos2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2位置").ToString();
                reagentStateInfoR1R2.ReagentType2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2类型").ToString();
                reagentStateInfoR1R2.Locked = true;
                ReagentStateInfo.Add(reagentStateInfoR1R2);
            }
            CommunicationEntity Reagent = new CommunicationEntity();
            Reagent.StrmethodName = "UnlockReagentState";
            Reagent.ObjParam = XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), ReagentStateInfo); ;
            ReagentStateSend(Reagent);
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int hand = e.RowHandle;//行号
            if (hand < 0)
            {
                return;
            }
            DataRow dr = gridView1.GetDataRow(hand);
            if (dr == null)
                return;
            string str = gridView1.GetRowCellValue(hand, "是否锁定").ToString();
            if (str=="True")
            {
                e.Appearance.ForeColor = Color.Red;//字体颜色
                e.Appearance.BackColor = Color.Linen;//行背景颜色
            }
        }
        private void btnReverseSelection_Click(object sender, EventArgs e)
        {
            int[] aaa = this.gridView1.GetSelectedRows();
            gridView1.SelectAll();
            for (int i = 0; i < aaa .Length; i++)
            {              
                gridView1.UnselectRow(aaa[i]);
            }
        }

        private void btnR1MarginDetection_Click(object sender, EventArgs e)
        {
            //异步方法调用
            BeginInvoke(new Action(ResidueR1));
        }
        /// <summary>
        /// 试剂1探测余量
        /// </summary>
        private void ResidueR1()
        {
            if (this.gridView1.SelectedRowsCount > 0)
            {
                if (SendNetworkCommandEvent != null)
                {
                    Command cmd = MachineInfo.GetCommandByName("RGTPanel1VolScan");

                    string pos = "";

                    for (int i = 0; i < this.gridView1.SelectedRowsCount; i++)
                    {
                        int selectedHandle = this.gridView1.GetSelectedRows()[i];
                        string position = this.gridView1.GetRowCellValue(selectedHandle, "试剂1位置").ToString().Trim();

                        if (position == "" || position == null)
                        {
                            break;
                        }
                        else
                        {
                            pos += position + "|";
                        }
                    }

                    if (pos == "")
                    {
                        return;
                    }



                    cmd.Para = "1:" + pos.TrimEnd('|');
                    cmd.State = 1;
                    //发送命令
                    SendNetworkCommandEvent(cmd);
                }
            }
            else
            {
                MessageBoxDraw.ShowMsg("请选择待检测余量的项目！", MsgType.Warning);
            }
        }

        private void btnR2MarginDetection_Click(object sender, EventArgs e)
        {
            //异步方法调用
            BeginInvoke(new Action(ResidueR2));
        }
        /// <summary>
        /// 探测试剂2余量
        /// </summary>
        private void ResidueR2() 
        {
            if (this.gridView1.SelectedRowsCount > 0)
            {
                if (SendNetworkCommandEvent != null)
                {
                    Command cmd = MachineInfo.GetCommandByName("RGTPanel2VolScan");

                    string pos = "";

                    for (int i = 0; i < this.gridView1.SelectedRowsCount; i++)
                    {
                        int selectedHandle = this.gridView1.GetSelectedRows()[i];
                        string position = this.gridView1.GetRowCellValue(selectedHandle, "试剂2位置").ToString().Trim();

                        if (position == "" || position == null)
                        {
                            break;
                        }
                        else
                        {
                            pos += position + "|";
                        }
                    }

                    if (pos == "")
                    {
                        return;
                    }


                    cmd.Para = "2:" + pos.TrimEnd('|');
                    cmd.State = 1;

                    SendNetworkCommandEvent(cmd);
                }
            }
            else
            {
                MessageBoxDraw.ShowMsg("请选择待检测余量的项目！", MsgType.Warning);
            }
        }
    }
}
