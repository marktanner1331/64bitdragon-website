using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyer_Moore_string_search_algorithm
{
    class Program
    {
        static char[] haystack = "Star Wars is an American epic space opera franchise centered on a film series created by George Lucas. The film series, consisting of two trilogies (and an upcoming third), has spawned an extensive media franchise called the Expanded Universe including books, television series, computer and video games, and comic books. These supplements to the franchise resulted in significant development of the series' fictional universe, keeping the franchise active in the 16-year interim between the two film trilogies. The franchise depicts a galaxy described as \"far, far away\" in the distant past, and commonly portrays Jedi as a representation of good, in conflict with the Sith, their evil counterpart. Their weapon of choice, the lightsaber, is commonly recognized in popular culture. The franchise's storylines contain many themes, with strong influences from philosophy and religion.".ToCharArray();
        static char[] needle = "the".ToCharArray();

        static Dictionary<char, int> jumpTable;

        static void Main(string[] args)
        {
            initializeJumpTable();
           
            for (int i = needle.Length - 1;i < haystack.Length;)
            {
                for (int j = 0; j < needle.Length; j++)
                {
                    if (needle[needle.Length - 1 - j] != haystack[i - j])
                    {
                        i += jumpTable[haystack[i - j]];
                        break;
                    }

                    if (j == needle.Length - 1)
                    {
                        Console.WriteLine("Found match at " + (i - j));
                        i += needle.Length - 1;
                        break;
                    }
                }
            }

            Console.Read();
        }

        static void initializeJumpTable()
        {
            //not bothering with the good jump heuristics, they are far too complicated
            jumpTable = new Dictionary<char,int>();

 	        for(int i = 0;i < 256;i++)
            {
                jumpTable[(char)i] = needle.Length;
            }

            for(int i = 0;i < needle.Length;i++)
            {
                jumpTable[needle[i]] = needle.Length - 1 - i;
            }

            for (int i = 0; i < 256; i++)
            {
                if (jumpTable[(char)i] == 0)
                {
                    jumpTable[(char)i] = needle.Length;
                }
            }
        }
    }
}
