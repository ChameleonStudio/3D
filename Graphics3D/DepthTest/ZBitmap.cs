using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Graphics3D;
using Graphics3D.Math3D;
using Graphics3D.Drawing3D;

namespace Graphics3D.DepthTest
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
        public Bitmap Bitmap
        {
            get 
            {
                g.Clear(backColor);
                BitmapData data = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int stride = data.Stride;
                unsafe
                {
                    byte* ptr = (byte*)data.Scan0;
                    foreach(Pixel pixel in pixels)
                    {
                        if (pixel.Color != backColor)
                        {
                            ptr[(pixel.X * 3) + pixel.Y * stride] = pixel.Color.B;
                            ptr[(pixel.X * 3) + pixel.Y * stride + 1] = pixel.Color.G;
                            ptr[(pixel.X * 3) + pixel.Y * stride + 2] = pixel.Color.R;
                            //ptr[(pixel.X * 4) + pixel.Y * stride + 3] = pixel.Color.A;
                        }
                    }

                    
                }
                b.UnlockBits(data);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                       // if (pixels[x, y].Color != backColor) {
                        pixels[x, y].Depth = Double.MaxValue;
                        pixels[x, y].Color = backColor;
                    //}
                    }
                }
                
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
            Point3D lightnes = new Point3D(-1, 1, -0.5);
            double currentDepth;
            Point3D P1 =  new Point3D(triangle.P1.TPoint.X * (z / (z - triangle.P1.TPoint.Z)) + width / 2, -triangle.P1.TPoint.Y * (z / (z - triangle.P1.TPoint.Z)) + height / 2, triangle.P1.TPoint.Z);
            Point3D P2 =  new Point3D(triangle.P2.TPoint.X * (z / (z - triangle.P2.TPoint.Z)) + width / 2, -triangle.P2.TPoint.Y * (z / (z - triangle.P2.TPoint.Z)) + height / 2, triangle.P2.TPoint.Z);
            Point3D P3 =  new Point3D(triangle.P3.TPoint.X * (z / (z - triangle.P3.TPoint.Z)) + width / 2, -triangle.P3.TPoint.Y * (z / (z - triangle.P3.TPoint.Z)) + height / 2, triangle.P3.TPoint.Z);
            Border border = MathUtils.GetBorder(P1,P2,P3);
            if (!(border.Right < 0 || border.Top > Height || border.Left > Width || border.Bottom < 0))
            {
                double A11, A12, A13;
                double A,B,C,D;
                A11 = (P2.Y - P1.Y) * (P3.Z - P1.Z) - (P2.Z - P1.Z) * (P3.Y - P1.Y);
                A12 = (P2.X - P1.X) * (P3.Z - P1.Z) - (P2.Z - P1.Z) * (P3.X - P1.X);
                A13 = (P2.X - P1.X) * (P3.Y - P1.Y) - (P2.Y - P1.Y) * (P3.X - P1.X);
                A = A11;
                B = -A12;
                C = A13;
                D = -P1.X * A11 + P1.Y * A12 - P1.Z * A13;

                double fi = triangle.GetAngle(lightnes);
                fi = 255 - (fi / Math.PI) * 255;

                int startx = Width, endx = 0;
                for (int y = border.Top; y <= border.Bottom; y++)
                {
                    startx = Width; endx = 0;
                    if (y >= 0 && y < Width)
                    {
                        for (int x = border.Left; x < border.Right; x++)
                            if (Math.Sign((P1.X - x) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P1.Y - y)) == Math.Sign((P2.X - x) * (P3.Y - P2.Y) - (P3.X - P2.X) * (P2.Y - y)))
                                if (Math.Sign((P1.X - x) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P1.Y - y)) == Math.Sign((P3.X - x) * (P1.Y - P3.Y) - (P1.X - P3.X) * (P3.Y - y)))
                                      { startx = x; break; }
                        for (int x = border.Right; x > border.Left; x--)
                            if (Math.Sign((P1.X - x) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P1.Y - y)) == Math.Sign((P2.X - x) * (P3.Y - P2.Y) - (P3.X - P2.X) * (P2.Y - y)))
                                if (Math.Sign((P1.X - x) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P1.Y - y)) == Math.Sign((P3.X - x) * (P1.Y - P3.Y) - (P1.X - P3.X) * (P3.Y - y)))
                                    { endx = x; break; }
                        endx+=2;
                        for (int x = startx; x < endx; x++)
                            if (x >= 0 && x < Width)
                            {
                                currentDepth = (-A * x - B * y - D) / C;
                                pixels[x, y].Set(Color.FromArgb(255, (int)((double)triangle.FillColor.R / 255.0 * fi), (int)((double)triangle.FillColor.G / 255.0 * fi), (int)((double)triangle.FillColor.B / 255.0 * fi)), currentDepth);
                            }
                            }
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
        public void DrawLine(Line line, int w = 1)
        {
            double currentDepth;
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
               swap = 0;
               if ((kl = dx) < (s = dy)) {
                  dx = s;  dy = kl;  kl = s; ++swap;
               }
               int oldKl = kl;
               s = (incr1 = 2*dy)-dx;
               incr2 = 2*dx;
               currentDepth = P1.Z;

                   if (xn >= 0 && xn < Width && yn >= 0 && yn < Width)
                       if (w == 1)
                           pixels[xn, yn].Set(line.BorderColor, currentDepth);
                       else
                       {
                           pixels[xn, yn].Set(line.BorderColor, currentDepth);
                           if (xn + 1 < Width) pixels[xn + 1, yn].Set(line.BorderColor, currentDepth);
                           if (yn + 1 < Height && xn + 1 < Width) pixels[xn + 1, yn + 1].Set(line.BorderColor, currentDepth);
                           if (yn + 1 < Height) pixels[xn, yn + 1].Set(line.BorderColor, currentDepth);
                       }

            while (--kl >= 0) 
            {
                if (s >= 0) {
                    if (swap > 0) xn += sx; else yn += sy;
                        s-= incr2;
                }
                if (swap > 0) yn+= sy; else xn+= sx;
                    s+=  incr1;

                currentDepth = P2.Z + ((P1.Z - P2.Z) / (double)oldKl) * (double)kl;
                if (xn >= 0 && xn < Width && yn >= 0 && yn < Width)
                    if(w==1)
                        pixels[xn, yn].Set(line.BorderColor, currentDepth);
                    else
                    {
                        pixels[xn, yn].Set(line.BorderColor, currentDepth);
                        if (xn + 1 < Width) pixels[xn + 1, yn].Set(line.BorderColor, currentDepth);
                        if (yn + 1 < Height && xn + 1 < Width) pixels[xn + 1, yn + 1].Set(line.BorderColor, currentDepth);
                        if (yn + 1 < Height) pixels[xn, yn + 1].Set(line.BorderColor, currentDepth);
                    }

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
            g.Clear(color);
            System.Threading.Thread t = new System.Threading.Thread(()=>System.GC.Collect()); t.Start();
        }
    }
}
