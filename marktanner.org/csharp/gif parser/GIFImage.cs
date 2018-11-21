using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gif_parser
{
    class GIFImage
    {
        public ushort left;
        public ushort top;
        public ushort width;
        public ushort height;

        public bool hasData = false;

        public Color[] pixels;

        public int disposalMethod;
        public bool userInput;
        public bool transparencyColorFlag;
        public ushort delay;
        public byte transparencyIndex;

        public Image toImage()
        {
            Bitmap bitmap = new Bitmap(width, height);

            int k = 0;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    bitmap.SetPixel(i, j, pixels[k]);
                    k++;
                }
            }

            return (Image)bitmap;
        }
    }
}
