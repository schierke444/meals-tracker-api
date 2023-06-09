using BuildingBlocks.Commons.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.EFCore;

public abstract class ApplicationDbContextBase : DbContext 
{
    private readonly IConfiguration _config;

    protected ApplicationDbContextBase(IConfiguration config)
    {
        _config = config;
    }

    protected ApplicationDbContextBase(DbContextOptions options, IConfiguration config) : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<BaseEntity>())
        {
            if(item.State == EntityState.Added)
            {
                item.Entity.CreatedAt = DateTime.UtcNow;
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
            }
            if(item.State == EntityState.Modified)
            {
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}