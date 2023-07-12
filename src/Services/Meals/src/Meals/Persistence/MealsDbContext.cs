using System.Reflection;
using BuildingBlocks.EFCore;
using Meals.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Meals.Persistence;

public class MealsDbContext : ApplicationDbContextBase
{
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Entities.Category> Categories => Set<Entities.Category>();
    public DbSet<MealCategory> MealCategories => Set<MealCategory>(); 
    public DbSet<MealIngredients> MealIngredients => Set<MealIngredients>();
    public DbSet<UsersMeals> UsersMeals => Set<UsersMeals>();
    public MealsDbContext(IConfiguration config) : base(config)
    {
    }

    public MealsDbContext(DbContextOptions options, IConfiguration config) : base(options, config)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}