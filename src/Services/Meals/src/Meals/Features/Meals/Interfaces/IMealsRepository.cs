using BuildingBlocks.Commons.Interfaces;
using Meals.Entities;
using Meals.Features.Meals.Dtos;

namespace Meals.Commons.Interfaces;

public interface IMealsRepository : IReadRepository<Meal>, IWriteRepository<Meal>
{
    Task<IEnumerable<MealsDto>> GetAllMeals();
    Task<MealDetailsDto> GetMealsById(string MealId);
    Task<IEnumerable<MealsDto>> GetAllMealsByOwnerId(string OwnerId);
}