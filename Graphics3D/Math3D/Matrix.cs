using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graphics3D.Drawing3D;

namespace Graphics3D.Math3D
{
    public class Matrix
    {
        int width, height;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public Matrix()
        {
            this.width = 0;
            this.height = 0;
        }

        public Matrix(int height, int width)
        {
            this.width = width;
            this.height = height;
            matrix = new double[height, width];
        }

        public Matrix(int size)
        {
            this.width = size;
            this.height = size;
            matrix = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                matrix[i, i] = 1;
            }
        }

        double[,] matrix;

        public static implicit operator Matrix(double[,] a)
        {
            Matrix m = new Matrix(a.GetLength(0), a.GetLength(1));
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    m[i, j] = a[i, j];
                }
            }
            return m;
        }

        public double this[int row, int col]
        {
            get { return matrix[row, col]; }
            set { matrix[row, col] = value; }
        }

        public static Matrix operator *(double a, Matrix m)
        {
            for (int i = 0; i < m.Width; i++)
            {
                for (int j = 0; j < m.Height; j++)
                {
                    m[i, j] *= a;
                }
            }
            return m;

        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix m;
            if (m1.Width == m2.Height)
            {
                m = new Matrix(m1.Height, m2.Width);
                for (int i = 0; i < m.Width; i++)
                {
                    for (int j = 0; j < m.Height; j++)
                    {
                        double sum = 0;
                        for (int q = 0; q < m1.Width; q++)
                        {
                            sum += m1[j, q] * m2[q, i];
                        }
                        m[j, i] = sum;
                    }
                }
            }
            else
            {
                throw new Exception("Erorr! Inmultiplible matrix.");
            }
            return m;
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (m1.Height != m2.Height || m1.Width != m2.Height)
                return false;
            for (int i = 0; i < m1.Width; i++)
                for (int j = 0; j < m1.Height; j++)
                    if (m1[i, j] != m2[1, j])
                        return false;
            return true;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            if (m1.Height == m2.Height && m1.Width == m2.Height)
                return false;
            bool ans = true;
            for (int i = 0; i < m1.Width; i++)
                for (int j = 0; j < m1.Height; j++)
                    ans = ans && (m1[i, j] == m2[1, j]);
            return !ans;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    s.Append(matrix[i, j] + "\t");
                }
                s.Append("\n");
            }
            return s.ToString();
        }

        public static Matrix GetRotationMatrix(Transformation transformation)
        {
            double ar =ToRadian(transformation.OZ);
            double br=ToRadian(transformation.OY);
            double gr=ToRadian(transformation.OX);

            Matrix m = new double[,]{
            {Math.Cos(ar)*Math.Cos(br),                                          -Math.Cos(br)*Math.Sin(ar),                                          -Math.Sin(br),               0},
            {Math.Cos(ar)*Math.Sin(gr)*Math.Sin(br) + Math.Sin(ar)*Math.Cos(gr), -Math.Sin(ar)*Math.Sin(gr)*Math.Sin(br) + Math.Cos(ar)*Math.Cos(gr),  Math.Sin(gr)*Math.Cos(br),  0},
            {Math.Cos(ar)*Math.Cos(gr)*Math.Sin(br) - Math.Sin(ar)*Math.Sin(gr), -Math.Sin(ar)*Math.Sin(br)*Math.Cos(gr) - Math.Cos(ar)*Math.Sin(gr),  Math.Cos(gr)*Math.Cos(br),  0},
            {0,                                                                   0,                                                                   0,                          1}
            };

            return m;
        }

        public static Matrix GetRotationOZMatrix(double a)
        {
            a = ToRadian(a);
            Matrix m = new double[,]{
            {Math.Cos(a), -Math.Sin(a), 0, 0},
            {Math.Sin(a), Math.Cos(a), 0, 0},
            {0, 0, 1, 0},
            {0, 0, 0, 1}
            };
            return m;
        }

        public static Matrix GetRotationOYMatrix(double a)
        {
            a = ToRadian(a);
            Matrix m = new double[,]{
            {Math.Cos(a), 0, -Math.Sin(a), 0},
            {0, 1, 0, 0},
            {Math.Sin(a),0, Math.Cos(a), 0},
            {0, 0, 0, 1}
            };
            return m;
        }

        public static Matrix GetRotationOXMatrix(double a)
        {
            a = ToRadian(a);
            Matrix m = new double[,]{
            {1,0, 0, 0},
            {0,Math.Cos(a), 0,Math.Sin(a)},
            {0,-Math.Sin(a),0, Math.Cos(a)},
            {0, 0, 0, 1}
            };
            return m;
        }


        public static Matrix GetScaleMatrix(Transformation transformation)
        {
            Matrix m = new double[,]{
            {transformation.OX, 0, 0, 0},
            {0, transformation.OY, 0, 0},
            {0, 0, transformation.OZ, 0},
            {0, 0, 0, 1}
            };
            return m;
        }

        public static Matrix GetScaleMatrix(double s)
        {
            Matrix m = new double[,]{
            {s, 0, 0, 0},
            {0, s, 0, 0},
            {0, 0, s, 0},
            {0, 0, 0, 1}
            };
            return m;
        }

        public static Matrix GetTranslateMatrix(Transformation transformation)
        {
            Matrix m = new double[,]{
            {1, 0, 0, 0},
            {0, 1, 0, 0},
            {0, 0, 1, 0},
            {transformation.OX, transformation.OY, transformation.OZ, 1}
            };
            return m;
        }

        private static double ToRadian(double a)
        {
            return a * Math.PI / 180;
        }
    }
}
