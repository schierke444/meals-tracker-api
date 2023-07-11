using Posts.Entities;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Repositories;

public interface IUsersPostsRepository
{
    Task<UsersPosts?> GetUserById(Guid Id);
    Task Add(UsersPosts entity);
    void Remove(UsersPosts entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}