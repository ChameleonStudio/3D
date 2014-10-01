using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Graphics3D.Drawing3D
{
    [Serializable]
    public class MyColor
    {
        public MyColor(Color color)
        {

        }

        Color color = Color.Black;

        public String Value
        {
            get { return color.Name; }
            set { color = Color.FromName(value); }
        }
    }
}
