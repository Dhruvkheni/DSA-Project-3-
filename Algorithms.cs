using System;
using System.Collections.Generic;
using System.Linq;
using Project3.GraphLib;

namespace Project3.Algorithms
{
    public static class Influence
    {
        // ----------- BFS (Unweighted) -----------
        public static Dictionary<string, int> BFS(Graph g, string start)
        {
            var dist = g.Vertices.ToDictionary(v => v, v => int.MaxValue);
            Queue<string> q = new Queue<string>();

            dist[start] = 0;
            q.Enqueue(start);

            while (q.Count > 0)
            {
                string current = q.Dequeue();

                foreach (var (n, _) in g.Adj[current])
                {
                    if (dist[n] == int.MaxValue)
                    {
                        dist[n] = dist[current] + 1;
                        q.Enqueue(n);
                    }
                }
            }

            return dist;
        }

        // ----------- Dijkstra (Weighted) -----------
        public static Dictionary<string, double> Dijkstra(Graph g, string start)
        {
            var dist = g.Vertices.ToDictionary(v => v, v => double.PositiveInfinity);
            var visited = g.Vertices.ToDictionary(v => v, v => false);

            dist[start] = 0;

            for (int i = 0; i < g.Count; i++)
            {
                string best = null;
                double bestDist = double.PositiveInfinity;

                foreach (var v in g.Vertices)
                {
                    if (!visited[v] && dist[v] < bestDist)
                    {
                        bestDist = dist[v];
                        best = v;
                    }
                }

                if (best == null) break;

                visited[best] = true;

                foreach (var (n, w) in g.Adj[best])
                {
                    double nd = dist[best] + w;
                    if (nd < dist[n])
                        dist[n] = nd;
                }
            }

            return dist;
        }

        // ----------- Influence score -----------
        public static Dictionary<string, double> CalculateUnweighted(Graph g)
        {
            var scores = new Dictionary<string, double>();
            int n = g.Count;

            foreach (var node in g.Vertices)
            {
                var d = BFS(g, node);
                double sum = d.Values.Where(v => v != int.MaxValue).Sum();
                scores[node] = (n - 1) / sum;
            }

            return scores;
        }

        public static Dictionary<string, double> CalculateWeighted(Graph g)
        {
            var scores = new Dictionary<string, double>();
            int n = g.Count;

            foreach (var node in g.Vertices)
            {
                var d = Dijkstra(g, node);
                double sum = d.Values.Sum();
                scores[node] = (n - 1) / sum;
            }

            return scores;
        }

        public static void PrintBestAndWorst(Dictionary<string, double> scores)
        {
            var sorted = scores.OrderByDescending(x => x.Value).ToList();
            var best = sorted.First();
            var worst = sorted.Last();

            Console.WriteLine($"Most influential node: {best.Key} (score {best.Value:F4})");
            Console.WriteLine($"Least influential node: {worst.Key} (score {worst.Value:F4})");
        }
    }
}
