using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace ProductSeeker.Utils.NetTopologySuite
{
    public static class LocationUtils
    {
        /// <summary>
        /// Converts latitude and longitude to a NetTopologySuite Point.
        /// </summary>
        /// <remarks>
        /// If the latitude or longitude is null, or if they are outside the valid ranges (-90 to 90 for latitude, -180 to 180 for longitude), 
        /// it returns null.
        /// </remarks>
        /// <param name="latitude">Latitude of the point.</param>
        /// <param name="longitude">Longitude of the point.</param>
        /// <returns>A Point representing the geographical location.</returns>
        public static Point? ConvertToPoint(double? latitude, double? longitude)
        {
            // Check if latitude and longitude are within valid ranges
            if (latitude == null || longitude == null || 
                latitude < -90 || latitude > 90 || 
                longitude < -180 || longitude > 180)
            {
                return null; 
            }
            
            var lon = longitude.Value;
            var lat = latitude.Value;

            // Create a GeometryFactory with the appropriate SRID (Spatial Reference Identifier)
            // SRID 4326 is commonly used for geographic coordinates (WGS 84)
            //This could be moved to a DI service if needed
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            return geometryFactory.CreatePoint( new Coordinate(lon, lat));
        }

        public static bool AreLocationsClose(Point point1, Point point2, double thresholdInMeters = 100)
        {
            if (point1 == null || point2 == null)
                return false;

            // Calculate the distance in meters between the two points
            var distance = point1.Distance(point2) * 111319.9; // Convert degrees to meters (approximation)

            return distance <= thresholdInMeters;
        }
    }
}
