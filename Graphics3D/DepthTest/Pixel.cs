using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Graphics3D.DepthTest
{
    [Serializable]
    public class Pixel
    {
        public Pixel(int x,int y,double depth = Double.MaxValue)
        {
            X = x;
            Y = y;
            Depth = depth;
        }
        int x = 0, y = 0;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        Color col = Color.White;

        public Color Color
        {
            get { return col; }
            set { col = value; }
        }

        public void Set(Color color, double depth)
        {
            if (depth <= Depth)
            {
                this.col = color;
                this.depth = depth;
            }
        }
        double depth = 0;

        public double Depth
        {
            get { return depth; }
            set { depth = value; }
        }

    }
}
