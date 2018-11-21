using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sieve_of_Eratosthenes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> primes = generatePrimesUpTo(100);

            Console.WriteLine("primes between 0 and {0}", 100);

            foreach (int prime in primes)
            {
                Console.WriteLine(prime);
            }

            Console.Read();
        }

        private static List<int> generatePrimesUpTo(int p)
        {
            //if numbers[i] == true, then i is prime
            bool[] numbers = new bool[p];

            //pretend they are all prime
            for (int i = 0; i < p; i++)
            {
                numbers[i] = true;
            }

            //0 and 1 aren't prime
            numbers[0] = false;
            numbers[1] = false;

            int maxFactor = (int)Math.Sqrt(p);
            for (int i = 0; i < maxFactor; i++)
            {
                //if i isnt prime, then multiples of i's factors will have already
                //handled the multiples of i
                if (numbers[i] == false)
                {
                    continue;
                }

                //go through all the multiples of i, and set them to composite (e.g. non prime)
                for (int a = 2; a * i < p; a++)
                {
                    numbers[a * i] = false;
                }
            }

            //make a list out of all the primes
            List<int> primes = new List<int>();
            for (int i = 0; i < p; i++)
            {
                if (numbers[i] == true)
                {
                    primes.Add(i);
                }
            }

            return primes;
        }
    }
}
