using System;
using System.Collections.Generic;

namespace LAB_2021_CS_10B
{
    //Stage 2 - 1.5 points
    public static class BaseFunctions
    {
        public static Func<double, double> ConstantFunction(double d)
        {
            return delegate(double a) { return d; };
        }
        public static Func<double, double> ModulusFunction(double a, double b)
        {
            return delegate (double x) { return Math.Abs(a * x + b); };
        }
        public static Func<double, double> PolynomialFunction(params double[] tab)
        {
            return delegate (double x)
            {
                double result = tab[0];
                for (int i = 1; i < tab.GetLength(0); i++)
                {
                    result = result * x + tab[i];
                }
                return result;
            };
        }
    }

    //Stage 3 - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, double> ChooseFunction(Func<double, double> f, Func<double, double> g)
        {
            return x => x%1 > 0.5 ? f(x) : g(x);
        } 
        public static Func<double, double> MultiplyFunctions(Func<double, double> f, Func<double, double> g)
        {
            return x => f(x) * g(x);
        }

        public static Func<double, double> CombineFunctions(Func<double, double> f, Func<double, double> g)
        {
            return x => f(g(x));
        }
    }

    //Stage 4 - 1.5 points
    public static class ExtensionMethods
    {
        public static void PerformAction(this IEnumerable<Person> ie, Action<Person, int> act)
        {
            int i = 0;
            foreach (Person p in ie) act(p, i++);
        }

        public static double Accumulate(this IEnumerable<Person> ie, Func<Person, double, double> acc, double sum)
        {
            foreach (Person p in ie) sum = acc(p, sum);
            return sum;
        }
    }
}