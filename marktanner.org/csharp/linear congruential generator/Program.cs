using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linear_congruential_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Generator gen = new Generator(100);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(gen.next());
            }

            Console.Read();
        }
    }
}
