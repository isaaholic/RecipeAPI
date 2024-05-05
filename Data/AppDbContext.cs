using Microsoft.EntityFrameworkCore;
using RecipeAPI.Models;

namespace RecipeAPI.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Recipe> Recipes { get; set; }
}
