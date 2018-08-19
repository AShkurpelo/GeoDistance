using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Priority_Queue;

namespace GeoDistance
{
    public static class HeuristicAlgorithm
    {
        public static (double, List<int> path) GreedyPath(double[,] matrix, double[,] distances, int start, int end)
        {
            var except = new HashSet<int> { start };
            var path = new List<int> { start };
            var cur = start;
            double value = 0;
            while (path.Count != 0)
            {
                double minDistance = double.MaxValue;
                int next = -1;
                for (int i = 0; i < matrix.GetLength(1); ++i)
                {
                    if (matrix[cur, i] == 0 || minDistance <= distances[i, end] || except.Contains(i))
                        continue;
                    minDistance = distances[i, end];
                    next = i;
                }
                if (next != -1)
                {
                    path.Add(next);
                    value += matrix[cur, next];
                    cur = next;
                    except.Add(cur);
                    if (cur == end)
                    {
                        return (value, path);
                    }
                }
                else
                {
                    var removed = path.Last();
                    path.RemoveAt(path.Count - 1);
                    cur = path.Last();
                    value -= matrix[cur, removed];
                }
            }
            return (-1, null);
        }

        public static (double, List<int> path) AStarPath(double[,] matrix, double[,] distances, int start, int end)
        {
            var frontier = new SimplePriorityQueue<int>();
            frontier.Enqueue(start, 0);
            var cameFrom = new int[matrix.GetLength(1)];
            Array.Fill(cameFrom, -1);

            var visited = new HashSet<int> { start };
            var path = new List<int> { start };
            var cur = start;
            double value = 0;
            while (path.Count != 0)
            {
                double minDistance = double.MaxValue;
                int next = -1;
                for (int i = 0; i < matrix.GetLength(1); ++i)
                {
                    var curDistance = distances[i, end] + matrix[cur, i];
                    if (matrix[cur, i] == 0
                        || minDistance <= curDistance
                        || visited.Contains(i))
                        continue;
                    minDistance = curDistance;
                    next = i;
                }
                if (next != -1)
                {
                    path.Add(next);
                    value += matrix[cur, next];
                    cur = next;
                    visited.Add(cur);
                    if (cur == end)
                    {
                        return (value, path);
                    }
                }
                else
                {
                    var removed = path.Last();
                    path.RemoveAt(path.Count - 1);
                    cur = path.Last();
                    value -= matrix[cur, removed];
                }
            }
            return (-1, null);
        }
    }
}
