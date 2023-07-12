using Posts.Entities;

namespace Posts.Commons.Interfaces;

public interface IUsersPostsRepository
{
    Task<UsersPosts?> GetUserById(Guid Id);
    Task Add(UsersPosts entity);
    void Remove(UsersPosts entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}