using System;
using System.Collections.Generic;
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

            Console.WriteLine("GRASP:");
            RunGRASP(10);
            Console.WriteLine("\nVNS:");
            RunVNS(10);

        }

        static void RunGRASP(int tests)
        {
            GRASP GRASP = new GRASP();
            for (int i = 1; i <= tests; i++)
            {
                stopWatch.Start();
                int result = GRASP.Start(ITERATIONS_GRASP, Location.Instance("Plaza del Adelantado"), 30);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"Execution = {i}, Cost = {result} Minutes, CPU = {ts.ToString("ss'.'fffffff")} Seconds");
                stopWatch.Reset();
            }
        }

        static void RunVNS(int tests)
        {
            VariableNeighborhoodSearch VNS = new VariableNeighborhoodSearch();
            for (int i = 1; i <= tests; i++)
            {
                stopWatch.Start();
                int result = VNS.Start(ITERATIONS_VNS, K_MAX);
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Console.WriteLine($"Execution = {i}, Cost = {result} Minutes, CPU = {ts.ToString("ss'.'fffffff")} Seconds");
                stopWatch.Reset();
            }
        }
    }
}
