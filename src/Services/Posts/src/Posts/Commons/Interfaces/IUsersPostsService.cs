using Posts.Entities;

namespace Posts.Commons.Interfaces;

public interface IUsersPostsService
{
    Task<UsersPosts> CreateUsersRecord(Guid Id, string Username, CancellationToken cancellationToken = default);
}