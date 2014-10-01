using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class Camera
    {
        public Camera() : this(new Point3D(0, 0, -100), new Point3D()) { }
        public Camera(Point3D eye, Point3D target)
        {
            Eye = eye;
            Target = target;
        }

        Point3D eye = new Point3D();
        public Point3D Eye
        {
            get { return eye; }
            set { eye = value; }
        }

        Point3D target = new Point3D();
        public Point3D Target
        {
            get { return target; }
            set { target = value; }
        }
       
    }
}
