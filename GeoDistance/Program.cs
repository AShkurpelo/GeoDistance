using System;
using System.Linq;

namespace GeoDistance
{
    class Program
    {
        static void Main()
        {
            var cities = CityPreloadedData.CityNames;
            var cityConnections = CityPreloadedData.CityConnections;
            var country = CityPreloadedData.Country;
            var (roadDistanceMatrix, geographicDistanceMatrix) = GoogleDistance.GetCityDistanceMatrices(cities, country, cityConnections);
            while (true)
            {
                PrintCities(cities);
                Console.Write("Origin city number: ");
                var parsed = int.TryParse(Console.ReadLine(), out var origin);
                Console.Write("Destination city number: ");
                parsed &= int.TryParse(Console.ReadLine(), out var destination);
                origin--;
                destination--;
                if (parsed)
                {
                    var (greedyResultDistance, greedyResultPath) =
                        HeuristicAlgorithm.GreedyPath(roadDistanceMatrix, geographicDistanceMatrix, origin,
                            destination);
                    var (aStarResultDistance, aStarResultPath) =
                        HeuristicAlgorithm.AStarPath(roadDistanceMatrix, geographicDistanceMatrix, origin,
                            destination);
                    Console.WriteLine($"{cities[origin]} - {cities[destination]}:");
                    Console.WriteLine("  Greedy algorithm:");
                    Console.WriteLine($"   Road distance: {Math.Round(greedyResultDistance, 3)}km");
                    Console.WriteLine($"   Path: {string.Join(" -> ", greedyResultPath.Select(el => cities[el]))}");
                    Console.WriteLine("  A* algorithm:");
                    Console.WriteLine($"   Road distance: {Math.Round(aStarResultDistance, 3)}km");
                    Console.WriteLine($"   Path: {string.Join(" -> ", aStarResultPath.Select(el => cities[el]))}");
                }
                else
                {
                    Console.WriteLine("Input can't be parsed.");
                }
                var ans = String.Empty;
                while (ans != "y" && ans != "n")
                {
                    Console.WriteLine("\r\nRepeat? y - yes; n - no, exit");
                    ans = Console.ReadLine().ToLower().Trim();
                }
                if (ans == "n")
                    break;
            }
        }

        static void PrintCities(string[] cities)
        {
            Console.WriteLine("Cities:");
            for (int i = 0; i < cities.Length; ++i)
            {
                Console.WriteLine($"  {i + 1} - {cities[i]}");
            }
        }
    }
}
