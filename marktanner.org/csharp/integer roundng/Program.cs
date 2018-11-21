using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integer_roundng
{
    class Program
    {
        /// <summary>
        /// we have the problem "given the number X, find the multiple of Z that is closest to X.
        /// in the past i would use floats, but a simpler solution is possible using nothing but integers
        /// </summary>
        static void Main(string[] args)
        {
            for (int i = 1; i < 50; i++)
            {
                for (int j = 1; j < 50; j++)
                {
                    int a = oldSolution(i, j);
                    int b = newSolution(i, j);

                    if (a != b)
                    {
                        throw new Exception("something bad happened");
                    }
                }
            }
        }

        static int newSolution(int x, int z)
        {
            int mod = x % z;
            if (2 * mod < z)
            {
                return x - mod;
            }
            else
            {
                return x + z - mod;
            }
        }

        static int oldSolution(int x, int z)
        {
            double a = (double)x / z;
            a = Math.Round(a, MidpointRounding.AwayFromZero);

            return (int)(a * z);
        }
    }
}
