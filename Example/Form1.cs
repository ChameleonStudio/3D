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
            Graphics3D.Drawing3D.Environment.Save(System.Environment.CurrentDirectory + "\\f.xml", E);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            E = Graphics3D.Drawing3D.Environment.Load(System.Environment.CurrentDirectory + "\\f.xml");
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Figure f = new Figure();
            f.Points.Add(new Point3D(0,0,0));
            f.Points.Add(new Point3D(10, 0, 0));
            f.Points.Add(new Point3D(0, 10, 0));
            f.Points.Add(new Point3D(0, 0, 10));
            f.AddLine(0, 1);
            f.AddLine(0, 2);
            f.AddLine(0, 3);
            f.Lines[0].BorderColor = "Red";      //x
            f.Lines[1].BorderColor = "Blue";     //y
            f.Lines[2].BorderColor = "Green";    //z
            E.Figures.Add(f);

            Figure f2 = new Figure();

            f2.Points.Add(new Point3D(1, 1, 1)); //0
            f2.Points.Add(new Point3D(1, 5, 1)); //1
            f2.Points.Add(new Point3D(5, 5, 5)); //2
            f2.Points.Add(new Point3D(1, 5, 5)); //3
            f2.Points.Add(new Point3D(5, 5, 1)); //4
            f2.Points.Add(new Point3D(5, 1, 5)); //5
            f2.Points.Add(new Point3D(5, 1, 1)); //6
            f2.Points.Add(new Point3D(1, 1, 5)); //7
            f2.AddLine(0, 1);
            f2.AddLine(0, 6);
            f2.AddLine(0, 7);

            f2.AddLine(6, 5);
            f2.AddLine(6, 4);

            f2.AddLine(7, 5);
            f2.AddLine(7, 3);

            f2.AddLine(1, 3);
            f2.AddLine(1, 4);

            f2.AddLine(2, 4);
            f2.AddLine(2, 5);
            f2.AddLine(2, 3);
            E.Figures.Add(f2);
            E.Transform(Matrix.GetRotationMatrix(0, 0, 0), Matrix.GetScaleMatrix(30), new Matrix(4), new Matrix(4));
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            E.Transform(Matrix.GetRotationMatrix(0, 0, 1), new Matrix(4), new Matrix(4), new Matrix(4));
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            E.Transform(Matrix.GetRotationMatrix(0, 1, 0), new Matrix(4), new Matrix(4), new Matrix(4));
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            E.Transform(Matrix.GetRotationMatrix(1, 0, 0), new Matrix(4), new Matrix(4), new Matrix(4));
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            E.Transform(Matrix.GetRotationMatrix(trackBar3.Value, trackBar2.Value, trackBar1.Value), new Matrix(4), new Matrix(4), new Matrix(4));
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

     }
}
