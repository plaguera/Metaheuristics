﻿using System.Collections.Generic;
using System.Linq;

namespace Metaheuristics.Models
{
    public class Location
    {
        public static List<Location> Instances = new List<Location>();

        public static Location Instance(int id)
        {
            foreach (Location location in Instances)
                if (location.ID == id) return location;
            return null;
        }

        public static Location Instance(string name)
        {
            foreach (Location location in Instances)
                if (location.Name.Equals(name)) return location;
            return null;
        }

        public static Location Instantiate(int id, double latitude, double longitude, string name, int category)
        {
            //if (Instance(name) != null) return null; 
            Instances.Add(new Location(id, latitude, longitude, name, category));
            return Instances.Last();
        }

        public Category Category { get; private set; }
        public Coordinate Coordinate { get; private set; }
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Dictionary<Location, int> Distances { get; private set; }

        Location(int id, double latitude, double longitude, string name, int category) : this(id, new Coordinate(latitude, longitude), name, Category.Instance(category)) {}

        Location(int id, Coordinate coordinate, string name, Category category)
        {
            ID = id;
            Coordinate = coordinate;
            Name = name;
            Category = category;
            Distances = new Dictionary<Location, int>();
        }

        public void AddDistance(Location location, int distance)
        {
            Distances.Add(location, distance);
            Edge.Instantiate(this, location, distance);
        }

        public Location Closest(List<Location> locations)
        {
            Location closest = null;
            int distance = int.MaxValue;
            foreach (KeyValuePair<Location, int> pair in Distances)
                if (!locations.Contains(pair.Key) && pair.Value < distance)
                    closest = pair.Key;
            return closest;
        }

        public Location Closest()
        {
            Location closest = null;
            int distance = int.MaxValue;
            foreach (KeyValuePair<Location, int> pair in Distances)
                if (pair.Value < distance)
                {
                    closest = pair.Key;
                    distance = pair.Value;
                }
            return closest;
        }

        public List<Location> Closest(int amount, List<Location> locations)
        {
            var ordered = Distances.OrderBy((arg) => arg.Value);
            List<Location> result = new List<Location>();
            foreach (KeyValuePair<Location, int> location in ordered)
            {
                if (!locations.Contains(location.Key)) result.Add(location.Key);
                if (result.Count == amount) break;
            }
            return result;
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToStringLong()
        {
            return $"<Location [{GetHashCode()}]: ID = {ID}, Name = {Name}, Coordinates = {Coordinate}, Category = {Category}>";
        }
    }
}
