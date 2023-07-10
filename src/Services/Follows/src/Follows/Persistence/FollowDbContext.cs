using System.Reflection;
using BuildingBlocks.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Follows.Persistence;

public class FollowDbContext: ApplicationDbContextBase 
{
    public DbSet<Entities.Follows> Follows => Set<Entities.Follows>();
    public FollowDbContext(DbContextOptions options, IConfiguration config) : base(options, config)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}