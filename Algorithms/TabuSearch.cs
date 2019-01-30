using System;
using System.Collections.Generic;
using System.Linq;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public class TabuSearch : Algorithm
    {
        readonly int Neighbours;

        public TabuSearch(int neighbours = 4, Location initial = null, int rlc = DEFAULT_RLC, int path_length = DEFAULT_PATH_LENGTH) : base(initial, rlc, path_length)
        {
            Neighbours = neighbours;
        }

        public override int Start(int iterations)
        {
            var tabu = new TabuList();
            List<Location> candidate = GreedyRandomizedConstruction(), solution = candidate;
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 1; j < PathLength-1; j++)
                {
                    List<Location> neighbourhood = candidate[j].Closest();
                    Location location = candidate[j];
                    int k = 0, h = 0;
                    while (k < Neighbours)
                    {
                        Location neighbour = neighbourhood[h++];
                        if (location == neighbour || candidate.Contains(neighbour) || tabu.Contains(location, neighbour)) continue;
                        candidate.RemoveAt(j);
                        candidate.Insert(j, neighbour);
                        tabu.Add(location, neighbour);
                        k++;
                        if (PathCost(candidate) < PathCost(solution)) solution = new List<Location>(candidate);

                    }
                }
            }
            return PathCost(solution);
        }
    }

    class TabuList
    {
        Dictionary<Move, int> list = new Dictionary<Move, int>();
        public const int DEFAULT_T = 5, MAX_SIZE = 10;

        public void Add(Location item1, Location item2)
        {
            list.Add(new Move(item1, item2), DEFAULT_T);
        }

        public void Add(Move move)
        {
            list.Add(move, DEFAULT_T);
            if (list.Count == 500) list.Remove(list.Keys.First());
        }

        public bool Contains(Location item1, Location item2)
        {
            return list.ContainsKey(new Move(item1, item2));
        }

        public void Next()
        {
            var keys = new List<Move>(list.Keys);
            foreach (var i in keys)
            {
                list[i] -= 1;
                if (list[i] == 0) list.Remove(i);
            }
        }
    }

    class Move
    {
        public readonly Location Item1, Item2;

        public Move(Location item1, Location item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return Equals((Move)obj);
        }

        public bool Equals(Move other)
        {
            return other.Item1.Equals(Item1) && other.Item2.Equals(Item2);
        }

        public override int GetHashCode()
        {
            return Item1.GetHashCode() ^ Item2.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Item1} - {Item2})";
        }
    }
}
