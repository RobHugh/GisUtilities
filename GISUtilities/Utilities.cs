using System;

namespace GISUtilities
{
    public static class Utilities
    {
        /// <summary>
        /// Mean Earths Radius in meters
        /// </summary>
        public const double EarthsMeanRadius = 6371008.8;
        /// <summary>
        /// Earths radius at the poles in meters
        /// </summary>
        public const double EarthsPolarRadius = 6356752.3142;
        /// <summary>
        /// Earths radius at the equator in meters
        /// </summary>
        public const double EarthsEquatorialRadius = 6378137.0;

        /// <summary>
        /// Calculates the Earths Radius at a given latitude in degrees
        /// </summary>
        /// <param name="latitude">latitude degrees as a signed double</param>
        /// <returns>radius in meters</returns>
        public static double GeoCentricRadius(double latitude)
        {
            double latitudeRadians = ToRadians(latitude);
            double plrSqRadius = EarthsPolarRadius * EarthsPolarRadius;
            double eqSqRadius = EarthsEquatorialRadius * EarthsEquatorialRadius;
            double cosLatitude = Math.Cos(latitudeRadians);
            double sinLatitude = Math.Sin(latitudeRadians);
            double lr = Math.Sqrt((Math.Pow(eqSqRadius * cosLatitude, 2) + Math.Pow(plrSqRadius * sinLatitude, 2))/(Math.Pow(EarthsEquatorialRadius * cosLatitude, 2) + Math.Pow(EarthsPolarRadius * sinLatitude, 2)));
            return lr;
        }

        /// <summary>
        /// Convert degrees to Radians
        /// </summary>
        /// <param name="degrees">degrees as signed double</param>
        /// <returns>radians as double</returns>
        public static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        /// <summary>
        /// Convert Radians to degrees
        /// </summary>
        /// <param name="radians">radians as double</param>
        /// <returns>degrees as signed double</returns>
        public static double ToDegrees(double radians)
        {
            return radians * 180.0 / Math.PI;
        }

        /// <summary>
        /// Calculates the distance in meters between two points on the earths surface defined by n-vectors
        /// </summary>
        /// <param name="start">A n-vector defining the start point of a path</param>
        /// <param name="end">A n-vector defining the end point of a path</param>
        /// <returns>A double defining the distance between 2 points in meters</returns>
        public static double DistanceBetweenPoints(NVector start, NVector end)
        {
            double angle = AngleTo(start, end);
            return EarthsMeanRadius * angle;
        }

        /// <summary>
        /// Calculates the angle between two nvectors
        /// </summary>
        /// <param name="from">the start nvector</param>
        /// <param name="to">the end nvector</param>
        /// <returns>angle in radians</returns>
        public static double AngleTo(NVector from, NVector to)
        {
            double sinTheta = Vector3.CrossProduct(from.nVec, to.nVec).Size();
            double cosTheta = from.nVec.DotProduct(to.nVec);
            return Math.Atan2(sinTheta, cosTheta);
        }

        /// <summary>
        /// Calculates whether two n-vector great circles intersect within a certain distance of the point of intersection
        /// </summary>
        /// <param name="path1Start">start of the first path as an n-vector</param>
        /// <param name="path1End">end of the first path as an n-vector</param>
        /// <param name="path2Start">start of the second path as an n-vector</param>
        /// <param name="path2End">end of the second path as an n-vector</param>
        /// <param name="centerToIntersectDistance">the distance from the intersection point for a valid path intersection</param>
        /// <returns>true if two line segments intersect</returns>
        public static bool SegmentsIntersect(NVector path1Start, NVector path1End,
                                                NVector path2Start, NVector path2End, 
                                                out double centerToIntersectDistance)
        {
            // find center point of cones and test that intersection distance to center is less than cones to center
            NVector intersection = Intersection(path1Start, path1End, path2Start, path2End);
            if (intersection == null)
            {
                centerToIntersectDistance = 0.0;
                return false;
            }
                
            NVector centerPoint = MidPoint(path2Start, path2End);
            // calculate the distances of arc and arc points to intersection
            double length1T = DistanceBetweenPoints(path1Start, path1End);
            double length1A = DistanceBetweenPoints(path1Start, intersection);
            double length1B = DistanceBetweenPoints(path1End, intersection);
            double length2T = DistanceBetweenPoints(path2Start, path2End);
            double length2A = DistanceBetweenPoints(path2Start, intersection);
            double length2B = DistanceBetweenPoints(path2End, intersection);
            // calculate the distance of the intersection from the centerpoint
            centerToIntersectDistance = DistanceBetweenPoints(centerPoint, intersection);

            // epsilon is to deal with inaccuracy of IN (set to 30cm)
            const double epsilon = 0.3;
            double length1Delta = length1T - length1A - length1B;
            double length2Delta = length2T - length2A - length2B;

            // check that intersection lies on both great arcs
            if ((length1Delta < epsilon && length1Delta > -epsilon) && (length2Delta < epsilon && length2Delta > -epsilon))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calculates the normal to a great circle plane through the earths center
        /// </summary>
        /// <param name="start">A n-vector defining the start point of a path</param>
        /// <param name="end">A n-vector defining the end point of a path</param>
        /// <returns>A vector which defines the normal of a plane of a great circle through the earths center</returns>
        public static NVector GreatCircle(NVector start, NVector end)
        {
            NVector nVec = new NVector(Vector3.CrossProduct(start.nVec, end.nVec).Normalize());
            return nVec;
        }

        /// <summary>
        /// Calculates the midpoint of two n-vectors as an n-vector
        /// </summary>
        /// <param name="start">A n-vector defining the start point of a path</param>
        /// <param name="end">>A n-vector defining the end point of a path</param>
        /// <returns>A normalized vector defining the mid point of the path</returns>
        public static NVector MidPoint(NVector start, NVector end)
        {
            NVector nVec= new NVector((start.nVec + end.nVec).Normalize());
            return nVec;
        }

        /// <summary>
        /// Given two line segments as n-vector points, calculates the point of their intersection
        /// </summary>
        /// <param name="path1Start">The start of the first path</param>
        /// <param name="path1End">The end of the first path</param>
        /// <param name="path2Start">The start of the second path</param>
        /// <param name="path2End">The end of the second path</param>
        /// <returns>The point of intersection of the two lines as an n-vector, or null if lines are parallel</returns>
        public static NVector Intersection(NVector path1Start, NVector path1End, NVector path2Start, NVector path2End)
        {
            // calculate the great circle paths
            NVector c1 = GreatCircle(path1Start, path1End);
            NVector c2 = GreatCircle(path2Start, path2End);

            // there are 2 antipodal intersection candidate points
            NVector i1 = new NVector(Vector3.CrossProduct(c1.nVec, c2.nVec).Normalize());
            NVector i2 = new NVector(Vector3.CrossProduct(c2.nVec, c1.nVec).Normalize());

            // select the nearest intersection to the midpoint of all points
            var mid = (path1Start.nVec + path2Start.nVec + path1End.nVec + path2End.nVec).Normalize();
            var dp = mid.DotProduct(i1.nVec);
            var ep = 1.0E-12;
            if (dp >= 0 - ep && dp <= 0 + ep)
                return null;
            else
                return dp > 0 ? i1 : i2;
        }

        /// <summary>
        /// Calculates a new NVector position from a given start point, bearing and distance
        /// </summary>
        /// <param name="startPoint">start point as nVector</param>
        /// <param name="angularDistance">angular distance</param>
        /// <param name="bearingRadians">bearing in radians</param>
        /// <returns>new nVector point</returns>
        public static NVector DestinationPoint(NVector startPoint, double angularDistance, double bearingRadians)
        {
            // N = polevector
            // dE = eastvector = N x start
            // dN = northvector = start x dE
            // d = directionvector in dir of bearing = dN*cos(bearing) + dE*sin(bearing)
            // dP = destinationpoint = start*cos(angulardistance) + d*sin(angulardistance)
            NVector poleVector = new NVector(0, 0, 1);            

            var dE = Vector3.CrossProduct(poleVector.nVec, startPoint.nVec).Normalize();
            var dN = Vector3.CrossProduct(startPoint.nVec, dE).Normalize();

            var d = (dN * Math.Cos(bearingRadians)) + (dE * Math.Sin(bearingRadians));
            var dP = ((startPoint.nVec * Math.Cos(angularDistance)) + (d * Math.Sin(angularDistance))).Normalize();
            return new NVector(dP);
        }

        public static NVector DestinationPoint(NVector startPoint, double distance, double bearingDegrees, double latitude)
        {
            double angularDistance = distance / GeoCentricRadius(latitude);
            double bearingRadians = ToRadians(bearingDegrees);
            return DestinationPoint(startPoint, angularDistance, bearingRadians);
        }

        /// <summary>
        /// Calculates new LonLat position based on a start point, bearing and distance
        /// </summary>
        /// <param name="startPosition">LonLat of start point</param>
        /// <param name="distance">distance to travel in meters</param>
        /// <param name="bearing">bearing to travel in degrees</param>
        /// <returns>LonLat of destination</returns>
        public static LonLat DestinationPoint(LonLat startPosition, double distance, double bearingDegrees)
        {
            double angularDistance = distance / GeoCentricRadius(startPosition.Latitude);
            double bearingRadians = ToRadians(bearingDegrees);
            NVector nVec = new NVector(startPosition);
            nVec = DestinationPoint(nVec, angularDistance, bearingRadians);
            return nVec.GetLonLat();
        }

        
    }
}
