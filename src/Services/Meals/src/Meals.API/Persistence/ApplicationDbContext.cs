using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Meals.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meals.API.Persistence;

public class ApplicationDbContext : ApplicationDbContextBase 
{
    public DbSet<Meal> Meals => Set<Meal>();
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
