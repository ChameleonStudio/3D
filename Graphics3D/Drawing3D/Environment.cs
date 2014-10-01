using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void Save(String filename)
        {
            System.Xml.Schema.XmlSchema sh = new System.Xml.Schema.XmlSchema();
            new XmlSerializer(typeof(Graphics3D.Drawing3D.Environment)).Serialize(XmlWriter.Create(filename), this);
        }

    }
}
