using System;
using System.Collections.Generic;
using System.Linq;

namespace Metaheuristics.Models
{
    public class Edge
    {
        public static List<Edge> Instances = new List<Edge>();

        public static Edge Instance(Location from, Location to)
        {
            foreach (Edge edge in Instances)
                if (edge.From == from && edge.To == to) return edge;
            return null;
        }

        public static Edge Instantiate(Location from, Location to, int cost)
        {
            Instances.Add(new Edge(from, to, cost));
            return Instances.Last();
        }

        public static Edge LowestCost()
        {
            int min = int.MaxValue;
            Edge minEdge = null;
            foreach (Edge edge in Instances)
                if (edge.Cost < min)
                {
                    min = edge.Cost;
                    minEdge = edge;
                }
            return minEdge;
        }

        public Location From { get; private set; }
        public Location To { get; private set; }
        public int Cost { get; private set; }

        Edge(Location from, Location to, int cost)
        {
            From = from;
            To = to;
            Cost = cost;
        }
    }
}
