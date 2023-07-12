using Meals.Entities;

namespace Meals.Commons.Interfaces;

public interface IUsersMealsRepository
{
    Task<UsersMeals?> GetUserById(Guid Id);
    Task Add(UsersMeals entity);
    void Remove(UsersMeals entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}