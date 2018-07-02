using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common
{
    public class TimeCourseInfo
    {
        public TimeCourseInfo()
        {
            timeCourseNo = 0;
            cUVNO = 0;
            drawDate = DateTime.Now;
            cuvBlkWm = 0;
            cuvBlkWs = 0;
            cuv1Wm = 0;
            cuv1Ws = 0;
            cuv2Wm = 0;
            cuv2Ws = 0;
            cuv3Wm = 0;
            cuv3Ws = 0;
            cuv4Wm = 0;
            cuv4Ws = 0;
            cuv5Wm = 0;
            cuv5Ws = 0;
            cuv6Wm = 0;
            cuv6Ws = 0;
            cuv7Wm = 0;
            cuv7Ws = 0;
            cuv8Wm = 0;
            cuv8Ws = 0;
            cuv9Wm = 0;
            cuv9Ws = 0;
            cuv10Wm = 0;
            cuv10Ws = 0;
            cuv11Wm = 0;
            cuv11Ws = 0;
            cuv12Wm = 0;
            cuv12Ws = 0;
            cuv13Wm = 0;
            cuv13Ws = 0;
            cuv14Wm = 0;
            cuv14Ws = 0;
            cuv15Wm = 0;
            cuv15Ws = 0;
            cuv16Wm = 0;
            cuv16Ws = 0;
            cuv17Wm = 0;
            cuv17Ws = 0;
            cuv18Wm = 0;
            cuv18Ws = 0;
            cuv19Wm = 0;
            cuv19Ws = 0;
            cuv20Wm = 0;
            cuv20Ws = 0;
            cuv21Wm = 0;
            cuv21Ws = 0;
            cuv22Wm = 0;
            cuv22Ws = 0;
            cuv23Wm = 0;
            cuv23Ws = 0;
            cuv24Wm = 0;
            cuv24Ws = 0;
            cuv25Wm = 0;
            cuv25Ws = 0;
            cuv26Wm = 0;
            cuv26Ws = 0;
            cuv27Wm = 0;
            cuv27Ws = 0;
            cuv28Wm = 0;
            cuv28Ws = 0;
            cuv29Wm = 0;
            cuv29Ws = 0;
            cuv30Wm = 0;
            cuv30Ws = 0;
            cuv31Wm = 0;
            cuv31Ws = 0;
            cuv32Wm = 0;
            cuv32Ws = 0;
            cuv33Wm = 0;
            cuv33Ws = 0;
            cuv34Wm = 0;
            cuv34Ws = 0;
            cuv35Wm = 0;
            cuv35Ws = 0;
            cuv36Wm = 0;
            cuv36Ws = 0;
            cuv37Wm = 0;
            cuv37Ws = 0;
            cuv38Wm = 0;
            cuv38Ws = 0;
            cuv39Wm = 0;
            cuv39Ws = 0;
            cuv40Wm = 0;
            cuv40Ws = 0;
            cuv41Wm = 0;
            cuv41Ws = 0;
            cuv42Wm = 0;
            cuv42Ws = 0;
            cuv43Wm = 0;
            cuv43Ws = 0;
            cuv44Wm = 0;
            cuv44Ws = 0;
            cuv45Wm = 0;
            cuv45Ws = 0;
            cuv46Wm = 0;
            cuv46Ws = 0;
            cuv47Wm = 0;
            cuv47Ws = 0;
            cuv48Wm = 0;
            cuv48Ws = 0;
            cuv49Wm = 0;
            cuv49Ws = 0;
            cuv50Wm = 0;
            cuv50Ws = 0;
        }

        private int timeCourseNo;
        /// <summary>
        /// 反应进程
        /// </summary>
        public int TimeCourseNo
        {
            get { return timeCourseNo; }
            set { timeCourseNo = value; }
        }
        private int cUVNO;
        /// <summary>
        /// 比色杯编号
        /// </summary>
        public int CUVNO
        {
            get { return cUVNO; }
            set { cUVNO = value; }
        }
        private DateTime drawDate;
        /// <summary>
        /// 产生时间
        /// </summary>
        public DateTime DrawDate
        {
            get { return drawDate; }
            set { drawDate = value; }
        }
        private float cuvBlkWm;

        public float CuvBlkWm
        {
            get { return cuvBlkWm; }
            set { cuvBlkWm = value; }
        }
        private float cuvBlkWs;

        public float CuvBlkWs
        {
            get { return cuvBlkWs; }
            set { cuvBlkWs = value; }
        }
        private float cuv1Wm;

        public float Cuv1Wm
        {
            get { return cuv1Wm; }
            set { cuv1Wm = value; }
        }
        private float cuv1Ws;

        public float Cuv1Ws
        {
            get { return cuv1Ws; }
            set { cuv1Ws = value; }
        }
        private float cuv2Wm;

        public float Cuv2Wm
        {
            get { return cuv2Wm; }
            set { cuv2Wm = value; }
        }
        private float cuv2Ws;

        public float Cuv2Ws
        {
            get { return cuv2Ws; }
            set { cuv2Ws = value; }
        }
        private float cuv3Wm;

        public float Cuv3Wm
        {
            get { return cuv3Wm; }
            set { cuv3Wm = value; }
        }
        private float cuv3Ws;

        public float Cuv3Ws
        {
            get { return cuv3Ws; }
            set { cuv3Ws = value; }
        }
        private float cuv4Wm;

        public float Cuv4Wm
        {
            get { return cuv4Wm; }
            set { cuv4Wm = value; }
        }
        private float cuv4Ws;

        public float Cuv4Ws
        {
            get { return cuv4Ws; }
            set { cuv4Ws = value; }
        }
        private float cuv5Wm;

        public float Cuv5Wm
        {
            get { return cuv5Wm; }
            set { cuv5Wm = value; }
        }
        private float cuv5Ws;

        public float Cuv5Ws
        {
            get { return cuv5Ws; }
            set { cuv5Ws = value; }
        }
        private float cuv6Wm;

        public float Cuv6Wm
        {
            get { return cuv6Wm; }
            set { cuv6Wm = value; }
        }
        private float cuv6Ws;

        public float Cuv6Ws
        {
            get { return cuv6Ws; }
            set { cuv6Ws = value; }
        }
        private float cuv7Wm;

        public float Cuv7Wm
        {
            get { return cuv7Wm; }
            set { cuv7Wm = value; }
        }
        private float cuv7Ws;

        public float Cuv7Ws
        {
            get { return cuv7Ws; }
            set { cuv7Ws = value; }
        }
        private float cuv8Wm;

        public float Cuv8Wm
        {
            get { return cuv8Wm; }
            set { cuv8Wm = value; }
        }
        private float cuv8Ws;

        public float Cuv8Ws
        {
            get { return cuv8Ws; }
            set { cuv8Ws = value; }
        }
        private float cuv9Wm;

        public float Cuv9Wm
        {
            get { return cuv9Wm; }
            set { cuv9Wm = value; }
        }
        private float cuv9Ws;

        public float Cuv9Ws
        {
            get { return cuv9Ws; }
            set { cuv9Ws = value; }
        }
        private float cuv10Wm;

        public float Cuv10Wm
        {
            get { return cuv10Wm; }
            set { cuv10Wm = value; }
        }
        private float cuv10Ws;

        public float Cuv10Ws
        {
            get { return cuv10Ws; }
            set { cuv10Ws = value; }
        }
        private float cuv11Wm;

        public float Cuv11Wm
        {
            get { return cuv11Wm; }
            set { cuv11Wm = value; }
        }
        private float cuv11Ws;

        public float Cuv11Ws
        {
            get { return cuv11Ws; }
            set { cuv11Ws = value; }
        }
        private float cuv12Wm;

        public float Cuv12Wm
        {
            get { return cuv12Wm; }
            set { cuv12Wm = value; }
        }
        private float cuv12Ws;

        public float Cuv12Ws
        {
            get { return cuv12Ws; }
            set { cuv12Ws = value; }
        }
        private float cuv13Wm;

        public float Cuv13Wm
        {
            get { return cuv13Wm; }
            set { cuv13Wm = value; }
        }
        private float cuv13Ws;

        public float Cuv13Ws
        {
            get { return cuv13Ws; }
            set { cuv13Ws = value; }
        }
        private float cuv14Wm;

        public float Cuv14Wm
        {
            get { return cuv14Wm; }
            set { cuv14Wm = value; }
        }
        private float cuv14Ws;

        public float Cuv14Ws
        {
            get { return cuv14Ws; }
            set { cuv14Ws = value; }
        }
        private float cuv15Wm;

        public float Cuv15Wm
        {
            get { return cuv15Wm; }
            set { cuv15Wm = value; }
        }
        private float cuv15Ws;

        public float Cuv15Ws
        {
            get { return cuv15Ws; }
            set { cuv15Ws = value; }
        }
        private float cuv16Wm;

        public float Cuv16Wm
        {
            get { return cuv16Wm; }
            set { cuv16Wm = value; }
        }
        private float cuv16Ws;

        public float Cuv16Ws
        {
            get { return cuv16Ws; }
            set { cuv16Ws = value; }
        }
        private float cuv17Wm;

        public float Cuv17Wm
        {
            get { return cuv17Wm; }
            set { cuv17Wm = value; }
        }
        private float cuv17Ws;

        public float Cuv17Ws
        {
            get { return cuv17Ws; }
            set { cuv17Ws = value; }
        }
        private float cuv18Wm;

        public float Cuv18Wm
        {
            get { return cuv18Wm; }
            set { cuv18Wm = value; }
        }
        private float cuv18Ws;

        public float Cuv18Ws
        {
            get { return cuv18Ws; }
            set { cuv18Ws = value; }
        }
        private float cuv19Wm;

        public float Cuv19Wm
        {
            get { return cuv19Wm; }
            set { cuv19Wm = value; }
        }
        private float cuv19Ws;

        public float Cuv19Ws
        {
            get { return cuv19Ws; }
            set { cuv19Ws = value; }
        }
        private float cuv20Wm;

        public float Cuv20Wm
        {
            get { return cuv20Wm; }
            set { cuv20Wm = value; }
        }
        private float cuv20Ws;

        public float Cuv20Ws
        {
            get { return cuv20Ws; }
            set { cuv20Ws = value; }
        }
        private float cuv21Wm;

        public float Cuv21Wm
        {
            get { return cuv21Wm; }
            set { cuv21Wm = value; }
        }
        private float cuv21Ws;

        public float Cuv21Ws
        {
            get { return cuv21Ws; }
            set { cuv21Ws = value; }
        }
        private float cuv22Wm;

        public float Cuv22Wm
        {
            get { return cuv22Wm; }
            set { cuv22Wm = value; }
        }
        private float cuv22Ws;

        public float Cuv22Ws
        {
            get { return cuv22Ws; }
            set { cuv22Ws = value; }
        }
        private float cuv23Wm;

        public float Cuv23Wm
        {
            get { return cuv23Wm; }
            set { cuv23Wm = value; }
        }
        private float cuv23Ws;

        public float Cuv23Ws
        {
            get { return cuv23Ws; }
            set { cuv23Ws = value; }
        }
        private float cuv24Wm;

        public float Cuv24Wm
        {
            get { return cuv24Wm; }
            set { cuv24Wm = value; }
        }
        private float cuv24Ws;

        public float Cuv24Ws
        {
            get { return cuv24Ws; }
            set { cuv24Ws = value; }
        }
        private float cuv25Wm;

        public float Cuv25Wm
        {
            get { return cuv25Wm; }
            set { cuv25Wm = value; }
        }
        private float cuv25Ws;

        public float Cuv25Ws
        {
            get { return cuv25Ws; }
            set { cuv25Ws = value; }
        }
        private float cuv26Wm;

        public float Cuv26Wm
        {
            get { return cuv26Wm; }
            set { cuv26Wm = value; }
        }
        private float cuv26Ws;

        public float Cuv26Ws
        {
            get { return cuv26Ws; }
            set { cuv26Ws = value; }
        }
        private float cuv27Wm;

        public float Cuv27Wm
        {
            get { return cuv27Wm; }
            set { cuv27Wm = value; }
        }
        private float cuv27Ws;

        public float Cuv27Ws
        {
            get { return cuv27Ws; }
            set { cuv27Ws = value; }
        }
        private float cuv28Wm;

        public float Cuv28Wm
        {
            get { return cuv28Wm; }
            set { cuv28Wm = value; }
        }
        private float cuv28Ws;

        public float Cuv28Ws
        {
            get { return cuv28Ws; }
            set { cuv28Ws = value; }
        }
        private float cuv29Wm;

        public float Cuv29Wm
        {
            get { return cuv29Wm; }
            set { cuv29Wm = value; }
        }
        private float cuv29Ws;

        public float Cuv29Ws
        {
            get { return cuv29Ws; }
            set { cuv29Ws = value; }
        }
        private float cuv30Wm;

        public float Cuv30Wm
        {
            get { return cuv30Wm; }
            set { cuv30Wm = value; }
        }
        private float cuv30Ws;

        public float Cuv30Ws
        {
            get { return cuv30Ws; }
            set { cuv30Ws = value; }
        }
        private float cuv31Wm;

        public float Cuv31Wm
        {
            get { return cuv31Wm; }
            set { cuv31Wm = value; }
        }
        private float cuv31Ws;

        public float Cuv31Ws
        {
            get { return cuv31Ws; }
            set { cuv31Ws = value; }
        }
        private float cuv32Wm;

        public float Cuv32Wm
        {
            get { return cuv32Wm; }
            set { cuv32Wm = value; }
        }
        private float cuv32Ws;

        public float Cuv32Ws
        {
            get { return cuv32Ws; }
            set { cuv32Ws = value; }
        }
        private float cuv33Wm;

        public float Cuv33Wm
        {
            get { return cuv33Wm; }
            set { cuv33Wm = value; }
        }
        private float cuv33Ws;

        public float Cuv33Ws
        {
            get { return cuv33Ws; }
            set { cuv33Ws = value; }
        }
        private float cuv34Wm;

        public float Cuv34Wm
        {
            get { return cuv34Wm; }
            set { cuv34Wm = value; }
        }
        private float cuv34Ws;

        public float Cuv34Ws
        {
            get { return cuv34Ws; }
            set { cuv34Ws = value; }
        }
        private float cuv35Wm;

        public float Cuv35Wm
        {
            get { return cuv35Wm; }
            set { cuv35Wm = value; }
        }
        private float cuv35Ws;

        public float Cuv35Ws
        {
            get { return cuv35Ws; }
            set { cuv35Ws = value; }
        }
        private float cuv36Wm;

        public float Cuv36Wm
        {
            get { return cuv36Wm; }
            set { cuv36Wm = value; }
        }
        private float cuv36Ws;

        public float Cuv36Ws
        {
            get { return cuv36Ws; }
            set { cuv36Ws = value; }
        }
        private float cuv37Wm;

        public float Cuv37Wm
        {
            get { return cuv37Wm; }
            set { cuv37Wm = value; }
        }
        private float cuv37Ws;

        public float Cuv37Ws
        {
            get { return cuv37Ws; }
            set { cuv37Ws = value; }
        }
        private float cuv38Wm;

        public float Cuv38Wm
        {
            get { return cuv38Wm; }
            set { cuv38Wm = value; }
        }
        private float cuv38Ws;

        public float Cuv38Ws
        {
            get { return cuv38Ws; }
            set { cuv38Ws = value; }
        }
        private float cuv39Wm;

        public float Cuv39Wm
        {
            get { return cuv39Wm; }
            set { cuv39Wm = value; }
        }
        private float cuv39Ws;

        public float Cuv39Ws
        {
            get { return cuv39Ws; }
            set { cuv39Ws = value; }
        }
        private float cuv40Wm;

        public float Cuv40Wm
        {
            get { return cuv40Wm; }
            set { cuv40Wm = value; }
        }
        private float cuv40Ws;

        public float Cuv40Ws
        {
            get { return cuv40Ws; }
            set { cuv40Ws = value; }
        }
        private float cuv41Wm;

        public float Cuv41Wm
        {
            get { return cuv41Wm; }
            set { cuv41Wm = value; }
        }
        private float cuv41Ws;

        public float Cuv41Ws
        {
            get { return cuv41Ws; }
            set { cuv41Ws = value; }
        }
        private float cuv42Wm;

        public float Cuv42Wm
        {
            get { return cuv42Wm; }
            set { cuv42Wm = value; }
        }
        private float cuv42Ws;

        public float Cuv42Ws
        {
            get { return cuv42Ws; }
            set { cuv42Ws = value; }
        }
        private float cuv43Wm;

        public float Cuv43Wm
        {
            get { return cuv43Wm; }
            set { cuv43Wm = value; }
        }
        private float cuv43Ws;

        public float Cuv43Ws
        {
            get { return cuv43Ws; }
            set { cuv43Ws = value; }
        }
        private float cuv44Wm;

        public float Cuv44Wm
        {
            get { return cuv44Wm; }
            set { cuv44Wm = value; }
        }
        private float cuv44Ws;

        public float Cuv44Ws
        {
            get { return cuv44Ws; }
            set { cuv44Ws = value; }
        }
        private float cuv45Wm;

        public float Cuv45Wm
        {
            get { return cuv45Wm; }
            set { cuv45Wm = value; }
        }
        private float cuv45Ws;

        public float Cuv45Ws
        {
            get { return cuv45Ws; }
            set { cuv45Ws = value; }
        }
        private float cuv46Wm;

        public float Cuv46Wm
        {
            get { return cuv46Wm; }
            set { cuv46Wm = value; }
        }
        private float cuv46Ws;

        public float Cuv46Ws
        {
            get { return cuv46Ws; }
            set { cuv46Ws = value; }
        }
        private float cuv47Wm;

        public float Cuv47Wm
        {
            get { return cuv47Wm; }
            set { cuv47Wm = value; }
        }
        private float cuv47Ws;

        public float Cuv47Ws
        {
            get { return cuv47Ws; }
            set { cuv47Ws = value; }
        }
        private float cuv48Wm;

        public float Cuv48Wm
        {
            get { return cuv48Wm; }
            set { cuv48Wm = value; }
        }
        private float cuv48Ws;

        public float Cuv48Ws
        {
            get { return cuv48Ws; }
            set { cuv48Ws = value; }
        }
        private float cuv49Wm;

        public float Cuv49Wm
        {
            get { return cuv49Wm; }
            set { cuv49Wm = value; }
        }
        private float cuv49Ws;

        public float Cuv49Ws
        {
            get { return cuv49Ws; }
            set { cuv49Ws = value; }
        }
        private float cuv50Wm;

        public float Cuv50Wm
        {
            get { return cuv50Wm; }
            set { cuv50Wm = value; }
        }
        private float cuv50Ws;

        public float Cuv50Ws
        {
            get { return cuv50Ws; }
            set { cuv50Ws = value; }
        }
        // 存储主波长和次波长一系列值
        public List<float> CuvXWmList = new List<float>();
        public List<float> CuvXWsList = new List<float>();
    }
}
