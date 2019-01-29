using System;
using System.Collections.Generic;
using System.Linq;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public class VariableNeighborhoodSearch : GRASP
    {
        readonly int kMax;

        public VariableNeighborhoodSearch(  int kmax = 3,
                                            Location initial = null, int rlc = DEFAULT_RLC, int path_length = DEFAULT_PATH_LENGTH)
                                            : base(initial, rlc, path_length)
                                            { kMax = kmax; }

        public override int Start(int iterations)
        {
            List<Location> solution = GreedyRandomizedConstruction();
            List<Location> solution_ = new List<Location>();
            List<Location> solution__ = new List<Location>();
            int cost = PathCost(solution), temp = 0, k = 1;
            for (int i = 0; i < iterations; i++)
            {
                k = 1;
                while (k <= kMax)
                {
                    // SHAKING
                    solution_ = Shake(k, solution);
                    // LOCAL SEARCH
                    solution__ = LocalSearch(solution_);
                    // MOVE OR NOT
                    temp = PathCost(solution__);
                    if (temp < cost)
                    {
                        cost = temp;
                        solution = new List<Location>(solution__);
                        k = 1;
                    }
                    else
                        k++;
                }
            }
            return PathCost(solution);
        }

        public List<Location> Shake(int k, List<Location> x)
        {
            List<Location> result = new List<Location>();
            List<Location> rlc = BuildRLC(x);
            List<Location> s = new List<Location>(x);
            List<int> indexes = new List<int>();
            int add = 0;
            for (int i = 0; i < k && i < rlc.Count && s.Any(); i++)
            {
                Location location = s[Random.Next(1, s.Count-2)];
                indexes.Add(s.IndexOf(location));
                s.Remove(location);
                add++;
            }
            foreach (Location i in s)
                result.Add(i);
            for (int i = 0; i < add; i++)
                result.Add(null);

            for (int i = 0; i < add; i++)
            {
                Location tmp = RandomRLCMember(rlc);
                result.Insert(indexes[i], tmp);
                rlc.Remove(tmp);
            }
            result.RemoveAll((obj) => obj == null);
            return result;
        }
    }

}
