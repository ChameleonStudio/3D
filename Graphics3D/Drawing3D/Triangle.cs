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
