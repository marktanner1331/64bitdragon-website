using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mccarthy_91
{
    class Program
    {
        static void Main(string[] args)
        {
            //the mcCarthy 91 function will return 91 for all integers smaller than 102
            for (int i = 1; i < 102; i++)
            {
                Debug.WriteLine(i + ": " + m(i));
            }
        }

        static int m(int n)
        {
            if (n > 100)
            {
                return n - 10;
            }
            else
            {
                return m(m(n + 11));
            }
        }
    }
}
