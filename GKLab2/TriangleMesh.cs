﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static GKLab2.TriangleMesh;

namespace GKLab2
{
    public class TriangleMesh
    {
        public class Triangle
        {
            public Vector3D A { get; set; }
            public Vector3D B { get; set; }
            public Vector3D C { get; set; }
            public Vector3D ANormal { get; set; }
            public Vector3D BNormal { get; set; }
            public Vector3D CNormal { get; set; }
            public List<Vector3D> Points { get; set; }
            public List<Point> ExtendedPoints { get; set; }
            public Triangle(Vector3D A, Vector3D B, Vector3D C)
            {
                this.A = A; this.B = B; this.C = C;
                this.ANormal = Utils.GetNormalVector(A);
                this.BNormal = Utils.GetNormalVector(B);
                this.CNormal = Utils.GetNormalVector(C);
                Points = new List<Vector3D>
                {
                    A,B,C
                };
                Points.Sort((a, b) => a.Y.CompareTo(b.Y));
                ExtendedPoints = Points.Select(v => new Point((int)(v.X * Width), (int)(v.Y * Height))).ToList();
            }
        }
        public static PictureBox pictureBox { get; set; }
        private List<Triangle> triangles = new List<Triangle>();
        public static double[,] ControlPoints { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public static double kd { get; set; }
        public static double ks { get; set; }
        public static int m { get; set; }
        public static Vector3D Io { get; set; }
        public static bool UseImage { get; set; }
        public static bool UseNormalMap { get; set; }
        public static Vector3D[,] INV { get; set; }
        public static Vector3D[,] LT { get; set; }
        public bool ShowMesh { get; set; }
        public static int Height { get; set; }
        public static int Width { get; set; }
        public static bool ReplaceN { get; set; }
        public TriangleMesh() 
        {
            ControlPoints = new double[4,4];
            X = 40; Y = 40;
            Io = new Vector3D(1, 0, 0);
            kd = 1;
            ks = 0;
            m = 1;
            InitializeTriangles();
            FillBitmap();
        }
        public void InitializeTriangles()
        {
            triangles.Clear();
            double stepX = 1.0 / (X-1);
            double stepY = 1.0 / (Y-1);
            var pointsArray = new Vector3D[X, Y];
            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    var x = i * stepX;
                    var y = j * stepY;
                    pointsArray[i, j] = new Vector3D(x, y, Utils.Z(x, y));
                }
            }
         
            for (int j = 0; j < Y; ++j)
            {
                for (int i = 0; i < X - 1; i++)
                {
                    if (j != 0)
                        triangles.Add(new Triangle(pointsArray[i, j], pointsArray[i + 1, j], pointsArray[i, j - 1]));
                    if (j != Y - 1)
                        triangles.Add(new Triangle(pointsArray[i, j], pointsArray[i + 1, j], pointsArray[i + 1, j + 1]));
                }
            }
        }
        public void FillBitmap()
        {
            var newCanvas = new Bitmap(Width + 1, Height + 1, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            var lockBitmap = new LockBitmap(newCanvas);
            lockBitmap.LockBits();
            Parallel.For(0, triangles.Count, i =>
            {
                FillTriangle(triangles[i], lockBitmap);
            });
            lockBitmap.UnlockBits();
            if (ShowMesh)
            {
                foreach (var tri in triangles)
                {
                    using (var graphics = Graphics.FromImage(newCanvas))
                    {
                        var points = tri.Points.Select(x => new Point((int)(x.X*pictureBox.Width), (int)(x.Y * pictureBox.Height))).ToArray();
                        graphics.DrawPolygon(new Pen(Brushes.Black), points);
                    }
                }
            }
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
