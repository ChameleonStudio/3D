using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphics3D;
using Graphics3D.Math3D;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Vertex
    {
        Point3D point, tPoint;

        public Vertex() : this(0, 0, 0) { }

        public Vertex (Point3D point)
        {
            this.point = point.Clone();
            this.tPoint = point.Clone();
        }

        public Vertex(double x, double y, double z)
        {
            point = new Point3D(x, y, z);
            tPoint = new Point3D(x, y, z);
        }

        public Point3D TPoint
        {
            get { return tPoint; }
            set { tPoint = value; }
        }

        public Point3D Point
        {
            get { return point; }
            set { 
                point = value;
                tPoint = point.Clone();
                }
        }

        public void ResetTransform()
        {
            TPoint = Point.Clone();
        }


        public void Transform(Matrix transformation)
        {

            Matrix m = (new double[,] { { Point.X, Point.Y, Point.Z, 1 } }) * transformation;
            TPoint.X = m[0, 0];
            TPoint.Y = m[0, 1];
            TPoint.Z = m[0, 2];
        }

        public void AbsTransform(Matrix transformation)
        {

            Matrix m = (new double[,] { { Point.X, Point.Y, Point.Z, 1 } }) * transformation;
            Point.X = m[0, 0];
            Point.Y = m[0, 1];
            Point.Z = m[0, 2];
            TPoint.X = Point.X;
            TPoint.Y = Point.Y;
            TPoint.Z = Point.Z;
        }
    }
}
