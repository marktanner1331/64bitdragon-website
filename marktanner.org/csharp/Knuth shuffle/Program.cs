using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuth_shuffle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                Console.Write(i < arr.Length - 1 ? ", " : "\n");
            }

            arr = knuthShuffle(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                Console.Write(i < arr.Length - 1 ? ", " : "\n");
            }

            Console.Read();
        }

        static T[] knuthShuffle<T>(T[] source)
        {
            source = (T[])source.Clone();
            Random rand = new Random();

            for (int i = 0; i < source.Length; i++)
            {
                int j = rand.Next(i, source.Length);

                //exchange the chars at i and j
                T temp = source[i];
                source[i] = source[j];
                source[j] = temp;
            }

            return source;
        }
    }
}
