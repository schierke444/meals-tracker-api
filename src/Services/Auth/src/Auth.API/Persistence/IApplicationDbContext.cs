using Auth.API.Entity;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Persistence;

public interface IApplicationDbContext
{
    DbSet<User> Users { get;  }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
