namespace GISUtilities
{ 
    /// <summary>
    /// Simple POD to contain a Latitude/Longitude decimal double value
    /// </summary>
    public class LonLat
    {
        /// <summary>
        /// Longitude as a signed decimal value
        /// </summary>
        public double Longitude { get; set; }

        // Properties
        /// <summary>
        /// Latitude as a signed decimal value
        /// </summary>
        public double Latitude { get; set; }


        public LonLat()
        {
            Longitude = 0.0;
            Latitude = 0.0;            
        }

        /// <summary>
        /// Constructs a coordinate based on the latitude and longitude input
        /// </summary>
        /// <param name="lat">Latitude input as a signed double</param>
        /// <param name="lon">Longitude input as a signed double</param>
        public LonLat(double lon, double lat)
        {
            Longitude = lon;
            Latitude = lat;            
        }

        /// <summary>
        /// Equals method checks latitude and longitude parameters for equality
        /// </summary>
        /// <param name="pos">position to compare with this one</param>
        /// <returns>true if both latitude and longitude are equal to compared LonLat</returns>
        public bool Equals(LonLat pos)
        {
            if(this.Longitude == pos.Longitude && this.Latitude == pos.Latitude)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -1416534245;
            hashCode = hashCode * -1521134295 + Longitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Latitude.GetHashCode();
            return hashCode;
        }
    }
}
