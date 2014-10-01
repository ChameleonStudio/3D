using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Graphics3D;
using Graphics3D.Drawing3D;
using Graphics3D.Math3D;

namespace Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics3D.Drawing3D.Environment E = new Graphics3D.Drawing3D.Environment();

        private void button1_Click(object sender, EventArgs e)
        {
            Figure f = new Figure();
           
            f.Points.Add(new Point3D(1,2,3));
            f.Points.Add(new Point3D(2, 2, 3));
            f.Points.Add(new Point3D(3, 2, 3));
            f.Points.Add(new Point3D(4, 2, 3));
            f.Points.Add(new Point3D(5, 2, 3));
            f.AddTriangle(0, 1, 2);
            f.AddLine(4, 3);
            f.AddLine(1, 3);
            f.AddLine(0, 3);
            E.Figures.Add(f);
            E.Camera.Target = new Point3D(1, 1, 1);

            Graphics3D.Drawing3D.Environment.Save(System.Environment.CurrentDirectory + "\\f.xml", E);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics3D.Drawing3D.Environment.Load(System.Environment.CurrentDirectory + "\\f.xml");
        }

     }
}
