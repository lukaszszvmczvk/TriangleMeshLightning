using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static GKLab2.TriangleMesh;
using static System.Windows.Forms.DataFormats;

namespace GKLab2
{
    public static class Utils
    {
        static float eps = 1e-6f;
        public static double ComputeZ(double u, double v)
        {
            int n = 3;
            double sum = 0.0;
            double x = u / TriangleMesh.pictureBox.Width;
            double y = v / TriangleMesh.pictureBox.Height;
            var cp = TriangleMesh.ControlPoints;

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    sum += cp[i, j] * B(x, i) * B(y, j);
                }
            }
            return sum;
        }

        private static double B(double t, int i)
        {
            int n = 3;
            double nFactorial = 6.0;
            double newton = nFactorial;

            for (int k = 1; k <= i; k++)
            {
                newton /= (double)k;
            }

            int ni = n - i;
            for (int k = 1; k <= ni; k++)
            {
                newton /= (double)k;
            }

            return newton * Math.Pow(t, i) * Math.Pow(t, ni);
        }

        public static Vector3 Pu(double u, double v)
        {
            double h = 1e-6;
            double z = (ComputeZ(u + h, v) - ComputeZ(u - h, v)) / (2 * h);
            return new Vector3(1, 0, (float)z);
        }

        public static Vector3 Pv(double u, double v)
        {
            double h = 1e-6;
            double z = (ComputeZ(u, v + h) - ComputeZ(u, v - h)) / (2 * h);
            return new Vector3(0, 1, (float)z);
        }

        public static Vector3 GetNormalVector(Vector3 v)
        {
            var x = v.X; var y = v.Y;
            Vector3 N = Vector3.Cross(Pu(x, y), Pv(x, y));
            return Vector3.Normalize(N);
        }
        public static Color GetColor(Triangle triangle, int xi, int yi)
        {
            var A = triangle.A; var B = triangle.B; var C = triangle.C;
            var x = xi;
            var y = yi;

            float alpha = ((B.Y * C.X - C.Y * B.X) + (C.Y * x - y * C.X)
                    + (y * B.X - B.Y * x)) / ((B.Y * C.X - C.Y * B.X)
                    + (C.Y * A.X - A.Y * C.X) + (A.Y * B.X - B.Y * A.X));
            float beta = ((C.Y * A.X - A.Y * C.X) + (A.Y * x - y * A.X)
                 + (y * C.X - C.Y * x)) / ((B.Y * C.X - C.Y * B.X)
                 + (C.Y * A.X - A.Y * C.X) + (A.Y * B.X - B.Y * A.X));
            float gamma = 1 - alpha - beta;

            Vector3 N = alpha * triangle.ANormal + beta * triangle.BNormal + gamma * triangle.CNormal;
            N = Vector3.Normalize(N);

            float z = alpha * triangle.A.Z + beta * triangle.B.Z + gamma * triangle.C.Z;

            var L = Vector3.Normalize(new Vector3(LightSource.LightPositon.X - xi, LightSource.LightPositon.Y - yi, LightSource.LightPositon.Z - z));
            var V = new Vector3(0, 0, 1);
            var R = 2 * Vector3.Dot(N, L) * N - L;

            float nl = Vector3.Dot(N, L);
            float vr = Vector3.Dot(V, R);
            nl = nl < 0 ? 0 : nl;
            vr = vr < 0 ? 0 : vr;

            var I = kd * LightSource.LightColorNormalized * Io * nl + ks * LightSource.LightColorNormalized * Io * MathF.Pow(vr, m);

            var r = float.IsNaN(I.X * 255) || I.X * 255 > 255 ? 255 : I.X * 255;
            var g = float.IsNaN(I.Y * 255) || I.Y * 255 > 255 ? 255 : I.Y * 255;
            var b = float.IsNaN(I.Z * 255) || I.Z * 255 > 255 ? 255 : I.Z * 255;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }
    }
}
