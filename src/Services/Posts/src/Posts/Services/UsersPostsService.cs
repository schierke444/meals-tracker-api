using Posts.Commons.Interfaces;
using Posts.Entities;

namespace Posts.Services;

public class UsersPostsService : IUsersPostsService
{
    private readonly IUsersPostsRepository _usersPostsRepository;
    public UsersPostsService(IUsersPostsRepository usersPostsRepository)
    {
        _usersPostsRepository = usersPostsRepository;
    }

    public async Task<UsersPosts> CreateUsersRecord(Guid Id, string Username, CancellationToken cancellationToken = default)
    {
        var existingUser = await _usersPostsRepository.GetUserById(Id);

        // if no users_posts record, make a new one, then assigned to usersPosts;
        // else assign to usersPosts
        UsersPosts user;
        if (existingUser is null)
        {
            var result = await CreateUsersPostsRecord(Id, Username);
            user = result;
        }
        else
        {
            user = existingUser;
        }

        return user;
    }

    private async Task<UsersPosts> CreateUsersPostsRecord(Guid Id, string Username, CancellationToken cancellationToken = default)
    {

        UsersPosts newUser = new()
        {
            UserId = Id,
            Username = Username
        };

        await _usersPostsRepository.Add(newUser);
        await _usersPostsRepository.SaveChangesAsync(cancellationToken);

        return newUser;
    }
}


