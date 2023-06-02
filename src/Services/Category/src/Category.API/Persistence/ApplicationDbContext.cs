using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Category.API.Persistence;

public class ApplicationDbContext : ApplicationDbContextBase
{
    public DbSet<Entities.Category> Categories => Set<Entities.Category>();
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
