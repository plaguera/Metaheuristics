using System;
using System.Collections.Generic;
using System.IO;

namespace Metaheuristics.Models
{
    public class Distances
    {
        public static Distances Instance;

        public static void Parse(string path)
        {
            StreamReader file = new StreamReader(path);
            string[] names = file.ReadLine().Split(';');
            int[,] matrix = new int[names.Length, names.Length];
            string line;
            int row = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(';');
                Location A = Location.Instance(tokens[0]);
                for (int column = 1; column < tokens.Length; column++)
                {
                    matrix[row, column-1] = int.Parse(tokens[column]);
                    Location B = Location.Instance(names[column - 1]);
                    if (A == B) continue;
                    A.AddDistance(B, int.Parse(tokens[column]));
                }
                row++;
            }
            Instance = new Distances(names, matrix);
        }

        public int[,] matrix;
        public string[] names;

        Distances(string[] names_, int[,] matrix_)
        {
            names = names_;
            matrix = matrix_;
        }

        public int this[string row, string column]
        {
            get
            {
                int i = 0, j = 0;
                for (i = 0; i < names.Length; i++)
                    if (names[i].Equals(row))
                        break;
                for (j = 0; j < names.Length; j++)
                    if (names[j].Equals(column))
                        break;

                return matrix[i,j];
            }
        }

        public int[] this[string row]
        {
            get
            {
                int i = 0, j = 0;
                int[] result = new int[names.Length];
                for (i = 0; i < names.Length; i++)
                    if (names[i].Equals(row))
                    {
                        for (j = 0; j < names.Length; j++)
                            result[j] = matrix[i, j];
                        break;
                    }
                return result;
            }
        }

        public void GRASP(string[] path)
        {

        }


    }
}
