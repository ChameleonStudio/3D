using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Poligon
    {
        public Poligon(){}

        public Poligon(params Vertex[] points)
        {
            this.points.AddRange(points);
        }

        List<Vertex> points = new List<Vertex>();

        public List<Vertex> Points
        {
            get { return points; }
        }

        public List<Triangle> GetTriangles()
        {
            List<Triangle> ans = new List<Triangle>();

            //TODO:



            return ans;
        }
    }
}
