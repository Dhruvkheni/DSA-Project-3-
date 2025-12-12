using System;
using System.Collections.Generic;
using System.IO;

namespace Project3.GraphLib
{
    public class Graph
    {
        // string node → list of (neighbour, weight)
        public Dictionary<string, List<(string neighbour, double weight)>> Adj =
            new Dictionary<string, List<(string, double)>>();

        public bool IsWeighted;

        public Graph(bool weighted)
        {
            IsWeighted = weighted;
        }

        public void AddEdge(string a, string b, double w)
        {
            if (!Adj.ContainsKey(a))
                Adj[a] = new List<(string, double)>();

            Adj[a].Add((b, w));
        }

        public IEnumerable<string> Vertices => Adj.Keys;

        public int Count => Adj.Count;

        // ---- UNWEIGHTED CSV (e.g. Alicia,Britney) ----
        public static Graph LoadUnweighted(string file)
        {
            Graph g = new Graph(false);

            foreach (var line in File.ReadLines(file))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var p = line.Split(',');
                string a = p[0].Trim();
                string b = p[1].Trim();

                g.AddEdge(a, b, 1);
                g.AddEdge(b, a, 1);
            }

            return g;
        }

        // ---- WEIGHTED CSV (e.g. A,B,3) ----
        public static Graph LoadWeighted(string file)
        {
            Graph g = new Graph(true);

            bool isFirstLine = true;

            foreach (var line in File.ReadLines(file))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var p = line.Split(',');

                if (isFirstLine && (p[0].ToLower().Contains("node") || p[2].ToLower().Contains("weight")))
                {
                    isFirstLine = false;
                    continue;
                }

                isFirstLine = false;

                string a = p[0].Trim();
                string b = p[1].Trim();
                double w = double.Parse(p[2].Trim());

                g.AddEdge(a, b, w);
                g.AddEdge(b, a, w);
            }

            return g;
        }

    }
}
