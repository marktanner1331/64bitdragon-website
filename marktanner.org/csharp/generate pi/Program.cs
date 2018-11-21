using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generate_pi
{
    class Program
    {
        static void Main(string[] args)
        {
            int numPoints = 1000000;
            int numPointsInside = 0;

            Random rand = new Random();

            for(int i = 0;i < numPoints;i++)
            {
                double x = rand.NextDouble();
                double y = rand.NextDouble();

                if (x * x + y * y <= 1) 
                {
                    numPointsInside++;
                }

                //pseudo random numbers always repeat, lets stop that
                if(i % 10000 == 0)
                {
                    rand = new Random();
                }
            }

            double pi = 4.0 * (double)numPointsInside / (double)numPoints;

            Console.WriteLine(pi);
            Console.Read();
        }
    }
}
