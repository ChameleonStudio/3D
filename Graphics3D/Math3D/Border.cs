using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphics3D.Math3D
{
    public class Border
    {

        #region Constructors
        public Border() : this(0, 0, 0, 0) { }
        public Border(int left, int right, int top,int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }
        #endregion

        #region Properties
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
        #endregion

    }
}
