using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xor_encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string plaintext = "Star Wars is an American epic space opera franchise centered on a film series created by George Lucas.";
            Console.WriteLine(plaintext);

            Console.WriteLine();

            byte[] key = getRandomKey();
            string cipherText = encrypt(plaintext, key);

            Console.WriteLine(cipherText);

            Console.WriteLine();

            plaintext = decrypt(cipherText, key);

            Console.WriteLine(plaintext);

            Console.Read();
        }

        static byte[] getRandomKey()
        {
            Random random = new Random();
            byte[] key = new byte[16];

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

        static string decrypt(string cipherText, byte[] key)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(cipherText);
            bytes = decrypt(bytes, key);

            return Encoding.ASCII.GetString(bytes);
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
    }
}
