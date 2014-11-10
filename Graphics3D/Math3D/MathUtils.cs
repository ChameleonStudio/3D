using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphics3D.Drawing3D;

namespace Graphics3D.Math3D
{
    public class MathUtils
    {
        public static Border GetBorder(Point3D p1, Point3D p2, Point3D p3)
        {
            return new Border((int)Min(p1.X, p2.X, p3.X),
                              (int)Max(p1.X, p2.X, p3.X),
                              (int)Min(p1.Y, p2.Y, p3.Y),
                              (int)Max(p1.Y, p2.Y, p3.Y));
        }

        public static double Min(params double[] items)
        {
            double min = items[0];
            for (int i = 1; i < items.Length; i++)
                if (items[i] < min)
                    min = items[i];
            return min;
        }

        public static double Max(params double[] items)
        {
            double max = items[0];
            for (int i = 1; i < items.Length; i++)
                if (items[i] > max)
                    max = items[i];
            return max;
        }
    }
}
