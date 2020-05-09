using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISUtilities
{
    /// <summary>
    /// Taken from the algorithm in GraphicGems 2 1991 Chapter 1.3 by Jack C. Morrison
    /// </summary>
    static class DistFromAPointToALine
    {
        public static Vector2 OriginTransformedLineVector(Vector2 point1, Vector2 point2)
        {
            return point2 - point1;
        }

        public static double OriginTransformedXValue(Vector2 point1, Vector2 point2)
        {
            return point2.x - point1.x;
        }

        public static double OriginTransformedYValue(Vector2 point1, Vector2 point2)
        {
            return point2.y - point1.y;
        }

        public static double TwiceSignedArea(Vector2 lineStart, Vector2 lineEnd, Vector2 point, Vector2 originTransformedLine)
        {
            var a2 = ((point.y - lineStart.y) * originTransformedLine.x) - ((point.x - lineStart.x) * originTransformedLine.y);
            return a2;
        }

        public static double DistanceSquared(Vector2 originTransformedLine, double twiceSignedArea)
        {
            var dSqr = Math.Pow(twiceSignedArea, 2) / (Math.Pow(originTransformedLine.x, 2) + Math.Pow(originTransformedLine.y, 2));
            return dSqr;
        }
    }
}
