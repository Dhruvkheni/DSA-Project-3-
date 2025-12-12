using System;
using System.Collections.Generic;
using Project3.GraphLib;
using Project3.Algorithms;

namespace Project3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Project 3: Recognising Key Influencers ===");
            Console.WriteLine("1. Unweighted Social Network");
            Console.WriteLine("2. Weighted Social Network");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                string file = "Data/unweighted_network.csv";
                Console.WriteLine($"Loading file: {file}");

                Graph g = Graph.LoadUnweighted(file);
                var scores = Influence.CalculateUnweighted(g);
                Influence.PrintBestAndWorst(scores);
            }
            else if (choice == "2")
            {
                string file = "Data/weighted_network.csv";
                Console.WriteLine($"Loading file: {file}");

                Graph g = Graph.LoadWeighted(file);
                var scores = Influence.CalculateWeighted(g);
                Influence.PrintBestAndWorst(scores);
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
        }
    }
}
