using FamEmanuelssonsRecept.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FamEmanuelssonsRecept;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


    }
}
