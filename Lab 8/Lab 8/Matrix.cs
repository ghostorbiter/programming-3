using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab08B
{
    class Matrix
    {
        private double[][] values;
        public int Rows
        {
            get; set;
        }
        public int Columns
        {
            get; set;
        }

        public Matrix(int r, int c)
        {
            Rows = r;
            Columns = c;
            values = new double[r][];
            for (int i = 0; i < r; ++i)
            {
                values[i] = new double[c];
            }
        }

        public Matrix(double[][] vals)
        {
            int r = vals.GetLength(0);
            Rows = r;
            int c = vals[0].GetLength(0);
            Columns = c;
            values = new double[r][];
            for (int i = 0; i < r; ++i)
            {
                c = vals[i].GetLength(0);
                values[i] = new double[c];
                vals[i].CopyTo(values[i], 0);
            }
        }

        public bool IsSymmetric
        {
            get
            {
                if (Rows != Columns)
                    return false;
                for (int i = 0; i < Rows; ++i)
                {
                    for (int j = 0; j < Columns; ++j)
                    {
                        if (values[i][j] != values[j][i])
                            return false;
                    }
                }
                return true;
            }
        }

        public void Print()
        {
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    Console.Write("{0, 4}   ", values[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static explicit operator double[][](Matrix m)
        {
            double[][] db = new double[m.Rows][];
            for (int i = 0; i < m.Rows; ++i)
            {
                db[i] = new double[m.Columns];
                for (int j = 0; j < m.Columns; ++j)
                {
                    db[i][j] = m.values[i][j];
                }
            }
            return db;
        }

        public static implicit operator Matrix(double[][] db)
        {
            return new Matrix(db);
        }

        public static explicit operator double(Matrix m)
        {
            double ans = 0;
            for (int i = 0; i < m.Rows; ++i)
            {
                for (int j = 0; j < m.Columns; ++j)
                {
                    ans += m.values[i][j];
                }
            }
            return ans;
        }

        public static double[] operator -(Matrix m)
        {
            int mini = Math.Min(m.Rows, m.Columns);
            double[] db = new double[mini];
            for (int i = 0; i < mini; ++i)
            {
                db[i] = m.values[i][i];
            }
            return db;
        }

        public static Matrix operator +(Matrix m, double d)
        {
            Matrix m_copy = new Matrix(m.values);
            for (int i = 0; i < m.Rows; ++i)
            {
                for (int j = 0; j < m.Columns; ++j)
                {
                    m_copy.values[i][j] += d;
                }
            }
            return m_copy;
        }

        public double this[int c, int r]
        {
            get
            {
                if (r > Rows || c > Columns || r < 0 || c < 0)
                    throw new Exception("Improper indexing");
                return values[r][c];
            }
            set
            {
                if (r > Rows || c > Columns || r < 0 || c < 0)
                    throw new Exception("Improper indexing");
                values[r][c] = value;
            }
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                return false;
            for (int i = 0; i < m1.Rows; ++i)
            {
                for (int j = 0; j < m1.Columns; ++j)
                {
                    if (m1.values[i][j] != m2.values[i][j])
                        return false;
                }
            }
            return true;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Matrix))
                return false;
            var p = (Matrix)obj;
            return p == this;
        }

        public override int GetHashCode()
        {
            double d = (double)this;
            return this.Columns.GetHashCode() ^ this.Rows.GetHashCode() ^ d.GetHashCode();
        }

    }
}
