using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selection_sort
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

            arr = knuthShuffle(arr);

            List<int> unsortedList = arr.ToList();
            List<int> sortedList = selectionSort(unsortedList);

            printArray(unsortedList.ToArray());
            printArray(sortedList.ToArray());

            Console.Read();
        }

        static List<T> selectionSort<T>(List<T> unsortedList) where T : IComparable
        {
            //clone
            unsortedList = unsortedList.ToList();

            List<T> sortedList = new List<T>(unsortedList.Count);

            while (unsortedList.Count > 0)
            {
                T biggest = unsortedList[0];
                int biggestIndex = 0;
                for (int i = 1; i < unsortedList.Count; i++)
                {
                    if (unsortedList[i].CompareTo(biggest) > 0)
                    {
                        biggest = unsortedList[i];
                        biggestIndex = i;
                    }
                }

                unsortedList.RemoveAt(biggestIndex);
                sortedList.Insert(0, biggest);
            }

            return sortedList;
        }

        static void printArray<T>(T[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i]);
                Console.Write(i < arr.Length - 1 ? ", " : "\n");
            }
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
