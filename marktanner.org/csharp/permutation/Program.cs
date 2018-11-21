using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace permutation
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] first = { 0, 1, 2, 3, 4 };

            do
            {
                Debug.WriteLine(permutationToString(first));
            }
            while (nextPermutation(ref first));
        }
        
        //returns false is there are no more permutations
        //techincally dont need the ref here, but it makes it nice and explicit that we are modifying 
        //the array that was passed in
        static bool nextPermutation(ref byte[] permutation)
        {
            int indexOfStartOfSuffix = permutation.Length - 1;
            while (indexOfStartOfSuffix > 0 && permutation[indexOfStartOfSuffix - 1] > permutation[indexOfStartOfSuffix])
            {
                indexOfStartOfSuffix--;
            }

            if (indexOfStartOfSuffix == 0)
            {
                return false;
            }

            int indexOfPivot = indexOfStartOfSuffix - 1;
            int indexOfNewPivot = permutation.Length - 1;

            while (permutation[indexOfPivot] > permutation[indexOfNewPivot])
            {
                indexOfNewPivot--;
            }

            byte temp = permutation[indexOfPivot];
            permutation[indexOfPivot] = permutation[indexOfNewPivot];
            permutation[indexOfNewPivot] = temp;

            int indexOfEndOfSuffix = permutation.Length - 1;

            while (indexOfStartOfSuffix < indexOfEndOfSuffix)
            {
                temp = permutation[indexOfStartOfSuffix];
                permutation[indexOfStartOfSuffix] = permutation[indexOfEndOfSuffix];
                permutation[indexOfEndOfSuffix] = temp;

                indexOfStartOfSuffix++;
                indexOfEndOfSuffix--;
            }

            return true;
        }

        static string permutationToString(byte[] permutation)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0;i < permutation.Length;i++)
            {
                if (i != 0)
                {
                    sb.Append(", " + permutation[i]);
                }
                else
                {
                    sb.Append(permutation[i]);
                }
            }

            return sb.ToString();
        }
    }
}
