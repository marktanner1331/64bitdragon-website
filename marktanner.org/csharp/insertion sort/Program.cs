using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insertion_sort
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
            List<int> sortedList = insertionSort(unsortedList);

            printArray(unsortedList.ToArray());
            printArray(sortedList.ToArray());

            Console.Read();
        }

        static List<T> insertionSort<T>(List<T> unsortedList) where T:IComparable
        {
            List<T> sortedList = new List<T>(unsortedList.Count);

            foreach (T randomT in unsortedList)
            {
                bool wasInserted = false;
                for (int i = 0; i < sortedList.Count; i++)
                {
                    if (sortedList[i].CompareTo(randomT) > 0)
                    {
                        sortedList.Insert(i, randomT);
                        wasInserted = true;
                        break;
                    }
                }

                //wouldnt need this if the list had sentinels.
                if (wasInserted == false)
                {
                    sortedList.Add(randomT);
                }
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
