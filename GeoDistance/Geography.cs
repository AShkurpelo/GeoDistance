using System;
using System.Collections.Generic;
using System.Text;

namespace GeoDistance
{
    public static partial class Geography
    {
        static double GetGeographicDistance(GeoCoordinate pos1, GeoCoordinate pos2)
        {
            double R = 6371;
            double dLat = ToRadian(pos2.Latitude - pos1.Latitude);
            double dLon = ToRadian(pos2.Longitude - pos1.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadian(pos1.Latitude)) * Math.Cos(ToRadian(pos2.Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;
            return d;
        }

        static double ToRadian(double val)
        {
            return (Math.PI / 180) * val;
        }

        public static double[,] GetDistanceMatrix(GeoCoordinate[] coordinates)
        {
            var size = coordinates.Length;
            var matrix = new double[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = i + 1; j < size; ++j)
                {
                    matrix[i, j] = matrix[j, i] = GetGeographicDistance(coordinates[i], coordinates[j]);
                }
            }
            return matrix;
        }
    }
}
