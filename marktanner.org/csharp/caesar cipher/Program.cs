using marktanner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceasar_cipher
{
    class Program
    {
        private static ByteArray bytes;
        
        static void Main(string[] args)
        {
            Boolean encrypt = false;
            uint offset = 1;

            if(encrypt)
            {
                bytes = new ByteArray(Directory.GetCurrentDirectory() + "/test.bmp");
                
                MemoryStream encyptedBytes = new MemoryStream();

                while (bytes.Position < bytes.Length)
                {
                    encyptedBytes.WriteByte((byte)addToByte((uint)bytes.getByte(), offset));
                }

                ByteArrayToFile(Directory.GetCurrentDirectory() + "/test encrypted.bmp", encyptedBytes.ToArray());
            }
            else
            {
                bytes = new ByteArray(Directory.GetCurrentDirectory() + "/test encrypted.bmp");

                MemoryStream decryptedBytes = new MemoryStream();

                while (bytes.Position < bytes.Length)
                {
                    decryptedBytes.WriteByte((byte)subtractFromByte((uint)bytes.getByte(), offset));
                }

                ByteArrayToFile(Directory.GetCurrentDirectory() + "/test decrypted.bmp", decryptedBytes.ToArray());
            }

            Console.Read();
        }

        private static void ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                FileStream _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }
        }

        private static uint subtractFromByte(uint b, uint amount)
        {
            if (b >= amount)
            {
                return b - amount;
            }
            else
            {
                return 255 - (amount - b);
            }
        }

        private static uint addToByte(uint b, uint amount)
        {
            b += amount;

            b %= 256;
            
            return b;
        }
    }
}
