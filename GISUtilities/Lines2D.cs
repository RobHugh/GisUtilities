using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISUtilities
{
    static public class Lines2D
    {
        /// <summary>
        /// equation for this is (line1 cross line2) / magnitude of line 1 * magnitude of line2
        /// assumes that the line vectors have been origin transformed
        /// </summary>
        /// <param name="originTransLine1">First line</param>
        /// <param name="originTransLine2">Second line</param>
        /// <returns>sine of angle between lines, 0 only if the lines are parallel</returns>
        public static double SineOfAngleBetween(Vector2 originTransLine1, Vector2 originTransLine2)
        {
            double cross = Vector2.CrossProduct(originTransLine1, originTransLine2);
            return cross / Math.Abs(originTransLine1.Size() * originTransLine2.Size());
        }

        /// <summary>
        /// Cross product from lines start and end points
        /// </summary>
        /// <param name="l1Start">line 1 start point</param>
        /// <param name="l1End">line 1 end point</param>
        /// <param name="l2Start">line 2 start point</param>
        /// <param name="l2End">line 2 end point</param>
        /// <returns>sine of angle between lines, 0 if the lines are parallel</returns>
        public static double SineOfAngleBetween(Vector2 l1Start, Vector2 l1End, Vector2 l2Start, Vector2 l2End)
        {
            return SineOfAngleBetween(l1End - l1Start, l2End - l2Start);
        }

        public static double SineOfHalfAngleBetween(Vector2 originTransLine1, Vector2 originTransLine2)
        {
            var cross = SineOfAngleBetween(originTransLine1, originTransLine2);
            return Math.Sin(0.5 * Math.Asin(cross));
        }

        /// <summary>
        /// transforms line so that its start point is 0,0
        /// </summary>
        /// <param name="start">line start</param>
        /// <param name="end">line end</param>
        /// <returns>transformed line with start point 0,0</returns>
        public static Vector2 OriginTransformLine(Vector2 start, Vector2 end)
        {
            return end - start;
        }

        public static Vector2 PointOnParameterizedLine(Vector2 lineStart, Vector2 lineEnd, double tVal)
        {
            var parameterVec = DistFromAPointToALine.OriginTransformedLineVector(lineStart, lineEnd);
            var point = lineStart + (parameterVec * tVal);
            return point;
        }

        public static bool CheckIfPointIsOnLineSegment(Vector2 lineStart, Vector2 lineEnd, Vector2 point)
        {
            // check if colinear
            var AB = OriginTransformLine(lineStart, lineEnd);
            var AC = OriginTransformLine(lineStart, point);
            var cross = Vector2.CrossProduct(AB, AC);
            if (cross == 0)
                return false;

            // check if between A and B
            var kAC = AB.DotProduct(AC);
            var kAB = AB.DotProduct(AB);
            if( 0.0 < kAC && kAC < kAB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
