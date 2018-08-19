using System;
using System.Dynamic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace GeoDistance
{
    class GoogleDistance
    {
        public static (double roadDistance, Geography.GeoCoordinate start, Geography.GeoCoordinate end)
            CallGoogleDirectionApi(string origin, string destination)
        {
            origin = string.Join('+', origin.Split(' '));
            destination = string.Join('+', destination.Split(' '));
            var url =
                $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key=AIzaSyCXNzSs72JNSA1REGU31_nago9tSJSYAws";

            string responseFromServer;
            try
            {
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                using (var receiveStream = response.GetResponseStream())
                using (var reader = new StreamReader(receiveStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }
            }
            catch (WebException webExcp)
            {
                Console.WriteLine("A WebException has been caught.");
                Console.WriteLine(webExcp.ToString());
                var status = webExcp.Status;
                if (status == WebExceptionStatus.ProtocolError)
                {
                    Console.Write("The server returned protocol error ");
                    var httpResponse = (HttpWebResponse)webExcp.Response;
                    Console.WriteLine($"{(int)httpResponse.StatusCode} - {httpResponse.StatusCode}");
                }
                return (-1, new Geography.GeoCoordinate(), new Geography.GeoCoordinate());
            }

            dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(responseFromServer);
            if (data.status != "OK")
            {
                Console.WriteLine("WebRequest responce has bad status.");
                Console.WriteLine($"\tstatus : {data.status}");
                Console.WriteLine($"\terror_message : {data.error_message}");
                return (-1, new Geography.GeoCoordinate(), new Geography.GeoCoordinate());
            }
            double road = data.routes[0].legs[0].distance.value / 1000.0;
            dynamic start = data.routes[0].legs[0].start_location;
            dynamic end = data.routes[0].legs[0].end_location;

            return (road, new Geography.GeoCoordinate(start.lat, start.lng), new Geography.GeoCoordinate(end.lat, end.lng));
        }

        public static (double[,] roadDistanceMatrix, double[,] geographicDistanceMatrix) GetCityDistanceMatrices(
            string[] cities, string country,
            int[][] cityConnections)
        {
            var size = cities.Length;
            var roadMatrix = new double[size, size];
            var geoCoordinates = new Geography.GeoCoordinate[size];

            for (int i = 0; i < size; ++i)
            {
                foreach (var j in cityConnections[i])
                {
                    var origin = $"{cities[i]},{country}";
                    var destination = $"{cities[j]},{country}";
                    double roadDistance;
                    (roadDistance, geoCoordinates[i], geoCoordinates[j]) =
                        CallGoogleDirectionApi(origin, destination);
                    if (roadDistance > 0)
                        roadMatrix[j, i] = roadMatrix[i, j] = roadDistance;
                    else
                        return (null, null);
                }
            }

            var geographicMatrix = Geography.GetDistanceMatrix(geoCoordinates);

            return (roadMatrix, geographicMatrix);
        }
    }
}
