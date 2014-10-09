using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace Graphics3D.Drawing3D
{
    public enum LineType
    {
        Solid,
        DashAndDot
    }


    [Serializable]
    public class Line 
    {
        Vertex p1 = new Vertex(),p2 = new Vertex();

        LineType type = LineType.Solid;

        public LineType Type
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

        public Line(Vertex p1, Vertex p2, LineType type = LineType.Solid, String color =  "Black")
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
