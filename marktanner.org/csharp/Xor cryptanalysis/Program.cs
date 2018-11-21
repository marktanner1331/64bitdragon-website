using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xor_cryptanalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            string plaintext = getPlaintext();
            Console.WriteLine(plaintext);

            Console.WriteLine();

            byte[] key = getRandomKey();

            string cipherText = encrypt(plaintext, key);
            
            Console.WriteLine(cipherText);

            Console.WriteLine();

            plaintext = crack(cipherText);

            Console.WriteLine(plaintext);

            Console.Read();
        }

        static string crack(string cipherText)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(cipherText);
            bytes = crack(bytes);

            return Encoding.ASCII.GetString(bytes);
        }

        static byte[] crack(byte[] cipherText)
        {
            int keyLength = getLengthOfKey(cipherText);
            byte[] key = new byte[keyLength];

            for (int i = 0; i < keyLength; i++)
            {
                key[i] = getByteOfKey(cipherText, keyLength, i);
            }

            return decrypt(cipherText, key);
        }

        static byte getByteOfKey(byte[] cipherText, int keyLength, int byteOfKeyIndex)
        {
            Dictionary<byte, int> dict = new Dictionary<byte, int>();

            for (int i = byteOfKeyIndex; i < cipherText.Length; i += keyLength)
            {
                if (dict.ContainsKey(cipherText[i]) == false)
                {
                    dict.Add(cipherText[i], 1);
                }
                else
                {
                    dict[cipherText[i]]++;
                }
            }

            int maxValue = 0;
            byte byteForMaxValue = 0;
            foreach (KeyValuePair<byte, int> pair in dict)
            {
                if (pair.Value > maxValue)
                {
                    maxValue = pair.Value;
                    byteForMaxValue = pair.Key;
                }
            }

            //hint: space is the most common character in english
            byte keyByte = (byte)(0x20 ^ byteForMaxValue);

            return keyByte;
        }

        static int getLengthOfKey(byte[] cipherText)
        {
            for (int keyLength = 1; keyLength < cipherText.Length; keyLength++)
            {
                byte[] shiftedBytes = shiftBytesRight(cipherText, keyLength);

                byte[] xoredBytes = xorBytes(cipherText, shiftedBytes);

                int numEqual = numEqualBytes(xoredBytes);

                double percent = (double)numEqual / cipherText.Length;

                if (percent > 0.06)
                {
                    return keyLength;
                }
            }

            throw new Exception("unable to break");
        }

        static byte[] xorBytes(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                throw new Exception("byte arrays must of the same length");
            }

            byte[] c = new byte[a.Length];

            for(int i = 0;i < a.Length;i++)
            {
                c[i] = (byte)(a[i] ^ b[i]);
            }

            return c;
        }

        static byte[] shiftBytesRight(byte[] bytes, int amount)
        {
            byte[] shiftedBytes = new byte[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                shiftedBytes[(i + amount) % bytes.Length] = bytes[i];
            }

            return shiftedBytes;
        }

        /// <summary>
        /// given a byte array, this method returns the largest amount of equal bytes
        /// </summary>
        static int numEqualBytes(byte[] bytes)
        {
            Dictionary<byte, int> dict = new Dictionary<byte, int>();

            for (int i = 0; i < bytes.Length; i++)
            {
                if (dict.ContainsKey(bytes[i]) == false)
                {
                    dict.Add(bytes[i], 1);
                }
                else
                {
                    dict[bytes[i]]++;
                }
            }

            int maxEqual = 0;

            foreach (KeyValuePair<byte, int> pair in dict)
            {
                maxEqual = Math.Max(maxEqual, pair.Value);
            }

            return maxEqual;
        }

        static byte[] getRandomKey()
        {
            Random random = new Random();
            int keyLength = random.Next(4, 16);

            byte[] key = new byte[keyLength];

            random.NextBytes(key);

            //setting the most significant bit to 0 will mean the cipher text will be valid ascii
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = (byte)(key[i] & 127);
            }

            return key;
        }

        static string encrypt(string plaintext, byte[] key)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(plaintext);
            bytes = encrypt(bytes, key);

            return Encoding.ASCII.GetString(bytes);
        }

        static byte[] encrypt(byte[] plaintext, byte[] key)
        {
            byte[] output = new byte[plaintext.Length];

            for (int i = 0; i < plaintext.Length; i++)
            {
                output[i] = (byte)(plaintext[i] ^ key[i % key.Length]);
            }

            return output;
        }

        static byte[] decrypt(byte[] cipherText, byte[] key)
        {
            byte[] output = new byte[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                output[i] = (byte)(cipherText[i] ^ key[i % key.Length]);
            }

            return output;
        }

        static string getPlaintext()
        {
            return @"Star Wars is an American epic space opera franchise centered on a film series created by George Lucas. The franchise depicts a galaxy described as far, far away in the distant past, and portrays Jedi as a representation of good, in conflict with the Sith, their evil counterpart. Their weapon of choice, the lightsaber, is commonly recognized in popular culture. The franchise's storylines contain many themes, with influences from philosophy and religion.
                    The first film in the series, Star Wars, was released on May 25, 1977 by 20th Century Fox, and became a worldwide pop culture phenomenon. It was followed by two sequels, released in 1980 and 1983. A prequel trilogy of films were later released between 1999 and 2005. Reaction to the original trilogy was largely positive, while the prequel trilogy received a more mixed reaction from critics and fans. All six films were nominated for or won Academy Awards, and all were box office successes; the overall box office revenue generated totals $4.38 billion, making Star Wars the fifth-highest-grossing film series. The series has spawned an extensive media franchise – the Expanded Universe – including books, television series, computer and video games, and comic books, resulting in significant development of the series's fictional universe.
                    In 2012, The Walt Disney Company acquired Lucasfilm for $4.05 billion and announced three new Star Wars films, with the first film, Star Wars: The Force Awakens, planned for release in 2015. 20th Century Fox retains the physical distribution rights to the first two Star Wars trilogies, owning permanent rights for the original film Episode IV: A New Hope and holding the rights to Episodes I–III, V and VI until May 2020. The Walt Disney Studios owns digital distribution rights to all the Star Wars films, excluding A New Hope. Star Wars also holds a Guinness World Records title for the Most successful film merchandising franchise. As of 2012, the total value of the Star Wars franchise was estimated at £19.51 billion ($30.7 billion), including box-office receipts as well as profits from their video games and DVD sales.";
        }
    }
}
