using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fast_square_root
{
    /// <summary>
    /// comparing this method to the 'proper' methods, its really slow, but i tried.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (uint i = 0; i < 10 * 1000 * 1000; i++)
            {
                uint root = squareRoot(i);
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            Console.Read();
        }

        //performs the same as Math.Floor(Math.Sqrt(num));
        static uint squareRoot(uint num)
        {
            //moving this after the if statement should be faster, but its not, 
            //probably something to do with parallelism
            uint numBits = countBits(num);

            //for this method to work, the number needs to be at least 3 bits
            if (num < 4)
            {
                if (num == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
             
            uint numBitsInRoot = (numBits >> 1) + (numBits & 1);

            uint firstUnknownBitIndex = numBitsInRoot - 2;

            uint guessForRoot = (uint)1 << (int)(numBitsInRoot - 1);
            
            while (true)
            {
                uint temp = (uint)(guessForRoot + (1 << (int)firstUnknownBitIndex));

                if (temp * temp <= num)
                {
                    guessForRoot = temp;
                }

                if (firstUnknownBitIndex == 0)
                {
                    return guessForRoot;
                }

                firstUnknownBitIndex--;
            }
        }

        static uint countBits(uint num)
        {
            uint r = 0;

            while (num != 0)
            {
                r++;
                num >>= 1;
            }

            return r;
        }
    }
}