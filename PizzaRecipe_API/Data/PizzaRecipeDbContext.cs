using Microsoft.EntityFrameworkCore;
using PizzaRecipe_API.Models;

namespace PizzaRecipe_API.Data
{
    public class PizzaRecipeDbContext : DbContext
    {
        public PizzaRecipeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Pizza> Pizzas { get; set; }
    }
}
