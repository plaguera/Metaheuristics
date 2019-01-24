using System.Collections.Generic;
using System.Linq;

namespace Metaheuristics.Models
{
    public class Category
    {
        public static List<Category> Instances = new List<Category>();

        public static Category Instance(int id)
        {
            foreach (Category category in Instances)
                if (category.ID == id) return category;
            return null;
        }

        public static Category Instantiate(int id, string name)
        {
            Instances.Add(new Category(id, name));
            return Instances.Last();
        }

        public string Name { get; }
        public int ID { get; }

        Category(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString() => $"<Category [{GetHashCode()}]: ID = {ID}, Name = {Name}>";
    }
}
