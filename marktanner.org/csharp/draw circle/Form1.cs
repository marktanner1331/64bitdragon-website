using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace draw_circle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Bitmap b = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            DrawCircle(b, b.Width / 2, b.Height / 2, b.Height / 4);

            pictureBox1.Image = b;
        }

        static void DrawCircle(Bitmap image, int x0, int y0, int radius)
        {
            int x = radius, y = 0;
            int radiusError = 1 - x;

            while (x >= y)
            {
                image.SetPixel(x + x0, y + y0, Color.Black);
                image.SetPixel(y + x0, x + y0, Color.Black);
                image.SetPixel(-x + x0, y + y0, Color.Black);
                image.SetPixel(-y + x0, x + y0, Color.Black);
                image.SetPixel(-x + x0, -y + y0, Color.Black);
                image.SetPixel(-y + x0, -x + y0, Color.Black);
                image.SetPixel(x + x0, -y + y0, Color.Black);
                image.SetPixel(y + x0, -x + y0, Color.Black);
                y++;
                if (radiusError < 0)
                {
                    radiusError += 2 * y + 1;
                }
                else
                {
                    x--;
                    radiusError += 2 * (y - x + 1);
                }
            }
        }
    }
}
