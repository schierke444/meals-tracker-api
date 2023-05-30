﻿using Auth.API.Entity;
using BuildingBlocks.Commons.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
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
