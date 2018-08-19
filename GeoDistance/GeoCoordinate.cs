
namespace GeoDistance
{
    public static partial class Geography
    {
        public struct GeoCoordinate
        {
            public double Latitude { get; }
            public double Longitude { get; }

            public GeoCoordinate(double latitude, double longitude)
            {
                this.Latitude = latitude;
                this.Longitude = longitude;
            }

            public override string ToString()
            {
                return $"{Latitude}, {Longitude}";
            }
        }
    }
}
