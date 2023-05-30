using Meals.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meals.API.Persistence;

public interface IApplicationDbContext
{
    DbSet<Meal> Meals { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
