using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Graphics3D.Math3D;
using System.Drawing;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Point3D
    {
        public Point3D() : this(0, 0, 0) { }
        public Point3D (double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D Reverse(Point3D point)
        {
            Point3D ans = new Point3D();
            ans.X = -point.X;
            ans.Y = -point.Y;
            ans.Z = -point.Z;
            return ans;
        }

        public Point3D Clone()
        {
            return new Point3D(X, Y, Z);
        }

        [XmlAttribute()]
        public double X { get; set; }
        [XmlAttribute()]
        public double Y { get; set; }
        [XmlAttribute()]
        public double Z { get; set; }

        public static implicit operator PointF(Point3D p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }

        public override string ToString()
        {
            return String.Format("[ {0} ; {1} ; {2} ]", X, Y, Z);
        }

    }

}
