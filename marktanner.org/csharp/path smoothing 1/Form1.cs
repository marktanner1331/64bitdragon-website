using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace path_smoothing_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<PointF> shape = generatePolygon();
            translateVertices(ref shape, new PointF(5, 5));

            Image canvas = (Image)new Bitmap(500, 250);

            Graphics g = Graphics.FromImage(canvas);

            drawClosedPolygon(shape, g);

            PointF[] smoothSquareArr = new PointF[shape.Count];
            shape.CopyTo(smoothSquareArr);
            List<PointF> smoothSquare = smoothSquareArr.ToList();

            for (int i = 0; i < 5; i++)
            {
                smoothSquare = performCornerCuttingOnClosedPolyline(ref smoothSquare);
            }
            
            translateVertices(ref smoothSquare, new PointF(250, 0));
            drawClosedPolygon(smoothSquare, g);
            
            pictureBox1.Image = canvas;
        }

        private List<PointF> performCornerCuttingOnClosedPolyline(ref List<PointF> vertices)
        {
            List<PointF> newVertices = new List<PointF>();

            PointF path;
            PointF currentPoint = vertices[0];

            for(int i = 1;i < vertices.Count;i++)
            {
                path = new PointF(vertices[i].X - currentPoint.X, vertices[i].Y - currentPoint.Y);
                newVertices.Add(new PointF(currentPoint.X + path.X / 3, currentPoint.Y + path.Y / 3));
                newVertices.Add(new PointF(currentPoint.X + 2 * path.X / 3, currentPoint.Y + 2 * path.Y / 3));

                currentPoint = vertices[i];
            }

            //for the last path that connects back to the first vertex
            path = new PointF(vertices[0].X - currentPoint.X, vertices[0].Y - currentPoint.Y);
            newVertices.Add(new PointF(currentPoint.X + path.X / 3, currentPoint.Y + path.Y / 3));
            newVertices.Add(new PointF(currentPoint.X + 2 * path.X / 3, currentPoint.Y + 2 * path.Y / 3));

            return newVertices;
        }

        private void drawClosedPolygon(List<PointF> vertices, Graphics g)
        {
            Pen pen = new Pen(Color.Black, 2);
            PointF currentPoint = vertices[0];

            for(int i = 1;i < vertices.Count;i++)
            {
                g.DrawLine(pen, currentPoint, vertices[i]);
                currentPoint = vertices[i];
            }

            //close the shape
            g.DrawLine(pen, currentPoint, vertices[0]);
        }

        private void translateVertices(ref List<PointF> vertices, PointF amount)
        {
            for(int i = 0;i < vertices.Count;i++)
            {
                vertices[i] = new PointF(vertices[i].X + amount.X, vertices[i].Y + amount.Y);
            }
        }

        private List<PointF> generatePolygon()
        {
            List<PointF> vertices = new List<PointF>();

            vertices.Add(new PointF(0, 0));
            vertices.Add(new PointF(200, 0));
            vertices.Add(new PointF(200, 100));
            vertices.Add(new PointF(100, 100));
            vertices.Add(new PointF(100, 200));
            vertices.Add(new PointF(0, 200));

            return vertices;
        }
    }
}
