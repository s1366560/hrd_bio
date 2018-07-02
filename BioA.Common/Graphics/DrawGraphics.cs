// ================================================================================================
//
// 文件名（File Name）：              DrawGraphics.cs
//
// 功能描述（Description）：          处理自制绘图功能类
//
// 数据表（Tables）：                 none
//
// 作者（Author）：                   冯旗
//
// 日期（Create Date）：              2017-6-26
//
// 修改记录（Revision History）：
//      R1:
//          修改人：
//          修改日期：
//          修改理由：
//
// ================================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BioA.Common
{
    public static class DrawGraphics
    {
        /// <summary>
        /// 绘图带圆角的矩形
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p">画笔</param>
        /// <param name="X">x轴起始点</param>
        /// <param name="Y">y轴起始点</param>
        /// <param name="width">矩形宽度</param>
        /// <param name="height">矩形高度</param>
        /// <param name="radius">角的弧度半径</param>
        //public static void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        //{
        //    GraphicsPath gp = new GraphicsPath();

        //    gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);

        //    gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);

        //    gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));

        //    gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);

        //    gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);

        //    gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);

        //    gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);

        //    gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);

        //    gp.CloseFigure();

        //    g.DrawPath(p, gp);
        //    gp.Dispose();
        //}
    }
}
