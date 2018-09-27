using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BioA.Common;
using BioA.Common.IO;
using System.Threading;

namespace BioA.UI
{
    public partial class AnologSamplePanel : Form
    {
        /// <summary>
        /// 存储客户端发送信息给服务器
        /// </summary>
        private static Dictionary<string, object[]> anologSampDic = new Dictionary<string, object[]>();
        public AnologSamplePanel(string PanelNumPanelNum)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            labWorkDiskNum.Text = PanelNumPanelNum;
            cboPanelNum.SelectedIndex = 0;
            paintState();
            
        }

        private TaskInfoForSamplePanelInfo taskInfoForSamPanel;

        public TaskInfoForSamplePanelInfo TaskInfoForSamPanel
        {
            get { return taskInfoForSamPanel; }
            set 
            { 
                taskInfoForSamPanel = value;
                this.BeginInvoke(new EventHandler(delegate { 
                    if (taskInfoForSamPanel != null)
                    {
                        txtSampleNum.Text = taskInfoForSamPanel.SampleNum.ToString();
                        txtSamplePos.Text = taskInfoForSamPanel.SamplePos;
                        txtSampleInspectInfo.Text = string.Format("样本编号 {0}{1}样本位置 {2}{3}样本类型 {4}{5}", 
                            taskInfoForSamPanel.SampleNum.ToString(), System.Environment.NewLine, taskInfoForSamPanel.SamplePos, System.Environment.NewLine,
                            taskInfoForSamPanel.SampleType, System.Environment.NewLine + System.Environment.NewLine);

                        List<SampleResultInfo> lstTaskResInfos = XmlUtility.Deserialize(typeof(List<SampleResultInfo>), taskInfoForSamPanel.InspectInfos) as List<SampleResultInfo>;

                        foreach (SampleResultInfo s in lstTaskResInfos)
                        {
                            string strValueCompare = "";
                            string unit = "";
                            if (s.UnitAndRange.Contains("("))
                            {
                                unit = s.UnitAndRange.Substring(0, s.UnitAndRange.IndexOf('(') + 1);
                                float lowestValue = (float)System.Convert.ToDouble(s.UnitAndRange.Substring(s.UnitAndRange.IndexOf('(') + 1, s.UnitAndRange.IndexOf('—') - s.UnitAndRange.IndexOf('(')));
                                float highestValue = (float)System.Convert.ToDouble(s.UnitAndRange.Substring(s.UnitAndRange.IndexOf('—') + 1, s.UnitAndRange.IndexOf('）') - s.UnitAndRange.IndexOf('—')));

                                if (s.ConcResult < lowestValue)
                                    strValueCompare = "(L)";
                                else if (s.ConcResult > highestValue)
                                    strValueCompare = "(H)";
                                else
                                    strValueCompare = "(M)";
                                    
                            }
                            else
                            {
                                unit = s.UnitAndRange;
                            }
                            txtSampleInspectInfo.Text += string.Format("{0}     {1}     {2}     {3}{4}", s.ProjectName, strValueCompare, s.ConcResult, unit, System.Environment.NewLine);
                        }
                    }
                }));
            }
        }

        private List<SampleInfo> lstSampleInfo = new List<SampleInfo>();

        public List<SampleInfo> LstSampleInfo
        {
            get { return lstSampleInfo; }
            set 
            { 
                lstSampleInfo = value; 
                foreach (SampleInfo s in lstSampleInfo)
                {
                    switch (s.SamplePos)
                    {
                        case 1:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox1.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox1.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox1.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox1.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox1.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox1.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 2:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox2.Image = global::BioA.UI.Properties.Resources.White;
                                    break;    
                                case 1:       
                                    pictureBox2.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;    
                                case 2:       
                                    pictureBox2.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;    
                                case 3:       
                                    pictureBox2.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;    
                                case 4:       
                                    pictureBox2.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;    
                                case 5:       
                                    pictureBox2.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 3:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox3.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox3.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox3.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox3.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox3.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox3.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 4:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox4.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox4.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox4.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox4.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox4.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox4.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 5:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox5.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox5.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox5.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox5.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox5.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox5.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 6:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox6.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox6.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox6.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox6.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox6.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox6.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 7:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox7.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox7.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox7.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox7.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox7.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox7.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 8:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox8.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox8.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox8.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox8.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox8.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox8.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 9:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox9.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox9.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox9.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox9.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox9.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox9.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 10:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox10.Image = global::BioA.UI.Properties.Resources.White;
                                    break;   
                                case 1:      
                                    pictureBox10.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;    
                                case 2:       
                                    pictureBox10.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;    
                                case 3:       
                                    pictureBox10.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;    
                                case 4:       
                                    pictureBox10.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;    
                                case 5:       
                                    pictureBox10.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 11:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox11.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox11.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox11.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox11.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox11.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox11.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 12:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox12.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox12.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox12.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox12.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox12.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox12.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 13:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox13.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox13.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox13.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox13.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox13.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox13.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 14:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox14.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox14.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox14.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox14.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox14.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox14.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 15:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox15.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox15.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox15.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox15.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox15.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox15.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 16:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox16.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox16.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox16.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox16.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox16.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox16.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 17:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox17.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox17.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox17.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox17.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox17.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox17.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 18:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox18.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox18.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox18.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox18.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox18.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox18.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 19:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox19.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox19.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox19.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox19.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox19.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox19.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 20:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox20.Image = global::BioA.UI.Properties.Resources.White;
                                    break;    
                                case 1:       
                                    pictureBox20.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;    
                                case 2:       
                                    pictureBox20.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;    
                                case 3:       
                                    pictureBox20.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;    
                                case 4:       
                                    pictureBox20.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;    
                                case 5:       
                                    pictureBox20.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 21:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox21.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox21.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox21.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox21.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox21.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox21.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 22:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox22.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox22.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox22.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox22.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox22.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox22.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 23:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox23.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox23.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox23.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox23.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox23.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox23.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 24:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox24.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox24.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox24.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox24.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox24.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox24.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 25:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox25.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox25.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox25.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox25.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox25.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox25.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 26:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox26.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox26.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox26.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox26.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox26.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox26.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 27:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox27.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox27.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox27.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox27.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox27.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox27.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 28:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox28.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox28.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox28.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox28.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox28.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox28.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 29:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox29.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox29.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox29.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox29.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox29.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox29.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 30:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox30.Image = global::BioA.UI.Properties.Resources.White;
                                    break;    
                                case 1:       
                                    pictureBox30.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;    
                                case 2:       
                                    pictureBox30.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;    
                                case 3:       
                                    pictureBox30.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;    
                                case 4:       
                                    pictureBox30.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;    
                                case 5:       
                                    pictureBox30.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 31:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox31.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox31.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox31.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox31.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox31.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox31.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 32:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox32.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox32.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;    
                                case 2:       
                                    pictureBox32.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox32.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox32.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox32.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 33:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox33.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox33.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox33.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox33.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox33.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox33.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 34:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox34.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox34.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox34.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox34.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox34.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox34.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 35:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox35.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox35.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox35.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox35.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox35.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox35.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 36:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox36.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox36.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;    
                                case 2:        
                                    pictureBox36.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox36.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox36.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox36.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 37:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox37.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox37.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox37.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox37.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox37.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox37.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 38:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox38.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox38.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox38.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox38.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox38.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox38.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 39:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox39.Image = global::BioA.UI.Properties.Resources.White;
                                    break;     
                                case 1:        
                                    pictureBox39.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;     
                                case 2:        
                                    pictureBox39.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;     
                                case 3:        
                                    pictureBox39.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;     
                                case 4:        
                                    pictureBox39.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;     
                                case 5:        
                                    pictureBox39.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 40:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox40.Image = global::BioA.UI.Properties.Resources.White;
                                    break;    
                                case 1:       
                                    pictureBox40.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;    
                                case 2:       
                                    pictureBox40.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;    
                                case 3:       
                                    pictureBox40.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;    
                                case 4:       
                                    pictureBox40.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;    
                                case 5:       
                                    pictureBox40.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 41:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox41.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox41.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox41.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox41.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox41.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox41.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 42:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox42.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox42.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox42.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox42.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox42.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox42.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 43:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox43.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox43.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox43.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox43.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox43.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox43.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 44:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox44.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox44.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox44.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox44.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox44.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox44.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 45:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox45.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox45.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox45.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox45.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox45.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox45.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 46:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox46.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox46.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox46.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox46.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox46.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox46.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 47:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox47.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox47.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox47.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox47.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox47.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox47.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 48:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox48.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox48.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox48.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox48.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox48.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox48.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 49:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox49.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox49.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox49.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox49.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox49.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox49.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 50:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox50.Image = global::BioA.UI.Properties.Resources.White;
                                    break;    
                                case 1:       
                                    pictureBox50.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;   
                                case 2:      
                                    pictureBox50.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;   
                                case 3:      
                                    pictureBox50.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;    
                                case 4:       
                                    pictureBox50.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;  
                                case 5:     
                                    pictureBox50.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 51:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox51.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox51.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox51.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox51.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox51.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox51.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 52:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox52.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox52.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox52.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox52.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox52.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox52.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 53:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox53.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox53.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox53.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox53.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox53.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox53.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 54:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox54.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox54.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox54.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox54.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox54.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox54.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 55:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox55.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox55.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox55.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox55.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox55.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox55.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 56:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox56.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox56.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox56.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox56.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox56.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox56.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 57:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox57.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox57.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox57.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox57.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox57.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox57.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 58:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox58.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox58.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox58.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox58.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox58.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox58.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 59:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox59.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox59.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox59.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox59.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox59.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox59.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 60:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox60.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox60.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox60.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox60.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox60.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox60.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 61:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox61.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox61.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox61.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox61.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox61.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox61.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 62:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox62.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox62.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox62.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox62.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox62.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox62.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 63:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox63.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox63.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox63.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox63.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox63.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox63.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 64:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox64.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox64.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox64.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox64.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox64.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox64.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 65:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox65.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox65.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox65.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox65.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox65.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox65.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 66:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox66.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox66.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox66.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox66.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox66.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox66.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 67:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox67.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox67.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox67.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox67.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox67.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox67.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 68:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox68.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox68.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox68.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox68.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox68.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox68.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 69:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox69.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox69.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox69.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox69.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox69.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox69.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 70:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox70.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox70.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox70.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox70.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox70.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox70.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 71:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox71.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox71.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox71.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox71.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox71.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox71.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 72:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox72.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox72.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox72.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox72.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox72.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox72.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 73:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox73.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox73.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox73.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox73.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox73.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox73.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 74:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox74.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox74.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox74.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox74.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox74.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox74.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 75:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox75.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox75.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox75.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox75.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox75.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox75.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 76:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox76.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox76.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox76.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox76.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox76.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox76.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 77:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox77.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox77.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox77.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox77.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox77.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox77.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 78:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox78.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox78.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox78.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox78.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox78.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox78.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 79:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox79.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox79.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox79.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox79.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox79.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox79.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 80:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox80.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox80.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox80.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox80.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox80.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox80.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 81:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox81.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox81.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox81.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox81.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox81.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox81.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 82:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox82.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox82.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox82.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox82.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox82.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox82.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 83:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox83.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox83.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox83.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox83.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox83.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox83.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 84:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox84.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox84.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox84.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox84.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox84.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox84.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 85:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox85.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox85.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox85.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox85.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox85.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox85.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 86:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox86.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox86.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox86.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox86.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox86.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox86.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 87:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox87.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox87.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox87.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox87.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox87.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox87.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 88:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox88.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox88.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox88.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox88.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox88.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox88.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 89:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox89.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox89.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox89.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox89.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox89.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox89.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 90:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox90.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox90.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox90.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox90.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox90.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox90.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 91:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox91.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox91.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox91.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox91.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox91.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox91.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 92:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox92.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox92.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox92.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox92.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox92.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox92.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 93:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox93.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox93.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox93.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox93.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox93.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox93.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 94:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox94.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox94.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox94.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox94.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox94.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox94.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 95:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox95.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox95.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox95.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox95.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox95.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox95.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 96:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox96.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox96.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox96.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox96.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox96.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox96.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 97:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox97.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox97.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox97.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox97.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox97.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox97.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 98:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox98.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox98.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox98.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox98.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox98.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox98.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 99:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox99.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox99.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox99.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox99.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox99.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox99.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 100:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox100.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox100.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox100.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox100.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox100.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox100.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 101:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox101.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox101.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox101.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox101.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox101.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox101.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 102:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox102.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox102.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox102.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox102.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox102.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox102.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 103:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox103.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox103.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox103.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox103.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox103.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox103.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 104:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox104.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox104.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox104.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox104.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox104.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox104.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 105:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox105.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox105.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox105.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox105.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox105.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox105.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 106:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox106.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox106.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox106.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox106.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox106.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox106.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 107:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox107.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox107.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox107.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox107.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox107.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox107.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 108:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox108.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox108.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox108.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox108.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox108.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox108.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 109:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox109.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox109.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox109.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox109.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox109.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox109.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 110:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox110.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox110.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox110.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox110.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox110.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox110.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 111:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox111.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox111.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox111.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox111.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox111.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox111.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 112:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox112.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox112.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox112.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox112.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox112.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox112.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 113:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox113.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox113.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox113.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox113.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox113.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox113.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 114:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox114.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox114.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox114.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox114.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox114.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox114.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 115:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox115.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox115.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox115.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox115.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox115.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox115.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 116:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox116.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox116.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox116.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox116.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox116.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox116.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 117:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox117.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox117.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox117.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox117.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox117.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox117.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 118:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox118.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox118.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox118.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox118.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox118.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox118.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 119:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox119.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox119.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox119.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox119.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox119.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox119.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 120:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox120.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox120.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox120.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox120.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox120.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox120.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 121:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox121.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox121.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox121.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox121.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox121.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox121.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 122:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox122.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox122.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox122.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox122.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox122.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox122.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 123:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox123.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox123.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox123.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox123.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox123.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox123.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 124:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox124.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox124.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox124.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox124.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox124.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox124.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 125:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox125.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox125.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox125.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox125.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox125.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox125.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 126:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox126.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox126.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox126.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox126.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox126.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox126.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 127:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox127.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox127.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox127.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox127.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox127.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox127.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 128:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox128.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox128.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox128.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox128.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox128.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox128.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 129:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox129.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox129.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox129.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox129.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox129.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox129.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 130:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox130.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox130.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox130.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox130.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox130.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox130.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 131:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox131.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox131.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox131.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox131.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox131.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox131.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 132:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox132.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox132.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox132.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox132.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox132.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox132.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 133:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox133.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox133.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox133.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox133.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox133.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox133.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 134:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox134.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox134.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox134.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox134.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox134.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox134.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 135:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox135.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox135.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox135.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox135.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox135.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox135.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 136:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox136.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox136.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox136.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox136.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox136.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox136.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 137:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox137.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox137.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox137.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox137.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox137.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox137.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 138:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox138.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox138.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox138.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox138.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox138.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox138.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 139:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox139.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox139.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox139.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox139.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox139.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox139.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 140:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox140.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox140.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox140.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox140.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox140.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox140.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 141:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox141.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox141.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox141.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox141.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox141.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox141.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 142:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox142.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox142.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox142.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox142.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox142.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox142.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 143:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox143.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox143.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox143.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox143.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox143.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox143.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 144:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox144.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox144.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox144.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox144.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox144.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox144.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 145:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox145.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox145.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox145.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox145.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox145.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox145.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 146:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox146.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox146.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox146.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox146.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox146.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox146.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 147:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox147.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox147.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox147.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox147.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox147.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox147.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 148:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox148.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox148.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox148.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox148.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox148.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox148.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 149:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox149.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox149.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox149.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox149.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox149.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox149.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 150:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox150.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox150.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox150.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox150.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox150.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox150.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 151:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox151.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox151.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox151.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox151.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox151.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox151.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 152:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox152.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox152.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox152.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox152.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox152.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox152.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 153:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox153.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox153.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox153.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox153.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox153.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox153.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 154:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox154.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox154.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox154.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox154.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox154.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox154.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 155:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox155.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox155.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox155.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox155.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox155.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox155.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 156:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox156.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox156.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox156.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox156.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox156.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox156.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 157:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox157.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox157.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox157.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox157.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox157.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox157.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 158:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox158.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox158.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox158.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox158.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox158.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox158.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 159:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox159.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox159.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox159.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox159.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox159.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox159.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break;
                        case 160:
                            switch (s.SampleState)
                            {
                                case 0:
                                    pictureBox160.Image = global::BioA.UI.Properties.Resources.White;
                                    break;
                                case 1:
                                    pictureBox160.Image = global::BioA.UI.Properties.Resources.Cyan;
                                    break;
                                case 2:
                                    pictureBox160.Image = global::BioA.UI.Properties.Resources.Green;
                                    break;
                                case 3:
                                    pictureBox160.Image = global::BioA.UI.Properties.Resources.Red;
                                    break;
                                case 4:
                                    pictureBox160.Image = global::BioA.UI.Properties.Resources.Purple;
                                    break;
                                case 5:
                                    pictureBox160.Image = global::BioA.UI.Properties.Resources.Yellow;
                                    break;
                            }
                            break; 
                    }
                }
            }
        }

        void paintState() {
            for (int i = 0; i < 160; i++)
            {
                if (i == 0)
                {
                    this.pictureBox1.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox1.Location = new System.Drawing.Point(1068, 748);
                    this.pictureBox1.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox1);

                }
                else if (i == 1)
                {
                    this.pictureBox2.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox2.Location = new System.Drawing.Point(1018, 780);
                    this.pictureBox2.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBoxView);
                }
                else if (i == 2)
                {

                    this.pictureBox3.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox3.Location = new System.Drawing.Point(964, 804);
                    this.pictureBox3.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox3);
                
                }
                else if (i == 3)
                {
                    this.pictureBox4.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox4.Location = new System.Drawing.Point(907, 817);
                    this.pictureBox4.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox4);
                }
                else if (i == 4)
                {
                    this.pictureBox5.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox5.Location = new System.Drawing.Point(848, 825);
                    this.pictureBox5.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox5);
                }
                else if (i == 5)
                {
                    this.pictureBox6.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox6.Location = new System.Drawing.Point(789, 821);
                    this.pictureBox6.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox6);
                }
                else if (i == 6)
                {
                    this.pictureBox7.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox7.Location = new System.Drawing.Point(731, 809);
                    this.pictureBox7.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox7);
                }
                else if (i == 7)
                {
                    this.pictureBox8.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox8.Location = new System.Drawing.Point(676, 787);
                    this.pictureBox8.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox8);
                }
                else if (i == 8)
                {
                    this.pictureBox9.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox9.Location = new System.Drawing.Point(624,757);
                    this.pictureBox9.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox9);
                }
                else if (i == 9)
                {
                    this.pictureBox10.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox10.Location = new System.Drawing.Point(579, 720);
                    this.pictureBox10.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox10);
                }
                else if (i == 10)
                {
                    this.pictureBox11.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox11.Location = new System.Drawing.Point(539, 675);
                    this.pictureBox11.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox11);
                }
                else if (i == 11)
                {
                    this.pictureBox12.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox12.Location = new System.Drawing.Point(507, 626);
                    this.pictureBox12.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox12);
                }
                else if (i == 12)
                {
                    this.pictureBox13.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox13.Location = new System.Drawing.Point(483, 571);
                    this.pictureBox13.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox13);
                }
                else if (i == 13)
                {
                    this.pictureBox14.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox14.Location = new System.Drawing.Point(468, 514);
                    this.pictureBox14.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox14);
                }
                else if (i == 14)
                {
                    this.pictureBox15.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox15.Location = new System.Drawing.Point(462, 455);
                    this.pictureBox15.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox15);
                }
                else if (i == 15)
                {
                    this.pictureBox16.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox16.Location = new System.Drawing.Point(466, 396);
                    this.pictureBox16.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox16);
                }
                else if (i == 16)
                {
                    this.pictureBox17.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox17.Location = new System.Drawing.Point(478, 338);
                    this.pictureBox17.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox17);
                }
                else if (i == 17)
                {
                    this.pictureBox18.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox18.Location = new System.Drawing.Point(500, 283);
                    this.pictureBox18.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox18);
 
                }
                else if (i == 18)
                {
                    this.pictureBox19.Image = global::BioA.UI.Properties.Resources.Blue;
                    this.pictureBox19.Location = new System.Drawing.Point(530, 231);
                    this.pictureBox19.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox19);

                }
                else if (i == 19)
                {
                    this.pictureBox20.Image = global::BioA.UI.Properties.Resources.Green;
                    this.pictureBox20.Location = new System.Drawing.Point(567, 186);
                    this.pictureBox20.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox20);
                }
                else if (i == 20)
                {
                    this.pictureBox21.Image = global::BioA.UI.Properties.Resources.Green;
                    this.pictureBox21.Location = new System.Drawing.Point(612, 146);
                    this.pictureBox21.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox21);
                }
                else if (i == 21)
                {
                    this.pictureBox22.Image = global::BioA.UI.Properties.Resources.Green;
                    this.pictureBox22.Location = new System.Drawing.Point(661, 114);
                    this.pictureBox22.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox22);
                }
                else if (i == 22)
                {
                    this.pictureBox23.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox23.Location = new System.Drawing.Point(716,90);
                    this.pictureBox23.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox23);
                }
                else if (i == 23)
                {
                    this.pictureBox24.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox24.Location = new System.Drawing.Point(773, 75);
                    this.pictureBox24.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox24);
                }
                else if (i == 24)
                {
                    this.pictureBox25.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox25.Location = new System.Drawing.Point(832, 69);
                    this.pictureBox25.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox25);
                }
                else if (i == 25)
                {
                    this.pictureBox26.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox26.Location = new System.Drawing.Point(891, 73);
                    this.pictureBox26.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox26);
                }
                else if (i == 26)
                {
                    this.pictureBox27.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox27.Location = new System.Drawing.Point(949, 85);
                    this.pictureBox27.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox27);
                }
                else if (i == 27)
                {
                    this.pictureBox28.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox28.Location = new System.Drawing.Point(1004, 107);
                    this.pictureBox28.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox28);
                }
                else if (i == 28)
                {
                    this.pictureBox29.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox29.Location = new System.Drawing.Point(1056, 137);
                    this.pictureBox29.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox29);
                }
                else if (i == 29)
                {
                    this.pictureBox30.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox30.Location = new System.Drawing.Point(1101, 174);
                    this.pictureBox30.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox30);
                }
                else if (i == 30)
                {
                    this.pictureBox31.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox31.Location = new System.Drawing.Point(1141, 219);
                    this.pictureBox31.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox31);
 
                }
                else if (i == 31)
                {
                    this.pictureBox32.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox32.Location = new System.Drawing.Point(1173, 268);
                    this.pictureBox32.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox32);
                
                }
                else if (i == 32)
                {
                    this.pictureBox33.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox33.Location = new System.Drawing.Point(1197, 323);
                    this.pictureBox33.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox33);

                }
                else if (i == 33)
                {
                    this.pictureBox34.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox34.Location = new System.Drawing.Point(1212, 380);
                    this.pictureBox34.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox34);
                }
                else if (i == 34)
                {
                    this.pictureBox35.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox35.Location = new System.Drawing.Point(1218, 439);
                    this.pictureBox35.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox35);
                }
                else if (i == 35)
                {
                    this.pictureBox36.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox36.Location = new System.Drawing.Point(1214, 498);
                    this.pictureBox36.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox36);
                }
                else if (i == 36)
                {
                    this.pictureBox37.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox37.Location = new System.Drawing.Point(1202, 556);
                    this.pictureBox37.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox37);
                }
                else if (i == 37)
                {
                    this.pictureBox38.Image = global::BioA.UI.Properties.Resources.Purple;
                    this.pictureBox38.Location = new System.Drawing.Point(1180, 611);
                    this.pictureBox38.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox38);
                }
                else if (i == 38)
                {
                    this.pictureBox39.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox39.Location = new System.Drawing.Point(1150, 663);
                    this.pictureBox39.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox39);
                }
                else if (i == 39)
                {
                    this.pictureBox40.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox40.Location = new System.Drawing.Point(1113, 708);
                    this.pictureBox40.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox40);
                }
                else if (i == 40)
                {
                    this.pictureBox41.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox41.Location = new System.Drawing.Point(1020, 727);
                    this.pictureBox41.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox41);
 
                }
                else if (i == 41)
                {
                    this.pictureBox42.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox42.Location = new System.Drawing.Point(974, 752);
                    this.pictureBox42.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox42);

                }
                else if (i == 42)
                {
                    this.pictureBox43.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox43.Location = new System.Drawing.Point(925, 769);
                    this.pictureBox43.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox43);

                }
                else if (i == 43)
                {
                    this.pictureBox44.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox44.Location = new System.Drawing.Point(873,779);
                    this.pictureBox44.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox44);

                }
                else if (i == 44)
                {
                    this.pictureBox45.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox45.Location = new System.Drawing.Point(821, 780);
                    this.pictureBox45.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox45);

                }
                else if (i == 45)
                {
                    this.pictureBox46.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox46.Location = new System.Drawing.Point(769, 773);
                    this.pictureBox46.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox46);

                }
                else if (i == 46)
                {
                    this.pictureBox47.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox47.Location = new System.Drawing.Point(719, 758);
                    this.pictureBox47.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox47);

                }
                else if (i == 47)
                {
                    this.pictureBox48.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox48.Location = new System.Drawing.Point(672, 735);
                    this.pictureBox48.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox48);

                }
                else if (i == 48)
                {
                    this.pictureBox49.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox49.Location = new System.Drawing.Point(629, 705);
                    this.pictureBox49.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox49);
                
                
                }
                else if (i == 49)
                {
                    this.pictureBox50.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox50.Location = new System.Drawing.Point(591, 669);
                    this.pictureBox50.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox50);


                }
                else if (i == 50) {

                    this.pictureBox51.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox51.Location = new System.Drawing.Point(560, 627);
                    this.pictureBox51.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox51);
                }
                else if (i == 51)
                {

                    this.pictureBox52.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox52.Location = new System.Drawing.Point(535, 581);
                    this.pictureBox52.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox52);
                }
                else if (i == 52)
                {

                    this.pictureBox53.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox53.Location = new System.Drawing.Point(518, 532);
                    this.pictureBox53.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox53);
                }
                else if (i == 53)
                {
                    this.pictureBox54.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox54.Location = new System.Drawing.Point(508, 480);
                    this.pictureBox54.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox54);
                }
                else if (i == 54)
                {
                    this.pictureBox55.Image = global::BioA.UI.Properties.Resources.Red;
                    this.pictureBox55.Location = new System.Drawing.Point(507,428);
                    this.pictureBox55.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox55);
                
                }
                else if (i == 55)
                {
                    this.pictureBox56.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox56.Location = new System.Drawing.Point(514, 376);
                    this.pictureBox56.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox56);
 
                }
                else if (i == 56)
                {
                    this.pictureBox57.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox57.Location = new System.Drawing.Point(529, 326);
                    this.pictureBox57.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox57);
 
                }
                else if (i == 57)
                {
                    this.pictureBox58.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox58.Location = new System.Drawing.Point(552, 279);
                    this.pictureBox58.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox58);

                }
                else if (i == 58)
                {
                    this.pictureBox59.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox59.Location = new System.Drawing.Point(582, 236);
                    this.pictureBox59.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox59);
                }
                else if (i == 59)
                {
                    this.pictureBox60.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox60.Location = new System.Drawing.Point(618, 198);
                    this.pictureBox60.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox60);
                }
                else if (i == 60)
                {
                    this.pictureBox61.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox61.Location = new System.Drawing.Point(660, 166);
                    this.pictureBox61.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox61);
                }
                else if (i == 61)
                {

                    this.pictureBox62.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox62.Location = new System.Drawing.Point(706, 142);
                    this.pictureBox62.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox62);
 
                }
                else if (i == 62)
                {
                    this.pictureBox63.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox63.Location = new System.Drawing.Point(755,125);
                    this.pictureBox63.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox63);
                }
                else if (i == 63)
                {
                    this.pictureBox64.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox64.Location = new System.Drawing.Point(807, 115);
                    this.pictureBox64.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox64);
 
                }
                else if (i == 64)
                {
                    this.pictureBox65.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox65.Location = new System.Drawing.Point(859, 114);
                    this.pictureBox65.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox65);
 
                }
                else if (i == 65)
                {
                    this.pictureBox66.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox66.Location = new System.Drawing.Point(911, 121);
                    this.pictureBox66.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox66);

                }
                else if (i == 66)
                {
                    this.pictureBox67.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox67.Location = new System.Drawing.Point(961, 136);
                    this.pictureBox67.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox67);
                }
                else if (i == 67)
                {
                    this.pictureBox68.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox68.Location = new System.Drawing.Point(1008, 159);
                    this.pictureBox68.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox68);
                }
                else if (i == 68)
                {
                    this.pictureBox69.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox69.Location = new System.Drawing.Point(1051, 189);
                    this.pictureBox69.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox69);
                }
                else if (i == 69)
                {
                    this.pictureBox70.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox70.Location = new System.Drawing.Point(1089, 225);
                    this.pictureBox70.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox70); 
                }
                else if (i == 70)
                {
                    this.pictureBox71.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox71.Location = new System.Drawing.Point(1120, 267);
                    this.pictureBox71.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox71); 
                
                }
                else if (i == 71)
                {
                    this.pictureBox72.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox72.Location = new System.Drawing.Point(1145, 313);
                    this.pictureBox72.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox72); 
                
                }
                else if (i == 72)
                {
                    this.pictureBox73.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox73.Location = new System.Drawing.Point(1162, 362);
                    this.pictureBox73.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox73);

                }
                else if (i == 73)
                {
                    this.pictureBox74.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox74.Location = new System.Drawing.Point(1172, 414);
                    this.pictureBox74.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox74);

                }
                else if (i == 74)
                {
                    this.pictureBox75.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox75.Location = new System.Drawing.Point(1173, 466);
                    this.pictureBox75.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox75);

                }
                else if (i == 75)
                {
                    this.pictureBox76.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox76.Location = new System.Drawing.Point(1166, 518);
                    this.pictureBox76.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox76);

                }
                else if (i == 76)
                {
                    this.pictureBox77.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox77.Location = new System.Drawing.Point(1151, 568);
                    this.pictureBox77.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox77);

                }
                else if (i == 77)
                {
                    this.pictureBox78.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox78.Location = new System.Drawing.Point(1128, 615);
                    this.pictureBox78.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox78);

                }
                else if (i == 78)
                {
                    this.pictureBox79.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox79.Location = new System.Drawing.Point(1098, 658);
                    this.pictureBox79.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox79);

                }
                else if (i == 79)
                {
                    this.pictureBox80.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox80.Location = new System.Drawing.Point(1062, 696);
                    this.pictureBox80.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox80);

                }
                else if (i == 80)
                {
                    this.pictureBox81.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox81.Location = new System.Drawing.Point(982, 698);
                    this.pictureBox81.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox81);

                }
                else if (i == 81)
                {
                    this.pictureBox82.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox82.Location = new System.Drawing.Point(941, 718);
                    this.pictureBox82.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox82);

                }
                else if (i == 82)
                {
                    this.pictureBox83.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox83.Location = new System.Drawing.Point(898,730);
                    this.pictureBox83.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox83);

                }
                else if (i == 83)
                {
                    this.pictureBox84.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox84.Location = new System.Drawing.Point(853, 736);
                    this.pictureBox84.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox84);

                }
                else if (i == 84)
                {
                    this.pictureBox85.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox85.Location = new System.Drawing.Point(807, 734);
                    this.pictureBox85.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox85);

                }
                else if (i == 85)
                {
                    this.pictureBox86.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox86.Location = new System.Drawing.Point(763, 725);
                    this.pictureBox86.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox86);

                }
                else if (i == 86)
                {
                    this.pictureBox87.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox87.Location = new System.Drawing.Point(720, 710);
                    this.pictureBox87.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox87);

                }
                else if (i == 87)
                {
                    this.pictureBox88.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox88.Location = new System.Drawing.Point(681, 688);
                    this.pictureBox88.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox88);

                }
                else if (i == 88)
                {
                    this.pictureBox89.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox89.Location = new System.Drawing.Point(645,660);
                    this.pictureBox89.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox89);

                }
                else if (i == 89)
                {
                    this.pictureBox90.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox90.Location = new System.Drawing.Point(614, 627);
                    this.pictureBox90.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox90);

                }
                else if (i == 90)
                {
                    this.pictureBox91.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox91.Location = new System.Drawing.Point(589, 589);
                    this.pictureBox91.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox91);

                }
                else if (i == 91)
                {
                    this.pictureBox92.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox92.Location = new System.Drawing.Point(569, 548);
                    this.pictureBox92.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox92);

                }
                else if (i == 92)
                {
                    this.pictureBox93.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox93.Location = new System.Drawing.Point(557, 505);
                    this.pictureBox93.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox93);

                }
                else if (i == 93)
                {
                    this.pictureBox94.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox94.Location = new System.Drawing.Point(551, 460);
                    this.pictureBox94.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox94);

                }
                else if (i == 94)
                {
                    this.pictureBox95.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox95.Location = new System.Drawing.Point(553, 414);
                    this.pictureBox95.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox95);

                }
                else if (i == 95)
                {
                    this.pictureBox96.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox96.Location = new System.Drawing.Point(561, 370);
                    this.pictureBox96.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox96);

                }
                else if (i == 96)
                {
                    this.pictureBox97.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox97.Location = new System.Drawing.Point(577, 327);
                    this.pictureBox97.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox97);

                }
                else if (i == 97)
                {
                    this.pictureBox98.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox98.Location = new System.Drawing.Point(599,288);
                    this.pictureBox98.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox98);

                }
                else if (i == 98)
                {
                    this.pictureBox99.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox99.Location = new System.Drawing.Point(627,252);
                    this.pictureBox99.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox99);

                }
                else if (i == 99)
                {
                    this.pictureBox100.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox100.Location = new System.Drawing.Point(660, 221);
                    this.pictureBox100.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox100);

                }
                else if (i == 100)
                {
                    this.pictureBox101.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox101.Location = new System.Drawing.Point(698, 196);
                    this.pictureBox101.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox101);

                }
                else if (i == 101)
                {
                    this.pictureBox102.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox102.Location = new System.Drawing.Point(739, 176);
                    this.pictureBox102.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox102);

                }
                else if (i == 102)
                {
                    this.pictureBox103.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox103.Location = new System.Drawing.Point(782, 164);
                    this.pictureBox103.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox103);

                }
                else if (i == 103)
                {
                    this.pictureBox104.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox104.Location = new System.Drawing.Point(827, 158);
                    this.pictureBox104.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox104);

                }
                else if (i == 104)
                {
                    this.pictureBox105.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox105.Location = new System.Drawing.Point(872, 160);
                    this.pictureBox105.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox105);

                }
                else if (i == 105)
                {
                    this.pictureBox106.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox106.Location = new System.Drawing.Point(917, 169);
                    this.pictureBox106.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox106);

                }
                else if (i ==106)
                {
                    this.pictureBox107.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox107.Location = new System.Drawing.Point(960, 184);
                    this.pictureBox107.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox107);

                }
                else if (i == 107)
                {
                    this.pictureBox108.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox108.Location = new System.Drawing.Point(999, 206);
                    this.pictureBox108.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox108);

                }
                else if (i == 108)
                {
                    this.pictureBox109.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox109.Location = new System.Drawing.Point(1035, 234);
                    this.pictureBox109.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox109);

                }
                else if (i == 109)
                {
                    this.pictureBox110.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox110.Location = new System.Drawing.Point(1066, 267);
                    this.pictureBox110.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox110);

                }
                else if (i == 110)
                {
                    this.pictureBox111.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox111.Location = new System.Drawing.Point(1091, 305);
                    this.pictureBox111.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox111);

                }
                else if (i == 111)
                {
                    this.pictureBox112.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox112.Location = new System.Drawing.Point(1111, 346);
                    this.pictureBox112.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox112);

                }
                else if (i == 112)
                {
                    this.pictureBox113.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox113.Location = new System.Drawing.Point(1122, 389);
                    this.pictureBox113.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox113);

                }
                else if (i == 113)
                {
                    this.pictureBox114.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox114.Location = new System.Drawing.Point(1129, 434);
                    this.pictureBox114.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox114);

                }
                else if (i == 114)
                {
                    this.pictureBox115.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox115.Location = new System.Drawing.Point(1127, 480);
                    this.pictureBox115.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox115);

                }
                else if (i == 115)
                {
                    this.pictureBox116.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox116.Location = new System.Drawing.Point(1118, 524);
                    this.pictureBox116.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox116);

                }
                else if (i == 116)
                {
                    this.pictureBox117.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox117.Location = new System.Drawing.Point(1103, 567);
                    this.pictureBox117.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox117);

                }
                else if (i == 117)
                {
                    this.pictureBox118.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox118.Location = new System.Drawing.Point(1081, 606);
                    this.pictureBox118.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox118);

                }
                else if (i == 118)
                {
                    this.pictureBox119.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox119.Location = new System.Drawing.Point(1053, 642);
                    this.pictureBox119.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox119);

                }
                else if (i == 119)
                {
                    this.pictureBox120.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox120.Location = new System.Drawing.Point(1020, 673);
                    this.pictureBox120.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox120);

                }
                else if (i == 120)
                {
                    this.pictureBox121.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox121.Location = new System.Drawing.Point(912, 681);
                    this.pictureBox121.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox121);

                }
                else if (i == 121)
                {
                    this.pictureBox122.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox122.Location = new System.Drawing.Point(874, 689);
                    this.pictureBox122.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox122);

                }
                else if (i == 122)
                {
                    this.pictureBox123.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox123.Location = new System.Drawing.Point(836, 691);
                    this.pictureBox123.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox123);

                }
                else if (i == 123)
                {
                    this.pictureBox124.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox124.Location = new System.Drawing.Point(798, 688);
                    this.pictureBox124.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox124);

                }
                else if (i == 124)
                {
                    this.pictureBox125.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox125.Location = new System.Drawing.Point(761, 678);
                    this.pictureBox125.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox125);

                }
                else if (i == 125)
                {
                    this.pictureBox126.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox126.Location = new System.Drawing.Point(725, 663);
                    this.pictureBox126.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox126);

                }
                else if (i == 126)
                {
                    this.pictureBox127.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox127.Location = new System.Drawing.Point(693, 642);
                    this.pictureBox127.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox127);

                }
                else if (i == 127)
                {
                    this.pictureBox128.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox128.Location = new System.Drawing.Point(664, 617);
                    this.pictureBox128.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox128);

                }
                else if (i == 128)
                {
                    this.pictureBox129.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox129.Location = new System.Drawing.Point(640, 587);
                    this.pictureBox129.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox129);

                }
                else if (i == 129)
                {
                    this.pictureBox130.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox130.Location = new System.Drawing.Point(620, 554);
                    this.pictureBox130.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox130);

                }
                else if (i == 130)
                {
                    this.pictureBox131.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox131.Location = new System.Drawing.Point(606, 519);
                    this.pictureBox131.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox131);

                }
                else if (i == 131)
                {
                    this.pictureBox132.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox132.Location = new System.Drawing.Point(598, 481);
                    this.pictureBox132.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox132);

                }
                else if (i == 132)
                {
                    this.pictureBox133.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox133.Location = new System.Drawing.Point(596, 443);
                    this.pictureBox133.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox133);

                }
                else if (i == 133)
                {
                    this.pictureBox134.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox134.Location = new System.Drawing.Point(600, 405);
                    this.pictureBox134.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox134);

                }
                else if (i == 134)
                {
                    this.pictureBox135.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox135.Location = new System.Drawing.Point(609, 368);
                    this.pictureBox135.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox135);

                }
                else if (i == 135)
                {
                    this.pictureBox136.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox136.Location = new System.Drawing.Point(624, 332);
                    this.pictureBox136.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox136);

                }
                else if (i == 136)
                {
                    this.pictureBox137.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox137.Location = new System.Drawing.Point(645, 300);
                    this.pictureBox137.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox137);

                }
                else if (i == 137)
                {
                    this.pictureBox138.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox138.Location = new System.Drawing.Point(670, 271);
                    this.pictureBox138.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox138);

                }
                else if (i == 138)
                {
                    this.pictureBox139.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox139.Location = new System.Drawing.Point(700, 247);
                    this.pictureBox139.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox139);

                }
                else if (i == 139)
                {
                    this.pictureBox140.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox140.Location = new System.Drawing.Point(733, 227);
                    this.pictureBox140.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox140);

                }
                else if (i == 140)
                {
                    this.pictureBox141.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox141.Location = new System.Drawing.Point(768, 213);
                    this.pictureBox141.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox141);

                }
                else if (i == 141)
                {
                    this.pictureBox142.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox142.Location = new System.Drawing.Point(806, 205);
                    this.pictureBox142.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox142);

                }
                else if (i == 142)
                {
                    this.pictureBox143.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox143.Location = new System.Drawing.Point(844, 203);
                    this.pictureBox143.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox143);

                }
                else if (i == 143)
                {
                    this.pictureBox144.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox144.Location = new System.Drawing.Point(882, 206);
                    this.pictureBox144.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox144);

                }
                else if (i == 144)
                {
                    this.pictureBox145.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox145.Location = new System.Drawing.Point(918, 216);
                    this.pictureBox145.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox145);

                }
                else if (i == 145)
                {
                    this.pictureBox146.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox146.Location = new System.Drawing.Point(955, 231);
                    this.pictureBox146.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox146);

                }
                else if (i == 146)
                {
                    this.pictureBox147.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox147.Location = new System.Drawing.Point(987, 251);
                    this.pictureBox147.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox147);

                }
                else if (i == 147)
                {
                    this.pictureBox148.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox148.Location = new System.Drawing.Point(1016, 277);
                    this.pictureBox148.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox148);

                }
                else if (i == 148)
                {
                    this.pictureBox149.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox149.Location = new System.Drawing.Point(1040, 307);
                    this.pictureBox149.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox149);

                }
                else if (i == 149)
                {
                    this.pictureBox150.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox150.Location = new System.Drawing.Point(1060, 340);
                    this.pictureBox150.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox150);

                }
                else if (i == 150)
                {
                    this.pictureBox151.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox151.Location = new System.Drawing.Point(1074, 375);
                    this.pictureBox151.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox151);

                }
                else if (i == 151)
                {
                    this.pictureBox152.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox152.Location = new System.Drawing.Point(1082, 413);
                    this.pictureBox152.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox152);

                }
                else if (i == 152)
                {
                    this.pictureBox153.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox153.Location = new System.Drawing.Point(1084, 451);
                    this.pictureBox153.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox153);

                }
                else if (i == 153)
                {
                    this.pictureBox154.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox154.Location = new System.Drawing.Point(1081, 489);
                    this.pictureBox154.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox154);

                }
                else if (i == 154)
                {
                    this.pictureBox155.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox155.Location = new System.Drawing.Point(1071, 526);
                    this.pictureBox155.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox155);

                }
                else if (i == 155)
                {
                    this.pictureBox156.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox156.Location = new System.Drawing.Point(1056, 561);
                    this.pictureBox156.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox156);

                }
                else if (i == 156)
                {
                    this.pictureBox157.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox157.Location = new System.Drawing.Point(1035, 594);
                    this.pictureBox157.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox157);

                }
                else if (i == 157)
                {
                    this.pictureBox158.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox158.Location = new System.Drawing.Point(1010, 623);
                    this.pictureBox158.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox158);

                }
                else if (i == 158)
                {
                    this.pictureBox159.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox159.Location = new System.Drawing.Point(980, 647);
                    this.pictureBox159.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox159);

                }
                else if (i == 159)
                {

                    this.pictureBox160.Image = global::BioA.UI.Properties.Resources.White;
                    this.pictureBox160.Location = new System.Drawing.Point(947, 666);
                    this.pictureBox160.Size = new System.Drawing.Size(20, 20);
                    this.Controls.Add(pictureBox160);
                }







            }
            this.pictureBoxSamplePanel.Image = global::BioA.UI.Properties.Resources.SamplePanel;
            this.pictureBoxSamplePanel.Location = new System.Drawing.Point(450, 57);
            this.pictureBoxSamplePanel.Name = "pictureBox1";
            this.pictureBoxSamplePanel.Size = new System.Drawing.Size(800, 800);
            this.pictureBoxSamplePanel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxSamplePanel.TabIndex = 0;
            this.pictureBoxSamplePanel.TabStop = false;
            this.Controls.Add(pictureBoxSamplePanel);
 
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            switch ((sender as PictureBox).Name)
            {
                case "pictureBox1":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask, 
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[]{ cboPanelNum.Text,  "1" }))));
                    break;
                case "pictureBox2":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "2" }))));
                    break;
                case "pictureBox3":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "3" }))));
                    break;
                case "pictureBox4":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "4" }))));
                    break;
                case "pictureBox5":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "5" }))));
                    break;
                case "pictureBox6":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "6" }))));
                    break;
                case "pictureBox7":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "7" }))));
                    break;
                case "pictureBox8":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "8" }))));
                    break;
                case "pictureBox9":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "9" }))));
                    break;
                case "pictureBox10":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "10" }))));
                    break;
                case "pictureBox11":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "11" }))));
                    break;
                case "pictureBox12":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "12" }))));
                    break;
                case "pictureBox13":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "13" }))));
                    break;
                case "pictureBox14":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "14" }))));
                    break;
                case "pictureBox15":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "15" }))));
                    break;
                case "pictureBox16":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "16" }))));
                    break;
                case "pictureBox17":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "17" }))));
                    break;
                case "pictureBox18":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "18" }))));
                    break;
                case "pictureBox19":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "19" }))));
                    break;
                case "pictureBox20":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "20" }))));
                    break;
                case "pictureBox21":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "21" }))));
                    break;
                case "pictureBox22":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "22" }))));
                    break;
                case "pictureBox23":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "23" }))));
                    break;
                case "pictureBox24":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "24" }))));
                    break;
                case "pictureBox25":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "25" }))));
                    break;
                case "pictureBox26":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "26" }))));
                    break;
                case "pictureBox27":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "27" }))));
                    break;
                case "pictureBox28":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "28" }))));
                    break;
                case "pictureBox29":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "29" }))));
                    break;
                case "pictureBox30":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "30" }))));
                    break;
                case "pictureBox31":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "31" }))));
                    break;
                case "pictureBox32":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "32" }))));
                    break;
                case "pictureBox33":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "33" }))));
                    break;
                case "pictureBox34":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "34" }))));
                    break;
                case "pictureBox35":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "35" }))));
                    break;
                case "pictureBox36":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "36" }))));
                    break;
                case "pictureBox37":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "37" }))));
                    break;
                case "pictureBox38":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "38" }))));
                    break;
                case "pictureBox39":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "39" }))));
                    break;
                case "pictureBox40":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "40" }))));
                    break;
                case "pictureBox41":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "41" }))));
                    break;
                case "pictureBox42":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "42" }))));
                    break;
                case "pictureBox43":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "43" }))));
                    break;
                case "pictureBox44":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "44" }))));
                    break;
                case "pictureBox45":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "45" }))));
                    break;
                case "pictureBox46":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "46" }))));
                    break;
                case "pictureBox47":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "47" }))));
                    break;
                case "pictureBox48":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "48" }))));
                    break;
                case "pictureBox49":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "49" }))));
                    break;
                case "pictureBox50":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "50" }))));
                    break;
                case "pictureBox51":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "51" }))));
                    break;
                case "pictureBox52":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "52" }))));
                    break;
                case "pictureBox53":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "53" }))));
                    break;
                case "pictureBox54":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "54" }))));
                    break;
                case "pictureBox55":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "55" }))));
                    break;
                case "pictureBox56":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "56" }))));
                    break;
                case "pictureBox57":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "57" }))));
                    break;
                case "pictureBox58":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "58" }))));
                    break;
                case "pictureBox59":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "59" }))));
                    break;
                case "pictureBox60":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "60" }))));
                    break;
                case "pictureBox61":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "61" }))));
                    break;
                case "pictureBox62":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "62" }))));
                    break;
                case "pictureBox63":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "63" }))));
                    break;
                case "pictureBox64":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "64" }))));
                    break;
                case "pictureBox65":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "65" }))));
                    break;
                case "pictureBox66":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "66" }))));
                    break;
                case "pictureBox67":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "67" }))));
                    break;
                case "pictureBox68":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "68" }))));
                    break;
                case "pictureBox69":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "69" }))));
                    break;
                case "pictureBox70":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "70" }))));
                    break;
                case "pictureBox71":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "71" }))));
                    break;
                case "pictureBox72":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "72" }))));
                    break;
                case "pictureBox73":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "73" }))));
                    break;
                case "pictureBox74":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "74" }))));
                    break;
                case "pictureBox75":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "75" }))));
                    break;
                case "pictureBox76":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "76" }))));
                    break;
                case "pictureBox77":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "77" }))));
                    break;
                case "pictureBox78":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "78" }))));
                    break;
                case "pictureBox79":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "79" }))));
                    break;
                case "pictureBox80":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "80" }))));
                    break;
                case "pictureBox81":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "81" }))));
                    break;
                case "pictureBox82":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "82" }))));
                    break;
                case "pictureBox83":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "83" }))));
                    break;
                case "pictureBox84":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "84" }))));
                    break;
                case "pictureBox85":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "85" }))));
                    break;
                case "pictureBox86":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "86" }))));
                    break;
                case "pictureBox87":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "87" }))));
                    break;
                case "pictureBox88":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "88" }))));
                    break;
                case "pictureBox89":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "89" }))));
                    break;
                case "pictureBox90":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "90" }))));
                    break;
                case "pictureBox91":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "91" }))));
                    break;
                case "pictureBox92":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "92" }))));
                    break;
                case "pictureBox93":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "93" }))));
                    break;
                case "pictureBox94":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "94" }))));
                    break;
                case "pictureBox95":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "95" }))));
                    break;
                case "pictureBox96":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "96" }))));
                    break;
                case "pictureBox97":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "97" }))));
                    break;
                case "pictureBox98":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "98" }))));
                    break;
                case "pictureBox99":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "99" }))));
                    break;
                case "pictureBox100":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "100" }))));
                    break;
                case "pictureBox101":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "101" }))));
                    break;
                case "pictureBox102":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "102" }))));
                    break;
                case "pictureBox103":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "103" }))));
                    break;
                case "pictureBox104":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "104" }))));
                    break;
                case "pictureBox105":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "105" }))));
                    break;
                case "pictureBox106":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "106" }))));
                    break;
                case "pictureBox107":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "107" }))));
                    break;
                case "pictureBox108":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "108" }))));
                    break;
                case "pictureBox109":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "109" }))));
                    break;
                case "pictureBox110":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "110" }))));
                    break;
                case "pictureBox111":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "111" }))));
                    break;
                case "pictureBox112":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "112" }))));
                    break;
                case "pictureBox113":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "113" }))));
                    break;
                case "pictureBox114":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "114" }))));
                    break;
                case "pictureBox115":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "115" }))));
                    break;
                case "pictureBox116":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "116" }))));
                    break;
                case "pictureBox117":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "117" }))));
                    break;
                case "pictureBox118":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "118" }))));
                    break;
                case "pictureBox119":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "119" }))));
                    break;
                case "pictureBox120":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "120" }))));
                    break;
                case "pictureBox121":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "121" }))));
                    break;
                case "pictureBox122":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "122" }))));
                    break;
                case "pictureBox123":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "123" }))));
                    break;
                case "pictureBox124":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "124" }))));
                    break;
                case "pictureBox125":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "125" }))));
                    break;
                case "pictureBox126":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "126" }))));
                    break;
                case "pictureBox127":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "127" }))));
                    break;
                case "pictureBox128":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "128" }))));
                    break;
                case "pictureBox129":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "129" }))));
                    break;
                case "pictureBox130":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "130" }))));
                    break;
                case "pictureBox131":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "131" }))));
                    break;
                case "pictureBox132":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "132" }))));
                    break;
                case "pictureBox133":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "133" }))));
                    break;
                case "pictureBox134":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "134" }))));
                    break;
                case "pictureBox135":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "135" }))));
                    break;
                case "pictureBox136":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "136" }))));
                    break;
                case "pictureBox137":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "137" }))));
                    break;
                case "pictureBox138":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "138" }))));
                    break;
                case "pictureBox139":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "139" }))));
                    break;
                case "pictureBox140":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "140" }))));
                    break;
                case "pictureBox141":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "141" }))));
                    break;
                case "pictureBox142":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "142" }))));
                    break;
                case "pictureBox143":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "143" }))));
                    break;
                case "pictureBox144":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "144" }))));
                    break;
                case "pictureBox145":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "145" }))));
                    break;
                case "pictureBox146":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "146" }))));
                    break;
                case "pictureBox147":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "147" }))));
                    break;
                case "pictureBox148":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "148" }))));
                    break;
                case "pictureBox149":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "149" }))));
                    break;
                case "pictureBox150":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "150" }))));
                    break;
                case "pictureBox151":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "151" }))));
                    break;
                case "pictureBox152":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "152" }))));
                    break;
                case "pictureBox153":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "153" }))));
                    break;
                case "pictureBox154":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "154" }))));
                    break;
                case "pictureBox155":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "155" }))));
                    break;
                case "pictureBox156":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "156" }))));
                    break;
                case "pictureBox157":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "157" }))));
                    break;
                case "pictureBox158":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "158" }))));
                    break;
                case "pictureBox159":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "159" }))));
                    break;
                case "pictureBox160":
                    CommunicationUI.ServiceClient.ClientSendMsgToService(ModuleInfo.WorkingAreaApplyTask,
                        XmlUtility.Serializer(typeof(CommunicationEntity), new CommunicationEntity("QueryTaskInfoForSamplePanel", XmlUtility.Serializer(typeof(string[]), new string[] { cboPanelNum.Text, "160" }))));
                    break;
            }
        }

        private void AnologSamplePanel_Load(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                anologSampDic.Add("QuerySamplePanelState", new object[] { "1" });
                SendToService(anologSampDic);
            }));
        }
        /// <summary>
        /// 发送信息给服务器
        /// </summary>
        /// <param name="sender"></param>
        private void SendToService(Dictionary<string, object[]> sender)
        {
            var anologSampThread = new Thread(() =>
            {
                CommunicationUI.ServiceClient.ClientSendMsgToServiceMethod(ModuleInfo.WorkingAreaApplyTask, anologSampDic);
            });
            anologSampThread.IsBackground = true;
            anologSampThread.Start();
        }

        private void btnChangePanel_Click(object sender, EventArgs e)
        {
            string panelNum = this.cboPanelNum.Text;
            if (panelNum != "请选择"  )
            {
                if (MessageBoxDraw.ShowMsg(string.Format("是否切换成逻辑盘{0}", panelNum), MsgType.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    anologSampDic.Clear();
                    anologSampDic.Add("UpdateRunningTaskWorDisk", new object[] { panelNum });
                    SendToService(anologSampDic);
                    this.WorkDiskNum.Text = "当前工作盘号：" + panelNum;
                }
            }
            else
            {
                MessageBox.Show("请选择您要修改的逻辑盘号！");
                return;
            }
        }
    }
}
