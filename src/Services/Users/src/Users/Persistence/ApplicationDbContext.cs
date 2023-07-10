using System.Reflection;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Users.Entities;

namespace Users.Persistence;

public class ApplicationDbContext : ApplicationDbContextBase 
{
    public DbSet<User> Users => Set<User>();
    public ApplicationDbContext(IConfiguration config) : base(config)
    {
    }

    public ApplicationDbContext(DbContextOptions options, IConfiguration config) : base(options, config)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}