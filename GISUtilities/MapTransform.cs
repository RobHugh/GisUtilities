using System;

namespace GISUtilities
{
    /// <summary>
    /// A class which encompasses a map transform from 0 lat 0 lon, with methods 
    /// to calculate distances in meters and diagrammatic distances in units based
    /// upon the size of a canvas
    /// </summary>
    public class MapTransform
    {
        private double _circumference;
        private RotationMatrix _rotationMatrix;
        private double _originX;
        private double _originY;
        private double _mapHeight;
        private double _mapWidth;
        private double _canvasHeight;
        private double _canvasWidth;

        public MapTransform()
        {
            _circumference = 0.0;
            _rotationMatrix = new RotationMatrix();
            _rotationMatrix.MakeIdentity();
            _originX = 0;
            _originY = 0;
            _mapHeight = 0;
            _mapWidth = 0;
            _canvasHeight = 0;
            _canvasWidth = 0;
        }

        private void SetCircumference(double circumference)
        {
            _circumference = circumference;
        }

        private void SetRadius(double radius)
        {
            SetCircumference(2 * Math.PI * radius);
        }

        public void SetEarthRadius()
        {
            SetRadius(Utilities.EarthsMeanRadius);
        }

        /// <summary>
        /// Sets up a rotation of the map from (0 Lat, 0 Lon, true north orientation) to the desired
        /// location and orientation. To rotate to a point on the globe with true north orientation
        /// the passed parameters would be SetRotation(0,lat,lon)
        /// </summary>
        /// <param name="xRotateDegrees">Orienation angle in degrees (from true north)</param>
        /// <param name="yRotateDegrees">Latitude angle in degrees</param>
        /// <param name="zRotateDegrees">Longitude angle in degrees</param>
        public void SetRotation(double xRotateDegrees, double yRotateDegrees, double zRotateDegrees)
        {
            RotationMatrix xMatrix = new RotationMatrix();
            RotationMatrix yMatrix = new RotationMatrix();
            RotationMatrix zMatrix = new RotationMatrix();
            xMatrix.MakeRotateX(Utilities.ToRadians(xRotateDegrees));
            // we need to reverse the orientation rotation
            xMatrix.matrix = Matrix3.Transpose(xMatrix.matrix);

            yMatrix.MakeRotateY(Utilities.ToRadians(yRotateDegrees));
            zMatrix.MakeRotateZ(Utilities.ToRadians(zRotateDegrees));
            _rotationMatrix.matrix = (xMatrix.matrix * yMatrix.matrix * zMatrix.matrix);
        }

        /// <summary>
        /// Sets where the center point of the map is in relation to the bottom left
        /// of the map in in meters
        /// </summary>
        /// <param name="originX">center of the map in x direction</param>
        /// <param name="originY">center of the map in y direction</param>
        public void SetMapOrigin(double originX, double originY)
        {
            _originX = originX;
            _originY = originY;
        }

        /// <summary>
        /// Sets the dimensions of the map 
        /// </summary>
        /// <param name="width">width in meters</param>
        /// <param name="height">height in meters</param>
        public void SetMapSize(double width, double height)
        {
            _mapWidth = width;
            _mapHeight = height;
        }

        /// <summary>
        /// Sets the canvas size in units
        /// </summary>
        /// <param name="width">width in units</param>
        /// <param name="height">height in units</param>
        public void SetCanvasSize(double width, double height)
        {
            _canvasWidth = width;
            _canvasHeight = height;
        }

        /// <summary>
        /// Returns a x,y coord distance from the map origin
        /// </summary>
        /// <param name="geoPosition">A lat lon of the position</param>
        /// <returns>a map relative x,y from origin coord in meters</returns>
        public double[] GetMapPosition(LonLat geoPosition)
        {
            return GetMapPosition(geoPosition.Longitude, geoPosition.Latitude);
        }

        /// <summary>
        /// Gets a map position in meters from the origin for the passed in coordinates
        /// </summary>
        /// <param name="longitude">longitude</param>
        /// <param name="latitude">latitude</param> 
        /// <returns>x, y in meters from origin</returns>
        public double[] GetMapPosition(double longitude, double latitude)
        {
            NVector nVec = new NVector(longitude, latitude);
            return GetMapPosition(nVec);
            
        }

        public double[] GetMapPosition(NVector position)
        {
            NVector nVec = _rotationMatrix.Transform(position);
            double lon = nVec.GetLongitude();
            double lat = nVec.GetLatitude();
            double x = lon * _circumference / 360.0;
            double y = lat * _circumference / 360.0;
            return new double[] { x, y };
        }

        /// <summary>
        /// returns a canvas coordinate in units from the top left 
        /// </summary>
        /// <param name="mapPosition">map position in meters (x,y)</param>
        /// <returns>canvas position in units</returns>
        public double[] GetPixelPosition(double[] mapPosition)
        {
            return GetPixelPosition(mapPosition[0], mapPosition[1]);
        }

        public double[] GetPixelPosition(double mapX, double mapY)
        {
            // rotate the map position through 90 degrees to align with the canvas
            // rotation matrix
            var matrixInitializer = new double[,] { { 0.0f, 1.0f }, { -1.0f, 0.0f } };
            Vector2 vec = new Vector2(mapX, mapY);
            Matrix2 rot90 = new Matrix2(matrixInitializer);
            Vector2 mapVec = rot90 * vec;

            double x = (_originX + mapVec.x) * (_canvasWidth / _mapWidth);
            double y = _canvasHeight - ((_originY + mapVec.y) * (_canvasHeight / _mapHeight));

             
            return new double[] { x, y };
        }

    }
}
