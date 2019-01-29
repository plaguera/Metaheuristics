using System.Collections.Generic;
using System.Linq;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public class GRASP : Algorithm
    {
        public GRASP(Location initial = null, int rlc = DEFAULT_RLC, int path_length = DEFAULT_PATH_LENGTH) : base(initial, rlc, path_length) {}

        public override int Start(int iterations)
        {
            List<Location> solution = new List<Location>();
            List<Location> minl = new List<Location>();
            int min = int.MaxValue, temp = 0;
            for (int i = 1; i <= iterations; i++)
            {
                solution = GreedyRandomizedConstruction();
                solution = LocalSearch(solution);

                temp = PathCost(solution);
                if (min > temp)
                {
                    min = temp;
                    minl = new List<Location>(solution);
                }
            }
            return PathCost(minl);
        }

        protected List<Location> LocalSearch(List<Location> solution)
        {
            List<Location> lc = BuildRLC(solution);
            if (!lc.Any()) return solution;
            List<Location> temp = new List<Location>();
            List<Location> sol = new List<Location>();
            int min = int.MaxValue, cost = 0;

            foreach (Location candidate in lc)
                foreach (Location location in solution)
                {
                    if (location == Initial) continue;
                    temp = new List<Location>(solution);
                    int i = temp.IndexOf(location);
                    temp.Remove(location);
                    temp.Insert(i, candidate);
                    cost = PathCost(temp);
                    if (min > cost)
                    {
                        sol = new List<Location>(temp);
                        min = cost;
                    }
                }
            return sol;
        }
    }
}
