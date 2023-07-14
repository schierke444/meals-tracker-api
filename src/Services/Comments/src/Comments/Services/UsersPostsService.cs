using Comments.Commons.Interfaces;
using Comments.Entities;

namespace Comments.Services;

public class UsersCommentsService : IUsersCommentsService
{
    private readonly IUsersCommentsRepository _usersCommentsRepository;
    public UsersCommentsService(IUsersCommentsRepository usersCommentsRepository)
    {
        _usersCommentsRepository = usersCommentsRepository;
    }

    public async Task<UsersComments> CreateUsersRecord(string Id, string Username, CancellationToken cancellationToken = default)
    {
        var existingUser = await _usersCommentsRepository.GetUserById(Id);

        // if no users_posts record, make a new one, then assigned to usersPosts;
        // else assign to usersPosts
        UsersComments user;
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

    private async Task<UsersComments> CreateUsersPostsRecord(string Id, string Username, CancellationToken cancellationToken = default)
    {

        UsersComments newUser = new()
        {
            UserId = Guid.Parse(Id),
            Username = Username
        };

        await _usersCommentsRepository.Add(newUser);
        await _usersCommentsRepository.SaveChangesAsync(cancellationToken);

        return newUser;
    }
}


