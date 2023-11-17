using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamEmanuelssonsRecept.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Recipe> Recipes { get; set; }

        public Category(string name)
        {
            Name = name;
            Recipes = new List<Recipe>();
        }
    }
}
