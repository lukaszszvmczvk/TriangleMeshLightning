using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKLab2
{
    public class AETNode
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int X { get; set; }
        private int adjustValue;

        public AETNode(float x1, float y1, float x2, float y2)
        {
            X1 = (int)x1;
            X2 = (int)x2;
            Y1 = (int)y1;
            Y2 = (int)y2;
            if (y1 > y2)
            {
                X = X2;
                adjustValue = (X1 - X2) / (Y1 - Y2);
            }
            else
            {
                X = X1;
                adjustValue = (X2 - X1) / (Y2 - Y1);
            }
        }
        public void AdjustX()
        {
            X += adjustValue;
        }
        public override bool Equals(object? obj)
        {
            return obj is AETNode node &&
                   X1 == node.X1 &&
                   X2 == node.X2 &&
                   Y1 == node.Y1 &&
                   Y2 == node.Y2;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X1, X2, Y1, Y2);
        }
    }
}
