using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marktanner
{
    public class ByteArray
    {
        private byte[] _bytes;
        private MemoryStream _stream;

        public byte[] bytes
        {
            get
            {
                return _bytes;
            }
        }

        public MemoryStream stream
        {
            get
            {
                return _stream;
            }
        }

        public ByteArray(byte[] data)
        {
            _bytes = data;
            _stream = new MemoryStream(_bytes);
        }

        public ByteArray(string fullFilePath)
        {
            _bytes = GetBytesFromFile(fullFilePath);
            _stream = new MemoryStream(_bytes);
        }

        public long Position
        {
            get
            {
                return stream.Position;
            }
            set
            {
                stream.Position = value;
            }
        }

        public uint Length
        {
            get
            {
                return (uint)bytes.Length;
            }
        }

        public void skipBytes(uint value)
        {
            stream.Position += value;
        }

        public byte getByte()
        {
            byte b = bytes[Position];
            Position++;
            return b;
        }

        public ushort getUI16()
        {
            ushort value = BitConverter.ToUInt16(bytes, (int)stream.Position);

            //have to increment the stream position manually here because we are 
            //dealing directory with the byte array
            stream.Position += 2;

            return value;
        }

        public uint getUI24()
        {
            uint value = BitConverter.ToUInt32(bytes, (int)stream.Position);

            //have to increment the stream position manually here because we are 
            //dealing directory with the byte array
            stream.Position += 3;

            return value & 0xffffff;
        }

        public uint getUI32()
        {
            uint value = BitConverter.ToUInt32(bytes, (int)stream.Position);

            //have to increment the stream position manually here because we are 
            //dealing directory with the byte array
            stream.Position += 4;

            return value;
        }

        public int getI32()
        {
            int value = BitConverter.ToInt32(bytes, (int)stream.Position);

            //have to increment the stream position manually here because we are 
            //dealing directory with the byte array
            stream.Position += 4;

            return value;
        }

        public string getString(int length)
        {
            byte[] text = new byte[length];

            for (int i = 0; i < length; i++)
            {
                text[i] = (byte)stream.ReadByte();
            }

            Encoding iso = Encoding.GetEncoding("us-ascii");
            return iso.GetString(text);
        }

        public ByteArray getBytes(uint count)
        {
            if (count == 0)
            {
                return new ByteArray(new byte[0]);
            }
            else
            {
                byte[] data = new byte[count];
                Array.Copy(bytes, stream.Position, data, 0, count);

                skipBytes(count);

                return new ByteArray(data);
            }
        }

        private byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }
    }
}
