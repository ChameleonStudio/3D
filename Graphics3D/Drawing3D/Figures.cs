using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Graphics3D.Drawing3D
{
    public class Figures
    {
        public static Figure Axis()
        {
            Figure f3 = new Figure("Axis");
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
            f3.Selectable = false;
            return f3;
        }

        public static Figure O(String name = "O")
        {
            Figure Letter = new Figure(name);
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
            return Letter;
        }

        public static Figure Cube(String name, Color color)
        {
            Figure f = new Figure(name);
            f.Vertexes.Add(new Vertex(1, 1, 1)); //0
            f.Vertexes.Add(new Vertex(1, -1, 1)); //1
            f.Vertexes.Add(new Vertex(-1, -1, 1)); //2
            f.Vertexes.Add(new Vertex(-1, 1, 1)); //3
            f.Vertexes.Add(new Vertex(1, 1, -1)); //4
            f.Vertexes.Add(new Vertex(1, -1, -1)); //5
            f.Vertexes.Add(new Vertex(-1, -1, -1)); //6
            f.Vertexes.Add(new Vertex(-1, 1, -1)); //7
            f.AddLine(0, 1);
            f.AddLine(1, 2);
            f.AddLine(2, 3);
            f.AddLine(3, 0);
            f.AddTriangle(1, 0, 2);
            f.AddTriangle(0, 3, 2);
            f.AddTriangle(0, 1, 5);
            f.AddTriangle(4, 0, 5);
            f.AddLine(4, 5);
            f.AddLine(5, 6);
            f.AddLine(6, 7);
            f.AddLine(7, 4);
            f.AddTriangle(4, 5, 6);
            f.AddTriangle(7, 4, 6);
            f.AddTriangle(2, 3, 6);
            f.AddTriangle(3, 7, 6);
            f.AddLine(0, 4);
            f.AddLine(1, 5);
            f.AddLine(2, 6);
            f.AddLine(3, 7);
            f.AddTriangle(7, 3, 0);
            f.AddTriangle(4, 7, 0);
            f.AddTriangle(1, 2, 6);
            f.AddTriangle(5, 1, 6);

            foreach (Line l in f.Lines)
                l.BorderColor = color;

            foreach (Triangle t in f.Triangles)
                t.FillColor = color;
            return f;
        }

        public static Figure Circle(String name, Color color, int count = 30)
        {
            Figure f = new Figure(name);
            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(Math.Sin(Math.PI * 2 * i / count), 0, Math.Cos(Math.PI * 2 * i / count)));
            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, (i + 1) % count);
                f.Lines[i].BorderColor = color;
            }
            return f;
        }

        public static Figure Cone(String name, Color color, int count = 30)
        {
            Figure f = new Figure(name);
            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(Math.Sin(Math.PI * 2 * i / count), 0, Math.Cos(Math.PI * 2 * i / count)));

            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, (i + 1) % count);
                f.Lines[i].BorderColor = color;
                f.AddTriangle(i,0, (i + 1) % count);
               
            }
            f.Vertexes.Add(new Vertex(0, 2, 0));
            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, count);
                f.Lines.Last().BorderColor = color;
                f.AddTriangle(count, i, (i + 1) % count);
               
            }
            f.Vertexes.Add(new Vertex(0, 0, 0));
            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, count + 1);
                f.Lines.Last().BorderColor = color;
            }

            foreach (Triangle t in f.Triangles)
                t.FillColor = color;
            return f;
        }

        public static Figure Cylinder(String name, Color color, int count = 30)
        {
            Figure f = new Figure(name);
            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(Math.Sin(Math.PI * 2 * i / count), 0, Math.Cos(Math.PI * 2 * i / count)));
            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, (i + 1) % count);
               
                f.Lines[i].BorderColor = color;
            }

            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(Math.Sin(Math.PI * 2 * i / count), 2, Math.Cos(Math.PI * 2 * i / count)));
            for (int i = count; i < count * 2; i++)
            {
                f.AddLine(i, count + (i + 1) % count);
                f.AddTriangle(count + (i + 1) % count, i, (i + 1) % count);
                f.Lines.Last().BorderColor = color;
            }


            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, i + count);
                f.AddTriangle(count + i, i, (i + 1) % count);
                f.Lines.Last().BorderColor = color;
            }


            f.Vertexes.Add(new Vertex(0, 0, 0));
            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, count * 2);
                f.Lines.Last().BorderColor = color;
                f.AddTriangle(i, count * 2, (i + 1) % count);
            }

            f.Vertexes.Add(new Vertex(0, 2, 0));
            for (int i = 0; i < count; i++)
            {
                f.AddLine(count + i, count * 2 + 1);
                f.AddTriangle(count * 2 + 1,count + i, count + (i + 1) % count);
                f.Lines.Last().BorderColor = color;
            }
            foreach (Triangle t in f.Triangles)
                t.FillColor = color;
            return f;
        }
       
        public static Figure Sphere(String name, Color color, int count = 30)
        {
            Figure f = new Figure(name);
       
            for (int j = 1; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                    f.Vertexes.Add(new Vertex(Math.Sin(Math.PI * j / count) * Math.Sin(Math.PI * 2 * i / count), Math.Cos(Math.PI * j / count), Math.Sin(Math.PI * j / count) * Math.Cos(Math.PI * 2 * i / count)));
                for (int i = (j - 1) * count; i < j * count; i++)
                {
                    f.AddLine(i, (j - 1) * count + (i + 1) % count);
                    f.Lines.Last().BorderColor = color;
                    
                    
                    if (j != 1)
                    {
                        f.AddTriangle(i, (j - 1) * count + (i + 1) % count, i - count);

                        f.AddTriangle((j - 1) * count + (i + 1) % count - count, i - count, (j - 1) * count + (i + 1) % count);

                        f.AddLine(i - count, i);
                        f.Lines.Last().BorderColor = color;
                    }
                }
            }

            f.Vertexes.Add(new Vertex(0, 1, 0));
            f.Vertexes.Add(new Vertex(0, -1, 0));

            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, count * (count - 1));   
                f.Lines.Last().BorderColor = color;

                f.AddTriangle((i + 1) % count, count * (count - 1), i);
            }

            for (int i = count * (count - 2); i < count * (count - 1); i++)
            {
                f.AddLine(i, count * (count - 1) + 1);

                f.AddTriangle(count * (count - 2) + (i + 1) % count, i, count * (count - 1) + 1);
                f.Lines.Last().BorderColor = color;
            }
            foreach (Triangle t in f.Triangles)
                t.FillColor = color;
            return f;
        }

        public static Figure Torus(String name, Color color, int count = 30, double radius = 2)
        {
            Random a = new Random();
            Figure f = new Figure(name);
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < count; i++)
                    f.Vertexes.Add(new Vertex((Math.Sin(Math.PI * 2 * j / count) + radius) * Math.Sin(Math.PI * 2 * i / count), Math.Cos(Math.PI * 2 * j / count), (Math.Sin(Math.PI * 2 * j / count) + radius) * Math.Cos(Math.PI * 2 * i / count)));
                for (int i = j * count; i < (j+1) * count; i++)
                {
                    f.AddLine(i, j * count + (i + 1) % count);
                    f.Lines.Last().BorderColor = color;
                    if (j != 0)
                    {
                        f.AddTriangle(i, j * count + (i + 1) % count, i - count);
                        f.AddTriangle(j * count + (i + 1) % count - count, i - count, j * count + (i + 1) % count);
                        f.AddLine(i - count, i);
                        f.Lines.Last().BorderColor = color;
                    }
                }
            }
            for (int i = 0; i < count; i++)
            {

                f.AddTriangle(i + count * (count - 1), i, (i + 1) % count);

                f.AddTriangle((i + 1) % count + count * (count - 1), i + count * (count - 1), (i + 1) % count);

                f.AddLine(i, i + count * (count - 1));
                f.Lines.Last().BorderColor = color;
            }
            foreach (Triangle t in f.Triangles)
                t.FillColor = color;
            return f;
        }

        public static Figure Tube(String name, Color color, int count = 30,double radius = 0.75)
        {
            Random a = new Random();
            Figure f = new Figure(name);
            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(Math.Sin(Math.PI * 2 * i / count), 0, Math.Cos(Math.PI * 2 * i / count)));
            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, (i + 1) % count);
                f.Lines[i].BorderColor = color;
            }

            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(radius * Math.Sin(Math.PI * 2 * i / count), 0, radius*Math.Cos(Math.PI * 2 * i / count)));
            for (int i = count; i < count * 2; i++)
            {
                f.AddLine(i, count + (i + 1) % count);
                f.Lines.Last().BorderColor = color;
            }


            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, i + count);
                f.AddTriangle((i + 1) % count,i, count + (i + 1) % count);

                f.AddTriangle(count + (i + 1) % count,i, count + i);
           
                f.Lines.Last().BorderColor = color;
            }


            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(Math.Sin(Math.PI * 2 * i / count), 2, Math.Cos(Math.PI * 2 * i / count)));
            for (int i = count*2; i < count * 3; i++)
            {
                f.AddLine(i, count*2 + (i + 1) % count);
                f.Lines.Last().BorderColor = color;
            }

            for (int i = 0; i < count; i++)
                f.Vertexes.Add(new Vertex(radius * Math.Sin(Math.PI * 2 * i / count), 2, radius * Math.Cos(Math.PI * 2 * i / count)));
            for (int i = count * 3; i < count * 4; i++)
            {
                f.AddLine(i, count * 3 + (i + 1) % count);
                f.Lines.Last().BorderColor = color;

                f.AddTriangle(       count * 3 + (i + 1) % count, i, i - count);
                f.AddTriangle(count * 2 + (i + 1) % count,count * 3 + (i + 1) % count, i - count);
            }


            for (int i = count * 2; i < count * 3; i++)
            {
                f.AddLine(i, i + count);
                f.Lines.Last().BorderColor = color;


                f.AddTriangle(count * 2 + (i + 1) % count,i, i - count*2);
                f.AddTriangle((i + 1) % count,count * 2 + (i + 1) % count, i - count*2);
                
              
            }

            for (int i = 0; i < count; i++)
            {
                f.AddLine(i, i + count*2);
                f.Lines.Last().BorderColor = color;
            }

            for (int i = count; i < count*2; i++)
            {
                f.AddLine(i, i + count * 2);
                f.Lines.Last().BorderColor = color;
                f.AddTriangle(i + count*2, count*3 + (i + 1) % count, i);
                f.AddTriangle(count * 3 + (i + 1) % count,count+(i+1)%count, i);
            }
            foreach (Triangle t in f.Triangles)
                t.FillColor = color;
            return f;
        }
    }
}
