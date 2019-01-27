using System;
using System.Collections.Generic;
using System.Linq;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public abstract class Algorithm
    {
        protected readonly int RLC = 3;
        protected readonly Random Random = new Random();
        protected Location Initial;
        protected int PathLength = 30;

        public abstract int Start(int iterations);

        public int Start(int iterations, Location initial, int path_length = 10)
        {
            Initial = initial;
            PathLength = path_length;
            return Start(iterations);
        }

        protected Location RandomRLCMember(List<Location> candidates)
        {
            if (candidates.Any())
                return candidates[Random.Next(candidates.Count)];
            return null;
        }

        protected List<Location> BuildRLC(List<Location> solution)
        {
            List<Location> locations = Candidates(solution).Keys.ToList();
            if (locations.Count >= RLC)
                return locations.GetRange(0, RLC);
            if (locations.Count > 0)
                return locations.GetRange(0, locations.Count);
            return new List<Location>();
        }

        protected Dictionary<Location, int> Candidates(List<Location> solution)
        {
            Dictionary<Location, int> candidates = new Dictionary<Location, int>();
            foreach (Location location in Location.Instances)
                if (!solution.Contains(location))
                    candidates.Add(location, PathCost(solution, location));
            return candidates.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        protected int PathCost(List<Location> locations)
        {
            int cost = 0;
            for (int i = 0; i < locations.Count - 1; i++)
                cost += locations[i].Distances[locations[i + 1]];
            return cost;
        }

        protected int PathCost(List<Location> locations, Location location)
        {
            return PathCost(locations) + locations.Last().Distances[location];
        }
    }
}
