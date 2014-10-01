using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Triangle
    {
        Point3D p1 = new Point3D(),p2 = new Point3D(),p3 = new Point3D();
        MyColor fillColor = new MyColor(Color.White);

        [XmlAttribute()]
        public MyColor FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }

        public Point3D P1 { get { return p1; } set { p1 = value; } }
        public Point3D P2 { get { return p2; } set { p2 = value; } }
        public Point3D P3 { get { return p3; } set { p3 = value; } }

        public Triangle() : this(new Point3D(), new Point3D(), new Point3D()) { }

        public Triangle(Point3D p1, Point3D p2, Point3D p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public IEnumerator<Point3D> GetPoints()
        {
            yield return P1;
            yield return P2;
            yield return P3;
        }

    }
}
