using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_heap
{
    //implementation of a min-heap
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = {40, 68, 27, 16, 79, 84, 36, 69, 29, 47};
            BinaryHeap heap = new BinaryHeap(values);

            while (heap.size > 1)
            {
                Console.Write(heap.deleteMin() + ", ");
            }

            Console.Write(heap.deleteMin());

            Console.Read();
        }
    }
}
