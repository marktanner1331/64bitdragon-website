using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fast_exponentiation
{
    class Program
    {
        static void Main(string[] args)
        {
            //calculate x to the power of y mod n
            uint x = 5;
            uint y = 7;
            uint n = 11;

            Console.WriteLine(fastExponentiation(x, y, n));
            Console.Read();
        }

        /// <summary>
        /// this method is fast because it spits the operation into as many parts as possible, and modding at every available chance
        /// </summary>
        /// <param name="x">the base</param>
        /// <param name="y">the index</param>
        /// <param name="n">the modulus</param>
        /// <returns></returns>
        static uint fastExponentiation(uint x, uint y, uint n)
        {
            uint temp;

            if(y == 1)
            {
                return x % n;
            }

            //is it even?
            if ((y & 1) == 0)
            {
                //if y is even, we can reduce it to the square of x to the power of half of y
                temp = fastExponentiation(x, y / 2, n);
                return (temp * temp) % n;
            }

            //its odd
            //we treat it as if its even, but then multiply it by an extra x to balance the y - 1
            temp = fastExponentiation(x, (y - 1) / 2, n);
            temp = (temp * temp) % n;
            temp = (temp * x) % n;

            return temp;
        }
    }
}
