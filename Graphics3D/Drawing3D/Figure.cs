using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Graphics3D;
using Graphics3D.Drawing3D;
using Graphics3D.Math3D;

namespace Graphics3D.Drawing3D
{

    [Serializable]
    public class Figure
    {
        String name;

        public Figure(String name)
        {
            Name = name;
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        List<Vertex> vertexses = new List<Vertex>();
        List<Triangle> triangles = new List<Triangle>();
        List<Line> lines = new List<Line>();


        public List<Line> Lines
        {
            get { return lines; }
            set { lines = value; }
        }

        public List<Vertex> Vertexes
        {
            get { return vertexses; }
            set { vertexses = value; }
        }

        public List<Triangle> Triangles
        {
            get { return triangles; }
            set { triangles = value; }
        }

        public void AddLine(int index1, int index2)
        {
            Lines.Add(new Line(Vertexes[index1], Vertexes[index2]));
        }

        public void AddLine(Line line)
        {
            Lines.Add(line);
        }

        public void AddTriangle(int index1, int index2,int index3)
        {
            Triangles.Add(new Triangle(Vertexes[index1], Vertexes[index2],Vertexes[index3]));
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
                poligon.Points.Add(Vertexes[index]);
            AddPoligon(poligon);
        }

        public void OXRotate(double angle)
        {
            transform(Matrix.GetRotationOXMatrix(angle));
        }

        public void OYRotate(double angle)
        {
            transform(Matrix.GetRotationOYMatrix(angle));
        }

        public void OZRotate(double angle)
        {
            transform(Matrix.GetRotationOZMatrix(angle));
        }

        public void Rotate(Transformation rotate)
        {
            transform(Matrix.GetRotationMatrix(rotate));
        }

        public void Translate(Transformation translate)
        {
            transform(Matrix.GetTranslateMatrix(translate));
        }

        public void Scale(Transformation scale)
        {
            transform(Matrix.GetScaleMatrix(scale));
        }

        public void Scale(double scale)
        {
            transform(Matrix.GetScaleMatrix(scale));
        }

        private void transform(Matrix transformation)
        {
            for (int i = 0; i < Vertexes.Count; i++)
            {
                Vertexes[i].AbsTransform(transformation);
            }
        }
    }
}
