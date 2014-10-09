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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SelectFigure != "")
            {
                E.Figures[SelectFigure].Rotate(new Transformation(trackBar3.Value, trackBar2.Value, trackBar1.Value));
            }
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
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
            //E.Figures.Add(f2.Name, f2);


            Figure Letter = new Figure("O");
            Letter.Vertexes.Add(new Vertex(2, 3, 0.5)); //0
            Letter.Vertexes.Add(new Vertex(2, -3, 0.5)); //1
            Letter.Vertexes.Add(new Vertex(-2, -3, 0.5)); //2
            Letter.Vertexes.Add(new Vertex(-2, 3, 0.5)); //3

            Letter.Vertexes.Add(new Vertex(1, 2, 0.5)); //4
            Letter.Vertexes.Add(new Vertex(1, -2, 0.5)); //5
            Letter.Vertexes.Add(new Vertex(-1, -2, 0.5)); //6
            Letter.Vertexes.Add(new Vertex(-1, 2, 0.5)); //7

            Letter.Vertexes.Add(new Vertex(2, 3, -0.5)); //8
            Letter.Vertexes.Add(new Vertex(2, -3, -0.5)); //9
            Letter.Vertexes.Add(new Vertex(-2, -3, -0.5)); //10
            Letter.Vertexes.Add(new Vertex(-2, 3, -0.5)); //11

            Letter.Vertexes.Add(new Vertex(1, 2, -0.5)); //12
            Letter.Vertexes.Add(new Vertex(1, -2, -0.5)); //13
            Letter.Vertexes.Add(new Vertex(-1, -2, -0.5)); //14
            Letter.Vertexes.Add(new Vertex(-1, 2, -0.5)); //15

            Letter.AddLine(0, 1);
            Letter.AddLine(1, 2);
            Letter.AddLine(2, 3);
            Letter.AddLine(3, 0);

            Letter.AddLine(4, 5);
            Letter.AddLine(5, 6);
            Letter.AddLine(6, 7);
            Letter.AddLine(7, 4);

            Letter.AddLine(8, 9);
            Letter.AddLine(9, 10);
            Letter.AddLine(10, 11);
            Letter.AddLine(11, 8);

            Letter.AddLine(12, 13);
            Letter.AddLine(13, 14);
            Letter.AddLine(14, 15);
            Letter.AddLine(15, 12);


            Letter.AddLine(0, 8);
            Letter.AddLine(1, 9);
            Letter.AddLine(2, 10);
            Letter.AddLine(3, 11);

            Letter.AddLine(4, 12);
            Letter.AddLine(5, 13);
            Letter.AddLine(6, 14);
            Letter.AddLine(7, 15);
            E.Figures.Add(Letter.Name, Letter);


            Figure f3 = new Figure("Cords");
            f3.Vertexes.Add(new Vertex(0, 0, 0));
            f3.Vertexes.Add(new Vertex(10, 0, 0));
            f3.Vertexes.Add(new Vertex(0, 10, 0));
            f3.Vertexes.Add(new Vertex(0, 0, 10));

            f3.Vertexes.Add(new Vertex(-10, 0, 0));
            f3.Vertexes.Add(new Vertex(0, -10, 0));
            f3.Vertexes.Add(new Vertex(0, 0, -10));

            f3.AddLine(0, 1);
            f3.AddLine(0, 2);
            f3.AddLine(0, 3);

            f3.AddLine(0, 4);
            f3.AddLine(0, 5);
            f3.AddLine(0, 6);

            f3.Lines[0].BorderColor = Color.Red;      //x
            f3.Lines[1].BorderColor = Color.Blue;     //y
            f3.Lines[2].BorderColor = Color.Green;    //z

            f3.Lines[3].BorderColor = Color.Red;      //x
            f3.Lines[4].BorderColor = Color.Blue;     //y
            f3.Lines[5].BorderColor = Color.Green;    //z

            f3.Lines[3].Type = System.Drawing.Drawing2D.DashStyle.Dash;      //x
            f3.Lines[4].Type = System.Drawing.Drawing2D.DashStyle.Dash;     //y
            f3.Lines[5].Type = System.Drawing.Drawing2D.DashStyle.Dash;    //z
            E.Figures.Add(f3.Name, f3);



            E.Scale = 30;

            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
            //propertyGrid1.SelectedObject = f2;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        bool canRotate = false;
        Point start;
        double OYStart,OXStart;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                canRotate = true;
                start = new Point(e.X, e.Y);
                OYStart = E.Angle.OY;
                OXStart = E.Angle.OX;
            }
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                SelectFigure = "";
                Figure f = E.CheckFigure(new Point2D(e.X, e.Y));
                foreach (Figure F in E.Figures.Values)
                {
                    if (ReferenceEquals(f, F))
                    {
                        F.Selected = true;
                        SelectFigure = F.Name;
                    }
                    else
                    {
                        F.Selected = false;
                    }
                }
            }
        }

        String SelectFigure = "";

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (canRotate)
            {
                E.Angle.OY = OYStart + (start.X - e.X)/2;
                E.Angle.OX = OXStart + (start.Y - e.Y)/2;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            canRotate = false;

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            E.Scale = trackBar4.Value;
        }


     }
}
