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

        public Poligon(params Point3D[] points)
        {
            this.points.AddRange(points);
        }

        List<Point3D> points = new List<Point3D>();

        public List<Point3D> Points
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
