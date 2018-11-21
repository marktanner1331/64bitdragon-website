using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bresenham_line
{
    /// <summary>
    /// drawing lines is more complicated than it first appears, 
    /// if the line is hoverring on a pixel boundary, deciding which pixel it should fill is difficult without
    /// resorting to floating point numbers
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Bitmap b = new Bitmap(1000, 700);
            drawBresenhamLine(b, new Point(0, 0), new Point(1000 - 1, 700 - 1));

            pictureBox1.Image = b;
        }

        static void drawBresenhamLine(Bitmap image, Point start, Point end)
        {
            int deltaX = Math.Abs(end.X - start.X);
            int deltaY = Math.Abs(end.Y - start.Y);

            int sx = start.X < end.X ? 1 : -1;
            int sy = start.Y < end.Y ? 1 : -1;

            int error = deltaX - deltaY;

            while (true)
            {
                image.SetPixel(start.X, start.Y, Color.Black);

                if (start.X == end.X && start.Y == end.Y)
                    break;

                int e2 = 2 * error;
                if (e2 > -deltaY)
                {
                    error -= deltaY;
                    start.X += sx;
                }

                if (e2 < deltaX)
                {
                    error += deltaX;
                    start.Y += sy;
                }
            }
        }
    }
}
