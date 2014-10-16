using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Graphics3D.Math3D;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Point2D
    {
        public Point2D() : this(0, 0) { }
        public Point2D (double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString()
        {
            return String.Format("[ {0} ; {1} ]", X , Y);
        }
    }
}
