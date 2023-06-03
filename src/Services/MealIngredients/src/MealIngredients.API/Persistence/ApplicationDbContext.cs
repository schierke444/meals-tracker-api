using BuildingBlocks.EFCore;
using MealIngredients.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealIngredients.API.Persistence;

public sealed class ApplicationDbContext : ApplicationDbContextBase
{
    public DbSet<MealIngredient> MealIngredients => Set<MealIngredient>();
    private readonly IConfiguration _config;

    public ApplicationDbContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["ConnectionStrings:DB"]);
        base.OnConfiguring(optionsBuilder);
    }
}