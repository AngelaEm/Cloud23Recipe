using FamEmanuelssonsRecept.Helpers;
using FamEmanuelssonsRecept.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FamEmanuelssonsRecept.Db
{
    internal class RecipeDbContext : DbContext
    {
        private static RecipeDbContext? _dbContext;

        public static RecipeDbContext _DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new RecipeDbContext();
                }
                return _dbContext;
            }
        }

        public DbSet<Recipe> _Recipes { get; set; }
        public DbSet<Category>? _Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Encryptor.GetConnectionString());
        }

}
}
