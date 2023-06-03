using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.EFCore;
using Ingredients.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ingredients.API.Persistence;

public sealed class ApplicationDbContext : ApplicationDbContextBase 
{
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
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