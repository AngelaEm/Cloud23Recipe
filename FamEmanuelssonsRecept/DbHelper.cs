using FamEmanuelssonsRecept.Db;
using FamEmanuelssonsRecept.Models;
using FamEmanuelssonsRecept.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamEmanuelssonsRecept
{
    public static class DbHelper
    {
        public static Category SelectedCategory { get; set; }
        public static Recipe SelectedRecipe { get; set; }

        public static List<Category> LoadCategories()
        {
            
            var categories = RecipeDbContext._DbContext._Categories.ToList();
            return categories;
        }

        public static List<Recipe> LoadRecipes()
        {
            var recipes = RecipeDbContext._DbContext._Recipes.ToList();
            return recipes;
        }

       
    }
}
