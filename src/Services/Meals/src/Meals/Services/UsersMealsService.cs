using Meals.Commons.Interfaces;
using Meals.Entities;

namespace Meals.Services;

public class UsersMealsService : IUsersMealsService 
{
    private readonly IUsersMealsRepository _usersMealsRepository;
    public UsersMealsService(IUsersMealsRepository usersMealsRepository)
    {
        _usersMealsRepository = usersMealsRepository;
    }

    public async Task<UsersMeals> CreateUsersRecord(Guid Id, string Username, CancellationToken cancellationToken = default)
    {
        var existingUser = await _usersMealsRepository.GetUserById(Id);

        // if no users_posts record, make a new one, then assigned to usersPosts;
        // else assign to usersPosts
        UsersMeals user;
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

    private async Task<UsersMeals> CreateUsersPostsRecord(Guid Id, string Username, CancellationToken cancellationToken = default)
    {

        UsersMeals newUser = new()
        {
            UserId = Id,
            Username = Username
        };

        await _usersMealsRepository.Add(newUser);
        await _usersMealsRepository.SaveChangesAsync(cancellationToken);

        return newUser;
    }
}


