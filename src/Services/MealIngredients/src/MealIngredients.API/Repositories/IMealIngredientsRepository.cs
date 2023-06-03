using BuildingBlocks.Commons.Interfaces;
using MealIngredients.API.Entities;

namespace MealIngredients.API.Repositories;

public interface IMealIngredientsRepository : IRepositoryBase<MealIngredient>
{
    Task AddBulkMealIngredients(IEnumerable<MealIngredient> mealIngredients);
}