using System;

namespace Metaheuristics
{
    class Metaheuristics
    {
        public static readonly string FILE_CATEGORY = @"../../Data/Categories.csv";
        public static readonly string FILE_LOCATION = @"../../Data/Locations.csv";
        public static readonly string FILE_DISTANCES = @"../../Data/Distances.csv";

        public static void Main(string[] args)
        {
            Utils.Parse.CSV<Models.Category>(FILE_CATEGORY);
            Utils.Parse.CSV<Models.Location>(FILE_LOCATION);
            Models.Distances.Parse(FILE_DISTANCES);
        }
    }
}
