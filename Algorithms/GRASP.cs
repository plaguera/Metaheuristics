using System;
using System.Collections.Generic;
using System.Linq;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public class GRASP : Algorithm
    {
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
            Initial = null;
            //foreach (Location i in minl)
                //Console.WriteLine(i);
            return PathCost(minl);
        }

        public List<Location> GreedyRandomizedConstruction()
        {
            List<Location> Solution = new List<Location>();
            List<Location> Solutionº;
            if (Initial == null)
            {
                Edge initial = Edge.LowestCost();
                Solution.Add(initial.From);
                Solution.Add(initial.To);
            }
            else
            {
                Solution.Add(Initial);
                Solution.Add(Initial.Closest());
            }

            do
            {
                Solutionº = new List<Location>(Solution);
                Location k = RandomRLCMember(BuildRLC(Solution));

                if (k != null && PathCost(Solution, k) >= PathCost(Solution))
                    Solution.Add(k);

            } while (Solution.Count < PathLength && !Solution.SequenceEqual(Solutionº));

            return Solution;
        }

        protected List<Location> LocalSearch(List<Location> solution)
        {
            List<Location> lc = BuildRLC(solution);
            if (!lc.Any()) return solution;
            List<Location> temp = new List<Location>();
            List<Location> sol = new List<Location>();
            int min = int.MaxValue; 
            int cost = 0;

            foreach (Location candidate in lc)
                foreach (Location location in solution)
                {
                    if (location == Initial) continue;
                    temp = new List<Location>(solution);
                    temp.Remove(location);
                    temp.Add(candidate);
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
