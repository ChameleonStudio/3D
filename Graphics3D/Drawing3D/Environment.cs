using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using Graphics3D.Math3D;
using System.Drawing;
using Graphics3D.DepthTest;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public enum ImageType
    {
        Poligons,Lines,LinesAndPoligons
    }
    [Serializable]
    public class Environment
    {
        Dictionary<String,Figure> figures = new Dictionary<String, Figure>();
        ImageType imageType = ImageType.LinesAndPoligons;

        public ImageType ImageType
        {
            get { return imageType; }
            set { imageType = value; }
        }

        public Dictionary<String, Figure> Figures
        {
            get { return figures; }
            set { figures = value; }
        }

        public void Transform(Matrix matrix4x4)
        {
            foreach (Figure f in Figures.Values)
            {
                for (int i = 0; i < f.Vertexes.Count; i++)
                {
                    f.Vertexes[i].Transform(matrix4x4);
                }
            }
        }

        void transform(Matrix Rotate, Matrix Scale, Matrix Translate)
        {
            Matrix transformation = new Matrix(4);
            transformation *= Rotate * Scale * Translate;
            foreach(Figure f in Figures.Values)
            {
                for (int i = 0; i < f.Vertexes.Count; i++)
                {
                    f.Vertexes[i].Transform(transformation);
                }
            }
        }

        Transformation angle = new Transformation(),
                       scale = new Transformation(1,1,1),
                       translate = new Transformation();

        public Transformation Translate
        {
            get { return translate; }
            set { translate = value;
            transform(Matrix.GetRotationMatrix(Angle), Matrix.GetScaleMatrix(Scale), Matrix.GetTranslateMatrix(Translate));
            }
        }

        public Transformation Scale
        {
            get { return scale; }
            set { scale = value;
            transform(Matrix.GetRotationMatrix(Angle), Matrix.GetScaleMatrix(Scale), Matrix.GetTranslateMatrix(Translate));
            }
        }

        public Transformation Angle
        {
            get { return angle; }
            set { angle = value;
            //transform(Matrix.GetRotationMatrix(Angle), Matrix.GetScaleMatrix(Scale), Matrix.GetTranslateMatrix(Translate));
            transform(Matrix.GetRotationOZMatrix(Angle.OZ) * Matrix.GetRotationOYMatrix(Angle.OY) * Matrix.GetRotationOXMatrix(Angle.OX), Matrix.GetScaleMatrix(Scale), Matrix.GetTranslateMatrix(Translate));
            }
        }

        public void TransformationRefresh()
        {
            transform(Matrix.GetRotationOZMatrix(Angle.OZ) * Matrix.GetRotationOYMatrix(Angle.OY) * Matrix.GetRotationOXMatrix(Angle.OX), Matrix.GetScaleMatrix(Scale), Matrix.GetTranslateMatrix(Translate));
        }
        double z = -500;

        [NonSerialized]
        ZBitmap b;

        public Bitmap GetImage()
        {
            TransformationRefresh();
                foreach (Figure f in Enumerable.Where(Figures.Values, f => !f.Hidden))
                {
                    int w = 1;
                    if (f.Selected)
                    {
                        w = 2;
                    }
                    switch(imageType)
                    {
                        case Drawing3D.ImageType.Lines:
                            foreach (Line l in f.Lines)
                                try
                                {
                                    b.DrawLine(l, w);
                                }
                                catch (Exception) { }
                            break;
                        case Drawing3D.ImageType.Poligons:
                            foreach (Triangle t in f.Triangles)
                                try
                                {
                                    b.FillTriangle(t);
                                }
                                catch (Exception) { }
                            if (f.Name == "Axis" || f.Selected)
                            {
                                foreach (Line l in f.Lines)
                                    try
                                    {
                                        b.DrawLine(l, w);
                                    }
                                    catch (Exception) { }
                            }
                            break;
                        case Drawing3D.ImageType.LinesAndPoligons:
                            foreach (Line l in f.Lines)
                                try
                                {
                                    b.DrawLine(l, w);
                                }
                                catch (Exception) { }
                            foreach (Triangle t in f.Triangles)
                                try
                                {
                                    b.FillTriangle(t);
                                }
                                catch (Exception) { }
                            break;
                    }
                   
                   
                }
           
            return b.Bitmap;
        }

        public void Resize(int width, int height)
        {
            b = new ZBitmap(width, height, backgroundColor);
        }

        public Figure CheckFigure(Point2D mouse)
        {
                    Figure ans = null;
                    if(ImageType != Drawing3D.ImageType.Poligons){
                    try
                    {
                        foreach (Figure f in Figures.Values)
                        {
                            foreach (Line l in f.Lines)
                            {
                                Point2D p1 = new Point2D(l.P1.TPoint.X * (z / (z - l.P1.TPoint.Z)) + b.Width / 2, -l.P1.TPoint.Y * (z / (z - l.P1.TPoint.Z)) + b.Height / 2);
                                Point2D p2 = new Point2D(l.P2.TPoint.X * (z / (z - l.P2.TPoint.Z)) + b.Width / 2, -l.P2.TPoint.Y * (z / (z - l.P2.TPoint.Z)) + b.Height / 2);
                                if (dist(mouse, p1) + dist(mouse, p2)- 0.3 <= dist(p1, p2) && f.Selectable)
                                    return f;
                            }
                        }
                    }
        

                    catch (Exception) {}
                    }else{
                    double depth = double.MaxValue;
            
                    foreach (Figure f in Figures.Values)
                    {
                        foreach (Triangle triangle in f.Triangles)
                        {
                            Point3D P1 =  new Point3D(triangle.P1.TPoint.X * (z / (z - triangle.P1.TPoint.Z)) + b.Width / 2, -triangle.P1.TPoint.Y * (z / (z - triangle.P1.TPoint.Z)) + b.Height / 2, triangle.P1.TPoint.Z);
                            Point3D P2 =  new Point3D(triangle.P2.TPoint.X * (z / (z - triangle.P2.TPoint.Z)) + b.Width / 2, -triangle.P2.TPoint.Y * (z / (z - triangle.P2.TPoint.Z)) + b.Height / 2, triangle.P2.TPoint.Z);
                            Point3D P3 =  new Point3D(triangle.P3.TPoint.X * (z / (z - triangle.P3.TPoint.Z)) + b.Width / 2, -triangle.P3.TPoint.Y * (z / (z - triangle.P3.TPoint.Z)) + b.Height / 2, triangle.P3.TPoint.Z);
                            double A11, A12, A13;
                            double A,B,C,D;
                            A11 = (P2.Y - P1.Y) * (P3.Z - P1.Z) - (P2.Z - P1.Z) * (P3.Y - P1.Y);
                            A12 = (P2.X - P1.X) * (P3.Z - P1.Z) - (P2.Z - P1.Z) * (P3.X - P1.X);
                            A13 = (P2.X - P1.X) * (P3.Y - P1.Y) - (P2.Y - P1.Y) * (P3.X - P1.X);
                            A = A11;
                            B = -A12;
                            C = A13;
                            D = -P1.X * A11 + P1.Y * A12 - P1.Z * A13;
                            if (Math.Sign((P1.X - mouse.X) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P1.Y - mouse.Y)) == Math.Sign((P2.X - mouse.X) * (P3.Y - P2.Y) - (P3.X - P2.X) * (P2.Y - mouse.Y)))
                                if (Math.Sign((P1.X - mouse.X) * (P2.Y - P1.Y) - (P2.X - P1.X) * (P1.Y - mouse.Y)) == Math.Sign((P3.X - mouse.X) * (P1.Y - P3.Y) - (P1.X - P3.X) * (P3.Y - mouse.Y)))
                                {
                                    if(depth > (-A * mouse.X - B * mouse.Y - D) / C)
                                    {
                                        depth = (-A * mouse.X - B * mouse.Y - D) / C;
                                        ans = f;
                                    }
                                }     
                        }
                    }
        }
                    return ans;
        }

        private double dist(Point2D p1, Point2D p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public void AddFigure(Figure figure)
        {
            Figures.Add(figure.Name, figure);
        }

        public static void Save(String filename, Environment environment)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = File.Open(filename, FileMode.Create);
            formatter.Serialize(stream, environment);
            stream.Close(); 
        }

        public static Environment Load(String filename)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = File.Open(filename, FileMode.Open);
            Environment env = (Environment)formatter.Deserialize(stream);
            stream.Close();
            return env;
        }

        Color backgroundColor = Color.Black;

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

    }
}
