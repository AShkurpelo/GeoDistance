
namespace GeoDistance
{
    public static class CityPreloadedData
    {
        public static readonly string[] CityNames = GetCityNames();
        public static readonly int[][] CityConnections = GetCityConnections();
        public static readonly string Country = "Portugal";

        static string[] GetCityNames()
        {
            var cities = new string[15];
            cities[0] = "Porto";
            cities[1] = "Viana do Castelo";
            cities[2] = "Braganca";
            cities[3] = "Guarda";
            cities[4] = "Viseu";
            cities[5] = "Coimbra";
            cities[6] = "Peniche";
            cities[7] = "Evora";
            cities[8] = "Setubal";
            cities[9] = "Silves";
            cities[10] = "Tavira";
            cities[11] = "Sagres";
            cities[12] = "Fatima";
            cities[13] = "Tomar";
            cities[14] = "Lisbon";
            return cities;
        }

        static int[][] GetCityConnections()
        {
            var connections = new int[15][];
            connections[0] = new[] { 1, 2, 4, 5 };
            connections[1] = new int[0];
            connections[2] = new[] { 3, 4 };
            connections[3] = new[] { 4, 5, 6, 7, 14 };
            connections[4] = new[] { 5, 6, 13 };
            connections[5] = new[] { 6, 12, 13 };
            connections[6] = new[] { 12, 13, 14 };
            connections[7] = new[] { 8, 9, 10, 12, 13 };
            connections[8] = new[] { 9, 10, 14 };
            connections[9] = new[] { 10, 11 };
            connections[10] = new int[0];
            connections[11] = new int[0];
            connections[12] = new[] { 13, 14 };
            connections[13] = new[] { 14 };
            connections[14] = new int[0];
            return connections;
        }
    }
}
