using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Line 
    {
        Point3D p1 = new Point3D(),p2 = new Point3D();

        Color color = Color.Black;
        public String BorderColor
        {
            get { return color.Name; }
            set { color = Color.FromName(value); }
        }

        public Point3D P1 { get { return p1; } set { p1 = value; } }
        public Point3D P2 { get { return p2; } set { p2 = value; } }

        //public Line() : this(new Point3D(), new Point3D()) { }

        public Line(Point3D p1, Point3D p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public IEnumerator<Point3D> GetPoints()
        {
            yield return P1;
            yield return P2;
        }


    }
}
