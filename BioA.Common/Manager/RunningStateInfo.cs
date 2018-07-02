using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class RunningStateInfo
    {
        public RunningStateInfo()
        {
            workDisk = 0;
            lastestSMPNO = 0;
            machineStyle = string.Empty;
            lastestTC = 0;
            r1PanelTemp = 0;
            r2PanelTemp = 0;
            cUVPanelTemp = 0;
            cUVBlkMin = 0;
            cUVBlkMax = 0;
            tempOffset = 0;
            rgtWarnCount = 0;
            rgtLeastCount = 0;
            qCSMPContainerType = string.Empty;
            sDTSMPContainerType = string.Empty;
            drawDate = DateTime.MinValue;
            isMutiRgtEnable = false;
            isLockRgtEnable = false;
            isAutoExchange = false;
            state1 = 0;
            state2 = 0;
        }

        private int      workDisk;
        private int      lastestSMPNO;
        private string   machineStyle;
        private int      lastestTC;
        private float    r1PanelTemp;
        private float    r2PanelTemp;
        private float    cUVPanelTemp;
        private float    cUVBlkMin;
        private float    cUVBlkMax;
        private float    tempOffset;
        private int      rgtWarnCount;
        private int      rgtLeastCount;
        private string     qCSMPContainerType;
        private string     sDTSMPContainerType;
        private DateTime drawDate;
        private bool     isMutiRgtEnable;
        private bool     isLockRgtEnable;
        private bool     isAutoExchange;
        private int      state1;
        private int      state2;

        public int WorkDisk
        {
            get { return workDisk; }
            set { workDisk = value; }
        }
        public int LastestSMPNO
        {
            get { return lastestSMPNO; }
            set { lastestSMPNO = value; }
        }
        public string MachineStyle
        {
            get { return machineStyle; }
            set { machineStyle = value; }
        }
        public int LastestTC
        {
            get { return lastestTC; }
            set { lastestTC = value; }
        }
        public float R1PanelTemp
        {
            get { return r1PanelTemp; }
            set { r1PanelTemp = value; }
        }
        public float R2PanelTemp
        {
            get { return r2PanelTemp; }
            set { r2PanelTemp = value; }
        }
        public float CUVPanelTemp
        {
            get { return cUVPanelTemp; }
            set { cUVPanelTemp = value; }
        }
        public float CUVBlkMin
        {
            get { return cUVBlkMin; }
            set { cUVBlkMin = value; }
        }
        public float CUVBlkMax
        {
            get { return cUVBlkMax; }
            set { cUVBlkMax = value; }
        }
        public float TempOffset
        {
            get { return tempOffset; }
            set { tempOffset = value; }
        }
        public int RgtWarnCount
        {
            get { return rgtWarnCount; }
            set { rgtWarnCount = value; }
        }
        public int RgtLeastCount
        {
            get { return rgtLeastCount; }
            set { rgtLeastCount = value; }
        }
        public string QCSMPContainerType
        {
            get { return qCSMPContainerType; }
            set { qCSMPContainerType = value; }
        }
        public string SDTSMPContainerType
        {
            get { return sDTSMPContainerType; }
            set { sDTSMPContainerType = value; }
        }
        public DateTime DrawDate
        {
            get { return drawDate; }
            set { drawDate = value; }
        }
        public bool IsMutiRgtEnable
        {
            get { return isMutiRgtEnable; }
            set { isMutiRgtEnable = value; }
        }
        public bool IsLockRgtEnable
        {
            get { return isLockRgtEnable; }
            set { isLockRgtEnable = value; }
        }
        public bool IsAutoExchange
        {
            get { return isAutoExchange; }
            set { isAutoExchange = value; }
        }
        public int State1
        {
            get { return state1; }
            set { state1 = value; }
        }
        public int State2
        {
            get { return state2; }
            set { state2 = value; }
        }
    }
}
