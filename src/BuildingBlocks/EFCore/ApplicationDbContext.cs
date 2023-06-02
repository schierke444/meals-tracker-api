using BuildingBlocks.Commons.Models;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.EFCore;

public abstract class ApplicationDbContextBase : DbContext 
{
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