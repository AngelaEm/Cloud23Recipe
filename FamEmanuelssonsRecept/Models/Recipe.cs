using FamEmanuelssonsRecept.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamEmanuelssonsRecept.Models
{
    public class Recipe
    {
        private double _grade;
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; } = "/Images/middag.png";
        public Category? Category { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        
        public double Grade
        {
            get => _grade;
            set
            {
                if (value >= 0 && value <= 10)
                    _grade = value;
                else
                    _grade = 0;
            }
        }

        public Recipe(string name, string ingredients, string description, double grade)
        {
            Name = name;
            Ingredients = ingredients;
            Description = description;
            Grade = grade;
        }

    }
}
