using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.UI.ServiceReference1;
using BioA.Common;
using BioA.Common.IO;
using BioA.Common.Machine;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace BioA.UI
{
    public partial class ReagentState : DevExpress.XtraEditors.XtraUserControl
    {
        List<ReagentStateInfoR1R2> lstReagentStateInfo = new List<ReagentStateInfoR1R2>();
        /// <summary>
        /// 存储客户端给服务器传递信息集合
        /// </summary>
        private Dictionary<string, object[]> reagentDictionary = new Dictionary<string, object[]>();
        /// <summary>
        /// 保存试剂信息
        /// </summary>
        DataTable dt = new DataTable();
        /// <summary>
        /// 探测试剂余量事件
        /// </summary>
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
            //reagentDictionary.Clear();
            //reagentDictionary.Add("QueryReagentState", new object[] { "" });
            lstReagentStateInfo = new BioA.Service.ReagentState().QueryReagentStateInfo("QueryReagentState", "");
            BeginInvoke(new Action(() =>
            {
                InitialReagentStateInfo(lstReagentStateInfo);
            }));
            //ReagentStateSend(reagentDictionary);
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.OptionsSelection.ResetSelectionClickOutsideCheckboxSelector = true;
        }

        private void ReagentStateSend(Dictionary<string, object[]> sender)
        {
            var threadReagentState = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.ReagentState, sender);
            });
            threadReagentState.IsBackground = true;
            threadReagentState.Start();
        }
        private void ReagentState_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(ReagentStateLoad));
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
            gridReagentState.DataSource = dt;
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
        }

     

        public void DataTransfer_Event(string strMethod, object sender)
        {
            switch (strMethod)
            {
                case "QueryReagentState":
                    lstReagentStateInfo = (List<ReagentStateInfoR1R2>)XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), sender as string);
                    BeginInvoke(new Action(() =>
                    {
                        InitialReagentStateInfo(lstReagentStateInfo);
                    }));
                    break;
                case "LockReagentState":
                    lstReagentStateInfo.Clear();
                    lstReagentStateInfo = (List<ReagentStateInfoR1R2>)XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), sender as string);
                    if (lstReagentStateInfo.Count == 0)
                        MessageBox.Show("设置试剂状态失败！");
                    else
                        BeginInvoke(new Action(() =>
                        {
                            InitialReagentStateInfo(lstReagentStateInfo);
                        }));
                    break;
                case "UnlockReagentState":
                    lstReagentStateInfo.Clear();
                    lstReagentStateInfo = (List<ReagentStateInfoR1R2>)XmlUtility.Deserialize(typeof(List<ReagentStateInfoR1R2>), sender as string);
                    if (lstReagentStateInfo.Count == 0)
                        MessageBox.Show("设置试剂状态失败！");
                    else
                        BeginInvoke(new Action(() =>
                        {
                            InitialReagentStateInfo(lstReagentStateInfo);
                        }));
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
                gridReagentState.RefreshDataSource();

                dt.Rows.Clear();
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
                            reagentStateInfo.ReagentResidualVol,reagentStateInfo.ValidPercent + "%",
                            reagentStateInfo.ReagentName2,reagentStateInfo.ResidualQuantity2,reagentStateInfo.Pos2,reagentStateInfo.ReagentType2,
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
            }));
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            gridView1.SelectAll();
        }
        /// <summary>
        /// 锁定状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLocking_Click(object sender, EventArgs e)
        {
            if(this.gridView1.GetSelectedRows().Count() == 0)
            {
                MessageBoxDraw.ShowMsg("请选择一条记录！",MsgType.OK);
                return;
            }
            List<ReagentStateInfoR1R2> ReagentStateInfo = new List<ReagentStateInfoR1R2>();
            
            int [] aaa = this.gridView1.GetSelectedRows();
            for (int i=0;i<aaa.Length;i++)
            {
                ReagentStateInfoR1R2 reagentStateInfoR1R2 = new ReagentStateInfoR1R2();
                reagentStateInfoR1R2.ProjectName = this.gridView1.GetRowCellValue(aaa[i], "项目名称").ToString();
                reagentStateInfoR1R2.ReagentName = this.gridView1.GetRowCellValue(aaa[i], "试剂1名称").ToString();
                reagentStateInfoR1R2.ReagentType = this.gridView1.GetRowCellValue(aaa[i], "试剂1类型").ToString();
                reagentStateInfoR1R2.ReagentName2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2名称").ToString();
                reagentStateInfoR1R2.ReagentType2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2类型").ToString();
                reagentStateInfoR1R2.Locked = true;
                ReagentStateInfo.Add(reagentStateInfoR1R2);
            }
            
            lstReagentStateInfo.Clear();
            lstReagentStateInfo = new BioA.Service.ReagentState().UpdataReagentStateInfo("LockReagentState", ReagentStateInfo);
            if (lstReagentStateInfo.Count == 0)
                MessageBox.Show("设置试剂状态失败！");
            else
            {
                foreach(int r in aaa)
                {
                    dt.Rows[r][13] = "锁定";
                }
                gridReagentState.RefreshDataSource();
            }
                //BeginInvoke(new Action(() =>
                //{
                //   InitialReagentStateInfo(lstReagentStateInfo);
                //}));

            //reagentDictionary.Clear();
            //reagentDictionary.Add("LockReagentState", new object[] { XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), ReagentStateInfo) });

            //ReagentStateSend(reagentDictionary);           
        }
        /// <summary>
        /// 解锁状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deblocking_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() == 0)
            {
                MessageBoxDraw.ShowMsg("请选择一条记录！", MsgType.OK);
                return;
            }
            List<ReagentStateInfoR1R2> ReagentStateInfo = new List<ReagentStateInfoR1R2>();

            int[] aaa = this.gridView1.GetSelectedRows();
            for (int i = 0; i < aaa.Length; i++)
            {
                ReagentStateInfoR1R2 reagentStateInfoR1R2 = new ReagentStateInfoR1R2();
                reagentStateInfoR1R2.ProjectName = this.gridView1.GetRowCellValue(aaa[i], "项目名称").ToString();
                reagentStateInfoR1R2.ReagentName = this.gridView1.GetRowCellValue(aaa[i], "试剂1名称").ToString();
                reagentStateInfoR1R2.ReagentType = this.gridView1.GetRowCellValue(aaa[i], "试剂1类型").ToString();
                reagentStateInfoR1R2.ReagentName2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2名称").ToString();
                reagentStateInfoR1R2.ReagentType2 = this.gridView1.GetRowCellValue(aaa[i], "试剂2类型").ToString();
                reagentStateInfoR1R2.Locked = true;
                ReagentStateInfo.Add(reagentStateInfoR1R2);
            }

            lstReagentStateInfo.Clear();
            lstReagentStateInfo = new BioA.Service.ReagentState().UpdataUnlockReagentState("UnlockReagentState", ReagentStateInfo);
            if (lstReagentStateInfo.Count == 0)
                MessageBox.Show("设置试剂状态失败！");
            else
            {
                foreach (int r in aaa)
                {
                    dt.Rows[r][13] = "未锁定";
                }
                gridReagentState.RefreshDataSource();
            }
                //BeginInvoke(new Action(() =>
                //{
                //    InitialReagentStateInfo(lstReagentStateInfo);
                //}));

            //reagentDictionary.Clear();
            //reagentDictionary.Add("UnlockReagentState", new object[] { XmlUtility.Serializer(typeof(List<ReagentStateInfoR1R2>), ReagentStateInfo) });
            //ReagentStateSend(reagentDictionary);
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
                    var ResidueR1Thread = new Thread(() =>
                    {
                        //发送命令
                        SendNetworkCommandEvent(cmd);
                    });
                    ResidueR1Thread.IsBackground = true;
                    ResidueR1Thread.Start();
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

                    BeginInvoke(new Action(() =>
                    {
                        //发送命令
                        SendNetworkCommandEvent(cmd);
                    }));
                }
            }
            else
            {
                MessageBoxDraw.ShowMsg("请选择待检测余量的项目！", MsgType.Warning);
            }
        }

        //私有成员变量
        private int hotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        /// <summary>
        /// 存储测试点下面每一行的句柄
        /// </summary>
        private int HotTrackRow
        {
            get
            {
                return hotTrackRow;
            }
            set
            {
                if (hotTrackRow != value)

                {
                    int prevHotTrackRow = hotTrackRow;

                    hotTrackRow = value;
                    gridView1.RefreshRow(prevHotTrackRow);

                    gridView1.RefreshRow(hotTrackRow);
                    if (hotTrackRow >= 0)
                        gridReagentState.Cursor = Cursors.Hand;

                    else
                        gridReagentState.Cursor = Cursors.Default;
                }
            }
        }
        //鼠标滑过gridview时，鼠标所指行显示浅蓝色
        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle == HotTrackRow)
                e.Appearance.BackColor = Color.CornflowerBlue;
            int hand = e.RowHandle;//行号
            if (hand < 0)
            {
                return;
            }
            DataRow dr = gridView1.GetDataRow(hand);
            if (dr == null)
                return;
            string str = gridView1.GetRowCellValue(hand, "是否锁定").ToString();
            if (str == "True")
            {
                e.Appearance.ForeColor = Color.Red;//字体颜色
                e.Appearance.BackColor = Color.Linen;//行背景颜色
            }
        }
        //获取指定点的gridview视图坐标信息
        private void gridView1_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(new Point(e.X, e.Y));
            if (info.InRowCell)
                HotTrackRow = info.RowHandle;
            else
                HotTrackRow = DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }
    }
}
