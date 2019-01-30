using System;
using System.Diagnostics;
using Metaheuristics.Algorithms;
using Metaheuristics.Models;

namespace Metaheuristics
{
    class Metaheuristics
    {
        public const int ITERATIONS_GRASP = 10;
        public const int ITERATIONS_SA = 10;
        public const int ITERATIONS_TS = 10;
        public const int ITERATIONS_VNS = 10;

        public const int K_MAX = 3;
        public const int RLC = 2;

        public static readonly string FILE_CATEGORY = @"../../Data/Categories.csv";
        public static readonly string FILE_LOCATION = @"../../Data/Locations.csv";
        public static readonly string FILE_DISTANCES = @"../../Data/Distances.csv";

        static Stopwatch stopWatch = new Stopwatch();

        public static void Main(string[] args)
        {
            Utils.Parse.CSV<Category>(FILE_CATEGORY);
            Utils.Parse.CSV<Location>(FILE_LOCATION);
            Distances.Parse(FILE_DISTANCES);

            GRASP GRASP = new GRASP(Location.Instance("Plaza del Adelantado"));
            SimulatedAnnealing SA = new SimulatedAnnealing(50, .05, Location.Instance("Plaza del Adelantado"));
            TabuSearch TS = new TabuSearch(7, Location.Instance("Plaza del Adelantado"));
            VariableNeighborhoodSearch VNS = new VariableNeighborhoodSearch(5, Location.Instance("Plaza del Adelantado"));

            Console.WriteLine("GRASP:");
            Run(GRASP, 20);
            Console.WriteLine("\nSA:");
            //Run(SA, 20);
            Console.WriteLine("\nTS:");
            Run(TS, 20);
            //Console.WriteLine("\nVNS:");
            //Run(VNS, 20);

        }

        static void Run(Algorithm algorithm, int tests)
        {
            int sum = 0;
            for (int i = 1; i <= tests; i++)
            {
                stopWatch.Start();
                int result = algorithm.Start(ITERATIONS_SA);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"Execution = {i}, Cost = {result} Minutes, CPU = {ts.ToString("ss'.'fffffff")} Seconds");
                stopWatch.Reset();
                sum += result;
            }
            Console.WriteLine($"Mean = {sum / tests}");
        }
    }
}
