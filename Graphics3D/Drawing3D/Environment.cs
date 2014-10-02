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
        Camera camera = new Camera();
        List<Figure> figures = new List<Figure>();

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public List<Figure> Figures
        {
            get { return figures; }
            set { figures = value; }
        }

        public void Transform(Matrix Rotate , Matrix Scale, Matrix Translate, Matrix Perspective)
        {
            Matrix transformation = new Matrix(4);
            transformation *= Rotate * Scale * Translate * Perspective;
            foreach(Figure f in Figures)
            {
                for (int i = 0; i < f.Points.Count; i++)
                {
                    f.Points[i].Transform(transformation);
                }
            }
        }

        public Bitmap GetImage(int width, int height)
        {
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            foreach (Figure f in Figures)
            {
                foreach (Line l in f.Lines)
                {
                    g.DrawLine(new Pen(Color.FromName(l.BorderColor), 1),
                        new Point3D(l.P1.X * (1000 / (1000 - l.P1.Z)) + width / 2, -l.P1.Y * (1000 / (1000 - l.P1.Z)) + height / 2, 1),
                        new Point3D(l.P2.X * (1000 / (1000 - l.P2.Z)) + width / 2, -l.P2.Y * (1000 / (1000 - l.P2.Z)) + height / 2, 1));
                }
            }
            return b;
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
