using Follows.Entities;

namespace Follows.Features.Interfaces;

public interface IUsersFollowsRepository
{
    Task<UsersFollows?> GetUserById(Guid Id);
    Task Add(UsersFollows entity);
    void Remove(UsersFollows entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}