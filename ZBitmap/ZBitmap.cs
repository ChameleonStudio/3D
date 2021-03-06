﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Graphics3D;
using Graphics3D.Drawing3D;

namespace ZBitmap
{
    public class ZBitmap
    {
        int width, height;
        Bitmap b;
        Graphics g;
        Pixel[,] pixels;
        Color backColor = Color.Black;

    #region Constructors
        public ZBitmap(int width, int height):this(width,height,Color.Black){}
        public ZBitmap(int width, int height,Color backColor)
        {
            this.backColor = backColor;
            this.width = width;
            this.height = height;
            b = new Bitmap(width, height);
            g = Graphics.FromImage(b);
            Clear(backColor);
        }
    #endregion

    #region Properties
        public int Height
        {
            get { return height; }
        }
        public int Width
        {
            get { return width; }
        }
        public Bitmap B
        {
            get 
            { 
                Pen pen = new Pen(Color.White);
                foreach(Pixel pixel in pixels)
                {
                    pen.Color = pixel.Color;
                    g.DrawRectangle(pen, pixel.X, pixel.Y, 1, 1);
                }
                g.Flush();
                return b; 
            }
        }
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }
    #endregion

        public void FillTriangle(Triangle triangle)
        {
            Point3D P1 =  new Point3D(triangle.P1.TPoint.X * (z / (z - triangle.P1.TPoint.Z)) + width / 2, -triangle.P1.TPoint.Y * (z / (z - triangle.P1.TPoint.Z)) + height / 2, triangle.P1.TPoint.Z);
            Point3D P2 =  new Point3D(triangle.P2.TPoint.X * (z / (z - triangle.P2.TPoint.Z)) + width / 2, -triangle.P2.TPoint.Y * (z / (z - triangle.P2.TPoint.Z)) + height / 2, triangle.P2.TPoint.Z);
            Point3D P3 =  new Point3D(triangle.P3.TPoint.X * (z / (z - triangle.P3.TPoint.Z)) + width / 2, -triangle.P3.TPoint.Y * (z / (z - triangle.P3.TPoint.Z)) + height / 2, triangle.P3.TPoint.Z);
            Border border = MathUtils.GetBorder(P1,P2,P3);
            for (int y = border.Top; y < border.Bottom; y++)
            {
                for (int x = border.Left; x < border.Right; x++)
                {

                }
            }
        }

        public void DrawTriangle(Triangle triangle, Color borderColor)
        {
            Line line1,line2,line3;
            line1 = new Line(triangle.P1, triangle.P2);
            line1.BorderColor = borderColor;
            line2 = new Line(triangle.P2, triangle.P3);
            line2.BorderColor = borderColor;
            line3 = new Line(triangle.P3, triangle.P1);
            line3.BorderColor = borderColor;
            DrawLine(line1);
            DrawLine(line2);
            DrawLine(line3);
        }


        double z = -500;
        public void DrawLine(Line line)
        {
            Point3D P1 =  new Point3D(line.P1.TPoint.X * (z / (z - line.P1.TPoint.Z)) + width / 2, -line.P1.TPoint.Y * (z / (z - line.P1.TPoint.Z)) + height / 2, line.P1.TPoint.Z);
            Point3D P2 =  new Point3D(line.P2.TPoint.X * (z / (z - line.P2.TPoint.Z)) + width / 2, -line.P2.TPoint.Y * (z / (z - line.P2.TPoint.Z)) + height / 2, line.P2.TPoint.Z);
            
            int  dx, dy, s, sx, sy, kl, swap, incr1, incr2;
            int  xn = (int)P1.X,
                 yn = (int)P1.Y,
                 xk = (int)P2.X,
                 yk = (int)P2.Y;
               /* Вычисление приращений и шагов */
               sx = 0;
               if ((dx = xk-xn) < 0) {dx = -dx; --sx;} else if (dx>0) ++sx;
               sy= 0;
               if ((dy = yk-yn) < 0) {dy = -dy; --sy;} else if (dy>0) ++sy;
               /* Учет наклона */
               swap = 0;
               if ((kl = dx) < (s = dy)) {
                  dx = s;  dy = kl;  kl = s; ++swap;
               }
               int oldKl = kl;
               s = (incr1 = 2*dy)-dx;/* incr1 - констан. перевычисления */
                                     /* разности если текущее s < 0  и  */
                                     /* s - начальное значение разности */
               incr2 = 2*dx;         /* Константа для перевычисления    */
                                     /* разности если текущее s >= 0    */
               pixels[xn,yn].Color = line.BorderColor; /* Первый  пиксел вектора       */
               pixels[xn, yn].Depth = P1.Z;
               while (--kl >= 0) {
                  if (s >= 0) {
                      if (swap > 0) xn += sx; else yn += sy;
                     s-= incr2;
                  }
                  if (swap > 0) yn+= sy; else xn+= sx;
                  s+=  incr1;
                  pixels[xn, yn].Color = line.BorderColor; ; /* Текущая  точка  вектора   */
                  pixels[xn, yn].Depth = P1.Z + (P2.Z - P1.Z) / (oldKl-kl+1);
               }
        }

        public void Clear(Color color)
        {
            pixels = new Pixel[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixels[x, y] = new Pixel(x, y);
                    pixels[x, y].Color = color;    
                }
            }
            foreach(Pixel pixel in pixels)
            {
               
            }
            g.Clear(color);
            //System.Threading.Thread t = new System.Threading.Thread(()=>System.GC.Collect()); t.Start();
        }
    }
}
