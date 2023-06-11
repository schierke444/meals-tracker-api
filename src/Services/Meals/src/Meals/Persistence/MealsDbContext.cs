using BuildingBlocks.EFCore;
using Meals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Meals.Persistence;

public class MealsDbContext : ApplicationDbContextBase
{
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public MealsDbContext(IConfiguration config) : base(config)
    {
    }

    public MealsDbContext(DbContextOptions options, IConfiguration config) : base(options, config)
    {
    }
}