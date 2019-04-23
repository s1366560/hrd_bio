using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioA.UI
{
    public partial class CuvettePanel : Form
    {
        bool States = false;

        public CuvettePanel()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow; //状态栏没有
            this.MaximizeBox = false;                   //最大化按钮
            this.MinimizeBox = false;
            this.Text = "样本盘状态";
            this.StartPosition = FormStartPosition.CenterParent;
            States = true;
        }
        private void init()
        {
            Graphics g = this.CreateGraphics();

            //画一个椭圆
            Pen p = new Pen(Color.FromArgb(192, 192, 192), 2);
            Pen pw = new Pen(Color.FromArgb(192, 192, 192), 1);
            Pen pr = new Pen(Color.Red, 2);

            g.DrawEllipse(p, 130, 50, 800, 800);//

            g.DrawEllipse(p, 133, 53, 794, 794);

            g.DrawEllipse(p, 150, 72, 760, 760);

            g.DrawEllipse(p, 153, 75, 753, 753);
            g.ResetTransform();

            //先平移到指定坐标,然后进行度旋转
            g.TranslateTransform(525, 455);
            for (int i = 0; i <= 160; i++)
            {
                g.RotateTransform((float)2.25);


                if (i == 0)
                // g.DrawEllipse(pr, 220, 74, 12, 12);
                {
                    //g.FillEllipse(Brushes.Yellow, 364,123, 12,12);
                    g.FillEllipse(Brushes.Yellow, 358, 137, 12, 12);
                    g.DrawLine(pw, 385, 0, 401, 0);
                }
                else
                    if (i == 1)
                    {
                        g.FillEllipse(Brushes.Green, 358, 137, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 2)
                    {
                        g.FillEllipse(Brushes.Black, 358, 137, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 3)
                    {
                        g.FillEllipse(Brushes.Black, 358, 137, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 4)
                    {
                        g.FillEllipse(Brushes.Red, 358, 137, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 5)
                    {
                        g.FillEllipse(Brushes.Black, 358, 137, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 6)
                    {
                        //g.FillEllipse(Brushes.Black, 363, 122, 12, 12);
                        g.FillEllipse(Brushes.Black, 357, 136, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 7)
                    {
                        g.FillEllipse(Brushes.Red, 357, 136, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 8)
                    {
                        g.FillEllipse(Brushes.Blue, 357, 136, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 9)
                    {
                        //g.FillEllipse(Brushes.Green, 363, 121, 12, 12);
                        g.FillEllipse(Brushes.Green, 357, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 10)
                    {
                        g.FillEllipse(Brushes.Black, 357, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 11)
                    {
                        g.FillEllipse(Brushes.Black, 357, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 12)
                    {
                        //g.FillEllipse(Brushes.Black, 361, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 355, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 13)
                    {
                        g.FillEllipse(Brushes.Black, 355, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 14)
                    {
                        g.FillEllipse(Brushes.Black, 355, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 15)
                    {
                        g.FillEllipse(Brushes.Red, 355, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 16)
                    {
                        g.FillEllipse(Brushes.Black, 355, 135, 12, 12);
                        g.DrawLine(pw, 385, 0, 401, 0);
                    }
                    else if (i == 17)
                    {
                        //g.FillEllipse(Brushes.Black, 360, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 383, 0, 401, 0);
                    }
                    else if (i == 18)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 383, 0, 401, 0);
                    }
                    else if (i == 19)
                    {
                        //g.FillEllipse(Brushes.Black, 359, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 353, 135, 12, 12);
                        g.DrawLine(pw, 382, 0, 401, 0);
                    }
                    else if (i == 20)
                    {
                        g.FillEllipse(Brushes.Black, 353, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 401, 0);
                    }
                    else if (i == 21)
                    {
                        g.FillEllipse(Brushes.Black, 353, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 401, 0);
                    }
                    else if (i == 22)
                    {
                        g.FillEllipse(Brushes.Black, 353, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 400, 0);
                    }
                    else if (i == 23)
                    {
                        g.FillEllipse(Brushes.Black, 353, 135, 12, 12);

                        g.DrawLine(pw, 381, 0, 400, 0);
                    }
                    else if (i == 24)
                    {
                        //g.FillEllipse(Brushes.Black, 358, 121, 12, 12);
                        g.FillEllipse(Brushes.Red, 352, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 400, 0);
                    }
                    else if (i == 25)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 399, 0);
                    }
                    else if (i == 26)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 399, 0);

                    }
                    else if (i == 27)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 399, 0);
                    }
                    else if (i == 28)
                    {
                        g.FillEllipse(Brushes.SaddleBrown, 352, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 399, 0);
                    }
                    else if (i == 29)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 381, 0, 399, 0);
                    }
                    else if (i == 30)
                    {
                        //g.FillEllipse(Brushes.Black, 356, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 350, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 31)
                    {
                        g.FillEllipse(Brushes.Black, 350, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 32)
                    {
                        g.FillEllipse(Brushes.Black, 350, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 33)
                    {
                        g.FillEllipse(Brushes.Black, 350, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 34)
                    {
                        //g.FillEllipse(Brushes.Black, 355, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 349, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 35)
                    {
                        g.FillEllipse(Brushes.Black, 349, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 36)
                    {
                        g.FillEllipse(Brushes.Black, 349, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 37)
                    {
                        g.FillEllipse(Brushes.Black, 349, 135, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 38)
                    {
                        //g.FillEllipse(Brushes.Black, 355, 119, 12, 12);
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 379, 0, 397, 0);
                    }
                    else if (i == 39)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 40)
                    {
                        g.FillEllipse(Brushes.Tomato, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 41)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 42)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 43)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 44)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 45)
                    {
                        g.FillEllipse(Brushes.Red, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 46)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 47)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 48)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 49)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 50)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 51)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 52)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 53)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 54)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 55)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 56)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 57)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 374, 0, 393, 0);
                    }
                    else if (i == 58)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 373, 0, 393, 0);
                    }
                    else if (i == 59)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 373, 0, 393, 0);
                    }
                    else if (i == 60)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 373, 0, 393, 0);
                    }
                    else if (i == 61)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 373, 0, 393, 0);
                    }
                    else if (i == 62)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 373, 0, 393, 0);
                    }
                    else if (i == 63)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 64)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 65)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 66)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 67)
                    {
                        g.FillEllipse(Brushes.Black, 349, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 68)
                    {
                        //g.FillEllipse(Brushes.SlateGray, 356, 119, 12, 12);
                        g.FillEllipse(Brushes.SlateGray, 350, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 69)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 70)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 71)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 72)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 377, 0, 395, 0);
                    }
                    else if (i == 73)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 74)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 75)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 76)
                    {
                        g.FillEllipse(Brushes.Black, 350, 133, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 77)
                    {
                        //g.FillEllipse(Brushes.Black, 358, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 78)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 79)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 80)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 81)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 82)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 83)
                    {
                        g.FillEllipse(Brushes.Black, 352, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 84)
                    {
                        //g.FillEllipse(Brushes.Red, 359, 121, 12, 12);
                        g.FillEllipse(Brushes.Red, 353, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 85)
                    {
                        //g.FillEllipse(Brushes.Black, 360, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 86)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 87)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 88)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 89)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 90)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 91)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 92)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 375, 0, 393, 0);
                    }
                    else if (i == 93)
                    {
                        g.FillEllipse(Brushes.Green, 354, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 393, 0);
                    }
                    else if (i == 94)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 95)
                    {
                        g.FillEllipse(Brushes.Black, 354, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 96)
                    {
                        //g.FillEllipse(Brushes.Black, 362, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 356, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 97)
                    {
                        g.FillEllipse(Brushes.Black, 356, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 98)
                    {
                        g.FillEllipse(Brushes.Black, 356, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 99)
                    {
                        //g.FillEllipse(Brushes.Black, 363, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 357, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 100)
                    {
                        g.FillEllipse(Brushes.Black, 357, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 101)
                    {
                        g.FillEllipse(Brushes.Black, 357, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 102)
                    {
                        g.FillEllipse(Brushes.Red, 357, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 103)
                    {
                        g.FillEllipse(Brushes.Black, 357, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 104)
                    {
                        //g.FillEllipse(Brushes.Black, 364, 121, 12, 12);
                        g.FillEllipse(Brushes.Black, 358, 135, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 105)
                    {
                        //g.FillEllipse(Brushes.Black, 364, 122, 12, 12);
                        g.FillEllipse(Brushes.Black, 358, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 106)
                    {
                        //g.FillEllipse(Brushes.Black, 365, 122, 12, 12);
                        g.FillEllipse(Brushes.Black, 359, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 107)
                    {
                        g.FillEllipse(Brushes.Black, 359, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 108)
                    {
                        g.FillEllipse(Brushes.Black, 359, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 109)
                    {
                        //g.FillEllipse(Brushes.Black, 366, 122, 12, 12);
                        g.FillEllipse(Brushes.Black, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 110)
                    {
                        g.FillEllipse(Brushes.Black, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 111)
                    {
                        g.FillEllipse(Brushes.Black, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 112)
                    {
                        g.FillEllipse(Brushes.Black, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 113)
                    {
                        g.FillEllipse(Brushes.Black, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 114)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 115)
                    {
                        g.FillEllipse(Brushes.Yellow, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 116)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 378, 0, 400, 0);
                    }
                    else if (i == 117)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 118)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 119)
                    {
                        g.FillEllipse(Brushes.Red, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 120)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 121)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 122)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 123)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 124)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 125)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 126)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 127)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 128)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 129)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 130)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 131)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 132)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 133)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 134)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 135)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 136)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 137)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 138)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 139)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 140)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 141)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 142)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 143)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 144)
                    {
                        g.FillEllipse(Brushes.Green, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 145)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 146)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 147)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 148)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 149)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 150)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 151)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 152)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 153)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 154)
                    {
                        g.FillEllipse(Brushes.Red, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 155)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 156)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 157)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 158)
                    {
                        g.FillEllipse(Brushes.Blue, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                    else if (i == 159)
                    {
                        g.FillEllipse(Brushes.Red, 360, 136, 12, 12);
                        g.DrawLine(pw, 381, 0, 403, 0);
                    }
                  //  else if (i == 160)
                  //  {
                  //      g.FillEllipse(Brushes.Blue, 220, 74, 9, 9);
                  //      g.DrawLine(pw, 381, 0, 403, 0);
                  //  }




              //  g.DrawLine(pw, 385, 0, 401, 0);
            }
            


            //g.DrawEllipse(Pens.Black,)
            g.Dispose();
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (States)
            {
                Task.Delay(250).
                    ContinueWith
                    (
                        t => { this.init(); }
                    );
                States = false;
            }
            else
                this.init();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
           // MessageBox.Show(e.X.ToString("#0.0000"));
          //  MessageBox.Show(e.Y.ToString("#0.0000"));
          //  MessageBox.Show(Math.Pow((e.X - 220), 2).ToString("#0.0000"));
          //  MessageBox.Show(Math.Pow((e.Y - 74), 2).ToString("#0.0000"));
          //  MessageBox.Show(Math.Pow(12, 2).ToString("#0.0000"));
          //  (x-x0)^2+(y-y0)^2<r^2
            if (Math.Pow((e.X - 603),2 ) + Math.Pow((e.Y - 383), 2) < Math.Pow(9,2 ))
            {
                MessageBox.Show("杯子1");
            }



        }
    }
}
