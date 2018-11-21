using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triangular_array
{
    class Program
    {
        static void Main(string[] args)
        {
            TriangularArray<int> arr = new TriangularArray<int>(5);

            arr.setElementAt(2, 2, 1);

            Console.WriteLine(arr);
            Console.Read();
        }
    }
}
