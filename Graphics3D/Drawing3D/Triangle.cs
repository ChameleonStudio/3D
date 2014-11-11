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
        Vertex p1 = new Vertex(),p2 = new Vertex(),p3 = new Vertex();
     
        Color color = Color.White;
        public Color FillColor
        {
            get { return color; }
            set { color = value; }
        }

        public double GetAngle(Point3D vector)
        {
            double A11, A12, A13;
            double A,B,C,D;
            A11 = (P2.Point.Y - P1.Point.Y) * (P3.Point.Z - P1.Point.Z) - (P2.Point.Z - P1.Point.Z) * (P3.Point.Y - P1.Point.Y);
            A12 = (P2.Point.X - P1.Point.X) * (P3.Point.Z - P1.Point.Z) - (P2.Point.Z - P1.Point.Z) * (P3.Point.X - P1.Point.X);
            A13 = (P2.Point.X - P1.Point.X) * (P3.Point.Y - P1.Point.Y) - (P2.Point.Y - P1.Point.Y) * (P3.Point.X - P1.Point.X);
            A = A11;
            B = -A12;
            C = A13;
            D = -P1.Point.X * A11 + P1.Point.Y * A12 - P1.Point.Z * A13;
            return Math.Acos((A * vector.X + B * vector.Y + C * vector.Z) / (Math.Sqrt(A * A + B * B + C * C) * Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z)));
        }

        public Vertex P1 { get { return p1; } set { p1 = value; } }
        public Vertex P2 { get { return p2; } set { p2 = value; } }
        public Vertex P3 { get { return p3; } set { p3 = value; } }

        public Triangle() : this(new Vertex(), new Vertex(), new Vertex()) { }

        public Triangle(Vertex p1, Vertex p2, Vertex p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public IEnumerator<Vertex> GetPoints()
        {
            yield return P1;
            yield return P2;
            yield return P3;
        }

    }
}
