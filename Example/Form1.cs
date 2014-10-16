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
            saveFileDialog1.ShowDialog();
            Graphics3D.Drawing3D.Environment.Save(saveFileDialog1.FileName, E);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            E = Graphics3D.Drawing3D.Environment.Load(openFileDialog1.FileName);
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
            trackBar4.Value = (int)E.Scale.OX;
            ListUpdate();
            trackBar4.BackColor = E.BackgroundColor;
            checkBox3.Checked = !E.Figures["Axis"].Hidden;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (SelectFigure != "")
            {
                if (radioButton1.Checked)
                {
                    if (checkBox2.Checked)
                        E.Figures[SelectFigure].RelativeRotate(new Transformation((double)trackBar3.Value / 18, (double)trackBar2.Value / 18, (double)trackBar1.Value / 18));
                    else
                        E.Figures[SelectFigure].Rotate(new Transformation((double)trackBar3.Value / 18, (double)trackBar2.Value / 18, (double)trackBar1.Value / 18));
                }
                else if (radioButton2.Checked)
                {
                        E.Figures[SelectFigure].Translate(new Transformation((double)trackBar3.Value / 72, (double)trackBar2.Value / 72, (double)trackBar1.Value / 72));
                }
                else if (radioButton3.Checked)
                {
                    if (!checkBox2.Checked)
                        if (!checkBox1.Checked)
                            E.Figures[SelectFigure].Scale(new Transformation(((double)trackBar3.Value / 1000 + 1), ((double)trackBar2.Value / 1000 + 1), ((double)trackBar1.Value / 1000 + 1)));
                        else
                            E.Figures[SelectFigure].Scale(new Transformation(((double)trackBar1.Value / 1000 + 1), ((double)trackBar1.Value / 1000 + 1), ((double)trackBar1.Value / 1000 + 1)));
                    else
                        if (!checkBox1.Checked)
                            E.Figures[SelectFigure].RelativeScale(new Transformation(((double)trackBar3.Value / 1000 + 1), ((double)trackBar2.Value / 1000 + 1), ((double)trackBar1.Value / 1000 + 1)));
                        else
                            E.Figures[SelectFigure].RelativeScale(new Transformation(((double)trackBar1.Value / 1000 + 1), ((double)trackBar1.Value / 1000 + 1), ((double)trackBar1.Value / 1000 + 1)));
                }
                
            }
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Figure axis = Figures.Axis();
            E.AddFigure(axis);

            Figure o = Figures.O();
            E.AddFigure(o);


            E.Scale = trackBar4.Value;
            pictureBox1.Image = E.GetImage(pictureBox1.Width, pictureBox1.Height);
            ListUpdate();
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
                        pictureBox2.BackColor = F.GetBorderColor();
                        break;
                    }
                    else
                    {
                        F.Selected = false;
                    }
                }
                if(SelectFigure == "") pictureBox2.BackColor = E.BackgroundColor;
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

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as TrackBar).Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Figure f = null;
            Random r = new Random();
            Color c = Color.FromArgb(255,r.Next(255),r.Next(255),r.Next(255));
            switch(comboBox1.Text)
            {
                case "O": f = Figures.O(textBox1.Text); break;
                case "Sphere": f = Figures.Sphere(textBox1.Text, c,(int)numericUpDown1.Value); break;
                case "Cone": f = Figures.Cone(textBox1.Text, c, (int)numericUpDown1.Value); break;
                case "Cube": f = Figures.Cube(textBox1.Text, c); break;
                case "Tube": f = Figures.Tube(textBox1.Text, c, (int)numericUpDown1.Value); break;
                case "Cylinder": f = Figures.Cylinder(textBox1.Text, c, (int)numericUpDown1.Value); break;
                case "Torus": f = Figures.Torus(textBox1.Text, c, (int)numericUpDown1.Value); break;
                case "Circle": f = Figures.Circle(textBox1.Text, c, (int)numericUpDown1.Value); break;
            }
            if (f != null)
            {
                E.AddFigure(f);
                SelectFigure = f.Name;
                foreach (Figure figure in Enumerable.Where(E.Figures.Values, figure => figure.Selectable == true))
                    figure.Selected = false;
                if (SelectFigure != "")
                    E.Figures[SelectFigure].Selected = true;
            }

            namecount++;
            textBox1.Text = "DefaultName " + namecount.ToString();
            ListUpdate();
        }
        int namecount = 0;

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                E.Figures.Remove(SelectFigure);
                SelectFigure = "";
                ListUpdate();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = pictureBox2.BackColor;
            colorDialog1.ShowDialog();
            if (SelectFigure != "")
                E.Figures[SelectFigure].SetBorderColor(colorDialog1.Color);
            else
                E.BackgroundColor = colorDialog1.Color;

            trackBar4.BackColor = E.BackgroundColor;
            pictureBox2.BackColor = colorDialog1.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SelectFigure != "")
                E.Figures[SelectFigure].TranslateToCenter();
        }

        void ListUpdate()
        {
            listBox1.Items.Clear();
            foreach (Figure f in Enumerable.Where(E.Figures.Values, f => f.Selectable == true))
            {
                listBox1.Items.Add(f.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ListBox).SelectedItem != null)
                SelectFigure = (sender as ListBox).SelectedItem.ToString();
            else
                SelectFigure = "";

            foreach (Figure f in Enumerable.Where(E.Figures.Values, f => f.Selectable == true))
                f.Selected = false;
            if (SelectFigure != "")
                E.Figures[SelectFigure].Selected = true;

            
            if (SelectFigure == "") pictureBox2.BackColor = E.BackgroundColor;
            else pictureBox2.BackColor = E.Figures[SelectFigure].GetBorderColor();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            E.Figures["Axis"].Hidden = !checkBox3.Checked;
        }


        

     }
}
