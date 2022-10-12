using System;
using System.Collections;

namespace EN_Lab_07
{
    class Fibonacci
    {
        private int n;

        public Fibonacci(int _n)
        {
            n = _n;
        }

        public IEnumerator GetEnumerator()
        {
            return FibonacciGet().GetEnumerator();
        }
        public IEnumerable FibonacciGet()
        {
            int prev = 1, prev_prev = 1;
             for (int i = 0; i < n; i++)
            {
                if (i == 0 || i == 1)
                    yield return 1;
                else
                {
                    yield return prev + prev_prev;
                    int tmp = prev_prev;
                    prev_prev = prev;
                    prev = tmp + prev_prev;
                }
            }
            yield break;
        }
    }
}
