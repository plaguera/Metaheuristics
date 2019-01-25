using System;
using System.Collections.Generic;
using System.IO;

namespace Metaheuristics.Models
{
    public static class Distances
    {
        static int[,] matrix;
        public static void Parse(string path)
        {
            StreamReader file = new StreamReader(path);
            string[] locations = file.ReadLine().Split(';');
            matrix = new int[locations.Length, locations.Length];
            string line;
            int row = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                for (int column = 1; column < tokens.Length; column++)
                {
                    matrix[row, column-1] = int.Parse(tokens[column]);
                    Location A = Location.Instance(tokens[0]);
                    Location B = Location.Instance(locations[column - 1]);
                    Console.WriteLine(locations[column - 1]);
                    A.AddDistance(B, int.Parse(tokens[column]));
                }
                row++;
            }
        }
    }
}
