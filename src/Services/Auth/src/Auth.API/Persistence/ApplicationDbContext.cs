using Auth.API.Entity;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Persistence;

public class ApplicationDbContext : ApplicationDbContextBase 
{
    public DbSet<User> Users => Set<User>();
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
