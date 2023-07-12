using Meals.Entities;

namespace Meals.Commons.Interfaces;

public interface IUsersMealsService
{
    Task<UsersMeals> CreateUsersRecord(Guid Id, string Username, CancellationToken cancellationToken = default);
}