using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using Graphics3D;
using Graphics3D.Drawing3D;
using Graphics3D.Math3D;

namespace Graphics3D.Drawing3D
{

    [Serializable]
    public class Figure
    {
        String name;

        bool selectable = true;

        public bool Selectable
        {
            get { return selectable; }
            set { selectable = value; }
        }

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


        public void SetBorderColor(Color color)
        {
            foreach(Line l in Lines)
            {
                l.BorderColor = color;
            }
            foreach (Triangle t in Triangles)
            {
                t.FillColor = color;
            }
        }
        public Color GetBorderColor()
        {
            try
            {
                return Lines[0].BorderColor;
            }
            catch (Exception)
            {
                return Color.Black;
            }             
        }


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

        public void RelativeRotate(Transformation rotate)
        {
            Point3D center = GetCenter();
            transform(Matrix.GetTranslateMatrix(new Transformation(Point3D.Reverse(center))));
            transform(Matrix.GetRotationMatrix(rotate));
            transform(Matrix.GetTranslateMatrix(new Transformation(center)));
        }

        public void Translate(Transformation translate)
        {
            transform(Matrix.GetTranslateMatrix(translate));
        }

        public void RelativeScale(Transformation scale)
        {
            Point3D center = GetCenter();
            transform(Matrix.GetTranslateMatrix(new Transformation(Point3D.Reverse(center))));
            transform(Matrix.GetScaleMatrix(scale));
            transform(Matrix.GetTranslateMatrix(new Transformation(center)));
        }

        public void TranslateToCenter()
        {
            Point3D center = GetCenter();
            transform(Matrix.GetTranslateMatrix(new Transformation(Point3D.Reverse(center))));
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

        public Point3D GetCenter()
        {
            Point3D center = new Point3D();
            for (int i = 0; i < Vertexes.Count; i++)
            {
                center.X += Vertexes[i].Point.X;
                center.Y += Vertexes[i].Point.Y;
                center.Z += Vertexes[i].Point.Z;
            }
            center.X /= Vertexes.Count;
            center.Y /= Vertexes.Count;
            center.Z /= Vertexes.Count;
            return center;
        }

        bool selected = false;

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        bool hidden = false;

        public bool Hidden
        {
            get { return hidden; }
            set { hidden = value; }
        }

    }
}
