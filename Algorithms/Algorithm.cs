using System;
using System.Collections.Generic;
using System.Linq;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public abstract class Algorithm
    {
        // DEFAULTS
        protected const int DEFAULT_RLC = 3, DEFAULT_PATH_LENGTH = 10;

        protected readonly Random Random = new Random();
        protected readonly Location Initial;
        protected readonly int RLC, PathLength;


        protected Algorithm(Location initial = null, int rlc = DEFAULT_RLC, int path_length = DEFAULT_PATH_LENGTH)
        {
            Initial = initial ?? Location.Random();
            PathLength = path_length;
            RLC = rlc;
        }

        public abstract int Start(int iterations);

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

        protected List<Location> GreedyRandomizedConstruction()
        {
            List<Location> Solution = new List<Location>();
            List<Location> Solutionº;
            if (Initial == null) Solution.Add(Edge.LowestCost().From);
            else Solution.Add(Initial);

            do
            {
                Solutionº = new List<Location>(Solution);
                Location k = RandomRLCMember(BuildRLC(Solution));

                if (Solution.Count == PathLength-1)
                    Solution.Add(Solution.First());
                else if (Solution.Count < PathLength && PathCost(Solution, k) >= PathCost(Solution))
                    Solution.Add(k);

            } while (Solution.Count <= PathLength && !Solution.SequenceEqual(Solutionº));

            return Solution;
        }

        public static int PathCost(List<Location> locations)
        {
            int cost = 0;
            for (int i = 0; i < locations.Count - 1; i++)
                cost += locations[i].Distances[locations[i + 1]];
            return cost;
        }

        public static int PathCost(List<Location> locations, Location location)
        {
            return PathCost(locations) + locations.Last().Distances[location];
        }
    }
}
