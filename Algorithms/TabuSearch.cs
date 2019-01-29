using System;
using System.Collections.Generic;
using System.Linq;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public class TabuSearch : Algorithm
    {
        int[,] Matrix;
        int Neighbors = 4;

        public TabuSearch(Location initial = null, int rlc = DEFAULT_RLC, int path_length = DEFAULT_PATH_LENGTH) : base(initial, rlc, path_length)
        {
            Matrix = new int[Location.Instances.Count,Location.Instances.Count];
        }

        public override int Start(int iterations)
        {
            List<Location> solution = GreedyRandomizedConstruction();
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < PathLength; j++)
                {
                    //List<Location> neighborhood = AvailableNeighbors(bestCandidate, solutionInitial[j]);

                }
            }
            return PathCost(solution);
        }

        List<Location> AvailableNeighbors(List<Location> solution, Location location)
        {
            return Candidates(solution).Keys.ToList().GetRange(0, Neighbors);
        }



        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
}
