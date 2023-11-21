using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static GKLab2.TriangleMesh;

namespace GKLab2
{
    public class TriangleMesh
    {
        public class Triangle
        {
            public Vector3 A { get; set; }
            public Vector3 B { get; set; }
            public Vector3 C { get; set; }
            public Vector3 ANormal { get; set; }
            public Vector3 BNormal { get; set; }
            public Vector3 CNormal { get; set; }
            public List<Vector3> Points { get; set; }
            public List<Point> ExtendedPoints { get; set; }
            public Triangle(Vector3 A, Vector3 B, Vector3 C)
            {
                this.A = A; this.B = B; this.C = C;
                this.ANormal = Utils.GetNormalVector(A);
                this.BNormal = Utils.GetNormalVector(B);
                this.CNormal = Utils.GetNormalVector(C);
                Points = new List<Vector3>
                {
                    A,B,C
                };
                Points.Sort((a, b) => a.Y.CompareTo(b.Y));
                ExtendedPoints = Points.Select(v => new Point((int)(v.X), (int)(v.Y))).ToList();
            }
        }
        public static PictureBox pictureBox { get; set; }
        private List<Triangle> triangles = new List<Triangle>();
        public static float[,] ControlPoints { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public static float kd { get; set; }
        public static float ks { get; set; }
        public static float m { get; set; }
        public static Vector3 Io { get; set; }

        public TriangleMesh() 
        {
            ControlPoints = new float[4,4];
            ControlPoints[1, 2] = 20;
            X = 4; Y = 4;
            Io = new Vector3(0.5f, 0, 0);
            kd = 0f;
            ks = 0f;
            m = 1;
            InitializeTriangles();
        }
        public void InitializeTriangles()
        {
            triangles.Clear();
            float xStep = (pictureBox.Size.Width-1) / (float)(X - 1);
            float yStep = (pictureBox.Size.Height-1) / (float)(Y - 1);
            var points = new Vector3[X, Y];
            for(int i=0; i<X; ++i)
            {
                for(int j=0; j<Y; ++j)
                {
                    var x = (float)(xStep * i);
                    var y = (float)(yStep * j);
                    points[i, j] = new Vector3(x, y, (float)Utils.ComputeZ(x, y));
                }
            }
            for (int j = 0; j < Y; ++j)
            {
                for (int i = 0; i < X - 1; i++)
                {
                    if (j != 0)
                        triangles.Add(new Triangle(points[i, j], points[i + 1, j], points[i, j - 1]));
                    if (j != Y - 1)
                        triangles.Add(new Triangle(points[i, j], points[i + 1, j], points[i + 1, j + 1]));
                }
            }
            FillBitmap();
        }
        public void FillBitmap()
        {
            var newCanvas = new Bitmap(pictureBox.Size.Width, pictureBox.Size.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var lockBitmap = new LockBitmap(newCanvas);
            lockBitmap.LockBits();
            Parallel.For(0, triangles.Count, i =>
            {
                FillTriangle(triangles[i], lockBitmap);
            });
            lockBitmap.UnlockBits();
            //foreach (var tri in triangles)
            //{
            //    using (var graphics = Graphics.FromImage(newCanvas))
            //    {
            //        var points = tri.Points.Select(x => new Point((int)(x.X*pictureBox.Width), (int)(x.Y * pictureBox.Height))).ToArray();
            //        graphics.DrawPolygon(new Pen(Brushes.Black), points);
            //    }
            //}
            pictureBox.Image = newCanvas;
        }
        public void FillTriangle(Triangle triangle, LockBitmap bitmap)
        {
            var points = triangle.ExtendedPoints;
            var miny = points[0].Y;
            var maxy = points[points.Count - 1].Y;
            List<AETNode> AET = new List<AETNode>();

            int k = 0;
            for (int yi = miny; yi <= maxy; ++yi)
            {
                k = 0;
                while (points[k].Y == yi - 1)
                {
                    int prevK = k - 1 >= 0 ? k - 1 : points.Count - 1;
                    int nextK = (k + 1) % points.Count;

                    if (points[prevK].Y > points[k].Y)
                        AET.Add(new AETNode(points[k].X, points[k].Y, points[prevK].X, points[prevK].Y));
                    else if (points[prevK].Y < points[k].Y)
                        AET.Remove(new AETNode(points[k].X, points[k].Y, points[prevK].X, points[prevK].Y));

                    if (points[nextK].Y > points[k].Y)
                        AET.Add(new AETNode(points[nextK].X, points[nextK].Y, points[k].X, points[k].Y));
                    else if (points[nextK].Y < points[k].Y)
                        AET.Remove(new AETNode(points[nextK].X, points[nextK].Y, points[k].X, points[k].Y));
                    ++k;
                }

                AET.Sort((a1, a2) => a1.X.CompareTo(a2.X));

                for (int i = 0; i < AET.Count; i += 2)
                {
                    for (int xi = AET[i].X; xi <= AET[i + 1].X; xi++)
                    {
                        Color color = Utils.GetColor(triangle, xi, yi);
                        bitmap.SetPixel(xi, yi, color);
                    }
                }
                foreach (var node in AET)
                    node.AdjustX();
            }
        }
    }
}
