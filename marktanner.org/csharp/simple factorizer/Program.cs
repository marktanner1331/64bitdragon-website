using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_factorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            int p = 3960;
            List<int> factors = factorize(p);
            Console.Write("{0} = ", p);

            for (int i = 0; i < factors.Count; i++)
            {
                Console.Write(factors[i]);
                Console.Write(i < factors.Count - 1 ? ", " : "\n");
            }

            Console.Read();
        }

        private static List<int> factorize(int p)
        {
            List<int> factors = new List<int>();

            while(p % 2 == 0)
            {
                factors.Add(2);
                p /= 2;
            }

            //casting to an int will floor the double
            int max = (int)Math.Sqrt(p);
            for(int i = 3; i < p;i += 2)
            {
                while(p % i == 0)
                {
                    factors.Add(i);
                    p /= i;
                    max = (int)Math.Sqrt(p);
                }
            }

            if(p > 1)
            {
                factors.Add(p);
            }

            return factors;
        }
    }
}
