using System;
using System.Collections.Generic;
using Metaheuristics.Models;

namespace Metaheuristics.Algorithms
{
    public class SimulatedAnnealing : GRASP
    {
        readonly double Temperature, CoolingRate;

        public SimulatedAnnealing(  int temperature = 50, double cooling_rate = .05,
                                    Location initial = null, int rlc = DEFAULT_RLC, int path_length = DEFAULT_PATH_LENGTH)
                                    : base(initial, rlc, path_length)
                                    { Temperature = temperature; CoolingRate = cooling_rate; }

        public override int Start(int iterations)
        {
            double temp = Temperature;
            List<Location> solution = GreedyRandomizedConstruction();
            do
            {
                for (int i = 0; i < iterations; i++)
                {
                    List<Location> solution_ = GreedyRandomizedConstruction();
                    if (PathCost(solution_) < PathCost(solution)) solution = solution_;
                    else if (Random.NextDouble() < Math.Exp(-(PathCost(solution_) - PathCost(solution)) / temp))
                        solution = solution_;
                }
                temp *= 1 - CoolingRate;
            } while (temp > 0.01);

            return PathCost(solution);
        }
    }
}
