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
            Figure f2 = new Figure("Cube");
           
            f2.Vertexes.Add(new Vertex(1, 1, 1)); //0
            f2.Vertexes.Add(new Vertex(1, 5, 1)); //1
            f2.Vertexes.Add(new Vertex(5, 5, 5)); //2
            f2.Vertexes.Add(new Vertex(1, 5, 5)); //3
            f2.Vertexes.Add(new Vertex(5, 5, 1)); //4
            f2.Vertexes.Add(new Vertex(5, 1, 5)); //5
            f2.Vertexes.Add(new Vertex(5, 1, 1)); //6
            f2.Vertexes.Add(new Vertex(1, 1, 5)); //7
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
            E.Figures.Add(f2.Name, f2);
            //E.Transform(new Matrix(4), new Matrix(4), Matrix.GetTranslateMatrix(5, 0, 0));
            // E.Figures.Add(f2.Name+"1", f2);
            //E.Transform(new Matrix(4), new Matrix(4), Matrix.GetTranslateMatrix(0, 0, 0), new Matrix(4));


            Figure f =  new Figure("Gasya");
            f.Vertexes.Add(new Vertex(0, 0, 0));//0
            f.Vertexes.Add(new Vertex(0, 10, 0));//1
            f.Vertexes.Add(new Vertex(5, 10, 0));//2
            f.Vertexes.Add(new Vertex(5, 9, 0));//3
            f.Vertexes.Add(new Vertex(1, 9, 0));//4
            f.Vertexes.Add(new Vertex(1, 0, 0));//5

            f.Vertexes.Add(new Vertex(0, 0, 1));//6
            f.Vertexes.Add(new Vertex(0, 10, 1));//7
            f.Vertexes.Add(new Vertex(5, 10, 1));//8
            f.Vertexes.Add(new Vertex(5, 9, 1));//9
            f.Vertexes.Add(new Vertex(1, 9, 1));//10
            f.Vertexes.Add(new Vertex(1, 0, 1));//11

            f.AddLine(0, 1);
            f.AddLine(1, 2);
            f.AddLine(2, 3);
            f.AddLine(3, 4);
            f.AddLine(4, 5);
            f.AddLine(0, 5);

            f.AddLine(6, 7);
            f.AddLine(7, 8);
            f.AddLine(8, 9);
            f.AddLine(9, 10);
            f.AddLine(10, 11);
            f.AddLine(6, 11);

            f.AddLine(0, 6);
            f.AddLine(1, 7);
            f.AddLine(2, 8);
            f.AddLine(3, 9);
            f.AddLine(4, 10);
            f.AddLine(5, 11);

            E.AddFigure(f);


         


            Figure f3 = new Figure("Cords");
            f3.Vertexes.Add(new Vertex(0, 0, 0));
            f3.Vertexes.Add(new Vertex(10, 0, 0));
            f3.Vertexes.Add(new Vertex(0, 10, 0));
            f3.Vertexes.Add(new Vertex(0, 0, 10));
            f3.AddLine(0, 1);
            f3.AddLine(0, 2);
            f3.AddLine(0, 3);
            f3.Lines[0].BorderColor = "Red";      //x
            f3.Lines[1].BorderColor = "Blue";     //y
            f3.Lines[2].BorderColor = "Green";    //z
            E.Figures.Add(f3.Name, f3);



            E.Scale = 30;
     
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            E.Angle.OZ += 1;
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            E.Angle.OY += 1;
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            E.Angle.OX += 1;
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            E.Angle += new Transformation(trackBar3.Value, trackBar2.Value, trackBar1.Value);
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
