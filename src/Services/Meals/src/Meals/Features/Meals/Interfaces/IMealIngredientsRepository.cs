using BuildingBlocks.Commons.Interfaces;
using Meals.Entities;

namespace Meals.Features.Meals.Interfaces;

public interface IMealIngredientsRepository : IReadRepository<MealIngredients>, IWriteRepository<MealIngredients>
{
    
}