using System.IO;

namespace Metaheuristics.Utils
{
    public static class Parse
    {
        public static void CSV<T>(string path)
        {
            StreamReader file = new StreamReader(path);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (typeof(T) == typeof(Models.Category)) Category(line);
                if (typeof(T) == typeof(Models.Location)) Location(line);
            }
        }

        static void Category(string line)
        {
            string[] tokens = line.Split(';');
            Models.Category.Instantiate(int.Parse(tokens[0]), tokens[1]);
        }

        static void Location(string line)
        {
            string[] tokens = line.Split(';');
            Models.Location.Instantiate(int.Parse(tokens[0]),
                                        double.Parse(tokens[1]),
                                        double.Parse(tokens[2]),
                                        tokens[3],
                                        int.Parse(tokens[4]));
        }
    }
}
