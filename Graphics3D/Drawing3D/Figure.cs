using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace Graphics3D.Drawing3D
{

    [Serializable]
    public class Figure
    {

        List<Point3D> points = new List<Point3D>();
        List<Triangle> triangles = new List<Triangle>();
        List<Line> lines = new List<Line>();


        public List<Line> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        public List<Point3D> Points
        {
            get { return points; }
            set { points = value; }
        }

        public List<Triangle> Triangles
        {
            get { return triangles; }
            set { triangles = value; }
        }

        public void AddLine(int index1, int index2)
        {
            Lines.Add(new Line(Points[index1], Points[index2]));
        }

        public void AddLine(Line line)
        {
            Lines.Add(line);
        }

        public void AddTriangle(int index1, int index2,int index3)
        {
            Triangles.Add(new Triangle(Points[index1], Points[index2],Points[index3]));
        }

        public void AddTriangle(Triangle triangle)
        {
            Triangles.Add(triangle);
        }

        public void AddPoligon(Poligon poligon)
        {
            Triangles.AddRange(poligon.GetTriangles());
        }

        public void AddPoligon(params int[] indexes)
        {
            Poligon poligon = new Poligon();
            foreach (int index in indexes)
                poligon.Points.Add(Points[index]);
            AddPoligon(poligon);
        }



    }
}
