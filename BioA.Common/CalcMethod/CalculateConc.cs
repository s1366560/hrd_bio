using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioA.Common.CalcMethod
{
    public class MatrixGauss
    {
        public static void Gauss(int n, double[] a, double[] x)
        {
            double[,] d = new double[n, n + 1];
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n + 1; i++)
                {
                    d[j, i] = a[j * (n + 1) + i];
                }
            }
            Gauss(n, d, x);
        }
        public static void Gauss(int n, double[,] a, double[] x)
        {
            double d;
            // 消元        
            for (int k = 0; k < n; k++)
            {
                selectMainElement(n, k, a);// 选择主元素                   
                // for (int j = k; j <= n; j++ ) a[k, j] = a[k, j] / a[k, k];          
                // 若将下面两个语句改为本语句，则程序会出错，因为经过第1次循环          
                // 后a[k,k]=1，a[k,k]的值发生了变化，所以在下面的语句中先用d          
                // 将a[k,k]的值保存下来          
                d = a[k, k];
                for (int j = k; j <= n; j++)
                    a[k, j] = a[k, j] / d;

                // Guass消去法与Jordan消去法的主要区别就是在这一步，Gauss消去法是从k+1          
                // 到n循环，而Jordan消去法是从1到n循环，中间跳过第k行          
                for (int i = k + 1; i < n; i++)
                {
                    d = a[i, k];  // 这里使用变量d将a[i,k]的值保存下来的原理与上面注释中说明的一样             
                    for (int j = k; j <= n; j++) a[i, j] = a[i, j] - d * a[k, j];
                }
            }
            // 回代        
            x[n - 1] = a[n - 1, n];
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = a[i, n];
                for (int j = i + 1; j < n; j++) x[i] = x[i] - a[i, j] * x[j];
            }
        }
        // 选择主元素      
        public static void selectMainElement(int n, int k, double[,] a)
        {
            // 寻找第k列的主元素以及它所在的行号        
            double t, mainElement;            // mainElement用于保存主元素的值        
            int l;                            // 用于保存主元素所在的行号         
            // 从第k行到第n行寻找第k列的主元素，记下主元素mainElement和所在的行号l        
            mainElement = Math.Abs(a[k, k]);  // 注意别忘了取绝对值        
            l = k;
            for (int i = k + 1; i < n; i++)
            {
                if (mainElement < Math.Abs(a[i, k]))
                {
                    mainElement = Math.Abs(a[i, k]);
                    l = i;                        // 记下主元素所在的行号          
                }
            }
            // l是主元素所在的行。将l行与k行交换，每行前面的k个元素都是0，不必交换        
            if (l != k)
            {
                for (int j = k; j <= n; j++)
                {
                    t = a[k, j]; a[k, j] = a[l, j]; a[l, j] = t;
                }
            }
        }
    }
    public class TABLE
    {
        float _conc = 0.0f;
        public float conc
        {
            get { return _conc; }
            set { _conc = value; }
        }
        float _abs = 0.0f;
        public float abs
        {
            get { return _abs; }
            set { _abs = value; }
        }
    }
    public class CalculateConc
    {
        static float m_AbsoluteFactor = 0.0f;
        static TABLE[] m_data = null;
        public CalculateConc()
        {

        }
        static object locker = new object();
        public static float GetConc(SDTTableItem sdttable, float abs)
        {
            if (sdttable == null)
            {
                return 0;
            }
            lock (locker)
            {
                m_data = new TABLE[7];
                m_data[0] = new TABLE();
                m_data[0].abs = 0;
                m_data[0].conc = 0;
                m_data[1] = new TABLE();
                m_data[1].abs = 0;
                m_data[1].conc = 0;
                m_data[2] = new TABLE();
                m_data[2].abs = 0;
                m_data[2].conc = 0;
                m_data[3] = new TABLE();
                m_data[3].abs = 0;
                m_data[3].conc = 0;
                m_data[4] = new TABLE();
                m_data[4].abs = 0;
                m_data[4].conc = 0;
                m_data[5] = new TABLE();
                m_data[5].abs = 0;
                m_data[5].conc = 0;
                m_data[6] = new TABLE();
                m_data[6].abs = 0;
                m_data[6].conc = 0;

                m_data[0].abs = sdttable.BlkAbs;
                m_data[1].abs = sdttable.SDT1Abs;
                m_data[2].abs = sdttable.SDT2Abs;
                m_data[3].abs = sdttable.SDT3Abs;
                m_data[4].abs = sdttable.SDT4Abs;
                m_data[5].abs = sdttable.SDT5Abs;
                m_data[6].abs = sdttable.SDT6Abs;

                m_data[0].conc = sdttable.BlkConc;
                m_data[1].conc = sdttable.SDT1Conc;
                m_data[2].conc = sdttable.SDT2Conc;
                m_data[3].conc = sdttable.SDT3Conc;
                m_data[4].conc = sdttable.SDT4Conc;
                m_data[5].conc = sdttable.SDT5Conc;
                m_data[6].conc = sdttable.SDT6Conc;

                m_AbsoluteFactor = sdttable.AbsoluteFactor;
                switch (sdttable.CalibMethod)
                {
                    //case "K系数法": return ByAbsolute(abs);
                    case "2点线性法": return ByStraight(abs);
                    case "多点线性法": return ByMultiPTLine(abs);
                    case "对数法": return ByLogitLog(abs);
                    case "指数法": return ByExponential(abs);
                    case "折线法": return BySegmentLine(abs);
                    case "多项式法": return ByPolynomial(abs);
                    case "样条法": return ByCubilSpline(abs);
                    case "一次多项式法": return ByPoly1(abs);
                    case "二次多项式法": return ByPoly2(abs);
                    case "三次多项式法": return ByPoly3(abs);
                    case "四次多项式法": return ByPoly4(abs);
                }


            }

            return 0;
        }

        //public static float ByAbsolute(float abs)
        //{
        //    return (abs - m_data[0].abs) * m_AbsoluteFactor;
        //}

        public static float GetKConftMethodConc(AssayProjectCalibrationParamInfo asssyCalibParam, float abs)
        {
            return abs * asssyCalibParam.Factor;
        }

        public static float ByStraight(float abs)
        {
            float conc;
            float f = 0;
            float b = 0;

            if ((m_data[1].abs - m_data[0].abs) == 0)
            {
                f = 0;
            }
            else
            {
                f = (m_data[1].conc - m_data[0].conc) / (m_data[1].abs - m_data[0].abs);
                b = (m_data[0].abs * m_data[1].conc - m_data[1].abs * m_data[0].conc) / (m_data[1].conc - m_data[0].conc);
            }

            conc = (abs - b) * f;

            return conc;
        }
        public static float ByMultiPTLine(float abs)
        {
            int num = 0;
            float allc = 0;
            float alla = 0;
            float allsc = 0;
            float allac = 0;
            float allsa = 0;
            for (int i = 0; i < 6; i++)
            {
                if (m_data[i].abs != 0)
                {
                    num++;
                    allc += m_data[i].conc;
                    alla += m_data[i].abs;
                    allsc += m_data[i].conc * m_data[i].conc;
                    allsa += m_data[i].abs * m_data[i].abs;
                    allac += m_data[i].conc * m_data[i].abs;
                }
            }
            float fac = allac - allc * (alla / num);
            float fc = allsc - allc * (allc / num);

            float b = fac / fc;
            float a = alla / num - b * allc / num;
            float conc = 0;
            if (b != 0)
                conc = (abs - a) / b;
            else conc = -1;

            return conc;
        }
        static float LogitLog(float abs)
        {
            if (abs == 0) return 0;

            double[] d = new double[12];

            int num = 0;
            float alld = 0;
            float allc = 0;
            float allcd = 0;
            float alldd = 0;

            float allcc = 0;
            float y = 0;
            float yd = 0;
            float yc = 0;
            float b = 0;
            float k = 0;

            int i;
            float max = -4;
            float min = 4;

            TABLE[] m = new TABLE[6];
            if (m_data[1].abs < 0 || m_data[2].abs < 0 || m_data[3].abs < 0 || m_data[4].abs < 0 || m_data[5].abs < 0)
            {
                for (i = 0; i < 6; i++)
                {
                    m[i] = new TABLE();
                    m[i].abs = -m_data[i].abs;
                    m[i].conc = m_data[i].conc;
                }
                abs = -abs;
            }
            else
            {
                for (i = 0; i < 6; i++)
                {
                    m[i] = new TABLE();
                    m[i].abs = m_data[i].abs;
                    m[i].conc = m_data[i].conc;
                }
            }

            for (i = 1; i < 6; i++)
            {
                if (m[i].abs == 0 || m[i].conc == 0)
                    continue;
                if (m[i].abs > max)
                    max = m[i].abs;
                if (m[i].abs < min)
                    min = m[i].abs;
            }
            if (m[0].abs == 0)
            {
                k = (float)((max - min / 1.05) * 1.05);
                b = (float)(min / 1.05);
            }
            else
            {
                k = (float)((max - m[0].abs) * 1.05);
                b = m[0].abs / 1.0001f;
            }
            for (i = 0; i < 6; i++)
            {
                m[0].conc = 0.001F;
                if (m[i].abs == 0 || m[i].conc == 0)
                    continue;

                num++;
                alld += (float)Math.Log(m[i].conc);
                allc += m[i].conc;
                alldd += (float)(Math.Log(m[i].conc) * Math.Log(m[i].conc));
                allcd += (float)(m[i].conc * Math.Log(m[i].conc));
                allcc += (float)(m[i].conc * m[i].conc);
                double da = (m[i].abs - b) / (k - m[i].abs + b);
                y += (float)Math.Log(da);
                yd += (float)(Math.Log(da) * Math.Log(m[i].conc));
                yc += (float)(Math.Log(da) * m[i].conc);
            }
            d[0] = num;
            d[1] = alld;
            d[2] = allc;
            d[3] = y;
            d[4] = alld;
            d[5] = alldd;
            d[6] = allcd;
            d[7] = yd;
            d[8] = allc;
            d[9] = allcd;
            d[10] = allcc;
            d[11] = yc;

            double[] p = new double[3];
            MatrixGauss.Gauss(3, d, p);

            bool Bcontinu = true;
            double iniCdata = 0; double maxc = 0; double minc = 99999;
            for (i = 1; i < 6; i++)
            {
                if (m[i].abs == 0 || m[i].conc == 0)
                    continue;
                if (m[i].conc > maxc)
                    maxc = m[i].conc;
                if (m[i].conc < minc)
                    minc = m[i].conc;
            }

            iniCdata = minc;
            double r = 0;
            long n = 0;
            while (Bcontinu)
            {
                n++;
                double ss = (abs - b) / (k - abs + b);
                r = (p[1] * (1 - Math.Log(iniCdata)) - p[0] + Math.Log(ss)) / (p[1] / iniCdata + p[2]);
                if (Math.Abs(r - iniCdata) < 0.00001 || n > 5000)
                    Bcontinu = false;
                else
                {
                    iniCdata = r;
                }
            }
            return (float)r;
        }
        /// <summary>
        /// 对点线性法
        /// </summary>
        /// <param name="abs"></param>
        /// <returns></returns>
        public static float ByLogitLog(float abs)
        {
            float[] c = new float[5];
            if (m_data[1].abs != 0)
            {
                c[0] = LogitLog(m_data[1].abs);
                if (float.IsNaN(c[0]) == true)
                {
                    c[0] = m_data[1].conc;
                }
            }
            if (m_data[2].abs != 0)
            {
                c[1] = LogitLog(m_data[2].abs);
                if (float.IsNaN(c[1]) == true)
                {
                    c[1] = m_data[2].conc;
                }
            }
            if (m_data[3].abs != 0)
            {
                c[2] = LogitLog(m_data[3].abs);
                if (float.IsNaN(c[2]) == true)
                {
                    c[2] = m_data[3].conc;
                }
            }
            if (m_data[4].abs != 0)
            {
                c[3] = LogitLog(m_data[4].abs);
                if (float.IsNaN(c[3]) == true)
                {
                    c[3] = m_data[4].conc;
                }
            }
            if (m_data[5].abs != 0)
            {
                c[4] = LogitLog(m_data[5].abs);
                if (float.IsNaN(c[4]) == true)
                {
                    c[4] = m_data[5].conc;
                }
            }

            float[] cc = new float[6];
            cc[0] = Math.Abs(c[0] - m_data[1].conc);
            cc[1] = Math.Abs(c[1] - m_data[2].conc);
            cc[2] = Math.Abs(c[2] - m_data[3].conc);
            cc[3] = Math.Abs(c[3] - m_data[4].conc);
            cc[4] = Math.Abs(c[4] - m_data[5].conc);
            cc[5] = 0;
            int j = 0;
            double min = 0;
            for (int i = 0; i < 5; i++)
            {
                if (cc[i] > min)
                {
                    min = cc[i];
                    j = i;
                }
            }
            m_data[j + 1].conc = c[j];

            double r = LogitLog(abs);

            return (float)r;
        }
        public static float ByExponential(float abs)
        {
            int num = 0;
            double all1x = 0; double all2x = 0; double all3x = 0; double all4x = 0;
            double all5x = 0; double all6x = 0; double y = 0; double xy = 0; double x2y = 0;
            double x3y = 0;

            TABLE[] m = new TABLE[6];
            int i = 0;
            if (m_data[1].abs < 0 || m_data[2].abs < 0 || m_data[3].abs < 0 || m_data[4].abs < 0 || m_data[5].abs < 0)
            {
                for (i = 0; i < 6; i++)
                {
                    m[i] = new TABLE();
                    m[i].abs = -m_data[i].abs;
                    m[i].conc = m_data[i].conc;
                }
                abs = -abs;
            }
            else
            {
                for (i = 0; i < 6; i++)
                {
                    m[i] = new TABLE();
                    m[i].abs = m_data[i].abs;
                    m[i].conc = m_data[i].conc;
                }
            }

            for (i = 1; i < 6; i++)
            {
                if (m[i].abs == 0 || m[i].conc == 0)
                    continue;
                num++;
                all1x += Math.Log(m[i].conc);
                all2x += Math.Log(m[i].conc) * Math.Log(m[i].conc);
                all3x += Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc);
                all4x += Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc);
                all5x += Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc);
                all6x += Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc);
                y += Math.Log(m[i].abs - m[0].abs);
                xy += Math.Log(m[i].conc) * Math.Log(m[i].abs - m[0].abs);
                x2y += Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].abs - m[0].abs);
                x3y += Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].conc) * Math.Log(m[i].abs - m[0].abs);
            }

            double[] d = new double[20];
            d[0] = num;
            d[1] = all1x;
            d[2] = all2x;
            d[3] = all3x;
            d[4] = y;

            d[5] = all1x;
            d[6] = all2x;
            d[7] = all3x;
            d[8] = all4x;
            d[9] = xy;

            d[10] = all2x;
            d[11] = all3x;
            d[12] = all4x;
            d[13] = all5x;
            d[14] = x2y;

            d[15] = all3x;
            d[16] = all4x;
            d[17] = all5x;
            d[18] = all6x;
            d[19] = x3y;

            double[] p = new double[4];
            MatrixGauss.Gauss(4, d, p);

            bool Bcontinu = true;
            double iniCdata = 0; double maxc = 0; double minc = 99999;
            for (i = 1; i < 6; i++)
            {
                if (m[i].abs != 0 && m[i].conc != 0)
                {
                    if (m[i].conc > maxc)
                        maxc = m[i].conc;
                    if (m[i].conc < minc)
                        minc = m[i].conc;
                }
            }
            iniCdata = (Math.Log(maxc) + Math.Log(minc)) / 2;
            double r = 0;
            long n = 0;
            while (Bcontinu)
            {
                n++;
                r = (p[2] * iniCdata * iniCdata + 2 * p[3] * iniCdata * iniCdata * iniCdata - p[0] + Math.Log(abs - m[0].abs)) / (p[1] + 2 * p[2] * iniCdata + 3 * p[3] * iniCdata * iniCdata);
                if (Math.Abs(r - iniCdata) < 0.00001 || n < 5000)
                    Bcontinu = false;
                else
                {
                    iniCdata = r;
                }
            }
            return (float)Math.Exp(r);
        }
        static bool IsFloatZero(float f)
        {
            if (f > -0.0001f && f < 0.0001f)
            {
                return true;
            }
            return false;
        }
        public static float BySegmentLine(float abs)
        {
            TABLE[] tpdata = new TABLE[7];
            for (int i = 0; i < 7; i++)
            {
                tpdata[i] = new TABLE();
                tpdata[i].conc = 0;
                tpdata[i].abs = 0;
            }
            int j;
            if (m_data[0].abs != 0)
            {
                tpdata[0].conc = 0;
                tpdata[0].abs = m_data[0].abs;

                j = 1;
                for (int i = 1; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }
            else
            {
                j = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }

            j = 0;
            double r = -1;
            for (int i = 0; i < 6; i++)
            {
                if (m_data[1].abs > 0)
                {
                    if (abs >= tpdata[i].abs && abs < tpdata[i + 1].abs)
                    {
                        j = i;
                        break;
                    }
                    if (i == 5)
                    {
                        return 0;
                    }
                }
                else if (m_data[1].abs < 0)
                {
                    if (abs <= tpdata[i].abs && abs > tpdata[i + 1].abs)
                    {
                        j = i;
                        break;
                    }
                    if (i == 5)
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }

            double k = 0;
            double b = 0;
            k = (tpdata[j + 1].abs - tpdata[j].abs) / (tpdata[j + 1].conc - tpdata[j].conc);
            b = tpdata[j + 1].abs - k * tpdata[j + 1].conc;
            r = (abs - b) / k;
            return (float)r;
        }
        public static float ByPolynomial(float abs)
        {
            TABLE[] tpdata = new TABLE[6];
            for (int i = 0; i < 6; i++)
            {
                tpdata[i] = new TABLE();
                tpdata[i].conc = 0;
                tpdata[i].abs = 0;
            }

            int j;
            if (m_data[0].abs != 0)
            {
                tpdata[0].conc = 0;
                tpdata[0].abs = m_data[0].abs;
                j = 0;
                for (int i = 1; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }
            else
            {
                j = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }

            int n = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0 && tpdata[i].abs != 0)
                    n++;
                if (tpdata[i].abs != 0 && tpdata[i].conc != 0)
                    n++;
                else break;
            }

            if (n < 3) return -1;
            double r = 0;
            for (int i = 0; i < n; i++)
            {
                double p = 1;
                for (j = 0; j < n; j++)
                {
                    if (i == j)
                        continue;
                    else
                        p *= (abs - tpdata[j].abs) / (tpdata[i].abs - tpdata[j].abs);
                }
                r += tpdata[i].conc * p;
            }
            return (float)r;
        }
        public static float ByCubilSpline(float abs)
        {
            TABLE[] tpdata = new TABLE[6];
            for (int c = 0; c < 6; c++)
            {
                tpdata[c] = new TABLE();
                tpdata[c].conc = 0;
                tpdata[c].abs = 0;
            }
            int i = 0;
            int j = 0;
            while (i < 6)
            {
                if (m_data[i].abs == 0)
                    i++;
                else
                {
                    tpdata[j].conc = m_data[i].conc;
                    tpdata[j].abs = m_data[i].abs;
                    i++;
                    j++;
                }
            }

            int n = 0;
            for (i = 0; i < 6; i++)
            {
                if (tpdata[i].abs != 0)
                    n++;
                else break;
            }
            if (n < 3)
                return -1;

            double[] d = new double[420];
            double[] r = new double[20];

            double result = 0;
            if (n == 3)
            {
                d[0] = 0;
                d[1] = 0;
                d[2] = 2;
                d[3] = 6 * tpdata[0].abs;
                d[4] = 0;
                d[5] = 0;
                d[6] = 0;
                d[7] = 0;
                d[8] = 0;

                d[9] = 1;
                d[10] = tpdata[0].abs;
                d[11] = tpdata[0].abs * tpdata[0].abs;
                d[12] = tpdata[0].abs * tpdata[0].abs * tpdata[0].abs;
                d[13] = 0;
                d[14] = 0;
                d[15] = 0;
                d[16] = 0;
                d[17] = tpdata[0].conc;

                d[18] = 1;
                d[19] = tpdata[1].abs;
                d[20] = tpdata[1].abs * tpdata[1].abs;
                d[21] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[22] = 0;
                d[23] = 0;
                d[24] = 0;
                d[25] = 0;
                d[26] = tpdata[1].conc;

                d[27] = 0;
                d[28] = 0;
                d[29] = 2;
                d[30] = 6 * tpdata[1].abs;
                d[31] = 0;
                d[32] = 0;
                d[33] = -2;
                d[34] = (-6) * tpdata[1].abs;
                d[35] = 0;

                d[36] = 0;
                d[37] = 1;
                d[38] = 2 * tpdata[1].abs;
                d[39] = 3 * tpdata[1].abs * tpdata[1].abs;
                d[40] = 0;
                d[41] = -1;
                d[42] = (-2) * tpdata[1].abs;
                d[43] = (-3) * tpdata[1].abs * tpdata[1].abs;
                d[44] = 0;

                d[45] = 0;
                d[46] = 0;
                d[47] = 0;
                d[48] = 0;
                d[49] = 1;
                d[50] = tpdata[1].abs;
                d[51] = tpdata[1].abs * tpdata[1].abs;
                d[52] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[53] = tpdata[1].conc;

                d[54] = 0;
                d[55] = 0;
                d[56] = 0;
                d[57] = 0;
                d[58] = 1;
                d[59] = tpdata[2].abs;
                d[60] = tpdata[2].abs * tpdata[2].abs;
                d[61] = tpdata[2].abs * tpdata[2].abs * tpdata[2].abs;
                d[62] = tpdata[2].conc;

                d[63] = 0;
                d[64] = 0;
                d[65] = 0;
                d[66] = 0;
                d[67] = 0;
                d[68] = 0;
                d[69] = 2;
                d[70] = 6 * tpdata[2].abs;
                d[71] = 0;

                MatrixGauss.Gauss(8, d, r);

                if (abs > 0 && abs >= tpdata[0].abs && abs < tpdata[1].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;
                else if (abs < 0 && abs > tpdata[1].abs && abs <= tpdata[0].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;

                else if (abs > 0 && abs >= tpdata[1].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;
                else if (abs < 0 && abs <= tpdata[1].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;

                else result = -1;

                return (float)result;
            }
            else if (n == 4)
            {
                //1
                d[0] = 0;
                d[1] = 0;
                d[2] = 2;
                d[3] = 6 * tpdata[0].abs;
                d[4] = 0;
                d[5] = 0;
                d[6] = 0;
                d[7] = 0;
                d[8] = 0;
                d[9] = 0;
                d[10] = 0;
                d[11] = 0;
                d[12] = 0;
                //2
                d[13] = 1;
                d[14] = tpdata[0].abs;
                d[15] = tpdata[0].abs * tpdata[0].abs;
                d[16] = tpdata[0].abs * tpdata[0].abs * tpdata[0].abs;
                d[17] = 0;
                d[18] = 0;
                d[19] = 0;
                d[20] = 0;
                d[21] = 0;
                d[22] = 0;
                d[23] = 0;
                d[24] = 0;
                d[25] = tpdata[0].conc; ;
                //3
                d[26] = 1;
                d[27] = tpdata[1].abs;
                d[28] = tpdata[1].abs * tpdata[1].abs;
                d[29] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[30] = 0;
                d[31] = 0;
                d[32] = 0;
                d[33] = 0;
                d[34] = 0;
                d[35] = 0;
                d[36] = 0;
                d[37] = 0;
                d[38] = tpdata[1].conc;
                //4
                d[39] = 0;
                d[40] = 0;
                d[41] = 2;
                d[42] = 6 * tpdata[1].abs;
                d[43] = 0;
                d[44] = 0;
                d[45] = -2;
                d[46] = (-6) * tpdata[1].abs;
                d[47] = 0;
                d[48] = 0;
                d[49] = 0;
                d[50] = 0;
                d[51] = 0;
                //5
                d[52] = 0;
                d[53] = 1;
                d[54] = 2 * tpdata[1].abs;
                d[55] = 3 * tpdata[1].abs * tpdata[1].abs;
                d[56] = 0;
                d[57] = -1;
                d[58] = -2 * tpdata[1].abs;
                d[59] = -3 * tpdata[1].abs * tpdata[1].abs;
                d[60] = 0;
                d[61] = 0;
                d[62] = 0;
                d[63] = 0;
                d[64] = 0;
                //6
                d[65] = 0;
                d[66] = 0;
                d[67] = 0;
                d[68] = 0;
                d[69] = 1;
                d[70] = tpdata[1].abs;
                d[71] = tpdata[1].abs * tpdata[1].abs;
                d[72] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[73] = 0;
                d[74] = 0;
                d[75] = 0;
                d[76] = 0;
                d[77] = tpdata[1].conc;
                //7
                d[78] = 0;
                d[79] = 0;
                d[80] = 0;
                d[81] = 0;
                d[82] = 1;
                d[83] = tpdata[2].abs;
                d[84] = tpdata[2].abs * tpdata[2].abs;
                d[85] = tpdata[2].abs * tpdata[2].abs * tpdata[2].abs;
                d[86] = 0;
                d[87] = 0;
                d[88] = 0;
                d[89] = 0;
                d[90] = tpdata[2].conc;
                //8
                d[91] = 0;
                d[92] = 0;
                d[93] = 0;
                d[94] = 0;
                d[95] = 0;
                d[96] = 0;
                d[97] = 2;
                d[98] = 6 * tpdata[2].abs;
                d[99] = 0;
                d[100] = 0;
                d[101] = -2;
                d[102] = -6 * tpdata[2].abs;
                d[103] = 0;
                //9
                d[104] = 0;
                d[105] = 0;
                d[106] = 0;
                d[107] = 0;
                d[108] = 0;
                d[109] = 1;
                d[110] = 2 * tpdata[2].abs;
                d[111] = 3 * tpdata[2].abs * tpdata[2].abs;
                d[112] = 0;
                d[113] = -1;
                d[114] = -2 * tpdata[2].abs;
                d[115] = -3 * tpdata[2].abs * tpdata[2].abs;
                d[116] = 0;
                //10
                d[117] = 0;
                d[118] = 0;
                d[119] = 0;
                d[120] = 0;
                d[121] = 0;
                d[122] = 0;
                d[123] = 0;
                d[124] = 0;
                d[125] = 1;
                d[126] = tpdata[2].abs;
                d[127] = tpdata[2].abs * tpdata[2].abs;
                d[128] = tpdata[2].abs * tpdata[2].abs * tpdata[2].abs;
                d[129] = tpdata[2].conc;
                //11
                d[130] = 0;
                d[131] = 0;
                d[132] = 0;
                d[133] = 0;
                d[134] = 0;
                d[135] = 0;
                d[136] = 0;
                d[137] = 0;
                d[138] = 1;
                d[139] = tpdata[3].abs;
                d[140] = tpdata[3].abs * tpdata[3].abs;
                d[141] = tpdata[3].abs * tpdata[3].abs * tpdata[3].abs;
                d[142] = tpdata[3].conc;
                //12
                d[143] = 0;
                d[144] = 0;
                d[145] = 0;
                d[146] = 0;
                d[147] = 0;
                d[148] = 0;
                d[149] = 0;
                d[150] = 0;
                d[151] = 0;
                d[152] = 0;
                d[153] = 2;
                d[154] = 6 * tpdata[3].abs;
                d[155] = 0;

                MatrixGauss.Gauss(12, d, r);
                if (abs > 0 && abs >= tpdata[0].abs && abs < tpdata[1].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;
                else if (abs < 0 && abs > tpdata[1].abs && abs <= tpdata[0].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;

                else if (abs > 0 && abs >= tpdata[1].abs && abs < tpdata[2].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;
                else if (abs < 0 && abs > tpdata[2].abs && abs <= tpdata[1].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;

                else if (abs > 0 && abs >= tpdata[2].abs)
                    result = r[8] + r[9] * abs + r[10] * abs * abs + r[11] * abs * abs * abs;
                else if (abs < 0 && abs <= tpdata[2].abs)
                    result = r[8] + r[9] * abs + r[10] * abs * abs + r[11] * abs * abs * abs;
                else result = -1;
                return (float)result;
            }
            else if (n == 5)//此处处理由问题
            {
                //1
                d[0] = 0;
                d[1] = 0;
                d[2] = 2;
                d[3] = 6 * tpdata[0].abs;
                d[4] = 0;
                d[5] = 0;
                d[6] = 0;
                d[7] = 0;
                d[8] = 0;
                d[9] = 0;
                d[10] = 0;
                d[11] = 0;
                d[12] = 0;
                d[13] = 0;
                d[14] = 0;
                d[15] = 0;
                d[16] = 0;
                //2
                d[17] = 1;
                d[18] = tpdata[0].abs;
                d[19] = tpdata[0].abs * tpdata[0].abs;
                d[20] = tpdata[0].abs * tpdata[0].abs * tpdata[0].abs;
                d[21] = 0;
                d[22] = 0;
                d[23] = 0;
                d[24] = 0;
                d[25] = 0;
                d[26] = 0;
                d[27] = 0;
                d[28] = 0;
                d[29] = 0;
                d[30] = 0;
                d[31] = 0;
                d[32] = 0;
                d[33] = tpdata[0].conc;
                //3
                d[34] = 1;
                d[35] = tpdata[1].abs;
                d[36] = tpdata[1].abs * tpdata[1].abs;
                d[37] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[38] = 0;
                d[39] = 0;
                d[40] = 0;
                d[41] = 0;
                d[42] = 0;
                d[43] = 0;
                d[44] = 0;
                d[45] = 0;
                d[46] = 0;
                d[47] = 0;
                d[48] = 0;
                d[49] = 0;
                d[50] = tpdata[1].conc;
                //4
                d[51] = 0;
                d[52] = 0;
                d[53] = 2;
                d[54] = 6 * tpdata[1].abs;
                d[55] = 0;
                d[56] = 0;
                d[57] = -2;
                d[58] = -6 * tpdata[1].abs;
                d[59] = 0;
                d[60] = 0;
                d[61] = 0;
                d[62] = 0;
                d[63] = 0;
                d[64] = 0;
                d[65] = 0;
                d[66] = 0;
                d[67] = 0;
                //5
                d[68] = 0;
                d[69] = 1;
                d[70] = 2 * tpdata[1].abs;
                d[71] = 3 * tpdata[1].abs * tpdata[1].abs;
                d[72] = 0;
                d[73] = -1;
                d[74] = -2 * tpdata[1].abs;
                d[75] = -3 * tpdata[1].abs * tpdata[1].abs;
                d[76] = 0;
                d[77] = 0;
                d[78] = 0;
                d[79] = 0;
                d[80] = 0;
                d[81] = 0;
                d[82] = 0;
                d[83] = 0;
                d[84] = 0;
                //6
                d[85] = 0;
                d[86] = 0;
                d[87] = 0;
                d[88] = 0;
                d[89] = 1;
                d[90] = tpdata[1].abs;
                d[91] = tpdata[1].abs * tpdata[1].abs;
                d[92] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[93] = 0;
                d[94] = 0;
                d[95] = 0;
                d[96] = 0;
                d[97] = 0;
                d[98] = 0;
                d[99] = 0;
                d[100] = 0;
                d[101] = tpdata[1].conc;
                //7
                d[102] = 0;
                d[103] = 0;
                d[104] = 0;
                d[105] = 0;
                d[106] = 1;
                d[107] = tpdata[2].abs;
                d[108] = tpdata[2].abs * tpdata[2].abs;
                d[109] = tpdata[2].abs * tpdata[2].abs * tpdata[2].abs;
                d[110] = 0;
                d[111] = 0;
                d[112] = 0;
                d[113] = 0;
                d[114] = 0;
                d[115] = 0;
                d[116] = 0;
                d[117] = 0;
                d[118] = tpdata[2].conc;
                //8
                d[119] = 0;
                d[120] = 0;
                d[121] = 0;
                d[122] = 0;
                d[123] = 0;
                d[124] = 0;
                d[125] = 2;
                d[126] = 6 * tpdata[2].abs;
                d[127] = 0;
                d[128] = 0;
                d[129] = -2;
                d[130] = -6 * tpdata[2].abs;
                d[131] = 0;
                d[132] = 0;
                d[133] = 0;
                d[134] = 0;
                d[135] = 0;
                //9
                d[136] = 0;
                d[137] = 0;
                d[138] = 0;
                d[139] = 0;
                d[140] = 0;
                d[141] = 1;
                d[142] = 2 * tpdata[2].abs;
                d[143] = 3 * tpdata[2].abs * tpdata[2].abs;
                d[144] = 0;
                d[145] = -1;
                d[146] = -2 * tpdata[2].abs;
                d[147] = -3 * tpdata[2].abs * tpdata[2].abs;
                d[148] = 0;
                d[149] = 0;
                d[150] = 0;
                d[151] = 0;
                d[152] = 0;
                //10
                d[153] = 0;
                d[154] = 0;
                d[155] = 0;
                d[156] = 0;
                d[157] = 0;
                d[158] = 0;
                d[159] = 0;
                d[160] = 0;
                d[161] = 1;
                d[162] = tpdata[2].abs;
                d[163] = tpdata[2].abs * tpdata[2].abs;
                d[164] = tpdata[2].abs * tpdata[2].abs * tpdata[2].abs;
                d[165] = 0;
                d[166] = 0;
                d[167] = 0;
                d[168] = 0;
                d[169] = tpdata[2].conc;
                //11
                d[170] = 0;
                d[171] = 0;
                d[172] = 0;
                d[173] = 0;
                d[174] = 0;
                d[175] = 0;
                d[176] = 0;
                d[177] = 0;
                d[178] = 1;
                d[179] = tpdata[3].abs;
                d[180] = tpdata[3].abs * tpdata[3].abs;
                d[181] = tpdata[3].abs * tpdata[3].abs * tpdata[3].abs;
                d[182] = 0;
                d[183] = 0;
                d[184] = 0;
                d[185] = 0;
                d[186] = tpdata[3].conc;
                //12
                d[187] = 0;
                d[188] = 0;//111
                d[189] = 0;
                d[190] = 0;
                d[191] = 0;
                d[192] = 0;
                d[193] = 0;
                d[194] = 0;
                d[195] = 0;
                d[196] = 0;
                d[197] = 2;
                d[198] = 6 * tpdata[3].abs;
                d[199] = 0;
                d[200] = 0;
                d[201] = -2;
                d[202] = -6 * tpdata[3].abs;
                d[203] = 0;
                //13
                d[204] = 0;
                d[205] = 0;
                d[206] = 0;
                d[207] = 0;
                d[208] = 0;
                d[209] = 0;
                d[210] = 0;
                d[211] = 0;
                d[212] = 0;
                d[213] = 1;
                d[214] = 2 * tpdata[3].abs;
                d[215] = 3 * tpdata[3].abs * tpdata[3].abs;
                d[216] = 0;
                d[217] = -1;
                d[218] = -2 * tpdata[3].abs;
                d[219] = -3 * tpdata[3].abs * tpdata[3].abs;
                d[220] = 0;
                //14
                d[221] = 0;
                d[222] = 0;
                d[223] = 0;
                d[224] = 0;
                d[225] = 0;
                d[226] = 0;
                d[227] = 0;
                d[228] = 0;
                d[229] = 0;
                d[230] = 0;
                d[231] = 0;
                d[232] = 0;
                d[233] = 1;
                d[234] = tpdata[3].abs;
                d[235] = tpdata[3].abs * tpdata[3].abs;
                d[236] = tpdata[3].abs * tpdata[3].abs * tpdata[3].abs;
                d[237] = tpdata[3].conc;
                //15
                d[238] = 0;
                d[239] = 0;
                d[240] = 0;
                d[241] = 0;
                d[242] = 0;
                d[243] = 0;
                d[244] = 0;
                d[245] = 0;
                d[246] = 0;
                d[247] = 0;
                d[248] = 0;
                d[249] = 0;
                d[250] = 1;
                d[251] = tpdata[4].abs;
                d[252] = tpdata[4].abs * tpdata[4].abs;
                d[253] = tpdata[4].abs * tpdata[4].abs * tpdata[4].abs;
                d[254] = tpdata[4].conc;
                //16
                d[255] = 0;
                d[256] = 0;
                d[257] = 0;
                d[258] = 0;
                d[259] = 0;
                d[260] = 0;
                d[261] = 0;
                d[262] = 0;
                d[263] = 0;
                d[264] = 0;
                d[265] = 0;
                d[266] = 0;
                d[267] = 0;
                d[268] = 0;
                d[269] = 2;
                d[270] = 6 * tpdata[4].abs;
                d[271] = 0;

                MatrixGauss.Gauss(16, d, r);
                if (abs > 0 && abs >= tpdata[0].abs && abs < tpdata[1].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;
                else if (abs < 0 && abs > tpdata[1].abs && abs <= tpdata[0].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;

                else if (abs > 0 && abs >= tpdata[1].abs && abs < tpdata[2].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;
                else if (abs < 0 && abs > tpdata[2].abs && abs <= tpdata[1].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;

                else if (abs > 0 && abs >= tpdata[2].abs && abs < tpdata[3].abs)
                    result = r[8] + r[9] * abs + r[10] * abs * abs + r[11] * abs * abs * abs;
                else if (abs < 0 && abs > tpdata[3].abs && abs <= tpdata[2].abs)
                    result = r[8] + r[9] * abs + r[10] * abs * abs + r[11] * abs * abs * abs;

                else if (abs > 0 && abs >= tpdata[3].abs)
                    result = r[12] + r[13] * abs + r[14] * abs * abs + r[15] * abs * abs * abs;
                else if (abs < 0 && abs <= tpdata[3].abs)
                    result = r[12] + r[13] * abs + r[14] * abs * abs + r[15] * abs * abs * abs;

                else result = -1;
                return (float)result;
            }
            else if (n == 6)
            {
                //1
                d[0] = 0;
                d[1] = 0;
                d[2] = 2;
                d[3] = 6 * tpdata[0].abs;
                d[4] = 0;
                d[5] = 0;
                d[6] = 0;
                d[7] = 0;
                d[8] = 0;
                d[9] = 0;
                d[10] = 0;
                d[11] = 0;
                d[12] = 0;
                d[13] = 0;
                d[14] = 0;
                d[15] = 0;
                d[16] = 0;
                d[17] = 0;
                d[18] = 0;
                d[19] = 0;
                d[20] = 0;
                //2
                d[21] = 1;
                d[22] = tpdata[0].abs;
                d[23] = tpdata[0].abs * tpdata[0].abs;
                d[24] = tpdata[0].abs * tpdata[0].abs * tpdata[0].abs;
                d[25] = 0;
                d[26] = 0;
                d[27] = 0;
                d[28] = 0;
                d[29] = 0;
                d[30] = 0;
                d[31] = 0;
                d[32] = 0;
                d[33] = 0;
                d[34] = 0;
                d[35] = 0;
                d[36] = 0;
                d[37] = 0;
                d[38] = 0;
                d[39] = 0;
                d[40] = 0;
                d[41] = tpdata[0].conc;
                //3
                d[42] = 1;
                d[43] = tpdata[1].abs;
                d[44] = tpdata[1].abs * tpdata[1].abs;
                d[45] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[46] = 0;
                d[47] = 0;
                d[48] = 0;
                d[49] = 0;
                d[50] = 0;
                d[51] = 0;
                d[52] = 0;
                d[53] = 0;
                d[54] = 0;
                d[55] = 0;
                d[56] = 0;
                d[57] = 0;
                d[58] = 0;
                d[59] = 0;
                d[60] = 0;
                d[61] = 0;
                d[62] = tpdata[1].conc;
                //4
                d[63] = 0;
                d[64] = 0;
                d[65] = 2;
                d[66] = 6 * tpdata[1].abs;
                d[67] = 0;
                d[68] = 0;
                d[69] = -2;
                d[70] = -6 * tpdata[1].abs;
                d[71] = 0;
                d[72] = 0;
                d[73] = 0;
                d[74] = 0;
                d[75] = 0;
                d[76] = 0;
                d[77] = 0;
                d[78] = 0;
                d[79] = 0;
                d[80] = 0;
                d[81] = 0;
                d[82] = 0;
                d[83] = 0;
                //5
                d[84] = 0;
                d[85] = 1;
                d[86] = 2 * tpdata[1].abs;
                d[87] = 3 * tpdata[1].abs * tpdata[1].abs;
                d[88] = 0;
                d[89] = -1;
                d[90] = -2 * tpdata[1].abs;
                d[91] = -3 * tpdata[1].abs * tpdata[1].abs;
                d[92] = 0;
                d[93] = 0;
                d[94] = 0;
                d[95] = 0;
                d[96] = 0;
                d[97] = 0;
                d[98] = 0;
                d[99] = 0;
                d[100] = 0;
                d[101] = 0;
                d[102] = 0;
                d[103] = 0;
                d[104] = 0;
                //6
                d[105] = 0;
                d[106] = 0;
                d[107] = 0;
                d[108] = 0;
                d[109] = 1;
                d[110] = tpdata[1].abs;
                d[111] = tpdata[1].abs * tpdata[1].abs;
                d[112] = tpdata[1].abs * tpdata[1].abs * tpdata[1].abs;
                d[113] = 0;
                d[114] = 0;
                d[115] = 0;
                d[116] = 0;
                d[117] = 0;
                d[118] = 0;
                d[119] = 0;
                d[120] = 0;
                d[121] = 0;
                d[122] = 0;
                d[123] = 0;
                d[124] = 0;
                d[125] = tpdata[1].conc;
                //7
                d[126] = 0;
                d[127] = 0;
                d[128] = 0;
                d[129] = 0;
                d[130] = 1;
                d[131] = tpdata[2].abs;
                d[132] = tpdata[2].abs * tpdata[2].abs;
                d[133] = tpdata[2].abs * tpdata[2].abs * tpdata[2].abs;
                d[134] = 0;
                d[135] = 0;
                d[136] = 0;
                d[137] = 0;
                d[138] = 0;
                d[139] = 0;
                d[140] = 0;
                d[141] = 0;
                d[142] = 0;
                d[143] = 0;
                d[144] = 0;
                d[145] = 0;
                d[146] = tpdata[2].conc;
                //8
                d[147] = 0;
                d[148] = 0;
                d[149] = 0;
                d[150] = 0;
                d[151] = 0;
                d[152] = 0;
                d[153] = 2;
                d[154] = 6 * tpdata[2].abs;
                d[155] = 0;
                d[156] = 0;
                d[157] = -2;
                d[158] = -6 * tpdata[2].abs;
                d[159] = 0;
                d[160] = 0;
                d[161] = 0;
                d[162] = 0;
                d[163] = 0;
                d[164] = 0;
                d[165] = 0;
                d[166] = 0;
                d[167] = 0;
                //9
                d[168] = 0;
                d[169] = 0;
                d[170] = 0;
                d[171] = 0;
                d[172] = 0;
                d[173] = 1;
                d[174] = 2 * tpdata[2].abs;
                d[175] = 3 * tpdata[2].abs * tpdata[2].abs;
                d[176] = 0;
                d[177] = -1;
                d[178] = -2 * tpdata[2].abs;
                d[179] = -3 * tpdata[2].abs * tpdata[2].abs;
                d[180] = 0;
                d[181] = 0;
                d[182] = 0;
                d[183] = 0;
                d[184] = 0;
                d[185] = 0;
                d[186] = 0;
                d[187] = 0;
                d[188] = 0;
                //10
                d[189] = 0;
                d[190] = 0;
                d[191] = 0;
                d[192] = 0;
                d[193] = 0;
                d[194] = 0;
                d[195] = 0;
                d[196] = 0;
                d[197] = 1;
                d[198] = tpdata[2].abs;
                d[199] = tpdata[2].abs * tpdata[2].abs;
                d[200] = tpdata[2].abs * tpdata[2].abs * tpdata[2].abs;
                d[201] = 0;
                d[202] = 0;
                d[203] = 0;
                d[204] = 0;
                d[205] = 0;
                d[206] = 0;
                d[207] = 0;
                d[208] = 0;
                d[209] = tpdata[2].conc;
                //11
                d[210] = 0;
                d[211] = 0;
                d[212] = 0;
                d[213] = 0;
                d[214] = 0;
                d[215] = 0;
                d[216] = 0;
                d[217] = 0;
                d[218] = 1;
                d[219] = tpdata[3].abs;
                d[220] = tpdata[3].abs * tpdata[3].abs;
                d[221] = tpdata[3].abs * tpdata[3].abs * tpdata[3].abs;
                d[222] = 0;
                d[223] = 0;
                d[224] = 0;
                d[225] = 0;
                d[226] = 0;
                d[227] = 0;
                d[228] = 0;
                d[229] = 0;
                d[230] = tpdata[3].conc;
                //12
                d[231] = 0;
                d[232] = 0;
                d[233] = 0;
                d[234] = 0;
                d[235] = 0;
                d[236] = 0;
                d[237] = 0;
                d[238] = 0;
                d[239] = 0;
                d[240] = 0;
                d[241] = 2;
                d[242] = 6 * tpdata[3].abs;
                d[243] = 0;
                d[244] = 0;
                d[245] = -2;
                d[246] = -6 * tpdata[3].abs;
                d[247] = 0;
                d[248] = 0;
                d[249] = 0;
                d[250] = 0;
                d[251] = 0;
                //13
                d[252] = 0;
                d[253] = 0;
                d[254] = 0;
                d[255] = 0;
                d[256] = 0;
                d[257] = 0;
                d[258] = 0;
                d[259] = 0;
                d[260] = 0;
                d[261] = 1;
                d[262] = 2 * tpdata[3].abs;
                d[263] = 3 * tpdata[3].abs * tpdata[3].abs;
                d[264] = 0;
                d[265] = -1;
                d[266] = -2 * tpdata[3].abs;
                d[267] = -3 * tpdata[3].abs * tpdata[3].abs;
                d[268] = 0;
                d[269] = 0;
                d[270] = 0;
                d[271] = 0;
                d[272] = 0;
                //14
                d[273] = 0;
                d[274] = 0;
                d[275] = 0;
                d[276] = 0;
                d[277] = 0;
                d[278] = 0;
                d[279] = 0;
                d[280] = 0;
                d[281] = 0;
                d[282] = 0;
                d[283] = 0;
                d[284] = 0;
                d[285] = 1;
                d[286] = tpdata[3].abs;
                d[287] = tpdata[3].abs * tpdata[3].abs;
                d[288] = tpdata[3].abs * tpdata[3].abs * tpdata[3].abs;
                d[289] = 0;
                d[290] = 0;
                d[291] = 0;
                d[292] = 0;
                d[293] = tpdata[3].conc;
                //15
                d[294] = 0;
                d[295] = 0;
                d[296] = 0;
                d[297] = 0;
                d[298] = 0;
                d[299] = 0;
                d[300] = 0;
                d[301] = 0;
                d[302] = 0;
                d[303] = 0;
                d[304] = 0;
                d[305] = 0;
                d[306] = 1;
                d[307] = tpdata[4].abs;
                d[308] = tpdata[4].abs * tpdata[4].abs;
                d[309] = tpdata[4].abs * tpdata[4].abs * tpdata[4].abs;
                d[310] = 0;
                d[311] = 0;
                d[312] = 0;
                d[313] = 0;
                d[314] = tpdata[4].conc;
                //16
                d[315] = 0;
                d[316] = 0;
                d[317] = 0;
                d[318] = 0;
                d[319] = 0;
                d[320] = 0;
                d[321] = 0;
                d[322] = 0;
                d[323] = 0;
                d[324] = 0;
                d[325] = 0;
                d[326] = 0;
                d[327] = 0;
                d[328] = 0;
                d[329] = 2;
                d[330] = 6 * tpdata[4].abs;
                d[331] = 0;
                d[332] = 0;
                d[333] = -2;
                d[334] = -6 * tpdata[4].abs;
                d[335] = 0;
                //17
                d[336] = 0;
                d[337] = 0;
                d[338] = 0;
                d[339] = 0;
                d[340] = 0;
                d[341] = 0;
                d[342] = 0;
                d[343] = 0;
                d[344] = 0;
                d[345] = 0;
                d[346] = 0;
                d[347] = 0;
                d[348] = 0;
                d[349] = 1;
                d[350] = 2 * tpdata[4].abs;
                d[351] = 3 * tpdata[4].abs * tpdata[4].abs;
                d[352] = 0;
                d[353] = -1;
                d[354] = -2 * tpdata[4].abs;
                d[355] = -3 * tpdata[4].abs * tpdata[4].abs;
                d[356] = 0;
                //18
                d[357] = 0;
                d[358] = 0;
                d[359] = 0;
                d[360] = 0;
                d[361] = 0;
                d[362] = 0;
                d[363] = 0;
                d[364] = 0;
                d[365] = 0;
                d[366] = 0;
                d[367] = 0;
                d[368] = 0;
                d[369] = 0;
                d[370] = 0;
                d[371] = 0;
                d[372] = 0;
                d[373] = 1;
                d[374] = tpdata[4].abs;
                d[375] = tpdata[4].abs * tpdata[4].abs;
                d[376] = tpdata[4].abs * tpdata[4].abs * tpdata[4].abs;
                d[377] = tpdata[4].conc;
                //19
                d[378] = 0;
                d[379] = 0;
                d[380] = 0;
                d[381] = 0;
                d[382] = 0;
                d[383] = 0;
                d[384] = 0;
                d[385] = 0;
                d[386] = 0;
                d[387] = 0;
                d[388] = 0;
                d[389] = 0;
                d[390] = 0;
                d[391] = 0;
                d[392] = 0;
                d[393] = 0;
                d[394] = 1;
                d[395] = tpdata[5].abs;
                d[396] = tpdata[5].abs * tpdata[5].abs;
                d[397] = tpdata[5].abs * tpdata[5].abs * tpdata[5].abs;
                d[398] = tpdata[5].conc;
                //20
                d[399] = 0;
                d[400] = 0;
                d[401] = 0;
                d[402] = 0;
                d[403] = 0;
                d[404] = 0;
                d[405] = 0;
                d[406] = 0;
                d[407] = 0;
                d[408] = 0;
                d[409] = 0;
                d[410] = 0;
                d[411] = 0;
                d[412] = 0;
                d[413] = 0;
                d[414] = 0;
                d[415] = 0;
                d[416] = 0;
                d[417] = 2;
                d[418] = 6 * tpdata[5].abs;
                d[419] = 0;

                MatrixGauss.Gauss(20, d, r);
                if (abs >= tpdata[0].abs && abs < tpdata[1].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;
                else if (abs > tpdata[1].abs && abs <= tpdata[0].abs)
                    result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;

                else if (abs >= tpdata[1].abs && abs < tpdata[2].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;
                else if (abs > tpdata[2].abs && abs <= tpdata[1].abs)
                    result = r[4] + r[5] * abs + r[6] * abs * abs + r[7] * abs * abs * abs;

                else if (abs >= tpdata[2].abs && abs < tpdata[3].abs)
                    result = r[8] + r[9] * abs + r[10] * abs * abs + r[11] * abs * abs * abs;
                else if (abs > tpdata[3].abs && abs <= tpdata[2].abs)
                    result = r[8] + r[9] * abs + r[10] * abs * abs + r[11] * abs * abs * abs;

                else if (abs >= tpdata[3].abs && abs < tpdata[4].abs)
                    result = r[12] + r[13] * abs + r[14] * abs * abs + r[15] * abs * abs * abs;
                else if (abs > tpdata[4].abs && abs <= tpdata[3].abs)
                    result = r[12] + r[13] * abs + r[14] * abs * abs + r[15] * abs * abs * abs;

                else if (abs >= tpdata[4].abs && abs < tpdata[5].abs)
                    result = r[16] + r[17] * abs + r[18] * abs * abs + r[19] * abs * abs * abs;
                else if (abs > tpdata[5].abs)
                    result = -1;
                else result = -1;

                return (float)result;
            }

            return 0;
        }
        public static float ByPoly1(float abs)
        {
            return ByMultiPTLine(abs);
        }
        public static float ByPoly2(float abs)
        {
            TABLE[] tpdata = new TABLE[6];
            for (int i = 0; i < 6; i++)
            {
                tpdata[i] = new TABLE();
                tpdata[i].conc = 0;
                tpdata[i].abs = 0;
            }

            int j;
            if (m_data[0].abs != 0)
            {
                tpdata[0].conc = 0;
                tpdata[0].abs = m_data[0].abs;
                j = 0;
                for (int i = 1; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }
            else
            {
                j = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }

            int n = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0 && tpdata[i].abs != 0)
                    n++;
                if (tpdata[i].abs != 0 && tpdata[i].conc != 0)
                    n++;
                else break;
            }

            if (n < 3) return 0;
            double all1a = 0; double all2a = 0; double all3a = 0; double all4a = 0; double allc = 0; double all1ac = 0; double all2ac = 0;
            for (int i = 0; i < n; i++)
            {
                all1a += tpdata[i].abs;
                all2a += tpdata[i].abs * tpdata[i].abs;
                all3a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all4a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                allc += tpdata[i].conc;
                all1ac += tpdata[i].conc * tpdata[i].abs;
                all2ac += tpdata[i].conc * tpdata[i].abs * tpdata[i].abs;
            }
            double[] d = new double[12];
            for (int i = 0; i < 12; i++)
                d[i] = 0;
            double[] r = new double[3];
            for (int i = 0; i < 3; i++)
                r[i] = 0;

            d[0] = n;
            d[1] = all1a;
            d[2] = all2a;
            d[3] = allc;
            d[4] = all1a;
            d[5] = all2a;
            d[6] = all3a;
            d[7] = all1ac;
            d[8] = all2a;
            d[9] = all3a;
            d[10] = all4a;
            d[11] = all2ac;

            MatrixGauss.Gauss(3, d, r);
            double result = 0;
            result = r[0] + r[1] * abs + r[2] * abs * abs;

            return (float)result;
        }
        public static float ByPoly3(float abs)
        {
            TABLE[] tpdata = new TABLE[6];
            for (int i = 0; i < 6; i++)
            {
                tpdata[i] = new TABLE();
                tpdata[i].conc = 0;
                tpdata[i].abs = 0;
            }

            int j;
            if (m_data[0].abs != 0)
            {
                tpdata[0].conc = 0;
                tpdata[0].abs = m_data[0].abs;
                j = 0;
                for (int i = 1; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }
            else
            {
                j = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }

            int n = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0 && tpdata[i].abs != 0)
                    n++;
                if (tpdata[i].abs != 0 && tpdata[i].conc != 0)
                    n++;
                else break;
            }

            if (n < 3) return 0;
            double all1a = 0; double all2a = 0; double all3a = 0; double all4a = 0; double all5a = 0; double all6a = 0; double allc = 0; double all1ac = 0; double all2ac = 0; double all3ac = 0;
            for (int i = 0; i < n; i++)
            {
                all1a += tpdata[i].abs;
                all2a += tpdata[i].abs * tpdata[i].abs;
                all3a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all4a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all5a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all6a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                allc += tpdata[i].conc;
                all1ac += tpdata[i].conc * tpdata[i].abs;
                all2ac += tpdata[i].conc * tpdata[i].abs * tpdata[i].abs;
                all3ac += tpdata[i].conc * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
            }
            double[] d = new double[20];
            for (int i = 0; i < 20; i++)
                d[i] = 0;
            double[] r = new double[4];
            for (int i = 0; i < 4; i++)
                r[i] = 0;

            d[0] = n;
            d[1] = all1a;
            d[2] = all2a;
            d[3] = all3a;
            d[4] = allc;

            d[5] = all1a;
            d[6] = all2a;
            d[7] = all3a;
            d[8] = all4a;
            d[9] = all1ac;

            d[10] = all2a;
            d[11] = all3a;
            d[12] = all4a;
            d[13] = all5a;
            d[14] = all2ac;

            d[15] = all3a;
            d[16] = all4a;
            d[17] = all5a;
            d[18] = all6a;
            d[19] = all3ac;

            MatrixGauss.Gauss(4, d, r);
            double result = 0;
            result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs;

            return (float)result;
        }
        public static float ByPoly4(float abs)
        {
            TABLE[] tpdata = new TABLE[6];
            for (int i = 0; i < 6; i++)
            {
                tpdata[i] = new TABLE();
                tpdata[i].conc = 0;
                tpdata[i].abs = 0;
            }

            int j;
            if (m_data[0].abs != 0)
            {
                tpdata[0].conc = 0;
                tpdata[0].abs = m_data[0].abs;
                j = 0;
                for (int i = 1; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }
            else
            {
                j = 0;
                for (int i = 0; i < 6; i++)
                {
                    if (m_data[i].abs != 0 && m_data[i].conc != 0)
                    {
                        tpdata[j].conc = m_data[i].conc;
                        tpdata[j].abs = m_data[i].abs;
                        j++;
                    }
                }
            }

            int n = 0;
            for (int i = 0; i < 6; i++)
            {
                if (i == 0 && tpdata[i].abs != 0)
                    n++;
                if (tpdata[i].abs != 0 && tpdata[i].conc != 0)
                    n++;
                else break;
            }

            if (n < 3) return 0;
            double all1a = 0; double all2a = 0; double all3a = 0; double all4a = 0; double all5a = 0; double all6a = 0; double all7a = 0; double all8a = 0; double allc = 0; double all1ac = 0; double all2ac = 0; double all3ac = 0; double all4ac = 0;
            for (int i = 0; i < n; i++)
            {
                all1a += tpdata[i].abs;
                all2a += tpdata[i].abs * tpdata[i].abs;
                all3a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all4a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all5a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all6a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all7a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all8a += tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                allc += tpdata[i].conc;
                all1ac += tpdata[i].conc * tpdata[i].abs;
                all2ac += tpdata[i].conc * tpdata[i].abs * tpdata[i].abs;
                all3ac += tpdata[i].conc * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
                all4ac += tpdata[i].conc * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs * tpdata[i].abs;
            }

            double[] d = new double[30];
            for (int i = 0; i < 30; i++)
                d[i] = 0;
            double[] r = new double[5];
            for (int i = 0; i < 5; i++)
                r[i] = 0;

            d[0] = n;
            d[1] = all1a;
            d[2] = all2a;
            d[3] = all3a;
            d[4] = all4a;
            d[5] = allc;

            d[6] = all1a;
            d[7] = all2a;
            d[8] = all3a;
            d[9] = all4a;
            d[10] = all5a;
            d[11] = all1ac;

            d[12] = all2a;
            d[13] = all3a;
            d[14] = all4a;
            d[15] = all5a;
            d[16] = all6a;
            d[17] = all2ac;

            d[18] = all3a;
            d[19] = all4a;
            d[20] = all5a;
            d[21] = all6a;
            d[22] = all7a;
            d[23] = all3ac;

            d[24] = all4a;
            d[25] = all5a;
            d[26] = all6a;
            d[27] = all7a;
            d[28] = all8a;
            d[29] = all4ac;

            MatrixGauss.Gauss(5, d, r);

            double result = 0;
            result = r[0] + r[1] * abs + r[2] * abs * abs + r[3] * abs * abs * abs + r[4] * abs * abs * abs * abs;
            return (float)result;
        }
    }
}
