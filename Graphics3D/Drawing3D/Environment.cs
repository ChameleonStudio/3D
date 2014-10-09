using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphics3D.Math3D;
using System.Drawing;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Environment
    {
        Dictionary<String,Figure> figures = new Dictionary<String, Figure>();

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

        public Bitmap GetImage(int width, int height)
        {
            TransformationRefresh();
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Black);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            double z = -500;
            Random a = new Random();
            foreach (Figure f in Figures.Values)
            {
                foreach (Line l in f.Lines)
                {
                    g.DrawLine(new Pen(l.BorderColor, 1),
                        new Point3D(l.P1.TPoint.X * (z / (z - l.P1.TPoint.Z)) + width / 2, -l.P1.TPoint.Y * (z / (z - l.P1.TPoint.Z)) + height / 2, 1),
                        new Point3D(l.P2.TPoint.X * (z / (z - l.P2.TPoint.Z)) + width / 2, -l.P2.TPoint.Y * (z / (z - l.P2.TPoint.Z)) + height / 2, 1));
                }
            }
            return b;
        }

        public void AddFigure(Figure figure)
        {
            Figures.Add(figure.Name, figure);
        }

        public static void Save(String filename, Environment environment)
        {
            XmlWriter x;
            XmlWriterSettings s = new XmlWriterSettings();
            s.CloseOutput = true;
            s.Indent = true;
            x = XmlWriter.Create(filename,s);
            new XmlSerializer(typeof(Graphics3D.Drawing3D.Environment)).Serialize(x, environment);
            x.Close();
        }

        public static Environment Load(String filename)
        {
            XmlReader x;
            x = XmlReader.Create(filename);
            Environment environment = (Environment)new XmlSerializer(typeof(Graphics3D.Drawing3D.Environment)).Deserialize(x);
            x.Close();
            return environment;
        }
    }
}
