using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static GKLab2.TriangleMesh;
using static System.Windows.Forms.DataFormats;

namespace GKLab2
{
    public static class Utils
    {
        static double eps = 1e-6;
        public static double Z(double x, double y)
        {
            var cp = ControlPoints;
            double sum = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    sum += cp[i, j] * B(i, x) * B(j, y);
                }
            }
            return sum;
        }
        private static double B(int i, double x)
        {
            var result = Math.Pow(x, i) * Math.Pow(1 - x, 3 - i);
            if (i == 0 || i == 3)
                return result;
            else
                return result * 3;
        }
        private static Vector3D Pu(Vector3D v)
        {
            double Puz = (Z(v.X + eps, v.Y) - Z(v.X, v.Y)) / eps;
            return new Vector3D(1, 0, Puz);

        }
        private static Vector3D Pv(Vector3D v)
        {
            double Pvz = (Z(v.X, v.Y + eps) - Z(v.X, v.Y)) / eps;
            return new Vector3D(0, 1, Pvz);
        }
        public static Vector3D GetNormalVector(Vector3D v)
        {
            Vector3D pu = Pu(v);
            Vector3D pv = Pv(v);
            var vector = Vector3D.CrossProduct(pu, pv);
            vector.Normalize();
            return vector;
        }
        public static Color GetColor(Triangle triangle, int xi, int yi)
        {
            var A = triangle.A; var B = triangle.B; var C = triangle.C;
            var x = xi / (double)Width;
            var y = yi / (double)Height;

            double alpha = ((B.Y * C.X - C.Y * B.X) + (C.Y * x - y * C.X) + (y * B.X - B.Y * x)) / 
                ((B.Y * C.X - C.Y * B.X) + (C.Y * A.X - A.Y * C.X) + (A.Y * B.X - B.Y * A.X));
            double beta = ((C.Y * A.X - A.Y * C.X) + (A.Y * x - y * A.X) + (y * C.X - C.Y * x)) / 
                ((B.Y * C.X - C.Y * B.X) + (C.Y * A.X - A.Y * C.X) + (A.Y * B.X - B.Y * A.X));
            double gamma = 1 - alpha - beta;

            double z = alpha * A.Z + beta * B.Z + gamma * C.Z;

            Vector3D N = triangle.ANormal * alpha + triangle.BNormal * beta + triangle.CNormal * gamma;
            N.Normalize();
            Vector3D V = new Vector3D(0, 0, 1);
            if (UseNormalMap)
            {
                if(ReplaceN)
                {
                    N = INV[xi, yi];
                }
                else
                {
                    Vector3D vectorB;
                    if (Math.Abs(N.X) <= eps && Math.Abs(N.Y) <= eps && Math.Abs(N.Z - 1) <= eps)
                    {
                        vectorB = new Vector3D(0, 1, 0);
                    }
                    else
                    {
                        vectorB = Vector3D.CrossProduct(N, V);
                        vectorB.Normalize();
                    }

                    var T = Vector3D.CrossProduct(vectorB, N);
                    var M = new Matrix3D(T.X, vectorB.X, N.X, 0,
                        T.Y, vectorB.Y, N.Y, 0,
                        T.Z, vectorB.Z, N.Z, 0,
                        0, 0, 0, 0);
                    N = M.Transform(INV[xi, yi]);
                    N.Normalize();
                }
            }

            Vector3D L = new Vector3D(LightSource.LightPositon.X - xi, LightSource.LightPositon.Y - yi, LightSource.LightPositon.Z - z);
            L.Normalize();

            double cos1 = Vector3D.DotProduct(N, L);
            Vector3D R = 2 * cos1 * N - L;
            cos1 = Math.Max(cos1, 0);

            double cos2 = Vector3D.DotProduct(V, R);
            cos2 = Math.Max(cos2, 0);
            cos2 = Math.Pow(cos2, m);

            Vector3D objColor = Io;
            if (UseImage)
                objColor = LT[xi, yi];

            double red = 255 * objColor.X * LightSource.LCN.X * (kd * cos1 + ks * cos2);
            double green = 255 * objColor.Y * LightSource.LCN.Y * (kd * cos1 + ks * cos2);
            double blue = 255 * objColor.Z * LightSource.LCN.Z * (kd * cos1 + ks * cos2);
            red = red > 255 ? 255 : red;
            green = green > 255 ? 255 : green;
            blue = blue > 255 ? 255 : blue;
            return Color.FromArgb((int)red, (int)green, (int)blue);
        }
    }
}
