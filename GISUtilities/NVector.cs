using System;

namespace GISUtilities
{
    /// <summary>
    /// Implementation of NVector and operations
    /// </summary>
    public class NVector
    {
        public Vector3 nVec { get; set; }

        public NVector()
        {
            nVec = new Vector3();
            nVec.x = 0.0;
            nVec.y = 0.0;
            nVec.z = 0.0;
        }

        public NVector(double x, double y, double z)
        {
            nVec = new Vector3();
            nVec.x = x;
            nVec.y = y;
            nVec.z = z;
        }

        public NVector(Vector3 vec)
        {
            nVec = vec;
        }

        /// <summary>
        /// Constructor with longitude first to ease input from x-y systems where
        /// x = longitude and y = latitude
        /// </summary>
        /// <param name="longitude">decimal representation of longitude</param>
        /// <param name="latitude">decimal representation of latitude</param>
        public NVector(double longitude, double latitude)
        {
            nVec = new Vector3();
            SetLongitudeLatitude(longitude, latitude);
        }

        public NVector(LonLat geolocation)
        {
            nVec = new Vector3();
            SetLongitudeLatitude(geolocation.Longitude, geolocation.Latitude);
        }

        public static NVector operator +(NVector nv1, NVector nv2)
        {
            return new NVector(nv1.nVec + nv2.nVec);
        }

        public static NVector operator -(NVector nv1, NVector nv2)
        {
            return new NVector(nv1.nVec - nv2.nVec);
        }

        public static NVector Normalized(NVector vec)
        {
            Vector3 vec2 = vec.nVec.Normalize();
            NVector retVec = new NVector(vec2.x, vec2.y, vec2.z);
            return retVec;
        }

        public void Normalize()
        {
            this.nVec = nVec.Normalize();
        }

        public double GetLatitude()
        {
            return Utilities.ToDegrees(Math.Atan2(nVec.z, Math.Sqrt((nVec.x * nVec.x) + (nVec.y * nVec.y))));
        }

        public double GetLongitude()
        {
            return Utilities.ToDegrees(Math.Atan2(nVec.y, nVec.x));
        }

        public LonLat GetLonLat()
        {
            LonLat geolocation = new LonLat(this.GetLongitude(), this.GetLatitude());
            return geolocation;
        }

        private void SetLongitudeLatitude(double longitude, double latitude)
        {
            double latitudeRadians = Utilities.ToRadians(latitude);
            double longitudeRadians = Utilities.ToRadians(longitude);
            double cosLatitude = Math.Cos(latitudeRadians);
            double cosLongitude = Math.Cos(longitudeRadians);
            double sinLatitude = Math.Sin(latitudeRadians);
            double sinLongitude = Math.Sin(longitudeRadians);

            nVec.x = cosLatitude * cosLongitude;
            nVec.y = cosLatitude * sinLongitude;
            nVec.z = sinLatitude;

            Normalize();
        }
    }
}
