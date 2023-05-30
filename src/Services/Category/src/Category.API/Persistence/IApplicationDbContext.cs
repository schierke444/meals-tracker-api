using Microsoft.EntityFrameworkCore;
using CategoryEntity = Category.API.Entities.Category;

namespace Category.API.Persistence;

public interface IApplicationDbContext
{
    DbSet<CategoryEntity> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
