using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace Dürer_projection_algorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<Point3D> vertices = new List<Point3D>();
            List<Edge> edges = new List<Edge>();

            generateUnitCube(ref vertices, ref edges);
            translateVertices(ref vertices, new Point3D(0, 0, 3));

            Image canvas = (Image)new Bitmap(200, 200);
            Graphics g = Graphics.FromImage(canvas);

            List<PointF> points = createDurerProjection(vertices, 200, new DoubleRectangle(-0.5, -0.5, 1, 1));

            Pen pen = new Pen(Color.Black, 2);

            foreach(Edge edge in edges)
            {
                g.DrawLine(pen, points[edge.startIndex].X, points[edge.startIndex].Y, points[edge.endIndex].X, points[edge.endIndex].Y);
            }

            pictureBox1.Image = canvas;
        }
        
        /// <summary>
        /// converts points from 3d space to 2d space using the durer algorithm
        /// </summary>
        /// <param name="vertices">the vertices in 3d space</param>
        /// <param name="scale">the scale to apply to the the 2d space</param>
        /// <param name="frame">the minimum and maximum bounds of the frame</param>
        /// <returns>the points in 2d space, minimum point being 0 and maximum being 100</returns>
        private List<PointF> createDurerProjection(List<Point3D> vertices, double scale, DoubleRectangle frame)
        {
            List<PointF> points = new List<PointF>();

            foreach(Point3D vertex in vertices)
            {
                double xPrime = vertex.X / vertex.Z;
                double yPrime = vertex.Y / vertex.Z;

                PointF p = new PointF();
                p.X = (int)(scale * (1 - (xPrime - frame.left) / frame.width));
                p.Y = (int)(scale * (yPrime - frame.top) / frame.height);
                points.Add(p);
            }

            return points;
        }

        private void translateVertices(ref List<Point3D> vertices, Point3D amount)
        {
            for (int i = 0; i < vertices.Count;i++ )
            {
                vertices[i] = new Point3D(vertices[i].X + amount.X, vertices[i].Y + amount.Y, vertices[i].Z + amount.Z);
            }
        }

        private void generateUnitCube(ref List<Point3D> vertices, ref List<Edge> edges)
        {
            vertices.Add(new Point3D(-0.5, -0.5, -0.5));
            vertices.Add(new Point3D(-0.5, 0.5, -0.5));
            vertices.Add(new Point3D(0.5, 0.5, -0.5));
            vertices.Add(new Point3D(0.5, -0.5, -0.5));
            vertices.Add(new Point3D(-0.5, -0.5, 0.5));
            vertices.Add(new Point3D(-0.5, 0.5, 0.5));
            vertices.Add(new Point3D( 0.5, 0.5, 0.5 ));
            vertices.Add(new Point3D( 0.5, -0.5, 0.5));

            edges.Add(new Edge(0, 1));
            edges.Add(new Edge(1, 2));
            edges.Add(new Edge(2, 3));
            edges.Add(new Edge(3, 0));
            edges.Add(new Edge(0, 4));
            edges.Add(new Edge(1, 5));
            edges.Add(new Edge(2, 6));
            edges.Add(new Edge(3, 7));
            edges.Add(new Edge(4, 5));
            edges.Add(new Edge(5, 6));
            edges.Add(new Edge(6, 7));
            edges.Add(new Edge(7, 4));
        }
    }

    class DoubleRectangle
    {
        public double x = 0;
        public double y = 0;
        public double width = 0;
        public double height = 0;

        public DoubleRectangle(double x, double y, double width, double height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public double left
        {
            get { return x; }
            set { width = right - value; x = value; }
        }

        public double top
        {
            get { return y; }
            set { height = bottom - value; y = value; }
        }

        public double right
        {
            get { return x + width; }
            set { width = value - x; }
        }

        public double bottom
        {
            get { return y + height; }
            set { height = value - y; }
        }
    }

    class Edge
    {
        public int startIndex;
        public int endIndex;

        public Edge(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }
    }
}
