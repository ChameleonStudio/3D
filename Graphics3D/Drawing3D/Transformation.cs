using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Transformation
    {
        public Transformation() : this(0, 0, 0) { }

        public static implicit operator Transformation (double d)
        {
            return new Transformation(d, d, d);
        }

        public static Transformation operator +(Transformation t1, Transformation t2)
        {
            return new Transformation(t1.OX + t2.OX, t1.OY + t2.OY, t1.OZ + t2.OZ);
        }

        public static Transformation operator -(Transformation t1, Transformation t2)
        {
            return new Transformation(t1.OX - t2.OX, t1.OY - t2.OY, t1.OZ - t2.OZ);
        }

        public Transformation (double ox,double oy, double oz)
        {
            OX = ox;
            OY = oy;
            OZ = oz;
        }

        public Transformation(Point3D point)
        {
            OX = point.X;
            OY = point.Y;
            OZ = point.Z;
        }

        double ox = 0;

        public double OX
        {
            get { return ox; }
            set { ox = value; }
        }
        double oy = 0;

        public double OY
        {
            get { return oy; }
            set { oy = value; }
        }
        double oz = 0;

        public double OZ
        {
            get { return oz; }
            set { oz = value; }
        }
    }
}
