using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Cafe_Repository
{
    public class MenuItem
    {
        //overloaded constructor
        public MenuItem(int number, string name, string description, List<string> ingredients, double price)
        {
            Number = number;
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Price = price;
        }

        //properties
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public double Price { get; set; }
    }
}
