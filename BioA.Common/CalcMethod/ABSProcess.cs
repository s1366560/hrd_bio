using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace BioA.Common.CalcMethod
{
    public class SV
    {
        public float K { get; set; }
        public float R { get; set; }
    }
    public class NaL
    {
        public int index { get; set; }
        public int count { get; set; }
    }
    public class ABSProcess
    {
        public static void Least_Squaresstraight_LineFit(float[] X, float[] Y, out float K, out float B, out float R)
        {
            K = 0;
            B = 0;
            R = 1;

            if(X.Length!=Y.Length)
            {
                return;
            }

            float sum_x = 0;
            float sum_y = 0;
            float sum_xx = 0;
            float sum_yy = 0;
            float sum_xy = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum_x += X[i];
                sum_y += Y[i];
                sum_xx += X[i] * X[i];
                sum_yy += Y[i] * Y[i];
                sum_xy += X[i] * Y[i];
            }

            float av_x = sum_x / X.Length;
            float av_y = sum_y / X.Length;
            float av_xx = sum_xx / X.Length;
            float av_yy = sum_yy / X.Length;
            float av_xy = sum_xy / X.Length;

            K = (av_xy - av_x * av_y) / (av_xx - av_x * av_x);
            B = av_y - K * av_x;
            float f1=0;
            float f2=0;
            float f3=0;
            for (int i = 0; i < X.Length; i++)
            {
                f1 += (X[i] - av_x) * (Y[i] - av_y);
                f2 += (X[i] - av_x) * (X[i] - av_x);
                f3 += (Y[i] - av_y) * (Y[i] - av_y);
            }
            
            //double R1 = (av_xy - av_x * av_y)/ Math.Sqrt((av_xx - av_x*av_x)*(av_yy-av_y*av_y));

           if(f2!=0&&f3!=0) R = f1*f1/f2/f3;

        }

       
        //一点法
        public static float OnePoint(TimeCourseInfo TC, AssayProjectParamInfo AR, string VolType)
        {
            float[] AbsList = null;
            float[] TimeList = null;
            ProcessAbsLinear(TC, AR, out AbsList, out TimeList, VolType);
            float Sum = 0;
            for (int i = AR.MeasureLightDot3; i <= AR.MeasureLightDot4; i++)
            {
                Sum += AbsList[i - 1];
            }
            return Sum / (AR.MeasureLightDot4 - AR.MeasureLightDot3 + 1);
        }
        //二点法
        public static float TwoPoint(TimeCourseInfo TC, AssayProjectParamInfo AR, string VolType)
        {
            float[] AbsList = null;
            float[] TimeList = null;
            ProcessAbsLinear(TC, AR, out AbsList, out TimeList, VolType);

            float SecondPointSum = 0;
            for (int i = AR.MeasureLightDot3; i <= AR.MeasureLightDot4; i++)
            {
                SecondPointSum += AbsList[i - 1];
            }
            float SecondPointAbs = SecondPointSum / (AR.MeasureLightDot4 - AR.MeasureLightDot3 + 1);

            float FirstPointSum = 0;
            for (int i = AR.MeasureLightDot1; i <= AR.MeasureLightDot2; i++)
            {
                FirstPointSum += AbsList[i - 1];
            }
            float FirstPointAbs = FirstPointSum / (AR.MeasureLightDot2 - AR.MeasureLightDot1 + 1);

            return SecondPointAbs - FirstPointAbs;
        }
        public static void TimeCourseLineFit(TimeCourseInfo TC, AssayProjectParamInfo AR, out float K1, out float B1, out float R1, out float K2, out float B2, out float R2)
        {
            K1 = 0;
            B1 = 0;
            R1 = 0;
            K2 = 0;
            B2 = 0;
            R2 = 0;
            if (AR == null || TC == null)
            {
                return;
            }
            if (AR.MeasureLightDot4 == 0 && AR.MeasureLightDot3 == 0)
            {
                return;
            }

            float[] AbsList = null;
            float[] TimeList = null;
            ProcessAbsLinear(TC, AR, out AbsList, out TimeList, null);

            float[] X2 = new float[AR.MeasureLightDot4 - AR.MeasureLightDot3 + 1];
            float[] Y2 = new float[AR.MeasureLightDot4 - AR.MeasureLightDot3 + 1];
            for (int j = 0, i = AR.MeasureLightDot3; i <= AR.MeasureLightDot4; j++, i++)
            {
                X2[j] = TimeList[i - 1];
                Y2[j] = AbsList[i - 1];
            }

            Least_Squaresstraight_LineFit(X2, Y2, out K2, out  B2, out  R2);
            

            if (AR.MeasureLightDot4 == 0 && AR.MeasureLightDot3 == 0)
            {
                return;
            }

            float[] X1 = new float[AR.MeasureLightDot2 - AR.MeasureLightDot1 + 1];
            float[] Y1 = new float[AR.MeasureLightDot2 - AR.MeasureLightDot1 + 1];
            for (int j = 0, i = AR.MeasureLightDot1; i <= AR.MeasureLightDot2; j++, i++)
            {
                X1[j] = TimeList[i - 1];
                Y1[j] = AbsList[i - 1];
            }

            Least_Squaresstraight_LineFit(X1, Y1, out K1, out  B1, out  R1); 
            
        }

        //滤波算法
        static float RateProcessC(float[] AbsList, float[] TimeList, int start, int end)
        {
            float[] AbsFitList;
            RateAbsFit(AbsList, TimeList, out AbsFitList);

            //string v = "";
            //foreach (float e in AbsFitList)
            //{
            //    v += e.ToString("#0.0000");
            //    v += "	";
            //}
            //LogService.Log(v, "c1.lg");

            float[] X = new float[end - start + 1];
            float[] Y = new float[end - start + 1];
            for (int j = 0, i = start; i <= end; j++, i++)
            {
                X[j] = TimeList[i - 1];
                Y[j] = AbsList[i - 1];
            }
            float K = 0;
            float B = 0;
            float R = 0;
            Least_Squaresstraight_LineFit(X, Y, out K, out  B, out  R);

            return K;
        }

        //剔除
        static float RateProcessB(float[] AbsList, float[] TimeList, int start, int end)
        {
            List<int> PList = new List<int>();
            for (int i = start; i <= end; i++)
            {
                switch (i)
                {
                    //case 29: break;
                    //case 30: break;
                    //case 31: break;
                    //case 32: break;
                    default:
                        PList.Add(i);
                        break;
                }
            }

            float[] X = new float[PList.Count];
            float[] Y = new float[PList.Count];
            int j = 0;
            foreach (int p in PList)
            {
                string t = TimeList[p - 1].ToString();
                X[j] = float.Parse(t);
                string a = AbsList[p - 1].ToString();
                Y[j] = float.Parse(a);
                j++;
            }

            float K = 0;
            float B = 0;
            float R = 0;
            Least_Squaresstraight_LineFit(X, Y, out K, out  B, out  R);

            string sk = K.ToString();
            
            return float.Parse(sk);
        }
        static float RateProcessA(float[] AbsList, float[] TimeList, int start, int end,out bool iskey)
        {
            //计算全体线性度
            float[] X = new float[end - start + 1];
            float[] Y = new float[end - start + 1];
            for (int j = 0, i = start; i <= end; j++, i++)
            {
                X[j] = TimeList[i - 1];
                Y[j] = AbsList[i - 1];
            }

            //吸光度变化度
            float[] RY = new float[Y.Length - 1];
            for (int i = 0; i < RY.Length; i++)
            {
                RY[i] = Y[i + 1] - Y[i];
            }

            //吸光度变化度的变化度
            float[] RRY = new float[RY.Length - 1];
            for (int i = 0; i < RRY.Length; i++)
            {
                RRY[i] = RY[i + 1] - RY[i];
            }

            //找到异常点
            for (int i = 0; i < RRY.Length; i++)
            {
                if (Math.Abs(RRY[i]) > 0.0003)//判断阀值
                {
                    Y[i + 1] = float.NaN;
                }
            }
            //遍历距离
            NaL NaL = null;
            List<NaL> LNaL = new List<NaL>();
            for (int i = 0; i < Y.Length; i++)
            {
                if ( float.IsNaN( Y[i]))
                {
                    if (NaL != null)
                    {
                        LNaL.Add(NaL);
                        NaL = null;
                    }
                }
                else
                {
                    if (NaL == null)
                    {
                        NaL = new NaL();
                        NaL.index = i;
                    }
                    NaL.count += 1;
                }

                if (i == Y.Length - 1)
                {
                    if (NaL != null)
                    {
                        LNaL.Add(NaL);
                        NaL = null;
                    }
                }
            }
            //找出最好的数据段
            int cout = -1;
            foreach (NaL e in LNaL)
            {
                cout = e.count > cout ? e.count : cout;
            }
            
            if (cout >= 4)
            {
                foreach (NaL e in LNaL)
                {
                    if (e.count == cout)
                    {
                        NaL = e;
                        break;
                    }
                }

                float[] X1 = new float[cout];
                float[] Y1 = new float[cout];
                for (int i = 0, j = NaL.index; i < NaL.count; i++,j++)
                {
                    X1[i] = X[j];
                    Y1[i] = Y[j];
                }

                float K = 0;
                float B = 0;
                float R = 0;
                Least_Squaresstraight_LineFit(X1, Y1, out K, out  B, out  R);

                iskey = true;
                return K;
            }

            iskey = false;
            return 0;
        }

        static float RateProcess(float[] AbsList, float[] TimeList, int start, int end)
        {
            //计算全体线性度
            float[] X = new float[end - start + 1];
            float[] Y = new float[end - start + 1];
            for (int j = 0, i = start; i <= end; j++, i++)
            {
                X[j] = TimeList[i - 1];
                Y[j] = AbsList[i - 1];
            }
            float K = 0;
            float B = 0;
            float R = 0;
            Least_Squaresstraight_LineFit(X, Y, out K, out  B, out  R);
            //线性相关性判读
            if (Math.Abs(R - 1) > 0.05)
            {
                float[] X1 = null;
                float[] Y1 = null;
                ProcessData(X, Y, K, out X1, out Y1);
                if (X1.Length >= 4)//剔除异常点
                {
                    Least_Squaresstraight_LineFit(X1, Y1, out K, out  B, out  R);
                }
                else
                {
                    //查点
                    List<float> XList = new List<float>();
                    List<float> YList = new List<float>();
                    for (int i = start; i <= end; i++)
                    {
                        switch (i)
                        {
                            case 30: break;
                            case 31: break;
                            default:
                                XList.Add(TimeList[i - 1]);
                                YList.Add(AbsList[i - 1]);
                                break;
                        }
                    }
                    DynicLeast_Squaresstraight_LineFit(XList, YList, out K, out B, out R);
                }
            }


            return K;
        }

       
        //拟合修正
        static void ProcessData(float[] XSrc, float[] YSrc, float RefK,out float[] X,out float[] Y)
        {
            float[] X1 = new float[4];
            float[] Y1 = new float[4];

            List<float> XList = new List<float>();
            List<float> YList = new List<float>();
            List<SV> KList = new List<SV>();

            int validIndex = 0;
            for (int i = 0; i < XSrc.Length-3; i++)
            {
                X1[0] = XSrc[i];
                Y1[0] = YSrc[i];

                X1[1] = XSrc[i+1];
                Y1[1] = YSrc[i+1];

                X1[2] = XSrc[i+2];
                Y1[2] = YSrc[i+2];

                X1[3] = XSrc[i + 3];
                Y1[3] = YSrc[i + 3];

                float K = 0;
                float B = 0;
                float R = 0;
                Least_Squaresstraight_LineFit(X1, Y1, out K, out  B, out  R);

                SV SV = new SV();
                SV.K = K;
                SV.R = R;
                KList.Add(SV);


                if (Math.Abs(R - 1) < 0.05)
                {
                    XList.Add(XSrc[i]);
                    XList.Add(XSrc[i + 1]);
                    XList.Add(XSrc[i + 2]);
                    XList.Add(XSrc[i + 3]);

                    YList.Add(YSrc[i]);
                    YList.Add(YSrc[i+1]);
                    YList.Add(YSrc[i+2]);
                    YList.Add(YSrc[i+3]);

                    validIndex = i;
                    break;
                }
            }

            for (int j = validIndex + 3 + 1; j < XSrc.Length; j++)
            {
                XList.Add(XSrc[j]);
                YList.Add(YSrc[j]);

                float K1 = 0;
                float B1 = 0;
                float R1 = 0;
                DynicLeast_Squaresstraight_LineFit(XList, YList, out K1, out B1, out R1);
                if (Math.Abs(R1 - 1) < 0.05)
                {
                    continue;
                }
                else
                {
                    XList.Remove(XSrc[j]);
                    YList.Remove(YSrc[j]);
                }
            }

            X = new float[XList.Count];
            for (int i = 0; i < X.Length; i++)
            {
                X[i] = XList[i];
            }

            Y = new float[YList.Count];
            for (int i = 0; i < Y.Length; i++)
            {
                Y[i] = YList[i];
            }
        }

        static void DynicLeast_Squaresstraight_LineFit(List<float> XList, List<float> YList, out float K, out float B, out float R)
        {
            K = 0;
            B = 0;
            R = 0;

            if (XList.Count != YList.Count)
            {
                return;
            }

            float[] X = new float[XList.Count];
            for (int i = 0; i < X.Length; i++)
            {
                X[i] = XList[i];
            }

            float[] Y = new float[YList.Count];
            for (int i = 0; i < Y.Length; i++)
            {
                Y[i] = YList[i];
            }

            Least_Squaresstraight_LineFit(X, Y, out K, out  B, out  R);
        }

        //滤波
        static void RateAbsFit(float[] AbsList,float[] TimeList,out float[] AbsFitList)
        {
            List<float> XList = new List<float>();
            List<float> YList = new List<float>();
            for (int i = 26; i < 36; i++)
            {
                switch (i)
                {
                    case 29: break;
                    case 30: break;
                    case 31: break;
                    default:
                        XList.Add(TimeList[i]);
                        YList.Add(AbsList[i]);
                        break;
                }
            }

            float K1 = 0;
            float B1 = 0;
            float R1 = 0;
            DynicLeast_Squaresstraight_LineFit(XList, YList, out K1, out B1, out R1);

            AbsFitList = new float[AbsList.Length];
            for (int i = 0; i < AbsList.Length; i++)
            {
                AbsFitList[i] = AbsList[i];
            }

            AbsFitList[29] = K1 * TimeList[29] + B1;
            AbsFitList[30] = K1 * TimeList[30] + B1;
            AbsFitList[31] = K1 * TimeList[31] + B1;
        }
        //速A法
        public static float RateA(TimeCourseInfo TC, AssayProjectParamInfo AR, string VolType)
        {
            float[] AbsList = null;
            float[] TimeList = null;
            ProcessAbsLinear(TC, AR, out AbsList, out TimeList, VolType);

            return RateProcessC(AbsList, TimeList, AR.MeasureLightDot3, AR.MeasureLightDot4);
        }
        //速B法
        public static float RateB(TimeCourseInfo TC, AssayProjectParamInfo AR, string VolType)
        {
            float[] AbsList = null;
            float[] TimeList = null;
            ProcessAbsLinear(TC, AR, out AbsList, out TimeList, VolType);

            float K2 = RateProcessC(AbsList, TimeList, AR.MeasureLightDot3, AR.MeasureLightDot4);
            float K1 = RateProcessC(AbsList, TimeList, AR.MeasureLightDot1, AR.MeasureLightDot2);

            return K2 - K1;
        }
        //试剂吸光度
        public static float GetReangentAbs(TimeCourseInfo TC, AssayProjectParamInfo AR)
        {
            float Abs1 = TC.CuvXWmList[0] - TC.CuvBlkWm;
            float Abs2 = TC.CuvXWsList[0] - TC.CuvBlkWs;
           
            return AR.SecWaveLength == 0 ? Abs1 : Abs1 - Abs2;
        }
        //底物吸光度
        public static float GetAbsLimAbs(TimeCourseInfo TC, AssayProjectParamInfo AR)
        {
            float WmAbs = TC.CuvXWmList[AR.MeasureLightDot4 - 1] - TC.CuvBlkWm;
            float WsAbs = TC.CuvXWsList[AR.MeasureLightDot4 - 1] - TC.CuvBlkWs;
            
            return AR.SecWaveLength == 0 ? WmAbs : WmAbs - WsAbs;
        }
        
        //获取每个吸光度读数点
        //public static float[] ProcessAbsList(TimeCourse T, AssayRunPara A)
        //{
        //    AssayValuePara AV = new AssayValueParaService().Get(A.Name) as AssayValuePara;
        //    float[] AbsList = new float[MachineInfo.LastAbsPoint];
        //    for (int i = 0; i < MachineInfo.LastAbsPoint; i++)
        //    {
        //        float c = A.SubWaveLength == 0 ? (T.CuvXWmList[i] - T.CuvBlkWm) : ((T.CuvXWmList[i] - T.CuvBlkWm) - (T.CuvXWsList[i] - T.CuvBlkWs));
        //        if (AV != null)
        //        {
        //            AbsList[i] = c * AV.EquipAdjustRfA + AV.EquipAdjustRfB;
        //        }
        //        else
        //        {
        //            AbsList[i] = c;
        //        }

                
        //    }
        //    return AbsList;
        //}
        //获取每个吸光度读数点
        public static float[] ProcessAbsList(TimeCourseInfo T, AssayProjectParamInfo A, string VT)
        {
            float[] AbsList = new float[RunConfigureUtility.LastPoint];
            for (int i = 0; i < RunConfigureUtility.LastPoint; i++)
            {
                float c = A.SecWaveLength == 0 ? (T.CuvXWmList[i] - T.CuvBlkWm) : ((T.CuvXWmList[i] - T.CuvBlkWm) - (T.CuvXWsList[i] - T.CuvBlkWs));

                //R2体积修正
                if ((A != null) && (A.AnalysisMethod == "二点终点法" || A.AnalysisMethod == "速率B法") && i < (RunConfigureUtility.R2Point - 1))
                {
                    float k = 1;
                    switch (VT)
                    {
                        case "减量体积":
                                if (A.DecSamVol != 0)
                                {
                                    k = (A.Reagent1VolSettings + A.DecSamVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.DecSamVol);
                                }
                                else
                                {
                                    k = (A.Reagent1VolSettings + A.DecStosteVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.DecStosteVol);
                                }                            
                            break;
                        case "常规体积":
                            if (A.ComSamVol != 0)
                            {
                                k = (A.Reagent1VolSettings + A.ComSamVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.ComSamVol);
                            }
                            else
                            {
                                k = (A.Reagent1VolSettings + A.ComStosteVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.ComStosteVol);
                            }
                            break;
                        case "增量体积":
                            if (A.IncSamVol != 0)
                            {
                                k = (A.Reagent1VolSettings + A.IncSamVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.IncSamVol);
                            }
                            else
                            {
                                k = (A.Reagent1VolSettings + A.IncStosteVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.IncStosteVol);
                            }
                            break;
                        case "定标体积":
                            if (A.CalibSamVol != 0)
                            {
                                k = (A.Reagent1VolSettings + A.CalibSamVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.CalibSamVol);
                            }
                            else
                            {
                                k = (A.Reagent1VolSettings + A.CalibStosteVol) / (A.Reagent1VolSettings + A.Reagent2VolSettings + A.CalibStosteVol);
                            }
                            break;
                        case "自定义":
                            break;
                    }
                    c = c * k;
                }

                AbsList[i] = c ;
            }
            return AbsList;
        }
        //建立吸光度与时间关系
        public static void ProcessAbsLinear(TimeCourseInfo T, AssayProjectParamInfo A, out float[] AbsList, out float[] TimeList, string VT)
        {
            AbsList = ProcessAbsList(T,A,VT);
            TimeList = new float[AbsList.Count()];

            for (int i = 0; i < AbsList.Count(); i++)
            {
                TimeList[i] = RunConfigureUtility.GetTimeCourseTime(i) / 60;
            }
        }
        
        //获取前驱界限
        public static float GetProzontLimitValue(TimeCourseInfo T, AssayProjectParamInfo A, string VT, string VS)
        {
            float[] AbsList = ProcessAbsList(T, A, VT);
            float max = AbsList[0];
            for (int i = 0; i < AbsList.Count(); i++)
            {
                if (max < AbsList[i])
                {
                    max = AbsList[i];
                }
            }
            if (max > -0.000001 && max < 0.000001)
            {
                return 0.0f;
            }

            float sum = 0; int p = 0;
            for (int i = RunConfigureUtility.R2Point; i < RunConfigureUtility.BlankPoint; i++)
            {
                sum += RunConfigureUtility.PTInterval[i];
                if (sum > 150)
                {
                    p = i;
                    break;
                }
            }

            return AbsList[p-1] / max;

        }
    }
}
