using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace B
{
    // Implement here
    public interface IFormula<T>
    {
        public double Calculate(T x);
        public string PrintFormula();
    }


    public class CubicFormula : IFormula<double>
    {
        public double A {get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }

        public CubicFormula(double a, double b, double c, double d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public double Calculate(double x)
        {
            return A * Math.Pow(x, 3) + B * Math.Pow(x, 2) + C * x + D;
        }

        public string PrintFormula()
        {
            return $"{A}*x^3 + {B}*x^2 + {C}*x + {D}";
        }
    }

    static class CubicFormulaExtension 
    { 
        public static double CalculateDerivative(this CubicFormula c, double x)
        {
            return 3 * c.A * x * x + 2 * c.B * x + c.C;
        }
    }


    public class GeometricSequenceSumFormula : IFormula<uint>
    {
        public int A1 { get; set; }
        public int R { get; set; }

        public GeometricSequenceSumFormula(int a1, int r)
        {
            A1 = a1;
            R = r;
        }

        public double Calculate(uint x)
        {
            return A1 * (1 - Math.Pow(R, x)) / (1 - R);
        }

        public string PrintFormula()
        {
            return $"{A1}*(1-{R}^n)/(1-{R})";
        }
    }

    public class LucasGenerator<T> : IEnumerable
    {
        public IFormula<T> Formula { get; }

        public LucasGenerator(IFormula<T> ifo)
        {
            Formula = ifo;
        }

        public IEnumerator GetEnumerator()
        {
            return FibonacciGet().GetEnumerator();
        }

        public IEnumerable FibonacciGet()
        {
            dynamic l0 = 2, l1 = 1;
            while (true)
            {
                yield return Formula.Calculate(l0);
                int tmp = l0;
                l0 = l1;
                l1 = tmp + l1;
                
            }
        }
    }

}
