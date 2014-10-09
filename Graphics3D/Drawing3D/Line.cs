using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;

namespace Graphics3D.Drawing3D
{

    [Serializable]
    public class Line 
    {
        Vertex p1 = new Vertex(),p2 = new Vertex();

        DashStyle type = DashStyle.Solid;

        public DashStyle Type
        {
            get { return type; }
            set { type = value; }
        }

        Color color =  Color.Wheat;
        
        public Color BorderColor
        {
            get {
                
                

                return color; }
            set { color = value; }
        }

        public Vertex P1 { get { return p1; } set { p1 = value; } }
        public Vertex P2 { get { return p2; } set { p2 = value; } }

        public Line() : this(new Vertex(), new Vertex()) { }

        public Line(Vertex p1, Vertex p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public IEnumerator<Vertex> GetPoints()
        {
            yield return P1;
            yield return P2;
        }


    }
}
