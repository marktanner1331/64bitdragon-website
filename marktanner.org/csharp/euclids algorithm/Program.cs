using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace euclids_algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 639;
            int b = 2343;

            Console.WriteLine("gcd({0}, {1}) == {2}", a, b, gcd(a, b));
            Console.Read();
        }

        static int gcd(int a, int b)
        {
            while(b != 0)
            {
                int r = a % b;
                a = b;
                b = r;
            }

            return a;
        }
    }
}
