using System;

namespace Lab1
{
    class Program
    {

        //2x^2 + 3x + 2   =   2 + x(3 + x(2))  =  ((2*x) + 3)*x + 2

        private static int Factorial(int value)
        {
            if (value == 1 || value == 0) return 1;
            int ans = 1;
            
            for (int i = 2; i <= value; ++i)
            {
                ans = ans * i;
            }

            return ans;
        }
        private static double Horner(double[] coef, int x0, int n)
        {
            double ans = coef[0];

            for (int i = 1; i < n; ++i)
            {
                ans = ans * x0 + coef[i];
            }

            return ans;
        }
        static void Main(string[] args)
        {
            int x;
            int n;

            Console.WriteLine("Enter degree of polynomial");
            n = int.Parse(Console.ReadLine()) + 1;


            double[] coef = new double[n];

            Console.WriteLine("Please enter {0} coefficients", n);
            for (int i = 0; i < n; ++i)
            {
                coef[i] = double.Parse(Console.ReadLine());
            }

            Console.WriteLine("Please enter x");
            x = int.Parse(Console.ReadLine());

            double ans = Horner(coef, x, n);
            Console.WriteLine("Answer is {0}", ans);

            //Console.WriteLine(Factorial(12));
        }

    }
}
